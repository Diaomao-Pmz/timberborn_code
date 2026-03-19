using System;
using System.Collections.Generic;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.Population;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x02000018 RID: 24
	public class MigrationBatchControlTab : BatchControlTab
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000031E6 File Offset: 0x000013E6
		public MigrationBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, DistrictCenterRegistry districtCenterRegistry, ILoc loc, ManualMigrationPanelFactory manualMigrationPanelFactory, MigrationBatchControlRowGroupFactory migrationBatchControlRowGroupFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._loc = loc;
			this._manualMigrationPanelFactory = manualMigrationPanelFactory;
			this._migrationBatchControlRowGroupFactory = migrationBatchControlRowGroupFactory;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003211 File Offset: 0x00001411
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Migration";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003218 File Offset: 0x00001418
		public override string TabImage
		{
			get
			{
				return "Migration";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000321F File Offset: 0x0000141F
		public override string BindingKey
		{
			get
			{
				return "MigrationTab";
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E67 File Offset: 0x00001067
		public override bool IgnoreDistrictSelection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003226 File Offset: 0x00001426
		public override bool MiddleRowVisible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003229 File Offset: 0x00001429
		[OnEvent]
		public void OnPopulationChangedEvent(PopulationChangedEvent populationChangedEvent)
		{
			if (this._isTabVisible)
			{
				base.UpdateRowsVisibility();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002E67 File Offset: 0x00001067
		public override bool RemoveEmptyRowGroups
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000323C File Offset: 0x0000143C
		public override VisualElement GetHeader()
		{
			if (this._districtCenterRegistry.FinishedDistrictCenters.Count > 0)
			{
				this._manualMigrationPanel = this._manualMigrationPanelFactory.Create();
				return this._manualMigrationPanel.Root;
			}
			return null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000327D File Offset: 0x0000147D
		public override string GetRowsLabel()
		{
			return this._loc.T(MigrationBatchControlTab.AutomaticMigrationLocKey);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000328F File Offset: 0x0000148F
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				yield return this._migrationBatchControlRowGroupFactory.Create(districtCenter);
			}
			List<DistrictCenter>.Enumerator enumerator = default(List<DistrictCenter>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000329F File Offset: 0x0000149F
		public override void Show()
		{
			this._isTabVisible = true;
			ManualMigrationPanel manualMigrationPanel = this._manualMigrationPanel;
			if (manualMigrationPanel == null)
			{
				return;
			}
			manualMigrationPanel.Show();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032B8 File Offset: 0x000014B8
		public override void Update()
		{
			ManualMigrationPanel manualMigrationPanel = this._manualMigrationPanel;
			if (manualMigrationPanel == null)
			{
				return;
			}
			manualMigrationPanel.Update();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032CA File Offset: 0x000014CA
		public override void Hide()
		{
			ManualMigrationPanel manualMigrationPanel = this._manualMigrationPanel;
			if (manualMigrationPanel != null)
			{
				manualMigrationPanel.Hide();
			}
			this._isTabVisible = false;
		}

		// Token: 0x0400005D RID: 93
		public static readonly string AutomaticMigrationLocKey = "Migration.AutomaticMigration";

		// Token: 0x0400005E RID: 94
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x0400005F RID: 95
		public readonly ILoc _loc;

		// Token: 0x04000060 RID: 96
		public readonly ManualMigrationPanelFactory _manualMigrationPanelFactory;

		// Token: 0x04000061 RID: 97
		public readonly MigrationBatchControlRowGroupFactory _migrationBatchControlRowGroupFactory;

		// Token: 0x04000062 RID: 98
		public ManualMigrationPanel _manualMigrationPanel;

		// Token: 0x04000063 RID: 99
		public bool _isTabVisible;
	}
}
