using System;
using System.Linq;
using Steamworks;
using Timberborn.SteamStoreSystem;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x0200000C RID: 12
	public class SteamWorkshopItemUpdater
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000254C File Offset: 0x0000074C
		public SteamWorkshopUpdateHandle Update(SteamWorkshopUpdateRequest request, Action<SteamWorkshopUpdateResponse> updateCallback)
		{
			UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(SteamAppId.AppId, new PublishedFileId_t(request.ItemId));
			SteamWorkshopItemUpdater.SetUpdateContent(request, ugcupdateHandle_t);
			SteamAPICall_t hAPICall = SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, request.Changelog);
			CallResult<SubmitItemUpdateResult_t>.Create(null).Set(hAPICall, delegate(SubmitItemUpdateResult_t t, bool failure)
			{
				SteamWorkshopItemUpdater.OnItemUpdated(t, failure, updateCallback, request);
			});
			return new SteamWorkshopUpdateHandle(ugcupdateHandle_t);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025C4 File Offset: 0x000007C4
		public static void SetUpdateContent(SteamWorkshopUpdateRequest updateRequest, UGCUpdateHandle_t updateHandle)
		{
			if (!string.IsNullOrEmpty(updateRequest.Name))
			{
				SteamUGC.SetItemTitle(updateHandle, updateRequest.Name);
			}
			if (!string.IsNullOrEmpty(updateRequest.Description))
			{
				SteamUGC.SetItemDescription(updateHandle, updateRequest.Description);
			}
			if (updateRequest.Visibility != null)
			{
				SteamUGC.SetItemVisibility(updateHandle, updateRequest.Visibility.Value.ToStorageVisibility());
			}
			if (updateRequest.MandatoryTags.Length > 0 || updateRequest.ChosenTags.Length > 0)
			{
				SteamUGC.SetItemTags(updateHandle, updateRequest.MandatoryTags.Concat(updateRequest.ChosenTags).ToArray<string>(), false);
			}
			if (!string.IsNullOrEmpty(updateRequest.PreviewPath))
			{
				SteamUGC.SetItemPreview(updateHandle, updateRequest.PreviewPath);
			}
			if (!string.IsNullOrEmpty(updateRequest.ContentPath))
			{
				SteamUGC.SetItemContent(updateHandle, updateRequest.ContentPath);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026AD File Offset: 0x000008AD
		public static void OnItemUpdated(SubmitItemUpdateResult_t result, bool ioFailure, Action<SteamWorkshopUpdateResponse> updateCallback, SteamWorkshopUpdateRequest request)
		{
			updateCallback(new SteamWorkshopUpdateResponse(request, ioFailure ? EResult.k_EResultIOFailure : result.m_eResult));
		}
	}
}
