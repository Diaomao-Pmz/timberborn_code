using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Effects;
using Timberborn.EntityPanelSystem;
using Timberborn.RangedEffectSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000013 RID: 19
	public class WonderEffectBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002A52 File Offset: 0x00000C52
		public WonderEffectBuildingDescriber(EffectDescriber effectDescriber)
		{
			this._effectDescriber = effectDescriber;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A61 File Offset: 0x00000C61
		public void Awake()
		{
			this._wonderEffectController = base.GetComponent<WonderEffectController>();
			this._rangedEffectBuilding = base.GetComponent<RangedEffectBuilding>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A7B File Offset: 0x00000C7B
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._wonderEffectController.Effects.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this._effectDescriber.DescribeRangeEffects(this._wonderEffectController.Effects, stringBuilder, this._rangedEffectBuilding.EffectRadius);
				yield return EntityDescription.CreateTextSection(stringBuilder.ToStringWithoutNewLineEnd(), 1030);
			}
			yield break;
		}

		// Token: 0x04000029 RID: 41
		public readonly EffectDescriber _effectDescriber;

		// Token: 0x0400002A RID: 42
		public WonderEffectController _wonderEffectController;

		// Token: 0x0400002B RID: 43
		public RangedEffectBuilding _rangedEffectBuilding;
	}
}
