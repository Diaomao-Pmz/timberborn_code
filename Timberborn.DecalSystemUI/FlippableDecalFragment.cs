using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DecalSystem;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.DecalSystemUI
{
	// Token: 0x0200000A RID: 10
	public class FlippableDecalFragment : IEntityPanelFragment
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002696 File Offset: 0x00000896
		public FlippableDecalFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026A8 File Offset: 0x000008A8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/FlippableDecalFragment");
			this._root.ToggleDisplayStyle(false);
			this._toggle = UQueryExtensions.Q<Toggle>(this._root, "Flip", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._toggle, delegate(ChangeEvent<bool> evt)
			{
				this._flippableDecal.SetFlip(evt.newValue);
			});
			return this._root;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000270C File Offset: 0x0000090C
		public void ShowFragment(BaseComponent entity)
		{
			FlippableDecal component = entity.GetComponent<FlippableDecal>();
			if (component != null)
			{
				this._flippableDecal = component;
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002736 File Offset: 0x00000936
		public void ClearFragment()
		{
			this._flippableDecal = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000274B File Offset: 0x0000094B
		public void UpdateFragment()
		{
			if (this._flippableDecal)
			{
				this._toggle.SetValueWithoutNotify(this._flippableDecal.IsFlipped);
			}
		}

		// Token: 0x0400001E RID: 30
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001F RID: 31
		public VisualElement _root;

		// Token: 0x04000020 RID: 32
		public Toggle _toggle;

		// Token: 0x04000021 RID: 33
		public FlippableDecal _flippableDecal;
	}
}
