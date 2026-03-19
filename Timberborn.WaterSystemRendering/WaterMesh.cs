using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.MapStateSystem;
using Timberborn.PrefabOptimization;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000014 RID: 20
	public class WaterMesh : IWaterMesh, ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public WaterMesh(MapSize mapSize, EventBus eventBus, WaterOpacityService waterOpacityService, ISpecService specService, RootObjectProvider rootObjectProvider)
		{
			this._mapSize = mapSize;
			this._eventBus = eventBus;
			this._waterOpacityService = waterOpacityService;
			this._specService = specService;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D3C File Offset: 0x00001F3C
		public void Load()
		{
			this._waterMeshSpec = this._specService.GetSingleSpec<WaterMeshSpec>();
			this._waterMesh = this.GetWaterMesh();
			this._waterTiles = new GameObject(WaterMesh.WaterTileRootName);
			GameObject gameObject = this._rootObjectProvider.CreateRootObject("WaterMesh");
			this._waterTiles.transform.SetParent(gameObject.transform);
			this._eventBus.Register(this);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003DAC File Offset: 0x00001FAC
		public void Unload()
		{
			this._createdTiles.Clear();
			foreach (Mesh mesh in this._createdMeshes)
			{
				if (mesh != null)
				{
					mesh.Clear();
					Object.Destroy(mesh);
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E18 File Offset: 0x00002018
		public void Show()
		{
			this._waterTiles.SetActive(true);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E26 File Offset: 0x00002026
		public void Hide()
		{
			this._waterTiles.SetActive(false);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E34 File Offset: 0x00002034
		public void EnableTile(Vector3Int tileIndex)
		{
			MeshRenderer meshRenderer;
			if (!this._createdTiles.TryGetValue(tileIndex, out meshRenderer))
			{
				meshRenderer = this.CreateTile(tileIndex);
			}
			meshRenderer.enabled = true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E60 File Offset: 0x00002060
		public void DisableAllTiles()
		{
			foreach (MeshRenderer meshRenderer in this._createdTiles.Values)
			{
				meshRenderer.enabled = false;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003EB8 File Offset: 0x000020B8
		[OnEvent]
		public void OnWaterOpacityChanged(WaterOpacityChangedEvent waterOpacityChangedEvent)
		{
			this.UpdateMaterialTransparency();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003EC0 File Offset: 0x000020C0
		public IntermediateMesh GetWaterMesh()
		{
			int num = 48;
			int[] item = new int[]
			{
				1,
				0,
				4,
				1,
				4,
				5,
				5,
				6,
				1,
				6,
				2,
				1,
				6,
				7,
				2,
				7,
				3,
				2,
				8,
				9,
				4,
				9,
				5,
				4,
				9,
				10,
				5,
				10,
				6,
				5,
				10,
				11,
				6,
				11,
				7,
				6,
				12,
				13,
				8,
				13,
				9,
				8,
				13,
				14,
				9,
				14,
				10,
				9,
				11,
				10,
				14,
				11,
				14,
				15,
				32,
				16,
				17,
				32,
				17,
				33,
				33,
				17,
				18,
				33,
				18,
				34,
				34,
				18,
				35,
				35,
				18,
				19,
				36,
				21,
				20,
				36,
				37,
				21,
				37,
				22,
				21,
				37,
				38,
				22,
				38,
				39,
				22,
				39,
				23,
				22,
				40,
				24,
				25,
				40,
				25,
				41,
				41,
				25,
				42,
				42,
				25,
				26,
				42,
				26,
				43,
				43,
				26,
				27,
				47,
				31,
				30,
				47,
				30,
				46,
				46,
				30,
				29,
				46,
				29,
				45,
				45,
				29,
				44,
				44,
				29,
				28
			};
			return new IntermediateMesh
			{
				VertexCount = num,
				Vertices = new Vector3[num],
				Submeshes = new ValueTuple<NullableKey<Material>, int[]>[]
				{
					new ValueTuple<NullableKey<Material>, int[]>(new NullableKey<Material>(this._waterMeshSpec.OpaqueMaterial.Asset), item)
				},
				UV0 = Enumerable.Repeat<Vector4>(default(Vector4), num).ToArray<Vector4>()
			};
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F48 File Offset: 0x00002148
		public MeshRenderer CreateTile(Vector3Int index)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this._waterMeshSpec.WaterTile.Asset, this._waterTiles.transform);
			string text = string.Format("WaterTile ({0}, {1}, {2})", index.x, index.y, index.z);
			gameObject.name = text;
			Mesh mesh = this.BuildTileMesh(index, text);
			Bounds bounds = mesh.bounds;
			bounds.Encapsulate(new Vector3((float)index.x, (float)this._mapSize.TotalSize.z, (float)index.y));
			mesh.bounds = bounds;
			mesh.UploadMeshData(true);
			gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			component.shadowCastingMode = 0;
			component.sharedMaterial = this.GetWaterMaterial();
			this._createdMeshes.Add(mesh);
			this._createdTiles.Add(index, component);
			return component;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000403C File Offset: 0x0000223C
		public Mesh BuildTileMesh(Vector3Int index, string tileName)
		{
			this._meshBuilder.Reset(tileName);
			TileBounds2D tileBounds2D = this.TileBoundsLimitedToMap(index.XY());
			for (int i = tileBounds2D.MinX; i < tileBounds2D.MaxX; i++)
			{
				for (int j = tileBounds2D.MinY; j < tileBounds2D.MaxY; j++)
				{
					this.AppendWaterMesh(i, j, index.z);
				}
			}
			return this._meshBuilder.Build(0).Mesh;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000040B8 File Offset: 0x000022B8
		public TileBounds2D TileBoundsLimitedToMap(Vector2Int index)
		{
			TileBounds2D tileBounds2D = WorldTiling.TileBounds2D(index);
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			return new TileBounds2D(tileBounds2D.MinX, tileBounds2D.MinY, Math.Min(tileBounds2D.MaxX, terrainSize.x), Math.Min(tileBounds2D.MaxY, terrainSize.y));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004111 File Offset: 0x00002311
		public void AppendWaterMesh(int x, int y, int z)
		{
			this.UpdateWaterMesh(new Vector2Int(x, y), z);
			this._meshBuilder.AppendIntermediateMesh<TranslationTransform>(this._waterMesh, new TranslationTransform(new Vector3((float)x, 0f, (float)y)));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004148 File Offset: 0x00002348
		public void UpdateWaterMesh(Vector2Int tileCoordinates, int columnIndex)
		{
			this._waterMesh.Vertices[0] = this.GetVertexPosition(tileCoordinates, 0f, 0f, columnIndex);
			this._waterMesh.Vertices[1] = this.GetVertexPosition(tileCoordinates, 0.33333334f, 0f, columnIndex);
			this._waterMesh.Vertices[2] = this.GetVertexPosition(tileCoordinates, 0.6666667f, 0f, columnIndex);
			this._waterMesh.Vertices[3] = this.GetVertexPosition(tileCoordinates, 1f, 0f, columnIndex);
			this._waterMesh.Vertices[4] = this.GetVertexPosition(tileCoordinates, 0f, 0.33333334f, columnIndex);
			this._waterMesh.Vertices[5] = this.GetVertexPosition(tileCoordinates, 0.33333334f, 0.33333334f, columnIndex);
			this._waterMesh.Vertices[6] = this.GetVertexPosition(tileCoordinates, 0.6666667f, 0.33333334f, columnIndex);
			this._waterMesh.Vertices[7] = this.GetVertexPosition(tileCoordinates, 1f, 0.33333334f, columnIndex);
			this._waterMesh.Vertices[8] = this.GetVertexPosition(tileCoordinates, 0f, 0.6666667f, columnIndex);
			this._waterMesh.Vertices[9] = this.GetVertexPosition(tileCoordinates, 0.33333334f, 0.6666667f, columnIndex);
			this._waterMesh.Vertices[10] = this.GetVertexPosition(tileCoordinates, 0.6666667f, 0.6666667f, columnIndex);
			this._waterMesh.Vertices[11] = this.GetVertexPosition(tileCoordinates, 1f, 0.6666667f, columnIndex);
			this._waterMesh.Vertices[12] = this.GetVertexPosition(tileCoordinates, 0f, 1f, columnIndex);
			this._waterMesh.Vertices[13] = this.GetVertexPosition(tileCoordinates, 0.33333334f, 1f, columnIndex);
			this._waterMesh.Vertices[14] = this.GetVertexPosition(tileCoordinates, 0.6666667f, 1f, columnIndex);
			this._waterMesh.Vertices[15] = this.GetVertexPosition(tileCoordinates, 1f, 1f, columnIndex);
			this._waterMesh.Vertices[16] = (this._waterMesh.Vertices[32] = this._waterMesh.Vertices[0]);
			this._waterMesh.Vertices[17] = (this._waterMesh.Vertices[33] = this._waterMesh.Vertices[1]);
			this._waterMesh.Vertices[18] = (this._waterMesh.Vertices[34] = this._waterMesh.Vertices[2]);
			this._waterMesh.Vertices[19] = (this._waterMesh.Vertices[35] = this._waterMesh.Vertices[3]);
			this._waterMesh.Vertices[20] = (this._waterMesh.Vertices[36] = this._waterMesh.Vertices[0]);
			this._waterMesh.Vertices[21] = (this._waterMesh.Vertices[37] = this._waterMesh.Vertices[4]);
			this._waterMesh.Vertices[22] = (this._waterMesh.Vertices[38] = this._waterMesh.Vertices[8]);
			this._waterMesh.Vertices[23] = (this._waterMesh.Vertices[39] = this._waterMesh.Vertices[12]);
			this._waterMesh.Vertices[24] = (this._waterMesh.Vertices[40] = this._waterMesh.Vertices[3]);
			this._waterMesh.Vertices[25] = (this._waterMesh.Vertices[41] = this._waterMesh.Vertices[7]);
			this._waterMesh.Vertices[26] = (this._waterMesh.Vertices[42] = this._waterMesh.Vertices[11]);
			this._waterMesh.Vertices[27] = (this._waterMesh.Vertices[43] = this._waterMesh.Vertices[15]);
			this._waterMesh.Vertices[28] = (this._waterMesh.Vertices[44] = this._waterMesh.Vertices[12]);
			this._waterMesh.Vertices[29] = (this._waterMesh.Vertices[45] = this._waterMesh.Vertices[13]);
			this._waterMesh.Vertices[30] = (this._waterMesh.Vertices[46] = this._waterMesh.Vertices[14]);
			this._waterMesh.Vertices[31] = (this._waterMesh.Vertices[47] = this._waterMesh.Vertices[15]);
			this.SetUV0(0, 15, tileCoordinates, WaterMesh.EdgeVertexBit);
			this.SetUV0(5, 6, tileCoordinates, 0);
			this.SetUV0(9, 10, tileCoordinates, 0);
			this.SetUV0(16, 19, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.BottomSkirtBit);
			this.SetUV0(32, 35, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.BottomSkirtBit | WaterMesh.FloorSkirtBit);
			this.SetUV0(20, 23, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.LeftSkirtBit);
			this.SetUV0(36, 39, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.LeftSkirtBit | WaterMesh.FloorSkirtBit);
			this.SetUV0(24, 27, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.RightSkirtBit);
			this.SetUV0(40, 43, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.RightSkirtBit | WaterMesh.FloorSkirtBit);
			this.SetUV0(28, 31, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.TopSkirtBit);
			this.SetUV0(44, 47, tileCoordinates, WaterMesh.EdgeVertexBit | WaterMesh.SkirtBit | WaterMesh.TopSkirtBit | WaterMesh.FloorSkirtBit);
			this.AppendToUV0Mask(new int[]
			{
				0,
				3,
				12,
				15,
				20,
				16,
				19,
				20,
				23,
				24,
				27,
				28,
				31,
				32,
				35,
				36,
				39,
				40,
				43,
				44,
				47
			}, WaterMesh.CornerVertexBit);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004850 File Offset: 0x00002A50
		public Vector3 GetVertexPosition(Vector2Int tileCoordinates, float vertexX, float vertexY, int columnIndex)
		{
			return new Vector3(WaterMesh.TrimCoordinate(vertexX, (float)tileCoordinates.x, (float)this._mapSize.TotalSize.x), (float)columnIndex, WaterMesh.TrimCoordinate(vertexY, (float)tileCoordinates.y, (float)this._mapSize.TotalSize.y));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000048A9 File Offset: 0x00002AA9
		public static float TrimCoordinate(float coordinate, float tilePosition, float mapSize)
		{
			if (tilePosition + coordinate < WaterMesh.MapBorderMargin)
			{
				return WaterMesh.MapBorderMargin;
			}
			if (tilePosition + coordinate > mapSize - WaterMesh.MapBorderMargin)
			{
				return 1f - WaterMesh.MapBorderMargin;
			}
			return coordinate;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000048D4 File Offset: 0x00002AD4
		public void SetUV0(int startIndex, int endIndex, Vector2Int tileCoordinates, int mask)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				this._waterMesh.UV0[i] = new Vector4((float)tileCoordinates.x, (float)tileCoordinates.y, (float)i, (float)mask);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004918 File Offset: 0x00002B18
		public void AppendToUV0Mask(IReadOnlyList<int> indices, int bit)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				Vector4 vector = this._waterMesh.UV0[indices[i]];
				int num = (int)vector.w;
				num |= bit;
				this._waterMesh.UV0[indices[i]] = new Vector4(vector.x, vector.y, vector.z, (float)num);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000498C File Offset: 0x00002B8C
		public void UpdateMaterialTransparency()
		{
			Material waterMaterial = this.GetWaterMaterial();
			foreach (MeshRenderer meshRenderer in this._createdTiles.Values)
			{
				meshRenderer.sharedMaterial = waterMaterial;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000049EC File Offset: 0x00002BEC
		public Material GetWaterMaterial()
		{
			if (!this._waterOpacityService.IsWaterTransparent)
			{
				return this._waterMeshSpec.OpaqueMaterial.Asset;
			}
			return this._waterMeshSpec.TransparentMaterial.Asset;
		}

		// Token: 0x04000083 RID: 131
		public static readonly float MapBorderMargin = 0.1f;

		// Token: 0x04000084 RID: 132
		public static readonly string WaterTileRootName = "WaterTiles";

		// Token: 0x04000085 RID: 133
		public static readonly int EdgeVertexBit = 1;

		// Token: 0x04000086 RID: 134
		public static readonly int CornerVertexBit = 2;

		// Token: 0x04000087 RID: 135
		public static readonly int SkirtBit = 4;

		// Token: 0x04000088 RID: 136
		public static readonly int LeftSkirtBit = 8;

		// Token: 0x04000089 RID: 137
		public static readonly int RightSkirtBit = 16;

		// Token: 0x0400008A RID: 138
		public static readonly int TopSkirtBit = 32;

		// Token: 0x0400008B RID: 139
		public static readonly int BottomSkirtBit = 64;

		// Token: 0x0400008C RID: 140
		public static readonly int FloorSkirtBit = 128;

		// Token: 0x0400008D RID: 141
		public readonly MapSize _mapSize;

		// Token: 0x0400008E RID: 142
		public readonly EventBus _eventBus;

		// Token: 0x0400008F RID: 143
		public readonly WaterOpacityService _waterOpacityService;

		// Token: 0x04000090 RID: 144
		public readonly ISpecService _specService;

		// Token: 0x04000091 RID: 145
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000092 RID: 146
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x04000093 RID: 147
		public GameObject _waterTiles;

		// Token: 0x04000094 RID: 148
		public IntermediateMesh _waterMesh;

		// Token: 0x04000095 RID: 149
		public readonly List<Mesh> _createdMeshes = new List<Mesh>();

		// Token: 0x04000096 RID: 150
		public readonly Dictionary<Vector3Int, MeshRenderer> _createdTiles = new Dictionary<Vector3Int, MeshRenderer>();

		// Token: 0x04000097 RID: 151
		public WaterMeshSpec _waterMeshSpec;
	}
}
