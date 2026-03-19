using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Coordinates;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.CharacterControlSystem
{
	// Token: 0x02000004 RID: 4
	public class CharacterControlRootBehavior : RootBehavior, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public void Awake()
		{
			this._controllableCharacter = base.GetComponent<ControllableCharacter>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public void Start()
		{
			this._walkToPositionExecutor = base.GetComponent<WalkToPositionExecutor>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._controllableCharacter.UnderControl)
			{
				return Decision.ReleaseNow();
			}
			Vector3 position = CoordinateSystem.GridToWorld(this._controllableCharacter.Destination);
			switch (this._walkToPositionExecutor.Launch(position))
			{
			case ExecutorStatus.Success:
				this._controllableCharacter.PlayAnimation();
				return Decision.ReturnNextTick();
			case ExecutorStatus.Failure:
				return Decision.ReleaseNow();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkToPositionExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04000006 RID: 6
		public WalkToPositionExecutor _walkToPositionExecutor;

		// Token: 0x04000007 RID: 7
		public ControllableCharacter _controllableCharacter;
	}
}
