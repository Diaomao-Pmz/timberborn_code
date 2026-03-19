using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x0200000F RID: 15
	public class BlockObjectAccessible : BaseComponent, IAwakableComponent, IInitializableEntity, INavMeshListener, IDeletableEntity, IAccessibleNeeder
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002F67 File Offset: 0x00001167
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002F6F File Offset: 0x0000116F
		public Accessible Accessible { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002F78 File Offset: 0x00001178
		public BlockObjectAccessible(INavMeshListenerEntityRegistry navMeshListenerEntityRegistry)
		{
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002F87 File Offset: 0x00001187
		public string AccessibleComponentName
		{
			get
			{
				return "BlockObjectAccessible";
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F8E File Offset: 0x0000118E
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectAccessGenerator = base.GetComponent<BlockObjectAccessGenerator>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FA8 File Offset: 0x000011A8
		public void InitializeEntity()
		{
			if (!this._blockObject.IsPreview)
			{
				this.UpdateBounds();
				this.UpdateAccesses();
				this._navMeshListenerEntityRegistry.RegisterNavMeshListener(this);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FCF File Offset: 0x000011CF
		public void DeleteEntity()
		{
			this._navMeshListenerEntityRegistry.UnregisterNavMeshListener(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FDD File Offset: 0x000011DD
		public void SetAccessible(Accessible accessible)
		{
			this.Accessible = accessible;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			BoundingBox bounds = navMeshUpdate.Bounds;
			if (this._bounds.Intersects(bounds))
			{
				this.UpdateAccesses();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003012 File Offset: 0x00001212
		public void SetNumberOfAccessLevelsAboveGround(int accessLevelsAboveGround)
		{
			this._accessLevelsAboveGround = accessLevelsAboveGround;
			if (this.Accessible.Enabled)
			{
				this.UpdateBounds();
				this.UpdateAccesses();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003034 File Offset: 0x00001234
		public int MinZ
		{
			get
			{
				return this._blockObject.Coordinates.z;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003054 File Offset: 0x00001254
		public int MaxZ
		{
			get
			{
				return this._blockObject.Coordinates.z + this._accessLevelsAboveGround;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000307B File Offset: 0x0000127B
		public void UpdateBounds()
		{
			this._bounds = this._blockObjectAccessGenerator.GenerateAccessBounds(this.MinZ, this.MaxZ);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000309C File Offset: 0x0000129C
		public void UpdateAccesses()
		{
			this.Accessible.SetAccesses(this.GenerateAccesses(), null);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030C3 File Offset: 0x000012C3
		public IEnumerable<Vector3> GenerateAccesses()
		{
			return this._blockObjectAccessGenerator.GenerateAccesses(this.MinZ, this.MaxZ);
		}

		// Token: 0x04000039 RID: 57
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x0400003A RID: 58
		public BlockObject _blockObject;

		// Token: 0x0400003B RID: 59
		public BlockObjectAccessGenerator _blockObjectAccessGenerator;

		// Token: 0x0400003C RID: 60
		public BoundingBox _bounds;

		// Token: 0x0400003D RID: 61
		public int _accessLevelsAboveGround;
	}
}
