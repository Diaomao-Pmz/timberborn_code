using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.BehaviorSystem;
using Timberborn.Common;
using Timberborn.Effects;
using Timberborn.NeedBehaviorSystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.TimeSystem;

namespace Timberborn.SleepSystem
{
	// Token: 0x02000007 RID: 7
	public class Sleeper : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public Sleeper(IDayNightCycle dayNightCycle, IRandomNumberGenerator randomNumberGenerator)
		{
			this._dayNightCycle = dayNightCycle;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public ImmutableArray<ContinuousEffectSpec> SleepOutsideEffects
		{
			get
			{
				return this._sleeperSpec.SleepOutsideEffects;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002121 File Offset: 0x00000321
		public bool IsNewborn
		{
			get
			{
				return this._child && this._child.IsNewborn;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213D File Offset: 0x0000033D
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._child = base.GetComponent<Child>();
			this._sleeperSpec = base.GetComponent<SleeperSpec>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002163 File Offset: 0x00000363
		public void Start()
		{
			this._applyEffectExecutor = base.GetComponent<ApplyEffectExecutor>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002171 File Offset: 0x00000371
		public bool ShouldSleepCritically()
		{
			return this._needManager.NeedIsAtMinimumPoints(Sleeper.SleepNeedId);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002184 File Offset: 0x00000384
		public IExecutor LaunchExecutor(IEnumerable<ContinuousEffectSpec> sleepEffectsSpecs)
		{
			List<ContinuousEffect> list = this.ToSleepEffects(sleepEffectsSpecs).ToList<ContinuousEffect>();
			float timestamp = this.CalculateWakeUpTimestamp(list);
			this._applyEffectExecutor.LaunchToTimestamp(list, timestamp, Sleeper.SleepAnimationName);
			return this._applyEffectExecutor;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C0 File Offset: 0x000003C0
		public IEnumerable<ContinuousEffect> ToSleepEffects(IEnumerable<ContinuousEffectSpec> effectSpecs)
		{
			float scale = this.ShouldSleepCritically() ? 0.66f : 1f;
			return from effectSpec in effectSpecs
			select new ContinuousEffect(effectSpec.NeedId, effectSpec.PointsPerHour * scale);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002200 File Offset: 0x00000400
		public float CalculateWakeUpTimestamp(IReadOnlyList<ContinuousEffect> sleepEffects)
		{
			float num = this._dayNightCycle.HoursToNextStartOf(TimeOfDay.Daytime);
			ContinuousEffect effect2 = sleepEffects.Single((ContinuousEffect effect) => effect.NeedId == Sleeper.SleepNeedId);
			float num2 = this._needManager.FullyEffectiveDurationInHours(effect2);
			float num3 = this._randomNumberGenerator.Range(0f, this._sleeperSpec.MaxOffsetInHours);
			if (Math.Abs(num - num2) >= 1f && !this.IsNewborn)
			{
				return this._dayNightCycle.DayNumberHoursFromNow(num2 + num3);
			}
			return this._dayNightCycle.DayNumberHoursFromNow(num + num3);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string SleepNeedId = "Sleep";

		// Token: 0x04000009 RID: 9
		public static readonly string SleepAnimationName = "Sleeping";

		// Token: 0x0400000A RID: 10
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000B RID: 11
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400000C RID: 12
		public ApplyEffectExecutor _applyEffectExecutor;

		// Token: 0x0400000D RID: 13
		public NeedManager _needManager;

		// Token: 0x0400000E RID: 14
		public Child _child;

		// Token: 0x0400000F RID: 15
		public SleeperSpec _sleeperSpec;
	}
}
