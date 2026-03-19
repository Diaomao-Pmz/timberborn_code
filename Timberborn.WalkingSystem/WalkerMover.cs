using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EnterableSystem;
using Timberborn.TickSystem;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200001A RID: 26
	public class WalkerMover : TickableComponent, IAwakableComponent, ILateTickable
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003923 File Offset: 0x00001B23
		public WalkerMover(ITickService tickService)
		{
			this._tickService = tickService;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003932 File Offset: 0x00001B32
		public void Awake()
		{
			this._enterer = base.GetComponent<Enterer>();
			this._walker = base.GetComponent<Walker>();
			this._walkerSpeedManager = base.GetComponent<WalkerSpeedManager>();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003958 File Offset: 0x00001B58
		public override void Tick()
		{
			if (!this._walker.Stopped())
			{
				this.Move();
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003970 File Offset: 0x00001B70
		public void Move()
		{
			if (this._enterer.IsInside)
			{
				this._enterer.Exit();
				return;
			}
			this._walker.PathFollower.MoveAlongPath(this._tickService.TickIntervalInSeconds, WalkerMover.WalkingAnimation, new Func<float>(this._walkerSpeedManager.GetWalkerSpeedAtCurrentPosition));
		}

		// Token: 0x04000053 RID: 83
		public static readonly string WalkingAnimation = "Walking";

		// Token: 0x04000054 RID: 84
		public readonly ITickService _tickService;

		// Token: 0x04000055 RID: 85
		public Enterer _enterer;

		// Token: 0x04000056 RID: 86
		public Walker _walker;

		// Token: 0x04000057 RID: 87
		public WalkerSpeedManager _walkerSpeedManager;
	}
}
