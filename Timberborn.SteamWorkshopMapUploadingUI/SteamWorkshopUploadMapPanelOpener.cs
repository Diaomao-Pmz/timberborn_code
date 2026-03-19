using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapEditorPersistenceUI;
using Timberborn.MapEditorUI;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using Timberborn.SteamStoreSystem;
using Timberborn.SteamWorkshopUI;

namespace Timberborn.SteamWorkshopMapUploadingUI
{
	// Token: 0x02000009 RID: 9
	public class SteamWorkshopUploadMapPanelOpener : ILoadableSingleton
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000026EC File Offset: 0x000008EC
		public SteamWorkshopUploadMapPanelOpener(SteamWorkshopUploadPanel steamWorkshopUploadPanel, SteamWorkshopUploadableMapFactory steamWorkshopUploadableMapFactory, DialogBoxShower dialogBoxShower, MapPersistenceController mapPersistenceController, MapSaverLoader mapSaverLoader, ILoc loc, FilePanel filePanel, SteamManager steamManager)
		{
			this._steamWorkshopUploadPanel = steamWorkshopUploadPanel;
			this._steamWorkshopUploadableMapFactory = steamWorkshopUploadableMapFactory;
			this._dialogBoxShower = dialogBoxShower;
			this._mapPersistenceController = mapPersistenceController;
			this._mapSaverLoader = mapSaverLoader;
			this._loc = loc;
			this._filePanel = filePanel;
			this._steamManager = steamManager;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000273C File Offset: 0x0000093C
		public void Load()
		{
			if (this._steamManager.Initialized)
			{
				this._filePanel.AddMapFileButton(new Action(this.Open), SteamWorkshopUploadMapPanelOpener.UploadLocKey);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002768 File Offset: 0x00000968
		public void Open()
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(SteamWorkshopUploadMapPanelOpener.MapWillBeSavedLocKey).SetConfirmButton(delegate()
			{
				this._mapSaverLoader.Save(new Action(this.OpenUploadPanel));
			}, this._loc.T(CommonLocKeys.OKKey)).SetDefaultCancelButton(this._loc.T(CommonLocKeys.CancelKey)).Show();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027C8 File Offset: 0x000009C8
		public void OpenUploadPanel()
		{
			MapFileReference mapFileReference;
			if (this._mapPersistenceController.TryGetCurrentMap(out mapFileReference))
			{
				SteamWorkshopUploadableMap steamWorkshopUploadable = this._steamWorkshopUploadableMapFactory.Create(mapFileReference);
				this._steamWorkshopUploadPanel.Open(steamWorkshopUploadable);
				return;
			}
			throw new InvalidOperationException("Tried to upload unsaved map to Steam");
		}

		// Token: 0x04000025 RID: 37
		public static readonly string UploadLocKey = "MapEditor.UploadMap";

		// Token: 0x04000026 RID: 38
		public static readonly string MapWillBeSavedLocKey = "MapEditor.MapWillBeSaved";

		// Token: 0x04000027 RID: 39
		public readonly SteamWorkshopUploadPanel _steamWorkshopUploadPanel;

		// Token: 0x04000028 RID: 40
		public readonly SteamWorkshopUploadableMapFactory _steamWorkshopUploadableMapFactory;

		// Token: 0x04000029 RID: 41
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400002A RID: 42
		public readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x0400002B RID: 43
		public readonly MapSaverLoader _mapSaverLoader;

		// Token: 0x0400002C RID: 44
		public readonly ILoc _loc;

		// Token: 0x0400002D RID: 45
		public readonly FilePanel _filePanel;

		// Token: 0x0400002E RID: 46
		public readonly SteamManager _steamManager;
	}
}
