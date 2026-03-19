using System;
using Timberborn.CoreUI;
using Timberborn.MapRepositorySystemUI;

namespace Timberborn.MapEditorPersistenceUI
{
	// Token: 0x02000006 RID: 6
	public class MapSaverLoader
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002417 File Offset: 0x00000617
		public MapSaverLoader(MapPersistenceController mapPersistenceController, PanelStack panelStack, SaveMapBox saveMapBox, LoadMapBox loadMapBox, NewMapBox newMapBox)
		{
			this._mapPersistenceController = mapPersistenceController;
			this._panelStack = panelStack;
			this._saveMapBox = saveMapBox;
			this._loadMapBox = loadMapBox;
			this._newMapBox = newMapBox;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002444 File Offset: 0x00000644
		public void Save(Action successAction = null)
		{
			if (!this._mapPersistenceController.TrySaveCurrent(successAction))
			{
				this.SaveAs(successAction);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000245B File Offset: 0x0000065B
		public void SaveCurrentSilently()
		{
			this._mapPersistenceController.SaveCurrentSilently();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002468 File Offset: 0x00000668
		public void SaveAs(Action successAction = null)
		{
			this._saveMapBox.Open(successAction);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002476 File Offset: 0x00000676
		public void LoadMap()
		{
			this._loadMapBox.OpenAsOverlay();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002483 File Offset: 0x00000683
		public void NewMap()
		{
			this._panelStack.HideAndPushOverlay(this._newMapBox);
		}

		// Token: 0x0400000C RID: 12
		private readonly MapPersistenceController _mapPersistenceController;

		// Token: 0x0400000D RID: 13
		private readonly PanelStack _panelStack;

		// Token: 0x0400000E RID: 14
		private readonly SaveMapBox _saveMapBox;

		// Token: 0x0400000F RID: 15
		private readonly LoadMapBox _loadMapBox;

		// Token: 0x04000010 RID: 16
		private readonly NewMapBox _newMapBox;
	}
}
