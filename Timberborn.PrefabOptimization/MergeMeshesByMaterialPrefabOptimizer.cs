using System;
using System.Text;
using Timberborn.Timbermesh;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200001A RID: 26
	public class MergeMeshesByMaterialPrefabOptimizer : IPrefabOptimizer
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000041DE File Offset: 0x000023DE
		public void Optimize(GameObject prefab)
		{
			if (prefab.GetComponentInChildren<TimbermeshDescription>(true))
			{
				MergeMeshesByMaterialPrefabOptimizer.OptimizeTimbermeshMeshes(prefab);
				return;
			}
			MergeMeshesByMaterialPrefabOptimizer.VisitRootGameObject(prefab);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000041FC File Offset: 0x000023FC
		public static void VisitRootGameObject(GameObject visitee)
		{
			MeshBuilder meshBuilder = new MeshBuilder(MergeMeshesByMaterialPrefabOptimizer.MergedMeshName(visitee));
			MergeMeshesByMaterialPrefabOptimizer.VisitQualifyingGameObject(visitee, meshBuilder, Matrix4x4.identity, true);
			if (!meshBuilder.IsEmpty)
			{
				MeshFilter meshFilter;
				MeshRenderer meshRenderer;
				if (visitee.TryGetComponent<MeshFilter>(ref meshFilter) || visitee.TryGetComponent<MeshRenderer>(ref meshRenderer))
				{
					throw new InvalidOperationException(visitee.name + " already has a MeshFilter or a MeshRenderer, this is a bug.");
				}
				MeshFilter meshFilter2 = visitee.AddComponent<MeshFilter>();
				MeshRenderer meshRenderer2 = visitee.AddComponent<MeshRenderer>();
				BuiltMesh builtMesh = meshBuilder.Build(1);
				meshFilter2.sharedMesh = builtMesh.Mesh;
				meshRenderer2.sharedMaterials = builtMesh.Materials;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004284 File Offset: 0x00002484
		public static void VisitQualifyingGameObject(GameObject visitee, MeshBuilder meshBuilder, Matrix4x4 parentMatrix, bool root)
		{
			Transform transform = visitee.transform;
			Matrix4x4 matrix4x = root ? parentMatrix : (parentMatrix * Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale));
			MeshRenderer component = visitee.GetComponent<MeshRenderer>();
			if (component && component.sharedMaterials.Length != 0)
			{
				MeshFilter component2 = component.GetComponent<MeshFilter>();
				Mesh sharedMesh = component2.sharedMesh;
				meshBuilder.AppendMesh<Matrix4x4Transform>(sharedMesh, component.sharedMaterials, new Matrix4x4Transform(matrix4x));
				Object.DestroyImmediate(component2);
				Object.DestroyImmediate(component);
			}
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				GameObject gameObject = child.gameObject;
				if (SpecialGameObjects.GameObjectIsRoot(gameObject))
				{
					MergeMeshesByMaterialPrefabOptimizer.VisitRootGameObject(gameObject);
				}
				else
				{
					MergeMeshesByMaterialPrefabOptimizer.VisitQualifyingGameObject(child.gameObject, meshBuilder, matrix4x, false);
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000434C File Offset: 0x0000254C
		public static string MergedMeshName(GameObject visitee)
		{
			StringBuilder stringBuilder = new StringBuilder(visitee.name);
			while (visitee.transform.parent != visitee.transform.root)
			{
				visitee = visitee.transform.parent.gameObject;
				stringBuilder.Insert(0, "-");
				stringBuilder.Insert(0, visitee.name);
			}
			stringBuilder.Append(MergeMeshesByMaterialPrefabOptimizer.MergedMeshNamePostfix);
			return stringBuilder.ToString();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000043C4 File Offset: 0x000025C4
		public static void OptimizeTimbermeshMeshes(GameObject prefab)
		{
			foreach (MeshRenderer meshRenderer in prefab.GetComponentsInChildren<MeshRenderer>(true))
			{
				MeshFilter component = meshRenderer.GetComponent<MeshFilter>();
				if (component && meshRenderer.sharedMaterials.Length != 0)
				{
					Mesh sharedMesh = component.sharedMesh;
					MeshBuilder meshBuilder = new MeshBuilder(sharedMesh.name);
					meshBuilder.AppendMesh<Matrix4x4Transform>(sharedMesh, meshRenderer.sharedMaterials, new Matrix4x4Transform(Matrix4x4.identity));
					BuiltMesh builtMesh = meshBuilder.Build(1);
					component.sharedMesh = builtMesh.Mesh;
					meshRenderer.sharedMaterials = builtMesh.Materials;
				}
			}
		}

		// Token: 0x04000067 RID: 103
		public static readonly string MergedMeshNamePostfix = "-MergedMesh";
	}
}
