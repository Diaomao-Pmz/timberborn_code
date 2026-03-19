using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.CoreUI;
using Timberborn.GameFactionSystem;
using Timberborn.NeedSpecs;

namespace Timberborn.Effects
{
	// Token: 0x02000008 RID: 8
	public class EffectDescriber
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000022BA File Offset: 0x000004BA
		public EffectDescriber(FactionNeedService factionNeedService, NeedSpecFormatter needSpecFormatter)
		{
			this._factionNeedService = factionNeedService;
			this._needSpecFormatter = needSpecFormatter;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D0 File Offset: 0x000004D0
		public void DescribeEffects(IEnumerable<InstantEffectSpec> effects, StringBuilder description)
		{
			this.DescribeEffects(from effect in effects
			select effect.NeedId, description, 0);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022FF File Offset: 0x000004FF
		public void DescribeEffects(IEnumerable<ContinuousEffectSpec> effects, StringBuilder description)
		{
			this.DescribeEffects(from effect in effects
			select effect.NeedId, description, 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000232E File Offset: 0x0000052E
		public void DescribeRangeEffects(IEnumerable<ContinuousEffectSpec> effects, StringBuilder stringBuilder, int range)
		{
			this.DescribeEffects(from effect in effects
			select effect.NeedId, stringBuilder, range);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002360 File Offset: 0x00000560
		public void DescribeEffects(IEnumerable<string> needIds, StringBuilder description, int range = 0)
		{
			foreach (string id in needIds)
			{
				description.Append(SpecialStrings.RowStarter);
				NeedSpec beaverOrBotNeedById = this._factionNeedService.GetBeaverOrBotNeedById(id);
				description.Append((range > 0) ? this._needSpecFormatter.FormatRangedNeed(beaverOrBotNeedById, range) : this._needSpecFormatter.FormatNeed(beaverOrBotNeedById));
				description.AppendLine();
			}
		}

		// Token: 0x0400000F RID: 15
		public readonly FactionNeedService _factionNeedService;

		// Token: 0x04000010 RID: 16
		public readonly NeedSpecFormatter _needSpecFormatter;
	}
}
