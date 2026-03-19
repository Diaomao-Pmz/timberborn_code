using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystemUI
{
	// Token: 0x02000006 RID: 6
	public class BlockObjectSelectionDrawer
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000211A File Offset: 0x0000031A
		public BlockObjectSelectionDrawer(RectangleBoundsDrawer rectangleBoundsDrawer, RollingHighlighter rollingHighlighter, Color blockObjectHighlightColor, MeasurableAreaDrawer measurableAreaDrawer)
		{
			this._rectangleBoundsDrawer = rectangleBoundsDrawer;
			this._rollingHighlighter = rollingHighlighter;
			this._blockObjectHighlightColor = blockObjectHighlightColor;
			this._measurableAreaDrawer = measurableAreaDrawer;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213F File Offset: 0x0000033F
		public void Draw(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectingArea)
		{
			this._start = start;
			this._end = end;
			this._selectingArea = selectingArea;
			this.Draw();
			this._rollingHighlighter.HighlightPrimary(blockObjects, this._blockObjectHighlightColor);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000216F File Offset: 0x0000036F
		public void StopDrawing()
		{
			this._rollingHighlighter.UnhighlightAllPrimary();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217C File Offset: 0x0000037C
		public void Draw()
		{
			if (this._selectingArea)
			{
				this._rectangleBoundsDrawer.DrawOnLevel(this._start.XY(), this._end.XY(), this._start.z);
				this.DrawAreaMeasurement();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		public void DrawAreaMeasurement()
		{
			ValueTuple<Vector2Int, Vector2Int> valueTuple = Vectors.MinMax(this._start.XY(), this._end.XY());
			Vector2Int item = valueTuple.Item1;
			Vector2Int item2 = valueTuple.Item2;
			for (int i = item.x; i <= item2.x; i++)
			{
				for (int j = item.y; j <= item2.y; j++)
				{
					this._measurableAreaDrawer.AddMeasurableCoordinates(new Vector3Int(i, j, 0));
				}
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly RectangleBoundsDrawer _rectangleBoundsDrawer;

		// Token: 0x04000008 RID: 8
		public readonly RollingHighlighter _rollingHighlighter;

		// Token: 0x04000009 RID: 9
		public readonly Color _blockObjectHighlightColor;

		// Token: 0x0400000A RID: 10
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;

		// Token: 0x0400000B RID: 11
		public Vector3Int _start;

		// Token: 0x0400000C RID: 12
		public Vector3Int _end;

		// Token: 0x0400000D RID: 13
		public bool _selectingArea;
	}
}
