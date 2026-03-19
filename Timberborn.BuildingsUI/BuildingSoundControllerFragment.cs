using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.BuildingsUI
{
	// Token: 0x0200000B RID: 11
	public class BuildingSoundControllerFragment : IEntityPanelFragment
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002841 File Offset: 0x00000A41
		public BuildingSoundControllerFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002850 File Offset: 0x00000A50
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/BuildingSoundControllerFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._toggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._toggle, new EventCallback<ChangeEvent<bool>>(this.ToggleSound));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028B8 File Offset: 0x00000AB8
		public void ShowFragment(BaseComponent entity)
		{
			BuildingSoundController component = entity.GetComponent<BuildingSoundController>();
			if (component != null)
			{
				this._buildingSoundController = component;
				this._root.ToggleDisplayStyle(true);
				this._toggle.SetValueWithoutNotify(component.PlaySound);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002900 File Offset: 0x00000B00
		public void ClearFragment()
		{
			this._buildingSoundController = null;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002909 File Offset: 0x00000B09
		public void UpdateFragment()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000290B File Offset: 0x00000B0B
		public void ToggleSound(ChangeEvent<bool> evt)
		{
			if (evt.newValue)
			{
				this._buildingSoundController.EnableSound();
				return;
			}
			this._buildingSoundController.DisableSound();
		}

		// Token: 0x04000028 RID: 40
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000029 RID: 41
		public VisualElement _root;

		// Token: 0x0400002A RID: 42
		public Toggle _toggle;

		// Token: 0x0400002B RID: 43
		public BuildingSoundController _buildingSoundController;
	}
}
