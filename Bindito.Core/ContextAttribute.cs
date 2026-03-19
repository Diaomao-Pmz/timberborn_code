using System;
using JetBrains.Annotations;

namespace Bindito.Core
{
	// Token: 0x0200006F RID: 111
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
	public class ContextAttribute : Attribute
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000029CD File Offset: 0x00000BCD
		public string ContextName { get; }

		// Token: 0x060000EC RID: 236 RVA: 0x000029D5 File Offset: 0x00000BD5
		public ContextAttribute(string contextName)
		{
			this.ContextName = contextName;
		}
	}
}
