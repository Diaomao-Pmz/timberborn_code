using System;
using Timberborn.CoreUI;
using Timberborn.GameDistrictsMigration;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000011 RID: 17
	public class ManualMigrationPanelFactory
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002934 File Offset: 0x00000B34
		public ManualMigrationPanelFactory(EventBus eventBus, ManualMigrationDistrictColumnFactory manualMigrationDistrictColumnFactory, ManualMigrationDistrictSetter manualMigrationDistrictSetter, VisualElementLoader visualElementLoader)
		{
			this._eventBus = eventBus;
			this._manualMigrationDistrictColumnFactory = manualMigrationDistrictColumnFactory;
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000295C File Offset: 0x00000B5C
		public ManualMigrationPanel Create()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BatchControl/ManualMigrationPanel");
			VisualElement parent = UQueryExtensions.Q<VisualElement>(visualElement, "LeftDistrictContent", null);
			ManualMigrationDistrictColumn manualMigrationDistrictColumnLeft = this._manualMigrationDistrictColumnFactory.CreateLeftColumn(parent);
			VisualElement parent2 = UQueryExtensions.Q<VisualElement>(visualElement, "RightDistrictContent", null);
			ManualMigrationDistrictColumn manualMigrationDistrictColumnRight = this._manualMigrationDistrictColumnFactory.CreateRightColumn(parent2);
			return new ManualMigrationPanel(this._eventBus, this._manualMigrationDistrictSetter, visualElement, manualMigrationDistrictColumnLeft, manualMigrationDistrictColumnRight);
		}

		// Token: 0x0400002D RID: 45
		public readonly EventBus _eventBus;

		// Token: 0x0400002E RID: 46
		public readonly ManualMigrationDistrictColumnFactory _manualMigrationDistrictColumnFactory;

		// Token: 0x0400002F RID: 47
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;

		// Token: 0x04000030 RID: 48
		public readonly VisualElementLoader _visualElementLoader;
	}
}
