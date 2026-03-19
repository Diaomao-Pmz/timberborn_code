using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Collections.Immutable
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(ImmutableArray), "Create")]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[NonVersionable]
	public readonly struct ImmutableArray<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<!0>, IEnumerable, IReadOnlyCollection<T>, IList<T>, ICollection<!0>, IEquatable<ImmutableArray<T>>, IList, ICollection, IImmutableArray, IStructuralComparable, IStructuralEquatable, IImmutableList<T>
	{
		// Token: 0x1700003F RID: 63
		T IList<!0>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000044B2 File Offset: 0x000026B2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000044B8 File Offset: 0x000026B8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection<!0>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000044DC File Offset: 0x000026DC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int IReadOnlyCollection<!0>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17000043 RID: 67
		T IReadOnlyList<!0>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004523 File Offset: 0x00002723
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlySpan<T> AsSpan()
		{
			return new ReadOnlySpan<T>(this.array);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004530 File Offset: 0x00002730
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlyMemory<T> AsMemory()
		{
			return new ReadOnlyMemory<T>(this.array);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004540 File Offset: 0x00002740
		public int IndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, 0, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000456C File Offset: 0x0000276C
		public int IndexOf(T item, int startIndex, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, equalityComparer);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004594 File Offset: 0x00002794
		public int IndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000045BF File Offset: 0x000027BF
		public int IndexOf(T item, int startIndex, int count)
		{
			return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000045D0 File Offset: 0x000027D0
		public int IndexOf(T item, int startIndex, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (count == 0 && startIndex == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex + count <= immutableArray.Length, "count", null);
			if (equalityComparer == null)
			{
				equalityComparer = EqualityComparer<T>.Default;
			}
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.IndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (equalityComparer.Equals(immutableArray.array[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000467C File Offset: 0x0000287C
		public int LastIndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.IsEmpty)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, immutableArray.Length - 1, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000046B8 File Offset: 0x000028B8
		public int LastIndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.IsEmpty && startIndex == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000046EB File Offset: 0x000028EB
		public int LastIndexOf(T item, int startIndex, int count)
		{
			return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000046FC File Offset: 0x000028FC
		public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			if (startIndex == 0 && count == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
			if (equalityComparer == null)
			{
				equalityComparer = EqualityComparer<T>.Default;
			}
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.LastIndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (equalityComparer.Equals(item, immutableArray.array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000047A4 File Offset: 0x000029A4
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000047B3 File Offset: 0x000029B3
		public bool Contains(T item, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			return this.IndexOf(item, equalityComparer) >= 0;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000047D0 File Offset: 0x000029D0
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Insert(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.IsEmpty)
			{
				return ImmutableArray.Create<T>(item);
			}
			T[] array = new T[immutableArray.Length + 1];
			array[index] = item;
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + 1, immutableArray.Length - index);
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000486C File Offset: 0x00002A6C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (immutableArray.IsEmpty)
			{
				return ImmutableArray.CreateRange<T>(items);
			}
			int count = ImmutableExtensions.GetCount<T>(ref items);
			if (count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + count];
			if (index != 0)
			{
				Array.Copy(immutableArray.array, array, index);
			}
			if (index != immutableArray.Length)
			{
				Array.Copy(immutableArray.array, index, array, index + count, immutableArray.Length - index);
			}
			if (!items.TryCopyTo(array, index))
			{
				int num = index;
				foreach (T t in items)
				{
					array[num++] = t;
				}
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000496C File Offset: 0x00002B6C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> InsertRange(int index, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> items)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= result.Length, "index", null);
			if (result.IsEmpty)
			{
				return items;
			}
			if (items.IsEmpty)
			{
				return result;
			}
			return result.InsertSpanRangeInternal(index, items.AsSpan());
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000049D4 File Offset: 0x00002BD4
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Add(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.IsEmpty)
			{
				return ImmutableArray.Create<T>(item);
			}
			return immutableArray.Insert(immutableArray.Length, item);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004A08 File Offset: 0x00002C08
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004A2C File Offset: 0x00002C2C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange(T[] items, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<T[]>(items, "items");
			Requires.Range(length >= 0 && length <= items.Length, "length", null);
			if (items.Length == 0 || length == 0)
			{
				return immutableArray;
			}
			if (immutableArray.IsEmpty)
			{
				return ImmutableArray.Create<T>(items, 0, length);
			}
			T[] array = new T[immutableArray.Length + length];
			Array.Copy(immutableArray.array, array, immutableArray.Length);
			Array.Copy(items, 0, array, immutableArray.Length, length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004AC0 File Offset: 0x00002CC0
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange<[Nullable(0)] TDerived>(TDerived[] items) where TDerived : T
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<TDerived[]>(items, "items");
			if (items.Length == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + items.Length];
			Array.Copy(immutableArray.array, array, immutableArray.Length);
			Array.Copy(items, 0, array, immutableArray.Length, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004B28 File Offset: 0x00002D28
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> items, int length)
		{
			ImmutableArray<T> result = this;
			Requires.Range(length >= 0, "length", null);
			if (items.array != null)
			{
				return result.AddRange(items.array, length);
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004B68 File Offset: 0x00002D68
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange<TDerived>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TDerived> items) where TDerived : T
		{
			ImmutableArray<T> result = this;
			if (items.array != null)
			{
				return result.AddRange<TDerived>(items.array);
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004B94 File Offset: 0x00002D94
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004BB8 File Offset: 0x00002DB8
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> SetItem(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index < immutableArray.Length, "index", null);
			T[] array = new T[immutableArray.Length];
			Array.Copy(immutableArray.array, array, immutableArray.Length);
			array[index] = item;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004C1D File Offset: 0x00002E1D
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004C2C File Offset: 0x00002E2C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Replace(T oldValue, T newValue, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			int num = immutableArray.IndexOf(oldValue, 0, immutableArray.Length, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return immutableArray.SetItem(num, newValue);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004C6F File Offset: 0x00002E6F
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Remove(T item)
		{
			return this.Remove(item, EqualityComparer<T>.Default);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004C80 File Offset: 0x00002E80
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Remove(T item, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			int num = result.IndexOf(item, 0, result.Length, equalityComparer);
			if (num >= 0)
			{
				return result.RemoveAt(num);
			}
			return result;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004CBB File Offset: 0x00002EBB
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveAt(int index)
		{
			return this.RemoveRange(index, 1);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004CC8 File Offset: 0x00002EC8
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveRange(int index, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.Range(length >= 0 && index <= immutableArray.Length - length, "length", null);
			if (length == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - length];
			Array.Copy(immutableArray.array, array, index);
			Array.Copy(immutableArray.array, index + length, array, index, immutableArray.Length - index - length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004D65 File Offset: 0x00002F65
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004D74 File Offset: 0x00002F74
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEnumerable<T>>(items, "items");
			SortedSet<int> sortedSet = new SortedSet<int>();
			foreach (T item in items)
			{
				int num = -1;
				do
				{
					num = immutableArray.IndexOf(item, num + 1, equalityComparer);
				}
				while (num >= 0 && !sortedSet.Add(num) && num < immutableArray.Length - 1);
			}
			return immutableArray.RemoveAtRange(sortedSet);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004E0C File Offset: 0x0000300C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004E1A File Offset: 0x0000301A
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveRange([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<T[]>(items.array, "items");
			return this.RemoveRange(items.AsSpan(), equalityComparer);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004E3C File Offset: 0x0000303C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Predicate<T>>(match, "match");
			if (immutableArray.IsEmpty)
			{
				return immutableArray;
			}
			List<int> list = null;
			for (int i = 0; i < immutableArray.array.Length; i++)
			{
				if (match(immutableArray.array[i]))
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(i);
				}
			}
			if (list == null)
			{
				return immutableArray;
			}
			return immutableArray.RemoveAtRange(list);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004EB3 File Offset: 0x000030B3
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Clear()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004EBC File Offset: 0x000030BC
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Sort()
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, Comparer<T>.Default);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004EE4 File Offset: 0x000030E4
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(Comparer<T>.Create(comparison));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004F10 File Offset: 0x00003110
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Sort([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, comparer);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004F34 File Offset: 0x00003134
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Sort(int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0 && index + count <= immutableArray.Length, "count", null);
			if (count > 1)
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				bool flag = false;
				for (int i = index + 1; i < index + count; i++)
				{
					if (comparer.Compare(immutableArray.array[i - 1], immutableArray.array[i]) > 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					T[] array = new T[immutableArray.Length];
					Array.Copy(immutableArray.array, array, immutableArray.Length);
					Array.Sort<T>(array, index, count, comparer);
					return new ImmutableArray<T>(array);
				}
			}
			return immutableArray;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005000 File Offset: 0x00003200
		public IEnumerable<TResult> OfType<[Nullable(2)] TResult>()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array == null || immutableArray.array.Length == 0)
			{
				return Enumerable.Empty<TResult>();
			}
			return immutableArray.array.OfType<TResult>();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005038 File Offset: 0x00003238
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000505C File Offset: 0x0000325C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> AddRange(params T[] items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000507F File Offset: 0x0000327F
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ReadOnlySpan<T> AsSpan(int start, int length)
		{
			return new ReadOnlySpan<T>(this.array, start, length);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005090 File Offset: 0x00003290
		public void CopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(immutableArray.Length <= destination.Length, "destination", null);
			immutableArray.AsSpan().CopyTo(destination);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000050DC File Offset: 0x000032DC
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> InsertRange(int index, T[] items)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= result.Length, "index", null);
			Requires.NotNull<T[]>(items, "items");
			if (items.Length == 0)
			{
				return result;
			}
			if (result.IsEmpty)
			{
				return new ImmutableArray<T>(items);
			}
			return result.InsertSpanRangeInternal(index, items);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005148 File Offset: 0x00003348
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> InsertRange(int index, [ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= result.Length, "index", null);
			if (items.IsEmpty)
			{
				return result;
			}
			if (result.IsEmpty)
			{
				return items.ToImmutableArray<T>();
			}
			return result.InsertSpanRangeInternal(index, items);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000051A8 File Offset: 0x000033A8
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe ImmutableArray<T> RemoveRange([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer = null)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			if (items.IsEmpty || result.IsEmpty)
			{
				return result;
			}
			if (items.Length == 1)
			{
				return result.Remove(*items[0], equalityComparer);
			}
			SortedSet<int> sortedSet = new SortedSet<int>();
			ReadOnlySpan<T> readOnlySpan = items;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T item = *readOnlySpan[i];
				int num = -1;
				do
				{
					num = result.IndexOf(item, num + 1, equalityComparer);
				}
				while (num >= 0 && !sortedSet.Add(num) && num < result.Length - 1);
			}
			return result.RemoveAtRange(sortedSet);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005258 File Offset: 0x00003458
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> RemoveRange(T[] items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer = null)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<T[]>(items, "items");
			return immutableArray.RemoveRange(new ReadOnlySpan<T>(items), equalityComparer);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000528C File Offset: 0x0000348C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Slice(int start, int length)
		{
			ImmutableArray<T> items = this;
			items.ThrowNullRefIfNotInitialized();
			return ImmutableArray.Create<T>(items, start, length);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000052AF File Offset: 0x000034AF
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000052B6 File Offset: 0x000034B6
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000052BD File Offset: 0x000034BD
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000052C4 File Offset: 0x000034C4
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000052CB File Offset: 0x000034CB
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000052D4 File Offset: 0x000034D4
		IImmutableList<T> IImmutableList<!0>.Clear()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Clear();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000052FC File Offset: 0x000034FC
		IImmutableList<T> IImmutableList<!0>.Add(T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Add(value);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005324 File Offset: 0x00003524
		IImmutableList<T> IImmutableList<!0>.AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.AddRange(items);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000534C File Offset: 0x0000354C
		IImmutableList<T> IImmutableList<!0>.Insert(int index, T element)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Insert(index, element);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005378 File Offset: 0x00003578
		IImmutableList<T> IImmutableList<!0>.InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.InsertRange(index, items);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000053A4 File Offset: 0x000035A4
		IImmutableList<T> IImmutableList<!0>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Remove(value, equalityComparer);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000053D0 File Offset: 0x000035D0
		IImmutableList<T> IImmutableList<!0>.RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAll(match);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000053F8 File Offset: 0x000035F8
		IImmutableList<T> IImmutableList<!0>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(items, equalityComparer);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005424 File Offset: 0x00003624
		IImmutableList<T> IImmutableList<!0>.RemoveRange(int index, int count)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(index, count);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005450 File Offset: 0x00003650
		IImmutableList<T> IImmutableList<!0>.RemoveAt(int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAt(index);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005478 File Offset: 0x00003678
		IImmutableList<T> IImmutableList<!0>.SetItem(int index, T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.SetItem(index, value);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000054A4 File Offset: 0x000036A4
		IImmutableList<T> IImmutableList<!0>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000054CE File Offset: 0x000036CE
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000054D5 File Offset: 0x000036D5
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000054DC File Offset: 0x000036DC
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (default(T) == null && value == null);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000550C File Offset: 0x0000370C
		bool IList.Contains(object value)
		{
			if (ImmutableArray<T>.IsCompatibleObject(value))
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Contains((T)((object)value));
			}
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005540 File Offset: 0x00003740
		int IList.IndexOf(object value)
		{
			if (ImmutableArray<T>.IsCompatibleObject(value))
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005572 File Offset: 0x00003772
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005579 File Offset: 0x00003779
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000557C File Offset: 0x0000377C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005580 File Offset: 0x00003780
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000055A2 File Offset: 0x000037A2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000055A5 File Offset: 0x000037A5
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000055AC File Offset: 0x000037AC
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000055B3 File Offset: 0x000037B3
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000049 RID: 73
		[Nullable(2)]
		object IList.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000055EC File Offset: 0x000037EC
		void ICollection.CopyTo(Array array, int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			Array.Copy(immutableArray.array, 0, array, index, immutableArray.Length);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000561C File Offset: 0x0000381C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return true;
					}
					if (immutableArray.array == null)
					{
						return false;
					}
				}
			}
			return immutableArray.array.Equals(array, comparer);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005670 File Offset: 0x00003870
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			IStructuralEquatable structuralEquatable = immutableArray.array;
			if (structuralEquatable == null)
			{
				return immutableArray.GetHashCode();
			}
			return structuralEquatable.GetHashCode(comparer);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000056A4 File Offset: 0x000038A4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					array = immutableArray2.Array;
					if (immutableArray.array == null && array == null)
					{
						return 0;
					}
					if (immutableArray.array == null ^ array == null)
					{
						throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
					}
				}
			}
			if (array == null)
			{
				throw new ArgumentException(SR.ArrayLengthsNotEqual, "other");
			}
			T[] array2 = immutableArray.array;
			if (array2 == null)
			{
				throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
			}
			return array2.CompareTo(array, comparer);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005734 File Offset: 0x00003934
		private ImmutableArray<T> RemoveAtRange(ICollection<int> indicesToRemove)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<ICollection<int>>(indicesToRemove, "indicesToRemove");
			if (indicesToRemove.Count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length - indicesToRemove.Count];
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			foreach (int num4 in indicesToRemove)
			{
				int num5 = (num3 == -1) ? num4 : (num4 - num3 - 1);
				Array.Copy(immutableArray.array, num + num2, array, num, num5);
				num2++;
				num += num5;
				num3 = num4;
			}
			Array.Copy(immutableArray.array, num + num2, array, num, immutableArray.Length - (num + num2));
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000580C File Offset: 0x00003A0C
		private ImmutableArray<T> InsertSpanRangeInternal(int index, ReadOnlySpan<T> items)
		{
			T[] array = new T[this.Length + items.Length];
			if (index != 0)
			{
				Array.Copy(this.array, array, index);
			}
			items.CopyTo(new Span<T>(array, index, items.Length));
			if (index != this.Length)
			{
				Array.Copy(this.array, index, array, index + items.Length, this.Length - index);
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000587F File Offset: 0x00003A7F
		internal ImmutableArray([Nullable(new byte[]
		{
			2,
			1
		})] T[] items)
		{
			this.array = items;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005888 File Offset: 0x00003A88
		[NonVersionable]
		public static bool operator ==([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> left, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005892 File Offset: 0x00003A92
		[NonVersionable]
		public static bool operator !=([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> left, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000058A0 File Offset: 0x00003AA0
		public static bool operator ==([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T>? left, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000058C4 File Offset: 0x00003AC4
		public static bool operator !=([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T>? left, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x1700004A RID: 74
		public T this[int index]
		{
			[NonVersionable]
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000058F8 File Offset: 0x00003AF8
		public ref readonly T ItemRef(int index)
		{
			return ref this.array[index];
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005908 File Offset: 0x00003B08
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			[NonVersionable]
			get
			{
				return this.array.Length == 0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005914 File Offset: 0x00003B14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Length
		{
			[NonVersionable]
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000591E File Offset: 0x00003B1E
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefault
		{
			get
			{
				return this.array == null;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000592C File Offset: 0x00003B2C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefaultOrEmpty
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				return immutableArray.array == null || immutableArray.array.Length == 0;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005954 File Offset: 0x00003B54
		[Nullable(2)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Array IImmutableArray.Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000595C File Offset: 0x00003B5C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				if (!immutableArray.IsDefault)
				{
					return string.Format("Length = {0}", immutableArray.Length);
				}
				return "Uninitialized";
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005998 File Offset: 0x00003B98
		public void CopyTo(T[] destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, destination, immutableArray.Length);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000059C8 File Offset: 0x00003BC8
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, destinationIndex, immutableArray.Length);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000059F8 File Offset: 0x00003BF8
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, sourceIndex, destination, destinationIndex, length);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005A24 File Offset: 0x00003C24
		public ImmutableArray<T>.Builder ToBuilder()
		{
			ImmutableArray<T> items = this;
			if (items.Length == 0)
			{
				return new ImmutableArray<T>.Builder();
			}
			ImmutableArray<T>.Builder builder = new ImmutableArray<T>.Builder(items.Length);
			builder.AddRange(items);
			return builder;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005A5C File Offset: 0x00003C5C
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ImmutableArray<T>.Enumerator GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			return new ImmutableArray<T>.Enumerator(immutableArray.array);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005A84 File Offset: 0x00003C84
		public override int GetHashCode()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array != null)
			{
				return immutableArray.array.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005AB0 File Offset: 0x00003CB0
		[NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			IImmutableArray immutableArray = obj as IImmutableArray;
			return immutableArray != null && this.array == immutableArray.Array;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005AD7 File Offset: 0x00003CD7
		[NonVersionable]
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> other)
		{
			return this.array == other.array;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005AE8 File Offset: 0x00003CE8
		[NullableContext(0)]
		public static ImmutableArray<T> CastUp<[Nullable(2)] TDerived>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TDerived> items) where TDerived : class, T
		{
			T[] items2 = items.array;
			return new ImmutableArray<T>(items2);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005B02 File Offset: 0x00003D02
		[NullableContext(0)]
		public ImmutableArray<TOther> CastArray<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>((TOther[])this.array);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005B14 File Offset: 0x00003D14
		[NullableContext(0)]
		public ImmutableArray<TOther> As<[Nullable(2)] TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>(this.array as TOther[]);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005B28 File Offset: 0x00003D28
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005B50 File Offset: 0x00003D50
		IEnumerator IEnumerable.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005B76 File Offset: 0x00003D76
		internal void ThrowNullRefIfNotInitialized()
		{
			int num = this.array.Length;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005B81 File Offset: 0x00003D81
		private void ThrowInvalidOperationIfNotInitialized()
		{
			if (this.IsDefault)
			{
				throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
			}
		}

		// Token: 0x0400002A RID: 42
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public static readonly ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x0400002B RID: 43
		[Nullable(new byte[]
		{
			2,
			1
		})]
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal readonly T[] array;

		// Token: 0x0200009C RID: 156
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableArrayBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IReadOnlyList<!0>, IReadOnlyCollection<!0>
		{
			// Token: 0x060005E9 RID: 1513 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
			internal Builder(int capacity)
			{
				Requires.Range(capacity >= 0, "capacity", null);
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0000F0D5 File Offset: 0x0000D2D5
			internal Builder() : this(8)
			{
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000F0DE File Offset: 0x0000D2DE
			// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
			public int Capacity
			{
				get
				{
					return this._elements.Length;
				}
				set
				{
					if (value < this._count)
					{
						throw new ArgumentException(SR.CapacityMustBeGreaterThanOrEqualToCount, "value");
					}
					if (value != this._elements.Length)
					{
						if (value > 0)
						{
							T[] array = new T[value];
							if (this._count > 0)
							{
								Array.Copy(this._elements, array, this._count);
							}
							this._elements = array;
							return;
						}
						this._elements = ImmutableArray<T>.Empty.array;
					}
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000F157 File Offset: 0x0000D357
			// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000F160 File Offset: 0x0000D360
			public int Count
			{
				get
				{
					return this._count;
				}
				set
				{
					Requires.Range(value >= 0, "value", null);
					if (value < this._count)
					{
						if (this._count - value > 64)
						{
							Array.Clear(this._elements, value, this._count - value);
						}
						else
						{
							for (int i = value; i < this.Count; i++)
							{
								this._elements[i] = default(T);
							}
						}
					}
					else if (value > this._count)
					{
						this.EnsureCapacity(value);
					}
					this._count = value;
				}
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x0000F1E9 File Offset: 0x0000D3E9
			private static void ThrowIndexOutOfRangeException()
			{
				throw new IndexOutOfRangeException();
			}

			// Token: 0x17000117 RID: 279
			public T this[int index]
			{
				get
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					return this._elements[index];
				}
				set
				{
					if (index >= this.Count)
					{
						ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
					}
					this._elements[index] = value;
				}
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x0000F229 File Offset: 0x0000D429
			public ref readonly T ItemRef(int index)
			{
				if (index >= this.Count)
				{
					ImmutableArray<T>.Builder.ThrowIndexOutOfRangeException();
				}
				return ref this._elements[index];
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000F247 File Offset: 0x0000D447
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0000F24A File Offset: 0x0000D44A
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public ImmutableArray<T> ToImmutable()
			{
				return new ImmutableArray<T>(this.ToArray());
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x0000F257 File Offset: 0x0000D457
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public ImmutableArray<T> MoveToImmutable()
			{
				if (this.Capacity != this.Count)
				{
					throw new InvalidOperationException(SR.CapacityMustEqualCountOnMove);
				}
				T[] elements = this._elements;
				this._elements = ImmutableArray<T>.Empty.array;
				this._count = 0;
				return new ImmutableArray<T>(elements);
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0000F294 File Offset: 0x0000D494
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public ImmutableArray<T> DrainToImmutable()
			{
				T[] array = this._elements;
				if (array.Length != this._count)
				{
					array = this.ToArray();
				}
				this._elements = ImmutableArray<T>.Empty.array;
				this._count = 0;
				return new ImmutableArray<T>(array);
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x0000F2D7 File Offset: 0x0000D4D7
			public void Clear()
			{
				this.Count = 0;
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
			public void Insert(int index, T item)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				this.EnsureCapacity(this.Count + 1);
				if (index < this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + 1, this.Count - index);
				}
				this._count++;
				this._elements[index] = item;
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x0000F35C File Offset: 0x0000D55C
			public void InsertRange(int index, IEnumerable<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(items, "items");
				int count = ImmutableExtensions.GetCount<T>(ref items);
				this.EnsureCapacity(this.Count + count);
				if (index != this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + count, this._count - index);
				}
				if (!items.TryCopyTo(this._elements, index))
				{
					foreach (T t in items)
					{
						this._elements[index++] = t;
					}
				}
				this._count += count;
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x0000F434 File Offset: 0x0000D634
			public void InsertRange(int index, [Nullable(new byte[]
			{
				0,
				1
			})] ImmutableArray<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				if (items.IsEmpty)
				{
					return;
				}
				this.EnsureCapacity(this.Count + items.Length);
				if (index != this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + items.Length, this._count - index);
				}
				Array.Copy(items.array, 0, this._elements, index, items.Length);
				this._count += items.Length;
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
			public void Add(T item)
			{
				int num = this._count + 1;
				this.EnsureCapacity(num);
				this._elements[this._count] = item;
				this._count = num;
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0000F510 File Offset: 0x0000D710
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				int num;
				if (items.TryGetCount(out num))
				{
					this.EnsureCapacity(this.Count + num);
					if (items.TryCopyTo(this._elements, this._count))
					{
						this._count += num;
						return;
					}
				}
				foreach (T item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
			public void AddRange(params T[] items)
			{
				Requires.NotNull<T[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
			public void AddRange<[Nullable(0)] TDerived>(TDerived[] items) where TDerived : T
			{
				Requires.NotNull<TDerived[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				Array.Copy(items, 0, this._elements, count, items.Length);
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0000F620 File Offset: 0x0000D820
			public void AddRange(T[] items, int length)
			{
				Requires.NotNull<T[]>(items, "items");
				Requires.Range(length >= 0 && length <= items.Length, "length", null);
				int count = this.Count;
				this.Count += length;
				Array.Copy(items, 0, this._elements, count, length);
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0000F677 File Offset: 0x0000D877
			public void AddRange([Nullable(new byte[]
			{
				0,
				1
			})] ImmutableArray<T> items)
			{
				this.AddRange(items, items.Length);
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0000F687 File Offset: 0x0000D887
			public void AddRange([Nullable(new byte[]
			{
				0,
				1
			})] ImmutableArray<T> items, int length)
			{
				Requires.Range(length >= 0, "length", null);
				if (items.array != null)
				{
					this.AddRange(items.array, length);
				}
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
			public void AddRange([ParamCollection] [ScopedRef] [Nullable(new byte[]
			{
				0,
				1
			})] ReadOnlySpan<T> items)
			{
				int count = this.Count;
				this.Count += items.Length;
				items.CopyTo(new Span<T>(this._elements, count, items.Length));
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
			[NullableContext(0)]
			public unsafe void AddRange<TDerived>([ParamCollection] [ScopedRef] [Nullable(new byte[]
			{
				0,
				1
			})] ReadOnlySpan<TDerived> items) where TDerived : T
			{
				int count = this.Count;
				this.Count += items.Length;
				Span<T> span = new Span<T>(this._elements, count, items.Length);
				for (int i = 0; i < items.Length; i++)
				{
					*span[i] = (T)((object)(*items[i]));
				}
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0000F767 File Offset: 0x0000D967
			[NullableContext(0)]
			public void AddRange<TDerived>([Nullable(new byte[]
			{
				0,
				1
			})] ImmutableArray<TDerived> items) where TDerived : T
			{
				if (items.array != null)
				{
					this.AddRange<TDerived>(items.array);
				}
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0000F77D File Offset: 0x0000D97D
			public void AddRange([Nullable(new byte[]
			{
				1,
				0
			})] ImmutableArray<T>.Builder items)
			{
				Requires.NotNull<ImmutableArray<T>.Builder>(items, "items");
				this.AddRange(items._elements, items.Count);
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0000F79C File Offset: 0x0000D99C
			public void AddRange<[Nullable(0)] TDerived>(ImmutableArray<TDerived>.Builder items) where TDerived : T
			{
				Requires.NotNull<ImmutableArray<TDerived>.Builder>(items, "items");
				this.AddRange<TDerived>(items._elements, items.Count);
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0000F7BC File Offset: 0x0000D9BC
			public bool Remove(T element)
			{
				int num = this.IndexOf(element);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
			public bool Remove(T element, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				int num = this.IndexOf(element, 0, this._count, equalityComparer);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0000F80C File Offset: 0x0000DA0C
			public void RemoveAll(Predicate<T> match)
			{
				List<int> list = null;
				for (int i = 0; i < this._count; i++)
				{
					if (match(this._elements[i]))
					{
						if (list == null)
						{
							list = new List<int>();
						}
						list.Add(i);
					}
				}
				if (list != null)
				{
					this.RemoveAtRange(list);
				}
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0000F85C File Offset: 0x0000DA5C
			public void RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this.Count - 1)
				{
					Array.Copy(this._elements, index + 1, this._elements, index, this.Count - index - 1);
				}
				int count = this.Count;
				this.Count = count - 1;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
			public void RemoveRange(int index, int length)
			{
				Requires.Range(index >= 0 && index <= this._count, "index", null);
				Requires.Range(length >= 0 && index <= this._count - length, "length", null);
				if (length == 0)
				{
					return;
				}
				if (index + length < this._count)
				{
					Array.Copy(this._elements, index + length, this._elements, index, this.Count - index - length);
				}
				this._count -= length;
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x0000F947 File Offset: 0x0000DB47
			public void RemoveRange(IEnumerable<T> items)
			{
				this.RemoveRange(items, EqualityComparer<T>.Default);
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0000F958 File Offset: 0x0000DB58
			public void RemoveRange(IEnumerable<T> items, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				SortedSet<int> sortedSet = new SortedSet<int>();
				foreach (T item in items)
				{
					int num = this.IndexOf(item, 0, this._count, equalityComparer);
					while (num >= 0 && !sortedSet.Add(num) && num + 1 < this._count)
					{
						num = this.IndexOf(item, num + 1, equalityComparer);
					}
				}
				this.RemoveAtRange(sortedSet);
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
			public void Replace(T oldValue, T newValue)
			{
				this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
			public void Replace(T oldValue, T newValue, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				int num = this.IndexOf(oldValue, 0, this._count, equalityComparer);
				if (num >= 0)
				{
					this._elements[num] = newValue;
				}
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0000FA26 File Offset: 0x0000DC26
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x0000FA38 File Offset: 0x0000DC38
			public T[] ToArray()
			{
				if (this.Count == 0)
				{
					return ImmutableArray<T>.Empty.array;
				}
				T[] array = new T[this.Count];
				Array.Copy(this._elements, array, this.Count);
				return array;
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x0000FA78 File Offset: 0x0000DC78
			public void CopyTo(T[] array, int index)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0 && index + this.Count <= array.Length, "index", null);
				Array.Copy(this._elements, 0, array, index, this.Count);
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x0000FAC6 File Offset: 0x0000DCC6
			public void CopyTo(T[] destination)
			{
				Requires.NotNull<T[]>(destination, "destination");
				Array.Copy(this._elements, 0, destination, 0, this.Count);
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
			public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
			{
				Requires.NotNull<T[]>(destination, "destination");
				Requires.Range(length >= 0, "length", null);
				Requires.Range(sourceIndex >= 0 && sourceIndex + length <= this.Count, "sourceIndex", null);
				Requires.Range(destinationIndex >= 0 && destinationIndex + length <= destination.Length, "destinationIndex", null);
				Array.Copy(this._elements, sourceIndex, destination, destinationIndex, length);
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x0000FB64 File Offset: 0x0000DD64
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int newSize = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, newSize);
				}
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x0000FB99 File Offset: 0x0000DD99
			public int IndexOf(T item)
			{
				return this.IndexOf(item, 0, this._count, EqualityComparer<T>.Default);
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x0000FBAE File Offset: 0x0000DDAE
			public int IndexOf(T item, int startIndex)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, EqualityComparer<T>.Default);
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x0000FBC5 File Offset: 0x0000DDC5
			public int IndexOf(T item, int startIndex, int count)
			{
				return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
			public int IndexOf(T item, int startIndex, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex + count <= this.Count, "count", null);
				if (equalityComparer == null)
				{
					equalityComparer = EqualityComparer<T>.Default;
				}
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.IndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (equalityComparer.Equals(this._elements[i], item))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x0000FC72 File Offset: 0x0000DE72
			public int IndexOf(T item, int startIndex, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, equalityComparer);
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x0000FC85 File Offset: 0x0000DE85
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0000FCAB File Offset: 0x0000DEAB
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				return this.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
			public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
				if (equalityComparer == null)
				{
					equalityComparer = EqualityComparer<T>.Default;
				}
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.LastIndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i >= startIndex - count + 1; i--)
				{
					if (equalityComparer.Equals(item, this._elements[i]))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x0000FD94 File Offset: 0x0000DF94
			public void Reverse()
			{
				int i = 0;
				int num = this._count - 1;
				T[] elements = this._elements;
				while (i < num)
				{
					T t = elements[i];
					elements[i] = elements[num];
					elements[num] = t;
					i++;
					num--;
				}
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0000FDDF File Offset: 0x0000DFDF
			public void Sort()
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this.Count, Comparer<T>.Default);
				}
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0000FE01 File Offset: 0x0000E001
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, Comparer<T>.Create(comparison));
				}
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x0000FE2F File Offset: 0x0000E02F
			public void Sort([Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, comparer);
				}
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0000FE50 File Offset: 0x0000E050
			public void Sort(int index, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
				if (count > 1)
				{
					Array.Sort<T>(this._elements, index, count, comparer);
				}
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
			public void CopyTo([Nullable(new byte[]
			{
				0,
				1
			})] Span<T> destination)
			{
				Requires.Range(this.Count <= destination.Length, "destination", null);
				new ReadOnlySpan<T>(this._elements, 0, this.Count).CopyTo(destination);
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
			public IEnumerator<T> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num + 1)
				{
					yield return this[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0000FF00 File Offset: 0x0000E100
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x0000FF08 File Offset: 0x0000E108
			private void AddRange<TDerived>(TDerived[] items, int length) where TDerived : T
			{
				this.EnsureCapacity(this.Count + length);
				int count = this.Count;
				this.Count += length;
				T[] elements = this._elements;
				for (int i = 0; i < length; i++)
				{
					elements[count + i] = (T)((object)items[i]);
				}
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x0000FF68 File Offset: 0x0000E168
			private void RemoveAtRange(ICollection<int> indicesToRemove)
			{
				Requires.NotNull<ICollection<int>>(indicesToRemove, "indicesToRemove");
				if (indicesToRemove.Count == 0)
				{
					return;
				}
				int num = 0;
				int num2 = 0;
				int num3 = -1;
				foreach (int num4 in indicesToRemove)
				{
					int num5 = (num3 == -1) ? num4 : (num4 - num3 - 1);
					Array.Copy(this._elements, num + num2, this._elements, num, num5);
					num2++;
					num += num5;
					num3 = num4;
				}
				Array.Copy(this._elements, num + num2, this._elements, num, this._elements.Length - (num + num2));
				this._count -= indicesToRemove.Count;
			}

			// Token: 0x040000C8 RID: 200
			private T[] _elements;

			// Token: 0x040000C9 RID: 201
			private int _count;
		}

		// Token: 0x0200009D RID: 157
		[Nullable(0)]
		public struct Enumerator
		{
			// Token: 0x0600062A RID: 1578 RVA: 0x0001002C File Offset: 0x0000E22C
			internal Enumerator(T[] array)
			{
				this._array = array;
				this._index = -1;
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001003C File Offset: 0x0000E23C
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x00010050 File Offset: 0x0000E250
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._array.Length;
			}

			// Token: 0x040000CA RID: 202
			private readonly T[] _array;

			// Token: 0x040000CB RID: 203
			private int _index;
		}

		// Token: 0x0200009E RID: 158
		private sealed class EnumeratorObject : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x0600062D RID: 1581 RVA: 0x00010078 File Offset: 0x0000E278
			private EnumeratorObject(T[] array)
			{
				this._index = -1;
				this._array = array;
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001008E File Offset: 0x0000E28E
			public T Current
			{
				get
				{
					if (this._index < this._array.Length)
					{
						return this._array[this._index];
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x0600062F RID: 1583 RVA: 0x000100B7 File Offset: 0x0000E2B7
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x000100C4 File Offset: 0x0000E2C4
			public bool MoveNext()
			{
				int num = this._index + 1;
				int num2 = this._array.Length;
				if (num <= num2)
				{
					this._index = num;
					return num < num2;
				}
				return false;
			}

			// Token: 0x06000631 RID: 1585 RVA: 0x000100F4 File Offset: 0x0000E2F4
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000632 RID: 1586 RVA: 0x000100FD File Offset: 0x0000E2FD
			public void Dispose()
			{
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x000100FF File Offset: 0x0000E2FF
			internal static IEnumerator<T> Create(T[] array)
			{
				if (array.Length != 0)
				{
					return new ImmutableArray<T>.EnumeratorObject(array);
				}
				return ImmutableArray<T>.EnumeratorObject.s_EmptyEnumerator;
			}

			// Token: 0x040000CC RID: 204
			private static readonly IEnumerator<T> s_EmptyEnumerator = new ImmutableArray<T>.EnumeratorObject(ImmutableArray<T>.Empty.array);

			// Token: 0x040000CD RID: 205
			private readonly T[] _array;

			// Token: 0x040000CE RID: 206
			private int _index;
		}
	}
}
