using System;
using JetBrains.Annotations;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Method)]
	[MeansImplicitUse]
	public class OnEventAttribute : Attribute
	{
	}
}
