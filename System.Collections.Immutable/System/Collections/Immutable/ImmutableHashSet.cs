using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(ImmutableHashSet), "Create")]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableHashSet<[Nullable(2)] T> : IImmutableSet<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IHashKeyCollection<T>, ICollection<T>, ISet<T>, ICollection, IStrongEnumerable<T, ImmutableHashSet<T>.Enumerator>
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x0000314E File Offset: 0x0000134E
		internal ImmutableHashSet(IEqualityComparer<T> equalityComparer) : this(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, equalityComparer, 0)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003160 File Offset: 0x00001360
		private ImmutableHashSet(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			root.Freeze(ImmutableHashSet<T>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
			this._equalityComparer = equalityComparer;
			this._hashBucketEqualityComparer = ImmutableHashSet<T>.GetHashBucketEqualityComparer(equalityComparer);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000031B5 File Offset: 0x000013B5
		public ImmutableHashSet<T> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableHashSet<T>.Empty.WithComparer(this._equalityComparer);
			}
			return this;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000031D1 File Offset: 0x000013D1
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000031D9 File Offset: 0x000013D9
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000031E4 File Offset: 0x000013E4
		public IEqualityComparer<T> KeyComparer
		{
			get
			{
				return this._equalityComparer;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000031EC File Offset: 0x000013EC
		IImmutableSet<T> IImmutableSet<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000031F4 File Offset: 0x000013F4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000031F7 File Offset: 0x000013F7
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000031FA File Offset: 0x000013FA
		internal IBinaryTree Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003202 File Offset: 0x00001402
		[Nullable(0)]
		private ImmutableHashSet<T>.MutationInput Origin
		{
			get
			{
				return new ImmutableHashSet<T>.MutationInput(this);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000320A File Offset: 0x0000140A
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		public ImmutableHashSet<T>.Builder ToBuilder()
		{
			return new ImmutableHashSet<T>.Builder(this);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003214 File Offset: 0x00001414
		public ImmutableHashSet<T> Add(T item)
		{
			return ImmutableHashSet<T>.Add(item, this.Origin).Finalize(this);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003238 File Offset: 0x00001438
		public ImmutableHashSet<T> Remove(T item)
		{
			return ImmutableHashSet<T>.Remove(item, this.Origin).Finalize(this);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000325C File Offset: 0x0000145C
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			int key = (equalValue != null) ? this._equalityComparer.GetHashCode(equalValue) : 0;
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (this._root.TryGetValue(key, out hashBucket))
			{
				return hashBucket.TryExchange(equalValue, this._equalityComparer, out actualValue);
			}
			actualValue = equalValue;
			return false;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000032A9 File Offset: 0x000014A9
		public ImmutableHashSet<T> Union(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this.Union(other, false);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000032C0 File Offset: 0x000014C0
		internal ImmutableHashSet<T> Union([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other)
		{
			return ImmutableHashSet<T>.Union(other, this.Origin).Finalize(this);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000032E4 File Offset: 0x000014E4
		public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Intersect(other, this.Origin).Finalize(this);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003314 File Offset: 0x00001514
		public ImmutableHashSet<T> Except(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root).Finalize(this);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003350 File Offset: 0x00001550
		public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.SymmetricExcept(other, this.Origin).Finalize(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000337D File Offset: 0x0000157D
		public bool SetEquals(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000339C File Offset: 0x0000159C
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000033B5 File Offset: 0x000015B5
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000033CE File Offset: 0x000015CE
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000033E7 File Offset: 0x000015E7
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003400 File Offset: 0x00001600
		public bool Overlaps(IEnumerable<T> other)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			return ImmutableHashSet<T>.Overlaps(other, this.Origin);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003419 File Offset: 0x00001619
		IImmutableSet<T> IImmutableSet<!0>.Add(T item)
		{
			return this.Add(item);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003422 File Offset: 0x00001622
		IImmutableSet<T> IImmutableSet<!0>.Remove(T item)
		{
			return this.Remove(item);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000342B File Offset: 0x0000162B
		IImmutableSet<T> IImmutableSet<!0>.Union(IEnumerable<T> other)
		{
			return this.Union(other);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003434 File Offset: 0x00001634
		IImmutableSet<T> IImmutableSet<!0>.Intersect(IEnumerable<T> other)
		{
			return this.Intersect(other);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000343D File Offset: 0x0000163D
		IImmutableSet<T> IImmutableSet<!0>.Except(IEnumerable<T> other)
		{
			return this.Except(other);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003446 File Offset: 0x00001646
		IImmutableSet<T> IImmutableSet<!0>.SymmetricExcept(IEnumerable<T> other)
		{
			return this.SymmetricExcept(other);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000344F File Offset: 0x0000164F
		public bool Contains(T item)
		{
			return ImmutableHashSet<T>.Contains(item, this.Origin);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000345D File Offset: 0x0000165D
		public ImmutableHashSet<T> WithComparer([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				equalityComparer = EqualityComparer<T>.Default;
			}
			if (equalityComparer == this._equalityComparer)
			{
				return this;
			}
			return new ImmutableHashSet<T>(equalityComparer).Union(this, true);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003481 File Offset: 0x00001681
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003488 File Offset: 0x00001688
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000348F File Offset: 0x0000168F
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003496 File Offset: 0x00001696
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000349D File Offset: 0x0000169D
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000034A4 File Offset: 0x000016A4
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000034A8 File Offset: 0x000016A8
		void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
		{
			Requires.NotNull<T[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003534 File Offset: 0x00001734
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000353B File Offset: 0x0000173B
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003542 File Offset: 0x00001742
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000354C File Offset: 0x0000174C
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

		// Token: 0x0600010E RID: 270 RVA: 0x000035E0 File Offset: 0x000017E0
		[NullableContext(0)]
		public ImmutableHashSet<T>.Enumerator GetEnumerator()
		{
			return new ImmutableHashSet<T>.Enumerator(this._root, null);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000035F0 File Offset: 0x000017F0
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000361D File Offset: 0x0000181D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000362C File Offset: 0x0000182C
		private static bool IsSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			using (DisposableEnumeratorAdapter<T, ImmutableHashSet<T>.Enumerator> enumerator = other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003694 File Offset: 0x00001894
		private static ImmutableHashSet<T>.MutationResult Add(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int num = (item != null) ? origin.EqualityComparer.GetHashCode(item) : 0;
			ImmutableHashSet<T>.OperationResult operationResult;
			ImmutableHashSet<T>.HashBucket newBucket = origin.Root.GetValueOrDefault(num).Add(item, origin.EqualityComparer, out operationResult);
			if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
			{
				return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
			}
			return new ImmutableHashSet<T>.MutationResult(ImmutableHashSet<T>.UpdateRoot(origin.Root, num, origin.HashBucketEqualityComparer, newBucket), 1, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000370C File Offset: 0x0000190C
		private static ImmutableHashSet<T>.MutationResult Remove(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			ImmutableHashSet<T>.OperationResult operationResult = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
			int num = (item != null) ? origin.EqualityComparer.GetHashCode(item) : 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = origin.Root;
			ImmutableHashSet<T>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(num, out hashBucket))
			{
				ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(item, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
				{
					return new ImmutableHashSet<T>.MutationResult(origin.Root, 0, ImmutableHashSet<T>.CountType.Adjustment);
				}
				root = ImmutableHashSet<T>.UpdateRoot(origin.Root, num, origin.HashBucketEqualityComparer, newBucket);
			}
			return new ImmutableHashSet<T>.MutationResult(root, (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged) ? -1 : 0, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003798 File Offset: 0x00001998
		private static bool Contains(T item, ImmutableHashSet<T>.MutationInput origin)
		{
			int key = (item != null) ? origin.EqualityComparer.GetHashCode(item) : 0;
			ImmutableHashSet<T>.HashBucket hashBucket;
			return origin.Root.TryGetValue(key, out hashBucket) && hashBucket.Contains(item, origin.EqualityComparer);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000037E0 File Offset: 0x000019E0
		private static ImmutableHashSet<T>.MutationResult Union(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int num2 = (t != null) ? origin.EqualityComparer.GetHashCode(t) : 0;
				ImmutableHashSet<T>.OperationResult operationResult;
				ImmutableHashSet<T>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(num2).Add(t, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
				{
					sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, num2, origin.HashBucketEqualityComparer, newBucket);
					num++;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000038A4 File Offset: 0x00001AA4
		private unsafe static ImmutableHashSet<T>.MutationResult Union(ReadOnlySpan<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = origin.Root;
			ReadOnlySpan<T> readOnlySpan = other;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				int num2 = (t != null) ? origin.EqualityComparer.GetHashCode(t) : 0;
				ImmutableHashSet<T>.OperationResult operationResult;
				ImmutableHashSet<T>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(num2).Add(t, origin.EqualityComparer, out operationResult);
				if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
				{
					sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, num2, origin.HashBucketEqualityComparer, newBucket);
					num++;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000393C File Offset: 0x00001B3C
		private static bool Overlaps(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			using (DisposableEnumeratorAdapter<T, ImmutableHashSet<T>.Enumerator> enumerator = other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000039B4 File Offset: 0x00001BB4
		private static bool SetEquals(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count != hashSet.Count)
			{
				return false;
			}
			using (HashSet<T>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003A34 File Offset: 0x00001C34
		private static SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int hashCode, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, ImmutableHashSet<T>.HashBucket newBucket)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketEqualityComparer, out flag, out flag2);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003A64 File Offset: 0x00001C64
		private static ImmutableHashSet<T>.MutationResult Intersect(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			int num = 0;
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				if (ImmutableHashSet<T>.Contains(item, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					root = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(root, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003B0C File Offset: 0x00001D0C
		private static ImmutableHashSet<T>.MutationResult Except(IEnumerable<T> other, IEqualityComparer<T> equalityComparer, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> sortedInt32KeyNode = root;
			foreach (T t in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				int num2 = (t != null) ? equalityComparer.GetHashCode(t) : 0;
				ImmutableHashSet<T>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(num2, out hashBucket))
				{
					ImmutableHashSet<T>.OperationResult operationResult;
					ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(t, equalityComparer, out operationResult);
					if (operationResult == ImmutableHashSet<T>.OperationResult.SizeChanged)
					{
						num--;
						sortedInt32KeyNode = ImmutableHashSet<T>.UpdateRoot(sortedInt32KeyNode, num2, hashBucketEqualityComparer, newBucket);
					}
				}
			}
			return new ImmutableHashSet<T>.MutationResult(sortedInt32KeyNode, num, ImmutableHashSet<T>.CountType.Adjustment);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private static ImmutableHashSet<T>.MutationResult SymmetricExcept(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			ImmutableHashSet<T> immutableHashSet = ImmutableHashSet.CreateRange<T>(origin.EqualityComparer, other);
			int num = 0;
			SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			foreach (T item in new ImmutableHashSet<T>.NodeEnumerable(origin.Root))
			{
				if (!immutableHashSet.Contains(item))
				{
					ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(item, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					root = mutationResult.Root;
					num += mutationResult.Count;
				}
			}
			foreach (T item2 in immutableHashSet)
			{
				if (!ImmutableHashSet<T>.Contains(item2, origin))
				{
					ImmutableHashSet<T>.MutationResult mutationResult2 = ImmutableHashSet<T>.Add(item2, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, origin.HashBucketEqualityComparer, num));
					root = mutationResult2.Root;
					num += mutationResult2.Count;
				}
			}
			return new ImmutableHashSet<T>.MutationResult(root, num, ImmutableHashSet<T>.CountType.FinalValue);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003CFC File Offset: 0x00001EFC
		private static bool IsProperSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return other.Any<T>();
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			if (origin.Count >= hashSet.Count)
			{
				return false;
			}
			int num = 0;
			bool flag = false;
			using (HashSet<T>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						num++;
					}
					else
					{
						flag = true;
					}
					if (num == origin.Count && flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003DAC File Offset: 0x00001FAC
		private static bool IsProperSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return false;
			}
			int num = 0;
			foreach (T item in other.GetEnumerableDisposable<T, ImmutableHashSet<T>.Enumerator>())
			{
				num++;
				if (!ImmutableHashSet<T>.Contains(item, origin))
				{
					return false;
				}
			}
			return origin.Count > num;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003E34 File Offset: 0x00002034
		private static bool IsSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
		{
			Requires.NotNull<IEnumerable<T>>(other, "other");
			if (origin.Root.IsEmpty)
			{
				return true;
			}
			HashSet<T> hashSet = new HashSet<T>(other, origin.EqualityComparer);
			int num = 0;
			using (HashSet<T>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ImmutableHashSet<T>.Contains(enumerator.Current, origin))
					{
						num++;
					}
				}
			}
			return num == origin.Count;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003EBC File Offset: 0x000020BC
		private static ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableHashSet<T>(root, equalityComparer, count);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003EEE File Offset: 0x000020EE
		private static IEqualityComparer<ImmutableHashSet<T>.HashBucket> GetHashBucketEqualityComparer(IEqualityComparer<T> valueComparer)
		{
			if (!ImmutableExtensions.IsValueType<T>())
			{
				return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.DefaultInstance;
			}
			if (valueComparer == EqualityComparer<T>.Default)
			{
				return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.DefaultInstance;
			}
			return new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(valueComparer);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003F11 File Offset: 0x00002111
		private ImmutableHashSet<T> Wrap(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == this._root)
			{
				return this;
			}
			return new ImmutableHashSet<T>(root, this._equalityComparer, adjustedCountIfDifferentRoot);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003F2C File Offset: 0x0000212C
		private ImmutableHashSet<T> Union(IEnumerable<T> items, bool avoidWithComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty && !avoidWithComparer)
			{
				ImmutableHashSet<T> immutableHashSet = items as ImmutableHashSet<T>;
				if (immutableHashSet != null)
				{
					return immutableHashSet.WithComparer(this.KeyComparer);
				}
			}
			return ImmutableHashSet<T>.Union(items, this.Origin).Finalize(this);
		}

		// Token: 0x04000023 RID: 35
		public static readonly ImmutableHashSet<T> Empty = new ImmutableHashSet<T>(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, EqualityComparer<T>.Default, 0);

		// Token: 0x04000024 RID: 36
		private static readonly Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableHashSet<T>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x04000025 RID: 37
		private readonly IEqualityComparer<T> _equalityComparer;

		// Token: 0x04000026 RID: 38
		private readonly int _count;

		// Token: 0x04000027 RID: 39
		private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

		// Token: 0x04000028 RID: 40
		private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

		// Token: 0x02000091 RID: 145
		private sealed class HashBucketByValueEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x17000103 RID: 259
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000E6F3 File Offset: 0x0000C8F3
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByValueEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x0000E6FA File Offset: 0x0000C8FA
			internal HashBucketByValueEqualityComparer(IEqualityComparer<T> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(valueComparer, "valueComparer");
				this._valueComparer = valueComparer;
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0000E714 File Offset: 0x0000C914
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByValue(y, this._valueComparer);
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0000E724 File Offset: 0x0000C924
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040000AA RID: 170
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByValueEqualityComparer(EqualityComparer<T>.Default);

			// Token: 0x040000AB RID: 171
			private readonly IEqualityComparer<T> _valueComparer;
		}

		// Token: 0x02000092 RID: 146
		private sealed class HashBucketByRefEqualityComparer : IEqualityComparer<ImmutableHashSet<T>.HashBucket>
		{
			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000E73C File Offset: 0x0000C93C
			internal static IEqualityComparer<ImmutableHashSet<T>.HashBucket> DefaultInstance
			{
				get
				{
					return ImmutableHashSet<T>.HashBucketByRefEqualityComparer.s_defaultInstance;
				}
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x0000E743 File Offset: 0x0000C943
			private HashBucketByRefEqualityComparer()
			{
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x0000E74B File Offset: 0x0000C94B
			public bool Equals(ImmutableHashSet<T>.HashBucket x, ImmutableHashSet<T>.HashBucket y)
			{
				return x.EqualsByRef(y);
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x0000E755 File Offset: 0x0000C955
			public int GetHashCode(ImmutableHashSet<T>.HashBucket obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040000AC RID: 172
			private static readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> s_defaultInstance = new ImmutableHashSet<T>.HashBucketByRefEqualityComparer();
		}

		// Token: 0x02000093 RID: 147
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		public sealed class Builder : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, ISet<!0>, ICollection<!0>
		{
			// Token: 0x060005A5 RID: 1445 RVA: 0x0000E768 File Offset: 0x0000C968
			internal Builder(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._count = set._count;
				this._equalityComparer = set._equalityComparer;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
				this._immutable = set;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
			// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000E7DC File Offset: 0x0000C9DC
			public IEqualityComparer<T> KeyComparer
			{
				get
				{
					return this._equalityComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<T>>(value, "value");
					if (value != this._equalityComparer)
					{
						ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union(this, new ImmutableHashSet<T>.MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode, value, this._hashBucketEqualityComparer, 0));
						this._immutable = null;
						this._equalityComparer = value;
						this.Root = mutationResult.Root;
						this._count = mutationResult.Count;
					}
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000E83E File Offset: 0x0000CA3E
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000E846 File Offset: 0x0000CA46
			[Nullable(0)]
			private ImmutableHashSet<T>.MutationInput Origin
			{
				get
				{
					return new ImmutableHashSet<T>.MutationInput(this.Root, this._equalityComparer, this._hashBucketEqualityComparer, this._count);
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000E865 File Offset: 0x0000CA65
			// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000E86D File Offset: 0x0000CA6D
			[Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
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

			// Token: 0x060005AE RID: 1454 RVA: 0x0000E894 File Offset: 0x0000CA94
			[NullableContext(0)]
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, this);
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
			public ImmutableHashSet<T> ToImmutable()
			{
				ImmutableHashSet<T> result;
				if ((result = this._immutable) == null)
				{
					result = (this._immutable = ImmutableHashSet<T>.Wrap(this._root, this._equalityComparer, this._count));
				}
				return result;
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0000E8DC File Offset: 0x0000CADC
			public bool TryGetValue(T equalValue, out T actualValue)
			{
				int key = (equalValue != null) ? this._equalityComparer.GetHashCode(equalValue) : 0;
				ImmutableHashSet<T>.HashBucket hashBucket;
				if (this._root.TryGetValue(key, out hashBucket))
				{
					return hashBucket.TryExchange(equalValue, this._equalityComparer, out actualValue);
				}
				actualValue = equalValue;
				return false;
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x0000E92C File Offset: 0x0000CB2C
			public bool Add(T item)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Add(item, this.Origin);
				this.Apply(result);
				return result.Count != 0;
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x0000E958 File Offset: 0x0000CB58
			public bool Remove(T item)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Remove(item, this.Origin);
				this.Apply(result);
				return result.Count != 0;
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x0000E983 File Offset: 0x0000CB83
			public bool Contains(T item)
			{
				return ImmutableHashSet<T>.Contains(item, this.Origin);
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x0000E991 File Offset: 0x0000CB91
			public void Clear()
			{
				this._count = 0;
				this.Root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
			public void ExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Except(other, this._equalityComparer, this._hashBucketEqualityComparer, this._root);
				this.Apply(result);
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
			public void IntersectWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Intersect(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x0000E9F9 File Offset: 0x0000CBF9
			public bool IsProperSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0000EA07 File Offset: 0x0000CC07
			public bool IsProperSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x0000EA15 File Offset: 0x0000CC15
			public bool IsSubsetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0000EA23 File Offset: 0x0000CC23
			public bool IsSupersetOf(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0000EA31 File Offset: 0x0000CC31
			public bool Overlaps(IEnumerable<T> other)
			{
				return ImmutableHashSet<T>.Overlaps(other, this.Origin);
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0000EA3F File Offset: 0x0000CC3F
			public bool SetEquals(IEnumerable<T> other)
			{
				return this == other || ImmutableHashSet<T>.SetEquals(other, this.Origin);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0000EA54 File Offset: 0x0000CC54
			public void SymmetricExceptWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.SymmetricExcept(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0000EA78 File Offset: 0x0000CC78
			public void UnionWith(IEnumerable<T> other)
			{
				ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Union(other, this.Origin);
				this.Apply(result);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0000EA99 File Offset: 0x0000CC99
			void ICollection<!0>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
			void ICollection<!0>.CopyTo(T[] array, int arrayIndex)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (T t in this)
				{
					array[arrayIndex++] = t;
				}
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0000EB30 File Offset: 0x0000CD30
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0000EB3D File Offset: 0x0000CD3D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0000EB4A File Offset: 0x0000CD4A
			private void Apply(ImmutableHashSet<T>.MutationResult result)
			{
				this.Root = result.Root;
				if (result.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					this._count += result.Count;
					return;
				}
				this._count = result.Count;
			}

			// Token: 0x040000AD RID: 173
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root = SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.EmptyNode;

			// Token: 0x040000AE RID: 174
			private IEqualityComparer<T> _equalityComparer;

			// Token: 0x040000AF RID: 175
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;

			// Token: 0x040000B0 RID: 176
			private int _count;

			// Token: 0x040000B1 RID: 177
			private ImmutableHashSet<T> _immutable;

			// Token: 0x040000B2 RID: 178
			private int _version;
		}

		// Token: 0x02000094 RID: 148
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, IStrongEnumerator<T>
		{
			// Token: 0x060005C4 RID: 1476 RVA: 0x0000EB84 File Offset: 0x0000CD84
			internal Enumerator([Nullable(new byte[]
			{
				1,
				0,
				0
			})] SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, [Nullable(new byte[]
			{
				2,
				0
			})] ImmutableHashSet<T>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000EBB7 File Offset: 0x0000CDB7
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000EBCF File Offset: 0x0000CDCF
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x0000EBDC File Offset: 0x0000CDDC
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableHashSet<T>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x0000EC36 File Offset: 0x0000CE36
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableHashSet<T>.HashBucket.Enumerator);
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x0000EC76 File Offset: 0x0000CE76
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0000EC8E File Offset: 0x0000CE8E
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x040000B3 RID: 179
			private readonly ImmutableHashSet<T>.Builder _builder;

			// Token: 0x040000B4 RID: 180
			private SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x040000B5 RID: 181
			private ImmutableHashSet<T>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x040000B6 RID: 182
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x02000095 RID: 149
		[NullableContext(0)]
		internal enum OperationResult
		{
			// Token: 0x040000B8 RID: 184
			SizeChanged,
			// Token: 0x040000B9 RID: 185
			NoChangeRequired
		}

		// Token: 0x02000096 RID: 150
		[NullableContext(0)]
		internal readonly struct HashBucket
		{
			// Token: 0x060005CB RID: 1483 RVA: 0x0000ECB6 File Offset: 0x0000CEB6
			private HashBucket(T firstElement, ImmutableList<T>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = (additionalElements ?? ImmutableList<T>.Node.EmptyNode);
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000ECCF File Offset: 0x0000CECF
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0000ECDA File Offset: 0x0000CEDA
			public ImmutableHashSet<T>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.HashBucket.Enumerator(this);
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x0000ECE7 File Offset: 0x0000CEE7
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x0000ECEE File Offset: 0x0000CEEE
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x0000ECF5 File Offset: 0x0000CEF5
			internal bool EqualsByRef(ImmutableHashSet<T>.HashBucket other)
			{
				return this._firstValue == other._firstValue && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x0000ED1F File Offset: 0x0000CF1F
			internal bool EqualsByValue(ImmutableHashSet<T>.HashBucket other, [Nullable(1)] IEqualityComparer<T> valueComparer)
			{
				return valueComparer.Equals(this._firstValue, other._firstValue) && this._additionalElements == other._additionalElements;
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x0000ED48 File Offset: 0x0000CF48
			internal ImmutableHashSet<T>.HashBucket Add([Nullable(1)] T value, [Nullable(1)] IEqualityComparer<T> valueComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(value, null);
				}
				if (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				result = ImmutableHashSet<T>.OperationResult.SizeChanged;
				return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.Add(value));
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x0000EDAB File Offset: 0x0000CFAB
			[NullableContext(1)]
			internal bool Contains(T value, IEqualityComparer<T> valueComparer)
			{
				return !this.IsEmpty && (valueComparer.Equals(value, this._firstValue) || this._additionalElements.IndexOf(value, valueComparer) >= 0);
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x0000EDDC File Offset: 0x0000CFDC
			[NullableContext(1)]
			internal unsafe bool TryExchange(T value, IEqualityComparer<T> valueComparer, out T existingValue)
			{
				if (!this.IsEmpty)
				{
					if (valueComparer.Equals(value, this._firstValue))
					{
						existingValue = this._firstValue;
						return true;
					}
					int num = this._additionalElements.IndexOf(value, valueComparer);
					if (num >= 0)
					{
						existingValue = *this._additionalElements.ItemRef(num);
						return true;
					}
				}
				existingValue = value;
				return false;
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0000EE44 File Offset: 0x0000D044
			internal ImmutableHashSet<T>.HashBucket Remove([Nullable(1)] T value, [Nullable(1)] IEqualityComparer<T> equalityComparer, out ImmutableHashSet<T>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
					return this;
				}
				if (equalityComparer.Equals(this._firstValue, value))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableHashSet<T>.OperationResult.SizeChanged;
						return default(ImmutableHashSet<T>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(value, equalityComparer);
					if (num < 0)
					{
						result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableHashSet<T>.OperationResult.SizeChanged;
					return new ImmutableHashSet<T>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x0000EEF3 File Offset: 0x0000D0F3
			internal void Freeze()
			{
				ImmutableList<T>.Node additionalElements = this._additionalElements;
				if (additionalElements == null)
				{
					return;
				}
				additionalElements.Freeze();
			}

			// Token: 0x040000BA RID: 186
			private readonly T _firstValue;

			// Token: 0x040000BB RID: 187
			private readonly ImmutableList<T>.Node _additionalElements;

			// Token: 0x020000D6 RID: 214
			internal struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x060008AF RID: 2223 RVA: 0x00016722 File Offset: 0x00014922
				internal Enumerator(ImmutableHashSet<T>.HashBucket bucket)
				{
					this._disposed = false;
					this._bucket = bucket;
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<T>.Enumerator);
				}

				// Token: 0x170001CB RID: 459
				// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00016745 File Offset: 0x00014945
				[Nullable(2)]
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x170001CC RID: 460
				// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00016754 File Offset: 0x00014954
				[Nullable(1)]
				public T Current
				{
					[NullableContext(1)]
					get
					{
						this.ThrowIfDisposed();
						ImmutableHashSet<T>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						T result;
						if (currentPosition != ImmutableHashSet<T>.HashBucket.Enumerator.Position.First)
						{
							if (currentPosition != ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional)
							{
								throw new InvalidOperationException();
							}
							result = this._additionalEnumerator.Current;
						}
						else
						{
							result = this._bucket._firstValue;
						}
						return result;
					}
				}

				// Token: 0x060008B2 RID: 2226 RVA: 0x0001679C File Offset: 0x0001499C
				public bool MoveNext()
				{
					this.ThrowIfDisposed();
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<T>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableHashSet<T>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x060008B3 RID: 2227 RVA: 0x00016848 File Offset: 0x00014A48
				public void Reset()
				{
					this.ThrowIfDisposed();
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x060008B4 RID: 2228 RVA: 0x00016862 File Offset: 0x00014A62
				public void Dispose()
				{
					this._disposed = true;
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x060008B5 RID: 2229 RVA: 0x00016876 File Offset: 0x00014A76
				private void ThrowIfDisposed()
				{
					if (this._disposed)
					{
						Requires.FailObjectDisposed<ImmutableHashSet<T>.HashBucket.Enumerator>(this);
					}
				}

				// Token: 0x04000177 RID: 375
				private readonly ImmutableHashSet<T>.HashBucket _bucket;

				// Token: 0x04000178 RID: 376
				private bool _disposed;

				// Token: 0x04000179 RID: 377
				private ImmutableHashSet<T>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x0400017A RID: 378
				private ImmutableList<T>.Enumerator _additionalEnumerator;

				// Token: 0x020000DC RID: 220
				private enum Position
				{
					// Token: 0x04000190 RID: 400
					BeforeFirst,
					// Token: 0x04000191 RID: 401
					First,
					// Token: 0x04000192 RID: 402
					Additional,
					// Token: 0x04000193 RID: 403
					End
				}
			}
		}

		// Token: 0x02000097 RID: 151
		private readonly struct MutationInput
		{
			// Token: 0x060005D7 RID: 1495 RVA: 0x0000EF05 File Offset: 0x0000D105
			internal MutationInput(ImmutableHashSet<T> set)
			{
				Requires.NotNull<ImmutableHashSet<T>>(set, "set");
				this._root = set._root;
				this._equalityComparer = set._equalityComparer;
				this._count = set._count;
				this._hashBucketEqualityComparer = set._hashBucketEqualityComparer;
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x0000EF44 File Offset: 0x0000D144
			internal MutationInput(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, IEqualityComparer<T> equalityComparer, IEqualityComparer<ImmutableHashSet<T>.HashBucket> hashBucketEqualityComparer, int count)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				Requires.Range(count >= 0, "count", null);
				Requires.NotNull<IEqualityComparer<ImmutableHashSet<T>.HashBucket>>(hashBucketEqualityComparer, "hashBucketEqualityComparer");
				this._root = root;
				this._equalityComparer = equalityComparer;
				this._count = count;
				this._hashBucketEqualityComparer = hashBucketEqualityComparer;
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000EFA2 File Offset: 0x0000D1A2
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000EFAA File Offset: 0x0000D1AA
			internal IEqualityComparer<T> EqualityComparer
			{
				get
				{
					return this._equalityComparer;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000EFBA File Offset: 0x0000D1BA
			internal IEqualityComparer<ImmutableHashSet<T>.HashBucket> HashBucketEqualityComparer
			{
				get
				{
					return this._hashBucketEqualityComparer;
				}
			}

			// Token: 0x040000BC RID: 188
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x040000BD RID: 189
			private readonly IEqualityComparer<T> _equalityComparer;

			// Token: 0x040000BE RID: 190
			private readonly int _count;

			// Token: 0x040000BF RID: 191
			private readonly IEqualityComparer<ImmutableHashSet<T>.HashBucket> _hashBucketEqualityComparer;
		}

		// Token: 0x02000098 RID: 152
		private enum CountType
		{
			// Token: 0x040000C1 RID: 193
			Adjustment,
			// Token: 0x040000C2 RID: 194
			FinalValue
		}

		// Token: 0x02000099 RID: 153
		private readonly struct MutationResult
		{
			// Token: 0x060005DD RID: 1501 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
			internal MutationResult(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root, int count, ImmutableHashSet<T>.CountType countType = ImmutableHashSet<T>.CountType.Adjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
				this._count = count;
				this._countType = countType;
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
			internal SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000EFEC File Offset: 0x0000D1EC
			internal int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
			internal ImmutableHashSet<T>.CountType CountType
			{
				get
				{
					return this._countType;
				}
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0000EFFC File Offset: 0x0000D1FC
			internal ImmutableHashSet<T> Finalize(ImmutableHashSet<T> priorSet)
			{
				Requires.NotNull<ImmutableHashSet<T>>(priorSet, "priorSet");
				int num = this.Count;
				if (this.CountType == ImmutableHashSet<T>.CountType.Adjustment)
				{
					num += priorSet._count;
				}
				return priorSet.Wrap(this.Root, num);
			}

			// Token: 0x040000C3 RID: 195
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;

			// Token: 0x040000C4 RID: 196
			private readonly int _count;

			// Token: 0x040000C5 RID: 197
			private readonly ImmutableHashSet<T>.CountType _countType;
		}

		// Token: 0x0200009A RID: 154
		private readonly struct NodeEnumerable : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060005E2 RID: 1506 RVA: 0x0000F039 File Offset: 0x0000D239
			internal NodeEnumerable(SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> root)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket>>(root, "root");
				this._root = root;
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x0000F04D File Offset: 0x0000D24D
			public ImmutableHashSet<T>.Enumerator GetEnumerator()
			{
				return new ImmutableHashSet<T>.Enumerator(this._root, null);
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0000F05B File Offset: 0x0000D25B
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0000F068 File Offset: 0x0000D268
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040000C6 RID: 198
			private readonly SortedInt32KeyNode<ImmutableHashSet<T>.HashBucket> _root;
		}
	}
}
