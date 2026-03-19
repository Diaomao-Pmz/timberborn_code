using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.InventorySystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000010 RID: 16
	public class ConstructionSiteBuildersLimiter : BaseComponent, IAwakableComponent, IInitializableEntity, IUnfinishedStateListener
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003206 File Offset: 0x00001406
		public void Awake()
		{
			this._constructionSite = base.GetComponent<ConstructionSite>();
			this._constructionSiteReservations = base.GetComponent<ConstructionSiteReservations>();
			this._constructionSiteBuildersLimiterSpec = base.GetComponent<ConstructionSiteBuildersLimiterSpec>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000322C File Offset: 0x0000142C
		public void InitializeEntity()
		{
			this.UpdateBuildersCapacity();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003234 File Offset: 0x00001434
		public void OnEnterUnfinishedState()
		{
			this._constructionSite.Inventory.InventoryChanged += this.OnInventoryChanged;
			this._constructionSite.OnConstructionSiteProgressed += this.OnConstructionSiteProgressed;
			this.UpdateBuildersCapacity();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000326F File Offset: 0x0000146F
		public void OnExitUnfinishedState()
		{
			this._constructionSite.Inventory.InventoryChanged -= this.OnInventoryChanged;
			this._constructionSite.OnConstructionSiteProgressed -= this.OnConstructionSiteProgressed;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000322C File Offset: 0x0000142C
		public void OnConstructionSiteProgressed(object sender, EventArgs e)
		{
			this.UpdateBuildersCapacity();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000322C File Offset: 0x0000142C
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateBuildersCapacity();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000032A4 File Offset: 0x000014A4
		public void UpdateBuildersCapacity()
		{
			this._constructionSiteReservations.SetCapacity(this.CalculateBuildersCapacity());
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000032B8 File Offset: 0x000014B8
		public int CalculateBuildersCapacity()
		{
			if (this._constructionSite.Inventory.IsFull)
			{
				return this._constructionSiteBuildersLimiterSpec.MaxAllowedBuilders;
			}
			float num = this._constructionSite.MaterialProgress - this._constructionSite.BuildTimeProgress;
			if (num <= ConstructionSiteBuildersLimiter.ProgressGapNoBuilders)
			{
				return 0;
			}
			if (num >= ConstructionSiteBuildersLimiter.ProgressGapFullBuilders)
			{
				return this._constructionSiteBuildersLimiterSpec.MaxAllowedBuilders;
			}
			return (int)Math.Ceiling((double)((float)this._constructionSiteBuildersLimiterSpec.MaxAllowedBuilders * (num - ConstructionSiteBuildersLimiter.ProgressGapNoBuilders) / (ConstructionSiteBuildersLimiter.ProgressGapFullBuilders - ConstructionSiteBuildersLimiter.ProgressGapNoBuilders)));
		}

		// Token: 0x04000041 RID: 65
		public static readonly float ProgressGapNoBuilders = 0.1f;

		// Token: 0x04000042 RID: 66
		public static readonly float ProgressGapFullBuilders = 0.3f;

		// Token: 0x04000043 RID: 67
		public ConstructionSite _constructionSite;

		// Token: 0x04000044 RID: 68
		public ConstructionSiteReservations _constructionSiteReservations;

		// Token: 0x04000045 RID: 69
		public ConstructionSiteBuildersLimiterSpec _constructionSiteBuildersLimiterSpec;
	}
}
