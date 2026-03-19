using System;
using Steamworks;
using Timberborn.SteamStoreSystem;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000009 RID: 9
	public class SteamWorkshopItemCreator
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002300 File Offset: 0x00000500
		public void CreateItem(Action<SteamWorkshopCreateResponse> createCallback)
		{
			SteamAPICall_t hAPICall = SteamUGC.CreateItem(SteamAppId.AppId, EWorkshopFileType.k_EWorkshopFileTypeFirst);
			CallResult<CreateItemResult_t>.Create(null).Set(hAPICall, delegate(CreateItemResult_t t, bool failure)
			{
				SteamWorkshopItemCreator.OnItemCreated(t, failure, createCallback);
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000233E File Offset: 0x0000053E
		public static void OnItemCreated(CreateItemResult_t result, bool ioFailure, Action<SteamWorkshopCreateResponse> createCallback)
		{
			createCallback(new SteamWorkshopCreateResponse(result.m_nPublishedFileId.m_PublishedFileId, ioFailure ? EResult.k_EResultIOFailure : result.m_eResult));
		}
	}
}
