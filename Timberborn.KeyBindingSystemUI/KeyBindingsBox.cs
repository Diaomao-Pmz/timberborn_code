using System;
using System.Collections.Immutable;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.KeyBindingSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x0200000B RID: 11
	public class KeyBindingsBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027AF File Offset: 0x000009AF
		public KeyBindingsBox(DialogBoxShower dialogBoxShower, KeyBindingSpecService keyBindingSpecService, KeyBindingRowFactory keyBindingRowFactory, PanelStack panelStack, VisualElementLoader visualElementLoader, DevModeManager devModeManager)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._keyBindingSpecService = keyBindingSpecService;
			this._keyBindingRowFactory = keyBindingRowFactory;
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._devModeManager = devModeManager;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027E4 File Offset: 0x000009E4
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/KeyBindingsBox");
			this._content = UQueryExtensions.Q<ScrollView>(this._root, "Content", null);
			this._keyBindingGroups = this._keyBindingRowFactory.CreateAll().ToImmutableArray<KeyBindingGroup>();
			foreach (KeyBindingGroup keyBindingGroup in this._keyBindingGroups)
			{
				this._content.Add(keyBindingGroup.Root);
			}
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "ResetToDefault", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ShowResetDialogBox), 0);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028AA File Offset: 0x00000AAA
		public VisualElement GetPanel()
		{
			this.UpdateGroupsVisibility();
			return this._root;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028B8 File Offset: 0x00000AB8
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028BB File Offset: 0x00000ABB
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void UpdateGroupsVisibility()
		{
			foreach (KeyBindingGroup keyBindingGroup in this._keyBindingGroups)
			{
				bool visible = !keyBindingGroup.IsHidden || this._devModeManager.Enabled;
				keyBindingGroup.Root.ToggleDisplayStyle(visible);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002911 File Offset: 0x00000B11
		public void ShowResetDialogBox(ClickEvent evt)
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(KeyBindingsBox.ResetToDefaultMessageLocKey).SetConfirmButton(new Action(this.ResetToDefault)).SetDefaultCancelButton().Show();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002944 File Offset: 0x00000B44
		public void ResetToDefault()
		{
			this._keyBindingSpecService.ResetToDefault();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002951 File Offset: 0x00000B51
		public void Close()
		{
			this._content.scrollOffset = Vector2.zero;
			this._panelStack.Pop(this);
		}

		// Token: 0x0400001C RID: 28
		public static readonly string ResetToDefaultMessageLocKey = "KeyBindingBox.ResetToDefaultMessage";

		// Token: 0x0400001D RID: 29
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400001E RID: 30
		public readonly KeyBindingSpecService _keyBindingSpecService;

		// Token: 0x0400001F RID: 31
		public readonly KeyBindingRowFactory _keyBindingRowFactory;

		// Token: 0x04000020 RID: 32
		public readonly PanelStack _panelStack;

		// Token: 0x04000021 RID: 33
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000022 RID: 34
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000023 RID: 35
		public VisualElement _root;

		// Token: 0x04000024 RID: 36
		public ScrollView _content;

		// Token: 0x04000025 RID: 37
		public ImmutableArray<KeyBindingGroup> _keyBindingGroups;
	}
}
