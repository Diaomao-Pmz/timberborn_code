using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200001E RID: 30
	public class WaterMoverFragment : IEntityPanelFragment
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004FC7 File Offset: 0x000031C7
		public WaterMoverFragment(VisualElementLoader visualElementLoader, WaterMoverToggleFactory waterMoverToggleFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._waterMoverToggleFactory = waterMoverToggleFactory;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004FE0 File Offset: 0x000031E0
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/WaterMoverFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._waterMoverToggle = this._waterMoverToggleFactory.Create(this._root);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005030 File Offset: 0x00003230
		public void ShowFragment(BaseComponent entity)
		{
			WaterMover component = entity.GetComponent<WaterMover>();
			if (component != null)
			{
				this._waterMoverToggle.Show(component);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000505F File Offset: 0x0000325F
		public void ClearFragment()
		{
			this._waterMoverToggle.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005078 File Offset: 0x00003278
		public void UpdateFragment()
		{
			this._waterMoverToggle.Update();
		}

		// Token: 0x040000CC RID: 204
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000CD RID: 205
		public readonly WaterMoverToggleFactory _waterMoverToggleFactory;

		// Token: 0x040000CE RID: 206
		public VisualElement _root;

		// Token: 0x040000CF RID: 207
		public WaterMoverToggle _waterMoverToggle;
	}
}
