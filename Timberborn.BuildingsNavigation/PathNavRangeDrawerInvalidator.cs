using System;
using System.Collections.Generic;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000021 RID: 33
	public class PathNavRangeDrawerInvalidator : ILoadableSingleton, ISingletonInstantNavMeshListener
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000043F9 File Offset: 0x000025F9
		public PathNavRangeDrawerInvalidator(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004413 File Offset: 0x00002613
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004421 File Offset: 0x00002621
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this.MarkDistrictDrawersDirty();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004421 File Offset: 0x00002621
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.MarkDistrictDrawersDirty();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004429 File Offset: 0x00002629
		public void AddDistrictDrawer(DistrictPathNavRangeDrawer districtPathNavRangeDrawer)
		{
			this._districtPathNavRangeDrawers.Add(districtPathNavRangeDrawer);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004437 File Offset: 0x00002637
		public void RemoveDistrictDrawer(DistrictPathNavRangeDrawer districtPathNavRangeDrawer)
		{
			this._districtPathNavRangeDrawers.Remove(districtPathNavRangeDrawer);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004448 File Offset: 0x00002648
		public void MarkDistrictDrawersDirty()
		{
			foreach (DistrictPathNavRangeDrawer districtPathNavRangeDrawer in this._districtPathNavRangeDrawers)
			{
				districtPathNavRangeDrawer.MarkDirty();
			}
		}

		// Token: 0x04000073 RID: 115
		public readonly EventBus _eventBus;

		// Token: 0x04000074 RID: 116
		public readonly List<DistrictPathNavRangeDrawer> _districtPathNavRangeDrawers = new List<DistrictPathNavRangeDrawer>();
	}
}
