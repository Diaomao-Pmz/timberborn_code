using System;
using Timberborn.DropdownSystem;
using Timberborn.GameDistricts;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200000D RID: 13
	public class ManualMigrationDistrictDropdown
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000262A File Offset: 0x0000082A
		public ManualMigrationDistrictDropdown(DropdownItemsSetter dropdownItemsSetter, ManualMigrationDistrictDropdownProvider manualMigrationDistrictDropdownProvider, Dropdown dropdown)
		{
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._manualMigrationDistrictDropdownProvider = manualMigrationDistrictDropdownProvider;
			this._dropdown = dropdown;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002647 File Offset: 0x00000847
		public void SetDistrict(DistrictCenter selectedDistrict)
		{
			this._manualMigrationDistrictDropdownProvider.SetDistrict(selectedDistrict);
			this.UpdateDistricts();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000265B File Offset: 0x0000085B
		public void UpdateDistricts()
		{
			this._manualMigrationDistrictDropdownProvider.UpdateDistrictsList();
			this._dropdownItemsSetter.SetItems(this._dropdown, this._manualMigrationDistrictDropdownProvider);
		}

		// Token: 0x0400001F RID: 31
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000020 RID: 32
		public readonly ManualMigrationDistrictDropdownProvider _manualMigrationDistrictDropdownProvider;

		// Token: 0x04000021 RID: 33
		public readonly Dropdown _dropdown;

		// Token: 0x04000022 RID: 34
		public readonly Action<DistrictCenter> _districtChangedAction;
	}
}
