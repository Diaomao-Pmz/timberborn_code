using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using Timberborn.Timbermesh;
using UnityEngine;

namespace Timberborn.TimbermeshMaterials
{
	// Token: 0x0200000B RID: 11
	public class MaterialRepository : ILoadableSingleton, IMaterialRepository
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002389 File Offset: 0x00000589
		public MaterialRepository(ISpecService specService, IAssetLoader assetLoader, IEnumerable<IMaterialCollectionIdsProvider> materialCollectionProviders)
		{
			this._specService = specService;
			this._assetLoader = assetLoader;
			this._materialCollectionProviders = materialCollectionProviders.ToImmutableArray<IMaterialCollectionIdsProvider>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023B8 File Offset: 0x000005B8
		public void Load()
		{
			this._defaultMaterial = this._assetLoader.Load<Material>(MaterialRepository.DefaultMaterialPath);
			foreach (Material material in this.GetMaterials().Distinct<Material>())
			{
				if (!this._materials.TryAdd(material.name, material))
				{
					throw new InvalidOperationException("Material " + material.name + " is already loaded.");
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002448 File Offset: 0x00000648
		public Material GetMaterial(string materialName)
		{
			if (string.IsNullOrWhiteSpace(materialName))
			{
				return this._defaultMaterial;
			}
			Material result;
			if (this._materials.TryGetValue(materialName, out result))
			{
				return result;
			}
			throw new ArgumentException("Material " + materialName + " not found in repository.");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000248B File Offset: 0x0000068B
		public IEnumerable<Material> GetMaterials()
		{
			ImmutableArray<MaterialCollectionSpec> materialCollectionSpecs = this._specService.GetSpecs<MaterialCollectionSpec>().ToImmutableArray<MaterialCollectionSpec>();
			foreach (IMaterialCollectionIdsProvider materialCollectionIdsProvider in this._materialCollectionProviders)
			{
				IEnumerable<string> materialCollectionIds = materialCollectionIdsProvider.GetMaterialCollectionIds();
				using (IEnumerator<string> enumerator2 = materialCollectionIds.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string materialCollectionId = enumerator2.Current;
						ImmutableArray<MaterialCollectionSpec> immutableArray = materialCollectionSpecs;
						Func<MaterialCollectionSpec, bool> predicate;
						Func<MaterialCollectionSpec, bool> <>9__0;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = ((MaterialCollectionSpec s) => s.CollectionId == materialCollectionId));
						}
						foreach (MaterialCollectionSpec materialCollectionSpec in immutableArray.Where(predicate))
						{
							foreach (AssetRef<Material> assetRef in materialCollectionSpec.Materials)
							{
								yield return assetRef.Asset;
							}
							ImmutableArray<AssetRef<Material>>.Enumerator enumerator4 = default(ImmutableArray<AssetRef<Material>>.Enumerator);
						}
						IEnumerator<MaterialCollectionSpec> enumerator3 = null;
					}
				}
				IEnumerator<string> enumerator2 = null;
			}
			ImmutableArray<IMaterialCollectionIdsProvider>.Enumerator enumerator = default(ImmutableArray<IMaterialCollectionIdsProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400000E RID: 14
		public static readonly string DefaultMaterialPath = "Materials/Common/Empty";

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;

		// Token: 0x04000010 RID: 16
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000011 RID: 17
		public readonly ImmutableArray<IMaterialCollectionIdsProvider> _materialCollectionProviders;

		// Token: 0x04000012 RID: 18
		public readonly Dictionary<string, Material> _materials = new Dictionary<string, Material>();

		// Token: 0x04000013 RID: 19
		public Material _defaultMaterial;
	}
}
