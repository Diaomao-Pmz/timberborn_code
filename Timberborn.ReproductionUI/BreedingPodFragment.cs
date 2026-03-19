using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InventorySystemUI;
using Timberborn.Reproduction;
using UnityEngine.UIElements;

namespace Timberborn.ReproductionUI
{
	// Token: 0x02000009 RID: 9
	public class BreedingPodFragment : IEntityPanelFragment
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000023D7 File Offset: 0x000005D7
		public BreedingPodFragment(VisualElementLoader visualElementLoader, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F0 File Offset: 0x000005F0
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/BreedingPodFragment");
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._root.ToggleDisplayStyle(false);
			VisualElement root = UQueryExtensions.Q<VisualElement>(this._root, "BreedingPodInventoryFragment", null);
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(root).ShowRowLimit().ShowEmptyRows().Build();
			return this._root;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000246F File Offset: 0x0000066F
		public void ShowFragment(BaseComponent entity)
		{
			this._breedingPod = entity.GetComponent<BreedingPod>();
			if (this._breedingPod)
			{
				this._inventoryFragment.ShowFragment(this._breedingPod.Inventory);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A0 File Offset: 0x000006A0
		public void ClearFragment()
		{
			this._breedingPod = null;
			this._inventoryFragment.ClearFragment();
			this.UpdateFragment();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024BC File Offset: 0x000006BC
		public void UpdateFragment()
		{
			if (this._breedingPod && this._breedingPod.Enabled)
			{
				this._progressBar.SetProgress(this._breedingPod.CalculateProgress());
				this._inventoryFragment.UpdateFragment();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000018 RID: 24
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000019 RID: 25
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x0400001A RID: 26
		public InventoryFragment _inventoryFragment;

		// Token: 0x0400001B RID: 27
		public BreedingPod _breedingPod;

		// Token: 0x0400001C RID: 28
		public VisualElement _root;

		// Token: 0x0400001D RID: 29
		public ProgressBar _progressBar;
	}
}
