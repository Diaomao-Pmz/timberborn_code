using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
	internal sealed class ValueProviderAttribute : Attribute
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000208F File Offset: 0x0000028F
		[NotNull]
		public string Name { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002097 File Offset: 0x00000297
		public ValueProviderAttribute([NotNull] string name)
		{
			this.Name = name;
		}
	}
}
