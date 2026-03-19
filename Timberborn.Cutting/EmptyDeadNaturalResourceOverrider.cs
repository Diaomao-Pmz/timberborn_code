using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GoodStackSystem;
using Timberborn.NaturalResourcesLifecycle;

namespace Timberborn.Cutting
{
	// Token: 0x0200000D RID: 13
	public class EmptyDeadNaturalResourceOverrider : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002740 File Offset: 0x00000940
		public void Awake()
		{
			this._cuttable = base.GetComponent<Cuttable>();
			this._goodStack = base.GetComponent<GoodStack>();
			this._blockObject = base.GetComponent<BlockObject>();
			base.GetComponent<LivingNaturalResource>().Died += delegate(object _, EventArgs _)
			{
				this.MakeOverridable();
			};
			base.GetComponent<LivingNaturalResource>().ReversedDeath += delegate(object _, EventArgs _)
			{
				this.MakeNonOverridable();
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000279F File Offset: 0x0000099F
		public void MakeNonOverridable()
		{
			this._blockObject.MakeNonOverridable();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027AC File Offset: 0x000009AC
		public void MakeOverridable()
		{
			if (!this.CuttableWithYield() && !this.GoodStackWithGood())
			{
				this._blockObject.MakeOverridable();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027CC File Offset: 0x000009CC
		public bool CuttableWithYield()
		{
			return this._cuttable && this._cuttable.Yielder.Yield.Amount > 0 && !this._cuttable.RemoveOnCut;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002811 File Offset: 0x00000A11
		public bool GoodStackWithGood()
		{
			return this._goodStack && !this._goodStack.Inventory.IsEmpty;
		}

		// Token: 0x04000016 RID: 22
		public Cuttable _cuttable;

		// Token: 0x04000017 RID: 23
		public GoodStack _goodStack;

		// Token: 0x04000018 RID: 24
		public BlockObject _blockObject;
	}
}
