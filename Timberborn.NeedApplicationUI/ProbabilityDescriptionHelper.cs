using System;
using Timberborn.NeedApplication;

namespace Timberborn.NeedApplicationUI
{
	// Token: 0x02000007 RID: 7
	public static class ProbabilityDescriptionHelper
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022C4 File Offset: 0x000004C4
		public static string GetDisplayName(EffectProbability probability)
		{
			string result;
			switch (probability)
			{
			case EffectProbability.Low:
				result = ProbabilityDescriptionHelper.LowProbabilityLocKey;
				break;
			case EffectProbability.Medium:
				result = ProbabilityDescriptionHelper.MediumProbabilityLocKey;
				break;
			case EffectProbability.High:
				result = ProbabilityDescriptionHelper.HighProbabilityLocKey;
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unknown probability: {0}", probability));
			}
			return result;
		}

		// Token: 0x04000010 RID: 16
		public static readonly string LowProbabilityLocKey = "EffectProbability.Low";

		// Token: 0x04000011 RID: 17
		public static readonly string MediumProbabilityLocKey = "EffectProbability.Medium";

		// Token: 0x04000012 RID: 18
		public static readonly string HighProbabilityLocKey = "EffectProbability.High";
	}
}
