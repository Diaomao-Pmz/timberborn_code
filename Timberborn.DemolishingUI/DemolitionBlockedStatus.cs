using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Demolishing;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using Timberborn.TerrainPhysics;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.DemolishingUI
{
	// Token: 0x02000011 RID: 17
	public class DemolitionBlockedStatus : TickableComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003049 File Offset: 0x00001249
		public DemolitionBlockedStatus(IBlockService blockService, ILoc loc, ITerrainPhysicsService terrainPhysicsService)
		{
			this._blockService = blockService;
			this._loc = loc;
			this._terrainPhysicsService = terrainPhysicsService;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003074 File Offset: 0x00001274
		public void Awake()
		{
			this._demolishable = base.GetComponent<Demolishable>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._demolitionBlockedStatus = StatusToggle.CreateNormalStatus("DemolitionBlocked", this._loc.T(DemolitionBlockedStatus.BlockedKey));
			this._demolishable.Marked += this.OnMarked;
			this._demolishable.Unmarked += delegate(object _, EventArgs _)
			{
				base.DisableComponent();
			};
			base.DisableComponent();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000030F0 File Offset: 0x000012F0
		public void InitializeEntity()
		{
			if (!this._blockObject.IsPreview)
			{
				Vector3Int offset = new Vector3Int(0, 0, 1);
				IEnumerable<Vector3Int> collection = from block in this._blockObject.PositionedBlocks.GetOccupiedBlocks()
				where block.Stackable.IsStackable()
				select block.Coordinates + offset;
				this._stackableCoordinates.AddRange(collection);
				if (this._demolishable.IsMarked)
				{
					this.Enable();
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003183 File Offset: 0x00001383
		public override void StartTickable()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._demolitionBlockedStatus);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003196 File Offset: 0x00001396
		public override void Tick()
		{
			this.UpdateStatus();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000319E File Offset: 0x0000139E
		public void OnMarked(object sender, EventArgs e)
		{
			this.Enable();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031A6 File Offset: 0x000013A6
		public void UpdateStatus()
		{
			if (this.IsBlocked())
			{
				this._demolitionBlockedStatus.Activate();
				return;
			}
			this._demolitionBlockedStatus.Deactivate();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031C8 File Offset: 0x000013C8
		public bool IsBlocked()
		{
			foreach (Vector3Int coordinates in this._stackableCoordinates)
			{
				foreach (BlockObject blockObject in this._blockService.GetStackedObjectsAt(coordinates))
				{
					Demolishable component = blockObject.GetComponent<Demolishable>();
					if (component == null || !component.IsMarked)
					{
						return true;
					}
				}
			}
			return !this._terrainPhysicsService.CanBeDestroyed(this._blockObject);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000327C File Offset: 0x0000147C
		public void Enable()
		{
			if (this._stackableCoordinates.Count > 0)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x04000044 RID: 68
		public static readonly string BlockedKey = "Demolish.Blocked";

		// Token: 0x04000045 RID: 69
		public readonly IBlockService _blockService;

		// Token: 0x04000046 RID: 70
		public readonly ILoc _loc;

		// Token: 0x04000047 RID: 71
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x04000048 RID: 72
		public Demolishable _demolishable;

		// Token: 0x04000049 RID: 73
		public BlockObject _blockObject;

		// Token: 0x0400004A RID: 74
		public readonly List<Vector3Int> _stackableCoordinates = new List<Vector3Int>();

		// Token: 0x0400004B RID: 75
		public StatusToggle _demolitionBlockedStatus;
	}
}
