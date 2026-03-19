using System;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SimulationSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TerrainSystemRendering;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000010 RID: 16
	[MapEditorTickable]
	public class SoilMoistureService : ISoilMoistureService, ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003105 File Offset: 0x00001305
		public SoilMoistureService(SoilMoistureSimulator soilMoistureSimulator, MapIndexService mapIndexService, IThreadSafeColumnTerrainMap threadSafeColumnTerrainMap, IBlockService blockService, TerrainMaterialMap terrainMaterialMap, SimulationController simulationController, ISpecService specService)
		{
			this._soilMoistureSimulator = soilMoistureSimulator;
			this._mapIndexService = mapIndexService;
			this._threadSafeColumnTerrainMap = threadSafeColumnTerrainMap;
			this._blockService = blockService;
			this._terrainMaterialMap = terrainMaterialMap;
			this._simulationController = simulationController;
			this._specService = specService;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003144 File Offset: 0x00001344
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			SoilMoistureMapSpec singleSpec = this._specService.GetSingleSpec<SoilMoistureMapSpec>();
			this._maxDesertIntensity = singleSpec.MaxDesertIntensity;
			this._desertMoistureThreshold = singleSpec.DesertMoistureThreshold;
			this.InitializeDesertIntensity();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000318C File Offset: 0x0000138C
		public void Tick()
		{
			Array.Resize<float>(ref this._threadSafeMoistureLevels, this._soilMoistureSimulator.MoistureLevels.Length);
			if (this._simulationController.ShouldResetSimulation)
			{
				this.Reset();
				return;
			}
			this.UpdateMoistureLevels();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031D4 File Offset: 0x000013D4
		public bool SoilIsMoist(Vector3Int coordinates)
		{
			int index2D = this._mapIndexService.CellToIndex(coordinates.XY());
			int num;
			return this._threadSafeColumnTerrainMap.TryGetIndexAtCeiling(index2D, coordinates.z, out num) && this._threadSafeMoistureLevels[num] > 0f;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000321B File Offset: 0x0000141B
		public float SoilMoisture(int index)
		{
			return this._threadSafeMoistureLevels[index];
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003228 File Offset: 0x00001428
		public void InitializeDesertIntensity()
		{
			ReadOnlySpan<float> moistureLevels = this._soilMoistureSimulator.MoistureLevels;
			this._threadSafeMoistureLevels = new float[moistureLevels.Length];
			moistureLevels.CopyTo(this._threadSafeMoistureLevels);
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int columnCount = this._threadSafeColumnTerrainMap.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int num2 = num + i * this._verticalStride;
					int columnCeiling = this._threadSafeColumnTerrainMap.GetColumnCeiling(num2);
					Vector3Int coordinates = this._mapIndexService.IndexToCoordinates(num, columnCeiling);
					this.UpdateDesertIntensity(coordinates, this._threadSafeMoistureLevels[num2]);
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032E8 File Offset: 0x000014E8
		public void Reset()
		{
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int columnCount = this._threadSafeColumnTerrainMap.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int index3D = num + i * this._verticalStride;
					int columnCeiling = this._threadSafeColumnTerrainMap.GetColumnCeiling(index3D);
					Vector3Int coordinates = this._mapIndexService.IndexToCoordinates(num, columnCeiling);
					this.SetMoistureLevel(coordinates, index3D, 0f);
				}
			}
			this._terrainMaterialMap.ResetDesertMap();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003380 File Offset: 0x00001580
		public unsafe void UpdateMoistureLevels()
		{
			ReadOnlySpan<float> moistureLevels = this._soilMoistureSimulator.MoistureLevels;
			ReadOnlySpan<bool> moistureLevelsChangedLastTick = this._soilMoistureSimulator.MoistureLevelsChangedLastTick;
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int columnCount = this._threadSafeColumnTerrainMap.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int num2 = num + i * this._verticalStride;
					if (*moistureLevelsChangedLastTick[num2] != 0)
					{
						int columnCeiling = this._threadSafeColumnTerrainMap.GetColumnCeiling(num2);
						Vector3Int coordinates = this._mapIndexService.IndexToCoordinates(num, columnCeiling);
						this.SetMoistureLevel(coordinates, num2, *moistureLevels[num2]);
					}
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000343C File Offset: 0x0000163C
		public void SetMoistureLevel(Vector3Int coordinates, int index3D, float newLevel)
		{
			float num = this._threadSafeMoistureLevels[index3D];
			if (newLevel > 0f && num <= 0f)
			{
				DryObject dryObjectAt = this.GetDryObjectAt(coordinates);
				if (dryObjectAt != null)
				{
					dryObjectAt.ExitDryState();
				}
			}
			else if (newLevel <= 0f && num > 0f)
			{
				DryObject dryObjectAt2 = this.GetDryObjectAt(coordinates);
				if (dryObjectAt2 != null)
				{
					dryObjectAt2.EnterDryState();
				}
			}
			this.UpdateDesertIntensity(coordinates, newLevel);
			this._threadSafeMoistureLevels[index3D] = newLevel;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000034A9 File Offset: 0x000016A9
		public DryObject GetDryObjectAt(Vector3Int coordinates)
		{
			return this._blockService.GetBottomObjectComponentAt<DryObject>(coordinates);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000034B8 File Offset: 0x000016B8
		public void UpdateDesertIntensity(Vector3Int coordinates, float moistureLevel)
		{
			float num;
			if (moistureLevel == 0f)
			{
				num = 1f;
			}
			else if (moistureLevel <= (float)this._desertMoistureThreshold)
			{
				num = 1f - moistureLevel / (float)this._desertMoistureThreshold;
				num *= this._maxDesertIntensity;
			}
			else
			{
				num = 0f;
			}
			this._terrainMaterialMap.SetDesertIntensity(coordinates, num);
		}

		// Token: 0x04000034 RID: 52
		public readonly SoilMoistureSimulator _soilMoistureSimulator;

		// Token: 0x04000035 RID: 53
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000036 RID: 54
		public readonly IThreadSafeColumnTerrainMap _threadSafeColumnTerrainMap;

		// Token: 0x04000037 RID: 55
		public readonly IBlockService _blockService;

		// Token: 0x04000038 RID: 56
		public readonly TerrainMaterialMap _terrainMaterialMap;

		// Token: 0x04000039 RID: 57
		public readonly SimulationController _simulationController;

		// Token: 0x0400003A RID: 58
		public readonly ISpecService _specService;

		// Token: 0x0400003B RID: 59
		public float[] _threadSafeMoistureLevels;

		// Token: 0x0400003C RID: 60
		public int _verticalStride;

		// Token: 0x0400003D RID: 61
		public float _maxDesertIntensity;

		// Token: 0x0400003E RID: 62
		public int _desertMoistureThreshold;
	}
}
