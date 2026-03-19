using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000028 RID: 40
	public class VerticalShapeBuilder
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00006033 File Offset: 0x00004233
		public VerticalShapeBuilder(IPrefabOptimizationChain prefabOptimizationChain, OptimizedPrefabInstantiator optimizedPrefabInstantiator)
		{
			this._prefabOptimizationChain = prefabOptimizationChain;
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006060 File Offset: 0x00004260
		public GameObject Build(Transform parent, VerticalShapeInfo shapeInfo)
		{
			GameObject orCreateShapePrefab = this.GetOrCreateShapePrefab(shapeInfo);
			return this._optimizedPrefabInstantiator.Instantiate(orCreateShapePrefab, parent);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006084 File Offset: 0x00004284
		public GameObject GetOrCreateShapePrefab(VerticalShapeInfo shapeInfo)
		{
			GameObject result;
			if (!this._shapeInfoCache.TryGetValue(shapeInfo, out result))
			{
				this.CreateMeshShape(shapeInfo);
				this._shapeInfoCache.Add(shapeInfo, result = this.BuildPrefab());
			}
			return result;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000060C0 File Offset: 0x000042C0
		public void CreateMeshShape(VerticalShapeInfo shapeInfo)
		{
			this._meshBuilder.Reset(shapeInfo.Name);
			BuiltMesh meshAndMaterials = VerticalShapeBuilder.GetMeshAndMaterials(shapeInfo.StartPrefab);
			BuiltMesh meshAndMaterials2 = VerticalShapeBuilder.GetMeshAndMaterials(shapeInfo.RepeatingPrefab);
			for (int i = 0; i < shapeInfo.TotalPrefabCount; i++)
			{
				TranslationTransform transform = new TranslationTransform((float)i * Vector3.down);
				this._meshBuilder.AppendMesh<TranslationTransform>((i == 0) ? meshAndMaterials : meshAndMaterials2, transform);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006134 File Offset: 0x00004334
		public static BuiltMesh GetMeshAndMaterials(GameObject source)
		{
			MeshRenderer componentInChildren = source.GetComponentInChildren<MeshRenderer>();
			return new BuiltMesh(source.GetComponentInChildren<MeshFilter>().sharedMesh, componentInChildren.sharedMaterials);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006160 File Offset: 0x00004360
		public GameObject BuildPrefab()
		{
			BuiltMesh builtMesh = this._meshBuilder.Build(1);
			GameObject gameObject = new GameObject(builtMesh.Mesh.name);
			gameObject.AddComponent<MeshRenderer>().sharedMaterials = builtMesh.Materials;
			gameObject.AddComponent<MeshFilter>().sharedMesh = builtMesh.Mesh;
			GameObject result = this._prefabOptimizationChain.Process(gameObject);
			Object.Destroy(gameObject);
			return result;
		}

		// Token: 0x040000BD RID: 189
		public readonly IPrefabOptimizationChain _prefabOptimizationChain;

		// Token: 0x040000BE RID: 190
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x040000BF RID: 191
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x040000C0 RID: 192
		public readonly Dictionary<VerticalShapeInfo, GameObject> _shapeInfoCache = new Dictionary<VerticalShapeInfo, GameObject>();
	}
}
