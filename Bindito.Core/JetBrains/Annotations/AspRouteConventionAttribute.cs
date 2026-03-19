using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class AspRouteConventionAttribute : Attribute
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000271B File Offset: 0x0000091B
		[CanBeNull]
		public string PredefinedPattern { get; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00002723 File Offset: 0x00000923
		public AspRouteConventionAttribute()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000272B File Offset: 0x0000092B
		public AspRouteConventionAttribute(string predefinedPattern)
		{
			this.PredefinedPattern = predefinedPattern;
		}
	}
}
