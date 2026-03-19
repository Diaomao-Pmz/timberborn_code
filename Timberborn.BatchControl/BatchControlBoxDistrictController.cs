using System;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityNaming;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000005 RID: 5
	public class BatchControlBoxDistrictController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002346 File Offset: 0x00000546
		public BatchControlBoxDistrictController(BatchControlBoxTabController batchControlBoxTabController, BatchControlDistrict batchControlDistrict, DistrictCenterRegistry districtCenterRegistry, DistrictContextService districtContextService, DistrictDropdownProvider districtDropdownProvider, DropdownItemsSetter dropdownItemsSetter, EventBus eventBus)
		{
			this._batchControlBoxTabController = batchControlBoxTabController;
			this._batchControlDistrict = batchControlDistrict;
			this._districtCenterRegistry = districtCenterRegistry;
			this._districtContextService = districtContextService;
			this._districtDropdownProvider = districtDropdownProvider;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._eventBus = eventBus;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002383 File Offset: 0x00000583
		public void Initialize(VisualElement root)
		{
			this._root = root;
			this._dropdown = UQueryExtensions.Q<Dropdown>(this._root, "DistrictDropdown", null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023A3 File Offset: 0x000005A3
		public void Show()
		{
			this._batchControlDistrict.SetDistrict(this._districtContextService.SelectedDistrict);
			this.UpdateDropdown();
			this._eventBus.Register(this);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023CD File Offset: 0x000005CD
		public void Clear()
		{
			this._dropdown.ClearItems();
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E8 File Offset: 0x000005E8
		[OnEvent]
		public void OnBatchControlTabShown(BatchControlTabShownEvent batchControlTabShownEvent)
		{
			bool visible = !batchControlTabShownEvent.BatchControlTab.IgnoreDistrictSelection;
			this._dropdown.ToggleDisplayStyle(visible);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002410 File Offset: 0x00000610
		[OnEvent]
		public void OnDistrictCenterRegistryChanged(DistrictCenterRegistryChangedEvent districtCenterRegistryChangedEvent)
		{
			if (this._batchControlDistrict.SelectedDistrict && !this._districtCenterRegistry.FinishedDistrictCenters.Contains(this._batchControlDistrict.SelectedDistrict))
			{
				this._batchControlDistrict.SetDistrict(null);
			}
			this.UpdateDropdown();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002461 File Offset: 0x00000661
		[OnEvent]
		public void OnBatchControlDistrictChanged(BatchControlDistrictChangedEvent batchControlDistrictChangedEvent)
		{
			BatchControlTab currentTab = this._batchControlBoxTabController.CurrentTab;
			if (currentTab != null)
			{
				currentTab.UpdateRowsVisibility();
			}
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002484 File Offset: 0x00000684
		[OnEvent]
		public void OnEntityNameChanged(EntityNameChangedEvent entityNameChangedEvent)
		{
			if (entityNameChangedEvent.Entity.GetComponent<DistrictCenter>())
			{
				this.UpdateDropdown();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000249E File Offset: 0x0000069E
		public void UpdateDropdown()
		{
			this._districtDropdownProvider.UpdateDistrictsList();
			this._dropdownItemsSetter.SetItems(this._dropdown, this._districtDropdownProvider);
		}

		// Token: 0x04000012 RID: 18
		public readonly BatchControlBoxTabController _batchControlBoxTabController;

		// Token: 0x04000013 RID: 19
		public readonly BatchControlDistrict _batchControlDistrict;

		// Token: 0x04000014 RID: 20
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000015 RID: 21
		public readonly DistrictContextService _districtContextService;

		// Token: 0x04000016 RID: 22
		public readonly DistrictDropdownProvider _districtDropdownProvider;

		// Token: 0x04000017 RID: 23
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public VisualElement _root;

		// Token: 0x0400001A RID: 26
		public Dropdown _dropdown;
	}
}
