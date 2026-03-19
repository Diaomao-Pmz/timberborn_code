using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.Goods;

namespace Timberborn.Yielding
{
	// Token: 0x0200001B RID: 27
	public class YieldRemovalSuccessValidator : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003C05 File Offset: 0x00001E05
		public YieldRemovalSuccessValidator(YieldRemovalChanceBonusService yieldRemovalChanceBonusService)
		{
			this._yieldRemovalChanceBonusService = yieldRemovalChanceBonusService;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003C14 File Offset: 0x00001E14
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
			this._yielderRemover = base.GetComponent<YielderRemover>();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003C30 File Offset: 0x00001E30
		public bool ValidateYieldSuccess()
		{
			GoodAmount yield = this._yielderRemover.ReservedYielder.Yield;
			return this._yieldRemovalChanceBonusService.CheckYieldRemovalSuccess(this._bonusManager, yield.GoodId);
		}

		// Token: 0x0400004E RID: 78
		public readonly YieldRemovalChanceBonusService _yieldRemovalChanceBonusService;

		// Token: 0x0400004F RID: 79
		public BonusManager _bonusManager;

		// Token: 0x04000050 RID: 80
		public YielderRemover _yielderRemover;

		// Token: 0x04000051 RID: 81
		public RemoveYieldExecutor _removeYieldExecutor;
	}
}
