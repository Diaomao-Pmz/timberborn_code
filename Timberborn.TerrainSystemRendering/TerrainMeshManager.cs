using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.LevelVisibilitySystem;
using Timberborn.MapIndexSystem;
using Timberborn.MapStateSystem;
using Timberborn.PrefabOptimization;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x02000015 RID: 21
	public class TerrainMeshManager : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004234 File Offset: 0x00002434
		public TerrainMeshManager(ITerrainService terrainService, TerrainBlockRepository terrainBlockRepository, TerrainBlockRandomizer terrainBlockRandomizer, MapIndexService mapIndexService, ILevelVisibilityService levelVisibilityService, RootObjectProvider rootObjectProvider, ISpecService specService, MapSize mapSize)
		{
			this._terrainService = terrainService;
			this._terrainBlockRepository = terrainBlockRepository;
			this._terrainBlockRandomizer = terrainBlockRandomizer;
			this._mapIndexService = mapIndexService;
			this._rootObjectProvider = rootObjectProvider;
			this._specService = specService;
			this._mapSize = mapSize;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000042A8 File Offset: 0x000024A8
		public void Load()
		{
			this._root = this._rootObjectProvider.CreateRootObject("TerrainMeshManager");
			this._terrainTilePrefab = this._specService.GetSingleSpec<TerrainMeshManagerSpec>().TerrainTilePrefab.Asset;
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
			this._surfaceShapeCodes = new byte[this._terrainService.Size.x + 2, this._terrainService.Size.y + 2, this._terrainService.Size.z + 3];
			this.InitializeTerrainTiles();
			Shader.SetGlobalFloat(TerrainMeshManager.TerrainStumpHeightProperty, TerrainMeshManager.TerrainStumpHeight);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000435C File Offset: 0x0000255C
		public void LateUpdateSingleton()
		{
			if (!this._dirtyCodes.IsEmpty<Vector3Int>())
			{
				foreach (Vector3Int coords in this._dirtyCodes)
				{
					this.UpdateCodeForCoords(coords);
				}
				this._dirtyCodes.Clear();
				foreach (Vector3Int tileIndex in this._invalidTiles)
				{
					this.UpdateTile(tileIndex);
				}
				this._invalidTiles.Clear();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004418 File Offset: 0x00002618
		public void ToggleVisibilityForDebugging()
		{
			this._root.SetActive(!this._root.activeSelf);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004434 File Offset: 0x00002634
		public void InitializeTerrainTiles()
		{
			Vector3Int size = this._terrainService.Size;
			Vector3Int vector3Int = WorldTiling.TileCount3D(size.x, size.y, this._mapSize.MaxGameTerrainHeight + 1);
			for (int i = -2; i < size.z + 1; i++)
			{
				for (int j = -1; j < size.y + 1; j++)
				{
					for (int k = -1; k < size.x + 1; k++)
					{
						this.UpdateCodeForCoords(new Vector3Int(k, j, i));
					}
				}
			}
			for (int l = 0; l < vector3Int.z; l++)
			{
				for (int m = 0; m < vector3Int.y; m++)
				{
					for (int n = 0; n < vector3Int.x; n++)
					{
						this.InstantiateTile(new Vector3Int(n, m, l));
					}
				}
			}
			for (int num = 0; num < vector3Int.z; num++)
			{
				for (int num2 = 0; num2 < vector3Int.y; num2++)
				{
					for (int num3 = 0; num3 < vector3Int.x; num3++)
					{
						this.UpdateTile(new Vector3Int(num3, num2, num));
					}
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004564 File Offset: 0x00002764
		public void InstantiateTile(Vector3Int tileIndex)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this._terrainTilePrefab, this._root.transform);
			gameObject.name = TerrainMeshManager.TileName(tileIndex);
			this._tiles[tileIndex] = new TerrainMeshManager.TileComponents(gameObject);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000045A6 File Offset: 0x000027A6
		public void UpdateTile(Vector3Int tileIndex)
		{
			this._meshBuilder.Reset(TerrainMeshManager.TileName(tileIndex));
			this.CollectMeshesForTile(tileIndex);
			this.UpdateTileMesh(tileIndex);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000045C8 File Offset: 0x000027C8
		public void CollectMeshesForTile(Vector3Int tileIndex)
		{
			TileBounds3D tileBounds3D = WorldTiling.TileBounds3D(tileIndex);
			int x = (tileBounds3D.MinX > 0) ? tileBounds3D.MinX : -1;
			int num = Math.Min(tileBounds3D.MaxX, this._terrainService.Size.x);
			int y = (tileBounds3D.MinY > 0) ? tileBounds3D.MinY : -1;
			int num2 = Math.Min(tileBounds3D.MaxY, this._terrainService.Size.y);
			int minZ = tileBounds3D.MinZ;
			int num3 = Math.Min(tileBounds3D.MaxZ, this._terrainService.Size.z);
			int tileMinZ = minZ - 1;
			int tileMaxZ = num3 + 1;
			Vector3Int vector3Int = default(Vector3Int);
			vector3Int.y = y;
			while (vector3Int.y < num2)
			{
				vector3Int.x = x;
				int num4;
				while (vector3Int.x < num)
				{
					if (this.HasAnyColumnInside(vector3Int.XY(), tileMinZ, tileMaxZ))
					{
						vector3Int.z = minZ;
						while (vector3Int.z < num3)
						{
							this.CollectMeshesForCoordinates(vector3Int);
							num4 = vector3Int.z + 1;
							vector3Int.z = num4;
						}
					}
					num4 = vector3Int.x + 1;
					vector3Int.x = num4;
				}
				num4 = vector3Int.y + 1;
				vector3Int.y = num4;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004720 File Offset: 0x00002920
		public bool HasAnyColumnInside(Vector2Int coords, int tileMinZ, int tileMaxZ)
		{
			int numberOfColumnsWithTerrainOnly = 0;
			foreach (Vector3Int value in TerrainMeshManager.NeighborOffsets)
			{
				Vector2Int coords2 = coords + value.XY();
				if (this.HasTerrainInColumn(coords2, tileMinZ, tileMaxZ, ref numberOfColumnsWithTerrainOnly))
				{
					return true;
				}
			}
			return !TerrainMeshManager.HasColumnsWithOnlyTerrainOrAir(numberOfColumnsWithTerrainOnly);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004774 File Offset: 0x00002974
		public void CollectMeshesForCoordinates(Vector3Int coordinates)
		{
			SurfaceBlockShape shape = this.ExpectedShape(coordinates);
			if (shape.IsVisible)
			{
				IntermediateMesh terrainBlock = this.GetTerrainBlock(coordinates, shape);
				if (terrainBlock.UV1 == null || terrainBlock.UV1.Length != terrainBlock.VertexCount)
				{
					terrainBlock.UV1 = new Vector4[terrainBlock.VertexCount];
				}
				for (int i = 0; i < terrainBlock.VertexCount; i++)
				{
					Vector4 vector = terrainBlock.UV1[i];
					terrainBlock.UV1[i] = new Vector4(vector.x, (float)coordinates.z, vector.z, vector.w);
				}
				Vector3 translation = CoordinateSystem.GridToWorld(coordinates) + TerrainMeshManager.PrefabTranslationOffset;
				TranslationTransform fittingTransform = new TranslationTransform(translation);
				this.CollectMeshesFromModel(terrainBlock, fittingTransform);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000483C File Offset: 0x00002A3C
		public bool HasTerrainInColumn(Vector2Int coords, int tileMinZ, int tileMaxZ, ref int terrainCount)
		{
			int num = this._mapIndexService.CellToIndex(coords);
			int columnCount = this._terrainService.GetColumnCount(num);
			for (int i = 0; i < columnCount; i++)
			{
				int index3D = num + i * this._mapIndexService.VerticalStride;
				int columnFloor = this._terrainService.GetColumnFloor(index3D);
				int columnCeiling = this._terrainService.GetColumnCeiling(index3D);
				if ((columnFloor >= tileMinZ && columnFloor <= tileMaxZ) || (columnCeiling >= tileMinZ && columnCeiling <= tileMaxZ))
				{
					return true;
				}
				if (columnFloor <= tileMinZ && columnCeiling >= tileMaxZ)
				{
					terrainCount++;
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000048C6 File Offset: 0x00002AC6
		public static bool HasColumnsWithOnlyTerrainOrAir(int numberOfColumnsWithTerrainOnly)
		{
			return numberOfColumnsWithTerrainOnly == 4 || numberOfColumnsWithTerrainOnly == 0;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000048D2 File Offset: 0x00002AD2
		public void CollectMeshesFromModel(IntermediateMesh intermediateMesh, TranslationTransform fittingTransform)
		{
			this._meshBuilder.AppendIntermediateMesh<TranslationTransform>(intermediateMesh, fittingTransform);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000048E4 File Offset: 0x00002AE4
		public void UpdateTileMesh(Vector3Int tileIndex)
		{
			TerrainMeshManager.TileComponents tileComponents = this._tiles[tileIndex];
			if (!this._meshBuilder.IsEmpty)
			{
				tileComponents.UpdateMesh(this._meshBuilder.Build(1));
				return;
			}
			tileComponents.Deactivate();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004928 File Offset: 0x00002B28
		public IntermediateMesh GetTerrainBlock(Vector3Int coordinates, SurfaceBlockShape shape)
		{
			ImmutableArray<IntermediateMesh> terrainBlocks = this._terrainBlockRepository.GetTerrainBlocks(shape);
			return this._terrainBlockRandomizer.PickVariation(terrainBlocks, shape, coordinates);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004950 File Offset: 0x00002B50
		public SurfaceBlockShape ExpectedShape(Vector3Int coords)
		{
			byte b = this._surfaceShapeCodes[coords.x + 1, coords.y + 1, coords.z + 2];
			RelativeHeight height = (RelativeHeight)(b & 3);
			RelativeHeight height2 = (RelativeHeight)(b >> 2 & 3);
			RelativeHeight height3 = (RelativeHeight)(b >> 4 & 3);
			return new SurfaceBlockShape((RelativeHeight)(b >> 6 & 3), height3, height, height2);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000049A4 File Offset: 0x00002BA4
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			Vector2Int coordinates = change.Coordinates;
			for (int i = change.From; i <= Mathf.Max(change.To + 1, this._terrainService.Size.z - 1); i++)
			{
				foreach (Vector3Int vector3Int in TerrainMeshManager.NeighborOffsets)
				{
					this._dirtyCodes.Add(new Vector3Int(coordinates.x - vector3Int.x, coordinates.y - vector3Int.y, i));
				}
			}
			foreach (Vector2Int vector2Int in Deltas.Neighbors8AndSelfVector2Int)
			{
				Vector2Int coordinates2 = coordinates + vector2Int;
				if (this._terrainService.Contains(coordinates2))
				{
					Vector3Int vector3Int2 = WorldTiling.CoordinatesToTileIndex3D(new Vector3Int(coordinates2.x, coordinates2.y, change.From));
					Vector3Int vector3Int3 = WorldTiling.CoordinatesToTileIndex3D(new Vector3Int(coordinates2.x, coordinates2.y, change.To + 2));
					if (vector3Int2.XY() != vector3Int3.XY())
					{
						throw new Exception(string.Format("Unexpected tile indices: {0} and {1}.", vector3Int2, vector3Int3) + " This should never happen.");
					}
					int num = Math.Min(vector3Int2.z, vector3Int3.z);
					int num2 = Math.Max(vector3Int2.z, vector3Int3.z);
					for (int k = num; k <= num2; k++)
					{
						this._invalidTiles.Add(new Vector3Int(vector3Int2.x, vector3Int2.y, k));
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004B68 File Offset: 0x00002D68
		public void UpdateCodeForCoords(Vector3Int coords)
		{
			byte b = 0;
			for (int i = 0; i < TerrainMeshManager.NeighborOffsets.Length; i++)
			{
				Vector3Int vector3Int = TerrainMeshManager.NeighborOffsets[i];
				Vector3Int vector3Int2 = coords + vector3Int;
				bool flag = this._terrainService.Contains(vector3Int2.XY());
				b |= (byte)((flag && this._terrainService.Underground(vector3Int2.Below())) ? (1 << i * 2) : 0);
				b |= (byte)((flag && this._terrainService.Underground(vector3Int2)) ? (1 << i * 2 + 1) : 0);
			}
			this._surfaceShapeCodes[coords.x + 1, coords.y + 1, coords.z + 2] = b;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C22 File Offset: 0x00002E22
		public static string TileName(Vector3Int tileIndex)
		{
			return string.Format("TerrainTile ({0}, {1}, {2})", tileIndex.x, tileIndex.y, tileIndex.z);
		}

		// Token: 0x04000067 RID: 103
		public static readonly float TerrainStumpHeight = 0.85f;

		// Token: 0x04000068 RID: 104
		public static readonly int TerrainStumpHeightProperty = Shader.PropertyToID("_TerrainStumpHeight");

		// Token: 0x04000069 RID: 105
		public static readonly Vector3Int[] NeighborOffsets = new Vector3Int[]
		{
			new Vector3Int(0, 0, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(1, 1, 0)
		};

		// Token: 0x0400006A RID: 106
		public static readonly Vector3 PrefabTranslationOffset = new Vector3(1f, 0f, 1f);

		// Token: 0x0400006B RID: 107
		public readonly ITerrainService _terrainService;

		// Token: 0x0400006C RID: 108
		public readonly TerrainBlockRepository _terrainBlockRepository;

		// Token: 0x0400006D RID: 109
		public readonly TerrainBlockRandomizer _terrainBlockRandomizer;

		// Token: 0x0400006E RID: 110
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400006F RID: 111
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000070 RID: 112
		public readonly ISpecService _specService;

		// Token: 0x04000071 RID: 113
		public readonly MapSize _mapSize;

		// Token: 0x04000072 RID: 114
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x04000073 RID: 115
		public readonly Dictionary<Vector3Int, TerrainMeshManager.TileComponents> _tiles = new Dictionary<Vector3Int, TerrainMeshManager.TileComponents>();

		// Token: 0x04000074 RID: 116
		public readonly HashSet<Vector3Int> _invalidTiles = new HashSet<Vector3Int>();

		// Token: 0x04000075 RID: 117
		public readonly HashSet<Vector3Int> _dirtyCodes = new HashSet<Vector3Int>();

		// Token: 0x04000076 RID: 118
		public GameObject _root;

		// Token: 0x04000077 RID: 119
		public GameObject _terrainTilePrefab;

		// Token: 0x04000078 RID: 120
		public byte[,,] _surfaceShapeCodes;

		// Token: 0x02000016 RID: 22
		public readonly struct TileComponents
		{
			// Token: 0x06000091 RID: 145 RVA: 0x00004CDA File Offset: 0x00002EDA
			public TileComponents(GameObject gameObject)
			{
				this._gameObject = gameObject;
				this._meshRenderer = gameObject.GetComponent<MeshRenderer>();
				this._meshFilter = gameObject.GetComponent<MeshFilter>();
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00004CFC File Offset: 0x00002EFC
			public void UpdateMesh(BuiltMesh buildMesh)
			{
				Object.Destroy(this._meshFilter.sharedMesh);
				this._meshFilter.sharedMesh = buildMesh.Mesh;
				this._meshFilter.sharedMesh.UploadMeshData(true);
				if (this._meshRenderer.sharedMaterials == null || this._meshRenderer.sharedMaterials.Length != buildMesh.Materials.Length)
				{
					this._meshRenderer.sharedMaterials = buildMesh.Materials;
				}
				this._gameObject.SetActive(true);
			}

			// Token: 0x06000093 RID: 147 RVA: 0x00004D7F File Offset: 0x00002F7F
			public void Deactivate()
			{
				this._gameObject.SetActive(false);
			}

			// Token: 0x04000079 RID: 121
			public readonly GameObject _gameObject;

			// Token: 0x0400007A RID: 122
			public readonly MeshRenderer _meshRenderer;

			// Token: 0x0400007B RID: 123
			public readonly MeshFilter _meshFilter;
		}
	}
}
