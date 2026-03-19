using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.MortalSystem;
using Timberborn.Persistence;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.DeathSystem
{
	// Token: 0x02000005 RID: 5
	public class DieRootBehavior : RootBehavior, IAwakableComponent, IStartableComponent, IPersistentEntity
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public DieRootBehavior(RandomDestinationPicker randomDestinationPicker)
		{
			this._randomDestinationPicker = randomDestinationPicker;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
		public void Awake()
		{
			this._mortal = base.GetComponent<Mortal>();
			this._enterer = base.GetComponent<Enterer>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002106 File Offset: 0x00000306
		public void Start()
		{
			this._walkToPositionExecutor = base.GetComponent<WalkToPositionExecutor>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._mortal.IsTimeToDie)
			{
				return Decision.ReleaseNow();
			}
			if (this.WalkToDeathPositionBeforeDying && !this._wentToDeathPosition)
			{
				this._wentToDeathPosition = true;
				return this.GoToRandomDeathPosition();
			}
			this._mortal.DieIfItIsTime();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002162 File Offset: 0x00000362
		public void Save(IEntitySaver entitySaver)
		{
			if (this._wentToDeathPosition)
			{
				entitySaver.GetComponent(DieRootBehavior.MortalRootBehaviorKey).Set(DieRootBehavior.WentToDeathPositionKey, this._wentToDeathPosition);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(DieRootBehavior.MortalRootBehaviorKey, out objectLoader))
			{
				this._wentToDeathPosition = objectLoader.Get(DieRootBehavior.WentToDeathPositionKey);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B5 File Offset: 0x000003B5
		public bool WalkToDeathPositionBeforeDying
		{
			get
			{
				return this._mortal.ShouldDiePublicly || !this._enterer.IsInside;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
		public Decision GoToRandomDeathPosition()
		{
			Vector3 position = this._randomDestinationPicker.RandomDestination(this._citizen);
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

		// Token: 0x04000006 RID: 6
		public static readonly ComponentKey MortalRootBehaviorKey = new ComponentKey("MortalRootBehavior");

		// Token: 0x04000007 RID: 7
		public static readonly PropertyKey<bool> WentToDeathPositionKey = new PropertyKey<bool>("WentToDeathPosition");

		// Token: 0x04000008 RID: 8
		public readonly RandomDestinationPicker _randomDestinationPicker;

		// Token: 0x04000009 RID: 9
		public Mortal _mortal;

		// Token: 0x0400000A RID: 10
		public WalkToPositionExecutor _walkToPositionExecutor;

		// Token: 0x0400000B RID: 11
		public Enterer _enterer;

		// Token: 0x0400000C RID: 12
		public Citizen _citizen;

		// Token: 0x0400000D RID: 13
		public bool _wentToDeathPosition;
	}
}
