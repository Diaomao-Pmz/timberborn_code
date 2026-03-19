using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.GoodStackSystem;
using Timberborn.WorldPersistence;
using Timberborn.Yielding;

namespace Timberborn.Gathering
{
	// Token: 0x02000007 RID: 7
	public class Gatherable : BaseComponent, IAwakableComponent, IDeletableEntity, IPostInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler Gathered;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002175 File Offset: 0x00000375
		public Yielder Yielder { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
		public Gatherable(GoodStackService<GathererFlag> goodStackService, IGoodService goodService)
		{
			this._goodStackService = goodStackService;
			this._goodService = goodService;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002194 File Offset: 0x00000394
		public float YieldGrowthTimeInDays
		{
			get
			{
				return this._gatherableSpec.YieldGrowthTimeInDays;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021A1 File Offset: 0x000003A1
		public YielderSpec YielderSpec
		{
			get
			{
				return this._gatherableSpec.Yielder;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021AE File Offset: 0x000003AE
		public bool UsableWithCurrentFeatureToggles
		{
			get
			{
				return this._goodService.HasGood(this.YielderSpec.Yield.Id);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CB File Offset: 0x000003CB
		public void Awake()
		{
			this._goodStack = base.GetComponent<GoodStack>();
			this._gatherableSpec = base.GetComponent<GatherableSpec>();
			this._gatherableModel = base.GetComponent<GatherableModel>();
			this.Yielder = this.GetNamedComponent(this.YielderSpec.YielderComponentName);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002208 File Offset: 0x00000408
		public void PostInitializeEntity()
		{
			if (this.UsableWithCurrentFeatureToggles)
			{
				this.Yielder.YieldDecreased += delegate(object _, EventArgs _)
				{
					this.OnYieldDecreased();
				};
				this._goodStack.GoodStackDisabled += delegate(object _, EventArgs _)
				{
					this.RemoveGoodStack();
				};
				this.AddGoodStack();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002246 File Offset: 0x00000446
		public void DeleteEntity()
		{
			this.RemoveGoodStack();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000224E File Offset: 0x0000044E
		public void UpdateModel()
		{
			this._gatherableModel.UpdateMaterial(this.Yielder.IsYielding);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002268 File Offset: 0x00000468
		public void OnYieldDecreased()
		{
			if (this.Yielder.Yield.Amount > 0)
			{
				this._goodStack.EnableGoodStack(this.Yielder.Yield);
				this.Yielder.RemoveRemainingYield();
			}
			this.AddGoodStack();
			EventHandler gathered = this.Gathered;
			if (gathered == null)
			{
				return;
			}
			gathered(this, EventArgs.Empty);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022C8 File Offset: 0x000004C8
		public void AddGoodStack()
		{
			if (this._goodStack.Enabled)
			{
				this._goodStackService.Add(this._goodStack);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022E8 File Offset: 0x000004E8
		public void RemoveGoodStack()
		{
			this._goodStackService.Remove(this._goodStack);
		}

		// Token: 0x0400000A RID: 10
		public readonly GoodStackService<GathererFlag> _goodStackService;

		// Token: 0x0400000B RID: 11
		public readonly IGoodService _goodService;

		// Token: 0x0400000C RID: 12
		public GoodStack _goodStack;

		// Token: 0x0400000D RID: 13
		public GatherableSpec _gatherableSpec;

		// Token: 0x0400000E RID: 14
		public GatherableModel _gatherableModel;
	}
}
