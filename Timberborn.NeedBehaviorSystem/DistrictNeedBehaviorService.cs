using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Effects;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using UnityEngine;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x02000013 RID: 19
	public class DistrictNeedBehaviorService : BaseComponent
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002C48 File Offset: 0x00000E48
		public DistrictNeedBehaviorService(NeedBehaviorKeyGenerator needBehaviorKeyGenerator)
		{
			this._needBehaviorKeyGenerator = needBehaviorKeyGenerator;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C6D File Offset: 0x00000E6D
		public void AddNeedBehavior(IReadOnlyList<InstantEffectSpec> effects, NeedBehavior needBehavior)
		{
			this.GetNeedBehaviors(effects).AddBehavior(needBehavior);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C7C File Offset: 0x00000E7C
		public void AddNeedBehavior(IReadOnlyList<ContinuousEffectSpec> effects, NeedBehavior needBehavior)
		{
			this.GetNeedBehaviors(effects).AddBehavior(needBehavior);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C8B File Offset: 0x00000E8B
		public void RemoveNeedBehavior(IReadOnlyList<InstantEffectSpec> effects, NeedBehavior needBehavior)
		{
			this.GetNeedBehaviors(effects).RemoveBehavior(needBehavior);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C9A File Offset: 0x00000E9A
		public void RemoveNeedBehavior(IReadOnlyList<ContinuousEffectSpec> effects, NeedBehavior needBehavior)
		{
			this.GetNeedBehaviors(effects).RemoveBehavior(needBehavior);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002CA9 File Offset: 0x00000EA9
		public AppraisedAction? PickBestAction(NeedManager needManager, Vector3 essentialActionPosition, float hoursLeftForNonEssentialActions, NeedFilter needFilter)
		{
			this.AppraiseNeedBehaviors(needManager, needFilter);
			AppraisedAction? result = this.PickShortestAction(needManager, essentialActionPosition, hoursLeftForNonEssentialActions, needFilter.OnlyCriticalStateNeeds);
			this._appraisedNeedBehaviors.Clear();
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public void AppraiseNeedBehaviors(NeedManager needManager, NeedFilter needFilter)
		{
			Appraiser component = needManager.GetComponent<Appraiser>();
			foreach (DistrictNeedBehaviorService.NeedBehaviorGroup needBehaviorGroup in this._needBehaviors.Values)
			{
				float num = component.AppraiseEffects(needBehaviorGroup.Effects, needFilter);
				if (num > 0f)
				{
					this._appraisedNeedBehaviors.Add(new DistrictNeedBehaviorService.AppraisedNeedBehaviorGroup(needBehaviorGroup, num));
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D54 File Offset: 0x00000F54
		public AppraisedAction? PickShortestAction(NeedManager needManager, Vector3 essentialActionPosition, float hoursLeftForNonEssentialActions, bool onlyNeedsInCriticalState)
		{
			ActionDurationCalculator component = needManager.GetComponent<ActionDurationCalculator>();
			foreach (DistrictNeedBehaviorService.AppraisedNeedBehaviorGroup appraisedNeedBehaviorGroup in this._appraisedNeedBehaviors)
			{
				AppraisedAction? appraisedAction = null;
				float num = float.MaxValue;
				float points = appraisedNeedBehaviorGroup.Points;
				DistrictNeedBehaviorService.NeedBehaviorGroup needBehaviorGroup = appraisedNeedBehaviorGroup.NeedBehaviorGroup;
				List<NeedBehavior> needBehaviors = needBehaviorGroup.NeedBehaviors;
				for (int i = 0; i < needBehaviors.Count; i++)
				{
					NeedBehavior needBehavior = needBehaviors[i];
					Vector3? vector = needBehavior.ActionPosition(needManager);
					if (vector != null)
					{
						Vector3 valueOrDefault = vector.GetValueOrDefault();
						float num2 = component.DurationWithReturnInHours(valueOrDefault, essentialActionPosition);
						if ((hoursLeftForNonEssentialActions > num2 || onlyNeedsInCriticalState) && num2 < num)
						{
							appraisedAction = new AppraisedAction?(new AppraisedAction(needBehavior, needBehaviorGroup.Needs, points));
							num = num2;
						}
					}
				}
				if (appraisedAction != null)
				{
					return new AppraisedAction?(appraisedAction.Value);
				}
			}
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E70 File Offset: 0x00001070
		public DistrictNeedBehaviorService.NeedBehaviorGroup GetNeedBehaviors(IReadOnlyList<InstantEffectSpec> effectSpecs)
		{
			string key = this._needBehaviorKeyGenerator.GenerateKey(effectSpecs);
			DistrictNeedBehaviorService.NeedBehaviorGroup needBehaviorGroup;
			if (!this._needBehaviors.TryGetValue(key, out needBehaviorGroup))
			{
				IEnumerable<string> needs = from effect in effectSpecs
				select effect.NeedId;
				IEnumerable<InstantEffect> effects = from effect in effectSpecs
				select InstantEffect.FromSpec(effect, 100);
				needBehaviorGroup = new DistrictNeedBehaviorService.NeedBehaviorGroup(needs, effects);
				this._needBehaviors[key] = needBehaviorGroup;
			}
			return needBehaviorGroup;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EFC File Offset: 0x000010FC
		public DistrictNeedBehaviorService.NeedBehaviorGroup GetNeedBehaviors(IReadOnlyList<ContinuousEffectSpec> effectSpecs)
		{
			string key = this._needBehaviorKeyGenerator.GenerateKey(effectSpecs);
			DistrictNeedBehaviorService.NeedBehaviorGroup needBehaviorGroup;
			if (!this._needBehaviors.TryGetValue(key, out needBehaviorGroup))
			{
				IEnumerable<string> needs = from effect in effectSpecs
				select effect.NeedId;
				IEnumerable<InstantEffect> effects = from effect in effectSpecs
				select InstantEffect.DiscretizeContinuousEffect(effect.NeedId);
				needBehaviorGroup = new DistrictNeedBehaviorService.NeedBehaviorGroup(needs, effects);
				this._needBehaviors[key] = needBehaviorGroup;
			}
			return needBehaviorGroup;
		}

		// Token: 0x04000033 RID: 51
		public readonly NeedBehaviorKeyGenerator _needBehaviorKeyGenerator;

		// Token: 0x04000034 RID: 52
		public readonly Dictionary<string, DistrictNeedBehaviorService.NeedBehaviorGroup> _needBehaviors = new Dictionary<string, DistrictNeedBehaviorService.NeedBehaviorGroup>();

		// Token: 0x04000035 RID: 53
		public readonly SortedSet<DistrictNeedBehaviorService.AppraisedNeedBehaviorGroup> _appraisedNeedBehaviors = new SortedSet<DistrictNeedBehaviorService.AppraisedNeedBehaviorGroup>();

		// Token: 0x02000014 RID: 20
		public class NeedBehaviorGroup
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F86 File Offset: 0x00001186
			public ImmutableArray<string> Needs { get; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F8E File Offset: 0x0000118E
			public ImmutableArray<InstantEffect> Effects { get; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F96 File Offset: 0x00001196
			public List<NeedBehavior> NeedBehaviors { get; } = new List<NeedBehavior>();

			// Token: 0x0600005A RID: 90 RVA: 0x00002F9E File Offset: 0x0000119E
			public NeedBehaviorGroup(IEnumerable<string> needs, IEnumerable<InstantEffect> effects)
			{
				this.Needs = needs.ToImmutableArray<string>();
				this.Effects = effects.ToImmutableArray<InstantEffect>();
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002FC9 File Offset: 0x000011C9
			public void AddBehavior(NeedBehavior needBehavior)
			{
				this.NeedBehaviors.Add(needBehavior);
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002FD7 File Offset: 0x000011D7
			public void RemoveBehavior(NeedBehavior needBehavior)
			{
				this.NeedBehaviors.Remove(needBehavior);
			}
		}

		// Token: 0x02000015 RID: 21
		public readonly struct AppraisedNeedBehaviorGroup : IComparable<DistrictNeedBehaviorService.AppraisedNeedBehaviorGroup>
		{
			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00002FE6 File Offset: 0x000011E6
			public DistrictNeedBehaviorService.NeedBehaviorGroup NeedBehaviorGroup { get; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00002FEE File Offset: 0x000011EE
			public float Points { get; }

			// Token: 0x0600005F RID: 95 RVA: 0x00002FF6 File Offset: 0x000011F6
			public AppraisedNeedBehaviorGroup(DistrictNeedBehaviorService.NeedBehaviorGroup needBehaviorGroup, float points)
			{
				this.NeedBehaviorGroup = needBehaviorGroup;
				this.Points = points;
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00003006 File Offset: 0x00001206
			public int CompareTo(DistrictNeedBehaviorService.AppraisedNeedBehaviorGroup other)
			{
				if (this.Points >= other.Points)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
