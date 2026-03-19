using System;

namespace Timberborn.InputSystem
{
	// Token: 0x02000014 RID: 20
	public class KeywordMatchedEvent
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003020 File Offset: 0x00001220
		public string KeywordNotification { get; }

		// Token: 0x0600008C RID: 140 RVA: 0x00003028 File Offset: 0x00001228
		public KeywordMatchedEvent(string keywordNotification)
		{
			this.KeywordNotification = keywordNotification;
		}
	}
}
