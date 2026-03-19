using System;
using System.Collections.Generic;

namespace Timberborn.Common
{
	// Token: 0x02000029 RID: 41
	public readonly struct ReadOnlyHashSet<T>
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000035B7 File Offset: 0x000017B7
		public ReadOnlyHashSet(HashSet<T> set)
		{
			this._set = set;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000035C0 File Offset: 0x000017C0
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000035CD File Offset: 0x000017CD
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000035DA File Offset: 0x000017DA
		public bool Contains(T item)
		{
			return this._set.Contains(item);
		}

		// Token: 0x04000042 RID: 66
		public readonly HashSet<T> _set;
	}
}
