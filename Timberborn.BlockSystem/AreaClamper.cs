using System;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000007 RID: 7
	public class AreaClamper
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public Vector3Int ClampEnd(Vector3Int start, Vector3Int end, int maxSize)
		{
			int num = Math.Sign(end.x - start.x);
			int num2 = Math.Sign(end.y - start.y);
			int val = Math.Abs(end.x - start.x) + 1;
			int val2 = Math.Abs(end.y - start.y) + 1;
			int num3 = Math.Min(val, maxSize) - 1;
			int num4 = Math.Min(val2, maxSize) - 1;
			return new Vector3Int(start.x + num3 * num, start.y + num4 * num2, end.z);
		}
	}
}
