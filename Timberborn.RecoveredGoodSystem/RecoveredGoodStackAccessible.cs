using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectAccesses;
using Timberborn.BlockSystem;
using Timberborn.BuildingsReachability;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.Navigation;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200000C RID: 12
	public class RecoveredGoodStackAccessible : BaseComponent, IAwakableComponent, INavMeshListener, IInitializableEntity, IDeletableEntity, IUnreachableEntity, IAccessibleNeeder
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002603 File Offset: 0x00000803
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000260B File Offset: 0x0000080B
		public Accessible Accessible { get; private set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002614 File Offset: 0x00000814
		public RecoveredGoodStackAccessible(IDistrictService districtService, INavMeshListenerEntityRegistry navMeshListenerEntityRegistry, MapSize mapSize)
		{
			this._districtService = districtService;
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
			this._mapSize = mapSize;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002631 File Offset: 0x00000831
		public string AccessibleComponentName
		{
			get
			{
				return "RecoveredGoodStack";
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002638 File Offset: 0x00000838
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectAccessGenerator = base.GetComponent<BlockObjectAccessGenerator>();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002652 File Offset: 0x00000852
		public void SetAccessible(Accessible accessible)
		{
			this.Accessible = accessible;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000265B File Offset: 0x0000085B
		public void InitializeEntity()
		{
			this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
			this.UpdateAccesses();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000266F File Offset: 0x0000086F
		public void DeleteEntity()
		{
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002680 File Offset: 0x00000880
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			BoundingBox bounds = navMeshUpdate.Bounds;
			if (this._bounds.Intersects(bounds))
			{
				this.UpdateAccesses();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026AA File Offset: 0x000008AA
		public bool IsUnreachable()
		{
			return !this._districtService.IsOnInstantDistrictRoadSpill(this.Accessible);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026C0 File Offset: 0x000008C0
		public void UpdateAccesses()
		{
			this._bounds = this._blockObjectAccessGenerator.GenerateAccessBounds(this.MinZ, this.MaxZ);
			this.Accessible.SetAccesses(this._blockObjectAccessGenerator.GenerateAccesses(this.MinZ, this.MaxZ), null);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002718 File Offset: 0x00000918
		public int MinZ
		{
			get
			{
				return this._blockObject.CoordinatesAtBaseZ.z - 1;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000273C File Offset: 0x0000093C
		public int MaxZ
		{
			get
			{
				return this._mapSize.TotalSize.z - 1;
			}
		}

		// Token: 0x0400001B RID: 27
		public readonly IDistrictService _districtService;

		// Token: 0x0400001C RID: 28
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x0400001D RID: 29
		public readonly MapSize _mapSize;

		// Token: 0x0400001E RID: 30
		public BlockObject _blockObject;

		// Token: 0x0400001F RID: 31
		public BlockObjectAccessGenerator _blockObjectAccessGenerator;

		// Token: 0x04000020 RID: 32
		public BoundingBox _bounds;
	}
}
