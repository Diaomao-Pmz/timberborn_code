using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.SingletonSystem;
using Timberborn.WorkSystem;
using UnityEngine;

namespace Timberborn.Forestry
{
	// Token: 0x0200000C RID: 12
	public class LumberjackFlagStatusDeactivator : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002532 File Offset: 0x00000732
		public LumberjackFlagStatusDeactivator(EventBus eventBus, TreeCuttingArea treeCuttingArea)
		{
			this._eventBus = eventBus;
			this._treeCuttingArea = treeCuttingArea;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002548 File Offset: 0x00000748
		public void Awake()
		{
			this._nothingToDoInRangeStatus = base.GetComponent<NothingToDoInRangeStatus>();
			this._buildingTerrainRange = base.GetComponent<BuildingTerrainRange>();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002562 File Offset: 0x00000762
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002570 File Offset: 0x00000770
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000257E File Offset: 0x0000077E
		[OnEvent]
		public void OnTreeCuttingAreaChanged(TreeCuttingAreaChangedEvent treeCuttingAreaChangedEvent)
		{
			if (treeCuttingAreaChangedEvent.CoordinatesAdded)
			{
				this.UpdateStatus();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000258E File Offset: 0x0000078E
		public void UpdateStatus()
		{
			if (this.AnyYielderInRange())
			{
				this._nothingToDoInRangeStatus.DeactivateStatus();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000025A4 File Offset: 0x000007A4
		public bool AnyYielderInRange()
		{
			foreach (Vector3Int coordinates in this._buildingTerrainRange.GetRange())
			{
				if (this._treeCuttingArea.HasYielder(coordinates))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400000C RID: 12
		public readonly EventBus _eventBus;

		// Token: 0x0400000D RID: 13
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x0400000E RID: 14
		public NothingToDoInRangeStatus _nothingToDoInRangeStatus;

		// Token: 0x0400000F RID: 15
		public BuildingTerrainRange _buildingTerrainRange;
	}
}
