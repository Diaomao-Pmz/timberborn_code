using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000011 RID: 17
	public class BuildingTerrainRange : BaseComponent, IAwakableComponent, IFinishedStateListener, INavMeshListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005A RID: 90 RVA: 0x00003014 File Offset: 0x00001214
		// (remove) Token: 0x0600005B RID: 91 RVA: 0x0000304C File Offset: 0x0000124C
		public event EventHandler<RangeChangedEventArgs> RangeChanged;

		// Token: 0x0600005C RID: 92 RVA: 0x00003081 File Offset: 0x00001281
		public BuildingTerrainRange(INavigationRangeService navigationRangeService, INavMeshListenerEntityRegistry navMeshListenerEntityRegistry, NavigationDistance navigationDistance)
		{
			this._navigationRangeService = navigationRangeService;
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
			this._navigationDistance = navigationDistance;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030A9 File Offset: 0x000012A9
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030B7 File Offset: 0x000012B7
		public ReadOnlyHashSet<Vector3Int> GetRange()
		{
			if (this._dirty)
			{
				this.UpdateRange();
			}
			return this._range.AsReadOnlyHashSet<Vector3Int>();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030D4 File Offset: 0x000012D4
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			BoundingBox bounds = navMeshUpdate.Bounds;
			if (this._boundingBox.Intersects(bounds))
			{
				this._dirty = true;
				EventHandler<RangeChangedEventArgs> rangeChanged = this.RangeChanged;
				if (rangeChanged != null)
				{
					rangeChanged(this, new RangeChangedEventArgs(!this._rangeInitialized));
				}
				this._rangeInitialized = true;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003126 File Offset: 0x00001326
		public void OnEnterFinishedState()
		{
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
			this.UpdateBoundingBox();
			this._dirty = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003141 File Offset: 0x00001341
		public void OnExitFinishedState()
		{
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000314F File Offset: 0x0000134F
		public Vector3? Access
		{
			get
			{
				return this._buildingAccessible.Accessible.UnblockedSingleAccess;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003164 File Offset: 0x00001364
		public void UpdateRange()
		{
			this._range.Clear();
			if (this.Access != null)
			{
				this._range.AddRange(this._navigationRangeService.GetTerrainNodesInRange(this.Access.Value));
				this._dirty = false;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031B8 File Offset: 0x000013B8
		public void UpdateBoundingBox()
		{
			Vector3 vector = CoordinateSystem.GridToWorld(this._buildingAccessible.Accessible.Accesses.Single<Vector3>());
			float num = this._navigationDistance.ResourceBuildings + 2f;
			Vector3 vector2;
			vector2..ctor(num, num, num);
			Vector3 value = vector + vector2;
			Vector3 value2 = vector - vector2;
			BoundingBox.Builder builder = default(BoundingBox.Builder);
			builder.Expand(value.CeilToInt());
			builder.Expand(value2.FloorToInt());
			this._boundingBox = builder.Build();
		}

		// Token: 0x04000035 RID: 53
		public readonly INavigationRangeService _navigationRangeService;

		// Token: 0x04000036 RID: 54
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x04000037 RID: 55
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x04000038 RID: 56
		public BuildingAccessible _buildingAccessible;

		// Token: 0x04000039 RID: 57
		public readonly HashSet<Vector3Int> _range = new HashSet<Vector3Int>();

		// Token: 0x0400003A RID: 58
		public BoundingBox _boundingBox;

		// Token: 0x0400003B RID: 59
		public bool _dirty;

		// Token: 0x0400003C RID: 60
		public bool _rangeInitialized;
	}
}
