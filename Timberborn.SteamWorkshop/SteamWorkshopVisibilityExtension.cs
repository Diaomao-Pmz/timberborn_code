using System;
using Steamworks;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000013 RID: 19
	public static class SteamWorkshopVisibilityExtension
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002988 File Offset: 0x00000B88
		public static ERemoteStoragePublishedFileVisibility ToStorageVisibility(this SteamWorkshopVisibility visibility)
		{
			ERemoteStoragePublishedFileVisibility result;
			switch (visibility)
			{
			case SteamWorkshopVisibility.Private:
				result = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate;
				break;
			case SteamWorkshopVisibility.Friends:
				result = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly;
				break;
			case SteamWorkshopVisibility.Public:
				result = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic;
				break;
			case SteamWorkshopVisibility.Unlisted:
				result = ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityUnlisted;
				break;
			default:
				throw new ArgumentOutOfRangeException("visibility", visibility, null);
			}
			return result;
		}
	}
}
