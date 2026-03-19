using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x0200000C RID: 12
	public class AutoAtlasingPrefabOptimizer : IPrefabOptimizer
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002BAF File Offset: 0x00000DAF
		public AutoAtlasingPrefabOptimizer(AutoAtlaser autoAtlaser)
		{
			this._autoAtlaser = autoAtlaser;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BCC File Offset: 0x00000DCC
		public void Optimize(GameObject prefab)
		{
			foreach (MeshRenderer meshRenderer in prefab.GetComponentsInChildren<MeshRenderer>(true))
			{
				this.OptimizeMeshRenderer(meshRenderer, prefab.name);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C00 File Offset: 0x00000E00
		public void OptimizeMeshRenderer(MeshRenderer meshRenderer, string usageName)
		{
			MeshFilter component = meshRenderer.GetComponent<MeshFilter>();
			Mesh sharedMesh = component.sharedMesh;
			Material[] sharedMaterials = meshRenderer.sharedMaterials;
			Vector2[] uv = sharedMesh.uv;
			if (!sharedMaterials.IsEmpty<Material>() && !uv.IsEmpty<Vector2>())
			{
				int subMeshCount = sharedMesh.subMeshCount;
				Material[] array = (Material[])sharedMaterials.Clone();
				Vector2[] array2 = (Vector2[])uv.Clone();
				for (int i = 0; i < subMeshCount; i++)
				{
					Material material = sharedMaterials[i];
					if (material != null && material.shader.name == AutoAtlasingPrefabOptimizer.EnvironmentShaderName)
					{
						EnvironmentMaterialProperties environmentMaterialProperties = EnvironmentMaterialProperties.FromMaterial(material);
						Texture2D mainTex = environmentMaterialProperties.MainTex;
						Texture2D bumpMap = environmentMaterialProperties.BumpMap;
						Texture2D colorMask = environmentMaterialProperties.ColorMask;
						Texture2D ambientOcclusion = environmentMaterialProperties.AmbientOcclusion;
						Texture2D metallicGlossMap = environmentMaterialProperties.MetallicGlossMap;
						Texture2D lightingMap = environmentMaterialProperties.LightingMap;
						AutoAtlasKey key = new AutoAtlasKey(mainTex, bumpMap, colorMask, ambientOcclusion, metallicGlossMap, lightingMap);
						AutoAtlasFragment autoAtlasFragment;
						if (this._autoAtlaser.TryGetAutoAtlasFragment(key, usageName, out autoAtlasFragment))
						{
							EnvironmentMaterialProperties environmentMaterialProperties2 = environmentMaterialProperties.<Clone>$();
							environmentMaterialProperties2.MainTex = autoAtlasFragment.CombinedMainTex;
							environmentMaterialProperties2.BumpMap = autoAtlasFragment.CombinedBumpMap;
							environmentMaterialProperties2.ColorMask = autoAtlasFragment.CombinedColorMask;
							environmentMaterialProperties2.AmbientOcclusion = autoAtlasFragment.CombinedAmbientOcclusion;
							environmentMaterialProperties2.MetallicGlossMap = autoAtlasFragment.CombinedMetallicGlossMap;
							environmentMaterialProperties2.LightingMap = autoAtlasFragment.CombinedLightingMap;
							EnvironmentMaterialProperties properties = environmentMaterialProperties2;
							array[i] = this.GetMaterialFromProperties(properties, autoAtlasFragment.AtlasName);
							int[] indices = sharedMesh.GetIndices(i);
							for (int j = 0; j < indices.Length; j++)
							{
								array2[indices[j]] = uv[indices[j]] * autoAtlasFragment.UVScale + autoAtlasFragment.UVOffset;
							}
						}
					}
				}
				Mesh mesh = Object.Instantiate<Mesh>(sharedMesh);
				mesh.name = sharedMesh.name;
				meshRenderer.sharedMaterials = array;
				mesh.uv = array2;
				component.sharedMesh = mesh;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public Material GetMaterialFromProperties(EnvironmentMaterialProperties properties, string atlasName)
		{
			return this._materialRegistry.GetOrAdd(properties, delegate()
			{
				int num = this._materialRegistry.Count + 1;
				Material material = new Material(Shader.Find(AutoAtlasingPrefabOptimizer.EnvironmentShaderName))
				{
					name = string.Format("{0}{1}", atlasName, num)
				};
				properties.ApplyToMaterial(material);
				if (num > AutoAtlasingPrefabOptimizer.MaxExpectedRegistrySize)
				{
					Debug.LogWarning("The AutoAtlasingPrefabOptimizer registry size" + string.Format(" is now {0}", num) + string.Format(", exceeding the expected size of {0}", AutoAtlasingPrefabOptimizer.MaxExpectedRegistrySize));
				}
				return material;
			});
		}

		// Token: 0x04000025 RID: 37
		public static readonly string EnvironmentShaderName = "Shader Graphs/EnvironmentURP";

		// Token: 0x04000026 RID: 38
		public static readonly int MaxExpectedRegistrySize = 20;

		// Token: 0x04000027 RID: 39
		public readonly AutoAtlaser _autoAtlaser;

		// Token: 0x04000028 RID: 40
		public readonly Dictionary<EnvironmentMaterialProperties, Material> _materialRegistry = new Dictionary<EnvironmentMaterialProperties, Material>();
	}
}
