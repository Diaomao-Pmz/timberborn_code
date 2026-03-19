using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectGridFootprint : BaseComponent, IAwakableComponent, IPostPlacementChangeListener, IPreviewSelectionListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BlockObjectGridFootprint(ConstructionGuidelinesRenderingService constructionGuidelinesRenderingService)
		{
			this._constructionGuidelinesRenderingService = constructionGuidelinesRenderingService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._preview = base.GetComponent<Preview>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		public void OnPostPlacementChanged()
		{
			if (this._blockObject.IsPreview)
			{
				this._min = new Vector2Int(int.MaxValue, int.MaxValue);
				this._max = new Vector2Int(int.MinValue, int.MinValue);
				this._footprintsCoordinatesPerCell.Clear();
				foreach (Block block in this._blockObject.PositionedBlocks.GetOccupiedBlocks())
				{
					Vector2Int coords2D;
					coords2D..ctor(block.Coordinates.x, block.Coordinates.y);
					this.UpdateLowestCoordinatePerCell(block, coords2D);
					this._min = new Vector2Int(Math.Min(this._min.x, coords2D.x), Math.Min(this._min.y, coords2D.y));
					this._max = new Vector2Int(Math.Max(this._max.x, coords2D.x), Math.Max(this._max.y, coords2D.y));
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000227C File Offset: 0x0000047C
		public void OnPreviewSelect()
		{
			if (this._preview.PreviewState.IsLast)
			{
				this._constructionGuidelinesRenderingService.SetPreviewFootprint(this._min, this._max, this._blockObjectCenter.GridCenterAtBaseZ, this._footprintsCoordinatesPerCell.Values);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022CB File Offset: 0x000004CB
		public void OnPreviewUnselect()
		{
			this._footprintsCoordinatesPerCell.Clear();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022D8 File Offset: 0x000004D8
		public void UpdateLowestCoordinatePerCell(Block block, Vector2Int coords2D)
		{
			Vector3Int coordinates = block.Coordinates;
			if (block.IsOccupied)
			{
				bool canHaveFootprint = (block.Occupation & (BlockOccupations.Floor | BlockOccupations.Bottom | BlockOccupations.Corners | BlockOccupations.Path | BlockOccupations.Middle)) == BlockOccupations.None;
				FootprintCoordinates footprintCoordinates;
				if (this._footprintsCoordinatesPerCell.TryGetValue(coords2D, out footprintCoordinates))
				{
					if (footprintCoordinates.Coordinates.z > coordinates.z)
					{
						this._footprintsCoordinatesPerCell[coords2D] = new FootprintCoordinates(coordinates, canHaveFootprint);
						return;
					}
				}
				else
				{
					this._footprintsCoordinatesPerCell.Add(coords2D, new FootprintCoordinates(coordinates, canHaveFootprint));
				}
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly Dictionary<Vector2Int, FootprintCoordinates> _footprintsCoordinatesPerCell = new Dictionary<Vector2Int, FootprintCoordinates>();

		// Token: 0x04000009 RID: 9
		public readonly ConstructionGuidelinesRenderingService _constructionGuidelinesRenderingService;

		// Token: 0x0400000A RID: 10
		public BlockObject _blockObject;

		// Token: 0x0400000B RID: 11
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x0400000C RID: 12
		public Preview _preview;

		// Token: 0x0400000D RID: 13
		public Vector2Int _min;

		// Token: 0x0400000E RID: 14
		public Vector2Int _max;
	}
}
