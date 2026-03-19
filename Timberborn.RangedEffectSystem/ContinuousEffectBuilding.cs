using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000008 RID: 8
	public class ContinuousEffectBuilding : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public ImmutableArray<ContinuousEffectSpec> Effects
		{
			get
			{
				return this._continuousEffectBuildingSpec.Effects;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000211E File Offset: 0x0000031E
		public void Awake()
		{
			this._continuousEffectBuildingSpec = base.GetComponent<ContinuousEffectBuildingSpec>();
			this._rangedEffectBuilding = base.GetComponent<RangedEffectBuilding>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002138 File Offset: 0x00000338
		public void OnEnterFinishedState()
		{
			foreach (ContinuousEffectSpec additionalEffect in this.Effects)
			{
				this._rangedEffectBuilding.AddEffect(additionalEffect);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002174 File Offset: 0x00000374
		public void OnExitFinishedState()
		{
			foreach (ContinuousEffectSpec additionalEffect in this.Effects)
			{
				this._rangedEffectBuilding.RemoveEffect(additionalEffect);
			}
		}

		// Token: 0x04000009 RID: 9
		public ContinuousEffectBuildingSpec _continuousEffectBuildingSpec;

		// Token: 0x0400000A RID: 10
		public RangedEffectBuilding _rangedEffectBuilding;
	}
}
