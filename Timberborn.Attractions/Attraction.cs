using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Effects;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.Attractions
{
	// Token: 0x02000007 RID: 7
	public class Attraction : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public bool SatisfiesAnyNeedToMaxValue { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002117 File Offset: 0x00000317
		public IReadOnlyList<ContinuousEffectSpec> Effects { get; private set; }

		// Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		public Attraction(FactionNeedService factionNeedService)
		{
			this._factionNeedService = factionNeedService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000213A File Offset: 0x0000033A
		public bool IsUsable
		{
			get
			{
				if (this._blockableObject.IsUnblocked)
				{
					return this._efficiencyProviders.All((IBuildingEfficiencyProvider e) => e.CanUse);
				}
				return false;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002178 File Offset: 0x00000378
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			base.GetComponents<IBuildingEfficiencyProvider>(this._efficiencyProviders);
			this.Effects = (from spec in base.GetComponent<AttractionSpec>().Effects
			where this._factionNeedService.IsCurrentFactionNeed(spec.NeedId)
			select spec).ToList<ContinuousEffectSpec>();
			this.SatisfiesAnyNeedToMaxValue = this.Effects.Any((ContinuousEffectSpec spec) => spec.SatisfyToMaxValue);
			base.DisableComponent();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021FC File Offset: 0x000003FC
		public void GetEfficiencyAdjustedEffects(List<ContinuousEffect> continuousEffects)
		{
			float efficiency = this.GetEfficiency();
			for (int i = 0; i < this.Effects.Count; i++)
			{
				ContinuousEffectSpec continuousEffectSpec = this.Effects[i];
				float pointsPerHour = continuousEffectSpec.PointsPerHour * efficiency;
				continuousEffects.Add(new ContinuousEffect(continuousEffectSpec.NeedId, pointsPerHour));
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000224E File Offset: 0x0000044E
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002256 File Offset: 0x00000456
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002260 File Offset: 0x00000460
		public float GetEfficiency()
		{
			float num = 1f;
			for (int i = 0; i < this._efficiencyProviders.Count; i++)
			{
				IBuildingEfficiencyProvider buildingEfficiencyProvider = this._efficiencyProviders[i];
				num *= buildingEfficiencyProvider.Efficiency;
			}
			return num;
		}

		// Token: 0x0400000A RID: 10
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x0400000B RID: 11
		public readonly List<IBuildingEfficiencyProvider> _efficiencyProviders = new List<IBuildingEfficiencyProvider>();

		// Token: 0x0400000C RID: 12
		public BlockableObject _blockableObject;
	}
}
