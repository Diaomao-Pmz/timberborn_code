using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.ConstructionSites;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x0200000A RID: 10
	public class LinkedConstructionSite : BaseComponent, IAwakableComponent, IInitializableEntity, IUnfinishedStateListener, IFinishedStateListener, IConstructionFinishBlocker
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000244C File Offset: 0x0000064C
		public bool IsFinishBlocked
		{
			get
			{
				if (this.IsUnfinished && this._linked != null)
				{
					LinkedConstructionSite linked = this._linked;
					return linked != null && linked.IsUnfinished && !this._linked._constructionSite.IsReadyToFinish;
				}
				return true;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002492 File Offset: 0x00000692
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._constructionSite = base.GetComponent<ConstructionSite>();
			base.GetComponent<LinkedBuilding>().BuildingLinked += this.OnBuildingLinked;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024C4 File Offset: 0x000006C4
		public void InitializeEntity()
		{
			if (this.IsUnfinished)
			{
				this._constructionSite.OnConstructionSiteReserved += this.OnConstructionSiteReserved;
				this._constructionSite.OnConstructionSiteUnreserved += this.OnConstructionSiteUnreserved;
				this._constructionSite.OnConstructionSiteProgressed += this.OnConstructionSiteProgressed;
				this._constructionSite.Inventory.InventoryStockChanged += this.OnInventoryStockChanged;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002218 File Offset: 0x00000418
		public void OnEnterUnfinishedState()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000253C File Offset: 0x0000073C
		public void OnExitUnfinishedState()
		{
			this._constructionSite.OnConstructionSiteReserved -= this.OnConstructionSiteReserved;
			this._constructionSite.OnConstructionSiteUnreserved -= this.OnConstructionSiteUnreserved;
			this._constructionSite.OnConstructionSiteProgressed -= this.OnConstructionSiteProgressed;
			this._constructionSite.Inventory.InventoryStockChanged -= this.OnInventoryStockChanged;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025AC File Offset: 0x000007AC
		public void OnEnterFinishedState()
		{
			LinkedConstructionSite linked = this._linked;
			if (linked != null && linked.IsUnfinished)
			{
				this._linked._constructionSite.FinishNow();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002218 File Offset: 0x00000418
		public void OnExitFinishedState()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025DB File Offset: 0x000007DB
		public bool IsUnfinished
		{
			get
			{
				return this._blockObject.IsUnfinished;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025E8 File Offset: 0x000007E8
		public void OnBuildingLinked(object sender, LinkedBuilding e)
		{
			this._linked = e.GetComponent<LinkedConstructionSite>();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025F8 File Offset: 0x000007F8
		public void OnConstructionSiteReserved(object sender, ConstructionSiteReservationEventArgs e)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				using (this._mirrorOperationLock.Lock())
				{
					this._linked.ReserveForBuild(e.Builder);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000264C File Offset: 0x0000084C
		public void OnConstructionSiteUnreserved(object sender, ConstructionSiteReservationEventArgs e)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				using (this._mirrorOperationLock.Lock())
				{
					this._linked.UnreserveForBuild(e.Builder);
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026A0 File Offset: 0x000008A0
		public void OnConstructionSiteProgressed(object sender, EventArgs e)
		{
			this._linked.EqualizeProgress();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026B0 File Offset: 0x000008B0
		public void OnInventoryStockChanged(object sender, InventoryAmountChangedEventArgs e)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				if (e.GoodAmount.Amount > 0)
				{
					this._linked.MirrorInventoryChange(e.GoodAmount);
					return;
				}
				if (e.GoodAmount.Amount < 0)
				{
					throw new NotSupportedException("Taking goods from a construction site is not supported.");
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000270C File Offset: 0x0000090C
		public void ReserveForBuild(Builder builder)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				using (this._mirrorOperationLock.Lock())
				{
					this._constructionSite.ReserveForBuild(builder);
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000275C File Offset: 0x0000095C
		public void UnreserveForBuild(Builder builder)
		{
			if (this._mirrorOperationLock.IsUnlocked)
			{
				using (this._mirrorOperationLock.Lock())
				{
					this._constructionSite.UnreserveForBuild(builder);
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027AC File Offset: 0x000009AC
		public void EqualizeProgress()
		{
			float num = this._linked._constructionSite.BuildTimeProgressInHours - this._constructionSite.BuildTimeProgressInHours;
			if (num > 0f)
			{
				this._constructionSite.IncreaseBuildTime(num);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027EC File Offset: 0x000009EC
		public void MirrorInventoryChange(GoodAmount goodAmount)
		{
			using (this._mirrorOperationLock.Lock())
			{
				this._constructionSite.Inventory.GiveIgnoringCapacityReservation(goodAmount);
				this._constructionSite.DeactivateLackOfResourcesStatus();
			}
		}

		// Token: 0x04000010 RID: 16
		public BlockObject _blockObject;

		// Token: 0x04000011 RID: 17
		public ConstructionSite _constructionSite;

		// Token: 0x04000012 RID: 18
		public LinkedConstructionSite _linked;

		// Token: 0x04000013 RID: 19
		public readonly MirrorOperationLock _mirrorOperationLock = new MirrorOperationLock();
	}
}
