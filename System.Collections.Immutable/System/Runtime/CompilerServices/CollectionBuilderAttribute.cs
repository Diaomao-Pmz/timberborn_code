using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	internal sealed class CollectionBuilderAttribute : Attribute
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002D72 File Offset: 0x00000F72
		public CollectionBuilderAttribute(Type builderType, string methodName)
		{
			this.BuilderType = builderType;
			this.MethodName = methodName;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002D88 File Offset: 0x00000F88
		public Type BuilderType { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002D90 File Offset: 0x00000F90
		public string MethodName { get; }
	}
}
