using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.PrefabOptimization;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Ruins
{
	// Token: 0x02000008 RID: 8
	public class RuinModelFactory : ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00000420
		public RuinModelFactory(IRandomNumberGenerator randomNumberGenerator, IPrefabOptimizationChain prefabOptimizationChain, ISpecService specService)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._prefabOptimizationChain = prefabOptimizationChain;
			this._specService = specService;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002248 File Offset: 0x00000448
		public void Load()
		{
			this._ruinModelFactorySpec = this._specService.GetSingleSpec<RuinModelFactorySpec>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000225C File Offset: 0x0000045C
		public void CreateModels(string variantId, Ruin ruin)
		{
			ImmutableArray<RuinModelVariantSpec> ruinModelVariants = this._ruinModelFactorySpec.RuinModelVariants;
			RuinModelVariantSpec modelVariantSpec = ruinModelVariants.SingleOrDefault((RuinModelVariantSpec variant) => variant.Id == variantId) ?? this._randomNumberGenerator.GetListElementOrDefault<RuinModelVariantSpec>(ruinModelVariants);
			this.CreateModels(modelVariantSpec, ruin);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022B4 File Offset: 0x000004B4
		public void CreateModels(RuinModelVariantSpec modelVariantSpec, Ruin ruin)
		{
			RuinModels component = ruin.GetComponent<RuinModels>();
			GameObject wetModel = this.CreateWetModel(modelVariantSpec, ruin);
			GameObject dryModel = this.CreateDryModel(modelVariantSpec, ruin);
			component.Initialize(modelVariantSpec.Id, wetModel, dryModel);
			this._meshBuilder.Reset("");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F6 File Offset: 0x000004F6
		public GameObject CreateWetModel(RuinModelVariantSpec modelVariantSpec, Ruin ruin)
		{
			GameObject gameObject = this.CreateModel("RuinModelWet", ruin.SpecifiedHeight, modelVariantSpec.Model.Asset, this._ruinModelFactorySpec.IvyWetModel.Asset);
			gameObject.transform.SetParent(ruin.ModelParent, false);
			return gameObject;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002336 File Offset: 0x00000536
		public GameObject CreateDryModel(RuinModelVariantSpec modelVariantSpec, Ruin ruin)
		{
			GameObject gameObject = this.CreateModel("RuinModelDry", ruin.SpecifiedHeight, modelVariantSpec.Model.Asset, this._ruinModelFactorySpec.IvyDryModel.Asset);
			gameObject.transform.SetParent(ruin.ModelParent, false);
			return gameObject;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002378 File Offset: 0x00000578
		public GameObject CreateModel(string ruinName, int height, GameObject ruin, GameObject ivy)
		{
			this._meshBuilder.Reset(ruinName);
			GameObject gameObject = new GameObject(ruinName);
			TranslationTransform transform = default(TranslationTransform);
			this._meshBuilder.AppendMesh<TranslationTransform>(this.GetMesh(ruin, height), this.GetMaterials(ruin, height), transform);
			this._meshBuilder.AppendMesh<TranslationTransform>(this.GetMesh(ivy, height), this.GetMaterials(ivy, height), transform);
			this._meshBuilder.AppendMesh<TranslationTransform>(this.GetMesh(ivy, 0), this.GetMaterials(ivy, 0), transform);
			BuiltMesh builtMesh = this._meshBuilder.Build(1);
			gameObject.AddComponent<MeshFilter>().sharedMesh = builtMesh.Mesh;
			gameObject.AddComponent<MeshRenderer>().sharedMaterials = builtMesh.Materials;
			gameObject.SetActive(false);
			return gameObject;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002430 File Offset: 0x00000630
		public Material[] GetMaterials(GameObject model, int i)
		{
			return this._prefabOptimizationChain.Process(model).GetComponentsInChildren<MeshRenderer>().Single((MeshRenderer mr) => RuinModelFactory.IsOfHeight(mr, i)).sharedMaterials;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002474 File Offset: 0x00000674
		public Mesh GetMesh(GameObject model, int i)
		{
			return this._prefabOptimizationChain.Process(model).GetComponentsInChildren<MeshFilter>().Single((MeshFilter mf) => RuinModelFactory.IsOfHeight(mf, i)).sharedMesh;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024B5 File Offset: 0x000006B5
		public static bool IsOfHeight(Component component, int i)
		{
			return component.name.Contains(string.Format("{0}", i));
		}

		// Token: 0x0400000C RID: 12
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000D RID: 13
		public readonly IPrefabOptimizationChain _prefabOptimizationChain;

		// Token: 0x0400000E RID: 14
		public readonly ISpecService _specService;

		// Token: 0x0400000F RID: 15
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x04000010 RID: 16
		public RuinModelFactorySpec _ruinModelFactorySpec;
	}
}
