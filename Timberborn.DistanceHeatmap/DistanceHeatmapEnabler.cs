using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;

namespace Timberborn.DistanceHeatmap
{
	// Token: 0x02000005 RID: 5
	public class DistanceHeatmapEnabler : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002115 File Offset: 0x00000315
		public void Awake()
		{
			this._pathDistrictRetriever = base.GetComponent<PathDistrictRetriever>();
			base.DisableComponent();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002129 File Offset: 0x00000329
		public void Update()
		{
			this.ShowHeatmap();
			base.DisableComponent();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002137 File Offset: 0x00000337
		public void OnSelect()
		{
			base.EnableComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213F File Offset: 0x0000033F
		public void OnUnselect()
		{
			this.HideHeatmap();
			base.DisableComponent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		public void ShowHeatmap()
		{
			DistrictCenter finishedDistrictCenter = this._pathDistrictRetriever.GetFinishedDistrictCenter();
			if (finishedDistrictCenter != null)
			{
				this._shownDistanceHeatmapShower = finishedDistrictCenter.GetComponent<DistanceHeatmapShower>();
				this._shownDistanceHeatmapShower.ShowHeatmap();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002183 File Offset: 0x00000383
		public void HideHeatmap()
		{
			if (this._shownDistanceHeatmapShower)
			{
				this._shownDistanceHeatmapShower.HideHeatmap();
				this._shownDistanceHeatmapShower = null;
			}
		}

		// Token: 0x04000006 RID: 6
		public PathDistrictRetriever _pathDistrictRetriever;

		// Token: 0x04000007 RID: 7
		public DistanceHeatmapShower _shownDistanceHeatmapShower;
	}
}
