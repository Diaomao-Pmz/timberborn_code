using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000007 RID: 7
	public class AreaTileDrawer
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AreaTileDrawer(Mesh mesh, Material material, Vector2Int tileCount, GameObject parent)
		{
			this._materials = new Material[]
			{
				material
			};
			this._tileCount = tileCount;
			this._parent = parent;
			this.Initialize(mesh);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000214F File Offset: 0x0000034F
		public void HideAllTiles()
		{
			this._parent.SetActive(false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000215D File Offset: 0x0000035D
		public void ShowAllTiles()
		{
			this._parent.SetActive(true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216B File Offset: 0x0000036B
		public void UpdateArea(IEnumerable<Vector3Int> coordinates)
		{
			this.Clear();
			this.UpdateMeshBuilders(coordinates);
			this.UpdateTiles();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002180 File Offset: 0x00000380
		public void Initialize(Mesh mesh)
		{
			MeshBuilder meshBuilder = new MeshBuilder();
			for (int i = 0; i < this._tileCount.y; i++)
			{
				for (int j = 0; j < this._tileCount.x; j++)
				{
					Vector2Int key;
					key..ctor(j, i);
					this.InitializeMeshBuilder(key);
					this.InitializeTile(key, meshBuilder);
				}
			}
			meshBuilder.Reset("");
			meshBuilder.AppendMesh<TranslationTransform>(mesh, this._materials, default(TranslationTransform));
			this._intermediateMesh = meshBuilder.BuildIntermediateMesh();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002205 File Offset: 0x00000405
		public void InitializeMeshBuilder(Vector2Int key)
		{
			this._meshBuilders.Add(key, new MeshBuilder());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002218 File Offset: 0x00000418
		public void InitializeTile(Vector2Int key, MeshBuilder meshBuilder)
		{
			GameObject gameObject = new GameObject(key.ToString());
			gameObject.transform.parent = this._parent.transform;
			Mesh mesh = meshBuilder.Build(1).Mesh;
			mesh.MarkDynamic();
			gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
			gameObject.AddComponent<MeshRenderer>().sharedMaterials = this._materials;
			this._tiles.Add(key, gameObject);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002290 File Offset: 0x00000490
		public void Clear()
		{
			foreach (KeyValuePair<Vector2Int, MeshBuilder> keyValuePair in this._meshBuilders)
			{
				Vector2Int vector2Int;
				MeshBuilder meshBuilder;
				keyValuePair.Deconstruct(ref vector2Int, ref meshBuilder);
				Vector2Int vector2Int2 = vector2Int;
				meshBuilder.Reset(vector2Int2.ToString());
			}
			foreach (GameObject gameObject in this._tiles.Values)
			{
				gameObject.SetActive(false);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002344 File Offset: 0x00000544
		public void UpdateMeshBuilders(IEnumerable<Vector3Int> coordinates)
		{
			foreach (Vector3Int vector3Int in coordinates)
			{
				Vector3 translation = CoordinateSystem.GridToWorldCentered(vector3Int) + new Vector3(0f, AreaTileDrawer.YOffset, 0f);
				TranslationTransform transform = new TranslationTransform(translation);
				Vector2Int key = WorldTiling.CoordinatesToTileIndex2D(vector3Int.XY());
				this._meshBuilders[key].AppendIntermediateMesh<TranslationTransform>(this._intermediateMesh, transform);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023D0 File Offset: 0x000005D0
		public void UpdateTiles()
		{
			foreach (Vector2Int key in this._meshBuilders.Keys)
			{
				MeshBuilder meshBuilder = this._meshBuilders[key];
				if (!meshBuilder.IsEmpty)
				{
					GameObject gameObject = this._tiles[key];
					meshBuilder.Build(gameObject.GetComponent<MeshFilter>().sharedMesh);
					gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly float YOffset = 0.02f;

		// Token: 0x04000009 RID: 9
		public IntermediateMesh _intermediateMesh;

		// Token: 0x0400000A RID: 10
		public readonly Material[] _materials;

		// Token: 0x0400000B RID: 11
		public readonly Vector2Int _tileCount;

		// Token: 0x0400000C RID: 12
		public readonly GameObject _parent;

		// Token: 0x0400000D RID: 13
		public readonly Dictionary<Vector2Int, GameObject> _tiles = new Dictionary<Vector2Int, GameObject>();

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<Vector2Int, MeshBuilder> _meshBuilders = new Dictionary<Vector2Int, MeshBuilder>();
	}
}
