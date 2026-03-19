using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000011 RID: 17
	public class GateFragment : IEntityPanelFragment
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000031DE File Offset: 0x000013DE
		public GateFragment(VisualElementLoader visualElementLoader, GateToggleFactory gateToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._gateToggleFactory = gateToggleFactory;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000031F4 File Offset: 0x000013F4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/GateFragment");
			VisualElement parent = UQueryExtensions.Q<VisualElement>(this._root, "ModeToggle", null);
			Label label = UQueryExtensions.Q<Label>(this._root, "ModeLabel", null);
			this._gateToggle = this._gateToggleFactory.Create(parent, label);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003260 File Offset: 0x00001460
		public void ShowFragment(BaseComponent entity)
		{
			Gate component = entity.GetComponent<Gate>();
			if (component != null)
			{
				this._gateToggle.Show(component);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000328F File Offset: 0x0000148F
		public void ClearFragment()
		{
			this._gateToggle.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000032A8 File Offset: 0x000014A8
		public void UpdateFragment()
		{
			this._gateToggle.Update();
		}

		// Token: 0x04000060 RID: 96
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000061 RID: 97
		public readonly GateToggleFactory _gateToggleFactory;

		// Token: 0x04000062 RID: 98
		public VisualElement _root;

		// Token: 0x04000063 RID: 99
		public GateToggle _gateToggle;
	}
}
