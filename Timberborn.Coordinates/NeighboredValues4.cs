using System;
using System.Collections.Generic;

namespace Timberborn.Coordinates
{
	// Token: 0x02000010 RID: 16
	public class NeighboredValues4<T>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002D22 File Offset: 0x00000F22
		public bool IsEmpty
		{
			get
			{
				return this._values.Count == 0;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D32 File Offset: 0x00000F32
		public void AddVariants(T value, bool down, bool left, bool up, bool right)
		{
			this.AddVariants(value, NeighboredValues4<T>.BoolToByte(down), NeighboredValues4<T>.BoolToByte(left), NeighboredValues4<T>.BoolToByte(up), NeighboredValues4<T>.BoolToByte(right));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D55 File Offset: 0x00000F55
		public void AddExact(T value, byte down, byte left, byte up, byte right)
		{
			this._values[NeighboredValues4<T>.GetIndex(down, left, up, right)] = new OrientedValue<T>(value, Orientation.Cw0);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D74 File Offset: 0x00000F74
		public OrientedValue<T> GetMatch(bool down, bool left, bool up, bool right)
		{
			OrientedValue<T> result;
			if (this.TryGetMatch(NeighboredValues4<T>.BoolToByte(down), NeighboredValues4<T>.BoolToByte(left), NeighboredValues4<T>.BoolToByte(up), NeighboredValues4<T>.BoolToByte(right), out result))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException(string.Format("Couldn't find value for {0} {1} {2} {3}", new object[]
			{
				down,
				left,
				up,
				right
			}));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public bool TryGetMatch(byte down, byte left, byte up, byte right, out OrientedValue<T> value)
		{
			return this._values.TryGetValue(NeighboredValues4<T>.GetIndex(down, left, up, right), out value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DFC File Offset: 0x00000FFC
		public void AddVariants(T value, byte down, byte left, byte up, byte right)
		{
			this._values[NeighboredValues4<T>.GetIndex(down, left, up, right)] = new OrientedValue<T>(value, Orientation.Cw0);
			this._values[NeighboredValues4<T>.GetIndex(right, down, left, up)] = new OrientedValue<T>(value, Orientation.Cw90);
			this._values[NeighboredValues4<T>.GetIndex(up, right, down, left)] = new OrientedValue<T>(value, Orientation.Cw180);
			this._values[NeighboredValues4<T>.GetIndex(left, up, right, down)] = new OrientedValue<T>(value, Orientation.Cw270);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E7D File Offset: 0x0000107D
		public static byte BoolToByte(bool key)
		{
			return key ? 0 : 1;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E87 File Offset: 0x00001087
		public static long GetIndex(byte down, byte left, byte up, byte right)
		{
			return (long)((ulong)down + (ulong)left * 256UL + (ulong)up * 256UL * 256UL + (ulong)right * 256UL * 256UL * 256UL);
		}

		// Token: 0x04000036 RID: 54
		public readonly Dictionary<long, OrientedValue<T>> _values = new Dictionary<long, OrientedValue<T>>();
	}
}
