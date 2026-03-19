using System;
using Timberborn.MapRepositorySystemUI;
using Timberborn.SingletonSystem;
using Timberborn.SteamOverlaySystem;
using Timberborn.SteamStoreSystem;

namespace Timberborn.SteamWorkshopMapDownloadingUI
{
	// Token: 0x02000004 RID: 4
	public class SteamWorkshopMapDownloader : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public SteamWorkshopMapDownloader(SteamOverlayOpener steamOverlayOpener, MapDownloader mapDownloader, SteamManager steamManager)
		{
			this._steamOverlayOpener = steamOverlayOpener;
			this._mapDownloader = mapDownloader;
			this._steamManager = steamManager;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public void Load()
		{
			if (this._steamManager.Initialized)
			{
				this._mapDownloader.SetDownloadAction(new Action(this.ShowDownloadableMaps));
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002101 File Offset: 0x00000301
		public void ShowDownloadableMaps()
		{
			this._steamOverlayOpener.OpenWorkshopSearch("Map");
		}

		// Token: 0x04000006 RID: 6
		public readonly SteamOverlayOpener _steamOverlayOpener;

		// Token: 0x04000007 RID: 7
		public readonly MapDownloader _mapDownloader;

		// Token: 0x04000008 RID: 8
		public readonly SteamManager _steamManager;
	}
}
