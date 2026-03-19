using System;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000011 RID: 17
	public class ManualMigrationDistrictSetter : ILoadableSingleton, ISaveableSingleton, IPostLoadableSingleton
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002665 File Offset: 0x00000865
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000266D File Offset: 0x0000086D
		public DistrictCenter LeftDistrict { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002676 File Offset: 0x00000876
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000267E File Offset: 0x0000087E
		public DistrictCenter RightDistrict { get; private set; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002687 File Offset: 0x00000887
		public ManualMigrationDistrictSetter(DistrictCenterRegistry districtCenterRegistry, DistrictConnections districtConnections, EventBus eventBus, ISingletonLoader singletonLoader)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._districtConnections = districtConnections;
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026BA File Offset: 0x000008BA
		public bool AreDistrictsSet
		{
			get
			{
				return this.IsLeftDistrictSet && this.IsRightDistrictSet;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026CC File Offset: 0x000008CC
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(ManualMigrationDistrictSetter.ManualMigrationDistrictSetterKey);
			singleton.Set(ManualMigrationDistrictSetter.LeftDistrictLastIndexKey, this.DistrictCenters.IndexOf(this.LeftDistrict));
			singleton.Set(ManualMigrationDistrictSetter.RightDistrictLastIndexKey, this.DistrictCenters.IndexOf(this.RightDistrict));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002728 File Offset: 0x00000928
		public void Load()
		{
			this._eventBus.Register(this);
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(ManualMigrationDistrictSetter.ManualMigrationDistrictSetterKey, out objectLoader) && objectLoader.Has<int>(ManualMigrationDistrictSetter.LeftDistrictLastIndexKey))
			{
				this._leftDistrictLastIndex = objectLoader.Get(ManualMigrationDistrictSetter.LeftDistrictLastIndexKey);
				this._rightDistrictLastIndex = objectLoader.Get(ManualMigrationDistrictSetter.RightDistrictLastIndexKey);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002784 File Offset: 0x00000984
		public void PostLoad()
		{
			if (this._leftDistrictLastIndex >= 0 && this._leftDistrictLastIndex < this.DistrictCenters.Count)
			{
				this.LeftDistrict = this.DistrictCenters[this._leftDistrictLastIndex];
			}
			if (this._rightDistrictLastIndex >= 0 && this._rightDistrictLastIndex < this.DistrictCenters.Count)
			{
				this.RightDistrict = this.DistrictCenters[this._rightDistrictLastIndex];
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002803 File Offset: 0x00000A03
		public void SetLeftDistrict(DistrictCenter districtCenter)
		{
			this.SetLeftDistrict(districtCenter, MigrationDistrictChangedEvent.Create());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002811 File Offset: 0x00000A11
		public void SetRightDistrict(DistrictCenter districtCenter)
		{
			this.SetRightDistrict(districtCenter, MigrationDistrictChangedEvent.Create());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000281F File Offset: 0x00000A1F
		public void SetLeftDistrictWithHighlight(DistrictCenter districtCenter)
		{
			this.SetLeftDistrict(districtCenter, MigrationDistrictChangedEvent.CreateWithLeftHighlight());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000282D File Offset: 0x00000A2D
		public void SetRightDistrictWithHighlight(DistrictCenter districtCenter)
		{
			this.SetRightDistrict(districtCenter, MigrationDistrictChangedEvent.CreateWithRightHighlight());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000283B File Offset: 0x00000A3B
		public void ResetRightDistrictChangeCheck()
		{
			this._wasRightDistrictChanged = false;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002844 File Offset: 0x00000A44
		public void DifferentiateDistricts()
		{
			if (this.LeftDistrict == this.RightDistrict)
			{
				if (this._wasRightDistrictChanged)
				{
					this.LeftDistrict = null;
				}
				else
				{
					this.RightDistrict = null;
				}
				this.CheckDistrictsAndPostEvent(MigrationDistrictChangedEvent.Create());
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002878 File Offset: 0x00000A78
		[OnEvent]
		public void OnDistrictCenterRegistryChanged(DistrictCenterRegistryChangedEvent districtCenterRegistryChangedEvent)
		{
			if (!this.DistrictCenters.Contains(this.LeftDistrict))
			{
				this.LeftDistrict = null;
			}
			if (!this.DistrictCenters.Contains(this.RightDistrict))
			{
				this.RightDistrict = null;
			}
			this.CheckDistrictsAndPostEvent(MigrationDistrictChangedEvent.Create());
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028CA File Offset: 0x00000ACA
		public ReadOnlyList<DistrictCenter> DistrictCenters
		{
			get
			{
				return this._districtCenterRegistry.FinishedDistrictCenters;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028D8 File Offset: 0x00000AD8
		public bool IsLeftDistrictSet
		{
			get
			{
				return this.LeftDistrict && this.DistrictCenters.Contains(this.LeftDistrict);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002908 File Offset: 0x00000B08
		public bool IsRightDistrictSet
		{
			get
			{
				return this.RightDistrict && this.DistrictCenters.Contains(this.RightDistrict);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002938 File Offset: 0x00000B38
		public void SetLeftDistrict(DistrictCenter districtCenter, MigrationDistrictChangedEvent migrationDistrictChangedEvent)
		{
			this.LeftDistrict = districtCenter;
			this._wasRightDistrictChanged = false;
			this.CheckDistrictsAndPostEvent(migrationDistrictChangedEvent);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000294F File Offset: 0x00000B4F
		public void SetRightDistrict(DistrictCenter districtCenter, MigrationDistrictChangedEvent migrationDistrictChangedEvent)
		{
			this.RightDistrict = districtCenter;
			this._wasRightDistrictChanged = true;
			this.CheckDistrictsAndPostEvent(migrationDistrictChangedEvent);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002966 File Offset: 0x00000B66
		public void CheckDistrictsAndPostEvent(MigrationDistrictChangedEvent migrationDistrictChangedEvent)
		{
			this.UpdateDistricts();
			this._eventBus.Post(migrationDistrictChangedEvent);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000297C File Offset: 0x00000B7C
		public void UpdateDistricts()
		{
			if (this.DistrictCenters.Count == 0)
			{
				this.LeftDistrict = null;
				this.RightDistrict = null;
				return;
			}
			if (!this.RightDistrict)
			{
				if (!this.LeftDistrict)
				{
					this.LeftDistrict = this.DistrictCenters[0];
				}
				this.RightDistrict = this._districtConnections.GetFirstConnectedOrAny(this.LeftDistrict);
				return;
			}
			if (!this.LeftDistrict)
			{
				this.LeftDistrict = this._districtConnections.GetFirstConnectedOrAny(this.RightDistrict);
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly SingletonKey ManualMigrationDistrictSetterKey = new SingletonKey("ManualMigrationDistrictSetter");

		// Token: 0x0400001D RID: 29
		public static readonly PropertyKey<int> LeftDistrictLastIndexKey = new PropertyKey<int>("LeftDistrictLastIndex");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<int> RightDistrictLastIndexKey = new PropertyKey<int>("RightDistrictLastIndex");

		// Token: 0x04000021 RID: 33
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000022 RID: 34
		public readonly DistrictConnections _districtConnections;

		// Token: 0x04000023 RID: 35
		public readonly EventBus _eventBus;

		// Token: 0x04000024 RID: 36
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000025 RID: 37
		public int _leftDistrictLastIndex = -1;

		// Token: 0x04000026 RID: 38
		public int _rightDistrictLastIndex = -1;

		// Token: 0x04000027 RID: 39
		public bool _wasRightDistrictChanged;
	}
}
