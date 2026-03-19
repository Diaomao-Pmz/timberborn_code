using System;
using Timberborn.Common;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200000F RID: 15
	public class DialogBoxShower
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000253F File Offset: 0x0000073F
		public DialogBoxShower(ILoc loc, PanelStack panelStack, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000255C File Offset: 0x0000075C
		public DialogBoxShower.Builder Create()
		{
			VisualElement root = this._visualElementLoader.LoadVisualElement("Core/DialogBox");
			return new DialogBoxShower.Builder(this, this._loc, this._panelStack, root);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000258D File Offset: 0x0000078D
		public void Show(DialogBox dialogBox, bool hideTop)
		{
			if (hideTop)
			{
				this._panelStack.HideAndPushDialog(dialogBox);
				return;
			}
			this._panelStack.PushDialog(dialogBox);
		}

		// Token: 0x04000018 RID: 24
		public readonly ILoc _loc;

		// Token: 0x04000019 RID: 25
		public readonly PanelStack _panelStack;

		// Token: 0x0400001A RID: 26
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x02000010 RID: 16
		public class Builder
		{
			// Token: 0x06000026 RID: 38 RVA: 0x000025AB File Offset: 0x000007AB
			public Builder(DialogBoxShower dialogBoxShower, ILoc loc, PanelStack panelStack, VisualElement root)
			{
				this._dialogBoxShower = dialogBoxShower;
				this._loc = loc;
				this._panelStack = panelStack;
				this._root = root;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x000025E2 File Offset: 0x000007E2
			public DialogBoxShower.Builder SetMessage(string text)
			{
				UQueryExtensions.Q<Label>(this._root, "Message", null).text = text;
				return this;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x000025FC File Offset: 0x000007FC
			public DialogBoxShower.Builder SetLocalizedMessage(string locKey)
			{
				return this.SetMessage(this._loc.T(locKey));
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002610 File Offset: 0x00000810
			public DialogBoxShower.Builder SetConfirmButton(Action confirmAction)
			{
				this._confirmAction = confirmAction;
				return this;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x0000261A File Offset: 0x0000081A
			public DialogBoxShower.Builder SetConfirmButton(Action confirmAction, string confirmText)
			{
				this._confirmText = confirmText;
				return this.SetConfirmButton(confirmAction);
			}

			// Token: 0x0600002B RID: 43 RVA: 0x0000262A File Offset: 0x0000082A
			public DialogBoxShower.Builder SetCancelButton(Action cancelAction)
			{
				this._cancelAction = cancelAction;
				return this;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002634 File Offset: 0x00000834
			public DialogBoxShower.Builder SetCancelButton(Action cancelAction, string cancelText)
			{
				this._cancelText = cancelText;
				return this.SetCancelButton(cancelAction);
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002644 File Offset: 0x00000844
			public DialogBoxShower.Builder SetDefaultCancelButton()
			{
				return this.SetCancelButton(new Action(DialogBoxShower.Builder.EmptyCallback));
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002658 File Offset: 0x00000858
			public DialogBoxShower.Builder SetDefaultCancelButton(string cancelText)
			{
				return this.SetCancelButton(new Action(DialogBoxShower.Builder.EmptyCallback), cancelText);
			}

			// Token: 0x0600002F RID: 47 RVA: 0x0000266D File Offset: 0x0000086D
			public DialogBoxShower.Builder SetInfoButton(Action infoAction, string infoText)
			{
				this._infoAction = infoAction;
				this._infoText = infoText;
				return this;
			}

			// Token: 0x06000030 RID: 48 RVA: 0x0000267E File Offset: 0x0000087E
			public DialogBoxShower.Builder SetOffset(Vector2Int offsetInPixels)
			{
				DialogBoxShower.Builder.OffsetBox(this._root, offsetInPixels);
				return this;
			}

			// Token: 0x06000031 RID: 49 RVA: 0x0000268D File Offset: 0x0000088D
			public DialogBoxShower.Builder SetMaxWidth(int maxWidth)
			{
				UQueryExtensions.Q<VisualElement>(this._root, "Box", null).style.maxWidth = (float)maxWidth;
				return this;
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000026B2 File Offset: 0x000008B2
			public DialogBoxShower.Builder AddContent(VisualElement content)
			{
				UQueryExtensions.Q<VisualElement>(this._root, "Content", null).Add(content);
				return this;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x000026CC File Offset: 0x000008CC
			public DialogBox Show()
			{
				DialogBox dialogBox = this.CreateDialogBox();
				this._dialogBoxShower.Show(dialogBox, false);
				return dialogBox;
			}

			// Token: 0x06000034 RID: 52 RVA: 0x000026EE File Offset: 0x000008EE
			public void HideTopAndShow()
			{
				this._dialogBoxShower.Show(this.CreateDialogBox(), true);
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00002704 File Offset: 0x00000904
			public DialogBox CreateDialogBox()
			{
				DialogBox dialogBox = new DialogBox(this._panelStack, this._confirmAction, this._cancelAction, this._root);
				this.SetupButtons(dialogBox);
				return dialogBox;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002738 File Offset: 0x00000938
			public void SetupButtons(DialogBox dialogBox)
			{
				Button confirmButton = UQueryExtensions.Q<Button>(this._root, "ConfirmButton", null);
				Button cancelButton = UQueryExtensions.Q<Button>(this._root, "CancelButton", null);
				Button infoButton = UQueryExtensions.Q<Button>(this._root, "InfoButton", null);
				this.SetupButtonsText(confirmButton, cancelButton, infoButton);
				this.SetupButtonsActions(dialogBox, confirmButton, cancelButton, infoButton);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002790 File Offset: 0x00000990
			public void SetupButtonsText(Button confirmButton, Button cancelButton, Button infoButton)
			{
				if (this._confirmText == null)
				{
					string key = (this._cancelAction == null) ? CommonLocKeys.OKKey : CommonLocKeys.YesKey;
					this._confirmText = this._loc.T(key);
				}
				confirmButton.text = this._confirmText;
				if (this._cancelAction != null)
				{
					if (this._cancelText == null)
					{
						this._cancelText = this._loc.T(CommonLocKeys.NoKey);
					}
					cancelButton.text = this._cancelText;
				}
				if (!string.IsNullOrEmpty(this._infoText))
				{
					infoButton.text = this._infoText;
				}
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002824 File Offset: 0x00000A24
			public void SetupButtonsActions(DialogBox dialogBox, Button confirmButton, Button cancelButton, Button infoButton)
			{
				confirmButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					dialogBox.OnUIConfirmed();
				}, 0);
				if (this._cancelAction != null)
				{
					cancelButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
					{
						dialogBox.OnUICancelled();
					}, 0);
				}
				else
				{
					cancelButton.ToggleDisplayStyle(false);
				}
				if (this._infoAction != null)
				{
					infoButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
					{
						this._infoAction();
					}, 0);
					return;
				}
				infoButton.ToggleDisplayStyle(false);
			}

			// Token: 0x06000039 RID: 57 RVA: 0x000028A1 File Offset: 0x00000AA1
			public static void EmptyCallback()
			{
			}

			// Token: 0x0600003A RID: 58 RVA: 0x000028A4 File Offset: 0x00000AA4
			public static void OffsetBox(VisualElement box, Vector2Int offsetInPixels)
			{
				IStyle style = box.style;
				int x = offsetInPixels.x;
				if (x < 0)
				{
					style.right = (float)Math.Abs(x);
				}
				else if (x > 0)
				{
					style.left = (float)x;
				}
				int y = offsetInPixels.y;
				if (y < 0)
				{
					style.top = (float)Math.Abs(y);
					return;
				}
				if (y > 0)
				{
					style.bottom = (float)y;
				}
			}

			// Token: 0x0400001B RID: 27
			public readonly DialogBoxShower _dialogBoxShower;

			// Token: 0x0400001C RID: 28
			public readonly ILoc _loc;

			// Token: 0x0400001D RID: 29
			public readonly PanelStack _panelStack;

			// Token: 0x0400001E RID: 30
			public readonly VisualElement _root;

			// Token: 0x0400001F RID: 31
			public Action _confirmAction = new Action(DialogBoxShower.Builder.EmptyCallback);

			// Token: 0x04000020 RID: 32
			public Action _cancelAction;

			// Token: 0x04000021 RID: 33
			public Action _infoAction;

			// Token: 0x04000022 RID: 34
			public string _confirmText;

			// Token: 0x04000023 RID: 35
			public string _cancelText;

			// Token: 0x04000024 RID: 36
			public string _infoText;
		}
	}
}
