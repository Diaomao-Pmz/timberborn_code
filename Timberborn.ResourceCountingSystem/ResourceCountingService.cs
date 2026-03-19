using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;

namespace Timberborn.ResourceCountingSystem
{
	// Token: 0x0200000B RID: 11
	public class ResourceCountingService : ILoadableSingleton
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000292A File Offset: 0x00000B2A
		public ResourceCountingService(DistrictCenterRegistry districtCenterRegistry, EventBus eventBus)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002956 File Offset: 0x00000B56
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002964 File Offset: 0x00000B64
		public void SwitchDistrict(DistrictCenter districtCenter)
		{
			this._districtResourceCounter = this.GetResourceCounter(districtCenter);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002974 File Offset: 0x00000B74
		public ResourceCount GetGlobalResourceCount(string goodId)
		{
			ResourceCount resourceCount = ResourceCount.Empty;
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				resourceCount += this.GetDistrictResourceCounter(districtCenter).GetResourceCount(goodId);
			}
			return resourceCount;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029E4 File Offset: 0x00000BE4
		public ResourceCount GetDistrictResourceCount(string goodId)
		{
			if (!this._districtResourceCounter)
			{
				return ResourceCount.Empty;
			}
			return this._districtResourceCounter.GetResourceCount(goodId);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A05 File Offset: 0x00000C05
		public DistrictResourceCounter GetDistrictResourceCounter(DistrictCenter districtCenter)
		{
			return this.GetResourceCounter(districtCenter);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A10 File Offset: 0x00000C10
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			DistrictCenter key;
			if (exitedFinishedStateEvent.BlockObject.TryGetComponent<DistrictCenter>(out key))
			{
				this._counters.Remove(key);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A3C File Offset: 0x00000C3C
		public DistrictResourceCounter GetResourceCounter(DistrictCenter districtCenter)
		{
			if (districtCenter == null)
			{
				return this._emptyDistrictResourceCounter;
			}
			DistrictResourceCounter component;
			if (!this._counters.TryGetValue(districtCenter, out component))
			{
				component = districtCenter.GetComponent<DistrictResourceCounter>();
				if (component)
				{
					this._counters.Add(districtCenter, component);
				}
			}
			return component;
		}

		// Token: 0x04000020 RID: 32
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;

		// Token: 0x04000022 RID: 34
		public readonly Dictionary<DistrictCenter, DistrictResourceCounter> _counters = new Dictionary<DistrictCenter, DistrictResourceCounter>();

		// Token: 0x04000023 RID: 35
		public readonly DistrictResourceCounter _emptyDistrictResourceCounter = new DistrictResourceCounter();

		// Token: 0x04000024 RID: 36
		public DistrictResourceCounter _districtResourceCounter;
	}
}
