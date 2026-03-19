using System;
using Steamworks;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000007 RID: 7
	public class SteamWorkshopCreateResponse
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		public ulong ItemId { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002168 File Offset: 0x00000368
		public EResult Result { get; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002170 File Offset: 0x00000370
		public SteamWorkshopCreateResponse(ulong itemId, EResult result)
		{
			this.ItemId = itemId;
			this.Result = result;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002186 File Offset: 0x00000386
		public bool Successful
		{
			get
			{
				return this.Result == EResult.k_EResultOK;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002194 File Offset: 0x00000394
		public string ResultMessage
		{
			get
			{
				return string.Format("{0} ({1})", this.Result.ToString(), (int)this.Result);
			}
		}
	}
}
