using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.MergeableObjects
{
	// Token: 0x0200000A RID: 10
	public class MergeableObjectModelUpdater : BaseComponent, IAwakableComponent, IPrePlacementChangeListener, IPostPlacementChangeListener, IPreviewSelectionListener
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000025F8 File Offset: 0x000007F8
		public MergeableObjectModelUpdater(IBlockService blockService, PreviewBlockService previewBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000260E File Offset: 0x0000080E
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000261C File Offset: 0x0000081C
		public void OnPrePlacementChanged()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000261C File Offset: 0x0000081C
		public void OnPostPlacementChanged()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000261C File Offset: 0x0000081C
		public void OnPreviewSelect()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000261C File Offset: 0x0000081C
		public void OnPreviewUnselect()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002624 File Offset: 0x00000824
		public void UpdateNeighbors()
		{
			if (this._blockObject.Positioned)
			{
				Vector3Int vector3Int = this._blockObject.PositionedBlocks.GetOccupiedCoordinates().First<Vector3Int>();
				foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
				{
					this.UpdateNeighbor(vector3Int + vector3Int2);
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000267D File Offset: 0x0000087D
		public void UpdateNeighbor(Vector3Int target)
		{
			MergeableObjectModel bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<MergeableObjectModel>(target);
			if (bottomObjectComponentAt != null)
			{
				bottomObjectComponentAt.UpdateModel();
			}
			MergeableObjectModel bottomObjectComponentAt2 = this._previewBlockService.GetBottomObjectComponentAt<MergeableObjectModel>(target);
			if (bottomObjectComponentAt2 == null)
			{
				return;
			}
			bottomObjectComponentAt2.UpdateModel();
		}

		// Token: 0x04000011 RID: 17
		public readonly IBlockService _blockService;

		// Token: 0x04000012 RID: 18
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x04000013 RID: 19
		public BlockObject _blockObject;
	}
}
