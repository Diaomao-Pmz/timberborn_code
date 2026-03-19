using System;
using System.Text;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.GoodsUI;

namespace Timberborn.Effects
{
	// Token: 0x0200000B RID: 11
	public class GoodEffectDescriber
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002432 File Offset: 0x00000632
		public GoodEffectDescriber(EffectDescriber effectDescriber, IGoodService goodService, GoodDescriber goodDescriber)
		{
			this._effectDescriber = effectDescriber;
			this._goodService = goodService;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000245A File Offset: 0x0000065A
		public string DescribeEffectsWithHeader(string goodId)
		{
			return this.DescribeEffectsWithHeader(goodId, this._goodDescriber.Describe(goodId));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000246F File Offset: 0x0000066F
		public string DescribeEffects(string goodId)
		{
			this._description.Clear();
			this.DescribeEffects(goodId, this._description);
			return this._description.ToStringWithoutNewLineEnd();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002495 File Offset: 0x00000695
		public void DescribeEffects(string goodId, StringBuilder description)
		{
			this.DescribeEffects(this._goodService.GetGood(goodId), description);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024AA File Offset: 0x000006AA
		public string DescribeEffectsWithHeader(string goodId, string header)
		{
			this._description.Clear();
			this._description.AppendLine(header);
			this.DescribeEffects(goodId, this._description);
			return this._description.ToStringWithoutNewLineEnd();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024E0 File Offset: 0x000006E0
		public void DescribeEffects(GoodSpec goodSpec, StringBuilder description)
		{
			if (goodSpec.ConsumptionEffects.Length > 0)
			{
				this._effectDescriber.DescribeEffects(goodSpec.ConsumptionEffects, description);
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly EffectDescriber _effectDescriber;

		// Token: 0x04000016 RID: 22
		public readonly IGoodService _goodService;

		// Token: 0x04000017 RID: 23
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000018 RID: 24
		public readonly StringBuilder _description = new StringBuilder();
	}
}
