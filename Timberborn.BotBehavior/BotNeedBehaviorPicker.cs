using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.GameDistricts;
using Timberborn.NeedBehaviorSystem;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.BotBehavior
{
	// Token: 0x02000006 RID: 6
	public class BotNeedBehaviorPicker : BaseComponent, IAwakableComponent, IPersistentEntity, INeedBehaviorPicker
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000224A File Offset: 0x0000044A
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._citizen = base.GetComponent<Citizen>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002264 File Offset: 0x00000464
		public Behavior GetBestNeedBehaviorAffectingNeedsInCriticalState()
		{
			if (!this._needManager.AnyNeedIsInCriticalState())
			{
				return null;
			}
			return this.GetBestNeedBehavior(NeedFilter.NeedIsInCriticalState(this._needManager));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002286 File Offset: 0x00000486
		public Behavior GetBestNeedBehavior()
		{
			if (!this._needManager.AnyNeedIsInCriticalState())
			{
				return this.GetBestNeedBehavior(NeedFilter.NeedIsBelowWarningThreshold(this._needManager));
			}
			return this.GetBestNeedBehavior(NeedFilter.NeedIsInCriticalState(this._needManager));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022B8 File Offset: 0x000004B8
		public bool NeedIsBeingCriticallySatisfied(string needId)
		{
			return this._needsBeingCriticallySatisfied.Contains(needId);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C6 File Offset: 0x000004C6
		public void Save(IEntitySaver entitySaver)
		{
			if (this._needsBeingCriticallySatisfied.Count > 0)
			{
				entitySaver.GetComponent(BotNeedBehaviorPicker.BotNeedBehaviorPickerKey).Set(BotNeedBehaviorPicker.NeedsBeingCriticallySatisfiedKey, this._needsBeingCriticallySatisfied);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F4 File Offset: 0x000004F4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BotNeedBehaviorPicker.BotNeedBehaviorPickerKey, out objectLoader))
			{
				this._needsBeingCriticallySatisfied.UnionWith(objectLoader.Get(BotNeedBehaviorPicker.NeedsBeingCriticallySatisfiedKey));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002328 File Offset: 0x00000528
		public Behavior GetBestNeedBehavior(NeedFilter needFilter)
		{
			AppraisedAction bestNonEssentialAction = this.GetBestNonEssentialAction(base.Transform.position, needFilter);
			this._needsBeingCriticallySatisfied.Clear();
			Behavior needBehavior = bestNonEssentialAction.NeedBehavior;
			if (needBehavior != null)
			{
				this.AddCriticallySatisfiedNeeds(bestNonEssentialAction.AffectedNeeds);
				return needBehavior;
			}
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002370 File Offset: 0x00000570
		public AppraisedAction GetBestNonEssentialAction(Vector3 essentialActionPosition, NeedFilter needFilter)
		{
			if (this._citizen.HasAssignedDistrict)
			{
				AppraisedAction? appraisedAction = this._citizen.AssignedDistrict.GetComponent<DistrictNeedBehaviorService>().PickBestAction(this._needManager, essentialActionPosition, float.MaxValue, needFilter);
				if (appraisedAction != null)
				{
					return appraisedAction.GetValueOrDefault();
				}
			}
			return default(AppraisedAction);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023CC File Offset: 0x000005CC
		public void AddCriticallySatisfiedNeeds(ImmutableArray<string> needIds)
		{
			for (int i = 0; i < needIds.Length; i++)
			{
				this.AddCriticallySatisfiedNeed(needIds[i]);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023F9 File Offset: 0x000005F9
		public void AddCriticallySatisfiedNeed(string needId)
		{
			if (this._needManager.NeedIsBelowWarningThreshold(needId))
			{
				this._needsBeingCriticallySatisfied.Add(needId);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly ComponentKey BotNeedBehaviorPickerKey = new ComponentKey("BotNeedBehaviorPicker");

		// Token: 0x04000007 RID: 7
		public static readonly ListKey<string> NeedsBeingCriticallySatisfiedKey = new ListKey<string>("NeedsBeingCriticallySatisfied");

		// Token: 0x04000008 RID: 8
		public NeedManager _needManager;

		// Token: 0x04000009 RID: 9
		public Citizen _citizen;

		// Token: 0x0400000A RID: 10
		public readonly HashSet<string> _needsBeingCriticallySatisfied = new HashSet<string>();
	}
}
