using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Planting;
using UnityEngine.UIElements;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000015 RID: 21
	public class PlantablePrioritizerFragment : IEntityPanelFragment
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002FBC File Offset: 0x000011BC
		public PlantablePrioritizerFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FD4 File Offset: 0x000011D4
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/PlantablePrioritizerFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._dropdown = UQueryExtensions.Q<Dropdown>(this._root, "Priorities", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003024 File Offset: 0x00001224
		public void ShowFragment(BaseComponent entity)
		{
			this._plantablePrioritizer = entity.GetComponent<PlantablePrioritizer>();
			if (this._plantablePrioritizer)
			{
				PlantablePrioritizerDropdownProvider component = entity.GetComponent<PlantablePrioritizerDropdownProvider>();
				if (component != null && component.HasMultipleOptions)
				{
					this._dropdownItemsSetter.SetLocalizableItems(this._dropdown, component);
					this._plantablePrioritizer.PrioritizedPlantableChanged += this.OnPrioritizedPlantableChanged;
					this._root.ToggleDisplayStyle(true);
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003094 File Offset: 0x00001294
		public void ClearFragment()
		{
			if (this._plantablePrioritizer)
			{
				this._plantablePrioritizer.PrioritizedPlantableChanged -= this.OnPrioritizedPlantableChanged;
			}
			this._dropdown.ClearItems();
			this._plantablePrioritizer = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000030E3 File Offset: 0x000012E3
		public void UpdateFragment()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030E5 File Offset: 0x000012E5
		public void OnPrioritizedPlantableChanged(object sender, EventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x04000040 RID: 64
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000041 RID: 65
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000042 RID: 66
		public PlantablePrioritizer _plantablePrioritizer;

		// Token: 0x04000043 RID: 67
		public Dropdown _dropdown;

		// Token: 0x04000044 RID: 68
		public VisualElement _root;
	}
}
