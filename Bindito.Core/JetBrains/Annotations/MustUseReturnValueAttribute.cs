using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MustUseReturnValueAttribute : Attribute
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000022C7 File Offset: 0x000004C7
		[CanBeNull]
		public string Justification { get; }

		// Token: 0x06000036 RID: 54 RVA: 0x000022CF File Offset: 0x000004CF
		public MustUseReturnValueAttribute()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000022D7 File Offset: 0x000004D7
		public MustUseReturnValueAttribute([NotNull] string justification)
		{
			this.Justification = justification;
		}
	}
}
