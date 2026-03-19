using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Cutting;
using Timberborn.EntitySystem;
using Timberborn.GoodStackSystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.Yielding;

namespace Timberborn.Fields
{
	// Token: 0x02000007 RID: 7
	public class Crop : BaseComponent, IAwakableComponent, IStartableComponent, IDeletableEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public Crop(GoodStackService<FarmHouse> goodStackService)
		{
			this._goodStackService = goodStackService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210D File Offset: 0x0000030D
		public Yielder Yielder
		{
			get
			{
				return this._cuttable.Yielder;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000211A File Offset: 0x0000031A
		public void Awake()
		{
			this._cuttable = base.GetComponent<Cuttable>();
			this._goodStack = base.GetComponent<GoodStack>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002134 File Offset: 0x00000334
		public void Start()
		{
			LivingNaturalResource component = base.GetComponent<LivingNaturalResource>();
			if (component.IsDead)
			{
				this.Disable();
			}
			else
			{
				component.Died += delegate(object _, EventArgs _)
				{
					this.Disable();
				};
			}
			this._cuttable.WasCut += delegate(object _, EventArgs _)
			{
				this.AddGoodStack();
			};
			this._goodStack.GoodStackDisabled += delegate(object _, EventArgs _)
			{
				this.RemoveGoodStack();
			};
			this.AddGoodStack();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219E File Offset: 0x0000039E
		public void DeleteEntity()
		{
			this.RemoveGoodStack();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A6 File Offset: 0x000003A6
		public void AddGoodStack()
		{
			if (this._goodStack.Enabled)
			{
				this._goodStackService.Add(this._goodStack);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C6 File Offset: 0x000003C6
		public void RemoveGoodStack()
		{
			this._goodStackService.Remove(this._goodStack);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D9 File Offset: 0x000003D9
		public void Disable()
		{
			this._cuttable.Yielder.Disable();
		}

		// Token: 0x04000008 RID: 8
		public readonly GoodStackService<FarmHouse> _goodStackService;

		// Token: 0x04000009 RID: 9
		public GoodStack _goodStack;

		// Token: 0x0400000A RID: 10
		public Cuttable _cuttable;
	}
}
