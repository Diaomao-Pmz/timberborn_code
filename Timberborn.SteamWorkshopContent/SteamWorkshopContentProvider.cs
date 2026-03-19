using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using Timberborn.SteamStoreSystem;

namespace Timberborn.SteamWorkshopContent
{
	// Token: 0x02000005 RID: 5
	public class SteamWorkshopContentProvider
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		public SteamWorkshopContentProvider(SteamManager steamManager)
		{
			this._steamManager = steamManager;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E7 File Offset: 0x000002E7
		public IEnumerable<DirectoryInfo> GetContentDirectories()
		{
			if (this._steamManager.Initialized)
			{
				using (IEnumerator<PublishedFileId_t> enumerator = SteamWorkshopContentProvider.GetSubscribedItems().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ulong num;
						string path;
						uint num2;
						if (SteamUGC.GetItemInstallInfo(enumerator.Current, out num, out path, SteamWorkshopContentProvider.PathBufferSize, out num2))
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(path);
							if (directoryInfo.Exists)
							{
								yield return directoryInfo;
							}
						}
					}
				}
				IEnumerator<PublishedFileId_t> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		public static IEnumerable<PublishedFileId_t> GetSubscribedItems()
		{
			uint numSubscribedItems = SteamUGC.GetNumSubscribedItems();
			PublishedFileId_t[] array = new PublishedFileId_t[numSubscribedItems];
			SteamUGC.GetSubscribedItems(array, numSubscribedItems);
			return array;
		}

		// Token: 0x04000006 RID: 6
		public static readonly uint PathBufferSize = 1024U;

		// Token: 0x04000007 RID: 7
		public readonly SteamManager _steamManager;
	}
}
