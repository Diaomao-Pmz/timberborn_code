using System;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x0200000A RID: 10
	public readonly struct PackedList<T>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028D6 File Offset: 0x00000AD6
		public T[] Array { get; }

		// Token: 0x06000030 RID: 48 RVA: 0x000028DE File Offset: 0x00000ADE
		public PackedList(T[] array)
		{
			this.Array = array;
		}
	}
}
