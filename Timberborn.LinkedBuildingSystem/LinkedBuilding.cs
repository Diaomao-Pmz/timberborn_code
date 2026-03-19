using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x02000007 RID: 7
	public class LinkedBuilding : BaseComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedStateListener, IDeletableEntity, IPersistentEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler<LinkedBuilding> BuildingLinked;

		// Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
		public LinkedBuilding(IBlockService blockService, EntityService entityService, ReferenceSerializer referenceSerializer)
		{
			this._blockService = blockService;
			this._entityService = entityService;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000218A File Offset: 0x0000038A
		public bool IsLinked
		{
			get
			{
				return this._linked;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002197 File Offset: 0x00000397
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A5 File Offset: 0x000003A5
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsLinked)
			{
				entitySaver.GetComponent(LinkedBuilding.LinkedBuildingKey).Set<LinkedBuilding>(LinkedBuilding.LinkedKey, this._linked, this._referenceSerializer.Of<LinkedBuilding>());
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D8 File Offset: 0x000003D8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(LinkedBuilding.LinkedBuildingKey, out objectLoader))
			{
				this.LinkBuilding(objectLoader.Get<LinkedBuilding>(LinkedBuilding.LinkedKey, this._referenceSerializer.Of<LinkedBuilding>()));
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002210 File Offset: 0x00000410
		public void OnEnterFinishedState()
		{
			this.LinkIfNeeded();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002218 File Offset: 0x00000418
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002210 File Offset: 0x00000410
		public void OnEnterUnfinishedState()
		{
			this.LinkIfNeeded();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002218 File Offset: 0x00000418
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000221A File Offset: 0x0000041A
		public void DeleteEntity()
		{
			if (this.IsLinked)
			{
				this._linked.UnlinkAndDelete();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002230 File Offset: 0x00000430
		public void LinkIfNeeded()
		{
			if (!this.IsLinked)
			{
				LinkedBuilding bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<LinkedBuilding>(this._blockObject.CoordinatesBehind());
				if (bottomObjectComponentAt)
				{
					if (!bottomObjectComponentAt.IsLinked)
					{
						this.LinkBuilding(bottomObjectComponentAt);
						bottomObjectComponentAt.LinkBuilding(this);
						return;
					}
					throw new NotSupportedException("Trying to link to already linked building");
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002286 File Offset: 0x00000486
		public void LinkBuilding(LinkedBuilding linked)
		{
			this._linked = linked;
			EventHandler<LinkedBuilding> buildingLinked = this.BuildingLinked;
			if (buildingLinked == null)
			{
				return;
			}
			buildingLinked(this, linked);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A1 File Offset: 0x000004A1
		public void UnlinkAndDelete()
		{
			this._linked = null;
			this._entityService.Delete(this);
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey LinkedBuildingKey = new ComponentKey("LinkedBuilding");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<LinkedBuilding> LinkedKey = new PropertyKey<LinkedBuilding>("Linked");

		// Token: 0x0400000B RID: 11
		public readonly IBlockService _blockService;

		// Token: 0x0400000C RID: 12
		public readonly EntityService _entityService;

		// Token: 0x0400000D RID: 13
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400000E RID: 14
		public BlockObject _blockObject;

		// Token: 0x0400000F RID: 15
		public LinkedBuilding _linked;
	}
}
