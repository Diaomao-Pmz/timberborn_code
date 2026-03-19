using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000013 RID: 19
	public class RangedEffectsAffectingEnterable : BaseComponent
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002D5E File Offset: 0x00000F5E
		public ReadOnlyList<RangedEffect> ActiveEffects
		{
			get
			{
				return this._rangedEffects.ActiveEffects;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D6B File Offset: 0x00000F6B
		public void Add(RangedEffectApplier rangedEffectApplier)
		{
			this._rangedEffects.Add(rangedEffectApplier);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D79 File Offset: 0x00000F79
		public void Remove(RangedEffectApplier rangedEffectApplier)
		{
			this._rangedEffects.Remove(rangedEffectApplier);
		}

		// Token: 0x0400002C RID: 44
		public readonly RangedEffects _rangedEffects = new RangedEffects();
	}
}
