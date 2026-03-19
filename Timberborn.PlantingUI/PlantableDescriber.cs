using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Gathering;
using Timberborn.GatheringUI;
using Timberborn.Goods;
using Timberborn.Growing;
using Timberborn.GrowingUI;
using Timberborn.Localization;
using Timberborn.Planting;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.ToolSystemUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000009 RID: 9
	public class PlantableDescriber : ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022F4 File Offset: 0x000004F4
		public PlantableDescriber(EntityDescriptionService entityDescriptionService, TemplateInstantiator templateInstantiator, ILoc loc, VisualElementLoader visualElementLoader, GrowableToolPanelItemFactory growableToolPanelItemFactory, GatherableToolPanelItemFactory gatherableToolPanelItemFactory, IGoodService goodService, RootObjectProvider rootObjectProvider)
		{
			this._entityDescriptionService = entityDescriptionService;
			this._templateInstantiator = templateInstantiator;
			this._loc = loc;
			this._visualElementLoader = visualElementLoader;
			this._growableToolPanelItemFactory = growableToolPanelItemFactory;
			this._gatherableToolPanelItemFactory = gatherableToolPanelItemFactory;
			this._goodService = goodService;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000234F File Offset: 0x0000054F
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("PlantableDescriber").transform;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000056C
		public ToolDescription Describe(PlantableSpec plantableSpec, string buildingName)
		{
			Plantable previewFromTemplate = this.GetPreviewFromTemplate(plantableSpec);
			string elementName = "Game/EntityDescription/DescriptionEmptySection";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			this._entityDescriptionService.DescribeAsSeparateSections(previewFromTemplate, visualElement, this.GetDescription(buildingName));
			return new ToolDescription.Builder().AddSection(visualElement).AddSection(this.GetYieldSection(plantableSpec)).Build();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023C4 File Offset: 0x000005C4
		public string GetDescription(string buildingName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this._loc.T(PlantableDescriber.DescriptionLocKey));
			stringBuilder.Append(SpecialStrings.RowStarter + this._loc.T<string>(PlantableDescriber.RequiredBuildingLocKey, buildingName));
			return stringBuilder.ToString();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002414 File Offset: 0x00000614
		public Plantable GetPreviewFromTemplate(PlantableSpec plantableSpec)
		{
			return this._previewCache.GetOrAdd(plantableSpec, () => this.Create(plantableSpec));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002452 File Offset: 0x00000652
		public Plantable Create(PlantableSpec plantableSpec)
		{
			GameObject gameObject = this._templateInstantiator.Instantiate(plantableSpec.Blueprint, this._parent, null);
			gameObject.SetActive(false);
			return gameObject.GetComponentSlow<Plantable>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002478 File Offset: 0x00000678
		public VisualElement GetYieldSection(PlantableSpec plantableSpec)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ToolPanel/ResourceYieldPanel");
			VisualElement visualElement2 = this._growableToolPanelItemFactory.Create(plantableSpec.GetSpec<GrowableSpec>());
			visualElement.Add(visualElement2);
			GatherableSpec spec = plantableSpec.GetSpec<GatherableSpec>();
			if (spec != null && this._goodService.HasGood(spec.Yielder.Yield.Id))
			{
				VisualElement visualElement3 = this._gatherableToolPanelItemFactory.Create(spec);
				visualElement.Add(visualElement3);
				visualElement.AddToClassList(PlantableDescriber.TwoItemsClass);
			}
			return visualElement;
		}

		// Token: 0x04000013 RID: 19
		public static readonly string TwoItemsClass = "two-items";

		// Token: 0x04000014 RID: 20
		public static readonly string DescriptionLocKey = "PlantingTool.Description";

		// Token: 0x04000015 RID: 21
		public static readonly string RequiredBuildingLocKey = "PlantingTool.RequiredBuilding";

		// Token: 0x04000016 RID: 22
		public readonly EntityDescriptionService _entityDescriptionService;

		// Token: 0x04000017 RID: 23
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x04000018 RID: 24
		public readonly ILoc _loc;

		// Token: 0x04000019 RID: 25
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001A RID: 26
		public readonly GrowableToolPanelItemFactory _growableToolPanelItemFactory;

		// Token: 0x0400001B RID: 27
		public readonly GatherableToolPanelItemFactory _gatherableToolPanelItemFactory;

		// Token: 0x0400001C RID: 28
		public readonly IGoodService _goodService;

		// Token: 0x0400001D RID: 29
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400001E RID: 30
		public Transform _parent;

		// Token: 0x0400001F RID: 31
		public readonly Dictionary<PlantableSpec, Plantable> _previewCache = new Dictionary<PlantableSpec, Plantable>();
	}
}
