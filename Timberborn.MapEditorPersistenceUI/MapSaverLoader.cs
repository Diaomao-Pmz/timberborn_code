using System;
using Timberborn.CoreUI;
using Timberborn.MapRepositorySystemUI;

namespace Timberborn.MapEditorPersistenceUI
{
	// Token: 0x02000008 RID: 8
	public class MapSaverLoader
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002429 File Offset: 0x00000629
		public MapSaverLoader(MapPersistenceController mapPersistenceController, PanelStack panelStack, SaveMapBox saveMapBox, LoadMapBox loadMapBox, NewMapBox newMapBox)
		{
			this._mapPersistenceController = mapPersistenceController;
			this._panelStack = panelStack;
			this._saveMapBox = saveMapBox;
			this._loadMapBox = loadMapBox;
			this._newMapBox = newMapBox;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002456 File Offset: 0x00000656
		public void Save(Action successAction = null)
		{
			if (!this._mapPersistenceController.TrySaveCurrent(successAction))
			{
				this.SaveAs(successAction);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000246D File Offset: 0x0000066D
		public void SaveCurrentSilently()
		{
			this._mapPersistenceController.SaveCurrentSilently();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000247A File Offset: 0x0000067A
		public void SaveAs(Action successAction = null)
		{
			this._saveMapBox.Open(successAction);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002488 File Offset: 0x00000688
		public void LoadMap()
		{
			this._loadMapBox.OpenAsOverlay();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002495 File Offset: 0x00000695
		public void NewMap()
		{
			this._panelStack.HideAndPushOverlay(this._newMapBox);
		}

		// Token: 0x04000014 RID: 20
		public readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x04000015 RID: 21
		public readonly PanelStack _panelStack;

		// Token: 0x04000016 RID: 22
		public readonly SaveMapBox _saveMapBox;

		// Token: 0x04000017 RID: 23
		public readonly LoadMapBox _loadMapBox;

		// Token: 0x04000018 RID: 24
		public readonly NewMapBox _newMapBox;
	}
}
