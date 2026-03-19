using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockingSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000008 RID: 8
	public class ApplyEffectExecutor : BaseComponent, IAwakableComponent, IExecutor
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002186 File Offset: 0x00000386
		public ApplyEffectExecutor(IDayNightCycle dayNightCycle, ContinuousEffectValueSerializer continuousEffectValueSerializer, ReferenceSerializer referenceSerializer)
		{
			this._dayNightCycle = dayNightCycle;
			this._continuousEffectValueSerializer = continuousEffectValueSerializer;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021AE File Offset: 0x000003AE
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._enterer = base.GetComponent<Enterer>();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021D4 File Offset: 0x000003D4
		public void LaunchToTimestamp(IEnumerable<ContinuousEffect> effects, float timestamp, string animationName = null)
		{
			this.Launch(effects, timestamp, animationName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E0 File Offset: 0x000003E0
		public ExecutorStatus Tick(float deltaTimeInHours)
		{
			if (this._wasInsideAtLaunch && (!this._enterer.IsInside || !this._enteredBlockableBuilding || !this._enteredBlockableBuilding.IsUnblocked))
			{
				this.TurnOffAnimation();
				return ExecutorStatus.Failure;
			}
			if (this._dayNightCycle.PartialDayNumber > this._finishTimestamp)
			{
				this.TurnOffAnimation();
				return ExecutorStatus.Success;
			}
			this._needManager.ApplyEffects(this._effects, deltaTimeInHours);
			return ExecutorStatus.Running;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002254 File Offset: 0x00000454
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(ApplyEffectExecutor.ApplyEffectExecutorKey);
			component.Set<ContinuousEffect>(ApplyEffectExecutor.EffectsKey, this._effects, this._continuousEffectValueSerializer);
			component.Set(ApplyEffectExecutor.FinishTimestampKey, this._finishTimestamp);
			if (this._animationName != null)
			{
				component.Set(ApplyEffectExecutor.AnimationNameKey, this._animationName);
			}
			component.Set(ApplyEffectExecutor.WasInsideAtLaunchKey, this._wasInsideAtLaunch);
			if (this._enteredBlockableBuilding)
			{
				component.Set<BlockableObject>(ApplyEffectExecutor.EnteredBlockableBuildingKey, this._enteredBlockableBuilding, this._referenceSerializer.Of<BlockableObject>());
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022E8 File Offset: 0x000004E8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(ApplyEffectExecutor.ApplyEffectExecutorKey);
			this.SetEffects(component.Get<ContinuousEffect>(ApplyEffectExecutor.EffectsKey, this._continuousEffectValueSerializer));
			this._finishTimestamp = component.Get(ApplyEffectExecutor.FinishTimestampKey);
			if (component.Has<string>(ApplyEffectExecutor.AnimationNameKey))
			{
				this.TurnOnAnimation(component.Get(ApplyEffectExecutor.AnimationNameKey));
			}
			this._wasInsideAtLaunch = component.Get(ApplyEffectExecutor.WasInsideAtLaunchKey);
			if (component.Has<BlockableObject>(ApplyEffectExecutor.EnteredBlockableBuildingKey))
			{
				component.GetObsoletable<BlockableObject>(ApplyEffectExecutor.EnteredBlockableBuildingKey, this._referenceSerializer.Of<BlockableObject>(), out this._enteredBlockableBuilding);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002384 File Offset: 0x00000584
		public void Launch(IEnumerable<ContinuousEffect> effects, float finishTimestamp, string animationName = null)
		{
			this._finishTimestamp = finishTimestamp;
			this.TurnOnAnimation(animationName);
			this.SetEffects(effects);
			this._wasInsideAtLaunch = this._enterer.IsInside;
			if (this._wasInsideAtLaunch)
			{
				this._enteredBlockableBuilding = this._enterer.CurrentBuilding.GetComponent<BlockableObject>();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D5 File Offset: 0x000005D5
		public void SetEffects(IEnumerable<ContinuousEffect> effects)
		{
			this._effects.Clear();
			this._effects.AddRange(effects);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023EE File Offset: 0x000005EE
		public void TurnOnAnimation(string animationName)
		{
			if (animationName != null)
			{
				this._characterAnimator.SetBool(animationName, true);
				this._animationName = animationName;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002407 File Offset: 0x00000607
		public void TurnOffAnimation()
		{
			if (this._animationName != null)
			{
				this._characterAnimator.SetBool(this._animationName, false);
				this._animationName = null;
			}
		}

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey ApplyEffectExecutorKey = new ComponentKey("ApplyEffectExecutor");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<float> FinishTimestampKey = new PropertyKey<float>("FinishTimestamp");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<string> AnimationNameKey = new PropertyKey<string>("AnimationName");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<bool> WasInsideAtLaunchKey = new PropertyKey<bool>("WasInsideAtLaunch");

		// Token: 0x0400000E RID: 14
		public static readonly PropertyKey<BlockableObject> EnteredBlockableBuildingKey = new PropertyKey<BlockableObject>("EnteredBlockableBuilding");

		// Token: 0x0400000F RID: 15
		public static readonly ListKey<ContinuousEffect> EffectsKey = new ListKey<ContinuousEffect>("Effects");

		// Token: 0x04000010 RID: 16
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000011 RID: 17
		public readonly ContinuousEffectValueSerializer _continuousEffectValueSerializer;

		// Token: 0x04000012 RID: 18
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000013 RID: 19
		public NeedManager _needManager;

		// Token: 0x04000014 RID: 20
		public Enterer _enterer;

		// Token: 0x04000015 RID: 21
		public CharacterAnimator _characterAnimator;

		// Token: 0x04000016 RID: 22
		public readonly List<ContinuousEffect> _effects = new List<ContinuousEffect>();

		// Token: 0x04000017 RID: 23
		public float _finishTimestamp;

		// Token: 0x04000018 RID: 24
		public string _animationName;

		// Token: 0x04000019 RID: 25
		public bool _wasInsideAtLaunch;

		// Token: 0x0400001A RID: 26
		public BlockableObject _enteredBlockableBuilding;
	}
}
