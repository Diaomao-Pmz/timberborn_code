using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapEditorSceneLoading;
using Timberborn.MapItemsUI;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000006 RID: 6
	public class LoadMapBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		public LoadMapBox(DialogBoxShower dialogBoxShower, ILoc loc, MapEditorSceneLoader mapEditorSceneLoader, MapValidator mapValidator, PanelStack panelStack, VisualElementLoader visualElementLoader, MapSelection mapSelection, MapRepository mapRepository)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
			this._mapEditorSceneLoader = mapEditorSceneLoader;
			this._mapValidator = mapValidator;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._mapSelection = mapSelection;
			this._mapRepository = mapRepository;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/LoadMapBox");
			this._deleteMap = UQueryExtensions.Q<Button>(this._root, "DeleteMap", null);
			this._deleteMap.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnDeleteMapButtonClicked), 0);
			this._load = UQueryExtensions.Q<Button>(this._root, "LoadButton", null);
			this._load.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.TryLoadMap();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			this._mapSelection.InitializeWithMapGoalsHidden(this._root, delegate
			{
				this.TryLoadMap();
			});
			this._mapSelection.SelectedMapChanged += this.OnSelectionChanged;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000221C File Offset: 0x0000041C
		public void Open()
		{
			this._panelStack.HideAndPush(this);
			this._mapSelection.Open();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002235 File Offset: 0x00000435
		public void OpenAsOverlay()
		{
			this._panelStack.HideAndPushOverlay(this);
			this._mapSelection.Open();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000224E File Offset: 0x0000044E
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002256 File Offset: 0x00000456
		public bool OnUIConfirmed()
		{
			return this.TryLoadMap();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000225E File Offset: 0x0000045E
		public void OnUICancelled()
		{
			this._mapSelection.Clear();
			this._panelStack.Pop(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002278 File Offset: 0x00000478
		public void OnDeleteMapButtonClicked(ClickEvent evt)
		{
			MapItem mapItem;
			if (this._mapSelection.TryGetSelectedMap(out mapItem) && mapItem.IsDeletable)
			{
				this._dialogBoxShower.Create().SetMessage(this._loc.T<string>(LoadMapBox.DeleteMapPromptLocKey, mapItem.DisplayName)).SetConfirmButton(delegate()
				{
					this._mapRepository.DeleteMap(mapItem.MapFileReference);
				}).SetDefaultCancelButton().Show();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022FC File Offset: 0x000004FC
		public void OnSelectionChanged(object sender, EventArgs eventArgs)
		{
			MapItem mapItem;
			if (this._mapSelection.TryGetSelectedMap(out mapItem))
			{
				this._deleteMap.SetEnabled(mapItem.IsDeletable);
				this._load.SetEnabled(true);
				return;
			}
			this._deleteMap.SetEnabled(false);
			this._load.SetEnabled(false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002350 File Offset: 0x00000550
		public bool TryLoadMap()
		{
			MapItem mapItem;
			if (this._mapSelection.TryGetSelectedMap(out mapItem))
			{
				MapFileReference mapFileReference = mapItem.MapFileReference;
				this._mapValidator.ValidateForMapEditor(mapFileReference, delegate
				{
					this._mapEditorSceneLoader.LoadMap(mapFileReference);
				});
				return true;
			}
			return false;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string DeleteMapPromptLocKey = "LoadMapPanel.DeleteMapPrompt";

		// Token: 0x04000009 RID: 9
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly MapEditorSceneLoader _mapEditorSceneLoader;

		// Token: 0x0400000C RID: 12
		public readonly MapValidator _mapValidator;

		// Token: 0x0400000D RID: 13
		public readonly PanelStack _panelStack;

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly MapSelection _mapSelection;

		// Token: 0x04000010 RID: 16
		public readonly MapRepository _mapRepository;

		// Token: 0x04000011 RID: 17
		public Button _deleteMap;

		// Token: 0x04000012 RID: 18
		public Button _load;

		// Token: 0x04000013 RID: 19
		public VisualElement _root;
	}
}
