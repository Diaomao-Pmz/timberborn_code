using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.Localization;
using Timberborn.MapItemsUI;
using Timberborn.MapRepositorySystemUI;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x0200000E RID: 14
	public class NewGameMapPanel : IPanelController, ILoadableSingleton
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000038AF File Offset: 0x00001AAF
		public NewGameMapPanel(VisualElementLoader visualElementLoader, ILoc loc, PanelStack panelStack, NewGameModePanel newGameModePanel, MapValidator mapValidator, MapSelection mapSelection)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._panelStack = panelStack;
			this._newGameModePanel = newGameModePanel;
			this._mapValidator = mapValidator;
			this._mapSelection = mapSelection;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000038E4 File Offset: 0x00001AE4
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MainMenu/NewGameMapPanel");
			this._next = UQueryExtensions.Q<Button>(this._root, "NextButton", null);
			this._next.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnNextButtonClicked), 0);
			this._next.text = this._loc.T(CommonLocKeys.NavigationNextKey);
			Button button = UQueryExtensions.Q<Button>(this._root, "BackButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			button.text = this._loc.T(CommonLocKeys.NavigationBackKey);
			this._mapSelection.InitializeWithMapGoalsShown(this._root, delegate
			{
				this.OnNextButtonClicked(null);
			});
			this._mapSelection.SelectedMapChanged += this.OnSelectionChanged;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039BE File Offset: 0x00001BBE
		public void Open(FactionSpec factionSpec)
		{
			this._factionSpec = factionSpec;
			this._panelStack.HideAndPush(this);
			this._mapSelection.Open();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000039DE File Offset: 0x00001BDE
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000039E6 File Offset: 0x00001BE6
		public bool OnUIConfirmed()
		{
			this.ShowParametersPanelIfMapValid();
			return false;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000039EF File Offset: 0x00001BEF
		public void OnUICancelled()
		{
			this.Clear();
			this._panelStack.Pop(this);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A03 File Offset: 0x00001C03
		public void Clear()
		{
			this._mapSelection.Clear();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003A10 File Offset: 0x00001C10
		public void OnNextButtonClicked(ClickEvent evt)
		{
			this.ShowParametersPanelIfMapValid();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A18 File Offset: 0x00001C18
		public void OnSelectionChanged(object sender, EventArgs e)
		{
			MapItem mapItem;
			this._next.SetEnabled(this._mapSelection.TryGetSelectedMap(out mapItem));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003A40 File Offset: 0x00001C40
		public void ShowParametersPanelIfMapValid()
		{
			MapItem selectedMap;
			if (this._mapSelection.TryGetSelectedMap(out selectedMap))
			{
				this._mapValidator.ValidateForNewGame(selectedMap.MapFileReference, delegate
				{
					this.ShowParametersPanel(selectedMap);
				});
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A8F File Offset: 0x00001C8F
		public void ShowParametersPanel(MapItem mapItem)
		{
			this._newGameModePanel.SelectFactionAndMap(this._factionSpec, mapItem);
			this._panelStack.HideAndPush(this._newGameModePanel);
		}

		// Token: 0x04000065 RID: 101
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000066 RID: 102
		public readonly ILoc _loc;

		// Token: 0x04000067 RID: 103
		public readonly PanelStack _panelStack;

		// Token: 0x04000068 RID: 104
		public readonly NewGameModePanel _newGameModePanel;

		// Token: 0x04000069 RID: 105
		public readonly MapValidator _mapValidator;

		// Token: 0x0400006A RID: 106
		public readonly MapSelection _mapSelection;

		// Token: 0x0400006B RID: 107
		public FactionSpec _factionSpec;

		// Token: 0x0400006C RID: 108
		public VisualElement _root;

		// Token: 0x0400006D RID: 109
		public Button _next;
	}
}
