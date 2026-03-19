using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000027 RID: 39
	[Obsolete("Use [ContractAnnotation('=> halt')] instead")]
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class TerminatesProgramAttribute : Attribute
	{
	}
}
