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
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(ImmutableList), "Create")]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableList<[Nullable(2)] T> : IImmutableList<!0>, IReadOnlyList<!0>, IEnumerable<!0>, IEnumerable, IReadOnlyCollection<!0>, IList<!0>, ICollection<!0>, IList, ICollection, IOrderedCollection<T>, IImmutableListQueries<T>, IStrongEnumerable<T, ImmutableList<T>.Enumerator>
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00007473 File Offset: 0x00005673
		internal ImmutableList()
		{
			this._root = ImmutableList<T>.Node.EmptyNode;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007486 File Offset: 0x00005686
		private ImmutableList(ImmutableList<T>.Node root)
		{
			Requires.NotNull<ImmutableList<T>.Node>(root, "root");
			root.Freeze();
			this._root = root;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000074A6 File Offset: 0x000056A6
		public ImmutableList<T> Clear()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000074AD File Offset: 0x000056AD
		public int BinarySearch(T item)
		{
			return this.BinarySearch(item, null);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000074B7 File Offset: 0x000056B7
		public int BinarySearch(T item, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000074C8 File Offset: 0x000056C8
		public int BinarySearch(int index, int count, T item, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return this._root.BinarySearch(index, count, item, comparer);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000074DA File Offset: 0x000056DA
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			get
			{
				return this._root.IsEmpty;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000074E7 File Offset: 0x000056E7
		IImmutableList<T> IImmutableList<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000074EF File Offset: 0x000056EF
		public int Count
		{
			get
			{
				return this._root.Count;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000074FC File Offset: 0x000056FC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000074FF File Offset: 0x000056FF
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006C RID: 108
		public unsafe T this[int index]
		{
			get
			{
				return *this._root.ItemRef(index);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00007515 File Offset: 0x00005715
		public ref readonly T ItemRef(int index)
		{
			return this._root.ItemRef(index);
		}

		// Token: 0x1700006D RID: 109
		T IOrderedCollection<!0>.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000752C File Offset: 0x0000572C
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		public ImmutableList<T>.Builder ToBuilder()
		{
			return new ImmutableList<T>.Builder(this);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00007534 File Offset: 0x00005734
		public ImmutableList<T> Add(T value)
		{
			ImmutableList<T>.Node root = this._root.Add(value);
			return this.Wrap(root);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007558 File Offset: 0x00005758
		public ImmutableList<T> AddRange(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return ImmutableList<T>.CreateRange(items);
			}
			ImmutableList<T>.Node root = this._root.AddRange(items);
			return this.Wrap(root);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007594 File Offset: 0x00005794
		internal ImmutableList<T> AddRange([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			if (this.IsEmpty)
			{
				if (items.IsEmpty)
				{
					return ImmutableList<T>.Empty;
				}
				return new ImmutableList<T>(ImmutableList<T>.Node.NodeTreeFromList(items));
			}
			else
			{
				if (items.IsEmpty)
				{
					return this;
				}
				return this.Wrap(this._root.AddRange(items));
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000075E1 File Offset: 0x000057E1
		public ImmutableList<T> Insert(int index, T item)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			return this.Wrap(this._root.Insert(index, item));
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007614 File Offset: 0x00005814
		public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableList<T>.Node root = this._root.InsertRange(index, items);
			return this.Wrap(root);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000765F File Offset: 0x0000585F
		public ImmutableList<T> Remove(T value)
		{
			return this.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007670 File Offset: 0x00005870
		public ImmutableList<T> Remove(T value, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(value, equalityComparer);
			if (num >= 0)
			{
				return this.RemoveAt(num);
			}
			return this;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007694 File Offset: 0x00005894
		public ImmutableList<T> RemoveRange(int index, int count)
		{
			Requires.Range(index >= 0 && index <= this.Count, "index", null);
			Requires.Range(count >= 0 && index <= this.Count - count, "count", null);
			ImmutableList<T>.Node node = this._root;
			int num = count;
			while (num-- > 0)
			{
				node = node.RemoveAt(index);
			}
			return this.Wrap(node);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007701 File Offset: 0x00005901
		public ImmutableList<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007710 File Offset: 0x00005910
		public ImmutableList<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (this.IsEmpty)
			{
				return this;
			}
			ImmutableList<T>.Node node = this._root;
			foreach (T item in items.GetEnumerableDisposable<T, ImmutableList<T>.Enumerator>())
			{
				int num = node.IndexOf(item, equalityComparer);
				if (num >= 0)
				{
					node = node.RemoveAt(num);
				}
			}
			return this.Wrap(node);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000779C File Offset: 0x0000599C
		public ImmutableList<T> RemoveAt(int index)
		{
			Requires.Range(index >= 0 && index < this.Count, "index", null);
			ImmutableList<T>.Node root = this._root.RemoveAt(index);
			return this.Wrap(root);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000077D8 File Offset: 0x000059D8
		public ImmutableList<T> RemoveAll(Predicate<T> match)
		{
			Requires.NotNull<Predicate<T>>(match, "match");
			return this.Wrap(this._root.RemoveAll(match));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000077F7 File Offset: 0x000059F7
		public ImmutableList<T> SetItem(int index, T value)
		{
			return this.Wrap(this._root.ReplaceAt(index, value));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000780C File Offset: 0x00005A0C
		public ImmutableList<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000781C File Offset: 0x00005A1C
		public ImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			int num = this.IndexOf(oldValue, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return this.SetItem(num, newValue);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000784E File Offset: 0x00005A4E
		public ImmutableList<T> Reverse()
		{
			return this.Wrap(this._root.Reverse());
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007861 File Offset: 0x00005A61
		public ImmutableList<T> Reverse(int index, int count)
		{
			return this.Wrap(this._root.Reverse(index, count));
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007876 File Offset: 0x00005A76
		public ImmutableList<T> Sort()
		{
			return this.Wrap(this._root.Sort());
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007889 File Offset: 0x00005A89
		public ImmutableList<T> Sort(Comparison<T> comparison)
		{
			Requires.NotNull<Comparison<T>>(comparison, "comparison");
			return this.Wrap(this._root.Sort(comparison));
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000078A8 File Offset: 0x00005AA8
		public ImmutableList<T> Sort([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return this.Wrap(this._root.Sort(comparer));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000078BC File Offset: 0x00005ABC
		public ImmutableList<T> Sort(int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(this._root.Sort(index, count, comparer));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000791C File Offset: 0x00005B1C
		public void ForEach(Action<T> action)
		{
			Requires.NotNull<Action<T>>(action, "action");
			foreach (T obj in this)
			{
				action(obj);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007978 File Offset: 0x00005B78
		public void CopyTo(T[] array)
		{
			this._root.CopyTo(array);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007986 File Offset: 0x00005B86
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007995 File Offset: 0x00005B95
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this._root.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000079A8 File Offset: 0x00005BA8
		public ImmutableList<T> GetRange(int index, int count)
		{
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0, "count", null);
			Requires.Range(index + count <= this.Count, "count", null);
			return this.Wrap(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007A00 File Offset: 0x00005C00
		public ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
		{
			Requires.NotNull<Func<T, TOutput>>(converter, "converter");
			return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007A1E File Offset: 0x00005C1E
		public bool Exists(Predicate<T> match)
		{
			return this._root.Exists(match);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007A2C File Offset: 0x00005C2C
		[return: Nullable(2)]
		public T Find(Predicate<T> match)
		{
			return this._root.Find(match);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00007A3A File Offset: 0x00005C3A
		public ImmutableList<T> FindAll(Predicate<T> match)
		{
			return this._root.FindAll(match);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007A48 File Offset: 0x00005C48
		public int FindIndex(Predicate<T> match)
		{
			return this._root.FindIndex(match);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007A56 File Offset: 0x00005C56
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, match);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007A65 File Offset: 0x00005C65
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindIndex(startIndex, count, match);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00007A75 File Offset: 0x00005C75
		[return: Nullable(2)]
		public T FindLast(Predicate<T> match)
		{
			return this._root.FindLast(match);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007A83 File Offset: 0x00005C83
		public int FindLastIndex(Predicate<T> match)
		{
			return this._root.FindLastIndex(match);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007A91 File Offset: 0x00005C91
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, match);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			return this._root.FindLastIndex(startIndex, count, match);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public int IndexOf(T item, int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			return this._root.IndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007AC2 File Offset: 0x00005CC2
		public int LastIndexOf(T item, int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			return this._root.LastIndexOf(item, index, count, equalityComparer);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public bool TrueForAll(Predicate<T> match)
		{
			return this._root.TrueForAll(match);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007AE2 File Offset: 0x00005CE2
		public bool Contains(T value)
		{
			return this._root.Contains(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007AF5 File Offset: 0x00005CF5
		public int IndexOf(T value)
		{
			return this.IndexOf(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007B03 File Offset: 0x00005D03
		IImmutableList<T> IImmutableList<!0>.Add(T value)
		{
			return this.Add(value);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007B0C File Offset: 0x00005D0C
		IImmutableList<T> IImmutableList<!0>.AddRange(IEnumerable<T> items)
		{
			return this.AddRange(items);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007B15 File Offset: 0x00005D15
		IImmutableList<T> IImmutableList<!0>.Insert(int index, T item)
		{
			return this.Insert(index, item);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007B1F File Offset: 0x00005D1F
		IImmutableList<T> IImmutableList<!0>.InsertRange(int index, IEnumerable<T> items)
		{
			return this.InsertRange(index, items);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007B29 File Offset: 0x00005D29
		IImmutableList<T> IImmutableList<!0>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			return this.Remove(value, equalityComparer);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007B33 File Offset: 0x00005D33
		IImmutableList<T> IImmutableList<!0>.RemoveAll(Predicate<T> match)
		{
			return this.RemoveAll(match);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007B3C File Offset: 0x00005D3C
		IImmutableList<T> IImmutableList<!0>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			return this.RemoveRange(items, equalityComparer);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007B46 File Offset: 0x00005D46
		IImmutableList<T> IImmutableList<!0>.RemoveRange(int index, int count)
		{
			return this.RemoveRange(index, count);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007B50 File Offset: 0x00005D50
		IImmutableList<T> IImmutableList<!0>.RemoveAt(int index)
		{
			return this.RemoveAt(index);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00007B59 File Offset: 0x00005D59
		IImmutableList<T> IImmutableList<!0>.SetItem(int index, T value)
		{
			return this.SetItem(index, value);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00007B63 File Offset: 0x00005D63
		IImmutableList<T> IImmutableList<!0>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			return this.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007B70 File Offset: 0x00005D70
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007B9D File Offset: 0x00005D9D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007BAA File Offset: 0x00005DAA
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007BB1 File Offset: 0x00005DB1
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700006E RID: 110
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

		// Token: 0x060002AD RID: 685 RVA: 0x00007BC8 File Offset: 0x00005DC8
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00007BCF File Offset: 0x00005DCF
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007BD6 File Offset: 0x00005DD6
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007BD9 File Offset: 0x00005DD9
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007BE0 File Offset: 0x00005DE0
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this._root.CopyTo(array, arrayIndex);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007BEF File Offset: 0x00005DEF
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007BF6 File Offset: 0x00005DF6
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00007BFD File Offset: 0x00005DFD
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00007C04 File Offset: 0x00005E04
		bool IList.Contains(object value)
		{
			return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00007C1C File Offset: 0x00005E1C
		int IList.IndexOf(object value)
		{
			if (!ImmutableList<T>.IsCompatibleObject(value))
			{
				return -1;
			}
			return this.IndexOf((T)((object)value));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00007C34 File Offset: 0x00005E34
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00007C3B File Offset: 0x00005E3B
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00007C3E File Offset: 0x00005E3E
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00007C41 File Offset: 0x00005E41
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000072 RID: 114
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

		// Token: 0x060002BD RID: 701 RVA: 0x00007C5D File Offset: 0x00005E5D
		[NullableContext(0)]
		public ImmutableList<T>.Enumerator GetEnumerator()
		{
			return new ImmutableList<T>.Enumerator(this._root, null, -1, -1, false);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00007C6E File Offset: 0x00005E6E
		[Nullable(new byte[]
		{
			1,
			0
		})]
		internal ImmutableList<T>.Node Root
		{
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			get
			{
				return this._root;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007C76 File Offset: 0x00005E76
		private static ImmutableList<T> WrapNode(ImmutableList<T>.Node root)
		{
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return ImmutableList<T>.Empty;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007C8C File Offset: 0x00005E8C
		private static bool TryCastToImmutableList(IEnumerable<T> sequence, [NotNullWhen(true)] out ImmutableList<T> other)
		{
			other = (sequence as ImmutableList<T>);
			if (other != null)
			{
				return true;
			}
			ImmutableList<T>.Builder builder = sequence as ImmutableList<T>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007CBC File Offset: 0x00005EBC
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (default(T) == null && value == null);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007CE9 File Offset: 0x00005EE9
		private ImmutableList<T> Wrap(ImmutableList<T>.Node root)
		{
			if (root == this._root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableList<T>(root);
			}
			return this.Clear();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007D0C File Offset: 0x00005F0C
		private static ImmutableList<T> CreateRange(IEnumerable<T> items)
		{
			ImmutableList<T> result;
			if (ImmutableList<T>.TryCastToImmutableList(items, out result))
			{
				return result;
			}
			IOrderedCollection<T> orderedCollection = items.AsOrderedCollection<T>();
			if (orderedCollection.Count == 0)
			{
				return ImmutableList<T>.Empty;
			}
			return new ImmutableList<T>(ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count));
		}

		// Token: 0x04000036 RID: 54
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x04000037 RID: 55
		private readonly ImmutableList<T>.Node _root;

		// Token: 0x020000B0 RID: 176
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableListBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IList, ICollection, IOrderedCollection<!0>, IImmutableListQueries<T>, IReadOnlyList<!0>, IReadOnlyCollection<!0>
		{
			// Token: 0x060006C1 RID: 1729 RVA: 0x000114CA File Offset: 0x0000F6CA
			internal Builder(ImmutableList<T> list)
			{
				Requires.NotNull<ImmutableList<T>>(list, "list");
				this._root = list._root;
				this._immutable = list;
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000114FB File Offset: 0x0000F6FB
			public int Count
			{
				get
				{
					return this.Root.Count;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00011508 File Offset: 0x0000F708
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001150B File Offset: 0x0000F70B
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00011513 File Offset: 0x0000F713
			// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001151B File Offset: 0x0000F71B
			[Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Root
			{
				[return: Nullable(new byte[]
				{
					1,
					0
				})]
				get
				{
					return this._root;
				}
				private set
				{
					this._version++;
					if (this._root != value)
					{
						this._root = value;
						this._immutable = null;
					}
				}
			}

			// Token: 0x1700014C RID: 332
			public unsafe T this[int index]
			{
				get
				{
					return *this.Root.ItemRef(index);
				}
				set
				{
					this.Root = this.Root.ReplaceAt(index, value);
				}
			}

			// Token: 0x1700014D RID: 333
			T IOrderedCollection<!0>.this[int index]
			{
				get
				{
					return this[index];
				}
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x00011573 File Offset: 0x0000F773
			public ref readonly T ItemRef(int index)
			{
				return this.Root.ItemRef(index);
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x00011581 File Offset: 0x0000F781
			public int IndexOf(T item)
			{
				return this.Root.IndexOf(item, EqualityComparer<T>.Default);
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x00011594 File Offset: 0x0000F794
			public void Insert(int index, T item)
			{
				this.Root = this.Root.Insert(index, item);
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x000115A9 File Offset: 0x0000F7A9
			public void RemoveAt(int index)
			{
				this.Root = this.Root.RemoveAt(index);
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x000115BD File Offset: 0x0000F7BD
			public void Add(T item)
			{
				this.Root = this.Root.Add(item);
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x000115D1 File Offset: 0x0000F7D1
			public void Clear()
			{
				this.Root = ImmutableList<T>.Node.EmptyNode;
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x000115DE File Offset: 0x0000F7DE
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x000115F0 File Offset: 0x0000F7F0
			public bool Remove(T item)
			{
				int num = this.IndexOf(item);
				if (num < 0)
				{
					return false;
				}
				this.Root = this.Root.RemoveAt(num);
				return true;
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x0001161E File Offset: 0x0000F81E
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return this.Root.GetEnumerator(this);
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x0001162C File Offset: 0x0000F82C
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x00011639 File Offset: 0x0000F839
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060006D5 RID: 1749 RVA: 0x00011648 File Offset: 0x0000F848
			public void ForEach(Action<T> action)
			{
				Requires.NotNull<Action<T>>(action, "action");
				foreach (T obj in this)
				{
					action(obj);
				}
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x000116A4 File Offset: 0x0000F8A4
			public void CopyTo(T[] array)
			{
				this._root.CopyTo(array);
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x000116B2 File Offset: 0x0000F8B2
			public void CopyTo(T[] array, int arrayIndex)
			{
				this._root.CopyTo(array, arrayIndex);
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x000116C1 File Offset: 0x0000F8C1
			public void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				this._root.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x000116D4 File Offset: 0x0000F8D4
			public ImmutableList<T> GetRange(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				return ImmutableList<T>.WrapNode(ImmutableList<T>.Node.NodeTreeFromList(this, index, count));
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x0001172B File Offset: 0x0000F92B
			public ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
			{
				Requires.NotNull<Func<T, TOutput>>(converter, "converter");
				return ImmutableList<TOutput>.WrapNode(this._root.ConvertAll<TOutput>(converter));
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x00011749 File Offset: 0x0000F949
			public bool Exists(Predicate<T> match)
			{
				return this._root.Exists(match);
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x00011757 File Offset: 0x0000F957
			[return: Nullable(2)]
			public T Find(Predicate<T> match)
			{
				return this._root.Find(match);
			}

			// Token: 0x060006DD RID: 1757 RVA: 0x00011765 File Offset: 0x0000F965
			public ImmutableList<T> FindAll(Predicate<T> match)
			{
				return this._root.FindAll(match);
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x00011773 File Offset: 0x0000F973
			public int FindIndex(Predicate<T> match)
			{
				return this._root.FindIndex(match);
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x00011781 File Offset: 0x0000F981
			public int FindIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, match);
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x00011790 File Offset: 0x0000F990
			public int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindIndex(startIndex, count, match);
			}

			// Token: 0x060006E1 RID: 1761 RVA: 0x000117A0 File Offset: 0x0000F9A0
			[return: Nullable(2)]
			public T FindLast(Predicate<T> match)
			{
				return this._root.FindLast(match);
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x000117AE File Offset: 0x0000F9AE
			public int FindLastIndex(Predicate<T> match)
			{
				return this._root.FindLastIndex(match);
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x000117BC File Offset: 0x0000F9BC
			public int FindLastIndex(int startIndex, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, match);
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x000117CB File Offset: 0x0000F9CB
			public int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				return this._root.FindLastIndex(startIndex, count, match);
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x000117DB File Offset: 0x0000F9DB
			public int IndexOf(T item, int index)
			{
				return this._root.IndexOf(item, index, this.Count - index, EqualityComparer<T>.Default);
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x000117F7 File Offset: 0x0000F9F7
			public int IndexOf(T item, int index, int count)
			{
				return this._root.IndexOf(item, index, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x0001180C File Offset: 0x0000FA0C
			public int IndexOf(T item, int index, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				return this._root.IndexOf(item, index, count, equalityComparer);
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x0001181E File Offset: 0x0000FA1E
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x00011849 File Offset: 0x0000FA49
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				return this._root.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x0001186D File Offset: 0x0000FA6D
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this._root.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x00011882 File Offset: 0x0000FA82
			public int LastIndexOf(T item, int startIndex, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				return this._root.LastIndexOf(item, startIndex, count, equalityComparer);
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x00011894 File Offset: 0x0000FA94
			public bool TrueForAll(Predicate<T> match)
			{
				return this._root.TrueForAll(match);
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x000118A2 File Offset: 0x0000FAA2
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.AddRange(items);
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x000118C1 File Offset: 0x0000FAC1
			public void InsertRange(int index, IEnumerable<T> items)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(items, "items");
				this.Root = this.Root.InsertRange(index, items);
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x000118FF File Offset: 0x0000FAFF
			public int RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				int count = this.Count;
				this.Root = this.Root.RemoveAll(match);
				return count - this.Count;
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x0001192C File Offset: 0x0000FB2C
			public bool Remove(T item, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				int num = this.IndexOf(item, 0, this.Count, equalityComparer);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x00011958 File Offset: 0x0000FB58
			public void RemoveRange(int index, int count)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.Range(count >= 0 && index <= this.Count - count, "count", null);
				int num = count;
				while (num-- > 0)
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x000119B8 File Offset: 0x0000FBB8
			public void RemoveRange(IEnumerable<T> items, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				foreach (T item in items.GetEnumerableDisposable<T, ImmutableList<T>.Enumerator>())
				{
					int num = this.Root.IndexOf(item, equalityComparer);
					if (num >= 0)
					{
						this.RemoveAt(num);
					}
				}
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x00011A2C File Offset: 0x0000FC2C
			public void RemoveRange(IEnumerable<T> items)
			{
				this.RemoveRange(items, EqualityComparer<T>.Default);
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x00011A3A File Offset: 0x0000FC3A
			public void Replace(T oldValue, T newValue)
			{
				this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x00011A4C File Offset: 0x0000FC4C
			public void Replace(T oldValue, T newValue, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				int num = this.IndexOf(oldValue, 0, this.Count, equalityComparer);
				if (num < 0)
				{
					throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
				}
				this.Root = this.Root.ReplaceAt(num, newValue);
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00011A90 File Offset: 0x0000FC90
			public void Reverse()
			{
				this.Reverse(0, this.Count);
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x00011AA0 File Offset: 0x0000FCA0
			public void Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Reverse(index, count);
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x00011AFD File Offset: 0x0000FCFD
			public void Sort()
			{
				this.Root = this.Root.Sort();
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x00011B10 File Offset: 0x0000FD10
			public void Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				this.Root = this.Root.Sort(comparison);
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x00011B2F File Offset: 0x0000FD2F
			public void Sort([Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				this.Root = this.Root.Sort(comparer);
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x00011B44 File Offset: 0x0000FD44
			public void Sort(int index, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				this.Root = this.Root.Sort(index, count, comparer);
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x00011BA2 File Offset: 0x0000FDA2
			public int BinarySearch(T item)
			{
				return this.BinarySearch(item, null);
			}

			// Token: 0x060006FD RID: 1789 RVA: 0x00011BAC File Offset: 0x0000FDAC
			public int BinarySearch(T item, [Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				return this.BinarySearch(0, this.Count, item, comparer);
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x00011BBD File Offset: 0x0000FDBD
			public int BinarySearch(int index, int count, T item, [Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				return this.Root.BinarySearch(index, count, item, comparer);
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x00011BD0 File Offset: 0x0000FDD0
			public ImmutableList<T> ToImmutable()
			{
				ImmutableList<T> result;
				if ((result = this._immutable) == null)
				{
					result = (this._immutable = ImmutableList<T>.WrapNode(this.Root));
				}
				return result;
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x00011BFB File Offset: 0x0000FDFB
			int IList.Add(object value)
			{
				this.Add((T)((object)value));
				return this.Count - 1;
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00011C11 File Offset: 0x0000FE11
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00011C19 File Offset: 0x0000FE19
			bool IList.Contains(object value)
			{
				return ImmutableList<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00011C31 File Offset: 0x0000FE31
			int IList.IndexOf(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					return this.IndexOf((T)((object)value));
				}
				return -1;
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00011C49 File Offset: 0x0000FE49
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (T)((object)value));
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000705 RID: 1797 RVA: 0x00011C58 File Offset: 0x0000FE58
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000706 RID: 1798 RVA: 0x00011C5B File Offset: 0x0000FE5B
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x00011C5E File Offset: 0x0000FE5E
			void IList.Remove(object value)
			{
				if (ImmutableList<T>.IsCompatibleObject(value))
				{
					this.Remove((T)((object)value));
				}
			}

			// Token: 0x17000150 RID: 336
			[Nullable(2)]
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					this[index] = (T)((object)value);
				}
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x00011C92 File Offset: 0x0000FE92
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				this.Root.CopyTo(array, arrayIndex);
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x0600070B RID: 1803 RVA: 0x00011CA1 File Offset: 0x0000FEA1
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600070C RID: 1804 RVA: 0x00011CA4 File Offset: 0x0000FEA4
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

			// Token: 0x04000103 RID: 259
			private ImmutableList<T>.Node _root = ImmutableList<T>.Node.EmptyNode;

			// Token: 0x04000104 RID: 260
			private ImmutableList<T> _immutable;

			// Token: 0x04000105 RID: 261
			private int _version;

			// Token: 0x04000106 RID: 262
			private object _syncRoot;
		}

		// Token: 0x020000B1 RID: 177
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable, ISecurePooledObjectUser, IStrongEnumerator<T>
		{
			// Token: 0x0600070D RID: 1805 RVA: 0x00011CC8 File Offset: 0x0000FEC8
			internal Enumerator([Nullable(new byte[]
			{
				1,
				0
			})] ImmutableList<T>.Node root, [Nullable(new byte[]
			{
				2,
				0
			})] ImmutableList<T>.Builder builder = null, int startIndex = -1, int count = -1, bool reversed = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(root, "root");
				Requires.Range(startIndex >= -1, "startIndex", null);
				Requires.Range(count >= -1, "count", null);
				Requires.Argument(reversed || count == -1 || ((startIndex == -1) ? 0 : startIndex) + count <= root.Count);
				Requires.Argument(!reversed || count == -1 || ((startIndex == -1) ? (root.Count - 1) : startIndex) - count + 1 >= 0);
				this._root = root;
				this._builder = builder;
				this._current = null;
				this._startIndex = ((startIndex >= 0) ? startIndex : (reversed ? (root.Count - 1) : 0));
				this._count = ((count == -1) ? root.Count : count);
				this._remainingCount = this._count;
				this._reversed = reversed;
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (this._count > 0)
				{
					if (!SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>.TryTake(this, out this._stack))
					{
						this._stack = SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>.PrepNew(this, new Stack<RefAsValueType<ImmutableList<T>.Node>>(root.Height));
					}
					this.ResetStack();
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600070E RID: 1806 RVA: 0x00011E13 File Offset: 0x00010013
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600070F RID: 1807 RVA: 0x00011E1B File Offset: 0x0001001B
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

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000710 RID: 1808 RVA: 0x00011E3C File Offset: 0x0001003C
			[Nullable(2)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x00011E4C File Offset: 0x0001004C
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack;
				if (this._stack != null && this._stack.TryUse<ImmutableList<T>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
					SecureObjectPool<Stack<RefAsValueType<ImmutableList<T>.Node>>, ImmutableList<T>.Enumerator>.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x00011EA0 File Offset: 0x000100A0
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				this.ThrowIfChanged();
				if (this._stack != null)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					if (this._remainingCount > 0 && stack.Count > 0)
					{
						ImmutableList<T>.Node value = stack.Pop().Value;
						this._current = value;
						this.PushNext(this.NextBranch(value));
						this._remainingCount--;
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x00011F18 File Offset: 0x00010118
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._remainingCount = this._count;
				if (this._stack != null)
				{
					this.ResetStack();
				}
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x00011F58 File Offset: 0x00010158
			private void ResetStack()
			{
				Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
				stack.ClearFastWhenEmpty<RefAsValueType<ImmutableList<T>.Node>>();
				ImmutableList<T>.Node node = this._root;
				int num = this._reversed ? (this._root.Count - this._startIndex - 1) : this._startIndex;
				while (!node.IsEmpty && num != this.PreviousBranch(node).Count)
				{
					if (num < this.PreviousBranch(node).Count)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
					else
					{
						num -= this.PreviousBranch(node).Count + 1;
						node = this.NextBranch(node);
					}
				}
				if (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
				}
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x0001200F File Offset: 0x0001020F
			private ImmutableList<T>.Node NextBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Right;
				}
				return node.Left;
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x00012026 File Offset: 0x00010226
			private ImmutableList<T>.Node PreviousBranch(ImmutableList<T>.Node node)
			{
				if (!this._reversed)
				{
					return node.Left;
				}
				return node.Right;
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x0001203D File Offset: 0x0001023D
			private void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<ImmutableList<T>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<ImmutableList<T>.Enumerator>(this);
				}
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x00012068 File Offset: 0x00010268
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x00012090 File Offset: 0x00010290
			private void PushNext(ImmutableList<T>.Node node)
			{
				Requires.NotNull<ImmutableList<T>.Node>(node, "node");
				if (!node.IsEmpty)
				{
					Stack<RefAsValueType<ImmutableList<T>.Node>> stack = this._stack.Use<ImmutableList<T>.Enumerator>(ref this);
					while (!node.IsEmpty)
					{
						stack.Push(new RefAsValueType<ImmutableList<T>.Node>(node));
						node = this.PreviousBranch(node);
					}
				}
			}

			// Token: 0x04000107 RID: 263
			private readonly ImmutableList<T>.Builder _builder;

			// Token: 0x04000108 RID: 264
			private readonly int _poolUserId;

			// Token: 0x04000109 RID: 265
			private readonly int _startIndex;

			// Token: 0x0400010A RID: 266
			private readonly int _count;

			// Token: 0x0400010B RID: 267
			private int _remainingCount;

			// Token: 0x0400010C RID: 268
			private readonly bool _reversed;

			// Token: 0x0400010D RID: 269
			private ImmutableList<T>.Node _root;

			// Token: 0x0400010E RID: 270
			private SecurePooledObject<Stack<RefAsValueType<ImmutableList<T>.Node>>> _stack;

			// Token: 0x0400010F RID: 271
			private ImmutableList<T>.Node _current;

			// Token: 0x04000110 RID: 272
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020000B2 RID: 178
		[Nullable(0)]
		[DebuggerDisplay("{_key}")]
		internal sealed class Node : IBinaryTree<T>, IBinaryTree, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x0600071A RID: 1818 RVA: 0x000120DC File Offset: 0x000102DC
			private Node()
			{
				this._frozen = true;
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x000120EC File Offset: 0x000102EC
			private Node(T key, ImmutableList<T>.Node left, ImmutableList<T>.Node right, bool frozen = false)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				this._key = key;
				this._left = left;
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(left, right);
				this._count = ImmutableList<T>.Node.ParentCount(left, right);
				this._frozen = frozen;
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001214C File Offset: 0x0001034C
			public bool IsEmpty
			{
				get
				{
					return this._left == null;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x00012157 File Offset: 0x00010357
			public int Height
			{
				get
				{
					return (int)this._height;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001215F File Offset: 0x0001035F
			[Nullable(new byte[]
			{
				2,
				0
			})]
			public ImmutableList<T>.Node Left
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

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x00012167 File Offset: 0x00010367
			[Nullable(2)]
			IBinaryTree IBinaryTree.Left
			{
				get
				{
					return this._left;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001216F File Offset: 0x0001036F
			[Nullable(new byte[]
			{
				2,
				0
			})]
			public ImmutableList<T>.Node Right
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

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x00012177 File Offset: 0x00010377
			[Nullable(2)]
			IBinaryTree IBinaryTree.Right
			{
				get
				{
					return this._right;
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001217F File Offset: 0x0001037F
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

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x00012187 File Offset: 0x00010387
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

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001218F File Offset: 0x0001038F
			public T Value
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x00012197 File Offset: 0x00010397
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001219F File Offset: 0x0001039F
			internal T Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000161 RID: 353
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

			// Token: 0x06000728 RID: 1832 RVA: 0x0001221A File Offset: 0x0001041A
			internal ref readonly T ItemRef(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				return this.ItemRefUnchecked(index);
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x00012240 File Offset: 0x00010440
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

			// Token: 0x0600072A RID: 1834 RVA: 0x00012297 File Offset: 0x00010497
			[NullableContext(0)]
			public ImmutableList<T>.Enumerator GetEnumerator()
			{
				return new ImmutableList<T>.Enumerator(this, null, -1, -1, false);
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x000122A3 File Offset: 0x000104A3
			[ExcludeFromCodeCoverage]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x000122B0 File Offset: 0x000104B0
			[ExcludeFromCodeCoverage]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x000122BD File Offset: 0x000104BD
			[NullableContext(0)]
			internal ImmutableList<T>.Enumerator GetEnumerator([Nullable(new byte[]
			{
				1,
				0
			})] ImmutableList<T>.Builder builder)
			{
				return new ImmutableList<T>.Enumerator(this, builder, -1, -1, false);
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x000122CC File Offset: 0x000104CC
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal static ImmutableList<T>.Node NodeTreeFromList(IOrderedCollection<T> items, int start, int length)
			{
				Requires.NotNull<IOrderedCollection<T>>(items, "items");
				Requires.Range(start >= 0, "start", null);
				Requires.Range(length >= 0, "length", null);
				if (length == 0)
				{
					return ImmutableList<T>.Node.EmptyNode;
				}
				int num = (length - 1) / 2;
				int num2 = length - 1 - num;
				ImmutableList<T>.Node left = ImmutableList<T>.Node.NodeTreeFromList(items, start, num2);
				ImmutableList<T>.Node right = ImmutableList<T>.Node.NodeTreeFromList(items, start + num2 + 1, num);
				return new ImmutableList<T>.Node(items[start + num2], left, right, true);
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x00012344 File Offset: 0x00010544
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal unsafe static ImmutableList<T>.Node NodeTreeFromList([Nullable(new byte[]
			{
				0,
				1
			})] ReadOnlySpan<T> items)
			{
				if (items.IsEmpty)
				{
					return ImmutableList<T>.Node.EmptyNode;
				}
				int num = (items.Length - 1) / 2;
				int num2 = items.Length - 1 - num;
				ImmutableList<T>.Node left = ImmutableList<T>.Node.NodeTreeFromList(items.Slice(0, num2));
				ImmutableList<T>.Node right = ImmutableList<T>.Node.NodeTreeFromList(items.Slice(num2 + 1));
				return new ImmutableList<T>.Node(*items[num2], left, right, true);
			}

			// Token: 0x06000730 RID: 1840 RVA: 0x000123AC File Offset: 0x000105AC
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Add(T key)
			{
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateLeaf(key);
				}
				ImmutableList<T>.Node right = this._right.Add(key);
				ImmutableList<T>.Node node = this.MutateRight(right);
				if (!node.IsBalanced)
				{
					return node.BalanceRight();
				}
				return node;
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x000123F0 File Offset: 0x000105F0
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Insert(int index, T key)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateLeaf(key);
				}
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node left = this._left.Insert(index, key);
					ImmutableList<T>.Node node = this.MutateLeft(left);
					if (!node.IsBalanced)
					{
						return node.BalanceLeft();
					}
					return node;
				}
				else
				{
					ImmutableList<T>.Node right = this._right.Insert(index - this._left._count - 1, key);
					ImmutableList<T>.Node node2 = this.MutateRight(right);
					if (!node2.IsBalanced)
					{
						return node2.BalanceRight();
					}
					return node2;
				}
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x00012494 File Offset: 0x00010694
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node AddRange(IEnumerable<T> keys)
			{
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateRange(keys);
				}
				ImmutableList<T>.Node right = this._right.AddRange(keys);
				return this.MutateRight(right).BalanceMany();
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x000124D4 File Offset: 0x000106D4
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node AddRange([Nullable(new byte[]
			{
				0,
				1
			})] ReadOnlySpan<T> keys)
			{
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.NodeTreeFromList(keys);
				}
				ImmutableList<T>.Node right = this._right.AddRange(keys);
				return this.MutateRight(right).BalanceMany();
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x0001250C File Offset: 0x0001070C
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node InsertRange(int index, IEnumerable<T> keys)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				Requires.NotNull<IEnumerable<T>>(keys, "keys");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Node.CreateRange(keys);
				}
				ImmutableList<T>.Node node;
				if (index <= this._left._count)
				{
					ImmutableList<T>.Node left = this._left.InsertRange(index, keys);
					node = this.MutateLeft(left);
				}
				else
				{
					ImmutableList<T>.Node right = this._right.InsertRange(index - this._left._count - 1, keys);
					node = this.MutateRight(right);
				}
				return node.BalanceMany();
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x000125A4 File Offset: 0x000107A4
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node node;
				if (index == this._left._count)
				{
					if (this._right.IsEmpty && this._left.IsEmpty)
					{
						node = ImmutableList<T>.Node.EmptyNode;
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
						ImmutableList<T>.Node node2 = this._right;
						while (!node2._left.IsEmpty)
						{
							node2 = node2._left;
						}
						ImmutableList<T>.Node right = this._right.RemoveAt(0);
						node = node2.MutateBoth(this._left, right);
					}
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node left = this._left.RemoveAt(index);
					node = this.MutateLeft(left);
				}
				else
				{
					ImmutableList<T>.Node right2 = this._right.RemoveAt(index - this._left._count - 1);
					node = this.MutateRight(right2);
				}
				if (!node.IsEmpty && !node.IsBalanced)
				{
					return node.Balance();
				}
				return node;
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x000126EC File Offset: 0x000108EC
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node RemoveAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				ImmutableList<T>.Node node = this;
				ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(node, null, -1, -1, false);
				try
				{
					int num = 0;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							node = node.RemoveAt(num);
							enumerator.Dispose();
							enumerator = new ImmutableList<T>.Enumerator(node, null, num, -1, false);
						}
						else
						{
							num++;
						}
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				return node;
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x0001276C File Offset: 0x0001096C
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node ReplaceAt(int index, T value)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				ImmutableList<T>.Node result;
				if (index == this._left._count)
				{
					result = this.MutateKey(value);
				}
				else if (index < this._left._count)
				{
					ImmutableList<T>.Node left = this._left.ReplaceAt(index, value);
					result = this.MutateLeft(left);
				}
				else
				{
					ImmutableList<T>.Node right = this._right.ReplaceAt(index - this._left._count - 1, value);
					result = this.MutateRight(right);
				}
				return result;
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x000127F7 File Offset: 0x000109F7
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Reverse()
			{
				return this.Reverse(0, this.Count);
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x00012808 File Offset: 0x00010A08
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal unsafe ImmutableList<T>.Node Reverse(int index, int count)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "index", null);
				ImmutableList<T>.Node node = this;
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					T value = *node.ItemRef(i);
					T value2 = *node.ItemRef(num);
					node = node.ReplaceAt(num, value).ReplaceAt(i, value2);
					i++;
					num--;
				}
				return node;
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x00012897 File Offset: 0x00010A97
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Sort()
			{
				return this.Sort(Comparer<T>.Default);
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x000128A4 File Offset: 0x00010AA4
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Sort(Comparison<T> comparison)
			{
				Requires.NotNull<Comparison<T>>(comparison, "comparison");
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, comparison);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x000128E8 File Offset: 0x00010AE8
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Sort([Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				return this.Sort(0, this.Count, comparer);
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x000128F8 File Offset: 0x00010AF8
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			internal ImmutableList<T>.Node Sort(int index, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Argument(index + count <= this.Count);
				T[] array = new T[this.Count];
				this.CopyTo(array);
				Array.Sort<T>(array, index, count, comparer);
				return ImmutableList<T>.Node.NodeTreeFromList(array.AsOrderedCollection<T>(), 0, this.Count);
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x0001296C File Offset: 0x00010B6C
			internal int BinarySearch(int index, int count, T item, [Nullable(new byte[]
			{
				2,
				1
			})] IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				if (this.IsEmpty || count <= 0)
				{
					return ~index;
				}
				int count2 = this._left.Count;
				if (index + count <= count2)
				{
					return this._left.BinarySearch(index, count, item, comparer);
				}
				if (index > count2)
				{
					int num = this._right.BinarySearch(index - count2 - 1, count, item, comparer);
					int num2 = count2 + 1;
					if (num >= 0)
					{
						return num + num2;
					}
					return num - num2;
				}
				else
				{
					int num3 = comparer.Compare(item, this._key);
					if (num3 == 0)
					{
						return count2;
					}
					if (num3 > 0)
					{
						int num4 = count - (count2 - index) - 1;
						int num5 = (num4 < 0) ? -1 : this._right.BinarySearch(0, num4, item, comparer);
						int num6 = count2 + 1;
						if (num5 >= 0)
						{
							return num5 + num6;
						}
						return num5 - num6;
					}
					else
					{
						if (index == count2)
						{
							return ~index;
						}
						return this._left.BinarySearch(index, count, item, comparer);
					}
				}
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x00012A6C File Offset: 0x00010C6C
			internal int IndexOf(T item, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				return this.IndexOf(item, 0, this.Count, equalityComparer);
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x00012A7D File Offset: 0x00010C7D
			internal bool Contains(T item, IEqualityComparer<T> equalityComparer)
			{
				return ImmutableList<T>.Node.Contains(this, item, equalityComparer);
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x00012A88 File Offset: 0x00010C88
			internal int IndexOf(T item, int index, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				if (equalityComparer == null)
				{
					equalityComparer = EqualityComparer<T>.Default;
				}
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index++;
					}
				}
				return -1;
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x00012B48 File Offset: 0x00010D48
			internal int LastIndexOf(T item, int index, int count, [Nullable(new byte[]
			{
				2,
				1
			})] IEqualityComparer<T> equalityComparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && count <= this.Count, "count", null);
				Requires.Argument(index - count + 1 >= 0);
				if (equalityComparer == null)
				{
					equalityComparer = EqualityComparer<T>.Default;
				}
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, true))
				{
					while (enumerator.MoveNext())
					{
						if (equalityComparer.Equals(item, enumerator.Current))
						{
							return index;
						}
						index--;
					}
				}
				return -1;
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00012BF4 File Offset: 0x00010DF4
			internal void CopyTo(T[] array)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(array.Length >= this.Count, "array", null);
				int num = 0;
				foreach (T t in this)
				{
					array[num++] = t;
				}
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x00012C70 File Offset: 0x00010E70
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

			// Token: 0x06000745 RID: 1861 RVA: 0x00012CFC File Offset: 0x00010EFC
			internal void CopyTo(int index, T[] array, int arrayIndex, int count)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(index + count <= this.Count, "count", null);
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(arrayIndex + count <= array.Length, "arrayIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, index, count, false))
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						array[arrayIndex++] = t;
					}
				}
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x00012DC8 File Offset: 0x00010FC8
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

			// Token: 0x06000747 RID: 1863 RVA: 0x00012E5C File Offset: 0x0001105C
			internal ImmutableList<TOutput>.Node ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter)
			{
				ImmutableList<TOutput>.Node emptyNode = ImmutableList<TOutput>.Node.EmptyNode;
				if (this.IsEmpty)
				{
					return emptyNode;
				}
				return emptyNode.AddRange(this.Select(converter));
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x00012E88 File Offset: 0x00011088
			internal bool TrueForAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T obj in this)
				{
					if (!match(obj))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x00012EEC File Offset: 0x000110EC
			internal bool Exists(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T obj in this)
				{
					if (match(obj))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x00012F50 File Offset: 0x00011150
			[return: Nullable(2)]
			internal T Find(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				foreach (T t in this)
				{
					if (match(t))
					{
						return t;
					}
				}
				return default(T);
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x00012FBC File Offset: 0x000111BC
			internal ImmutableList<T> FindAll(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (this.IsEmpty)
				{
					return ImmutableList<T>.Empty;
				}
				List<T> list = null;
				foreach (T t in this)
				{
					if (match(t))
					{
						if (list == null)
						{
							list = new List<T>();
						}
						list.Add(t);
					}
				}
				if (list == null)
				{
					return ImmutableList<T>.Empty;
				}
				return ImmutableList.CreateRange<T>(list);
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x00013048 File Offset: 0x00011248
			internal int FindIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				return this.FindIndex(0, this._count, match);
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x00013063 File Offset: 0x00011263
			internal int FindIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0 && startIndex <= this.Count, "startIndex", null);
				return this.FindIndex(startIndex, this.Count - startIndex, match);
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x000130A0 File Offset: 0x000112A0
			internal int FindIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count >= 0, "count", null);
				Requires.Range(startIndex <= this.Count - count, "count", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, false))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x00013148 File Offset: 0x00011348
			[return: Nullable(2)]
			internal T FindLast(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, -1, -1, true))
				{
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return enumerator.Current;
						}
					}
				}
				return default(T);
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x000131BC File Offset: 0x000113BC
			internal int FindLastIndex(Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(this.Count - 1, this.Count, match);
				}
				return -1;
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x000131E8 File Offset: 0x000113E8
			internal int FindLastIndex(int startIndex, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(startIndex == 0 || startIndex < this.Count, "startIndex", null);
				if (!this.IsEmpty)
				{
					return this.FindLastIndex(startIndex, startIndex + 1, match);
				}
				return -1;
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x00013244 File Offset: 0x00011444
			internal int FindLastIndex(int startIndex, int count, Predicate<T> match)
			{
				Requires.NotNull<Predicate<T>>(match, "match");
				Requires.Range(startIndex >= 0, "startIndex", null);
				Requires.Range(count <= this.Count, "count", null);
				Requires.Range(startIndex - count + 1 >= 0, "startIndex", null);
				using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator(this, null, startIndex, count, true))
				{
					int num = startIndex;
					while (enumerator.MoveNext())
					{
						if (match(enumerator.Current))
						{
							return num;
						}
						num--;
					}
				}
				return -1;
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x000132F0 File Offset: 0x000114F0
			internal void Freeze()
			{
				if (!this._frozen)
				{
					this._left.Freeze();
					this._right.Freeze();
					this._frozen = true;
				}
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x00013317 File Offset: 0x00011517
			private ImmutableList<T>.Node RotateLeft()
			{
				return this._right.MutateLeft(this.MutateRight(this._right._left));
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x00013335 File Offset: 0x00011535
			private ImmutableList<T>.Node RotateRight()
			{
				return this._left.MutateRight(this.MutateLeft(this._left._right));
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x00013354 File Offset: 0x00011554
			private ImmutableList<T>.Node DoubleLeft()
			{
				ImmutableList<T>.Node right = this._right;
				ImmutableList<T>.Node left = right._left;
				return left.MutateBoth(this.MutateRight(left._left), right.MutateLeft(left._right));
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x00013390 File Offset: 0x00011590
			private ImmutableList<T>.Node DoubleRight()
			{
				ImmutableList<T>.Node left = this._left;
				ImmutableList<T>.Node right = left._right;
				return right.MutateBoth(left.MutateRight(right._left), this.MutateLeft(right._right));
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000758 RID: 1880 RVA: 0x000133C9 File Offset: 0x000115C9
			private int BalanceFactor
			{
				get
				{
					return (int)(this._right._height - this._left._height);
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000759 RID: 1881 RVA: 0x000133E2 File Offset: 0x000115E2
			private bool IsRightHeavy
			{
				get
				{
					return this.BalanceFactor >= 2;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x0600075A RID: 1882 RVA: 0x000133F0 File Offset: 0x000115F0
			private bool IsLeftHeavy
			{
				get
				{
					return this.BalanceFactor <= -2;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x0600075B RID: 1883 RVA: 0x000133FF File Offset: 0x000115FF
			private bool IsBalanced
			{
				get
				{
					return this.BalanceFactor + 1 <= 2;
				}
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x0001340F File Offset: 0x0001160F
			private ImmutableList<T>.Node Balance()
			{
				if (!this.IsLeftHeavy)
				{
					return this.BalanceRight();
				}
				return this.BalanceLeft();
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x00013426 File Offset: 0x00011626
			private ImmutableList<T>.Node BalanceLeft()
			{
				if (this._left.BalanceFactor <= 0)
				{
					return this.RotateRight();
				}
				return this.DoubleRight();
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x00013443 File Offset: 0x00011643
			private ImmutableList<T>.Node BalanceRight()
			{
				if (this._right.BalanceFactor >= 0)
				{
					return this.RotateLeft();
				}
				return this.DoubleLeft();
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x00013460 File Offset: 0x00011660
			private ImmutableList<T>.Node BalanceMany()
			{
				ImmutableList<T>.Node node = this;
				while (!node.IsBalanced)
				{
					if (node.IsRightHeavy)
					{
						node = node.BalanceRight();
						node.MutateLeft(node._left.BalanceMany());
					}
					else
					{
						node = node.BalanceLeft();
						node.MutateRight(node._right.BalanceMany());
					}
				}
				return node;
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x000134B8 File Offset: 0x000116B8
			private ImmutableList<T>.Node MutateBoth(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left, right, false);
				}
				this._left = left;
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(left, right);
				this._count = ImmutableList<T>.Node.ParentCount(left, right);
				return this;
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x0001351C File Offset: 0x0001171C
			private ImmutableList<T>.Node MutateLeft(ImmutableList<T>.Node left)
			{
				Requires.NotNull<ImmutableList<T>.Node>(left, "left");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, left, this._right, false);
				}
				this._left = left;
				this._height = ImmutableList<T>.Node.ParentHeight(left, this._right);
				this._count = ImmutableList<T>.Node.ParentCount(left, this._right);
				return this;
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x0001357C File Offset: 0x0001177C
			private ImmutableList<T>.Node MutateRight(ImmutableList<T>.Node right)
			{
				Requires.NotNull<ImmutableList<T>.Node>(right, "right");
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(this._key, this._left, right, false);
				}
				this._right = right;
				this._height = ImmutableList<T>.Node.ParentHeight(this._left, right);
				this._count = ImmutableList<T>.Node.ParentCount(this._left, right);
				return this;
			}

			// Token: 0x06000763 RID: 1891 RVA: 0x000135DC File Offset: 0x000117DC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static byte ParentHeight(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return checked(1 + Math.Max(left._height, right._height));
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x000135F2 File Offset: 0x000117F2
			private static int ParentCount(ImmutableList<T>.Node left, ImmutableList<T>.Node right)
			{
				return 1 + left._count + right._count;
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x00013603 File Offset: 0x00011803
			private ImmutableList<T>.Node MutateKey(T key)
			{
				if (this._frozen)
				{
					return new ImmutableList<T>.Node(key, this._left, this._right, false);
				}
				this._key = key;
				return this;
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x0001362C File Offset: 0x0001182C
			private static ImmutableList<T>.Node CreateRange(IEnumerable<T> keys)
			{
				ImmutableList<T> immutableList;
				if (ImmutableList<T>.TryCastToImmutableList(keys, out immutableList))
				{
					return immutableList._root;
				}
				IOrderedCollection<T> orderedCollection = keys.AsOrderedCollection<T>();
				return ImmutableList<T>.Node.NodeTreeFromList(orderedCollection, 0, orderedCollection.Count);
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x0001365E File Offset: 0x0001185E
			private static ImmutableList<T>.Node CreateLeaf(T key)
			{
				return new ImmutableList<T>.Node(key, ImmutableList<T>.Node.EmptyNode, ImmutableList<T>.Node.EmptyNode, false);
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x00013671 File Offset: 0x00011871
			private static bool Contains(ImmutableList<T>.Node node, T value, IEqualityComparer<T> equalityComparer)
			{
				return !node.IsEmpty && (equalityComparer.Equals(value, node._key) || ImmutableList<T>.Node.Contains(node._left, value, equalityComparer) || ImmutableList<T>.Node.Contains(node._right, value, equalityComparer));
			}

			// Token: 0x04000111 RID: 273
			[Nullable(new byte[]
			{
				1,
				0
			})]
			internal static readonly ImmutableList<T>.Node EmptyNode = new ImmutableList<T>.Node();

			// Token: 0x04000112 RID: 274
			private T _key;

			// Token: 0x04000113 RID: 275
			private bool _frozen;

			// Token: 0x04000114 RID: 276
			private byte _height;

			// Token: 0x04000115 RID: 277
			private int _count;

			// Token: 0x04000116 RID: 278
			private ImmutableList<T>.Node _left;

			// Token: 0x04000117 RID: 279
			private ImmutableList<T>.Node _right;
		}
	}
}
