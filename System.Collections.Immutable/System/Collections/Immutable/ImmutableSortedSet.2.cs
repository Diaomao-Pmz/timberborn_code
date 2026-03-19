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
	// Token: 0x02000045 RID: 69
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(ImmutableSortedSet), "Create")]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableSortedSet<[Nullable(2)] T> : IImmutableSet<!0>, IReadOnlyCollection<!0>, IEnumerable<!0>, IEnumerable, ISortKeyCollection<T>, IReadOnlyList<!0>, IList<!0>, ICollection<!0>, ISet<!0>, IList, ICollection, IStrongEnumerable<T, ImmutableSortedSet<T>.Enumerator>
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00008B44 File Offset: 0x00006D44
		internal ImmutableSortedSet([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer = null)
		{
			this._root = ImmutableSortedSet<T>.Node.EmptyNode;
			this._comparer = (comparer ?? Comparer<T>.Default);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008B67 File Offset: 0x00006D67
		private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
			Requires.NotNull<IComparer<T>>(comparer, "comparer");
			root.Freeze();
			this._root = root;
			this._comparer = comparer;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008B99 File Offset: 0x00006D99
		public ImmutableSortedSet<T> Clear()
		{
			if (!this._root.IsEmpty)
			{
				return ImmutableSortedSet<T>.Empty.WithComparer(this._comparer);
			}
			return this;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008BBA File Offset: 0x00006DBA
		[Nullable(2)]
		public T Max
		{
			[NullableContext(2)]
			get
			{
				return this._root.Max;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00008BC7 File Offset: 0x00006DC7
		[Nullable(2)]
		public T Min
		{
			[NullableContext(2)]
			get
			{
				return this._root.Min;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00008BE1 File Offset: 0x00006DE1
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008BEE File Offset: 0x00006DEE
		public IComparer<T> KeyComparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00008BF6 File Offset: 0x00006DF6
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000091 RID: 145
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00008C11 File Offset: 0x00006E11
		public ref readonly T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008C1F File Offset: 0x00006E1F
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		public ImmutableSortedSet<T>.Builder ToBuilder()
		{
			return new ImmutableSortedSet<T>.Builder(this);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008C28 File Offset: 0x00006E28
		public ImmutableSortedSet<T> Add(T value)
		{
			bool flag;
			return this.Wrap(this._root.Add(value, this._comparer, out flag));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008C50 File Offset: 0x00006E50
		public ImmutableSortedSet<T> Remove(T value)
		{
			bool flag;
			return this.Wrap(this._root.Remove(value, this._comparer, out flag));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008C78 File Offset: 0x00006E78
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
			if (node.IsEmpty)
			{
				actualValue = equalValue;
				return false;
			}
			actualValue = node.Key;
			return true;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public ImmutableSortedSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = this.Clear();
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(value))
				{
					immutableSortedSet = immutableSortedSet.Add(value);
				}
			}
			return immutableSortedSet;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008D2C File Offset: 0x00006F2C
		public ImmutableSortedSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T key in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Remove(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public ImmutableSortedSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet = ImmutableSortedSet.CreateRange<T>(this._comparer, other);
			ImmutableSortedSet<T> immutableSortedSet2 = this.Clear();
			foreach (T value in this)
			{
				if (!immutableSortedSet.Contains(value))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(value);
				}
			}
			foreach (T value2 in immutableSortedSet)
			{
				if (!this.Contains(value2))
				{
					immutableSortedSet2 = immutableSortedSet2.Add(value2);
				}
			}
			return immutableSortedSet2;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008E68 File Offset: 0x00007068
		public ImmutableSortedSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableSortedSet<T> immutableSortedSet;
			if (ImmutableSortedSet<T>.TryCastToImmutableSortedSet(other, out immutableSortedSet) && immutableSortedSet.KeyComparer == this.KeyComparer)
			{
				if (immutableSortedSet.IsEmpty)
				{
					return this;
				}
				if (this.IsEmpty)
				{
					return immutableSortedSet;
				}
				if (immutableSortedSet.Count > this.Count)
				{
					return immutableSortedSet.Union(this);
				}
			}
			int num;
			if (this.IsEmpty || (other.TryGetCount(out num) && (float)(this.Count + num) * 0.15f > (float)this.Count))
			{
				return this.LeafToRootRefill(other);
			}
			return this.UnionIncremental(other);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008EFB File Offset: 0x000070FB
		internal ImmutableSortedSet<T> Union([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other)
		{
			if (this.IsEmpty || (float)(this.Count + other.Length) * 0.15f > (float)this.Count)
			{
				return this.LeafToRootRefill(other);
			}
			return this.UnionIncremental(other);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00008F32 File Offset: 0x00007132
		public ImmutableSortedSet<T> WithComparer([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			if (comparer == this._comparer)
			{
				return this;
			}
			return new ImmutableSortedSet<T>(ImmutableSortedSet<T>.Node.EmptyNode, comparer).Union(this);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00008F5C File Offset: 0x0000715C
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this == other)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count != sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			foreach (T value in sortedSet)
			{
				if (!this.Contains(value))
				{
					return false;
				}
				num++;
			}
			return num == this.Count;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008FF4 File Offset: 0x000071F4
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return other.Any<T>();
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			if (this.Count >= sortedSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			foreach (T value in sortedSet)
			{
				if (this.Contains(value))
				{
					num++;
				}
				else
				{
					flag = true;
				}
				if (num == this.Count && flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000090A0 File Offset: 0x000072A0
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				num++;
				if (!this.Contains(value))
				{
					return false;
				}
			}
			return this.Count > num;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009124 File Offset: 0x00007324
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return true;
			}
			SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
			int num = 0;
			foreach (T value in sortedSet)
			{
				if (this.Contains(value))
				{
					num++;
				}
			}
			return num == this.Count;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000091A4 File Offset: 0x000073A4
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (!this.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009210 File Offset: 0x00007410
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (this.IsEmpty)
			{
				return false;
			}
			foreach (T value in other.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				if (this.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009284 File Offset: 0x00007484
		public IEnumerable<T> Reverse()
		{
			return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009291 File Offset: 0x00007491
		public int IndexOf(T item)
		{
			return this._root.IndexOf(item, this._comparer);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000092A5 File Offset: 0x000074A5
		public bool Contains(T value)
		{
			return this._root.Contains(value, this._comparer);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000092B9 File Offset: 0x000074B9
		IImmutableSet<T> IImmutableSet<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000092C1 File Offset: 0x000074C1
		IImmutableSet<T> IImmutableSet<!0>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000092CA File Offset: 0x000074CA
		IImmutableSet<T> IImmutableSet<!0>.Remove(T value)
		{
			return this.Remove(value);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000092D3 File Offset: 0x000074D3
		IImmutableSet<T> IImmutableSet<!0>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000092DC File Offset: 0x000074DC
		IImmutableSet<T> IImmutableSet<!0>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000092E5 File Offset: 0x000074E5
		IImmutableSet<T> IImmutableSet<!0>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000092EE File Offset: 0x000074EE
		IImmutableSet<T> IImmutableSet<!0>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000092F7 File Offset: 0x000074F7
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000092FE File Offset: 0x000074FE
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00009305 File Offset: 0x00007505
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000930C File Offset: 0x0000750C
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009313 File Offset: 0x00007513
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000931A File Offset: 0x0000751A
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000931D File Offset: 0x0000751D
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000932C File Offset: 0x0000752C
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009333 File Offset: 0x00007533
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000933A File Offset: 0x0000753A
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000093 RID: 147
		T IList<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00009351 File Offset: 0x00007551
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00009358 File Offset: 0x00007558
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000935F File Offset: 0x0000755F
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009362 File Offset: 0x00007562
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00009365 File Offset: 0x00007565
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00009368 File Offset: 0x00007568
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000936B File Offset: 0x0000756B
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009372 File Offset: 0x00007572
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000937C File Offset: 0x0000757C
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (default(T) == null && value == null);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000093A9 File Offset: 0x000075A9
		bool IList.Contains(object value)
		{
			return ImmutableSortedSet<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000093C1 File Offset: 0x000075C1
		int IList.IndexOf(object value)
		{
			if (ImmutableSortedSet<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000093D9 File Offset: 0x000075D9
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000093E0 File Offset: 0x000075E0
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000093E7 File Offset: 0x000075E7
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000098 RID: 152
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00009403 File Offset: 0x00007603
		void ICollection.CopyTo(Array array, int index)
		{
			this._root.CopyTo(array, index);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00009414 File Offset: 0x00007614
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009441 File Offset: 0x00007641
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000944E File Offset: 0x0000764E
		[NullableContext(0)]
		public ImmutableSortedSet<T>.Enumerator GetEnumerator()
		{
			return this._root.GetEnumerator();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000945C File Offset: 0x0000765C
		private static bool TryCastToImmutableSortedSet(IEnumerable<T> sequence, [NotNullWhen(true)] out ImmutableSortedSet<T> other)
		{
			other = (sequence as ImmutableSortedSet<T>);
			if (other != null)
			{
				return true;
			}
			ImmutableSortedSet<T>.Builder builder = sequence as ImmutableSortedSet<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000948C File Offset: 0x0000768C
		private static ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, comparer);
			}
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000094AC File Offset: 0x000076AC
		private ImmutableSortedSet<T> UnionIncremental(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableSortedSet<T>.Node node = this._root;
			foreach (T key in items.GetEnumerableDisposable<T, ImmutableSortedSet<T>.Enumerator>())
			{
				bool flag;
				node = node.Add(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00009524 File Offset: 0x00007724
		private unsafe ImmutableSortedSet<T> UnionIncremental(ReadOnlySpan<T> items)
		{
			ImmutableSortedSet<T>.Node node = this._root;
			ReadOnlySpan<T> readOnlySpan = items;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T key = *readOnlySpan[i];
				bool flag;
				node = node.Add(key, this._comparer, out flag);
			}
			return this.Wrap(node);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009571 File Offset: 0x00007771
		private ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableSortedSet<T>(root, this._comparer);
			}
			return this.Clear();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000959C File Offset: 0x0000779C
		private ImmutableSortedSet<T> LeafToRootRefill(IEnumerable<T> addedItems)
		{
			Requires.NotNull<IEnumerable<T>>(addedItems, "addedItems");
			List<T> list;
			if (this.IsEmpty)
			{
				int num;
				if (addedItems.TryGetCount(out num) && num == 0)
				{
					return this;
				}
				list = new List<T>(addedItems);
				if (list.Count == 0)
				{
					return this;
				}
			}
			else
			{
				list = new List<T>(this);
				list.AddRange(addedItems);
			}
			IComparer<T> keyComparer = this.KeyComparer;
			list.Sort(keyComparer);
			int num2 = 1;
			for (int i = 1; i < list.Count; i++)
			{
				if (keyComparer.Compare(list[i], list[i - 1]) != 0)
				{
					list[num2++] = list[i];
				}
			}
			list.RemoveRange(num2, list.Count - num2);
			ImmutableSortedSet<T>.Node root = ImmutableSortedSet<T>.Node.NodeTreeFromList(list.AsOrderedCollection<T>(), 0, list.Count);
			return this.Wrap(root);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00009668 File Offset: 0x00007868
		private unsafe ImmutableSortedSet<T> LeafToRootRefill(ReadOnlySpan<T> addedItems)
		{
			List<T> list;
			if (this.IsEmpty && addedItems.IsEmpty)
			{
				if (addedItems.IsEmpty)
				{
					return this;
				}
				list = new List<T>(addedItems.Length);
			}
			else
			{
				list = new List<T>(this.Count + addedItems.Length);
				list.AddRange(this);
			}
			ReadOnlySpan<T> readOnlySpan = addedItems;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T item = *readOnlySpan[i];
				list.Add(item);
			}
			IComparer<T> keyComparer = this.KeyComparer;
			list.Sort(keyComparer);
			int num = 1;
			for (int j = 1; j < list.Count; j++)
			{
				if (keyComparer.Compare(list[j], list[j - 1]) != 0)
				{
					list[num++] = list[j];
				}
			}
			list.RemoveRange(num, list.Count - num);
			ImmutableSortedSet<T>.Node root = ImmutableSortedSet<T>.Node.NodeTreeFromList(list.AsOrderedCollection<T>(), 0, list.Count);
			return this.Wrap(root);
		}

		// Token: 0x04000043 RID: 67
		private const float RefillOverIncrementalThreshold = 0.15f;

		// Token: 0x04000044 RID: 68
		public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>(null);

		// Token: 0x04000045 RID: 69
		private readonly ImmutableSortedSet<T>.Node _root;

		// Token: 0x04000046 RID: 70
		private readonly IComparer<T> _comparer;

		// Token: 0x020000B9 RID: 185
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableSortedSetBuilderDebuggerProxy<>))]
		public sealed class Builder : ISortKeyCollection<T>, IReadOnlyCollection<!0>, IEnumerable<!0>, IEnumerable, ISet<!0>, ICollection<!0>, ICollection
		{
			// Token: 0x060007DE RID: 2014 RVA: 0x00014BC8 File Offset: 0x00012DC8
			internal Builder(ImmutableSortedSet<T> set)
			{
				Requires.NotNull<ImmutableSortedSet<T>>(set, "set");
				this._root = set._root;
				this._comparer = set.KeyComparer;
				this._immutable = set;
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x060007DF RID: 2015 RVA: 0x00014C1B File Offset: 0x00012E1B
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00014C28 File Offset: 0x00012E28
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700018C RID: 396
			public unsafe T this[int index]
			{
				get
				{
					return *this._root.ItemRef(index);
				}
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x00014C3E File Offset: 0x00012E3E
			public ref readonly T ItemRef(int index)
			{
				return this._root.ItemRef(index);
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00014C4C File Offset: 0x00012E4C
			[Nullable(2)]
			public T Max
			{
				[NullableContext(2)]
				get
				{
					return this._root.Max;
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00014C59 File Offset: 0x00012E59
			[Nullable(2)]
			public T Min
			{
				[NullableContext(2)]
				get
				{
					return this._root.Min;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00014C66 File Offset: 0x00012E66
			// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00014C70 File Offset: 0x00012E70
			public IComparer<T> KeyComparer
			{
				get
				{
					return this._comparer;
				}
				set
				{
					Requires.NotNull<IComparer<T>>(value, "value");
					if (value != this._comparer)
					{
						ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
						foreach (T key in this)
						{
							bool flag;
							node = node.Add(key, value, out flag);
						}
						this._immutable = null;
						this._comparer = value;
						this.Root = node;
					}
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00014CF4 File Offset: 0x00012EF4
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00014CFC File Offset: 0x00012EFC
			// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00014D04 File Offset: 0x00012F04
			[Nullable(new byte[]
			{
				1,
				0
			})]
			private ImmutableSortedSet<T>.Node Root
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

			// Token: 0x060007EA RID: 2026 RVA: 0x00014D2C File Offset: 0x00012F2C
			public bool Add(T item)
			{
				bool result;
				this.Root = this.Root.Add(item, this._comparer, out result);
				return result;
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x00014D54 File Offset: 0x00012F54
			public void ExceptWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T key in other)
				{
					bool flag;
					this.Root = this.Root.Remove(key, this._comparer, out flag);
				}
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x00014DBC File Offset: 0x00012FBC
			public void IntersectWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
				foreach (T t in other)
				{
					if (this.Contains(t))
					{
						bool flag;
						node = node.Add(t, this._comparer, out flag);
					}
				}
				this.Root = node;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x00014E30 File Offset: 0x00013030
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSubsetOf(other);
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00014E3E File Offset: 0x0001303E
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsProperSupersetOf(other);
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x00014E4C File Offset: 0x0001304C
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSubsetOf(other);
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x00014E5A File Offset: 0x0001305A
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return this.ToImmutable().IsSupersetOf(other);
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00014E68 File Offset: 0x00013068
			public bool Overlaps(IEnumerable<T> other)
			{
				return this.ToImmutable().Overlaps(other);
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x00014E76 File Offset: 0x00013076
			public bool SetEquals(IEnumerable<T> other)
			{
				return this.ToImmutable().SetEquals(other);
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x00014E84 File Offset: 0x00013084
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				this.Root = this.ToImmutable().SymmetricExcept(other)._root;
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00014EA0 File Offset: 0x000130A0
			public void UnionWith(IEnumerable<T> other)
			{
				Requires.NotNull<IEnumerable<T>>(other, "other");
				foreach (T key in other)
				{
					bool flag;
					this.Root = this.Root.Add(key, this._comparer, out flag);
				}
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00014F08 File Offset: 0x00013108
			void ICollection<!0>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00014F12 File Offset: 0x00013112
			public void Clear()
			{
				this.Root = ImmutableSortedSet<T>.Node.EmptyNode;
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x00014F1F File Offset: 0x0001311F
			public bool Contains(T item)
			{
				return this.Root.Contains(item, this._comparer);
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00014F33 File Offset: 0x00013133
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x00014F44 File Offset: 0x00013144
			public bool Remove(T item)
			{
				bool result;
				this.Root = this.Root.Remove(item, this._comparer, out result);
				return result;
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x00014F6C File Offset: 0x0001316C
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x00014F7A File Offset: 0x0001317A
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.Root.GetEnumerator();
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x00014F8C File Offset: 0x0001318C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x00014F99 File Offset: 0x00013199
			public int IndexOf(T item)
			{
				return this.Root.IndexOf(item, this._comparer);
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x00014FAD File Offset: 0x000131AD
			public IEnumerable<T> Reverse()
			{
				return new ImmutableSortedSet<T>.ReverseEnumerable(this._root);
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00014FBC File Offset: 0x000131BC
			public ImmutableSortedSet<T> ToImmutable()
			{
				ImmutableSortedSet<T> result;
				if ((result = this._immutable) == null)
				{
					result = (this._immutable = ImmutableSortedSet<T>.Wrap(this.Root, this._comparer));
				}
				return result;
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00014FF0 File Offset: 0x000131F0
			public bool TryGetValue(T equalValue, out T actualValue)
			{
				ImmutableSortedSet<T>.Node node = this._root.Search(equalValue, this._comparer);
				if (!node.IsEmpty)
				{
					actualValue = node.Key;
					return true;
				}
				actualValue = equalValue;
				return false;
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x0001502E File Offset: 0x0001322E
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001503D File Offset: 0x0001323D
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000803 RID: 2051 RVA: 0x00015040 File Offset: 0x00013240
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

			// Token: 0x04000135 RID: 309
			private ImmutableSortedSet<T>.Node _root = ImmutableSortedSet<T>.Node.EmptyNode;

			// Token: 0x04000136 RID: 310
			private IComparer<T> _comparer = Comparer<T>.Default;

			// Token: 0x04000137 RID: 311
			private ImmutableSortedSet<T> _immutable;

			// Token: 0x04000138 RID: 312
			private int _version;

			// Token: 0x04000139 RID: 313
			private object _syncRoot;
		}

		// Token: 0x020000BA RID: 186
		private sealed class ReverseEnumerable : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06000804 RID: 2052 RVA: 0x00015062 File Offset: 0x00013262
			internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x0001507C File Offset: 0x0001327C
			public IEnumerator<T> GetEnumerator()
			{
				return this._root.Reverse();
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x00015089 File Offset: 0x00013289
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400013A RID: 314
			private readonly ImmutableSortedSet<T>.Node _root;
		}

		// Token: 0x020000BB RID: 187
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x06000807 RID: 2055 RVA: 0x00015094 File Offset: 0x00013294
			internal Enumerator([Nullable(new byte[]
			{
				1,
				0
			})] ImmutableSortedSet<T>.Node root, [Nullable(new byte[]
			{
				2,
				0
			})] ImmutableSortedSet<T>.Builder builder = null, bool reverse = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(root, "root");
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._reverse = reverse;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>.TryTake(this, out this._stack))
				{
					this._stack = SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>.PrepNew(this, new Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>(root.Height));
				}
				this.PushNext(this._root);
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000808 RID: 2056 RVA: 0x00015127 File Offset: 0x00013327
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001512F File Offset: 0x0001332F
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
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

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600080A RID: 2058 RVA: 0x00015150 File Offset: 0x00013350
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x00015160 File Offset: 0x00013360
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableSortedSet<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
					SecureObjectPool<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>, ImmutableSortedSet<T>.Enumerator>.TryAdd(this, this._stack);
					this._stack = null;
				}
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x000151B4 File Offset: 0x000133B4
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				if (stack.Count > 0)
				{
					ImmutableSortedSet<T>.Node value = stack.Pop().Value;
					this._current = value;
					this.PushNext(this._reverse ? value.Left : value.Right);
					return true;
				}
				this._current = null;
				return false;
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x0001521C File Offset: 0x0001341C
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._current = null;
				this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this).ClearFastWhenEmpty<RefAsValueType<ImmutableSortedSet<T>.Node>>();
				this.PushNext(this._root);
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x0001526F File Offset: 0x0001346F
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableSortedSet<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableSortedSet<T>.Enumerator>(this);
				}
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x0001529A File Offset: 0x0001349A
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x000152C4 File Offset: 0x000134C4
			private void PushNext(ImmutableSortedSet<T>.Node node)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(node, "node");
				Stack<RefAsValueType<ImmutableSortedSet<T>.Node>> stack = this._stack.Use<ImmutableSortedSet<T>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableSortedSet<T>.Node>(node));
					node = (this._reverse ? node.Right : node.Left);
				}
			}

			// Token: 0x0400013B RID: 315
			private readonly ImmutableSortedSet<T>.Builder _builder;

			// Token: 0x0400013C RID: 316
			private readonly int _poolUserId;

			// Token: 0x0400013D RID: 317
			private readonly bool _reverse;

			// Token: 0x0400013E RID: 318
			private ImmutableSortedSet<T>.Node _root;

			// Token: 0x0400013F RID: 319
			private SecurePooledObject<Stack<RefAsValueType<ImmutableSortedSet<T>.Node>>> _stack;

			// Token: 0x04000140 RID: 320
			private ImmutableSortedSet<T>.Node _current;

			// Token: 0x04000141 RID: 321
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020000BC RID: 188
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<!0>, IBinaryTree, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x06000811 RID: 2065 RVA: 0x00015317 File Offset: 0x00013517
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x00015328 File Offset: 0x00013528
			private Node(T key, ImmutableSortedSet<T>.Node left, ImmutableSortedSet<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(left, "left");
				Requires.NotNull<ImmutableSortedSet<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = checked(1 + Math.Max(left._height, right._height));
				this._count = 1 + left._count + right._count;
				this._frozen = frozen;
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001539D File Offset: 0x0001359D
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x000153A8 File Offset: 0x000135A8
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x000153B0 File Offset: 0x000135B0
			[Nullable(new byte[]
			{
				2,
				0
			})]
			public ImmutableSortedSet<T>.Node Left
			{
				[return: Nullable(new byte[]
				{
					2,
					0
				})]
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000816 RID: 2070 RVA: 0x000153B8 File Offset: 0x000135B8
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000817 RID: 2071 RVA: 0x000153C0 File Offset: 0x000135C0
			[Nullable(new byte[]
			{
				2,
				0
			})]
			public ImmutableSortedSet<T>.Node Right
			{
				[return: Nullable(new byte[]
				{
					2,
					0
				})]
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x000153C8 File Offset: 0x000135C8
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000819 RID: 2073 RVA: 0x000153D0 File Offset: 0x000135D0
			[Nullable(new byte[]
			{
				2,
				1
			})]
			IBinaryTree<T> IBinaryTree<!0>.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x0600081A RID: 2074 RVA: 0x000153D8 File Offset: 0x000135D8
			[Nullable(new byte[]
			{
				2,
				1
			})]
			IBinaryTree<T> IBinaryTree<!0>.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x0600081B RID: 2075 RVA: 0x000153E0 File Offset: 0x000135E0
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x0600081C RID: 2076 RVA: 0x000153E8 File Offset: 0x000135E8
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x0600081D RID: 2077 RVA: 0x000153F0 File Offset: 0x000135F0
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600081E RID: 2078 RVA: 0x000153F8 File Offset: 0x000135F8
			[Nullable(2)]
			internal T Max
			{
				[NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._right.IsEmpty)
					{
						node = node._right;
					}
					return node._key;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600081F RID: 2079 RVA: 0x00015438 File Offset: 0x00013638
			[Nullable(2)]
			internal T Min
			{
				[NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						return default(T);
					}
					ImmutableSortedSet<T>.Node node = this;
					while (!node._left.IsEmpty)
					{
						node = node._left;
					}
					return node._key;
				}
			}

			// Token: 0x170001A4 RID: 420
			internal T this[int index]
			{
				get
				{
					Requires.Range(index >= 0 && index < this.Count, "index", null);
					if (index < this._left._count)
					{
						return this._left[index];
					}
					if (index > this._left._count)
					{
						return this._right[index - this._left._count - 1];
					}
					return this._key;
				}
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x000154EA File Offset: 0x000136EA
			internal ref readonly T ItemRef(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				return this.ItemRefUnchecked(index);
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x00015510 File Offset: 0x00013710
			private ref readonly T ItemRefUnchecked(int index)
			{
				if (index < this._left._count)
				{
					return this._left.ItemRefUnchecked(index);
				}
				if (index > this._left._count)
				{
					return this._right.ItemRefUnchecked(index - this._left._count - 1);
				}
				return ref this._key;
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x00015567 File Offset: 0x00013767
			[NullableContext(0)]
			public ImmutableSortedSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, false);
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x00015571 File Offset: 0x00013771
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x0001557E File Offset: 0x0001377E
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x0001558B File Offset: 0x0001378B
			[NullableContext(0)]
			internal ImmutableSortedSet<T>.Enumerator GetEnumerator([Nullable(new byte[]
			{
				1,
				0
			})] ImmutableSortedSet<T>.Builder builder)
			{
				return new ImmutableSortedSet<T>.Enumerator(this, builder, false);
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x00015598 File Offset: 0x00013798
			internal void CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x00015624 File Offset: 0x00013824
			internal void CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array.SetValue(t, arrayIndex++);
				}
			}

			// Token: 0x06000829 RID: 2089 RVA: 0x000156B8 File Offset: 0x000138B8
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableSortedSet<T>.Node Add(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = true;
					return new ImmutableSortedSet<T>.Node(key, this, this, false);
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num > 0)
				{
					ImmutableSortedSet<T>.Node right = this._right.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right);
					}
				}
				else
				{
					if (num >= 0)
					{
						mutated = false;
						return this;
					}
					ImmutableSortedSet<T>.Node left = this._left.Add(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				if (!mutated)
				{
					return node;
				}
				return ImmutableSortedSet<T>.Node.MakeBalanced(node);
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x0001574C File Offset: 0x0001394C
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableSortedSet<T>.Node Remove(T key, IComparer<T> comparer, out bool mutated)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					mutated = false;
					return this;
				}
				ImmutableSortedSet<T>.Node node = this;
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					mutated = true;
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableSortedSet<T>.Node.EmptyNode;
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
						ImmutableSortedSet<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						bool flag;
						ImmutableSortedSet<T>.Node right = this._right.Remove(node2._key, comparer, out flag);
						node = node2.Mutate(this._left, right);
					}
				}
				else if (num < 0)
				{
					ImmutableSortedSet<T>.Node left = this._left.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(left, null);
					}
				}
				else
				{
					ImmutableSortedSet<T>.Node right2 = this._right.Remove(key, comparer, out mutated);
					if (mutated)
					{
						node = this.Mutate(null, right2);
					}
				}
				if (!node.IsEmpty)
				{
					return ImmutableSortedSet<T>.Node.MakeBalanced(node);
				}
				return node;
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x0001588F File Offset: 0x00013A8F
			internal bool Contains(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				return !this.Search(key, comparer).IsEmpty;
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x000158AC File Offset: 0x00013AAC
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x000158D4 File Offset: 0x00013AD4
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableSortedSet<T>.Node Search(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return this;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this;
				}
				if (num > 0)
				{
					return this._right.Search(key, comparer);
				}
				return this._left.Search(key, comparer);
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x00015928 File Offset: 0x00013B28
			internal int IndexOf(T key, IComparer<T> comparer)
			{
				Requires.NotNull<IComparer<T>>(comparer, "comparer");
				if (this.IsEmpty)
				{
					return -1;
				}
				int num = comparer.Compare(key, this._key);
				if (num == 0)
				{
					return this._left.Count;
				}
				if (num > 0)
				{
					int num2 = this._right.IndexOf(key, comparer);
					bool flag = num2 < 0;
					if (flag)
					{
						num2 = ~num2;
					}
					num2 = this._left.Count + 1 + num2;
					if (flag)
					{
						num2 = ~num2;
					}
					return num2;
				}
				return this._left.IndexOf(key, comparer);
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x000159A7 File Offset: 0x00013BA7
			internal IEnumerator<T> Reverse()
			{
				return new ImmutableSortedSet<T>.Enumerator(this, null, true);
			}

			// Token: 0x06000830 RID: 2096 RVA: 0x000159B8 File Offset: 0x00013BB8
			private static ImmutableSortedSet<T>.Node RotateLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node right = tree._right;
				return right.Mutate(tree.Mutate(null, right._left), null);
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x000159FC File Offset: 0x00013BFC
			private static ImmutableSortedSet<T>.Node RotateRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				ImmutableSortedSet<T>.Node left = tree._left;
				return left.Mutate(null, tree.Mutate(left._right, null));
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x00015A3E File Offset: 0x00013C3E
			private static ImmutableSortedSet<T>.Node DoubleLeft(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._right.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedSet<T>.Node.RotateLeft(tree.Mutate(null, ImmutableSortedSet<T>.Node.RotateRight(tree._right)));
			}

			// Token: 0x06000833 RID: 2099 RVA: 0x00015A71 File Offset: 0x00013C71
			private static ImmutableSortedSet<T>.Node DoubleRight(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (tree._left.IsEmpty)
				{
					return tree;
				}
				return ImmutableSortedSet<T>.Node.RotateRight(tree.Mutate(ImmutableSortedSet<T>.Node.RotateLeft(tree._left), null));
			}

			// Token: 0x06000834 RID: 2100 RVA: 0x00015AA4 File Offset: 0x00013CA4
			private static int Balance(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return (int)(tree._right._height - tree._left._height);
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x00015AC8 File Offset: 0x00013CC8
			private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x00015AE1 File Offset: 0x00013CE1
			private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x00015AFC File Offset: 0x00013CFC
			private static ImmutableSortedSet<T>.Node MakeBalanced(ImmutableSortedSet<T>.Node tree)
			{
				Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, "tree");
				if (ImmutableSortedSet<T>.Node.IsRightHeavy(tree))
				{
					if (ImmutableSortedSet<T>.Node.Balance(tree._right) >= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateLeft(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleLeft(tree);
				}
				else
				{
					if (!ImmutableSortedSet<T>.Node.IsLeftHeavy(tree))
					{
						return tree;
					}
					if (ImmutableSortedSet<T>.Node.Balance(tree._left) <= 0)
					{
						return ImmutableSortedSet<T>.Node.RotateRight(tree);
					}
					return ImmutableSortedSet<T>.Node.DoubleRight(tree);
				}
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x00015B60 File Offset: 0x00013D60
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal static ImmutableSortedSet<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				if (length == 0)
				{
					return ImmutableSortedSet<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableSortedSet<T>.Node left = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableSortedSet<T>.Node right = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableSortedSet<T>.Node(items[start + num2], left, right, true);
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x00015BB4 File Offset: 0x00013DB4
			private ImmutableSortedSet<T>.Node Mutate(ImmutableSortedSet<T>.Node left = null, ImmutableSortedSet<T>.Node right = null)
			{
				if (this._frozen)
				{
					return new ImmutableSortedSet<T>.Node(this._key, left ?? this._left, right ?? this._right, false);
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
				this._count = 1 + this._left._count + this._right._count;
				return this;
			}

			// Token: 0x04000142 RID: 322
			[Nullable(new byte[]
			{
				1,
				0
			})]
			internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();

			// Token: 0x04000143 RID: 323
			private readonly T _key;

			// Token: 0x04000144 RID: 324
			private bool _frozen;

			// Token: 0x04000145 RID: 325
			private byte _height;

			// Token: 0x04000146 RID: 326
			private int _count;

			// Token: 0x04000147 RID: 327
			private ImmutableSortedSet<T>.Node _left;

			// Token: 0x04000148 RID: 328
			private ImmutableSortedSet<T>.Node _right;
		}
	}
}
