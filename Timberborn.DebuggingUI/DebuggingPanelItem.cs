using System;
using Timberborn.CoreUI;
using Timberborn.SettingsSystem;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000005 RID: 5
	public class DebuggingPanelItem
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002257 File Offset: 0x00000457
		public DebuggingPanelItem(ISettings settings, IDebuggingPanel debuggingPanel, VisualElement root, string title)
		{
			this._settings = settings;
			this._debuggingPanel = debuggingPanel;
			this._root = root;
			this._title = title;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000227C File Offset: 0x0000047C
		public void Initialize()
		{
			UQueryExtensions.Q<Label>(this._root, "Title", null).text = this._title;
			this._infoLabel = UQueryExtensions.Q<Label>(this._root, "Info", null);
			this._showButton = UQueryExtensions.Q<Button>(this._root, "Show", null);
			this._showButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.TogglePanelVisibility(true);
			}, 0);
			this._hideButton = UQueryExtensions.Q<Button>(this._root, "Hide", null);
			this._hideButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.TogglePanelVisibility(false);
			}, 0);
			this._isVisible = this._settings.GetBool(DebuggingPanelItem.GetKey(this._title), false);
			this.UpdateElementsVisibility();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002340 File Offset: 0x00000540
		public void UpdateText()
		{
			if (this._isVisible)
			{
				string text = this._debuggingPanel.GetText();
				if (text != null)
				{
					this._infoLabel.text = text;
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002370 File Offset: 0x00000570
		public void TogglePanelVisibility(bool isVisible)
		{
			ISettings settings = this._settings;
			string key = DebuggingPanelItem.GetKey(this._title);
			this._isVisible = isVisible;
			settings.SetBool(key, isVisible);
			this.UpdateElementsVisibility();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023A3 File Offset: 0x000005A3
		public void UpdateElementsVisibility()
		{
			this._showButton.ToggleDisplayStyle(!this._isVisible);
			this._hideButton.ToggleDisplayStyle(this._isVisible);
			this._infoLabel.ToggleDisplayStyle(this._isVisible);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023DB File Offset: 0x000005DB
		public static string GetKey(string title)
		{
			return "DebuggingPanel." + title;
		}

		// Token: 0x0400000F RID: 15
		public readonly ISettings _settings;

		// Token: 0x04000010 RID: 16
		public readonly IDebuggingPanel _debuggingPanel;

		// Token: 0x04000011 RID: 17
		public readonly VisualElement _root;

		// Token: 0x04000012 RID: 18
		public readonly string _title;

		// Token: 0x04000013 RID: 19
		public Label _infoLabel;

		// Token: 0x04000014 RID: 20
		public Button _showButton;

		// Token: 0x04000015 RID: 21
		public Button _hideButton;

		// Token: 0x04000016 RID: 22
		public bool _isVisible;
	}
}
