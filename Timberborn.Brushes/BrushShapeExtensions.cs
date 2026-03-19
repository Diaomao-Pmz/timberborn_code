using System;

namespace Timberborn.Brushes
{
	// Token: 0x02000007 RID: 7
	public static class BrushShapeExtensions
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021BF File Offset: 0x000003BF
		public static string GetLocKey(this BrushShape brushShape)
		{
			if (brushShape == BrushShape.Square)
			{
				return BrushShapeExtensions.SquareLocKey;
			}
			if (brushShape != BrushShape.Round)
			{
				throw new ArgumentOutOfRangeException("brushShape", brushShape, null);
			}
			return BrushShapeExtensions.RoundLocKey;
		}

		// Token: 0x0400000D RID: 13
		public static readonly string RoundLocKey = "MapEditor.Brush.Shape.Round";

		// Token: 0x0400000E RID: 14
		public static readonly string SquareLocKey = "MapEditor.Brush.Shape.Square";
	}
}
