using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000008 RID: 8
	public class ModularShaft : BaseComponent, IAwakableComponent, IPrePlacementChangeListener, IPostPlacementChangeListener, IPreviewSelectionListener, IFinishedStateListener
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022AF File Offset: 0x000004AF
		public ModularShaft(IBlockService blockService, PreviewBlockService previewBlockService)
		{
			this._blockService = blockService;
			this._previewBlockService = previewBlockService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022C5 File Offset: 0x000004C5
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022D3 File Offset: 0x000004D3
		public void OnPrePlacementChanged()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022D3 File Offset: 0x000004D3
		public void OnPostPlacementChanged()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D3 File Offset: 0x000004D3
		public void OnPreviewSelect()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022D3 File Offset: 0x000004D3
		public void OnPreviewUnselect()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022D3 File Offset: 0x000004D3
		public void OnEnterFinishedState()
		{
			this.UpdateNeighbors();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022DB File Offset: 0x000004DB
		public void OnExitFinishedState()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022E0 File Offset: 0x000004E0
		public void UpdateNeighbors()
		{
			if (this._blockObject.Positioned)
			{
				foreach (Vector3Int vector3Int in Deltas.Neighbors6Vector3Int)
				{
					this.UpdateNeighbor(this._blockObject.Coordinates + vector3Int);
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000232D File Offset: 0x0000052D
		public void UpdateNeighbor(Vector3Int position)
		{
			BlockObjectModelController firstObjectWithComponentAt = this._blockService.GetFirstObjectWithComponentAt<BlockObjectModelController>(position);
			if (firstObjectWithComponentAt != null)
			{
				firstObjectWithComponentAt.UpdateModel();
			}
			BlockObjectModelController firstObjectWithComponentAt2 = this._previewBlockService.GetFirstObjectWithComponentAt<BlockObjectModelController>(position);
			if (firstObjectWithComponentAt2 == null)
			{
				return;
			}
			firstObjectWithComponentAt2.UpdateModel();
		}

		// Token: 0x0400000E RID: 14
		public readonly IBlockService _blockService;

		// Token: 0x0400000F RID: 15
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x04000010 RID: 16
		public BlockObject _blockObject;
	}
}
