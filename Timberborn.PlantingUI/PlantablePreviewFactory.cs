using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.Planting;
using Timberborn.Rendering;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.TemplateSystem;
using Timberborn.Timbermesh;
using UnityEngine;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200000C RID: 12
	public class PlantablePreviewFactory : ILoadableSingleton
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000255A File Offset: 0x0000075A
		public PlantablePreviewFactory(TemplateService templateService, TemplateInstantiator templateInstantiator, MaterialColorer materialColorer, RootObjectProvider rootObjectProvider)
		{
			this._templateService = templateService;
			this._templateInstantiator = templateInstantiator;
			this._materialColorer = materialColorer;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000258C File Offset: 0x0000078C
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("PlantablePreviewFactory").transform;
			foreach (PlantableSpec plantableSpec in this._templateService.GetAll<PlantableSpec>())
			{
				PlantablePreviewModelSpec spec = plantableSpec.GetSpec<PlantablePreviewModelSpec>();
				if (spec != null)
				{
					string templateName = plantableSpec.GetSpec<TemplateSpec>().TemplateName;
					if (!(spec.Model != null))
					{
						throw new Exception("Empty model path in PlantablePreviewModelSpec for plantable " + templateName);
					}
					this.CreateBlueprint(templateName, spec);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002634 File Offset: 0x00000834
		public PlantablePreview CreatePreview(string resource, Vector3Int coords)
		{
			GameObject gameObject = this._templateInstantiator.Instantiate(this._previewBlueprints[resource], this._parent, null);
			gameObject.transform.position = CoordinateSystem.GridToWorld(coords);
			PlantablePreview componentSlow = gameObject.GetComponentSlow<PlantablePreview>();
			this._materialColorer.EnableGrayscale(componentSlow);
			return componentSlow;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002684 File Offset: 0x00000884
		public void CreateBlueprint(string templateName, PlantablePreviewModelSpec modelSpec)
		{
			PlantablePreviewSpec plantablePreviewSpec = new PlantablePreviewSpec
			{
				Model = modelSpec.Model
			};
			TimbermeshSpec timbermeshSpec = new TimbermeshSpec
			{
				Model = modelSpec.Model
			};
			Blueprint value = new Blueprint(templateName + "-PreviewTemplate", new ComponentSpec[]
			{
				plantablePreviewSpec,
				timbermeshSpec
			}, ImmutableArray<Blueprint>.Empty);
			this._previewBlueprints.Add(templateName, value);
		}

		// Token: 0x04000022 RID: 34
		public readonly TemplateService _templateService;

		// Token: 0x04000023 RID: 35
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x04000024 RID: 36
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000025 RID: 37
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000026 RID: 38
		public Transform _parent;

		// Token: 0x04000027 RID: 39
		public readonly Dictionary<string, Blueprint> _previewBlueprints = new Dictionary<string, Blueprint>();
	}
}
