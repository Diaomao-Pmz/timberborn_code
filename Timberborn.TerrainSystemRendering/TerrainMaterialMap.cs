using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000011 RID: 17
	[MapEditorTickable]
	public class TerrainMaterialMap : ITickableSingleton, ILoadableSingleton, IPostLoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002EB0 File Offset: 0x000010B0
		public TerrainMaterialMap(ITerrainService terrainService, ISpecService specService)
		{
			this._terrainService = terrainService;
			this._specService = specService;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F08 File Offset: 0x00001108
		public void LateUpdateSingleton()
		{
			if (this._fieldOrCutoutTextureInvalid)
			{
				this._fieldOrCutoutTextureInvalid = false;
				this.UpdateFieldAndCutoutTexture();
			}
			if (this._desertIntensityLayersToUpdate.Count > 0)
			{
				this.ApplyDesertIntensityMapChanges();
				this._desertIntensityLayersToUpdate.Clear();
			}
			if (this._cutoutAndFieldLayersToUpdate.Count > 0)
			{
				this.ApplyCutoutAndFieldMapChanges();
				this._cutoutAndFieldLayersToUpdate.Clear();
			}
			if (this._contaminationLayersToUpdate.Count > 0)
			{
				this.ApplyContaminationTextureChanges();
				this._contaminationLayersToUpdate.Clear();
			}
			if (Application.isEditor)
			{
				this.UpdateShaderProperties();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F94 File Offset: 0x00001194
		public void Tick()
		{
			this.ProcessDesertTextureChanges();
			this.ProcessContaminationTextureChanges();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002FA4 File Offset: 0x000011A4
		public void Load()
		{
			this._terrainMaterialMapSpec = this._specService.GetSingleSpec<TerrainMaterialMapSpec>();
			this.InitializeTextures();
			this._terrainService.FieldOrCutoutChanged += this.OnFieldOrCutoutChanged;
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002FF6 File Offset: 0x000011F6
		public void PostLoad()
		{
			this.UpdateFieldAndCutoutTexture();
			this.ProcessDesertTextureChanges();
			this.ProcessDesertTextureChanges();
			this.ProcessContaminationTextureChanges();
			this.ProcessContaminationTextureChanges();
			this.UpdateShaderProperties();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000301C File Offset: 0x0000121C
		public void ResetDesertMap()
		{
			for (int i = 0; i < this._textureSize.z; i++)
			{
				int num = this._desertIntensityMap[i].Length;
				for (int j = 0; j < num; j++)
				{
					this._desertIntensityMap[i][j] = new PixelData(1f, 1f);
				}
				this._desertIntensityLayersToUpdate.Add((byte)i);
			}
			this._desertMapChanges.Clear();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000308C File Offset: 0x0000128C
		public void ResetContaminationMap()
		{
			for (int i = 0; i < this._textureSize.z; i++)
			{
				int num = this._contaminations[i].Length;
				for (int j = 0; j < num; j++)
				{
					this._contaminations[i][j] = new PixelData(0f, 0f);
				}
				this._contaminationLayersToUpdate.Add((byte)i);
			}
			this._contaminationMapChanges.Clear();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000030FC File Offset: 0x000012FC
		public void SetDesertIntensity(Vector3Int coordinates, float desertIntensity)
		{
			float desertIntensity2 = this.GetDesertIntensity(coordinates);
			if (Math.Abs(desertIntensity2 - desertIntensity) > TerrainMaterialMap.ChangeThreshold)
			{
				this._desertMapChanges.Enqueue(new TerrainMaterialMap.MapDataChange(coordinates, desertIntensity2, desertIntensity));
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003134 File Offset: 0x00001334
		public void SetSoilContamination(Vector3Int coordinates, float contamination)
		{
			float gnormalized = this._contaminations[coordinates.z][coordinates.y * this._textureSize.x + coordinates.x].GNormalized;
			if (Math.Abs(gnormalized - contamination) > TerrainMaterialMap.ChangeThreshold)
			{
				this._contaminationMapChanges.Enqueue(new TerrainMaterialMap.MapDataChange(coordinates, gnormalized, contamination));
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003197 File Offset: 0x00001397
		public float GetDesertIntensity(Vector3Int coordinates)
		{
			return this._desertIntensityMap[coordinates.z][coordinates.y * this._textureSize.x + coordinates.x].GNormalized;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000031CC File Offset: 0x000013CC
		public void InitializeTextures()
		{
			this._textureSize = this._terrainService.Size;
			this._textureScaleAsVector = new Vector4(1f / (float)this._textureSize.x, 1f / (float)this._textureSize.y);
			this._desertIntensityMapTexture = new Texture2DArray(this._textureSize.x, this._textureSize.y, this._textureSize.z, 62, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
			this._cutoutAndFieldMapTexture = new Texture2DArray(this._textureSize.x, this._textureSize.y, this._textureSize.z, 62, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
			this._contaminationMapTexture = new Texture2DArray(this._textureSize.x, this._textureSize.y, this._textureSize.z, 62, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
			this._bufferTexture = new Texture2D(this._textureSize.x, this._textureSize.y, 62, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
			this._desertIntensityMap = new PixelData[this._textureSize.z][];
			this._cutoutAndFieldMap = new PixelData[this._textureSize.z][];
			this._contaminations = new PixelData[this._textureSize.z][];
			for (int i = 0; i < this._textureSize.z; i++)
			{
				this._desertIntensityMap[i] = new PixelData[this._textureSize.x * this._textureSize.y];
				for (int j = 0; j < this._textureSize.x * this._textureSize.y; j++)
				{
					this._desertIntensityMap[i][j] = new PixelData(1f, 1f);
				}
				this._contaminations[i] = new PixelData[this._textureSize.x * this._textureSize.y];
				this._cutoutAndFieldMap[i] = new PixelData[this._textureSize.x * this._textureSize.y];
				this._contaminationLayersToUpdate.Add((byte)i);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000341C File Offset: 0x0000161C
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			if (!change.SetTerrain)
			{
				for (int i = change.To + 1; i > change.From; i--)
				{
					Vector3Int coordinates = change.Coordinates.ToVector3Int(i);
					this._desertMapChanges.Enqueue(new TerrainMaterialMap.MapDataChange(coordinates, 1f, 1f));
					this._contaminationMapChanges.Enqueue(new TerrainMaterialMap.MapDataChange(coordinates, 0f, 0f));
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003498 File Offset: 0x00001698
		public void ProcessDesertTextureChanges()
		{
			int count = this._desertMapChanges.Count;
			int num = 0;
			while (num++ < count)
			{
				TerrainMaterialMap.MapDataChange mapDataChange = this._desertMapChanges.Dequeue();
				this.UpdateDesertTexture(mapDataChange);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000034D4 File Offset: 0x000016D4
		public void ProcessContaminationTextureChanges()
		{
			int count = this._contaminationMapChanges.Count;
			int num = 0;
			while (num++ < count)
			{
				TerrainMaterialMap.MapDataChange mapDataChange = this._contaminationMapChanges.Dequeue();
				this.UpdateContaminationTexture(mapDataChange);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003510 File Offset: 0x00001710
		public void UpdateDesertTexture(in TerrainMaterialMap.MapDataChange mapDataChange)
		{
			float oldValue = mapDataChange.OldValue;
			float newValue = mapDataChange.NewValue;
			Vector3Int coordinates = mapDataChange.Coordinates;
			this._desertIntensityMap[coordinates.z][coordinates.y * this._textureSize.x + coordinates.x] = new PixelData(oldValue, newValue);
			this._desertIntensityLayersToUpdate.Add((byte)coordinates.z);
			if (newValue != oldValue)
			{
				this._desertMapChanges.Enqueue(new TerrainMaterialMap.MapDataChange(coordinates, newValue, newValue));
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003594 File Offset: 0x00001794
		public void UpdateContaminationTexture(in TerrainMaterialMap.MapDataChange contaminationMapChange)
		{
			float oldValue = contaminationMapChange.OldValue;
			float newValue = contaminationMapChange.NewValue;
			Vector3Int coordinates = contaminationMapChange.Coordinates;
			this._contaminations[coordinates.z][coordinates.y * this._textureSize.x + coordinates.x] = new PixelData(oldValue, newValue);
			this._contaminationLayersToUpdate.Add((byte)coordinates.z);
			if (newValue != oldValue)
			{
				this._contaminationMapChanges.Enqueue(new TerrainMaterialMap.MapDataChange(coordinates, newValue, newValue));
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003618 File Offset: 0x00001818
		public void OnFieldOrCutoutChanged(object sender, Vector3Int coordinates)
		{
			float r = this._terrainService.CellIsField(coordinates) ? 1f : 0f;
			float g = this._terrainService.CellIsCutout(coordinates) ? 0f : 1f;
			this._cutoutAndFieldMap[coordinates.z][coordinates.y * this._textureSize.x + coordinates.x] = new PixelData(r, g);
			this._cutoutAndFieldLayersToUpdate.Add((byte)coordinates.z);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036A4 File Offset: 0x000018A4
		public void UpdateFieldAndCutoutTexture()
		{
			for (int i = 0; i < this._textureSize.z; i++)
			{
				for (int j = 0; j < this._textureSize.y; j++)
				{
					for (int k = 0; k < this._textureSize.x; k++)
					{
						Vector3Int cellCoordinates;
						cellCoordinates..ctor(k, j, i);
						float r = this._terrainService.CellIsField(cellCoordinates) ? 1f : 0f;
						float g = this._terrainService.CellIsCutout(cellCoordinates) ? 0f : 1f;
						this._cutoutAndFieldMap[i][j * this._textureSize.x + k] = new PixelData(r, g);
					}
				}
				this._cutoutAndFieldLayersToUpdate.Add((byte)i);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003774 File Offset: 0x00001974
		public void UpdateShaderProperties()
		{
			Shader.SetGlobalFloat(TerrainMaterialMap.ProjectionUVScaleProperty, 1f / (float)WorldTiling.HorizontalTileSize);
			Shader.SetGlobalVector(TerrainMaterialMap.TerrainMaterialMapScaleProperty, this._textureScaleAsVector);
			Shader.SetGlobalTexture(TerrainMaterialMap.DesertIntensityMapProperty, this._desertIntensityMapTexture);
			Shader.SetGlobalTexture(TerrainMaterialMap.CutoutAndFieldMapProperty, this._cutoutAndFieldMapTexture);
			Shader.SetGlobalTexture(TerrainMaterialMap.ContaminationMapProperty, this._contaminationMapTexture);
			Shader.SetGlobalTexture(TerrainMaterialMap.DesertTextureProperty, this._terrainMaterialMapSpec.DesertTexture.Asset);
			Shader.SetGlobalTexture(TerrainMaterialMap.WetFieldTextureProperty, this._terrainMaterialMapSpec.WetFieldTexture.Asset);
			Shader.SetGlobalTexture(TerrainMaterialMap.DryFieldTextureProperty, this._terrainMaterialMapSpec.DryFieldTexture.Asset);
			Shader.SetGlobalTexture(TerrainMaterialMap.BlendingNoiseProperty, this._terrainMaterialMapSpec.BlendingNoise.Asset);
			Shader.SetGlobalFloat(TerrainMaterialMap.BlendingNoiseScaleProperty, this._terrainMaterialMapSpec.BlendingNoiseScale);
			Shader.SetGlobalFloat(TerrainMaterialMap.BlendingNoiseMultiplierProperty, this._terrainMaterialMapSpec.BlendingNoiseMultiplier);
			Shader.SetGlobalFloat(TerrainMaterialMap.BlendingSoftnessProperty, this._terrainMaterialMapSpec.BlendingSoftness);
			Shader.SetGlobalFloat(TerrainMaterialMap.BlendingMarginProperty, this._terrainMaterialMapSpec.BlendingMargin);
			Shader.SetGlobalFloat(TerrainMaterialMap.AltitudeCeilingProperty, this._terrainMaterialMapSpec.AltitudeCeiling);
			Shader.SetGlobalTexture(TerrainMaterialMap.AltitudeMultiplierProperty, this._terrainMaterialMapSpec.AltitudeMultiplier.Asset);
			Shader.SetGlobalTexture(TerrainMaterialMap.DesertAltitudeMultiplierProperty, this._terrainMaterialMapSpec.DesertAltitudeMultiplier.Asset);
			Shader.SetGlobalFloat(TerrainMaterialMap.CutoutMarginProperty, this._terrainMaterialMapSpec.CutoutMargin);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000038F4 File Offset: 0x00001AF4
		public void ApplyDesertIntensityMapChanges()
		{
			foreach (byte b in this._desertIntensityLayersToUpdate)
			{
				this._bufferTexture.SetPixelData<PixelData>(this._desertIntensityMap[(int)b], 0, 0);
				this._bufferTexture.Apply(false, false);
				Graphics.CopyTexture(this._bufferTexture, 0, 0, this._desertIntensityMapTexture, (int)b, 0);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003978 File Offset: 0x00001B78
		public void ApplyCutoutAndFieldMapChanges()
		{
			foreach (byte b in this._cutoutAndFieldLayersToUpdate)
			{
				this._bufferTexture.SetPixelData<PixelData>(this._cutoutAndFieldMap[(int)b], 0, 0);
				this._bufferTexture.Apply(false, false);
				Graphics.CopyTexture(this._bufferTexture, 0, 0, this._cutoutAndFieldMapTexture, (int)b, 0);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000039FC File Offset: 0x00001BFC
		public void ApplyContaminationTextureChanges()
		{
			foreach (byte b in this._contaminationLayersToUpdate)
			{
				this._bufferTexture.SetPixelData<PixelData>(this._contaminations[(int)b], 0, 0);
				this._bufferTexture.Apply(false, false);
				Graphics.CopyTexture(this._bufferTexture, 0, 0, this._contaminationMapTexture, (int)b, 0);
			}
		}

		// Token: 0x04000030 RID: 48
		public static readonly int ProjectionUVScaleProperty = Shader.PropertyToID("_ProjectionUVScale");

		// Token: 0x04000031 RID: 49
		public static readonly int TerrainMaterialMapScaleProperty = Shader.PropertyToID("_TerrainMaterialMapScale");

		// Token: 0x04000032 RID: 50
		public static readonly int DesertIntensityMapProperty = Shader.PropertyToID("_DesertIntensityMap");

		// Token: 0x04000033 RID: 51
		public static readonly int CutoutAndFieldMapProperty = Shader.PropertyToID("_CutoutAndFieldMap");

		// Token: 0x04000034 RID: 52
		public static readonly int ContaminationMapProperty = Shader.PropertyToID("_ContaminationMap");

		// Token: 0x04000035 RID: 53
		public static readonly int DesertTextureProperty = Shader.PropertyToID("_DesertTex");

		// Token: 0x04000036 RID: 54
		public static readonly int WetFieldTextureProperty = Shader.PropertyToID("_WetFieldTex");

		// Token: 0x04000037 RID: 55
		public static readonly int DryFieldTextureProperty = Shader.PropertyToID("_DryFieldTex");

		// Token: 0x04000038 RID: 56
		public static readonly int BlendingNoiseProperty = Shader.PropertyToID("_BlendingNoise");

		// Token: 0x04000039 RID: 57
		public static readonly int BlendingNoiseScaleProperty = Shader.PropertyToID("_BlendingNoiseScale");

		// Token: 0x0400003A RID: 58
		public static readonly int BlendingNoiseMultiplierProperty = Shader.PropertyToID("_BlendingNoiseMultiplier");

		// Token: 0x0400003B RID: 59
		public static readonly int BlendingSoftnessProperty = Shader.PropertyToID("_BlendingSoftness");

		// Token: 0x0400003C RID: 60
		public static readonly int BlendingMarginProperty = Shader.PropertyToID("_BlendingMargin");

		// Token: 0x0400003D RID: 61
		public static readonly int AltitudeCeilingProperty = Shader.PropertyToID("_AltitudeCeiling");

		// Token: 0x0400003E RID: 62
		public static readonly int AltitudeMultiplierProperty = Shader.PropertyToID("_AltitudeMultiplierTex");

		// Token: 0x0400003F RID: 63
		public static readonly int DesertAltitudeMultiplierProperty = Shader.PropertyToID("_DesertAltitudeMultiplierTex");

		// Token: 0x04000040 RID: 64
		public static readonly int CutoutMarginProperty = Shader.PropertyToID("_CutoutMargin");

		// Token: 0x04000041 RID: 65
		public static readonly float ChangeThreshold = 0.003921569f;

		// Token: 0x04000042 RID: 66
		public readonly ITerrainService _terrainService;

		// Token: 0x04000043 RID: 67
		public readonly ISpecService _specService;

		// Token: 0x04000044 RID: 68
		public Vector4 _textureScaleAsVector;

		// Token: 0x04000045 RID: 69
		public Texture2DArray _desertIntensityMapTexture;

		// Token: 0x04000046 RID: 70
		public Texture2DArray _cutoutAndFieldMapTexture;

		// Token: 0x04000047 RID: 71
		public Texture2DArray _contaminationMapTexture;

		// Token: 0x04000048 RID: 72
		public Texture2D _bufferTexture;

		// Token: 0x04000049 RID: 73
		public Vector3Int _textureSize;

		// Token: 0x0400004A RID: 74
		public PixelData[][] _desertIntensityMap;

		// Token: 0x0400004B RID: 75
		public PixelData[][] _cutoutAndFieldMap;

		// Token: 0x0400004C RID: 76
		public PixelData[][] _contaminations;

		// Token: 0x0400004D RID: 77
		public readonly Queue<TerrainMaterialMap.MapDataChange> _desertMapChanges = new Queue<TerrainMaterialMap.MapDataChange>();

		// Token: 0x0400004E RID: 78
		public readonly Queue<TerrainMaterialMap.MapDataChange> _contaminationMapChanges = new Queue<TerrainMaterialMap.MapDataChange>();

		// Token: 0x0400004F RID: 79
		public readonly HashSet<byte> _desertIntensityLayersToUpdate = new HashSet<byte>();

		// Token: 0x04000050 RID: 80
		public readonly HashSet<byte> _cutoutAndFieldLayersToUpdate = new HashSet<byte>();

		// Token: 0x04000051 RID: 81
		public readonly HashSet<byte> _contaminationLayersToUpdate = new HashSet<byte>();

		// Token: 0x04000052 RID: 82
		public bool _fieldOrCutoutTextureInvalid;

		// Token: 0x04000053 RID: 83
		public bool _applyTextureChanges;

		// Token: 0x04000054 RID: 84
		public bool _applyContaminationTextureChanges;

		// Token: 0x04000055 RID: 85
		public TerrainMaterialMapSpec _terrainMaterialMapSpec;

		// Token: 0x02000012 RID: 18
		public readonly struct MapDataChange
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000051 RID: 81 RVA: 0x00003B96 File Offset: 0x00001D96
			public Vector3Int Coordinates { get; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00003B9E File Offset: 0x00001D9E
			public float OldValue { get; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000053 RID: 83 RVA: 0x00003BA6 File Offset: 0x00001DA6
			public float NewValue { get; }

			// Token: 0x06000054 RID: 84 RVA: 0x00003BAE File Offset: 0x00001DAE
			public MapDataChange(Vector3Int coordinates, float oldValue, float newValue)
			{
				this.Coordinates = coordinates;
				this.OldValue = oldValue;
				this.NewValue = newValue;
			}
		}
	}
}
