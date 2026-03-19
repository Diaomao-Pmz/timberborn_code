using System;
using Steamworks;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000011 RID: 17
	public class SteamWorkshopUpdateResponse
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000291C File Offset: 0x00000B1C
		public SteamWorkshopUpdateRequest Request { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002924 File Offset: 0x00000B24
		public EResult Result { get; }

		// Token: 0x0600003F RID: 63 RVA: 0x0000292C File Offset: 0x00000B2C
		public SteamWorkshopUpdateResponse(SteamWorkshopUpdateRequest request, EResult result)
		{
			this.Request = request;
			this.Result = result;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002942 File Offset: 0x00000B42
		public bool Successful
		{
			get
			{
				return this.Result == EResult.k_EResultOK;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002950 File Offset: 0x00000B50
		public string ResultMessage
		{
			get
			{
				return string.Format("{0} ({1})", this.Result.ToString(), (int)this.Result);
			}
		}
	}
}
