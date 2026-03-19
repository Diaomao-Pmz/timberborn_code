using System;
using JetBrains.Annotations;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	[MeansImplicitUse]
	public class SingletonAttribute : Attribute
	{
	}
}
