using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x0200000A RID: 10
	public class NewGameFactionPanel : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000032D8 File Offset: 0x000014D8
		public NewGameFactionPanel(FactionSpecService factionSpecService, NewGameMapPanel newGameMapPanel, PanelStack panelStack, ILoc loc, MainMenuSoundController mainMenuSoundController, VisualElementLoader visualElementLoader, FactionUnlockingService factionUnlockingService, FactionUnlockConditionDescriber factionUnlockConditionDescriber)
		{
			this._factionSpecService = factionSpecService;
			this._newGameMapPanel = newGameMapPanel;
			this._panelStack = panelStack;
			this._loc = loc;
			this._mainMenuSoundController = mainMenuSoundController;
			this._visualElementLoader = visualElementLoader;
			this._factionUnlockingService = factionUnlockingService;
			this._factionUnlockConditionDescriber = factionUnlockConditionDescriber;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003328 File Offset: 0x00001528
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MainMenu/NewGameFactionPanel");
			this._next = UQueryExtensions.Q<Button>(this._root, "NextButton", null);
			this._next.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnNextButtonClicked), 0);
			this._next.text = this._loc.T(CommonLocKeys.NavigationNextKey);
			Button button = UQueryExtensions.Q<Button>(this._root, "BackButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			button.text = this._loc.T(CommonLocKeys.NavigationBackKey);
			this._unlockCondition = UQueryExtensions.Q<Label>(this._root, "UnlockCondition", null);
			this._factionSpecs = (from faction in this._factionSpecService.Factions
			orderby faction.Order
			select faction).ToImmutableArray<FactionSpec>();
			this.CreateFactions(this._root);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003430 File Offset: 0x00001630
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003438 File Offset: 0x00001638
		public bool OnUIConfirmed()
		{
			return this.ShowNewGameMap();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003440 File Offset: 0x00001640
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003450 File Offset: 0x00001650
		public void CreateFactions(VisualElement root)
		{
			this._factionList = UQueryExtensions.Q<VisualElement>(root, "FactionList", null);
			for (int i = 0; i < this._factionSpecs.Length; i++)
			{
				FactionSpec factionSpec = this._factionSpecs[i];
				VisualElement visualElement = this.CreateFaction(factionSpec);
				this.AssignArrowButtons(i, visualElement);
				this._factionList.Add(visualElement);
				if (i == 0)
				{
					this.SelectFaction(factionSpec, visualElement, false);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000034BC File Offset: 0x000016BC
		public VisualElement CreateFaction(FactionSpec factionSpec)
		{
			VisualElement factionElement = this._visualElementLoader.LoadVisualElement("MainMenu/NewGameFactionItem");
			UQueryExtensions.Q<VisualElement>(factionElement, "NormalFaction", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.SelectFaction(factionSpec, factionElement, true);
			}, 0);
			factionElement.RegisterCallback<KeyUpEvent>(delegate(KeyUpEvent evt)
			{
				if (evt.keyCode == 13)
				{
					this.SelectFaction(factionSpec, factionElement, true);
				}
			}, 0);
			Sprite asset = factionSpec.NewGameFullAvatar.Asset;
			Sprite asset2 = factionSpec.Logo.Asset;
			NewGameFactionPanel.SetBackground(factionElement, "Avatar", asset);
			NewGameFactionPanel.SetBackground(factionElement, "Logo", asset2);
			NewGameFactionPanel.SetBackground(factionElement, "SelectedLogo", asset2);
			NewGameFactionPanel.SetBackground(factionElement, "DescriptionLogo", asset2);
			NewGameFactionPanel.SetBackground(factionElement, "SelectedAvatar", asset);
			UQueryExtensions.Q<Label>(factionElement, "SelectedFactionName", null).text = factionSpec.DisplayName.Value;
			UQueryExtensions.Q<Label>(factionElement, "SelectedFactionDescription", null).text = factionSpec.Description.Value;
			return factionElement;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000035F8 File Offset: 0x000017F8
		public void AssignArrowButtons(int index, VisualElement element)
		{
			int previous = Math.Max(0, index - 1);
			UQueryExtensions.Q<Button>(element, "LeftArrow", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.SelectFaction(this._factionSpecs[previous], this._factionList[previous], true);
			}, 0);
			int next = Math.Min(this._factionSpecs.Length - 1, index + 1);
			UQueryExtensions.Q<Button>(element, "RightArrow", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.SelectFaction(this._factionSpecs[next], this._factionList[next], true);
			}, 0);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003678 File Offset: 0x00001878
		public void SelectFaction(FactionSpec factionSpec, VisualElement factionElement, bool playSound)
		{
			if (this._selectedFactionElement != factionElement)
			{
				VisualElement selectedFactionElement = this._selectedFactionElement;
				if (selectedFactionElement != null)
				{
					selectedFactionElement.RemoveFromClassList("selected-faction");
				}
				VisualElement selectedFactionElement2 = this._selectedFactionElement;
				if (selectedFactionElement2 != null)
				{
					selectedFactionElement2.AddToClassList("normal-faction");
				}
				this._selectedFactionElement = factionElement;
				this._selectedFactionElement.RemoveFromClassList("normal-faction");
				this._selectedFactionElement.AddToClassList("selected-faction");
				this._selectedFactionSpec = factionSpec;
				bool flag = this._factionUnlockingService.IsLocked(factionSpec);
				this._next.SetEnabled(!flag);
				this._unlockCondition.visible = flag;
				this._unlockCondition.text = this._factionUnlockConditionDescriber.Describe(factionSpec);
				int num = 352 + (this._factionSpecs.Length - 1) * 156;
				int num2 = this._factionList.IndexOf(factionElement);
				this._factionList.style.marginLeft = new StyleLength((float)(num - 352 - 156 * num2 * 2));
				if (playSound)
				{
					this._mainMenuSoundController.PlayFactionSelectedSound(factionSpec);
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003787 File Offset: 0x00001987
		public void OnNextButtonClicked(ClickEvent evt)
		{
			this.ShowNewGameMap();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003790 File Offset: 0x00001990
		public bool ShowNewGameMap()
		{
			if (this._selectedFactionSpec != null && !this._factionUnlockingService.IsLocked(this._selectedFactionSpec))
			{
				this._newGameMapPanel.Open(this._selectedFactionSpec);
				return true;
			}
			return false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000037C7 File Offset: 0x000019C7
		public static void SetBackground(VisualElement factionElement, string name, Sprite sprite)
		{
			UQueryExtensions.Q<VisualElement>(factionElement, name, null).style.backgroundImage = new StyleBackground(sprite);
		}

		// Token: 0x0400004E RID: 78
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x0400004F RID: 79
		public readonly NewGameMapPanel _newGameMapPanel;

		// Token: 0x04000050 RID: 80
		public readonly PanelStack _panelStack;

		// Token: 0x04000051 RID: 81
		public readonly ILoc _loc;

		// Token: 0x04000052 RID: 82
		public readonly MainMenuSoundController _mainMenuSoundController;

		// Token: 0x04000053 RID: 83
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000054 RID: 84
		public readonly FactionUnlockingService _factionUnlockingService;

		// Token: 0x04000055 RID: 85
		public readonly FactionUnlockConditionDescriber _factionUnlockConditionDescriber;

		// Token: 0x04000056 RID: 86
		public VisualElement _root;

		// Token: 0x04000057 RID: 87
		public VisualElement _selectedFactionElement;

		// Token: 0x04000058 RID: 88
		public FactionSpec _selectedFactionSpec;

		// Token: 0x04000059 RID: 89
		public VisualElement _factionList;

		// Token: 0x0400005A RID: 90
		public ImmutableArray<FactionSpec> _factionSpecs;

		// Token: 0x0400005B RID: 91
		public Button _next;

		// Token: 0x0400005C RID: 92
		public Label _unlockCondition;
	}
}
