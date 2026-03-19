using System;
using System.Collections.Generic;

namespace Timberborn.Coordinates
{
	// Token: 0x02000011 RID: 17
	public class NeighboredValues6<T>
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002ED1 File Offset: 0x000010D1
		public void AddVariants(T value, bool down, bool left, bool up, bool right, bool top, bool bottom)
		{
			this.AddVariants(value, NeighboredValues6<T>.BoolToByte(down), NeighboredValues6<T>.BoolToByte(left), NeighboredValues6<T>.BoolToByte(up), NeighboredValues6<T>.BoolToByte(right), NeighboredValues6<T>.BoolToByte(top), NeighboredValues6<T>.BoolToByte(bottom));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F04 File Offset: 0x00001104
		public OrientedValue<T> GetMatch(bool down, bool left, bool up, bool right, bool top, bool bottom)
		{
			OrientedValue<T> result;
			if (this.TryGetMatch(NeighboredValues6<T>.BoolToByte(down), NeighboredValues6<T>.BoolToByte(left), NeighboredValues6<T>.BoolToByte(up), NeighboredValues6<T>.BoolToByte(right), NeighboredValues6<T>.BoolToByte(top), NeighboredValues6<T>.BoolToByte(bottom), out result))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException(string.Format("Couldn't find value for {0} {1} {2} {3} {4} {5}", new object[]
			{
				down,
				left,
				up,
				right,
				top,
				bottom
			}));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F92 File Offset: 0x00001192
		public bool TryGetMatch(byte down, byte left, byte up, byte right, byte top, byte bottom, out OrientedValue<T> value)
		{
			return this._values.TryGetValue(NeighboredValues6<T>.GetIndex(down, left, up, right, top, bottom), out value);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002FB0 File Offset: 0x000011B0
		public void AddVariants(T value, byte down, byte left, byte up, byte right, byte top, byte bottom)
		{
			this._values[NeighboredValues6<T>.GetIndex(down, left, up, right, top, bottom)] = new OrientedValue<T>(value, Orientation.Cw0);
			this._values[NeighboredValues6<T>.GetIndex(right, down, left, up, top, bottom)] = new OrientedValue<T>(value, Orientation.Cw90);
			this._values[NeighboredValues6<T>.GetIndex(up, right, down, left, top, bottom)] = new OrientedValue<T>(value, Orientation.Cw180);
			this._values[NeighboredValues6<T>.GetIndex(left, up, right, down, top, bottom)] = new OrientedValue<T>(value, Orientation.Cw270);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E7D File Offset: 0x0000107D
		public static byte BoolToByte(bool key)
		{
			return key ? 0 : 1;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003044 File Offset: 0x00001244
		public static long GetIndex(byte down, byte left, byte up, byte right, byte top, byte bottom)
		{
			return (long)((ulong)down + (ulong)left * 256UL + (ulong)up * 256UL * 256UL + (ulong)right * 256UL * 256UL * 256UL + (ulong)top * 256UL * 256UL * 256UL * 256UL + (ulong)bottom * 256UL * 256UL * 256UL * 256UL * 256UL);
		}

		// Token: 0x04000037 RID: 55
		public readonly Dictionary<long, OrientedValue<T>> _values = new Dictionary<long, OrientedValue<T>>();
	}
}
