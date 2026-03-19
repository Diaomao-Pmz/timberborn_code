using System;

namespace Timberborn.Common
{
	// Token: 0x02000028 RID: 40
	public readonly struct ReadOnlyArray<T>
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000358C File Offset: 0x0000178C
		public ReadOnlyArray(T[] array)
		{
			this._array = array;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003595 File Offset: 0x00001795
		public ReadOnlySpan<T> AsSpan
		{
			get
			{
				return MemoryExtensions.AsSpan<T>(this._array);
			}
		}

		// Token: 0x1700000C RID: 12
		public T this[int index]
		{
			get
			{
				return ref this._array[index];
			}
		}

		// Token: 0x04000041 RID: 65
		public readonly T[] _array;
	}
}
