using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Coordinates
{
	// Token: 0x02000013 RID: 19
	public static class NeighbourFinder
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003260 File Offset: 0x00001460
		public static IEnumerable<Vector2Int> GetSpiralNeighboursXY(int range)
		{
			int num = range * 2 + 1;
			int neighbours = num * num - 1;
			int x = 0;
			int y = 0;
			int dx = 0;
			int dy = -1;
			int num3;
			for (int i = 0; i < neighbours; i = num3 + 1)
			{
				if (x == y || (x < 0 && x == -y) || (x > 0 && x == 1 - y))
				{
					int num2 = -dy;
					num3 = dx;
					dx = num2;
					dy = num3;
				}
				x += dx;
				y += dy;
				yield return new Vector2Int(x, y);
				num3 = i;
			}
			yield break;
		}
	}
}
