using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Forestry;
using UnityEngine.UIElements;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000009 RID: 9
	public class ForesterFragment : IEntityPanelFragment
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021BD File Offset: 0x000003BD
		public ForesterFragment(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CC File Offset: 0x000003CC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ForesterFragment");
			this._toggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._toggle, new EventCallback<ChangeEvent<bool>>(this.OnToggleValueChanged));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002230 File Offset: 0x00000430
		public void ShowFragment(BaseComponent entity)
		{
			this._forester = entity.GetComponent<Forester>();
			this.UpdateToggleState();
			this._root.ToggleDisplayStyle(this._forester);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000225A File Offset: 0x0000045A
		public void UpdateFragment()
		{
			this.UpdateToggleState();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002262 File Offset: 0x00000462
		public void ClearFragment()
		{
			this._forester = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002277 File Offset: 0x00000477
		public void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			this._forester.SetReplantDeadTrees(evt.newValue);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000228A File Offset: 0x0000048A
		public void UpdateToggleState()
		{
			if (this._forester)
			{
				this._toggle.SetValueWithoutNotify(this._forester.ReplantDeadTrees);
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000D RID: 13
		public VisualElement _root;

		// Token: 0x0400000E RID: 14
		public Toggle _toggle;

		// Token: 0x0400000F RID: 15
		public Forester _forester;
	}
}
