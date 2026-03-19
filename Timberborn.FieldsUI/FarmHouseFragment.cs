using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Fields;
using UnityEngine.UIElements;

namespace Timberborn.FieldsUI
{
	// Token: 0x02000006 RID: 6
	public class FarmHouseFragment : IEntityPanelFragment
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002147 File Offset: 0x00000347
		public FarmHouseFragment(VisualElementLoader visualElementLoader, FarmHouseToggleFactory farmHouseToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._farmHouseToggleFactory = farmHouseToggleFactory;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00000360
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/FarmHouseFragment");
			this._farmHouseToggle = this._farmHouseToggleFactory.Create(this._root);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AC File Offset: 0x000003AC
		public void ShowFragment(BaseComponent entity)
		{
			FarmHouse component = entity.GetComponent<FarmHouse>();
			if (component != null)
			{
				this._farmHouseToggle.Show(component);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DB File Offset: 0x000003DB
		public void ClearFragment()
		{
			this._farmHouseToggle.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F4 File Offset: 0x000003F4
		public void UpdateFragment()
		{
			this._farmHouseToggle.Update();
		}

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly FarmHouseToggleFactory _farmHouseToggleFactory;

		// Token: 0x0400000C RID: 12
		public FarmHouseToggle _farmHouseToggle;

		// Token: 0x0400000D RID: 13
		public VisualElement _root;
	}
}
