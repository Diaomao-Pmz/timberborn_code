using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TubeSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200002E RID: 46
	public class LargeTubewayNetworkAchievement : Achievement
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003CA6 File Offset: 0x00001EA6
		public LargeTubewayNetworkAchievement(EventBus eventBus, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003CBC File Offset: 0x00001EBC
		public override string Id
		{
			get
			{
				return "LARGE_TUBEWAY_NETWORK";
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003CC3 File Offset: 0x00001EC3
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (enteredFinishedStateEvent.BlockObject.HasComponent<TubeStationSpec>())
			{
				this._stationCount++;
			}
			if (enteredFinishedStateEvent.BlockObject.HasComponent<TubeSpec>())
			{
				this._tubewayCount++;
			}
			this.CheckUnlockCondition();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003D01 File Offset: 0x00001F01
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			if (exitedFinishedStateEvent.BlockObject.HasComponent<TubeStationSpec>())
			{
				this._stationCount--;
			}
			if (exitedFinishedStateEvent.BlockObject.HasComponent<TubeSpec>())
			{
				this._tubewayCount--;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003D39 File Offset: 0x00001F39
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
			this.ValidateInitialCount();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003D4D File Offset: 0x00001F4D
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003D5B File Offset: 0x00001F5B
		public void CheckUnlockCondition()
		{
			if (this._stationCount >= LargeTubewayNetworkAchievement.StationsRequired && this._tubewayCount >= LargeTubewayNetworkAchievement.TubewaysRequired)
			{
				base.Unlock();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003D80 File Offset: 0x00001F80
		public void ValidateInitialCount()
		{
			ImmutableArray<Building> immutableArray = this._entityComponentRegistry.GetEnabled<Building>().ToImmutableArray<Building>();
			this._stationCount = (from spec in immutableArray
			where spec.HasComponent<TubeStationSpec>()
			select spec).Count((Building spec) => spec.GetComponent<BlockObject>().IsFinished);
			this._tubewayCount = (from spec in immutableArray
			where spec.HasComponent<TubeSpec>()
			select spec).Count((Building spec) => spec.GetComponent<BlockObject>().IsFinished);
			this.CheckUnlockCondition();
		}

		// Token: 0x04000067 RID: 103
		public static readonly int StationsRequired = 10;

		// Token: 0x04000068 RID: 104
		public static readonly int TubewaysRequired = 1000;

		// Token: 0x04000069 RID: 105
		public readonly EventBus _eventBus;

		// Token: 0x0400006A RID: 106
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400006B RID: 107
		public int _stationCount;

		// Token: 0x0400006C RID: 108
		public int _tubewayCount;
	}
}
