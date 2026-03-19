using System;
using Timberborn.BlockSystem;
using Timberborn.ReservableSystem;
using Timberborn.TimeSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000019 RID: 25
	public class DemolishExecutor : WorkAtReservableExecutor
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003430 File Offset: 0x00001630
		public DemolishExecutor(IDayNightCycle dayNightCycle) : base(dayNightCycle)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003439 File Offset: 0x00001639
		public bool Demolish()
		{
			if (this._demolisher.HasReservedDemolishable)
			{
				base.Launch(DemolishExecutor.MaxDemolishingTimeInHours);
				return true;
			}
			return false;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003456 File Offset: 0x00001656
		public override string Animation
		{
			get
			{
				return "Building";
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000345D File Offset: 0x0000165D
		public override Reservable Reservable
		{
			get
			{
				if (!this._demolisher.HasReservedDemolishable)
				{
					return null;
				}
				return this._demolisher.Demolishable.Reservable;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000347E File Offset: 0x0000167E
		public override void Initialize()
		{
			this._demolisher = base.GetComponent<Demolisher>();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000348C File Offset: 0x0000168C
		public override bool CanComplete()
		{
			return this._demolisher.HasReservedDemolishable && this._demolisher.Demolishable.DemolishingProgress >= 1f && this._demolisher.Demolishable.GetComponent<BlockObject>().CanDelete();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000034C9 File Offset: 0x000016C9
		public override void PerformActionOnTick(float deltaTime)
		{
			if (this._demolisher.HasReservedDemolishable)
			{
				this._demolisher.Demolishable.ProgressDemolition(deltaTime);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000034E9 File Offset: 0x000016E9
		public override void PerformActionOnComplete()
		{
			this._demolisher.Demolish();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000034F6 File Offset: 0x000016F6
		public override void Unreserve()
		{
			this._demolisher.Unreserve();
		}

		// Token: 0x04000038 RID: 56
		public static readonly float MaxDemolishingTimeInHours = 0.5f;

		// Token: 0x04000039 RID: 57
		public Demolisher _demolisher;
	}
}
