using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000006 RID: 6
	public class DistrictMigrationSetterRowItemFactory
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002167 File Offset: 0x00000367
		public DistrictMigrationSetterRowItemFactory(ManualMigrationDistrictSetter manualMigrationDistrictSetter, VisualElementLoader visualElementLoader)
		{
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000380
		public IBatchControlRowItem Create(DistrictCenter districtCenter)
		{
			string elementName = "Game/BatchControl/DistrictMigrationSetterRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Button>(visualElement, "MigrateButtonLeft", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._manualMigrationDistrictSetter.SetLeftDistrictWithHighlight(districtCenter);
			}, 0);
			UQueryExtensions.Q<Button>(visualElement, "MigrateButtonRight", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._manualMigrationDistrictSetter.SetRightDistrictWithHighlight(districtCenter);
			}, 0);
			return new EmptyBatchControlRowItem(visualElement);
		}

		// Token: 0x0400000A RID: 10
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;
	}
}
