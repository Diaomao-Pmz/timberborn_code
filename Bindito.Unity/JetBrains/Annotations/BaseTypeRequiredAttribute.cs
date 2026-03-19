using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[BaseTypeRequired(typeof(Attribute))]
	internal sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021D7 File Offset: 0x000003D7
		[NotNull]
		public Type BaseType { get; }

		// Token: 0x0600001F RID: 31 RVA: 0x000021DF File Offset: 0x000003DF
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}
	}
}
