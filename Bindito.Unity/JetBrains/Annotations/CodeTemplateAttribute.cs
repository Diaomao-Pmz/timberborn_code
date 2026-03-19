using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class CodeTemplateAttribute : Attribute
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002456 File Offset: 0x00000656
		public string SearchTemplate { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000245E File Offset: 0x0000065E
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002466 File Offset: 0x00000666
		public string Message { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000246F File Offset: 0x0000066F
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002477 File Offset: 0x00000677
		public string ReplaceTemplate { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002480 File Offset: 0x00000680
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002488 File Offset: 0x00000688
		public string ReplaceMessage { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002491 File Offset: 0x00000691
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002499 File Offset: 0x00000699
		public bool FormatAfterReplace { get; set; } = true;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000024A2 File Offset: 0x000006A2
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000024AA File Offset: 0x000006AA
		public bool MatchSimilarConstructs { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000024B3 File Offset: 0x000006B3
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000024BB File Offset: 0x000006BB
		public bool ShortenReferences { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000024C4 File Offset: 0x000006C4
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000024CC File Offset: 0x000006CC
		public string SuppressionKey { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x000024D5 File Offset: 0x000006D5
		public CodeTemplateAttribute(string searchTemplate)
		{
			this.SearchTemplate = searchTemplate;
		}
	}
}
