using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.DwellingSystem;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.NeedBehaviorSystem;
using Timberborn.NeedSpecs;
using Timberborn.Persistence;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.SleepSystem
{
	// Token: 0x0200000B RID: 11
	public class SleepNeedBehavior : EssentialNeedBehavior, IAwakableComponent, IStartableComponent, IPersistentEntity
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000024C5 File Offset: 0x000006C5
		public SleepNeedBehavior(RandomDestinationPicker randomDestinationPicker, INavigationService navigationService)
		{
			this._randomDestinationPicker = randomDestinationPicker;
			this._navigationService = navigationService;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024DB File Offset: 0x000006DB
		public void Awake()
		{
			this._dweller = base.GetComponent<Dweller>();
			this._sleeper = base.GetComponent<Sleeper>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002501 File Offset: 0x00000701
		public void Start()
		{
			this._walkToPositionExecutor = base.GetComponent<WalkToPositionExecutor>();
			this._walkInsideExecutor = base.GetComponent<WalkInsideExecutor>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000251C File Offset: 0x0000071C
		public override EssentialAction GetEssentialAction()
		{
			if (this.ShouldSleepAtHome())
			{
				Vector3? homeAccess = this._dweller.HomeAccess;
				if (homeAccess != null)
				{
					Vector3 valueOrDefault = homeAccess.GetValueOrDefault();
					return this.SleepAtHomeAction(valueOrDefault);
				}
			}
			return this.SleepOutsideAction();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000255E File Offset: 0x0000075E
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this.ShouldSleepAtHome())
			{
				return this.SleepOutside();
			}
			return this.SleepAtHome();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002575 File Offset: 0x00000775
		public void Save(IEntitySaver entitySaver)
		{
			if (this._walkedToSleepingPosition)
			{
				entitySaver.GetComponent(SleepNeedBehavior.SleepNeedBehaviorKey).Set(SleepNeedBehavior.WalkedToSleepingPositionKey, this._walkedToSleepingPosition);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000259C File Offset: 0x0000079C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(SleepNeedBehavior.SleepNeedBehaviorKey, out objectLoader))
			{
				this._walkedToSleepingPosition = objectLoader.Get(SleepNeedBehavior.WalkedToSleepingPositionKey);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025C9 File Offset: 0x000007C9
		public bool ShouldSleepAtHome()
		{
			return this._dweller.HasHome && (!this._sleeper.ShouldSleepCritically() || this.IsCloseToHome());
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025F0 File Offset: 0x000007F0
		public bool IsCloseToHome()
		{
			Vector3? homeAccess = this._dweller.HomeAccess;
			if (homeAccess != null)
			{
				Vector3 valueOrDefault = homeAccess.GetValueOrDefault();
				return this._navigationService.HeuristicDistance(base.Transform.position, valueOrDefault) < SleepNeedBehavior.MaxDistanceToHomeWhenSleepingCritically;
			}
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000263C File Offset: 0x0000083C
		public EssentialAction SleepAtHomeAction(Vector3 homeAccess)
		{
			IReadOnlyCollection<ContinuousEffectSpec> sleepEffects = this._dweller.Home.SleepEffects;
			return this.SleepAction(homeAccess, sleepEffects);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002664 File Offset: 0x00000864
		public EssentialAction SleepOutsideAction()
		{
			ImmutableArray<ContinuousEffectSpec> sleepOutsideEffects = this._sleeper.SleepOutsideEffects;
			return this.SleepAction(base.Transform.position, sleepOutsideEffects);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002694 File Offset: 0x00000894
		public EssentialAction SleepAction(Vector3 position, IEnumerable<ContinuousEffectSpec> effectSpecs)
		{
			ContinuousEffect effect2 = ContinuousEffect.FromSpec(effectSpecs.Single((ContinuousEffectSpec effect) => effect.NeedId == Sleeper.SleepNeedId));
			float minDurationInHours = this._sleeper.IsNewborn ? 24f : 0f;
			return new EssentialAction(position, effect2, minDurationInHours);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026F0 File Offset: 0x000008F0
		public Decision SleepAtHome()
		{
			Dwelling home = this._dweller.Home;
			switch (this._walkInsideExecutor.Launch(home.GetComponent<Enterable>()))
			{
			case ExecutorStatus.Success:
				return this.Sleep(home.SleepEffects);
			case ExecutorStatus.Failure:
				return this.UnassignHome();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkInsideExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002754 File Offset: 0x00000954
		public Decision UnassignHome()
		{
			this._dweller.UnassignFromHome();
			return Decision.ReleaseNextTick();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002766 File Offset: 0x00000966
		public Decision SleepOutside()
		{
			if (!this._walkedToSleepingPosition)
			{
				return this.WalkToRandomSleepingPosition();
			}
			return this.Sleep(this._sleeper.SleepOutsideEffects);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002790 File Offset: 0x00000990
		public Decision WalkToRandomSleepingPosition()
		{
			this._walkedToSleepingPosition = true;
			Vector3 position;
			if (!this._randomDestinationPicker.TryGetSafeRandomDestination(this._citizen, out position))
			{
				return Decision.ReturnNextTick();
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

		// Token: 0x06000036 RID: 54 RVA: 0x000027ED File Offset: 0x000009ED
		public Decision Sleep(IEnumerable<ContinuousEffectSpec> sleepEffectsSpecs)
		{
			this._walkedToSleepingPosition = false;
			return Decision.ReleaseWhenFinished(this._sleeper.LaunchExecutor(sleepEffectsSpecs));
		}

		// Token: 0x04000015 RID: 21
		public static readonly ComponentKey SleepNeedBehaviorKey = new ComponentKey("SleepNeedBehavior");

		// Token: 0x04000016 RID: 22
		public static readonly PropertyKey<bool> WalkedToSleepingPositionKey = new PropertyKey<bool>("WalkedToSleepingPosition");

		// Token: 0x04000017 RID: 23
		public static readonly float MaxDistanceToHomeWhenSleepingCritically = 13f;

		// Token: 0x04000018 RID: 24
		public readonly RandomDestinationPicker _randomDestinationPicker;

		// Token: 0x04000019 RID: 25
		public readonly INavigationService _navigationService;

		// Token: 0x0400001A RID: 26
		public Dweller _dweller;

		// Token: 0x0400001B RID: 27
		public Sleeper _sleeper;

		// Token: 0x0400001C RID: 28
		public Citizen _citizen;

		// Token: 0x0400001D RID: 29
		public WalkToPositionExecutor _walkToPositionExecutor;

		// Token: 0x0400001E RID: 30
		public WalkInsideExecutor _walkInsideExecutor;

		// Token: 0x0400001F RID: 31
		public ImmutableArray<ContinuousEffect> _actionEffects;

		// Token: 0x04000020 RID: 32
		public bool _walkedToSleepingPosition;
	}
}
