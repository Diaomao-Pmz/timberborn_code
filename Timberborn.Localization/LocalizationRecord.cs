using System;
using JetBrains.Annotations;
using LINQtoCSV;

namespace Timberborn.Localization
{
	// Token: 0x02000014 RID: 20
	public class LocalizationRecord
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000307F File Offset: 0x0000127F
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003087 File Offset: 0x00001287
		[CsvColumn(Name = "ID")]
		public string Id { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003090 File Offset: 0x00001290
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003098 File Offset: 0x00001298
		[CsvColumn(Name = "Text")]
		public string Text { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030A1 File Offset: 0x000012A1
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000030A9 File Offset: 0x000012A9
		[UsedImplicitly]
		[CsvColumn(Name = "Comment")]
		public string Comment { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030B2 File Offset: 0x000012B2
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000030BA File Offset: 0x000012BA
		public bool HideWarning { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000030C3 File Offset: 0x000012C3
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000030CB File Offset: 0x000012CB
		public bool IsBuiltIn { get; set; }
	}
}
