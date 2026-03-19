using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.Goods;

namespace Timberborn.Carrying
{
	// Token: 0x02000016 RID: 22
	public class Overburdenable : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003731 File Offset: 0x00001931
		public Overburdenable(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003740 File Offset: 0x00001940
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._overburdenableSpec = base.GetComponent<OverburdenableSpec>();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003766 File Offset: 0x00001966
		public void Start()
		{
			this._goodCarrier.CarriedGoodsChanged += this.OnCarriedGoodsChanged;
			this.CheckIfOverburdened(this._goodCarrier.CarriedGoods);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003790 File Offset: 0x00001990
		public void OnCarriedGoodsChanged(object sender, CarriedGoodsChangedEventArgs e)
		{
			this.CheckIfOverburdened(e.CarriedGoods);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000037A0 File Offset: 0x000019A0
		public void CheckIfOverburdened(GoodAmount goodAmount)
		{
			if (!this._isOverburdened && this._goodCarrier.IsCarrying && this.IsCarryingTooMuch(goodAmount))
			{
				this.AddOverburdenedBonuses();
				return;
			}
			if (this._isOverburdened && !this._goodCarrier.IsCarrying)
			{
				this.RemoveOverburdenedBonuses();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000037F0 File Offset: 0x000019F0
		public bool IsCarryingTooMuch(GoodAmount goodAmount)
		{
			int weight = this._goodService.GetGood(goodAmount.GoodId).Weight;
			return goodAmount.Amount * weight > this._goodCarrier.LiftingCapacity;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000382B File Offset: 0x00001A2B
		public void AddOverburdenedBonuses()
		{
			this._bonusManager.AddBonuses(this._overburdenableSpec.OverburdenedBonuses);
			this._isOverburdened = true;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000384F File Offset: 0x00001A4F
		public void RemoveOverburdenedBonuses()
		{
			this._bonusManager.RemoveBonuses(this._overburdenableSpec.OverburdenedBonuses);
			this._isOverburdened = false;
		}

		// Token: 0x04000049 RID: 73
		public readonly IGoodService _goodService;

		// Token: 0x0400004A RID: 74
		public BonusManager _bonusManager;

		// Token: 0x0400004B RID: 75
		public GoodCarrier _goodCarrier;

		// Token: 0x0400004C RID: 76
		public OverburdenableSpec _overburdenableSpec;

		// Token: 0x0400004D RID: 77
		public bool _isOverburdened;
	}
}
