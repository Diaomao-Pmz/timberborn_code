using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x02000014 RID: 20
	public class TerraformingDirectionalBlocker : BaseComponent, IAwakableComponent, IStartableComponent, IUnfinishedStateListener
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000349D File Offset: 0x0000169D
		public TerraformingDirectionalBlocker(IBlockService blockService, ILoc loc, EventBus eventBus)
		{
			this._blockService = blockService;
			this._loc = loc;
			this._eventBus = eventBus;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000034C8 File Offset: 0x000016C8
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._perAxisBlockerData[Vector3Int.up] = new TerraformingDirectionalBlocker.BlockerData(Vector3Int.up, this._loc.T(TerraformingDirectionalBlocker.DirectionalBlockingLocKey));
			this._blockerDataWithStackable = new TerraformingDirectionalBlocker.BlockerData(Vector3Int.back, this._loc.T(TerraformingDirectionalBlocker.VerticalBlockingLocKey));
			this._perAxisBlockerData[Vector3Int.back] = this._blockerDataWithStackable;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003550 File Offset: 0x00001750
		public void Start()
		{
			foreach (TerraformingDirectionalBlocker.BlockerData blockerData in this._perAxisBlockerData.Values)
			{
				base.GetComponent<StatusSubject>().RegisterStatus(blockerData.StatusToggle);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035B4 File Offset: 0x000017B4
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			BlockObject blockObject = enteredUnfinishedStateEvent.BlockObject;
			if (this.IsValidBlockerBlockObject(blockObject, this._blockObject.Coordinates.Below()))
			{
				this._blockerDataWithStackable.StackableBlocker = blockObject;
				this._blockerDataWithStackable.UpdateBlockingState(this._blockableObject);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035FE File Offset: 0x000017FE
		[OnEvent]
		public void OnExitedUnfinishedState(ExitedUnfinishedStateEvent exitedUnfinishedStateEvent)
		{
			this.ClearStackableIfWasBlocking(exitedUnfinishedStateEvent.BlockObject);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000360C File Offset: 0x0000180C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			this.ClearStackableIfWasBlocking(enteredFinishedStateEvent.BlockObject);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000361C File Offset: 0x0000181C
		public void OnEnterUnfinishedState()
		{
			this._eventBus.Register(this);
			foreach (TerraformingDirectionalBlocker.BlockerData blockerData in this._perAxisBlockerData.Values)
			{
				blockerData.SetCoordinates(this._blockObject);
				this.CheckBlockBlockingThisObject(blockerData);
				this.CheckBlockBlockedByThisObject(blockerData);
				this.CheckForStackableBlocker(blockerData);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000369C File Offset: 0x0000189C
		public void OnExitUnfinishedState()
		{
			this._eventBus.Unregister(this);
			foreach (TerraformingDirectionalBlocker.BlockerData blockerData in this._perAxisBlockerData.Values)
			{
				if (blockerData.BlockedBy)
				{
					this.Unblock(blockerData.BlockedBy, blockerData.Axis);
				}
				if (blockerData.Blocking)
				{
					blockerData.Blocking.Unblock(this, blockerData.Axis);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003738 File Offset: 0x00001938
		public void ClearStackableIfWasBlocking(BlockObject blockObject)
		{
			if (blockObject == this._blockerDataWithStackable.StackableBlocker)
			{
				this._blockerDataWithStackable.StackableBlocker = null;
				this._blockerDataWithStackable.UpdateBlockingState(this._blockableObject);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003768 File Offset: 0x00001968
		public void CheckBlockBlockingThisObject(TerraformingDirectionalBlocker.BlockerData blockerData)
		{
			TerraformingDirectionalBlocker terraformingDirectionalBlocker = this._blockService.GetObjectsWithComponentAt<TerraformingDirectionalBlocker>(blockerData.BlockerCoordinates).FirstOrDefault<TerraformingDirectionalBlocker>();
			Vector3Int axis = blockerData.Axis;
			if (terraformingDirectionalBlocker && terraformingDirectionalBlocker.IsBlockingCoordinates(this._blockObject.Coordinates, axis))
			{
				this.Block(terraformingDirectionalBlocker, axis);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037B8 File Offset: 0x000019B8
		public void CheckBlockBlockedByThisObject(TerraformingDirectionalBlocker.BlockerData blockerData)
		{
			TerraformingDirectionalBlocker terraformingDirectionalBlocker = this._blockService.GetObjectsWithComponentAt<TerraformingDirectionalBlocker>(blockerData.BlockedCoordinates).FirstOrDefault<TerraformingDirectionalBlocker>();
			Vector3Int axis = blockerData.Axis;
			if (terraformingDirectionalBlocker && terraformingDirectionalBlocker.IsBlockerAtCoordinates(this._blockObject.Coordinates, axis))
			{
				terraformingDirectionalBlocker.Block(this, axis);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003808 File Offset: 0x00001A08
		public void CheckForStackableBlocker(TerraformingDirectionalBlocker.BlockerData blockerData)
		{
			BlockObject stackableBlocker;
			if (this._blockerDataWithStackable == blockerData && this.TryGetStackableBlocker(out stackableBlocker))
			{
				blockerData.StackableBlocker = stackableBlocker;
				blockerData.UpdateBlockingState(this._blockableObject);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000383B File Offset: 0x00001A3B
		public bool IsBlockingCoordinates(Vector3Int coordinates, Vector3Int axis)
		{
			return this._perAxisBlockerData[axis].BlockedCoordinates == coordinates;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003854 File Offset: 0x00001A54
		public bool IsBlockerAtCoordinates(Vector3Int coordinates, Vector3Int axis)
		{
			return this._perAxisBlockerData[axis].BlockerCoordinates == coordinates;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000386D File Offset: 0x00001A6D
		public void Block(TerraformingDirectionalBlocker other, Vector3Int axis)
		{
			TerraformingDirectionalBlocker.BlockerData blockerData = this._perAxisBlockerData[axis];
			blockerData.BlockedBy = other;
			other._perAxisBlockerData[axis].Blocking = this;
			blockerData.UpdateBlockingState(this._blockableObject);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000389F File Offset: 0x00001A9F
		public void Unblock(TerraformingDirectionalBlocker other, Vector3Int axis)
		{
			TerraformingDirectionalBlocker.BlockerData blockerData = this._perAxisBlockerData[axis];
			blockerData.BlockedBy = null;
			other._perAxisBlockerData[axis].Blocking = null;
			blockerData.UpdateBlockingState(this._blockableObject);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000038D4 File Offset: 0x00001AD4
		public bool TryGetStackableBlocker(out BlockObject stackableBlockObject)
		{
			Vector3Int coordinates = this._blockObject.Coordinates.Below();
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				if (this.IsValidBlockerBlockObject(blockObject, coordinates))
				{
					stackableBlockObject = blockObject;
					return true;
				}
			}
			stackableBlockObject = null;
			return false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003954 File Offset: 0x00001B54
		public bool IsValidBlockerBlockObject(BlockObject blockObject, Vector3Int coordinates)
		{
			return blockObject != this._blockObject && !blockObject.IsFinished && blockObject.PositionedBlocks.HasBlockAt(coordinates) && blockObject.PositionedBlocks.GetBlock(coordinates).Stackable == BlockStackable.BlockObject;
		}

		// Token: 0x0400003B RID: 59
		public static readonly string DirectionalBlockingLocKey = "Status.Buildings.DirectionalBlocking";

		// Token: 0x0400003C RID: 60
		public static readonly string VerticalBlockingLocKey = "Status.Buildings.VerticalBlocking";

		// Token: 0x0400003D RID: 61
		public readonly IBlockService _blockService;

		// Token: 0x0400003E RID: 62
		public readonly ILoc _loc;

		// Token: 0x0400003F RID: 63
		public readonly EventBus _eventBus;

		// Token: 0x04000040 RID: 64
		public readonly Dictionary<Vector3Int, TerraformingDirectionalBlocker.BlockerData> _perAxisBlockerData = new Dictionary<Vector3Int, TerraformingDirectionalBlocker.BlockerData>();

		// Token: 0x04000041 RID: 65
		public TerraformingDirectionalBlocker.BlockerData _blockerDataWithStackable;

		// Token: 0x04000042 RID: 66
		public BlockableObject _blockableObject;

		// Token: 0x04000043 RID: 67
		public BlockObject _blockObject;

		// Token: 0x02000015 RID: 21
		public class BlockerData
		{
			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000AD RID: 173 RVA: 0x000039AF File Offset: 0x00001BAF
			// (set) Token: 0x060000AE RID: 174 RVA: 0x000039B7 File Offset: 0x00001BB7
			public Vector3Int BlockedCoordinates { get; private set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000AF RID: 175 RVA: 0x000039C0 File Offset: 0x00001BC0
			// (set) Token: 0x060000B0 RID: 176 RVA: 0x000039C8 File Offset: 0x00001BC8
			public Vector3Int BlockerCoordinates { get; private set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x000039D1 File Offset: 0x00001BD1
			// (set) Token: 0x060000B2 RID: 178 RVA: 0x000039D9 File Offset: 0x00001BD9
			public TerraformingDirectionalBlocker Blocking { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x000039E2 File Offset: 0x00001BE2
			// (set) Token: 0x060000B4 RID: 180 RVA: 0x000039EA File Offset: 0x00001BEA
			public TerraformingDirectionalBlocker BlockedBy { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x000039F3 File Offset: 0x00001BF3
			// (set) Token: 0x060000B6 RID: 182 RVA: 0x000039FB File Offset: 0x00001BFB
			public BlockObject StackableBlocker { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003A04 File Offset: 0x00001C04
			public Vector3Int Axis { get; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003A0C File Offset: 0x00001C0C
			public StatusToggle StatusToggle { get; }

			// Token: 0x060000B9 RID: 185 RVA: 0x00003A14 File Offset: 0x00001C14
			public BlockerData(Vector3Int axis, string status)
			{
				this.Axis = axis;
				this.StatusToggle = StatusToggle.CreateNormalStatus("DirectionalBlocking", status);
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00003A34 File Offset: 0x00001C34
			public void SetCoordinates(BlockObject blockObject)
			{
				this.BlockedCoordinates = blockObject.TransformCoordinates(-this.Axis);
				this.BlockerCoordinates = blockObject.TransformCoordinates(this.Axis);
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00003A60 File Offset: 0x00001C60
			public void UpdateBlockingState(BlockableObject blockableObject)
			{
				if ((this.BlockedBy != null || this.StackableBlocker != null) && !this.StatusToggle.IsActive)
				{
					this.StatusToggle.Activate();
					blockableObject.Block(this);
					return;
				}
				if (this.BlockedBy == null && this.StackableBlocker == null && this.StatusToggle.IsActive)
				{
					this.StatusToggle.Deactivate();
					blockableObject.Unblock(this);
				}
			}
		}
	}
}
