using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	internal sealed class MacroAttribute : Attribute
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000236D File Offset: 0x0000056D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002375 File Offset: 0x00000575
		[CanBeNull]
		public string Expression { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000237E File Offset: 0x0000057E
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002386 File Offset: 0x00000586
		public int Editable { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000238F File Offset: 0x0000058F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002397 File Offset: 0x00000597
		[CanBeNull]
		public string Target { get; set; }
	}
}
