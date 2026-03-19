using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x02000014 RID: 20
	public class CyclicBuffer<T>
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002BEF File Offset: 0x00000DEF
		public CyclicBuffer(int size)
		{
			this._queue = new Queue<T>(size);
			this._size = size;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C0A File Offset: 0x00000E0A
		public IEnumerable<T> Values
		{
			get
			{
				return this._queue.AsReadOnlyEnumerable<T>();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C17 File Offset: 0x00000E17
		public void Add(T value)
		{
			if (this._queue.Count == this._size)
			{
				this._queue.Dequeue();
			}
			this._queue.Enqueue(value);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C44 File Offset: 0x00000E44
		public void AddRange(IEnumerable<T> values)
		{
			foreach (T value in values)
			{
				this.Add(value);
			}
		}

		// Token: 0x0400002B RID: 43
		public readonly Queue<T> _queue;

		// Token: 0x0400002C RID: 44
		public readonly int _size;
	}
}
