using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.SelectionSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200000B RID: 11
	public class ManualMigrationDistrictColumnFactory
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000247B File Offset: 0x0000067B
		public ManualMigrationDistrictColumnFactory(DistrictCenterRegistry districtCenterRegistry, DropdownItemsSetter dropdownItemsSetter, ManualMigrationDistrictSetter manualMigrationDistrictSetter, ManualMigrationPopulationRowFactory manualMigrationPopulationRowFactory, EntitySelectionService entitySelectionService, VisualElementLoader visualElementLoader)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
			this._manualMigrationPopulationRowFactory = manualMigrationPopulationRowFactory;
			this._entitySelectionService = entitySelectionService;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024B0 File Offset: 0x000006B0
		public ManualMigrationDistrictColumn CreateLeftColumn(VisualElement parent)
		{
			return this.BindUIAndCreateColumn(parent, this._manualMigrationPopulationRowFactory.CreateLeftRows(), new Action<DistrictCenter>(this._manualMigrationDistrictSetter.SetLeftDistrict));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024D5 File Offset: 0x000006D5
		public ManualMigrationDistrictColumn CreateRightColumn(VisualElement parent)
		{
			return this.BindUIAndCreateColumn(parent, this._manualMigrationPopulationRowFactory.CreateRightRows(), new Action<DistrictCenter>(this._manualMigrationDistrictSetter.SetRightDistrict));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024FC File Offset: 0x000006FC
		public ManualMigrationDistrictColumn BindUIAndCreateColumn(VisualElement parent, IReadOnlyList<ManualMigrationPopulationRow> rows, Action<DistrictCenter> districtChangedAction)
		{
			string elementName = "Game/BatchControl/ManualMigrationHeaderRow";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<VisualElement>(parent, "HeaderContent", null).Add(visualElement);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(parent, "RowsContent", null);
			foreach (ManualMigrationPopulationRow manualMigrationPopulationRow in rows)
			{
				visualElement2.Add(manualMigrationPopulationRow.Root);
			}
			return this.CreateManualMigrationDistrictColumn(visualElement, parent, rows, districtChangedAction);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002588 File Offset: 0x00000788
		public ManualMigrationDistrictColumn CreateManualMigrationDistrictColumn(VisualElement header, VisualElement parent, IReadOnlyList<ManualMigrationPopulationRow> rows, Action<DistrictCenter> districtChangedAction)
		{
			ManualMigrationDistrictDropdownProvider manualMigrationDistrictDropdownProvider = new ManualMigrationDistrictDropdownProvider(this._districtCenterRegistry, districtChangedAction);
			Dropdown dropdown = UQueryExtensions.Q<Dropdown>(header, "DistrictDropdown", null);
			ManualMigrationDistrictDropdown manualMigrationDistrictDropdown = new ManualMigrationDistrictDropdown(this._dropdownItemsSetter, manualMigrationDistrictDropdownProvider, dropdown);
			Image icon = UQueryExtensions.Q<Image>(header, "DistrictIcon", null);
			ManualMigrationDistrictColumn manualMigrationDistrictColumn = new ManualMigrationDistrictColumn(manualMigrationDistrictDropdown, rows, icon, parent);
			UQueryExtensions.Q<Button>(header, "Select", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._entitySelectionService.SelectAndFocusOn(manualMigrationDistrictColumn.DistrictCenter);
			}, 0);
			return manualMigrationDistrictColumn;
		}

		// Token: 0x04000017 RID: 23
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000018 RID: 24
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000019 RID: 25
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;

		// Token: 0x0400001A RID: 26
		public readonly ManualMigrationPopulationRowFactory _manualMigrationPopulationRowFactory;

		// Token: 0x0400001B RID: 27
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400001C RID: 28
		public readonly VisualElementLoader _visualElementLoader;
	}
}
