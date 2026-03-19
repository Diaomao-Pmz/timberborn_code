using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000049 RID: 73
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class KeysOrValuesCollectionAccessor<TKey, [Nullable(2)] TValue, [Nullable(2)] T> : ICollection<T>, IEnumerable<!2>, IEnumerable, ICollection
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x000099C5 File Offset: 0x00007BC5
		protected KeysOrValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary, IEnumerable<T> keysOrValues)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNull<IEnumerable<T>>(keysOrValues, "keysOrValues");
			this._dictionary = dictionary;
			this._keysOrValues = keysOrValues;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000099F1 File Offset: 0x00007BF1
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000099F4 File Offset: 0x00007BF4
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00009A01 File Offset: 0x00007C01
		protected IImmutableDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00009A09 File Offset: 0x00007C09
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00009A10 File Offset: 0x00007C10
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003AD RID: 941
		public abstract bool Contains(T item);

		// Token: 0x060003AE RID: 942 RVA: 0x00009A18 File Offset: 0x00007C18
		public void CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009AA7 File Offset: 0x00007CA7
		public IEnumerator<T> GetEnumerator()
		{
			return this._keysOrValues.GetEnumerator();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00009AB4 File Offset: 0x00007CB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00009ABC File Offset: 0x00007CBC
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array.SetValue(t, arrayIndex++);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00009B4C File Offset: 0x00007D4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00009B4F File Offset: 0x00007D4F
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400004B RID: 75
		private readonly IImmutableDictionary<TKey, TValue> _dictionary;

		// Token: 0x0400004C RID: 76
		private readonly IEnumerable<T> _keysOrValues;
	}
}
