using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Reproduction;
using UnityEngine.UIElements;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x0200000A RID: 10
	public class DwellingDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000024D2 File Offset: 0x000006D2
		public DwellingDebugFragment(DebugFragmentFactory debugFragmentFactory, NewbornSpawner newbornSpawner)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._newbornSpawner = newbornSpawner;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024E8 File Offset: 0x000006E8
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.SpawnNewborn), "Spawn newborn");
			this._root = this._debugFragmentFactory.Create(debugFragmentButton);
			this._button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			return this._root;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000253C File Offset: 0x0000073C
		public void ShowFragment(BaseComponent entity)
		{
			this._dwelling = entity.GetComponent<Dwelling>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000254A File Offset: 0x0000074A
		public void ClearFragment()
		{
			this._dwelling = null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002554 File Offset: 0x00000754
		public void UpdateFragment()
		{
			if (this._dwelling && this._dwelling.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				this._button.SetEnabled(this._dwelling.HasFreeSlots);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025AA File Offset: 0x000007AA
		public void SpawnNewborn()
		{
			if (this._dwelling && this._dwelling.Enabled && this._dwelling.HasFreeSlots)
			{
				this._newbornSpawner.SpawnChild(this._dwelling);
			}
		}

		// Token: 0x0400001C RID: 28
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x0400001D RID: 29
		public readonly NewbornSpawner _newbornSpawner;

		// Token: 0x0400001E RID: 30
		public Dwelling _dwelling;

		// Token: 0x0400001F RID: 31
		public VisualElement _root;

		// Token: 0x04000020 RID: 32
		public Button _button;
	}
}
