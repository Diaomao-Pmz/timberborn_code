using System;
using Timberborn.ReservableSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200000D RID: 13
	public class RemoveYieldExecutor : WorkAtReservableExecutor
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000028C4 File Offset: 0x00000AC4
		public RemoveYieldExecutor(IDayNightCycle dayNightCycle) : base(dayNightCycle)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028CD File Offset: 0x00000ACD
		public bool Remove()
		{
			if (this._yielderRemover.HasReservedYielder)
			{
				base.Launch(this._yielderRemover.ReservedYielder.RemovalTimeInHours);
				return true;
			}
			return false;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028F5 File Offset: 0x00000AF5
		public override string Animation
		{
			get
			{
				return this._yielderRemover.ReservedYielder.Animation;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002907 File Offset: 0x00000B07
		public override Reservable Reservable
		{
			get
			{
				if (!this._yielderRemover.HasReservedYielder)
				{
					return null;
				}
				return this._yielderRemover.ReservedYielder.Reservable;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002928 File Offset: 0x00000B28
		public override void Initialize()
		{
			this._yielderRemover = base.GetComponent<YielderRemover>();
			this._yieldRemovalSuccessValidator = base.GetComponent<YieldRemovalSuccessValidator>();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002942 File Offset: 0x00000B42
		public override bool CanComplete()
		{
			return this._yieldRemovalSuccessValidator.ValidateYieldSuccess();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000294F File Offset: 0x00000B4F
		public override void PerformActionOnComplete()
		{
			this._yielderRemover.CompleteReservation();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000295C File Offset: 0x00000B5C
		public override void PerformActionOnTick(float deltaTime)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000295E File Offset: 0x00000B5E
		public override void Unreserve()
		{
			this._yielderRemover.Unreserve();
		}

		// Token: 0x04000018 RID: 24
		public YielderRemover _yielderRemover;

		// Token: 0x04000019 RID: 25
		public YieldRemovalSuccessValidator _yieldRemovalSuccessValidator;
	}
}
