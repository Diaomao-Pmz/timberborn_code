using System;
using Timberborn.GameDistricts;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x0200000F RID: 15
	public class ManualMigrationBlocker : ILoadableSingleton
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000253F File Offset: 0x0000073F
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002547 File Offset: 0x00000747
		public string TooltipText { get; private set; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002550 File Offset: 0x00000750
		public ManualMigrationBlocker(DistrictConnections districtConnections, EventBus eventBus, ILoc loc, ManualMigrationDistrictSetter manualMigrationDistrictSetter)
		{
			this._districtConnections = districtConnections;
			this._eventBus = eventBus;
			this._loc = loc;
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002575 File Offset: 0x00000775
		public bool IsEnabled
		{
			get
			{
				return string.IsNullOrEmpty(this.TooltipText);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002582 File Offset: 0x00000782
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002590 File Offset: 0x00000790
		[OnEvent]
		public void OnDistrictConnectionsChanged(DistrictConnectionsChangedEvent districtConnectionsChangedEvent)
		{
			this.SetCurrentState();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002590 File Offset: 0x00000790
		[OnEvent]
		public void OnMigrationDistrictChangedEvent(MigrationDistrictChangedEvent migrationDistrictChangedEvent)
		{
			this.SetCurrentState();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002598 File Offset: 0x00000798
		public void SetCurrentState()
		{
			this.TooltipText = this.GetCurrentState();
			this._eventBus.Post(new ManualMigrationBlockingStateChangedEvent(this.IsEnabled));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025BC File Offset: 0x000007BC
		public string GetCurrentState()
		{
			if (this._manualMigrationDistrictSetter.AreDistrictsSet)
			{
				if (this._manualMigrationDistrictSetter.LeftDistrict == this._manualMigrationDistrictSetter.RightDistrict)
				{
					return this._loc.T(ManualMigrationBlocker.SameDistrictLocKey);
				}
				if (!this._districtConnections.AreDistrictsConnected(this._manualMigrationDistrictSetter.LeftDistrict, this._manualMigrationDistrictSetter.RightDistrict))
				{
					return this._loc.T(ManualMigrationBlocker.NotConnectedLocKey);
				}
			}
			return string.Empty;
		}

		// Token: 0x04000014 RID: 20
		public static readonly string SameDistrictLocKey = "Migration.SameDistrict";

		// Token: 0x04000015 RID: 21
		public static readonly string NotConnectedLocKey = "Migration.DistrictsNotConnected";

		// Token: 0x04000017 RID: 23
		public readonly DistrictConnections _districtConnections;

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;

		// Token: 0x0400001A RID: 26
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;
	}
}
