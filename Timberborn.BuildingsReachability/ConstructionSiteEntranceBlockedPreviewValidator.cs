using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectAccesses;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.BuildingsReachability
{
	// Token: 0x02000009 RID: 9
	public class ConstructionSiteEntranceBlockedPreviewValidator : BaseComponent, IAwakableComponent, IPreviewValidator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000022F8 File Offset: 0x000004F8
		public ConstructionSiteEntranceBlockedPreviewValidator(IBlockService blockService, NeighborCalculator neighborCalculator, ILoc loc)
		{
			this._blockService = blockService;
			this._neighborCalculator = neighborCalculator;
			this._loc = loc;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000232B File Offset: 0x0000052B
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockableEntranceBuilding = base.GetComponent<BlockableEntranceBuilding>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002345 File Offset: 0x00000545
		public bool IsValid(out string warningMessage)
		{
			warningMessage = this._loc.T(ConstructionSiteEntranceBlockedPreviewValidator.EntranceBlockedLocKey);
			return !this._blockableEntranceBuilding.IsEntranceBlocked();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002368 File Offset: 0x00000568
		public ReadOnlyHashSet<BaseComponent> InvalidatedObjects(out string warningMessage)
		{
			warningMessage = this._loc.T(ConstructionSiteEntranceBlockedPreviewValidator.EntranceBlockedLocKey);
			this._candidateBuildings.Clear();
			this._blockedBuildings.Clear();
			foreach (Vector3Int coordinates in this._neighborCalculator.GetNonInternalNeighborsWithoutDiagonal(this.PreviewCoordinates))
			{
				BlockableEntranceBuilding bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<BlockableEntranceBuilding>(coordinates);
				if (bottomObjectComponentAt)
				{
					this._candidateBuildings.Add(bottomObjectComponentAt);
				}
			}
			foreach (BlockableEntranceBuilding blockableEntranceBuilding in this._candidateBuildings)
			{
				if (blockableEntranceBuilding.IsEntranceBlockedByCoordinates(this.PreviewCoordinates))
				{
					this._blockedBuildings.Add(blockableEntranceBuilding);
				}
			}
			return this._blockedBuildings.AsReadOnlyHashSet<BaseComponent>();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002468 File Offset: 0x00000668
		public IEnumerable<Vector3Int> PreviewCoordinates
		{
			get
			{
				return from block in this._blockObject.PositionedBlocks.GetOccupiedBlocks()
				where block.Occupation.HasBottomOrFloorOrFull()
				select block.Coordinates;
			}
		}

		// Token: 0x0400000D RID: 13
		public static readonly string EntranceBlockedLocKey = "Buildings.EntranceBlocked";

		// Token: 0x0400000E RID: 14
		public readonly IBlockService _blockService;

		// Token: 0x0400000F RID: 15
		public readonly NeighborCalculator _neighborCalculator;

		// Token: 0x04000010 RID: 16
		public readonly ILoc _loc;

		// Token: 0x04000011 RID: 17
		public BlockableEntranceBuilding _blockableEntranceBuilding;

		// Token: 0x04000012 RID: 18
		public BlockObject _blockObject;

		// Token: 0x04000013 RID: 19
		public readonly HashSet<BlockableEntranceBuilding> _candidateBuildings = new HashSet<BlockableEntranceBuilding>();

		// Token: 0x04000014 RID: 20
		public readonly HashSet<BaseComponent> _blockedBuildings = new HashSet<BaseComponent>();
	}
}
