using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.WaterSourceSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x02000009 RID: 9
	public class WaterSourceRegulatorFragment : IEntityPanelFragment
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002523 File Offset: 0x00000723
		public WaterSourceRegulatorFragment(VisualElementLoader visualElementLoader, WaterSourceRegulatorToggleFactory waterSourceRegulatorToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._waterSourceRegulatorToggleFactory = waterSourceRegulatorToggleFactory;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000253C File Offset: 0x0000073C
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/WaterSourceRegulatorFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(this._root, "ModeToggle", null);
			Label label = UQueryExtensions.Q<Label>(this._root, "ModeLabel", null);
			this._waterSourceRegulatorToggle = this._waterSourceRegulatorToggleFactory.Create(parent, label);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025AC File Offset: 0x000007AC
		public void ShowFragment(BaseComponent entity)
		{
			WaterSourceRegulator component = entity.GetComponent<WaterSourceRegulator>();
			if (component != null)
			{
				this._waterSourceRegulatorToggle.Show(component);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025DB File Offset: 0x000007DB
		public void ClearFragment()
		{
			this._waterSourceRegulatorToggle.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025F4 File Offset: 0x000007F4
		public void UpdateFragment()
		{
			this._waterSourceRegulatorToggle.Update();
		}

		// Token: 0x0400001E RID: 30
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001F RID: 31
		public readonly WaterSourceRegulatorToggleFactory _waterSourceRegulatorToggleFactory;

		// Token: 0x04000020 RID: 32
		public VisualElement _root;

		// Token: 0x04000021 RID: 33
		public WaterSourceRegulatorToggle _waterSourceRegulatorToggle;
	}
}
