using System;
using Timberborn.BatchControl;
using Timberborn.Buildings;
using Timberborn.BuildingsNavigation;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.BuildingsUI
{
	// Token: 0x02000006 RID: 6
	public class BuildingBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000231A File Offset: 0x0000051A
		public VisualElement Root { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002324 File Offset: 0x00000524
		public BuildingBatchControlRowItem(DistanceToColorConverter distanceToColorConverter, VisualElement root, DistrictBuildingDistance districtBuildingDistance, Label distanceLabel, VisualElement pausableWrapper, Toggle pausableToggle, PausableBuilding pausableBuilding, ConstructionSite constructionSite, VisualElement constructionWrapper, Label constructionProgressLabel, VisualElement constructionProgressBar)
		{
			this._distanceToColorConverter = distanceToColorConverter;
			this.Root = root;
			this._districtBuildingDistance = districtBuildingDistance;
			this._distanceLabel = distanceLabel;
			this._pausableWrapper = pausableWrapper;
			this._pausableToggle = pausableToggle;
			this._pausableBuilding = pausableBuilding;
			this._constructionSite = constructionSite;
			this._constructionWrapper = constructionWrapper;
			this._constructionProgressLabel = constructionProgressLabel;
			this._constructionProgressBar = constructionProgressBar;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000238C File Offset: 0x0000058C
		public void UpdateRowItem()
		{
			this.UpdatePausable();
			this.UpdateDistance();
			this.UpdateConstructionSiteInfo();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A0 File Offset: 0x000005A0
		public void UpdatePausable()
		{
			if (this._pausableBuilding && this._pausableBuilding.IsPausable())
			{
				this._pausableToggle.SetValueWithoutNotify(!this._pausableBuilding.Paused);
				this._pausableWrapper.ToggleDisplayStyle(true);
				return;
			}
			this._pausableWrapper.ToggleDisplayStyle(false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023FC File Offset: 0x000005FC
		public void UpdateDistance()
		{
			int num;
			if (this._districtBuildingDistance && this._districtBuildingDistance.TryGetDistanceToDistrict(out num))
			{
				this._distanceLabel.text = NumberFormatter.Format(num);
				this._distanceLabel.style.color = this._distanceToColorConverter.DistanceToColor((float)num);
				this._distanceLabel.parent.ToggleDisplayStyle(true);
				return;
			}
			this._distanceLabel.parent.ToggleDisplayStyle(false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000247C File Offset: 0x0000067C
		public void UpdateConstructionSiteInfo()
		{
			if (this._constructionSite.Enabled)
			{
				float buildTimeProgress = this._constructionSite.BuildTimeProgress;
				string text = NumberFormatter.FormatAsPercentFloored((double)buildTimeProgress);
				this._constructionProgressLabel.text = text;
				this._constructionProgressBar.style.width = new StyleLength(Length.Percent(buildTimeProgress * 100f));
				this._constructionWrapper.ToggleDisplayStyle(true);
				return;
			}
			this._constructionWrapper.ToggleDisplayStyle(false);
		}

		// Token: 0x04000013 RID: 19
		public readonly DistanceToColorConverter _distanceToColorConverter;

		// Token: 0x04000014 RID: 20
		public readonly DistrictBuildingDistance _districtBuildingDistance;

		// Token: 0x04000015 RID: 21
		public readonly VisualElement _pausableWrapper;

		// Token: 0x04000016 RID: 22
		public readonly Toggle _pausableToggle;

		// Token: 0x04000017 RID: 23
		public readonly PausableBuilding _pausableBuilding;

		// Token: 0x04000018 RID: 24
		public readonly ConstructionSite _constructionSite;

		// Token: 0x04000019 RID: 25
		public readonly VisualElement _constructionWrapper;

		// Token: 0x0400001A RID: 26
		public readonly Label _constructionProgressLabel;

		// Token: 0x0400001B RID: 27
		public readonly Label _distanceLabel;

		// Token: 0x0400001C RID: 28
		public readonly VisualElement _constructionProgressBar;
	}
}
