using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.Persistence;
using Timberborn.WalkingSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Wandering
{
	// Token: 0x0200000F RID: 15
	public class WanderRootBehavior : RootBehavior, IAwakableComponent, IStartableComponent, IPersistentEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000045 RID: 69 RVA: 0x00002828 File Offset: 0x00000A28
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x00002860 File Offset: 0x00000A60
		public event EventHandler IdleStarted;

		// Token: 0x06000047 RID: 71 RVA: 0x00002895 File Offset: 0x00000A95
		public WanderRootBehavior(RandomDestinationPicker randomDestinationPicker, IRandomNumberGenerator randomNumberGenerator)
		{
			this._randomDestinationPicker = randomDestinationPicker;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000028AB File Offset: 0x00000AAB
		public void Awake()
		{
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028B9 File Offset: 0x00000AB9
		public void Start()
		{
			this._walkToPositionExecutor = base.GetComponent<WalkToPositionExecutor>();
			this._walkInsideExecutor = base.GetComponent<WalkInsideExecutor>();
			this._waitExecutor = base.GetComponent<WaitExecutor>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028E0 File Offset: 0x00000AE0
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._walked)
			{
				float hours = this._randomNumberGenerator.Range(0.1f, 0.3f);
				this._waitExecutor.LaunchForSpecifiedTime(hours);
				this._walked = false;
				EventHandler idleStarted = this.IdleStarted;
				if (idleStarted != null)
				{
					idleStarted(this, EventArgs.Empty);
				}
				return Decision.ReleaseWhenFinished(this._waitExecutor);
			}
			this._walked = true;
			return this.WalkToRandomDestination();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000294E File Offset: 0x00000B4E
		public void Save(IEntitySaver entitySaver)
		{
			if (this._walked)
			{
				entitySaver.GetComponent(WanderRootBehavior.WanderRootBehaviorKey).Set(WanderRootBehavior.WalkedKey, this._walked);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002974 File Offset: 0x00000B74
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(WanderRootBehavior.WanderRootBehaviorKey, out objectLoader))
			{
				this._walked = objectLoader.Get(WanderRootBehavior.WalkedKey);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029A1 File Offset: 0x00000BA1
		public void AllowVisitingRestPlaces()
		{
			this._restPlacesAllowed = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029AC File Offset: 0x00000BAC
		public Decision WalkToRandomDestination()
		{
			if (this.ShouldWalkToRandomRestPlace())
			{
				RestPlace restPlace = this.RandomRestPlaceInDistrict();
				if (restPlace != null)
				{
					return this.WalkToRestPlace(restPlace);
				}
			}
			return this.WanderAround();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029DC File Offset: 0x00000BDC
		public RestPlace RandomRestPlaceInDistrict()
		{
			IEnumerable<RestPlace> enabledBuildings = this._citizen.AssignedDistrict.DistrictBuildingRegistry.GetEnabledBuildings<RestPlace>();
			RestPlace result;
			if (!this._randomNumberGenerator.TryGetEnumerableElement<RestPlace>(enabledBuildings, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A12 File Offset: 0x00000C12
		public bool ShouldWalkToRandomRestPlace()
		{
			return this._restPlacesAllowed && this._randomNumberGenerator.Range(0f, 1f) < WanderRootBehavior.GoToRestPlaceChance;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A3C File Offset: 0x00000C3C
		public Decision WalkToRestPlace(RestPlace restPlace)
		{
			switch (this._walkInsideExecutor.Launch(restPlace.GetComponent<Enterable>()))
			{
			case ExecutorStatus.Success:
				return Decision.ReleaseNow();
			case ExecutorStatus.Failure:
				return Decision.ReleaseNow();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkInsideExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A8C File Offset: 0x00000C8C
		public Decision WanderAround()
		{
			Vector3 position;
			if (!this._randomDestinationPicker.TryGetSafeRandomDestination(this._citizen, out position))
			{
				return Decision.ReleaseNow();
			}
			switch (this._walkToPositionExecutor.Launch(position))
			{
			case ExecutorStatus.Success:
				return Decision.ReleaseNow();
			case ExecutorStatus.Failure:
				return Decision.ReleaseNow();
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(this._walkToPositionExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0400001A RID: 26
		public static readonly ComponentKey WanderRootBehaviorKey = new ComponentKey("WanderRootBehavior");

		// Token: 0x0400001B RID: 27
		public static readonly PropertyKey<bool> WalkedKey = new PropertyKey<bool>("Walked");

		// Token: 0x0400001C RID: 28
		public static readonly float GoToRestPlaceChance = 0.2f;

		// Token: 0x0400001E RID: 30
		public readonly RandomDestinationPicker _randomDestinationPicker;

		// Token: 0x0400001F RID: 31
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000020 RID: 32
		public Citizen _citizen;

		// Token: 0x04000021 RID: 33
		public WalkToPositionExecutor _walkToPositionExecutor;

		// Token: 0x04000022 RID: 34
		public WalkInsideExecutor _walkInsideExecutor;

		// Token: 0x04000023 RID: 35
		public WaitExecutor _waitExecutor;

		// Token: 0x04000024 RID: 36
		public bool _walked;

		// Token: 0x04000025 RID: 37
		public bool _restPlacesAllowed;
	}
}
