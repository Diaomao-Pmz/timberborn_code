using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapEditorPersistence;
using Timberborn.MapRepositorySystem;
using Timberborn.MapRepositorySystemUI;
using Timberborn.MapSystem;
using Timberborn.QuickNotificationSystem;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;
using UnityEngine;

namespace Timberborn.MapEditorPersistenceUI
{
	// Token: 0x02000005 RID: 5
	public class MapPersistenceController
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
		public MapPersistenceController(MapEditorMapLoader mapEditorMapLoader, MapSaver mapSaver, DialogBoxShower dialogBoxShower, QuickNotificationService quickNotificationService, ILoc loc, MapVersionCompatibilityService mapVersionCompatibilityService, EventBus eventBus)
		{
			this._mapEditorMapLoader = mapEditorMapLoader;
			this._mapSaver = mapSaver;
			this._dialogBoxShower = dialogBoxShower;
			this._quickNotificationService = quickNotificationService;
			this._loc = loc;
			this._mapVersionCompatibilityService = mapVersionCompatibilityService;
			this._eventBus = eventBus;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
		public Version CurrentMapVersion
		{
			get
			{
				MapFileReference mapFileReference;
				if (!this.TryGetCurrentMap(out mapFileReference))
				{
					return MapPersistenceController.NewMapVersion;
				}
				return this._mapVersionCompatibilityService.GetMapVersionNumber(mapFileReference);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		public bool IsCurrentMapCompatible
		{
			get
			{
				MapFileReference mapFileReference;
				return !this.TryGetCurrentMap(out mapFileReference) || this._mapVersionCompatibilityService.IsMapFullyCompatible(mapFileReference);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
		public void SaveAs(string mapName, Action successAction)
		{
			try
			{
				if (this._mapSaver.MapExists(mapName))
				{
					this._dialogBoxShower.Create().SetMessage(this._loc.T<string>(MapPersistenceController.MapExistsLocKey, mapName)).SetConfirmButton(delegate()
					{
						this.ForceSaveAs(mapName, successAction, true);
					}, this._loc.T(CommonLocKeys.OverwriteKey)).SetDefaultCancelButton(this._loc.T(CommonLocKeys.CancelKey)).Show();
				}
				else
				{
					this.ForceSaveAs(mapName, successAction, true);
				}
			}
			catch (MapSaverException ex)
			{
				Debug.LogError(string.Format("Error occured while saving: {0}", ex.InnerException));
				this._dialogBoxShower.Create().SetLocalizedMessage(MapPersistenceController.ErrorLocKey).Show();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002278 File Offset: 0x00000478
		public bool TrySaveCurrent(Action successAction)
		{
			return this.TrySaveCurrentInternal(true, successAction);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002282 File Offset: 0x00000482
		public void SaveCurrentSilently()
		{
			if (!this.TrySaveCurrentInternal(false, null))
			{
				throw new InvalidOperationException("No map to save");
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000229C File Offset: 0x0000049C
		public bool TryGetCurrentMap(out MapFileReference mapFileReference)
		{
			if (this._mapSaver.LastSavedMap != null)
			{
				mapFileReference = this._mapSaver.LastSavedMap.Value;
				return true;
			}
			if (this._mapEditorMapLoader.LoadedMap != null)
			{
				mapFileReference = this._mapEditorMapLoader.LoadedMap.Value;
				return mapFileReference.UserFolder;
			}
			mapFileReference = default(MapFileReference);
			return false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002318 File Offset: 0x00000518
		public bool TrySaveCurrentInternal(bool notify, Action successAction)
		{
			MapFileReference mapFileReference;
			if (this.TryGetCurrentMap(out mapFileReference))
			{
				this.ForceSaveAs(mapFileReference.Name, successAction, notify);
				return true;
			}
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002344 File Offset: 0x00000544
		public void ForceSaveAs(string mapName, Action successAction, bool notify)
		{
			try
			{
				this._mapSaver.Save(MapFileReference.FromUserFolder(mapName));
				if (successAction != null)
				{
					successAction();
				}
				if (notify)
				{
					this._quickNotificationService.SendNotification(this._loc.T<string>(MapPersistenceController.SavedAsLocKey, mapName));
				}
				this._eventBus.Post(new MapSavedEvent());
			}
			catch (MapSaverException ex)
			{
				Debug.LogError(string.Format("Error occured while saving: {0}", ex.InnerException));
				this._dialogBoxShower.Create().SetLocalizedMessage(MapPersistenceController.ErrorLocKey).Show();
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string MapExistsLocKey = "MapEditor.SaveMap.MapExists";

		// Token: 0x04000007 RID: 7
		public static readonly string SavedAsLocKey = "MapEditor.SaveMap.SavedAs";

		// Token: 0x04000008 RID: 8
		public static readonly string ErrorLocKey = "Saving.Error";

		// Token: 0x04000009 RID: 9
		public static readonly Version NewMapVersion = Version.Create("0");

		// Token: 0x0400000A RID: 10
		public readonly MapEditorMapLoader _mapEditorMapLoader;

		// Token: 0x0400000B RID: 11
		public readonly MapSaver _mapSaver;

		// Token: 0x0400000C RID: 12
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000D RID: 13
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x0400000E RID: 14
		public readonly ILoc _loc;

		// Token: 0x0400000F RID: 15
		public readonly MapVersionCompatibilityService _mapVersionCompatibilityService;

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;
	}
}
