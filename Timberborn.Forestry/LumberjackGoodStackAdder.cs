using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Cutting;
using Timberborn.EntitySystem;
using Timberborn.GoodStackSystem;

namespace Timberborn.Forestry
{
	// Token: 0x0200000F RID: 15
	public class LumberjackGoodStackAdder : BaseComponent, IAwakableComponent, IStartableComponent, IDeletableEntity
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000027A8 File Offset: 0x000009A8
		public LumberjackGoodStackAdder(GoodStackService<LumberjackFlagSpec> goodStackService)
		{
			this._goodStackService = goodStackService;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000027B8 File Offset: 0x000009B8
		public void Awake()
		{
			Cuttable component = base.GetComponent<Cuttable>();
			if (component != null)
			{
				this._cuttable = component;
				this._goodStack = base.GetComponent<GoodStack>();
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000027E4 File Offset: 0x000009E4
		public void Start()
		{
			if (this._cuttable)
			{
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
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002832 File Offset: 0x00000A32
		public void DeleteEntity()
		{
			this.RemoveGoodStack();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000283A File Offset: 0x00000A3A
		public void AddGoodStack()
		{
			if (this._goodStack.Enabled)
			{
				this._goodStackService.Add(this._goodStack);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000285A File Offset: 0x00000A5A
		public void RemoveGoodStack()
		{
			this._goodStackService.Remove(this._goodStack);
		}

		// Token: 0x04000018 RID: 24
		public readonly GoodStackService<LumberjackFlagSpec> _goodStackService;

		// Token: 0x04000019 RID: 25
		public Cuttable _cuttable;

		// Token: 0x0400001A RID: 26
		public GoodStack _goodStack;
	}
}
