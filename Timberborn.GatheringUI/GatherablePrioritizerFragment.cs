using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Gathering;
using UnityEngine.UIElements;

namespace Timberborn.GatheringUI
{
	// Token: 0x0200000A RID: 10
	public class GatherablePrioritizerFragment : IEntityPanelFragment
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000274A File Offset: 0x0000094A
		public GatherablePrioritizerFragment(VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter)
		{
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002760 File Offset: 0x00000960
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/GatherablePrioritizerFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._dropdown = UQueryExtensions.Q<Dropdown>(this._root, "Priorities", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027B0 File Offset: 0x000009B0
		public void ShowFragment(BaseComponent entity)
		{
			this._gatherablePrioritizer = entity.GetComponent<GatherablePrioritizer>();
			if (this._gatherablePrioritizer)
			{
				GatherablePrioritizerDropdownProvider component = entity.GetComponent<GatherablePrioritizerDropdownProvider>();
				if (component != null && component.HasMultipleOptions)
				{
					this._dropdownItemsSetter.SetItems(this._dropdown, component);
					this._gatherablePrioritizer.PrioritizedGatherableChanged += this.OnPrioritizedGatherableChanged;
					this._root.ToggleDisplayStyle(true);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002820 File Offset: 0x00000A20
		public void ClearFragment()
		{
			if (this._gatherablePrioritizer)
			{
				this._gatherablePrioritizer.PrioritizedGatherableChanged -= this.OnPrioritizedGatherableChanged;
			}
			this._dropdown.ClearItems();
			this._gatherablePrioritizer = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000286F File Offset: 0x00000A6F
		public void UpdateFragment()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002871 File Offset: 0x00000A71
		public void OnPrioritizedGatherableChanged(object sender, EventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400002C RID: 44
		public GatherablePrioritizer _gatherablePrioritizer;

		// Token: 0x0400002D RID: 45
		public Dropdown _dropdown;

		// Token: 0x0400002E RID: 46
		public VisualElement _root;
	}
}
