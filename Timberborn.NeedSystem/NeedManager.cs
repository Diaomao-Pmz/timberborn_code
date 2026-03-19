using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.Characters;
using Timberborn.Effects;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000009 RID: 9
	public class NeedManager : TickableComponent, IAwakableComponent, IPersistentEntity, IChildhoodInfluenced, IRegisteredComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600002D RID: 45 RVA: 0x00002500 File Offset: 0x00000700
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x00002538 File Offset: 0x00000738
		public event EventHandler<NeedChangedCriticalStateEventArgs> NeedChangedCriticalState;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002F RID: 47 RVA: 0x00002570 File Offset: 0x00000770
		// (remove) Token: 0x06000030 RID: 48 RVA: 0x000025A8 File Offset: 0x000007A8
		public event EventHandler<NeedChangedIsAtMinimumStateEventArgs> NeedChangedIsAtMinimumState;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000031 RID: 49 RVA: 0x000025E0 File Offset: 0x000007E0
		// (remove) Token: 0x06000032 RID: 50 RVA: 0x00002618 File Offset: 0x00000818
		public event EventHandler<NeedChangedIsFavorableEventArgs> NeedChangedIsFavorable;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000033 RID: 51 RVA: 0x00002650 File Offset: 0x00000850
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x00002688 File Offset: 0x00000888
		public event EventHandler<NeedChangedActiveStateEventArgs> NeedChangedActiveState;

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026BD File Offset: 0x000008BD
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000026C5 File Offset: 0x000008C5
		public ImmutableArray<NeedSpec> NeedSpecs { get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x000026CE File Offset: 0x000008CE
		public NeedManager(FactionNeedService factionNeedService, IDayNightCycle dayNightCycle, SerializedNeedValueSerializer serializedNeedValueSerializer)
		{
			this._factionNeedService = factionNeedService;
			this._dayNightCycle = dayNightCycle;
			this._serializedNeedValueSerializer = serializedNeedValueSerializer;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026EB File Offset: 0x000008EB
		public void Awake()
		{
			this._character = base.GetComponent<Character>();
			this.InitializeNeeds();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026FF File Offset: 0x000008FF
		public override void Tick()
		{
			this.UpdateNeeds();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002708 File Offset: 0x00000908
		public void InfluenceByChildhood(Character child)
		{
			foreach (Need need in child.GetComponent<NeedManager>()._needs.AllNeeds)
			{
				Need need2;
				if (this._needs.TryGetNeed(need.NeedSpec.Id, out need2))
				{
					bool isAtMinimumPoints = need2.IsAtMinimumPoints;
					bool isFavorable = need2.IsFavorable;
					bool isInCriticalState = need2.IsInCriticalState;
					bool isActive = need2.IsActive;
					need2.SetPoints(need.Points);
					this.CheckNewState(need2, isAtMinimumPoints, isFavorable, isInCriticalState, isActive);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002796 File Offset: 0x00000996
		public bool NeedIsActive(string needId)
		{
			return this.GetNeed(needId).IsActive;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027A4 File Offset: 0x000009A4
		public bool NeedIsEnabled(string needId)
		{
			return this.GetNeed(needId).Enabled;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027B2 File Offset: 0x000009B2
		public float NeedPointsToMax(string needId)
		{
			return this.GetNeed(needId).PointsToMax;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027C0 File Offset: 0x000009C0
		public float GetNeedPoints(string needId)
		{
			return this.GetNeed(needId).Points;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027CE File Offset: 0x000009CE
		public NeedSpec GetNeedSpec(string needId)
		{
			return this._needs.GetNeedSpec(needId);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027DC File Offset: 0x000009DC
		public bool NeedIsInCriticalState(string needId)
		{
			return this.GetNeed(needId).IsInCriticalState;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027EA File Offset: 0x000009EA
		public bool NeedIsAtMinimumPoints(string needId)
		{
			return this.GetNeed(needId).IsAtMinimumPoints;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027F8 File Offset: 0x000009F8
		public bool NeedIsFavorable(string needId)
		{
			return this.GetNeed(needId).IsFavorable;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002806 File Offset: 0x00000A06
		public int GetNeedWellbeing(string needId)
		{
			return this.GetNeed(needId).Wellbeing;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002814 File Offset: 0x00000A14
		public bool NeedIsBelowWarningThreshold(string needId)
		{
			return this.GetNeed(needId).IsBelowWarningThreshold;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002822 File Offset: 0x00000A22
		public bool AnyNeedIsInCriticalState()
		{
			return this._needs.Any((Need need) => need.IsInCriticalState);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000284E File Offset: 0x00000A4E
		public bool NeedIsCritical(string needId)
		{
			return this.GetNeed(needId).IsCritical;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000285C File Offset: 0x00000A5C
		public bool HasNeed(string needId)
		{
			return this._needs.Has(needId);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000286A File Offset: 0x00000A6A
		public void EnableUpdate(string needId)
		{
			this.GetNeed(needId).EnableUpdate();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002878 File Offset: 0x00000A78
		public void DisableUpdate(string needId)
		{
			this.GetNeed(needId).DisableUpdate();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002886 File Offset: 0x00000A86
		public void EnableNeed(string needId)
		{
			this.GetNeed(needId).EnableNeed();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002894 File Offset: 0x00000A94
		public void DisableNeed(string needId)
		{
			this.GetNeed(needId).DisableNeed();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void ResetNeed(string needId)
		{
			Need need = this.GetNeed(needId);
			bool isAtMinimumPoints = need.IsAtMinimumPoints;
			bool isFavorable = need.IsFavorable;
			bool isInCriticalState = need.IsInCriticalState;
			bool isActive = need.IsActive;
			need.Reset();
			this.CheckNewState(need, isAtMinimumPoints, isFavorable, isInCriticalState, isActive);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028E8 File Offset: 0x00000AE8
		public bool TryAppraise(string needId, in Effect effect, out float points)
		{
			return this.GetNeed(needId).TryAppraise(effect, out points);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002900 File Offset: 0x00000B00
		public void ApplyEffect(in ContinuousEffect effect, float deltaTimeInHours)
		{
			InstantEffect instantEffect = NeedManager.ToInstantEffect(effect, deltaTimeInHours);
			this.ApplyEffect(instantEffect);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002920 File Offset: 0x00000B20
		public void ApplyEffect(in InstantEffect effect)
		{
			Need need;
			if (this._needs.TryGetNeed(effect.NeedId, out need))
			{
				bool isAtMinimumPoints = need.IsAtMinimumPoints;
				bool isFavorable = need.IsFavorable;
				bool isInCriticalState = need.IsInCriticalState;
				bool isActive = need.IsActive;
				Need need2 = need;
				Effect effect2 = Effect.From(effect);
				need2.Apply(effect2);
				this.CheckNewState(need, isAtMinimumPoints, isFavorable, isInCriticalState, isActive);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002980 File Offset: 0x00000B80
		public void ApplyEffects(IReadOnlyList<ContinuousEffect> effects, float deltaTimeInHours)
		{
			for (int i = 0; i < effects.Count; i++)
			{
				ContinuousEffect continuousEffect = effects[i];
				this.ApplyEffect(continuousEffect, deltaTimeInHours);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029AF File Offset: 0x00000BAF
		public float FullyEffectiveDurationInHours(ContinuousEffect effect)
		{
			return this.GetNeed(effect.NeedId).EffectiveDurationInHours(effect);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(NeedManager.NeedManagerKey);
			IEnumerable<SerializedNeed> serializedNeeds = this.GetSerializedNeeds();
			component.Set<SerializedNeed>(NeedManager.NeedsKey, serializedNeeds.ToList<SerializedNeed>(), this._serializedNeedValueSerializer);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029FC File Offset: 0x00000BFC
		public void Load(IEntityLoader entityLoader)
		{
			foreach (SerializedNeed serializedNeed in entityLoader.GetComponent(NeedManager.NeedManagerKey).Get<SerializedNeed>(NeedManager.NeedsKey, this._serializedNeedValueSerializer))
			{
				Need need;
				Need need2;
				if (this._needs.TryGetNeed(serializedNeed.Id, out need))
				{
					this.LoadNeed(need, serializedNeed.Points);
				}
				else if (this._needs.TryGetBackwardCompatibleNeed(serializedNeed.Id, out need2))
				{
					this.LoadNeed(need2, serializedNeed.Points);
				}
				else
				{
					Debug.Log("Beaver doesn't have a " + serializedNeed.Id + " found in save, ignoring it.");
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public IEnumerable<SerializedNeed> GetSerializedNeeds()
		{
			return from needSpec in this.NeedSpecs
			select this.GetNeed(needSpec.Id) into need
			where need.Points != 0f || need.NeedSpec.StartingValue != 0f
			select new SerializedNeed(need.NeedSpec.Id, need.Points);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B2C File Offset: 0x00000D2C
		public void InitializeNeeds()
		{
			this.NeedSpecs = this.GetNeeds().ToImmutableArray<NeedSpec>();
			float deltaTimeInHours = this._dayNightCycle.FixedDeltaTimeInHours;
			IEnumerable<Need> needs = from spec in this.NeedSpecs
			select new Need(spec, deltaTimeInHours);
			this._needs = new Needs(needs);
			this._nullNeed = new Need(this._factionNeedService.NullNeed, deltaTimeInHours);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public IEnumerable<NeedSpec> GetNeeds()
		{
			if (!base.HasComponent<BotSpec>())
			{
				return this._factionNeedService.GetBeaverNeeds();
			}
			return this._factionNeedService.GetBotNeeds();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public Need GetNeed(string id)
		{
			Need result;
			if (!this._needs.TryGetNeed(id, out result))
			{
				return this._nullNeed;
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void UpdateNeeds()
		{
			if (this._character.Alive)
			{
				ImmutableArray<Need> allNeeds = this._needs.AllNeeds;
				for (int i = 0; i < allNeeds.Length; i++)
				{
					this.UpdateNeed(allNeeds[i]);
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C34 File Offset: 0x00000E34
		public void UpdateNeed(Need need)
		{
			bool isAtMinimumPoints = need.IsAtMinimumPoints;
			bool isFavorable = need.IsFavorable;
			bool isInCriticalState = need.IsInCriticalState;
			bool isActive = need.IsActive;
			need.Update();
			this.CheckNewState(need, isAtMinimumPoints, isFavorable, isInCriticalState, isActive);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C70 File Offset: 0x00000E70
		public void CheckNewState(Need need, bool wasAtMinimumPoints, bool wasFavorable, bool wasInCriticalState, bool wasActive)
		{
			bool isAtMinimumPoints = need.IsAtMinimumPoints;
			if (wasAtMinimumPoints != isAtMinimumPoints)
			{
				EventHandler<NeedChangedIsAtMinimumStateEventArgs> needChangedIsAtMinimumState = this.NeedChangedIsAtMinimumState;
				if (needChangedIsAtMinimumState != null)
				{
					needChangedIsAtMinimumState(this, new NeedChangedIsAtMinimumStateEventArgs(need.NeedSpec, isAtMinimumPoints));
				}
			}
			bool isFavorable = need.IsFavorable;
			if (wasFavorable != isFavorable)
			{
				EventHandler<NeedChangedIsFavorableEventArgs> needChangedIsFavorable = this.NeedChangedIsFavorable;
				if (needChangedIsFavorable != null)
				{
					needChangedIsFavorable(this, new NeedChangedIsFavorableEventArgs(need.NeedSpec));
				}
			}
			if (need.IsCritical)
			{
				bool isInCriticalState = need.IsInCriticalState;
				if (wasInCriticalState != isInCriticalState)
				{
					EventHandler<NeedChangedCriticalStateEventArgs> needChangedCriticalState = this.NeedChangedCriticalState;
					if (needChangedCriticalState != null)
					{
						needChangedCriticalState(this, new NeedChangedCriticalStateEventArgs(need.NeedSpec, isInCriticalState));
					}
				}
			}
			bool isActive = need.IsActive;
			if (wasActive != isActive)
			{
				EventHandler<NeedChangedActiveStateEventArgs> needChangedActiveState = this.NeedChangedActiveState;
				if (needChangedActiveState == null)
				{
					return;
				}
				needChangedActiveState(this, new NeedChangedActiveStateEventArgs(need.NeedSpec, isActive));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void LoadNeed(Need need, float points)
		{
			need.SetPoints(points);
			NeedSpec needSpec = need.NeedSpec;
			EventHandler<NeedChangedIsAtMinimumStateEventArgs> needChangedIsAtMinimumState = this.NeedChangedIsAtMinimumState;
			if (needChangedIsAtMinimumState != null)
			{
				needChangedIsAtMinimumState(this, new NeedChangedIsAtMinimumStateEventArgs(needSpec, need.IsAtMinimumPoints));
			}
			if (need.IsCritical)
			{
				EventHandler<NeedChangedCriticalStateEventArgs> needChangedCriticalState = this.NeedChangedCriticalState;
				if (needChangedCriticalState != null)
				{
					needChangedCriticalState(this, new NeedChangedCriticalStateEventArgs(needSpec, need.IsInCriticalState));
				}
			}
			EventHandler<NeedChangedIsFavorableEventArgs> needChangedIsFavorable = this.NeedChangedIsFavorable;
			if (needChangedIsFavorable != null)
			{
				needChangedIsFavorable(this, new NeedChangedIsFavorableEventArgs(needSpec));
			}
			EventHandler<NeedChangedActiveStateEventArgs> needChangedActiveState = this.NeedChangedActiveState;
			if (needChangedActiveState == null)
			{
				return;
			}
			needChangedActiveState(this, new NeedChangedActiveStateEventArgs(needSpec, need.IsActive));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public static InstantEffect ToInstantEffect(in ContinuousEffect continuousEffect, float deltaTimeInHours)
		{
			float points = continuousEffect.PointsPerHour * deltaTimeInHours;
			return new InstantEffect(continuousEffect.NeedId, points, 1);
		}

		// Token: 0x04000016 RID: 22
		public static readonly ComponentKey NeedManagerKey = new ComponentKey("NeedManager");

		// Token: 0x04000017 RID: 23
		public static readonly ListKey<SerializedNeed> NeedsKey = new ListKey<SerializedNeed>("Needs");

		// Token: 0x0400001D RID: 29
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400001E RID: 30
		public readonly SerializedNeedValueSerializer _serializedNeedValueSerializer;

		// Token: 0x0400001F RID: 31
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x04000020 RID: 32
		public Character _character;

		// Token: 0x04000021 RID: 33
		public Needs _needs;

		// Token: 0x04000022 RID: 34
		public Need _nullNeed;
	}
}
