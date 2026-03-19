using System;

namespace Timberborn.Coordinates
{
	// Token: 0x02000018 RID: 24
	public readonly struct OrientedValue<T>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000391A File Offset: 0x00001B1A
		public T Value { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003922 File Offset: 0x00001B22
		public Orientation Orientation { get; }

		// Token: 0x06000065 RID: 101 RVA: 0x0000392A File Offset: 0x00001B2A
		public OrientedValue(T value, Orientation orientation)
		{
			this.Value = value;
			this.Orientation = orientation;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Deconstruct(out T value, out Orientation orientation)
		{
			value = this.Value;
			orientation = this.Orientation;
		}
	}
}
