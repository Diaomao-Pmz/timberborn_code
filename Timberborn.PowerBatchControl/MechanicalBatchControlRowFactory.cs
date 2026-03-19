using System;
using Timberborn.AutomationUI;
using Timberborn.BatchControl;
using Timberborn.BuildingsUI;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.HaulingUI;
using Timberborn.MechanicalSystem;
using Timberborn.MechanicalSystemUI;
using Timberborn.StatusSystemUI;

namespace Timberborn.PowerBatchControl
{
	// Token: 0x02000004 RID: 4
	public class MechanicalBatchControlRowFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public MechanicalBatchControlRowFactory(VisualElementLoader visualElementLoader, BuildingBatchControlRowItemFactory buildingBatchControlRowItemFactory, StatusBatchControlRowItemFactory statusBatchControlRowItemFactory, MechanicalBatchControlRowItemFactory mechanicalBatchControlRowItemFactory, BatteryBatchControlRowItemFactory batteryBatchControlRowItemFactory, HaulCandidateBatchControlRowItemFactory haulCandidateBatchControlRowItemFactory, AutomatableBatchControlRowItemFactory automatableBatchControlRowItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._buildingBatchControlRowItemFactory = buildingBatchControlRowItemFactory;
			this._statusBatchControlRowItemFactory = statusBatchControlRowItemFactory;
			this._mechanicalBatchControlRowItemFactory = mechanicalBatchControlRowItemFactory;
			this._batteryBatchControlRowItemFactory = batteryBatchControlRowItemFactory;
			this._haulCandidateBatchControlRowItemFactory = haulCandidateBatchControlRowItemFactory;
			this._automatableBatchControlRowItemFactory = automatableBatchControlRowItemFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public BatchControlRow Create(EntityComponent entity)
		{
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow"), entity, new IBatchControlRowItem[]
			{
				this._buildingBatchControlRowItemFactory.Create(entity),
				this._mechanicalBatchControlRowItemFactory.Create(entity),
				this._haulCandidateBatchControlRowItemFactory.Create(entity),
				this._batteryBatchControlRowItemFactory.Create(entity),
				this._automatableBatchControlRowItemFactory.Create(entity),
				this._statusBatchControlRowItemFactory.Create(entity)
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002180 File Offset: 0x00000380
		public BatchControlRow Create(MechanicalGraph mechanicalGraph)
		{
			string elementName = "Game/BatchControl/BatchControlHeaderRow";
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement(elementName), new IBatchControlRowItem[]
			{
				this._mechanicalBatchControlRowItemFactory.Create(mechanicalGraph)
			});
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly BuildingBatchControlRowItemFactory _buildingBatchControlRowItemFactory;

		// Token: 0x04000008 RID: 8
		public readonly StatusBatchControlRowItemFactory _statusBatchControlRowItemFactory;

		// Token: 0x04000009 RID: 9
		public readonly MechanicalBatchControlRowItemFactory _mechanicalBatchControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly BatteryBatchControlRowItemFactory _batteryBatchControlRowItemFactory;

		// Token: 0x0400000B RID: 11
		public readonly HaulCandidateBatchControlRowItemFactory _haulCandidateBatchControlRowItemFactory;

		// Token: 0x0400000C RID: 12
		public readonly AutomatableBatchControlRowItemFactory _automatableBatchControlRowItemFactory;
	}
}
