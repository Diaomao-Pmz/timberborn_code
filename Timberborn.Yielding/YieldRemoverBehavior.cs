using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.InventorySystem;
using Timberborn.ReservableSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200001C RID: 28
	public class YieldRemoverBehavior : Behavior, IAwakableComponent, IStartableComponent, IJobBehavior
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003C66 File Offset: 0x00001E66
		public void Awake()
		{
			this._yielderRemover = base.GetComponent<YielderRemover>();
			this._goodReserver = base.GetComponent<GoodReserver>();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003C80 File Offset: 0x00001E80
		public void Start()
		{
			this._walkToReservableExecutor = base.GetComponent<WalkToReservableExecutor>();
			this._removeYieldExecutor = base.GetComponent<RemoveYieldExecutor>();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003C9C File Offset: 0x00001E9C
		public override Decision Decide(BehaviorAgent agent)
		{
			Yielder reservedYielder = this._yielderRemover.ReservedYielder;
			if (!reservedYielder || !reservedYielder.RemoveYieldStrategy.IsStillRemovable)
			{
				return this.UnreserveYielder();
			}
			switch (this._walkToReservableExecutor.Launch(reservedYielder.RemoveYieldStrategy.Reacher))
			{
			case ExecutorStatus.Success:
				return this.RemoveYielder();
			case ExecutorStatus.Failure:
				return this.UnreserveYielder();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkToReservableExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003D1B File Offset: 0x00001F1B
		public Decision RemoveYielder()
		{
			if (!this._removeYieldExecutor.Remove())
			{
				return Decision.ReturnNextTick();
			}
			return Decision.ReleaseWhenFinished(this._removeYieldExecutor);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003D3B File Offset: 0x00001F3B
		public Decision UnreserveYielder()
		{
			this._goodReserver.UnreserveCapacity();
			this._yielderRemover.Unreserve();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x04000052 RID: 82
		public YielderRemover _yielderRemover;

		// Token: 0x04000053 RID: 83
		public GoodReserver _goodReserver;

		// Token: 0x04000054 RID: 84
		public WalkToReservableExecutor _walkToReservableExecutor;

		// Token: 0x04000055 RID: 85
		public RemoveYieldExecutor _removeYieldExecutor;
	}
}
