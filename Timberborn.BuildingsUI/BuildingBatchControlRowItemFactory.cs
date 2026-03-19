using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Buildings;
using Timberborn.BuildingsNavigation;
using Timberborn.ConstructionSites;
using Timberborn.ConstructionSitesUI;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.BuildingsUI
{
	// Token: 0x02000007 RID: 7
	public class BuildingBatchControlRowItemFactory
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000024F0 File Offset: 0x000006F0
		public BuildingBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, EntitySelectionService entitySelectionService, DistanceToColorConverter distanceToColorConverter)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._entitySelectionService = entitySelectionService;
			this._distanceToColorConverter = distanceToColorConverter;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002518 File Offset: 0x00000718
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			LabeledEntity component = entity.GetComponent<LabeledEntity>();
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BatchControl/BuildingBatchControlRowItem");
			UQueryExtensions.Q<Button>(visualElement, "Select", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._entitySelectionService.SelectAndFocusOn(entity);
			}, 0);
			Image image = UQueryExtensions.Q<Image>(visualElement, "Image", null);
			image.sprite = component.Image;
			this._tooltipRegistrar.Register(image, component.DisplayName);
			DistrictBuildingDistance districtBuildingDistance = entity.GetComponent<DistrictBuildingDistance>();
			Label label = UQueryExtensions.Q<Label>(visualElement, "DistanceText", null);
			this._tooltipRegistrar.RegisterUpdatable(label, () => districtBuildingDistance.DescribeDistance());
			PausableBuilding component2 = entity.GetComponent<PausableBuilding>();
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(visualElement, "PausableWrapper", null);
			Toggle pausableToggle = UQueryExtensions.Q<Toggle>(visualElement, "PausableToggle", null);
			BuildingBatchControlRowItemFactory.InitializePausableBuilding(component2, visualElement2, pausableToggle);
			ConstructionSite component3 = entity.GetComponent<ConstructionSite>();
			ConstructionSiteDescriber constructionSiteDescriber = component3.GetComponent<ConstructionSiteDescriber>();
			VisualElement visualElement3 = UQueryExtensions.Q<VisualElement>(visualElement, "ConstructionWrapper", null);
			Label constructionProgressLabel = UQueryExtensions.Q<Label>(visualElement, "ProgressText", null);
			VisualElement constructionProgressBar = UQueryExtensions.Q<VisualElement>(visualElement, "Progress", null);
			this._tooltipRegistrar.RegisterUpdatable(visualElement3, () => constructionSiteDescriber.GetProgressInfoFull());
			return new BuildingBatchControlRowItem(this._distanceToColorConverter, visualElement, districtBuildingDistance, label, visualElement2, pausableToggle, component2, component3, visualElement3, constructionProgressLabel, constructionProgressBar);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000268C File Offset: 0x0000088C
		public static void InitializePausableBuilding(PausableBuilding pausableBuilding, VisualElement toggleWrapper, Toggle pausableToggle)
		{
			bool visible = pausableBuilding && pausableBuilding.IsPausable();
			toggleWrapper.ToggleDisplayStyle(visible);
			if (pausableBuilding)
			{
				pausableToggle.SetValueWithoutNotify(!pausableBuilding.Paused);
				INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(pausableToggle, delegate(ChangeEvent<bool> evt)
				{
					BuildingBatchControlRowItemFactory.ToggleActivationState(evt.newValue, pausableBuilding);
				});
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026FD File Offset: 0x000008FD
		public static void ToggleActivationState(bool resume, PausableBuilding pausableBuilding)
		{
			if (resume)
			{
				pausableBuilding.Resume();
				return;
			}
			pausableBuilding.Pause();
		}

		// Token: 0x0400001D RID: 29
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001E RID: 30
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400001F RID: 31
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000020 RID: 32
		public readonly DistanceToColorConverter _distanceToColorConverter;
	}
}
