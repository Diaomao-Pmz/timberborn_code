using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x02000011 RID: 17
	public class DynamicPathModelUpdater : BaseComponent, IAwakableComponent, IDeletableEntity, IPreviewSelectionListener, IPrePlacementChangeListener, IPostPlacementChangeListener
	{
		// Token: 0x0600006D RID: 109 RVA: 0x0000347D File Offset: 0x0000167D
		public DynamicPathModelUpdater(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000348C File Offset: 0x0000168C
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000349A File Offset: 0x0000169A
		public void DeleteEntity()
		{
			this.UpdatePathModel();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000349A File Offset: 0x0000169A
		public void OnPrePlacementChanged()
		{
			this.UpdatePathModel();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000349A File Offset: 0x0000169A
		public void OnPostPlacementChanged()
		{
			this.UpdatePathModel();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000349A File Offset: 0x0000169A
		public void OnPreviewSelect()
		{
			this.UpdatePathModel();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000349A File Offset: 0x0000169A
		public void OnPreviewUnselect()
		{
			this.UpdatePathModel();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034A4 File Offset: 0x000016A4
		public void UpdatePathModel()
		{
			if (this._blockObject.Positioned)
			{
				foreach (Vector3Int coordinates in this._blockObject.PositionedBlocks.GetFoundationCoordinates())
				{
					DynamicPathModel pathObjectComponentAt = this._blockService.GetPathObjectComponentAt<DynamicPathModel>(coordinates);
					if (pathObjectComponentAt)
					{
						pathObjectComponentAt.UpdateModel();
					}
				}
			}
		}

		// Token: 0x04000045 RID: 69
		public readonly IBlockService _blockService;

		// Token: 0x04000046 RID: 70
		public BlockObject _blockObject;
	}
}
