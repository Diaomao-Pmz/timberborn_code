using System;
using Steamworks;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x0200000E RID: 14
	public class SteamWorkshopUpdateHandle
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000026DD File Offset: 0x000008DD
		public SteamWorkshopUpdateHandle(UGCUpdateHandle_t updateHandle)
		{
			this._updateHandle = updateHandle;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026EC File Offset: 0x000008EC
		public float GetProgress()
		{
			if (this._updateHandle == UGCUpdateHandle_t.Invalid)
			{
				return 0f;
			}
			ulong num;
			ulong num2;
			if (SteamUGC.GetItemUpdateProgress(this._updateHandle, out num, out num2) != EItemUpdateStatus.k_EItemUpdateStatusInvalid)
			{
				return num / num2;
			}
			return 0f;
		}

		// Token: 0x0400001D RID: 29
		public readonly UGCUpdateHandle_t _updateHandle;
	}
}
