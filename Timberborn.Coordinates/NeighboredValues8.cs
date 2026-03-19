using System;
using System.Collections.Generic;

namespace Timberborn.Coordinates
{
	// Token: 0x02000012 RID: 18
	public class NeighboredValues8<T>
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000030E0 File Offset: 0x000012E0
		public void AddVariants(T value, bool down, bool downLeft, bool left, bool upLeft, bool up, bool upRight, bool right, bool downRight)
		{
			this._values[(long)NeighboredValues8<T>.GetIndex(down, downLeft, left, upLeft, up, upRight, right, downRight)] = new OrientedValue<T>(value, Orientation.Cw0);
			this._values[(long)NeighboredValues8<T>.GetIndex(right, downRight, down, downLeft, left, upLeft, up, upRight)] = new OrientedValue<T>(value, Orientation.Cw90);
			this._values[(long)NeighboredValues8<T>.GetIndex(up, upRight, right, downRight, down, downLeft, left, upLeft)] = new OrientedValue<T>(value, Orientation.Cw180);
			this._values[(long)NeighboredValues8<T>.GetIndex(left, upLeft, up, upRight, right, downRight, down, downLeft)] = new OrientedValue<T>(value, Orientation.Cw270);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003188 File Offset: 0x00001388
		public void AddExact(T value, bool down, bool downLeft, bool left, bool upLeft, bool up, bool upRight, bool right, bool downRight)
		{
			this._values[(long)NeighboredValues8<T>.GetIndex(down, downLeft, left, upLeft, up, upRight, right, downRight)] = new OrientedValue<T>(value, Orientation.Cw0);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000031BC File Offset: 0x000013BC
		public OrientedValue<T> GetMatch(bool down, bool downLeft, bool left, bool upLeft, bool up, bool upRight, bool right, bool downRight)
		{
			return this._values[(long)NeighboredValues8<T>.GetIndex(down, downLeft, left, upLeft, up, upRight, right, downRight)];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000031E8 File Offset: 0x000013E8
		public static int GetIndex(bool down, bool downLeft, bool left, bool upLeft, bool up, bool upRight, bool right, bool downRight)
		{
			return NeighboredValues8<T>.BoolToInt(down) + NeighboredValues8<T>.BoolToInt(downLeft) * 2 + NeighboredValues8<T>.BoolToInt(left) * 4 + NeighboredValues8<T>.BoolToInt(upLeft) * 8 + NeighboredValues8<T>.BoolToInt(up) * 16 + NeighboredValues8<T>.BoolToInt(upRight) * 32 + NeighboredValues8<T>.BoolToInt(right) * 64 + NeighboredValues8<T>.BoolToInt(downRight) * 128;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003245 File Offset: 0x00001445
		public static int BoolToInt(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x04000038 RID: 56
		public readonly Dictionary<long, OrientedValue<T>> _values = new Dictionary<long, OrientedValue<T>>();
	}
}
