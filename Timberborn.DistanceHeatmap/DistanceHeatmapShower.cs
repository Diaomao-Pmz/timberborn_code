using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.BuildingsNavigation;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.DistanceHeatmap
{
	// Token: 0x02000006 RID: 6
	public class DistanceHeatmapShower : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021AC File Offset: 0x000003AC
		public DistanceHeatmapShower(Highlighter highlighter, DistanceToColorConverter distanceToColorConverter)
		{
			this._highlighter = highlighter;
			this._distanceToColorConverter = distanceToColorConverter;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C4 File Offset: 0x000003C4
		public void Awake()
		{
			this._districtBuildingRegistry = base.GetComponent<DistrictBuildingRegistry>();
			this._districtBuildingRegistry.FinishedBuildingInstantRegistered += this.OnFinishedBuildingInstantRegistered;
			this._districtBuildingRegistry.FinishedBuildingInstantUnregistered += this.OnFinishedBuildingInstantUnregistered;
			this._accessible = base.GetComponent<BuildingAccessible>().Accessible;
			base.DisableComponent();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002224 File Offset: 0x00000424
		public void ShowHeatmap()
		{
			foreach (BuildingAccessible buildingAccessible in this._districtBuildingRegistry.GetEnabledBuildingsInstant<BuildingAccessible>())
			{
				this.Highlight(buildingAccessible);
			}
			base.EnableComponent();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000227C File Offset: 0x0000047C
		public void HideHeatmap()
		{
			this._highlighter.UnhighlightAllSecondary();
			base.DisableComponent();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00000490
		public void Highlight(BuildingAccessible buildingAccessible)
		{
			float num;
			if (!this._accessible.FindRoadPath(buildingAccessible.Accessible, out num))
			{
				this._accessible.FindInstantRoadPath(buildingAccessible.Accessible, out num);
			}
			if (num > 0f)
			{
				Color color = this._distanceToColorConverter.DistanceToColor((float)((int)num));
				this._highlighter.HighlightSecondary(buildingAccessible, color * DistanceHeatmapShower.DarkeningFactor);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F4 File Offset: 0x000004F4
		public void OnFinishedBuildingInstantRegistered(object sender, FinishedBuildingInstantRegisteredEventArgs e)
		{
			if (base.Enabled)
			{
				BuildingAccessible component = e.Building.GetComponent<BuildingAccessible>();
				if (component != null)
				{
					this.Highlight(component);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000231F File Offset: 0x0000051F
		public void OnFinishedBuildingInstantUnregistered(object sender, FinishedBuildingInstantUnregisteredEventArgs e)
		{
			if (base.Enabled)
			{
				this._highlighter.UnhighlightSecondary(e.Building);
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly float DarkeningFactor = 0.5f;

		// Token: 0x04000009 RID: 9
		public readonly Highlighter _highlighter;

		// Token: 0x0400000A RID: 10
		public readonly DistanceToColorConverter _distanceToColorConverter;

		// Token: 0x0400000B RID: 11
		public DistrictBuildingRegistry _districtBuildingRegistry;

		// Token: 0x0400000C RID: 12
		public Accessible _accessible;
	}
}
