using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.NeedSpecs;
using Timberborn.RangedEffectSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000015 RID: 21
	public class WonderEffectController : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B97 File Offset: 0x00000D97
		public ImmutableArray<ContinuousEffectSpec> Effects
		{
			get
			{
				return this._wonderEffectControllerSpec.Effects;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public void Awake()
		{
			this._rangedEffectBuilding = base.GetComponent<RangedEffectBuilding>();
			this._wonder = base.GetComponent<Wonder>();
			this._wonderEffectControllerSpec = base.GetComponent<WonderEffectControllerSpec>();
			this._wonder.WonderActivated += this.OnWonderActivated;
			this._wonder.WonderDeactivated += this.OnWonderDeactivated;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C04 File Offset: 0x00000E04
		public void EnableEffects()
		{
			foreach (ContinuousEffectSpec additionalEffect in this.Effects)
			{
				this._rangedEffectBuilding.AddEffect(additionalEffect);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002C40 File Offset: 0x00000E40
		public void DisableEffects()
		{
			foreach (ContinuousEffectSpec additionalEffect in this.Effects)
			{
				this._rangedEffectBuilding.RemoveEffect(additionalEffect);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C7B File Offset: 0x00000E7B
		public void OnWonderActivated(object sender, EventArgs e)
		{
			this.EnableEffects();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002C83 File Offset: 0x00000E83
		public void OnWonderDeactivated(object sender, EventArgs e)
		{
			this.DisableEffects();
		}

		// Token: 0x04000030 RID: 48
		public RangedEffectBuilding _rangedEffectBuilding;

		// Token: 0x04000031 RID: 49
		public Wonder _wonder;

		// Token: 0x04000032 RID: 50
		public WonderEffectControllerSpec _wonderEffectControllerSpec;
	}
}
