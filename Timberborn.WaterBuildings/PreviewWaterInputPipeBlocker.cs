using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200001D RID: 29
	public class PreviewWaterInputPipeBlocker : BaseComponent, IAwakableComponent, IPrePlacementChangeListener, IPostPlacementChangeListener, IPreviewSelectionListener
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public PreviewWaterInputPipeBlocker(PreviewWaterInputPipeBlockerService previewWaterInputPipeBlockerService)
		{
			this._previewWaterInputPipeBlockerService = previewWaterInputPipeBlockerService;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._pipeIntersectionAllowerSpec = base.GetComponent<PipeIntersectionAllowerSpec>();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003DDD File Offset: 0x00001FDD
		public void OnPrePlacementChanged()
		{
			this.Unblock();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public void OnPostPlacementChanged()
		{
			this.Block();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public void OnPreviewSelect()
		{
			this.Block();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003DDD File Offset: 0x00001FDD
		public void OnPreviewUnselect()
		{
			this.Unblock();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003DED File Offset: 0x00001FED
		public void Block()
		{
			if (this.IsValidBlocker() && !this._isBlocking)
			{
				this._previewWaterInputPipeBlockerService.Block(this.GetBlockingOccupiedTiles());
				this._isBlocking = true;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003E17 File Offset: 0x00002017
		public void Unblock()
		{
			if (this.IsValidBlocker() && this._isBlocking)
			{
				this._previewWaterInputPipeBlockerService.Unblock(this.GetBlockingOccupiedTiles());
				this._isBlocking = false;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003E41 File Offset: 0x00002041
		public bool IsValidBlocker()
		{
			return this._blockObject.IsPreview && this._pipeIntersectionAllowerSpec == null;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003E5E File Offset: 0x0000205E
		public IEnumerable<Vector3Int> GetBlockingOccupiedTiles()
		{
			return this._blockObject.PositionedBlocks.GetOccupiedCoordinatesIntersecting(WaterInputCoordinates.InvalidOccupations);
		}

		// Token: 0x04000052 RID: 82
		public readonly PreviewWaterInputPipeBlockerService _previewWaterInputPipeBlockerService;

		// Token: 0x04000053 RID: 83
		public BlockObject _blockObject;

		// Token: 0x04000054 RID: 84
		public PipeIntersectionAllowerSpec _pipeIntersectionAllowerSpec;

		// Token: 0x04000055 RID: 85
		public bool _isBlocking;
	}
}
