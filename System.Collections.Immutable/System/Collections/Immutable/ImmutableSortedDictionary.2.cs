using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableSortedDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<!0, !1>, IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISortKeyCollection<TKey>, IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IDictionary, ICollection
	{
		// Token: 0x060002EE RID: 750 RVA: 0x000081F1 File Offset: 0x000063F1
		internal ImmutableSortedDictionary([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer = null, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer = null)
		{
			this._keyComparer = (keyComparer ?? Comparer<TKey>.Default);
			this._valueComparer = (valueComparer ?? EqualityComparer<TValue>.Default);
			this._root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00008224 File Offset: 0x00006424
		private ImmutableSortedDictionary(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
			Requires.Range(count >= 0, "count", null);
			Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			root.Freeze();
			this._root = root;
			this._count = count;
			this._keyComparer = keyComparer;
			this._valueComparer = valueComparer;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000828E File Offset: 0x0000648E
		public ImmutableSortedDictionary<TKey, TValue> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this._keyComparer, this._valueComparer);
			}
			return this;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000082B5 File Offset: 0x000064B5
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._valueComparer;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000082BD File Offset: 0x000064BD
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000082CA File Offset: 0x000064CA
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000082D2 File Offset: 0x000064D2
		public IEnumerable<TKey> Keys
		{
			get
			{
				return this._root.Keys;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000082DF File Offset: 0x000064DF
		public IEnumerable<TValue> Values
		{
			get
			{
				return this._root.Values;
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000082EC File Offset: 0x000064EC
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000082F4 File Offset: 0x000064F4
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000082FC File Offset: 0x000064FC
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008304 File Offset: 0x00006504
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00008307 File Offset: 0x00006507
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this._keyComparer;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000830F File Offset: 0x0000650F
		[Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		internal ImmutableSortedDictionary<TKey, TValue>.Node Root
		{
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000082 RID: 130
		public TValue this[TKey key]
		{
			get
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (!this.TryGetValue(key, out result))
				{
					ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
				}
				return result;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00008342 File Offset: 0x00006542
		public ref readonly TValue ValueRef(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ValueRef(key, this._keyComparer);
		}

		// Token: 0x17000083 RID: 131
		TValue IDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008371 File Offset: 0x00006571
		[return: Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000837C File Offset: 0x0000657C
		public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
			return this.Wrap(root, this._count + 1);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000083C0 File Offset: 0x000065C0
		public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			bool flag;
			bool flag2;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
			return this.Wrap(root, flag ? this._count : (this._count + 1));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008410 File Offset: 0x00006610
		public ImmutableSortedDictionary<TKey, TValue> SetItems([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, true, false);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008426 File Offset: 0x00006626
		public ImmutableSortedDictionary<TKey, TValue> AddRange([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return this.AddRange(items, false, false);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000843C File Offset: 0x0000663C
		public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
		{
			Requires.NotNullAllowStructs<TKey>(value, "value");
			bool flag;
			ImmutableSortedDictionary<TKey, TValue>.Node root = this._root.Remove(value, this._keyComparer, out flag);
			return this.Wrap(root, this._count - 1);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008478 File Offset: 0x00006678
		public ImmutableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (TKey key in keys)
			{
				bool flag;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = node.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					node = node2;
					num--;
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000084F8 File Offset: 0x000066F8
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = Comparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (keyComparer != this._keyComparer)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode, 0, keyComparer, valueComparer).AddRange(this, false, true);
			}
			if (valueComparer == this._valueComparer)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(this._root, this._count, this._keyComparer, valueComparer);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000855B File Offset: 0x0000675B
		public ImmutableSortedDictionary<TKey, TValue> WithComparers([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._valueComparer);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000856A File Offset: 0x0000676A
		public bool ContainsValue(TValue value)
		{
			return this._root.ContainsValue(value, this._valueComparer);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000857E File Offset: 0x0000677E
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008588 File Offset: 0x00006788
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008592 File Offset: 0x00006792
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000859B File Offset: 0x0000679B
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000085A4 File Offset: 0x000067A4
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000085AD File Offset: 0x000067AD
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000085B6 File Offset: 0x000067B6
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.ContainsKey(key, this._keyComparer);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000085D5 File Offset: 0x000067D5
		public bool Contains([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> pair)
		{
			return this._root.Contains(pair, this._keyComparer, this._valueComparer);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000085EF File Offset: 0x000067EF
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return this._root.TryGetValue(key, this._keyComparer, out value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000860F File Offset: 0x0000680F
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return this._root.TryGetKey(equalKey, this._keyComparer, out actualKey);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000862F File Offset: 0x0000682F
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008636 File Offset: 0x00006836
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000863D File Offset: 0x0000683D
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008644 File Offset: 0x00006844
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000864B File Offset: 0x0000684B
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008654 File Offset: 0x00006854
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000086E0 File Offset: 0x000068E0
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000086E3 File Offset: 0x000068E3
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000086E6 File Offset: 0x000068E6
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000086EE File Offset: 0x000068EE
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000086F6 File Offset: 0x000068F6
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000086FD File Offset: 0x000068FD
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000870B File Offset: 0x0000690B
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000871D File Offset: 0x0000691D
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000088 RID: 136
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000873E File Offset: 0x0000693E
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008745 File Offset: 0x00006945
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index, this.Count);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000875A File Offset: 0x0000695A
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000875D File Offset: 0x0000695D
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008760 File Offset: 0x00006960
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000878D File Offset: 0x0000698D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000879A File Offset: 0x0000699A
		[NullableContext(0)]
		public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000087A7 File Offset: 0x000069A7
		private static ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int count, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000087C8 File Offset: 0x000069C8
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, [NotNullWhen(true)] out ImmutableSortedDictionary<TKey, TValue> other)
		{
			other = (sequence as ImmutableSortedDictionary<TKey, TValue>);
			if (other != null)
			{
				return true;
			}
			ImmutableSortedDictionary<TKey, TValue>.Builder builder = sequence as ImmutableSortedDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000087F8 File Offset: 0x000069F8
		private ImmutableSortedDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision, bool avoidToSortedMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			if (this.IsEmpty && !avoidToSortedMap)
			{
				return this.FillFromEmpty(items, overwriteOnCollision);
			}
			ImmutableSortedDictionary<TKey, TValue>.Node node = this._root;
			int num = this._count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				bool flag = false;
				bool flag2;
				ImmutableSortedDictionary<TKey, TValue>.Node node2 = overwriteOnCollision ? node.SetItem(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag, out flag2) : node.Add(keyValuePair.Key, keyValuePair.Value, this._keyComparer, this._valueComparer, out flag2);
				if (flag2)
				{
					node = node2;
					if (!flag)
					{
						num++;
					}
				}
			}
			return this.Wrap(node, num);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000088D0 File Offset: 0x00006AD0
		private ImmutableSortedDictionary<TKey, TValue> Wrap(ImmutableSortedDictionary<TKey, TValue>.Node root, int adjustedCountIfDifferentRoot)
		{
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedDictionary<TKey, TValue>(root, adjustedCountIfDifferentRoot, this._keyComparer, this._valueComparer);
			}
			return this.Clear();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008900 File Offset: 0x00006B00
		private ImmutableSortedDictionary<TKey, TValue> FillFromEmpty(IEnumerable<KeyValuePair<TKey, TValue>> items, bool overwriteOnCollision)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary;
			if (ImmutableSortedDictionary<TKey, TValue>.TryCastToImmutableMap(items, out immutableSortedDictionary))
			{
				return immutableSortedDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			IDictionary<TKey, TValue> dictionary = items as IDictionary<!0, !1>;
			SortedDictionary<TKey, TValue> sortedDictionary;
			if (dictionary != null)
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(dictionary, this.KeyComparer);
			}
			else
			{
				sortedDictionary = new SortedDictionary<TKey, TValue>(this.KeyComparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					TValue x;
					if (overwriteOnCollision)
					{
						sortedDictionary[keyValuePair.Key] = keyValuePair.Value;
					}
					else if (sortedDictionary.TryGetValue(keyValuePair.Key, out x))
					{
						if (!this._valueComparer.Equals(x, keyValuePair.Value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, keyValuePair.Key));
						}
					}
					else
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			if (sortedDictionary.Count == 0)
			{
				return this;
			}
			return new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromSortedDictionary(sortedDictionary), sortedDictionary.Count, this.KeyComparer, this.ValueComparer);
		}

		// Token: 0x0400003E RID: 62
		public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>(null, null);

		// Token: 0x0400003F RID: 63
		private readonly ImmutableSortedDictionary<TKey, TValue>.Node _root;

		// Token: 0x04000040 RID: 64
		private readonly int _count;

		// Token: 0x04000041 RID: 65
		private readonly IComparer<TKey> _keyComparer;

		// Token: 0x04000042 RID: 66
		private readonly IEqualityComparer<TValue> _valueComparer;

		// Token: 0x020000B6 RID: 182
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
		public sealed class Builder : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x06000776 RID: 1910 RVA: 0x00013920 File Offset: 0x00011B20
			internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._keyComparer = map.KeyComparer;
				this._valueComparer = map.ValueComparer;
				this._count = map.Count;
				this._immutable = map;
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000777 RID: 1911 RVA: 0x00013996 File Offset: 0x00011B96
			ICollection<TKey> IDictionary<!0, !1>.Keys
			{
				get
				{
					return this.Root.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000778 RID: 1912 RVA: 0x000139AE File Offset: 0x00011BAE
			public IEnumerable<TKey> Keys
			{
				get
				{
					return this.Root.Keys;
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000779 RID: 1913 RVA: 0x000139BB File Offset: 0x00011BBB
			ICollection<TValue> IDictionary<!0, !1>.Values
			{
				get
				{
					return this.Root.Values.ToArray(this.Count);
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x0600077A RID: 1914 RVA: 0x000139D3 File Offset: 0x00011BD3
			public IEnumerable<TValue> Values
			{
				get
				{
					return this.Root.Values;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x000139E0 File Offset: 0x00011BE0
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600077C RID: 1916 RVA: 0x000139E8 File Offset: 0x00011BE8
			bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x0600077D RID: 1917 RVA: 0x000139EB File Offset: 0x00011BEB
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600077E RID: 1918 RVA: 0x000139F3 File Offset: 0x00011BF3
			// (set) Token: 0x0600077F RID: 1919 RVA: 0x000139FB File Offset: 0x00011BFB
			[Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			private ImmutableSortedDictionary<TKey, TValue>.Node Root
			{
				get
				{
					return this._root;
				}
				set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x17000171 RID: 369
			public TValue this[TKey key]
			{
				get
				{
					TValue result;
					if (!this.TryGetValue(key, out result))
					{
						ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
					}
					return result;
				}
				set
				{
					bool flag;
					bool flag2;
					this.Root = this._root.SetItem(key, value, this._keyComparer, this._valueComparer, out flag, out flag2);
					if (flag2 && !flag)
					{
						this._count++;
					}
				}
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x00013A88 File Offset: 0x00011C88
			public ref readonly TValue ValueRef(TKey key)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				return this._root.ValueRef(key, this._keyComparer);
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000783 RID: 1923 RVA: 0x00013AA7 File Offset: 0x00011CA7
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x00013AAA File Offset: 0x00011CAA
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000785 RID: 1925 RVA: 0x00013AAD File Offset: 0x00011CAD
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x00013AC0 File Offset: 0x00011CC0
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000787 RID: 1927 RVA: 0x00013AD3 File Offset: 0x00011CD3
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
					return this._syncRoot;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000788 RID: 1928 RVA: 0x00013AF5 File Offset: 0x00011CF5
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000789 RID: 1929 RVA: 0x00013AF8 File Offset: 0x00011CF8
			// (set) Token: 0x0600078A RID: 1930 RVA: 0x00013B00 File Offset: 0x00011D00
			public IComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
				set
				{
					Requires.NotNull<IComparer<TKey>>(value, "value");
					if (value != this._keyComparer)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
						int num = 0;
						foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
						{
							bool flag;
							node = node.Add(keyValuePair.Key, keyValuePair.Value, value, this._valueComparer, out flag);
							if (flag)
							{
								num++;
							}
						}
						this._keyComparer = value;
						this.Root = node;
						this._count = num;
					}
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x0600078B RID: 1931 RVA: 0x00013BA0 File Offset: 0x00011DA0
			// (set) Token: 0x0600078C RID: 1932 RVA: 0x00013BA8 File Offset: 0x00011DA8
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this._valueComparer)
					{
						this._valueComparer = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x00013BCC File Offset: 0x00011DCC
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x00013BE0 File Offset: 0x00011DE0
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x00013BEE File Offset: 0x00011DEE
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x00013C00 File Offset: 0x00011E00
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x1700017A RID: 378
			[Nullable(2)]
			object IDictionary.this[object key]
			{
				get
				{
					return this[(TKey)((object)key)];
				}
				set
				{
					this[(TKey)((object)key)] = (TValue)((object)value);
				}
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x00013C36 File Offset: 0x00011E36
			void ICollection.CopyTo(Array array, int index)
			{
				this.Root.CopyTo(array, index, this.Count);
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x00013C4C File Offset: 0x00011E4C
			public void Add(TKey key, TValue value)
			{
				bool flag;
				this.Root = this.Root.Add(key, value, this._keyComparer, this._valueComparer, out flag);
				if (flag)
				{
					this._count++;
				}
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x00013C8B File Offset: 0x00011E8B
			public bool ContainsKey(TKey key)
			{
				return this.Root.ContainsKey(key, this._keyComparer);
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x00013CA0 File Offset: 0x00011EA0
			public bool Remove(TKey key)
			{
				bool flag;
				this.Root = this.Root.Remove(key, this._keyComparer, out flag);
				if (flag)
				{
					this._count--;
				}
				return flag;
			}

			// Token: 0x06000797 RID: 1943 RVA: 0x00013CD9 File Offset: 0x00011ED9
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return this.Root.TryGetValue(key, this._keyComparer, out value);
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x00013CEE File Offset: 0x00011EEE
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				return this.Root.TryGetKey(equalKey, this._keyComparer, out actualKey);
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x00013D0E File Offset: 0x00011F0E
			public void Add([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x00013D24 File Offset: 0x00011F24
			public void Clear()
			{
				this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				this._count = 0;
			}

			// Token: 0x0600079B RID: 1947 RVA: 0x00013D38 File Offset: 0x00011F38
			public bool Contains([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> item)
			{
				return this.Root.Contains(item, this._keyComparer, this._valueComparer);
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x00013D52 File Offset: 0x00011F52
			void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex, this.Count);
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x00013D67 File Offset: 0x00011F67
			public bool Remove([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x00013D81 File Offset: 0x00011F81
			[return: Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x00013D8F File Offset: 0x00011F8F
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x00013D9C File Offset: 0x00011F9C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x00013DA9 File Offset: 0x00011FA9
			public bool ContainsValue(TValue value)
			{
				return this._root.ContainsValue(value, this._valueComparer);
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x00013DC0 File Offset: 0x00011FC0
			public void AddRange([Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
				foreach (KeyValuePair<TKey, TValue> item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x00013E14 File Offset: 0x00012014
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey key in keys)
				{
					this.Remove(key);
				}
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x00013E68 File Offset: 0x00012068
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x00013E88 File Offset: 0x00012088
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				return defaultValue;
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x00013EB0 File Offset: 0x000120B0
			public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
			{
				ImmutableSortedDictionary<TKey, TValue> result;
				if ((result = this._immutable) == null)
				{
					result = (this._immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this._count, this._keyComparer, this._valueComparer));
				}
				return result;
			}

			// Token: 0x04000121 RID: 289
			private ImmutableSortedDictionary<TKey, TValue>.Node _root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;

			// Token: 0x04000122 RID: 290
			private IComparer<TKey> _keyComparer = Comparer<TKey>.Default;

			// Token: 0x04000123 RID: 291
			private IEqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

			// Token: 0x04000124 RID: 292
			private int _count;

			// Token: 0x04000125 RID: 293
			private ImmutableSortedDictionary<TKey, TValue> _immutable;

			// Token: 0x04000126 RID: 294
			private int _version;

			// Token: 0x04000127 RID: 295
			private object _syncRoot;
		}

		// Token: 0x020000B7 RID: 183
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x060007A7 RID: 1959 RVA: 0x00013EF0 File Offset: 0x000120F0
			internal Enumerator([Nullable(new byte[]
			{
				1,
				0,
				0
			})] ImmutableSortedDictionary<TKey, TValue>.Node root, [Nullable(new byte[]
			{
				2,
				0,
				0
			})] ImmutableSortedDictionary<TKey, TValue>.Builder builder = null)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>.TryTake(this, out this._stack))
					{
						this._stack = SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00013F89 File Offset: 0x00012189
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00013FAA File Offset: 0x000121AA
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060007AA RID: 1962 RVA: 0x00013FB2 File Offset: 0x000121B2
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x00013FC0 File Offset: 0x000121C0
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					SecureObjectPool<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x00014014 File Offset: 0x00012214
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						ImmutableSortedDictionary<TKey, TValue>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x00014074 File Offset: 0x00012274
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._current = null;
				if (this._stack != null)
				{
					this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this).ClearFastWhenEmpty<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x000140CF File Offset: 0x000122CF
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
				}
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x000140FA File Offset: 0x000122FA
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x00014124 File Offset: 0x00012324
			private void PushLeft(ImmutableSortedDictionary<TKey, TValue>.Node node)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>> stack = this._stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>(node));
					node = node.Left;
				}
			}

			// Token: 0x04000128 RID: 296
			private readonly ImmutableSortedDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x04000129 RID: 297
			private readonly int _poolUserId;

			// Token: 0x0400012A RID: 298
			private ImmutableSortedDictionary<TKey, TValue>.Node _root;

			// Token: 0x0400012B RID: 299
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedDictionary<TKey, TValue>.Node>>> _stack;

			// Token: 0x0400012C RID: 300
			private ImmutableSortedDictionary<TKey, TValue>.Node _current;

			// Token: 0x0400012D RID: 301
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020000B8 RID: 184
		[Nullable(0)]
		[DebuggerDisplay("{_key} = {_value}")]
		internal sealed class Node : IBinaryTree<KeyValuePair<TKey, TValue>>, IBinaryTree, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
		{
			// Token: 0x060007B1 RID: 1969 RVA: 0x00014167 File Offset: 0x00012367
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x00014178 File Offset: 0x00012378
			private Node(TKey key, TValue value, ImmutableSortedDictionary<TKey, TValue>.Node left, ImmutableSortedDictionary<TKey, TValue>.Node right, bool frozen = false)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(right, "right");
				this._key = key;
				this._value = value;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._frozen = frozen;
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000141ED File Offset: 0x000123ED
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000141F8 File Offset: 0x000123F8
			[Nullable(new byte[]
			{
				2,
				0,
				1,
				1
			})]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<!0, !1>>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00014200 File Offset: 0x00012400
			[Nullable(new byte[]
			{
				2,
				0,
				1,
				1
			})]
			IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<!0, !1>>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00014208 File Offset: 0x00012408
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00014210 File Offset: 0x00012410
			[Nullable(new byte[]
			{
				2,
				0,
				0
			})]
			public ImmutableSortedDictionary<TKey, TValue>.Node Left
			{
				[return: Nullable(new byte[]
				{
					2,
					0,
					0
				})]
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00014218 File Offset: 0x00012418
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00014220 File Offset: 0x00012420
			[Nullable(new byte[]
			{
				2,
				0,
				0
			})]
			public ImmutableSortedDictionary<TKey, TValue>.Node Right
			{
				[return: Nullable(new byte[]
				{
					2,
					0,
					0
				})]
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x060007BA RID: 1978 RVA: 0x00014228 File Offset: 0x00012428
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x060007BB RID: 1979 RVA: 0x00014230 File Offset: 0x00012430
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public KeyValuePair<TKey, TValue> Value
			{
				[return: Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x060007BC RID: 1980 RVA: 0x00014243 File Offset: 0x00012443
			int IBinaryTree.Count
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001424A File Offset: 0x0001244A
			internal IEnumerable<TKey> Keys
			{
				get
				{
					return from p in this
					select p.Key;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x060007BE RID: 1982 RVA: 0x00014271 File Offset: 0x00012471
			internal IEnumerable<TValue> Values
			{
				get
				{
					return from p in this
					select p.Value;
				}
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x00014298 File Offset: 0x00012498
			[NullableContext(0)]
			public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, null);
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x000142A1 File Offset: 0x000124A1
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x000142AE File Offset: 0x000124AE
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x000142BB File Offset: 0x000124BB
			[NullableContext(0)]
			internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator([Nullable(new byte[]
			{
				1,
				0,
				0
			})] ImmutableSortedDictionary<TKey, TValue>.Builder builder)
			{
				return new ImmutableSortedDictionary<TKey, TValue>.Enumerator(this, builder);
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x000142C4 File Offset: 0x000124C4
			internal void CopyTo([Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue>[] array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x060007C4 RID: 1988 RVA: 0x0001434C File Offset: 0x0001254C
			internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + dictionarySize, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x000143F8 File Offset: 0x000125F8
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(SortedDictionary<TKey, TValue> dictionary)
			{
				Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, "dictionary");
				IOrderedCollection<KeyValuePair<TKey, TValue>> orderedCollection = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
				return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x00014424 File Offset: 0x00012624
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Add(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				bool flag;
				return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out flag, out mutated);
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x00014463 File Offset: 0x00012663
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x00014497 File Offset: 0x00012697
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return this.RemoveRecursive(key, keyComparer, out mutated);
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x000144B8 File Offset: 0x000126B8
			internal ref readonly TValue ValueRef(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
				}
				return ref node._value;
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x000144EC File Offset: 0x000126EC
			internal bool TryGetValue(TKey key, IComparer<TKey> keyComparer, [MaybeNullWhen(false)] out TValue value)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
				if (node.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				value = node._value;
				return true;
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x00014538 File Offset: 0x00012738
			internal bool TryGetKey(TKey equalKey, IComparer<TKey> keyComparer, out TKey actualKey)
			{
				Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(equalKey, keyComparer);
				if (node.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				actualKey = node._key;
				return true;
			}

			// Token: 0x060007CC RID: 1996 RVA: 0x00014582 File Offset: 0x00012782
			internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				return !this.Search(key, keyComparer).IsEmpty;
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x000145AC File Offset: 0x000127AC
			internal bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (valueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060007CE RID: 1998 RVA: 0x00014618 File Offset: 0x00012818
			internal bool Contains([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> pair, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNullAllowStructs<TKey>(pair.Key, "Key");
				Requires.NotNull<IComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
				return !node.IsEmpty && valueComparer.Equals(node._value, pair.Value);
			}

			// Token: 0x060007CF RID: 1999 RVA: 0x00014678 File Offset: 0x00012878
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x000146A0 File Offset: 0x000128A0
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x060007D1 RID: 2001 RVA: 0x000146E4 File Offset: 0x000128E4
			private static ImmutableSortedDictionary<TKey, TValue>.Node RotateRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x00014726 File Offset: 0x00012926
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleLeft(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree.Mutate(null, ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree._right)));
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x00014759 File Offset: 0x00012959
			private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleRight(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree.Mutate(ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree._left), null));
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x0001478C File Offset: 0x0001298C
			private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x000147B0 File Offset: 0x000129B0
			private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x000147C9 File Offset: 0x000129C9
			private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x000147E4 File Offset: 0x000129E4
			private static ImmutableSortedDictionary<TKey, TValue>.Node MakeBalanced(ImmutableSortedDictionary<TKey, TValue>.Node tree)
			{
				Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, "tree");
				if (ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree);
					}
					return ImmutableSortedDictionary<TKey, TValue>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x00014848 File Offset: 0x00012A48
			private static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromList(IOrderedCollection<KeyValuePair<TKey, TValue>> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<KeyValuePair<TKey, TValue>>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedDictionary<TKey, TValue>.Node left = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedDictionary<TKey, TValue>.Node right = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				KeyValuePair<TKey, TValue> keyValuePair = items[start + num2];
				return new ImmutableSortedDictionary<TKey, TValue>.Node(keyValuePair.Key, keyValuePair.Value, left, right, true);
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x000148D0 File Offset: 0x00012AD0
			private ImmutableSortedDictionary<TKey, TValue>.Node SetOrAdd(TKey key, TValue value, IComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
			{
				replacedExistingValue = false;
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this, this, false);
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node right = this._right.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node left = this._left.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					if (valueComparer.Equals(this._value, value))
					{
						mutated = false;
						return this;
					}
					if (!overwriteExistingValue)
					{
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					}
					mutated = true;
					replacedExistingValue = true;
					node = new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this._left, this._right, false);
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
			}

			// Token: 0x060007DA RID: 2010 RVA: 0x000149C0 File Offset: 0x00012BC0
			private ImmutableSortedDictionary<TKey, TValue>.Node RemoveRecursive(TKey key, IComparer<TKey> keyComparer, out bool mutated)
			{
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedDictionary<TKey, TValue>.Node node = this;
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
					}
					else if (this._right.IsEmpty && !this._left.IsEmpty)
					{
						node = this._left;
					}
					else if (!this._right.IsEmpty && this._left.IsEmpty)
					{
						node = this._right;
					}
					else
					{
						ImmutableSortedDictionary<TKey, TValue>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedDictionary<TKey, TValue>.Node right = this._right.Remove(node2._key, keyComparer, out flag);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedDictionary<TKey, TValue>.Node left = this._left.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					ImmutableSortedDictionary<TKey, TValue>.Node right2 = this._right.Remove(key, keyComparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right2);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00014AF8 File Offset: 0x00012CF8
			private ImmutableSortedDictionary<TKey, TValue>.Node Mutate(ImmutableSortedDictionary<TKey, TValue>.Node left = null, ImmutableSortedDictionary<TKey, TValue>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedDictionary<TKey, TValue>.Node(this._key, this._value, left ?? this._left, right ?? this._right, false);
				}
				if (left != null)
				{
					this._left = left;
				}
				if (right != null)
				{
					this._right = right;
				}
				this._height = checked(1 + Math.Max(this._left._height, this._right._height));
				return this;
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x00014B70 File Offset: 0x00012D70
			private ImmutableSortedDictionary<TKey, TValue>.Node Search(TKey key, IComparer<TKey> keyComparer)
			{
				if (this.IsEmpty)
				{
					return this;
				}
				int num = keyComparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, keyComparer);
				}
				return this._left.Search(key, keyComparer);
			}

			// Token: 0x0400012E RID: 302
			[Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();

			// Token: 0x0400012F RID: 303
			private readonly TKey _key;

			// Token: 0x04000130 RID: 304
			private readonly TValue _value;

			// Token: 0x04000131 RID: 305
			private bool _frozen;

			// Token: 0x04000132 RID: 306
			private byte _height;

			// Token: 0x04000133 RID: 307
			private ImmutableSortedDictionary<TKey, TValue>.Node _left;

			// Token: 0x04000134 RID: 308
			private ImmutableSortedDictionary<TKey, TValue>.Node _right;
		}
	}
}
