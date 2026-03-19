using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.GameDistricts;
using Timberborn.NeedBehaviorSystem;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.BeaverBehavior
{
	// Token: 0x02000006 RID: 6
	public class BeaverNeedBehaviorPicker : BaseComponent, IAwakableComponent, IPersistentEntity, INeedBehaviorPicker
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000022C3 File Offset: 0x000004C3
		public BeaverNeedBehaviorPicker(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022DD File Offset: 0x000004DD
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._appraiser = base.GetComponent<Appraiser>();
			this._citizen = base.GetComponent<Citizen>();
			this._actionDurationCalculator = base.GetComponent<ActionDurationCalculator>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000230F File Offset: 0x0000050F
		public void InitializeEssentialNeedBehavior(EssentialNeedBehavior essentialNeedBehavior)
		{
			if (this._essentialBehavior == null)
			{
				this._essentialBehavior = essentialNeedBehavior;
				return;
			}
			throw new InvalidOperationException("There can be only one essential behavior " + string.Format("and it's already {0}", this._essentialBehavior));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002340 File Offset: 0x00000540
		public Behavior GetBestNeedBehaviorAffectingNeedsInCriticalState()
		{
			if (!this._needManager.AnyNeedIsInCriticalState())
			{
				return null;
			}
			return this.GetBestNeedBehavior(NeedFilter.NeedIsInCriticalState(this._needManager));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002362 File Offset: 0x00000562
		public Behavior GetBestNeedBehavior()
		{
			if (!this._needManager.AnyNeedIsInCriticalState())
			{
				return this.GetBestNeedBehavior(NeedFilter.AnyNeed());
			}
			return this.GetBestNeedBehavior(NeedFilter.NeedIsCritical(this._needManager));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000238E File Offset: 0x0000058E
		public bool NeedIsBeingCriticallySatisfied(string needId)
		{
			return this._needsBeingCriticallySatisfied.Contains(needId);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000239C File Offset: 0x0000059C
		public void Save(IEntitySaver entitySaver)
		{
			if (this._needsBeingCriticallySatisfied.Count > 0)
			{
				entitySaver.GetComponent(BeaverNeedBehaviorPicker.NeedBehaviorPickerKey).Set(BeaverNeedBehaviorPicker.NeedsBeingCriticallySatisfiedKey, this._needsBeingCriticallySatisfied);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023C8 File Offset: 0x000005C8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BeaverNeedBehaviorPicker.NeedBehaviorPickerKey, out objectLoader))
			{
				this._needsBeingCriticallySatisfied.UnionWith(objectLoader.Get(BeaverNeedBehaviorPicker.NeedsBeingCriticallySatisfiedKey));
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023FC File Offset: 0x000005FC
		public Behavior GetBestNeedBehavior(NeedFilter needFilter)
		{
			float num = this._dayNightCycle.HoursToNextStartOf(TimeOfDay.Daytime);
			EssentialAction essentialAction = this._essentialBehavior.GetEssentialAction();
			float num2 = this._actionDurationCalculator.FullyEffectiveDurationInHours(essentialAction);
			float hoursLeftForNonEssentialActions = num - num2;
			AppraisedAction bestNonEssentialAction = this.GetBestNonEssentialAction(essentialAction.Position, hoursLeftForNonEssentialActions, needFilter);
			this._needsBeingCriticallySatisfied.Clear();
			if (this.ShouldPickEssentialAction(essentialAction, num2, bestNonEssentialAction, num))
			{
				this.AddCriticallySatisfiedNeed(essentialAction.Effect.NeedId);
				return this._essentialBehavior;
			}
			Behavior needBehavior = bestNonEssentialAction.NeedBehavior;
			if (needBehavior != null)
			{
				this.AddCriticallySatisfiedNeeds(bestNonEssentialAction.AffectedNeeds);
				return needBehavior;
			}
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002498 File Offset: 0x00000698
		public bool ShouldPickEssentialAction(EssentialAction essentialAction, float fullDurationOfEssentialActionInHours, AppraisedAction nonEssentialAction, float hoursToDawn)
		{
			float essentialActionPoints = this._appraiser.AppraiseEffect(essentialAction);
			return BeaverNeedBehaviorPicker.ItIsTimeForEssentialAction(essentialActionPoints, fullDurationOfEssentialActionInHours, nonEssentialAction, hoursToDawn) || this.EssentialActionIsAtMinimumPoints(essentialActionPoints, essentialAction.Effect.NeedId, nonEssentialAction);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024D8 File Offset: 0x000006D8
		public static bool ItIsTimeForEssentialAction(float essentialActionPoints, float fullDurationOfEssentialActionInHours, AppraisedAction nonEssentialAction, float hoursToDawn)
		{
			return nonEssentialAction.NeedBehavior == null && essentialActionPoints > 0f && hoursToDawn < fullDurationOfEssentialActionInHours * 1.2f;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024F7 File Offset: 0x000006F7
		public bool EssentialActionIsAtMinimumPoints(float essentialActionPoints, string essentialActionNeedId, AppraisedAction nonEssentialAction)
		{
			return essentialActionPoints > nonEssentialAction.Points && this._needManager.NeedIsAtMinimumPoints(essentialActionNeedId);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002514 File Offset: 0x00000714
		public AppraisedAction GetBestNonEssentialAction(Vector3 essentialActionPosition, float hoursLeftForNonEssentialActions, NeedFilter needFilter)
		{
			if (this._citizen.HasAssignedDistrict)
			{
				AppraisedAction? appraisedAction = this._citizen.AssignedDistrict.GetComponent<DistrictNeedBehaviorService>().PickBestAction(this._needManager, essentialActionPosition, hoursLeftForNonEssentialActions, needFilter);
				if (appraisedAction != null)
				{
					return appraisedAction.GetValueOrDefault();
				}
			}
			return default(AppraisedAction);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000256C File Offset: 0x0000076C
		public void AddCriticallySatisfiedNeeds(ImmutableArray<string> needIds)
		{
			for (int i = 0; i < needIds.Length; i++)
			{
				this.AddCriticallySatisfiedNeed(needIds[i]);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002599 File Offset: 0x00000799
		public void AddCriticallySatisfiedNeed(string needId)
		{
			if (this._needManager.NeedIsInCriticalState(needId))
			{
				this._needsBeingCriticallySatisfied.Add(needId);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly ComponentKey NeedBehaviorPickerKey = new ComponentKey("BeaverNeedBehaviorPicker");

		// Token: 0x04000007 RID: 7
		public static readonly ListKey<string> NeedsBeingCriticallySatisfiedKey = new ListKey<string>("NeedsBeingCriticallySatisfied");

		// Token: 0x04000008 RID: 8
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000009 RID: 9
		public NeedManager _needManager;

		// Token: 0x0400000A RID: 10
		public Appraiser _appraiser;

		// Token: 0x0400000B RID: 11
		public Citizen _citizen;

		// Token: 0x0400000C RID: 12
		public ActionDurationCalculator _actionDurationCalculator;

		// Token: 0x0400000D RID: 13
		public readonly HashSet<string> _needsBeingCriticallySatisfied = new HashSet<string>();

		// Token: 0x0400000E RID: 14
		public EssentialNeedBehavior _essentialBehavior;
	}
}
