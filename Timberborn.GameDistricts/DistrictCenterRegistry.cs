using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000014 RID: 20
	public class DistrictCenterRegistry : ILoadableSingleton
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000035FC File Offset: 0x000017FC
		public DistrictCenterRegistry(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003621 File Offset: 0x00001821
		public ReadOnlyList<DistrictCenter> AllDistrictCenters
		{
			get
			{
				return this._allDistrictCenters.AsReadOnlyList<DistrictCenter>();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000362E File Offset: 0x0000182E
		public ReadOnlyList<DistrictCenter> FinishedDistrictCenters
		{
			get
			{
				return this._finishedDistrictCenters.AsReadOnlyList<DistrictCenter>();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000363B File Offset: 0x0000183B
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000364C File Offset: 0x0000184C
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			DistrictCenter districtCenter;
			if (enteredUnfinishedStateEvent.BlockObject.TryGetComponent<DistrictCenter>(out districtCenter))
			{
				this.AddDistrictCenter(districtCenter);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003670 File Offset: 0x00001870
		[OnEvent]
		public void OnExitedUnfinishedState(ExitedUnfinishedStateEvent exitedUnfinishedStateEvent)
		{
			DistrictCenter districtCenter;
			if (exitedUnfinishedStateEvent.BlockObject.TryGetComponent<DistrictCenter>(out districtCenter))
			{
				this.RemoveDistrictCenter(districtCenter);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003694 File Offset: 0x00001894
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			DistrictCenter districtCenter;
			if (enteredFinishedStateEvent.BlockObject.TryGetComponent<DistrictCenter>(out districtCenter))
			{
				this.RegisterFinishedDistrictCenter(districtCenter);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000036B8 File Offset: 0x000018B8
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			DistrictCenter districtCenter;
			if (exitedFinishedStateEvent.BlockObject.TryGetComponent<DistrictCenter>(out districtCenter))
			{
				this.UnregisterFinishedDistrictCenter(districtCenter);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036DB File Offset: 0x000018DB
		public void RegisterFinishedDistrictCenter(DistrictCenter districtCenter)
		{
			this.AddDistrictCenter(districtCenter);
			this._finishedDistrictCenters.Add(districtCenter);
			this._eventBus.Post(new DistrictCenterRegistryChangedEvent());
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003700 File Offset: 0x00001900
		public void UnregisterFinishedDistrictCenter(DistrictCenter districtCenter)
		{
			this.RemoveDistrictCenter(districtCenter);
			this._finishedDistrictCenters.Remove(districtCenter);
			this._eventBus.Post(new DistrictCenterRegistryChangedEvent());
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003726 File Offset: 0x00001926
		public void AddDistrictCenter(DistrictCenter districtCenter)
		{
			this._allDistrictCenters.Add(districtCenter);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003734 File Offset: 0x00001934
		public void RemoveDistrictCenter(DistrictCenter districtCenter)
		{
			this._allDistrictCenters.Remove(districtCenter);
		}

		// Token: 0x0400003D RID: 61
		public readonly EventBus _eventBus;

		// Token: 0x0400003E RID: 62
		public readonly List<DistrictCenter> _allDistrictCenters = new List<DistrictCenter>();

		// Token: 0x0400003F RID: 63
		public readonly List<DistrictCenter> _finishedDistrictCenters = new List<DistrictCenter>();
	}
}
