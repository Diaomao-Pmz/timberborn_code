using System;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameStartup;
using Timberborn.GameWonderCompletion;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.SettlementNameSystemUI
{
	// Token: 0x02000005 RID: 5
	public class SettlementNameBoxShower : ISettlementNamePromptShower
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021EE File Offset: 0x000003EE
		public SettlementNameBoxShower(PanelStack panelStack, VisualElementLoader visualElementLoader, GameSaveRepository gameSaveRepository, DialogBoxShower dialogBoxShower, GameWonderCompletionService gameWonderCompletionService, EventBus eventBus, InputService inputService)
		{
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._gameSaveRepository = gameSaveRepository;
			this._dialogBoxShower = dialogBoxShower;
			this._gameWonderCompletionService = gameWonderCompletionService;
			this._eventBus = eventBus;
			this._inputService = inputService;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000222C File Offset: 0x0000042C
		public void PromptDisallowingCancelling(bool includeResetStartLocationLink)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/SettlementNameBox");
			SettlementNameBox settlementNameBox = new SettlementNameBox(this._panelStack, this._gameSaveRepository, this._dialogBoxShower, delegate(string settlementName)
			{
				this._eventBus.Post(new SettlementNameChangedEvent(settlementName));
			}, visualElement, this._initialSettlementName);
			TextField textField = UQueryExtensions.Q<TextField>(visualElement, "Input", null);
			textField.maxLength = SettlementNameBoxShower.CharacterLimit;
			UQueryExtensions.Q<TextElement>(textField, null, null).RegisterCallback<FocusOutEvent>(delegate(FocusOutEvent _)
			{
				if (this._inputService.WasConfirmPressedLastFrame)
				{
					settlementNameBox.OnUIConfirmed();
				}
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "ConfirmButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				settlementNameBox.OnUIConfirmed();
			}, 0);
			Button button = UQueryExtensions.Q<Button>(visualElement, "RelocateButton", null);
			button.ToggleDisplayStyle(this._gameWonderCompletionService.IsWonderCompletedWithAnyFaction());
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnRelocateClicked(settlementNameBox);
			}, 0);
			Button button2 = UQueryExtensions.Q<Button>(visualElement, "ResetStartLocation", null);
			button2.ToggleDisplayStyle(includeResetStartLocationLink);
			button2.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnResetLocationClicked(settlementNameBox);
			}, 0);
			this._panelStack.PushOverlay(settlementNameBox);
			textField.Focus();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002340 File Offset: 0x00000540
		public void OnResetLocationClicked(IPanelController settlementNameBox)
		{
			this._panelStack.Pop(settlementNameBox);
			this._eventBus.Post(new ResetStartingLocationEvent());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000235E File Offset: 0x0000055E
		public void OnRelocateClicked(SettlementNameBox settlementNameBox)
		{
			this._initialSettlementName = settlementNameBox.SettlementName;
			this._panelStack.Pop(settlementNameBox);
			this._eventBus.Post(new RelocateSettlementEvent());
		}

		// Token: 0x0400000F RID: 15
		public static readonly int CharacterLimit = 50;

		// Token: 0x04000010 RID: 16
		public readonly PanelStack _panelStack;

		// Token: 0x04000011 RID: 17
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000012 RID: 18
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000013 RID: 19
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000014 RID: 20
		public readonly GameWonderCompletionService _gameWonderCompletionService;

		// Token: 0x04000015 RID: 21
		public readonly EventBus _eventBus;

		// Token: 0x04000016 RID: 22
		public readonly InputService _inputService;

		// Token: 0x04000017 RID: 23
		public string _initialSettlementName;
	}
}
