using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	internal sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002177 File Offset: 0x00000377
		[NotNull]
		public string Contract { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000217F File Offset: 0x0000037F
		public bool ForceFullStates { get; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002187 File Offset: 0x00000387
		public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002191 File Offset: 0x00000391
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}
	}
}
