using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.Wandering
{
	// Token: 0x0200000A RID: 10
	public class StrandedRootBehavior : RootBehavior, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002390 File Offset: 0x00000590
		public StrandedRootBehavior(IRandomNumberGenerator randomNumberGenerator, RandomDestinationPicker randomDestinationPicker)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._randomDestinationPicker = randomDestinationPicker;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023A6 File Offset: 0x000005A6
		public void Awake()
		{
			this._citizen = base.GetComponent<Citizen>();
			this._enterer = base.GetComponent<Enterer>();
			this._walkToPositionExecutor = base.GetComponent<WalkToPositionExecutor>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023CC File Offset: 0x000005CC
		public void Start()
		{
			this._animateExecutor = base.GetComponent<AnimateExecutor>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023DC File Offset: 0x000005DC
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._citizen.HasAssignedDistrict)
			{
				return Decision.ReleaseNow();
			}
			Vector3 position;
			if (!this._enterer.IsInside || !this._randomDestinationPicker.TryGetSafeRandomDestination(this._citizen, out position))
			{
				this._animateExecutor.Launch("Stranded", this.RandomWaitingTimeInHours);
				return Decision.ReleaseWhenFinished(this._animateExecutor);
			}
			ExecutorStatus executorStatus = this._walkToPositionExecutor.Launch(position);
			if (executorStatus <= ExecutorStatus.Failure)
			{
				return Decision.ReturnNextTick();
			}
			if (executorStatus != ExecutorStatus.Running)
			{
				throw new ArgumentOutOfRangeException();
			}
			return Decision.ReturnWhenFinished(this._walkToPositionExecutor);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000246E File Offset: 0x0000066E
		public float RandomWaitingTimeInHours
		{
			get
			{
				return this._randomNumberGenerator.Range(0.8f, 1.2f);
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000B RID: 11
		public readonly RandomDestinationPicker _randomDestinationPicker;

		// Token: 0x0400000C RID: 12
		public Citizen _citizen;

		// Token: 0x0400000D RID: 13
		public Enterer _enterer;

		// Token: 0x0400000E RID: 14
		public WalkToPositionExecutor _walkToPositionExecutor;

		// Token: 0x0400000F RID: 15
		public AnimateExecutor _animateExecutor;
	}
}
