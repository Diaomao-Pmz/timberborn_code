using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Effects;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000007 RID: 7
	public class AreaNeedApplier : BaseComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity, IProbabilityGroupProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler<NeedAppliedEventArgs> NeedApplied;

		// Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		public AreaNeedApplier(EffectProbabilityService effectProbabilityService, ITimeTriggerFactory timeTriggerFactory, CharacterPopulation characterPopulation)
		{
			this._effectProbabilityService = effectProbabilityService;
			this._timeTriggerFactory = timeTriggerFactory;
			this._characterPopulation = characterPopulation;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000218A File Offset: 0x0000038A
		public string ProbabilityGroupId
		{
			get
			{
				return "AreaNeedApplier";
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002194 File Offset: 0x00000394
		public void Awake()
		{
			this._blockObjectRange = base.GetComponent<BlockObjectRange>();
			this._areaNeedApplierSpec = base.GetComponent<AreaNeedApplierSpec>();
			this._applicationTrigger = this._timeTriggerFactory.Create(new Action(this.TryApplyNeed), AreaNeedApplier.CheckIntervalInDays);
			base.DisableComponent();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E1 File Offset: 0x000003E1
		public void OnEnterFinishedState()
		{
			this.UpdateInfluencedBlocks();
			this._applicationTrigger.Resume();
			base.EnableComponent();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FA File Offset: 0x000003FA
		public void OnExitFinishedState()
		{
			this._applicationTrigger.Pause();
			base.DisableComponent();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000220D File Offset: 0x0000040D
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(AreaNeedApplier.AreaNeedApplierKey).Set(AreaNeedApplier.ApplicationTriggerProgressKey, this._applicationTrigger.Progress);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(AreaNeedApplier.AreaNeedApplierKey);
			this._applicationTrigger.FastForwardProgress(component.Get(AreaNeedApplier.ApplicationTriggerProgressKey));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002260 File Offset: 0x00000460
		public void TryApplyNeed()
		{
			foreach (NeedApplierEffectSpec spec in this._areaNeedApplierSpec.Effects)
			{
				for (int i = 0; i < this._characterPopulation.Characters.Count; i++)
				{
					if (this._effectProbabilityService.CanApply(this, spec))
					{
						Character character = this._characterPopulation.Characters[i];
						Vector3Int value = CoordinateSystem.WorldToGridInt(character.Transform.position);
						if (this.IsInfluencedByApplier(value.XY()))
						{
							this.ApplyNeed(character, spec);
						}
					}
				}
			}
			this._applicationTrigger.Reset();
			this._applicationTrigger.Resume();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000231D File Offset: 0x0000051D
		public void UpdateInfluencedBlocks()
		{
			this._influencedBlocks = this._blockObjectRange.GetBlocksInRectangularRadius(this._areaNeedApplierSpec.ApplicationRadius);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000233B File Offset: 0x0000053B
		public bool IsInfluencedByApplier(Vector2Int coordinates)
		{
			return this._influencedBlocks.Contains(coordinates);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000234C File Offset: 0x0000054C
		public void ApplyNeed(Character character, NeedApplierEffectSpec spec)
		{
			NeedManager component = character.GetComponent<NeedManager>();
			if (component && component.HasNeed(spec.NeedId))
			{
				InstantEffect needEffect = spec.ToInstantEffect();
				component.ApplyEffect(needEffect);
				EventHandler<NeedAppliedEventArgs> needApplied = this.NeedApplied;
				if (needApplied == null)
				{
					return;
				}
				needApplied(this, new NeedAppliedEventArgs(character, needEffect));
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly float CheckIntervalInDays = 0.041666668f;

		// Token: 0x04000009 RID: 9
		public static readonly ComponentKey AreaNeedApplierKey = new ComponentKey("AreaNeedApplier");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<float> ApplicationTriggerProgressKey = new PropertyKey<float>("ApplicationTriggerProgress");

		// Token: 0x0400000C RID: 12
		public readonly EffectProbabilityService _effectProbabilityService;

		// Token: 0x0400000D RID: 13
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400000E RID: 14
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x0400000F RID: 15
		public BlockObjectRange _blockObjectRange;

		// Token: 0x04000010 RID: 16
		public AreaNeedApplierSpec _areaNeedApplierSpec;

		// Token: 0x04000011 RID: 17
		public ITimeTrigger _applicationTrigger;

		// Token: 0x04000012 RID: 18
		public IEnumerable<Vector2Int> _influencedBlocks;
	}
}
