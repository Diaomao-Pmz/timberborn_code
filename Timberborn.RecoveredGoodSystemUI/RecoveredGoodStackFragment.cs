using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.BuilderPrioritySystemUI;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InventorySystemUI;
using Timberborn.PrioritySystemUI;
using Timberborn.RecoveredGoodSystem;
using UnityEngine.UIElements;

namespace Timberborn.RecoveredGoodSystemUI
{
	// Token: 0x02000007 RID: 7
	public class RecoveredGoodStackFragment : IEntityPanelFragment
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023FA File Offset: 0x000005FA
		public RecoveredGoodStackFragment(BuilderPriorityToggleGroupFactory builderPriorityToggleGroupFactory, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory, VisualElementLoader visualElementLoader)
		{
			this._builderPriorityToggleGroupFactory = builderPriorityToggleGroupFactory;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002418 File Offset: 0x00000618
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/RecoveredGoodStackFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._priorityToggleGroup = this._builderPriorityToggleGroupFactory.Create(this._root, RecoveredGoodStackFragment.PriorityLabelLocKey);
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(this._root).Build();
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002488 File Offset: 0x00000688
		public void ShowFragment(BaseComponent entity)
		{
			this._recoveredGoodStack = entity.GetComponent<RecoveredGoodStack>();
			if (this._recoveredGoodStack)
			{
				BuilderPrioritizable component = entity.GetComponent<BuilderPrioritizable>();
				this._priorityToggleGroup.Enable(component);
				this._inventoryFragment.ShowFragment(this._recoveredGoodStack.Inventory);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024D7 File Offset: 0x000006D7
		public void ClearFragment()
		{
			this.Hide();
			this._recoveredGoodStack = null;
			this._priorityToggleGroup.Disable();
			this._inventoryFragment.ClearFragment();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024FC File Offset: 0x000006FC
		public void UpdateFragment()
		{
			if (this._recoveredGoodStack)
			{
				this._root.ToggleDisplayStyle(true);
				this._inventoryFragment.UpdateFragment();
				this._priorityToggleGroup.UpdateGroup();
				return;
			}
			this.Hide();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002534 File Offset: 0x00000734
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0400001B RID: 27
		public static readonly string PriorityLabelLocKey = "RecoveredGoodStack.Priority";

		// Token: 0x0400001C RID: 28
		public readonly BuilderPriorityToggleGroupFactory _builderPriorityToggleGroupFactory;

		// Token: 0x0400001D RID: 29
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x0400001E RID: 30
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001F RID: 31
		public RecoveredGoodStack _recoveredGoodStack;

		// Token: 0x04000020 RID: 32
		public InventoryFragment _inventoryFragment;

		// Token: 0x04000021 RID: 33
		public VisualElement _root;

		// Token: 0x04000022 RID: 34
		public PriorityToggleGroup _priorityToggleGroup;
	}
}
