using System;
using Timberborn.Localization;
using Timberborn.MainMenuModdingUI;
using Timberborn.Modding;
using Timberborn.SingletonSystem;
using Timberborn.SteamStoreSystem;
using Timberborn.SteamWorkshopUI;

namespace Timberborn.SteamWorkshopModUploadingUI
{
	// Token: 0x02000007 RID: 7
	public class SteamWorkshopModUploader : ILoadableSingleton
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002523 File Offset: 0x00000723
		public SteamWorkshopModUploader(SteamWorkshopUploadPanel steamWorkshopUploadPanel, SteamWorkshopUploadableModFactory steamWorkshopUploadableModFactory, ModUploaderBox modUploaderBox, ILoc loc, SteamManager steamManager)
		{
			this._steamWorkshopUploadPanel = steamWorkshopUploadPanel;
			this._steamWorkshopUploadableModFactory = steamWorkshopUploadableModFactory;
			this._modUploaderBox = modUploaderBox;
			this._loc = loc;
			this._steamManager = steamManager;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002550 File Offset: 0x00000750
		public void Load()
		{
			if (this._steamManager.Initialized)
			{
				this._modUploaderBox.AddUploader(this._loc.T(SteamWorkshopModUploader.UploadLocKey), new Action<Mod>(this.OpenUploadPanel));
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002586 File Offset: 0x00000786
		public void OpenUploadPanel(Mod mod)
		{
			this._steamWorkshopUploadPanel.Open(this._steamWorkshopUploadableModFactory.Create(mod));
		}

		// Token: 0x04000015 RID: 21
		public static readonly string UploadLocKey = "SteamWorkshop.UploadToSteamWorkshop";

		// Token: 0x04000016 RID: 22
		public readonly SteamWorkshopUploadPanel _steamWorkshopUploadPanel;

		// Token: 0x04000017 RID: 23
		public readonly SteamWorkshopUploadableModFactory _steamWorkshopUploadableModFactory;

		// Token: 0x04000018 RID: 24
		public readonly ModUploaderBox _modUploaderBox;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;

		// Token: 0x0400001A RID: 26
		public readonly SteamManager _steamManager;
	}
}
