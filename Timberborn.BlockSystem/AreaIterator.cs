using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000008 RID: 8
	public class AreaIterator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000219B File Offset: 0x0000039B
		public AreaIterator(AreaClamper areaClamper)
		{
			this._areaClamper = areaClamper;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AC File Offset: 0x000003AC
		public IEnumerable<Vector3Int> GetRectangle(Vector3Int start, Vector3Int end, int maxBlocks)
		{
			int maxSize = (int)Math.Sqrt((double)maxBlocks);
			Vector3Int end2;
			end2..ctor(end.x, end.y, start.z);
			return this.GetCuboid(start, end2, maxSize);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E8 File Offset: 0x000003E8
		public IEnumerable<Vector3Int> GetLine(Vector3Int start, Vector3Int end, int maxPoints, out LineDirection direction)
		{
			if (start == end)
			{
				direction = LineDirection.SinglePoint;
				return Enumerables.One<Vector3Int>(start);
			}
			Vector3Int furthestLineEnd = AreaIterator.GetFurthestLineEnd(start, end, out direction);
			return this.GetCuboid(start, furthestLineEnd, maxPoints);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public IEnumerable<Vector3Int> GetLine(Vector3Int start, Vector3Int end, LineDirection initialDirection, int maxPoints, out LineDirection direction)
		{
			if (start == end)
			{
				direction = LineDirection.SinglePoint;
				return Enumerables.One<Vector3Int>(start);
			}
			int num = (initialDirection == LineDirection.Left || initialDirection == LineDirection.Right) ? end.x : start.x;
			int num2 = (initialDirection == LineDirection.Up || initialDirection == LineDirection.Down) ? end.y : start.y;
			Vector3Int vector3Int;
			vector3Int..ctor(num, num2, start.z);
			direction = ((start == vector3Int) ? AreaIterator.GetLineDirection(start, end) : initialDirection);
			return this.GetCuboid(start, vector3Int, maxPoints);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000229F File Offset: 0x0000049F
		public IEnumerable<Vector3Int> GetCuboid(Vector3Int start, Vector3Int end, int maxSize = 0)
		{
			Vector3Int clampedEnd = (maxSize > 0) ? this._areaClamper.ClampEnd(start, end, maxSize) : end;
			int deltaX = (start.x < clampedEnd.x) ? 1 : -1;
			int deltaY = (start.y < clampedEnd.y) ? 1 : -1;
			int deltaZ = (start.z < clampedEnd.z) ? 1 : -1;
			for (int x = start.x; x != clampedEnd.x + deltaX; x += deltaX)
			{
				for (int y = start.y; y != clampedEnd.y + deltaY; y += deltaY)
				{
					for (int z = start.z; z != clampedEnd.z + deltaZ; z += deltaZ)
					{
						yield return new Vector3Int(x, y, z);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C4 File Offset: 0x000004C4
		public static LineDirection GetLineDirection(Vector3Int start, Vector3Int end)
		{
			LineDirection result;
			AreaIterator.GetFurthestLineEnd(start, end, out result);
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022DC File Offset: 0x000004DC
		public static Vector3Int GetFurthestLineEnd(Vector3Int start, Vector3Int end, out LineDirection direction)
		{
			int num = end.x - start.x;
			int num2 = end.y - start.y;
			int num3 = Math.Abs(num);
			int num4 = Math.Abs(num2);
			Vector2Int vector2Int;
			if (num3 > num4)
			{
				vector2Int..ctor(end.x, start.y);
				direction = ((num > 0) ? LineDirection.Right : LineDirection.Left);
			}
			else
			{
				vector2Int..ctor(start.x, end.y);
				direction = ((num2 > 0) ? LineDirection.Up : LineDirection.Down);
			}
			return new Vector3Int(vector2Int.x, vector2Int.y, start.z);
		}

		// Token: 0x04000008 RID: 8
		public readonly AreaClamper _areaClamper;
	}
}
