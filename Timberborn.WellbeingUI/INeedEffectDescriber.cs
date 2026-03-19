using System;
using System.Text;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.WellbeingUI
{
	// Token: 0x0200000B RID: 11
	public interface INeedEffectDescriber
	{
		// Token: 0x06000029 RID: 41
		void DescribeNeedEffects(StringBuilder content, NeedManager needManager, NeedSpec needSpec);
	}
}
