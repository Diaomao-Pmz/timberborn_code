using System;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.LevelVisibilitySystem;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainSystemRendering
{
	// Token: 0x0200001A RID: 26
	public class TerrainTopMeshService : ILoadableSingleton
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004FFD File Offset: 0x000031FD
		public TerrainTopMeshService(ITerrainService terrainService, ILevelVisibilityService levelVisibilityService, ISpecService specService, EventBus eventBus)
		{
			this._terrainService = terrainService;
			this._levelVisibilityService = levelVisibilityService;
			this._specService = specService;
			this._eventBus = eventBus;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005032 File Offset: 0x00003232
		public void Load()
		{
			this.InitializeTopLayerObject();
			this._eventBus.Register(this);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005046 File Offset: 0x00003246
		[OnEvent]
		public void OnMaxVisibleTerrainLevelChanged(MaxVisibleLevelChangedEvent e)
		{
			this._topLayerObject.SetActive(!this._levelVisibilityService.TerrainLevelIsAtMax);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005064 File Offset: 0x00003264
		public void InitializeTopLayerObject()
		{
			AssetRef<GameObject> layerToolTopMeshPrefab = this._specService.GetSingleSpec<TerrainMeshManagerSpec>().LayerToolTopMeshPrefab;
			this._topLayerObject = Object.Instantiate<GameObject>(layerToolTopMeshPrefab.Asset);
			this._topLayerObject.transform.position = Vector3.zero;
			this._topLayerObject.gameObject.SetActive(false);
			Mesh mesh = this.GenerateTopMesh(this._terrainService.Size.XY());
			Vector3 vector;
			vector..ctor((float)this._terrainService.Size.x, 0f, (float)this._terrainService.Size.y);
			mesh.bounds = new Bounds(vector / 2f, vector);
			this._topLayerObject.GetComponent<MeshFilter>().sharedMesh = mesh;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000512C File Offset: 0x0000332C
		public Mesh GenerateTopMesh(Vector2Int size)
		{
			Mesh mesh = new Mesh();
			int num = size.x * size.y;
			int num2 = num * 4;
			mesh.indexFormat = ((num2 > TerrainTopMeshService.VertexLimitFor16BitIndexBuffer) ? 1 : 0);
			IntermediateMesh intermediateMesh = new IntermediateMesh();
			intermediateMesh.VertexCount = num2;
			intermediateMesh.Vertices = new Vector3[num2];
			intermediateMesh.Normals = new Vector3[num2];
			intermediateMesh.UV0 = new Vector4[num2];
			intermediateMesh.Submeshes = new ValueTuple<NullableKey<Material>, int[]>[]
			{
				new ValueTuple<NullableKey<Material>, int[]>(default(NullableKey<Material>), new int[num * 6])
			};
			TerrainTopMeshService.GenerateVertices(size, intermediateMesh);
			TerrainTopMeshService.GenerateIndices(size, intermediateMesh);
			TranslationTransform transform = new TranslationTransform(Vector3.zero);
			this._meshBuilder.AppendIntermediateMesh<TranslationTransform>(intermediateMesh, transform);
			this._meshBuilder.Build(mesh);
			mesh.RecalculateBounds();
			return mesh;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000051FC File Offset: 0x000033FC
		public static void GenerateVertices(Vector2Int size, IntermediateMesh intermediateMesh)
		{
			Vector3[] vertices = intermediateMesh.Vertices;
			Vector3[] normals = intermediateMesh.Normals;
			Vector4[] uv = intermediateMesh.UV0;
			for (int i = 0; i < size.y; i++)
			{
				int num = i * size.x * 4;
				for (int j = 0; j < size.x; j++)
				{
					int num2 = j * 4 + num;
					int num3 = j * 4 + num + 1;
					int num4 = j * 4 + num + 2;
					int num5 = j * 4 + num + 3;
					vertices[num2] = new Vector3((float)j, 0f, (float)i);
					vertices[num3] = new Vector3((float)(j + 1), 0f, (float)i);
					vertices[num4] = new Vector3((float)(j + 1), 0f, (float)(i + 1));
					vertices[num5] = new Vector3((float)j, 0f, (float)(i + 1));
					normals[num2] = Vector3.up;
					normals[num3] = Vector3.up;
					normals[num4] = Vector3.up;
					normals[num5] = Vector3.up;
					Vector2 vector;
					vector..ctor((float)j + 0.5f, (float)i + 0.5f);
					uv[num2] = vector;
					uv[num3] = vector;
					uv[num4] = vector;
					uv[num5] = vector;
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005378 File Offset: 0x00003578
		public static void GenerateIndices(Vector2Int size, IntermediateMesh intermediateMesh)
		{
			int[] item = intermediateMesh.Submeshes[0].Item2;
			for (int i = 0; i < size.y; i++)
			{
				int num = i * size.x * 4;
				int num2 = i * size.x * 6;
				for (int j = 0; j < size.x; j++)
				{
					int num3 = j * 4 + num;
					int num4 = j * 6 + num2;
					int num5 = num3 + 1;
					int num6 = num3 + 2;
					int num7 = num3 + 3;
					item[num4] = num3;
					item[num4 + 1] = num5;
					item[num4 + 2] = num6;
					item[num4 + 3] = num3;
					item[num4 + 4] = num6;
					item[num4 + 5] = num7;
				}
			}
		}

		// Token: 0x0400007F RID: 127
		public static readonly int VertexLimitFor16BitIndexBuffer = 65535;

		// Token: 0x04000080 RID: 128
		public readonly ITerrainService _terrainService;

		// Token: 0x04000081 RID: 129
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000082 RID: 130
		public readonly ISpecService _specService;

		// Token: 0x04000083 RID: 131
		public readonly EventBus _eventBus;

		// Token: 0x04000084 RID: 132
		public readonly MeshBuilder _meshBuilder = new MeshBuilder("TerrainTopMesh");

		// Token: 0x04000085 RID: 133
		public GameObject _topLayerObject;
	}
}
