using System;
using Timberborn.InputSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000019 RID: 25
	public class InputBoxShower
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000303F File Offset: 0x0000123F
		public InputBoxShower(ILoc loc, PanelStack panelStack, VisualElementLoader visualElementLoader, InputService inputService)
		{
			this._loc = loc;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._inputService = inputService;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003064 File Offset: 0x00001264
		public InputBoxShower.Builder Create()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Core/InputBox");
			TextField input = UQueryExtensions.Q<TextField>(visualElement, "Input", null);
			return new InputBoxShower.Builder(this, this._loc, this._panelStack, this._inputService, visualElement, input);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030A9 File Offset: 0x000012A9
		public void Show(InputBox inputBox)
		{
			this._panelStack.PushDialog(inputBox);
		}

		// Token: 0x04000037 RID: 55
		public static readonly int CharacterLimit = 24;

		// Token: 0x04000038 RID: 56
		public readonly ILoc _loc;

		// Token: 0x04000039 RID: 57
		public readonly PanelStack _panelStack;

		// Token: 0x0400003A RID: 58
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003B RID: 59
		public readonly InputService _inputService;

		// Token: 0x0200001A RID: 26
		public class Builder
		{
			// Token: 0x0600006E RID: 110 RVA: 0x000030C0 File Offset: 0x000012C0
			public Builder(InputBoxShower inputBoxShower, ILoc loc, PanelStack panelStack, InputService inputService, VisualElement root, TextField input)
			{
				this._inputBoxShower = inputBoxShower;
				this._loc = loc;
				this._panelStack = panelStack;
				this._inputService = inputService;
				this._root = root;
				this._input = input;
			}

			// Token: 0x0600006F RID: 111 RVA: 0x00003125 File Offset: 0x00001325
			public InputBoxShower.Builder SetDefaultValue(string value)
			{
				this._input.value = value;
				return this;
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00003134 File Offset: 0x00001334
			public InputBoxShower.Builder SetLocalizedMessage(string locKey)
			{
				UQueryExtensions.Q<Label>(this._root, "Message", null).text = this._loc.T(locKey);
				return this;
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00003159 File Offset: 0x00001359
			public InputBoxShower.Builder SetConfirmButton(Action<string> confirmAction)
			{
				this._confirmAction = confirmAction;
				return this;
			}

			// Token: 0x06000072 RID: 114 RVA: 0x00003164 File Offset: 0x00001364
			public void Show()
			{
				this._input.maxLength = InputBoxShower.CharacterLimit;
				InputBox inputBox = new InputBox(this._panelStack, this._confirmAction, this._root, this._input);
				UQueryExtensions.Q<TextElement>(this._input, null, null).SetConfirmCancelActions(this._inputService, delegate
				{
					inputBox.OnUIConfirmed();
				}, delegate
				{
					inputBox.OnUICancelled();
				});
				UQueryExtensions.Q<Button>(this._root, "ConfirmButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					inputBox.OnUIConfirmed();
				}, 0);
				UQueryExtensions.Q<Button>(this._root, "CancelButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					inputBox.OnUICancelled();
				}, 0);
				this._inputBoxShower.Show(inputBox);
				this._input.Focus();
			}

			// Token: 0x0400003C RID: 60
			public readonly InputBoxShower _inputBoxShower;

			// Token: 0x0400003D RID: 61
			public readonly ILoc _loc;

			// Token: 0x0400003E RID: 62
			public readonly PanelStack _panelStack;

			// Token: 0x0400003F RID: 63
			public readonly InputService _inputService;

			// Token: 0x04000040 RID: 64
			public readonly VisualElement _root;

			// Token: 0x04000041 RID: 65
			public readonly TextField _input;

			// Token: 0x04000042 RID: 66
			public Action<string> _confirmAction = delegate(string _)
			{
			};
		}
	}
}
