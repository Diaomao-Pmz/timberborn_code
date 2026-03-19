using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002158 File Offset: 0x00000358
		[CanBeNull]
		public string ParameterName { get; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002160 File Offset: 0x00000360
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002168 File Offset: 0x00000368
		public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
		{
			this.ParameterName = parameterName;
		}
	}
}
