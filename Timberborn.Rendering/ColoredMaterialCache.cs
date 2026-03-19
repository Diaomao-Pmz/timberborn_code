using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x0200000A RID: 10
	public class ColoredMaterialCache : IUnloadableSingleton
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002700 File Offset: 0x00000900
		public Material GetCachedMaterial(Material inputMaterial, MaterialProperties materialProperties, out bool isNew)
		{
			Material valueOrDefault = CollectionExtensions.GetValueOrDefault<Material, Material>(this._coloredToInitial, inputMaterial, inputMaterial);
			ColoredMaterialCache.MaterialKey materialKey = new ColoredMaterialCache.MaterialKey(valueOrDefault, materialProperties);
			Material result;
			if (this._cachedMaterials.TryGetValue(materialKey, out result))
			{
				isNew = false;
				return result;
			}
			Material result2 = this.CreateMaterial(inputMaterial, valueOrDefault, materialKey);
			isNew = true;
			return result2;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002748 File Offset: 0x00000948
		public void Unload()
		{
			foreach (KeyValuePair<Material, Material> keyValuePair in this._coloredToInitial)
			{
				Material material;
				Material material2;
				keyValuePair.Deconstruct(ref material, ref material2);
				Material material3 = material;
				if (material3 != null)
				{
					Object.Destroy(material3);
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027B0 File Offset: 0x000009B0
		public Material CreateMaterial(Material inputMaterial, Material initialMaterial, ColoredMaterialCache.MaterialKey materialKey)
		{
			Material material = new Material(inputMaterial);
			this._cachedMaterials.Add(materialKey, material);
			this._coloredToInitial.Add(material, initialMaterial);
			return material;
		}

		// Token: 0x04000015 RID: 21
		public readonly Dictionary<ColoredMaterialCache.MaterialKey, Material> _cachedMaterials = new Dictionary<ColoredMaterialCache.MaterialKey, Material>();

		// Token: 0x04000016 RID: 22
		public readonly Dictionary<Material, Material> _coloredToInitial = new Dictionary<Material, Material>();

		// Token: 0x0200000B RID: 11
		public readonly struct MaterialKey : IEquatable<ColoredMaterialCache.MaterialKey>
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000027FD File Offset: 0x000009FD
			public MaterialKey(Material initialMaterial, MaterialProperties materialProperties)
			{
				this._initialMaterial = initialMaterial;
				this._materialProperties = materialProperties;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x0000280D File Offset: 0x00000A0D
			public bool Equals(ColoredMaterialCache.MaterialKey other)
			{
				return this._initialMaterial.Equals(other._initialMaterial) && this._materialProperties.Equals(other._materialProperties);
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002838 File Offset: 0x00000A38
			public override bool Equals(object obj)
			{
				if (obj is ColoredMaterialCache.MaterialKey)
				{
					ColoredMaterialCache.MaterialKey other = (ColoredMaterialCache.MaterialKey)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x0000285D File Offset: 0x00000A5D
			public override int GetHashCode()
			{
				return this._initialMaterial.GetHashCode() * 397 ^ this._materialProperties.GetHashCode();
			}

			// Token: 0x04000017 RID: 23
			public readonly Material _initialMaterial;

			// Token: 0x04000018 RID: 24
			public readonly MaterialProperties _materialProperties;
		}
	}
}
