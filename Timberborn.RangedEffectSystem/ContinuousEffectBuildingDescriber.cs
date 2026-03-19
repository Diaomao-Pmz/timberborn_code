using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Effects;
using Timberborn.EntityPanelSystem;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000009 RID: 9
	public class ContinuousEffectBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021B7 File Offset: 0x000003B7
		public ContinuousEffectBuildingDescriber(EffectDescriber effectDescriber)
		{
			this._effectDescriber = effectDescriber;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021C6 File Offset: 0x000003C6
		public void Awake()
		{
			this._continuousEffectBuilding = base.GetComponent<ContinuousEffectBuilding>();
			this._rangedEffectBuilding = base.GetComponent<RangedEffectBuilding>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E0 File Offset: 0x000003E0
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._continuousEffectBuilding.Effects.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this._effectDescriber.DescribeRangeEffects(this._continuousEffectBuilding.Effects, stringBuilder, this._rangedEffectBuilding.EffectRadius);
				yield return EntityDescription.CreateTextSection(stringBuilder.ToStringWithoutNewLineEnd(), 1020);
			}
			yield break;
		}

		// Token: 0x0400000B RID: 11
		public readonly EffectDescriber _effectDescriber;

		// Token: 0x0400000C RID: 12
		public ContinuousEffectBuilding _continuousEffectBuilding;

		// Token: 0x0400000D RID: 13
		public RangedEffectBuilding _rangedEffectBuilding;
	}
}
