using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true)]
	internal sealed class NoReorderAttribute : Attribute
	{
	}
}
