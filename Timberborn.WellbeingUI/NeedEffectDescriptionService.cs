using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Timberborn.Common;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200000C RID: 12
	public class NeedEffectDescriptionService
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002810 File Offset: 0x00000A10
		public NeedEffectDescriptionService(IEnumerable<INeedEffectDescriber> needEffectDescribers)
		{
			this._needEffectDescribers = needEffectDescribers.ToImmutableArray<INeedEffectDescriber>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000282F File Offset: 0x00000A2F
		public string GetNeedDescription(NeedSpec needSpec, NeedManager needManager)
		{
			this._contentBuilder.Clear();
			this.DescribeEffects(this._contentBuilder, needSpec, needManager);
			return this._contentBuilder.ToStringWithoutNewLineEnd() ?? "";
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002860 File Offset: 0x00000A60
		public void DescribeEffects(StringBuilder content, NeedSpec needSpec, NeedManager needManager)
		{
			foreach (INeedEffectDescriber needEffectDescriber in this._needEffectDescribers)
			{
				needEffectDescriber.DescribeNeedEffects(content, needManager, needSpec);
			}
		}

		// Token: 0x04000034 RID: 52
		public readonly ImmutableArray<INeedEffectDescriber> _needEffectDescribers;

		// Token: 0x04000035 RID: 53
		public readonly StringBuilder _contentBuilder = new StringBuilder();
	}
}
