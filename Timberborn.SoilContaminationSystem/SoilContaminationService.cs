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

namespace Timberborn.SoilContaminationSystem
{
	// Token: 0x0200000D RID: 13
	[MapEditorTickable]
	public class SoilContaminationService : ISoilContaminationService, ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public SoilContaminationService(SoilContaminationSimulator soilContaminationSimulator, MapIndexService mapIndexService, IThreadSafeColumnTerrainMap threadSafeColumnTerrainMap, TerrainMaterialMap terrainMaterialMap, IBlockService blockService, ISpecService specService, SimulationController simulationController, ITerrainService terrainService)
		{
			this._soilContaminationSimulator = soilContaminationSimulator;
			this._mapIndexService = mapIndexService;
			this._threadSafeColumnTerrainMap = threadSafeColumnTerrainMap;
			this._terrainMaterialMap = terrainMaterialMap;
			this._blockService = blockService;
			this._specService = specService;
			this._simulationController = simulationController;
			this._terrainService = terrainService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C28 File Offset: 0x00000E28
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			SoilContaminationMapSpec singleSpec = this._specService.GetSingleSpec<SoilContaminationMapSpec>();
			this._maxMapContamination = singleSpec.MaxMapContamination;
			this._contaminationThreshold = singleSpec.ContaminationThreshold;
			this.InitializeContamination();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C70 File Offset: 0x00000E70
		public void Tick()
		{
			int length = this._soilContaminationSimulator.ContaminationLevels.Length;
			Array.Resize<float>(ref this._threadSafeContaminationLevels, length);
			if (this._simulationController.ShouldResetSimulation)
			{
				this.Reset();
				return;
			}
			this.UpdateContaminationLevels();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public float Contamination(int index)
		{
			return this._threadSafeContaminationLevels[index];
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public bool SoilIsContaminated(Vector3Int coordinates)
		{
			int index2D = this._mapIndexService.CellToIndex(coordinates.XY());
			int num;
			return this._threadSafeColumnTerrainMap.TryGetIndexAtCeiling(index2D, coordinates.z, out num) && this._threadSafeContaminationLevels[num] > 0f;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D0C File Offset: 0x00000F0C
		public void InitializeContamination()
		{
			ReadOnlySpan<float> contaminationLevels = this._soilContaminationSimulator.ContaminationLevels;
			this._threadSafeContaminationLevels = new float[contaminationLevels.Length];
			contaminationLevels.CopyTo(this._threadSafeContaminationLevels);
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int columnCount = this._terrainService.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int num2 = num + i * this._verticalStride;
					int columnCeiling = this._terrainService.GetColumnCeiling(num2);
					Vector3Int coordinates = this._mapIndexService.IndexToCoordinates(num, columnCeiling);
					this.UpdateContamination(coordinates, this._threadSafeContaminationLevels[num2]);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DCC File Offset: 0x00000FCC
		public void Reset()
		{
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int columnCount = this._terrainService.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int index3D = num + i * this._verticalStride;
					int columnCeiling = this._terrainService.GetColumnCeiling(index3D);
					Vector3Int coordinates = this._mapIndexService.IndexToCoordinates(num, columnCeiling);
					this.SetContaminationLevel(coordinates, index3D, 0f);
				}
			}
			this._terrainMaterialMap.ResetContaminationMap();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E64 File Offset: 0x00001064
		public unsafe void UpdateContaminationLevels()
		{
			ReadOnlySpan<bool> contaminationsChangedLastTick = this._soilContaminationSimulator.ContaminationsChangedLastTick;
			ReadOnlySpan<float> contaminationLevels = this._soilContaminationSimulator.ContaminationLevels;
			foreach (int num in this._mapIndexService.Indices2D)
			{
				int columnCount = this._terrainService.GetColumnCount(num);
				for (int i = 0; i < columnCount; i++)
				{
					int num2 = num + i * this._verticalStride;
					if (*contaminationsChangedLastTick[num2] != 0)
					{
						int columnCeiling = this._terrainService.GetColumnCeiling(num2);
						Vector3Int coordinates = this._mapIndexService.IndexToCoordinates(num, columnCeiling);
						this.SetContaminationLevel(coordinates, num2, *contaminationLevels[num2]);
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F20 File Offset: 0x00001120
		public void SetContaminationLevel(Vector3Int coordinates, int index3D, float newLevel)
		{
			float num = this._threadSafeContaminationLevels[index3D];
			if (newLevel > 0f && num <= 0f)
			{
				ContaminatedObject contaminatedObjectAt = this.GetContaminatedObjectAt(coordinates);
				if (contaminatedObjectAt != null)
				{
					contaminatedObjectAt.EnterContaminatedState();
				}
			}
			else if (newLevel <= 0f && num > 0f)
			{
				ContaminatedObject contaminatedObjectAt2 = this.GetContaminatedObjectAt(coordinates);
				if (contaminatedObjectAt2 != null)
				{
					contaminatedObjectAt2.ExitContaminatedState();
				}
			}
			this.UpdateContamination(coordinates, newLevel);
			this._threadSafeContaminationLevels[index3D] = newLevel;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F8D File Offset: 0x0000118D
		public ContaminatedObject GetContaminatedObjectAt(Vector3Int coordinates)
		{
			return this._blockService.GetBottomObjectComponentAt<ContaminatedObject>(coordinates);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F9B File Offset: 0x0000119B
		public void UpdateContamination(Vector3Int coordinates, float contamination)
		{
			this._terrainMaterialMap.SetSoilContamination(coordinates, this.GetMapSoilContamination(contamination));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002FB0 File Offset: 0x000011B0
		public float GetMapSoilContamination(float contamination)
		{
			if (contamination <= 0f)
			{
				return 0f;
			}
			if (contamination <= this._contaminationThreshold)
			{
				float num = 1f - contamination / this._contaminationThreshold;
				return 1f - this._maxMapContamination * num;
			}
			return 1f;
		}

		// Token: 0x04000031 RID: 49
		public readonly SoilContaminationSimulator _soilContaminationSimulator;

		// Token: 0x04000032 RID: 50
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000033 RID: 51
		public readonly IThreadSafeColumnTerrainMap _threadSafeColumnTerrainMap;

		// Token: 0x04000034 RID: 52
		public readonly TerrainMaterialMap _terrainMaterialMap;

		// Token: 0x04000035 RID: 53
		public readonly IBlockService _blockService;

		// Token: 0x04000036 RID: 54
		public readonly ISpecService _specService;

		// Token: 0x04000037 RID: 55
		public readonly SimulationController _simulationController;

		// Token: 0x04000038 RID: 56
		public readonly ITerrainService _terrainService;

		// Token: 0x04000039 RID: 57
		public float[] _threadSafeContaminationLevels;

		// Token: 0x0400003A RID: 58
		public int _verticalStride;

		// Token: 0x0400003B RID: 59
		public float _maxMapContamination;

		// Token: 0x0400003C RID: 60
		public float _contaminationThreshold;
	}
}
