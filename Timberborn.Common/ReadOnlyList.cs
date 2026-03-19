using System;
using System.Collections;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x0200002B RID: 43
	public readonly struct ReadOnlyList<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003603 File Offset: 0x00001803
		public ReadOnlyList(List<T> list)
		{
			this._list = list;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000360C File Offset: 0x0000180C
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700000F RID: 15
		public T this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003627 File Offset: 0x00001827
		public List<T>.Enumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003634 File Offset: 0x00001834
		public bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003642 File Offset: 0x00001842
		public bool IsEmpty()
		{
			return this._list.Count == 0;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003652 File Offset: 0x00001852
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003652 File Offset: 0x00001852
		public IEnumerator GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000044 RID: 68
		public readonly List<T> _list;
	}
}
