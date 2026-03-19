using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(FrozenSet), "Create")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	[DebuggerDisplay("Count = {Count}")]
	public abstract class FrozenSet<[Nullable(2)] T> : ISet<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable, IReadOnlyCollection<!0>, ICollection
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000BB4F File Offset: 0x00009D4F
		private protected FrozenSet(IEqualityComparer<T> comparer)
		{
			this.Comparer = comparer;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000BB5E File Offset: 0x00009D5E
		public static FrozenSet<T> Empty { get; } = new EmptyFrozenSet<T>(EqualityComparer<T>.Default);

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000BB65 File Offset: 0x00009D65
		public IEqualityComparer<T> Comparer { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000BB6D File Offset: 0x00009D6D
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<T> Items
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return ImmutableCollectionsMarshal.AsImmutableArray<T>(this.ItemsCore);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600044A RID: 1098
		private protected abstract T[] ItemsCore { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000BB7A File Offset: 0x00009D7A
		public int Count
		{
			get
			{
				return this.CountCore;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600044C RID: 1100
		private protected abstract int CountCore { get; }

		// Token: 0x0600044D RID: 1101 RVA: 0x0000BB82 File Offset: 0x00009D82
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ThrowHelper.ThrowIfNull(destination, "destination");
			this.CopyTo(MemoryExtensions.AsSpan<T>(destination, destinationIndex));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000BB9C File Offset: 0x00009D9C
		public void CopyTo([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> destination)
		{
			this.Items.AsSpan().CopyTo(destination);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
			}
			T[] itemsCore = this.ItemsCore;
			Array.Copy(itemsCore, 0, array, index, itemsCore.Length);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000BBFC File Offset: 0x00009DFC
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000BBFF File Offset: 0x00009DFF
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000BC02 File Offset: 0x00009E02
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000BC05 File Offset: 0x00009E05
		public bool Contains(T item)
		{
			return this.FindItemIndex(item) >= 0;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000BC14 File Offset: 0x00009E14
		public bool TryGetValue(T equalValue, [MaybeNullWhen(false)] out T actualValue)
		{
			int num = this.FindItemIndex(equalValue);
			if (num >= 0)
			{
				actualValue = this.Items[num];
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x06000455 RID: 1109
		private protected abstract int FindItemIndex(T item);

		// Token: 0x06000456 RID: 1110 RVA: 0x0000BC4C File Offset: 0x00009E4C
		private protected virtual int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return -1;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000BC4F File Offset: 0x00009E4F
		[NullableContext(0)]
		public FrozenSet<T>.Enumerator GetEnumerator()
		{
			return this.GetEnumeratorCore();
		}

		// Token: 0x06000458 RID: 1112
		[NullableContext(0)]
		private protected abstract FrozenSet<T>.Enumerator GetEnumeratorCore();

		// Token: 0x06000459 RID: 1113 RVA: 0x0000BC58 File Offset: 0x00009E58
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (this.Count != 0)
			{
				return this.GetEnumerator();
			}
			return Array.Empty<T>().GetEnumerator();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000BC88 File Offset: 0x00009E88
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.Count != 0)
			{
				return this.GetEnumerator();
			}
			return Array.Empty<T>().GetEnumerator();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000BCB5 File Offset: 0x00009EB5
		bool ISet<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000BCBC File Offset: 0x00009EBC
		void ISet<!0>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000BCC3 File Offset: 0x00009EC3
		void ISet<!0>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000BCCA File Offset: 0x00009ECA
		void ISet<!0>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000BCD1 File Offset: 0x00009ED1
		void ISet<!0>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000BCDF File Offset: 0x00009EDF
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000BCE6 File Offset: 0x00009EE6
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000BCED File Offset: 0x00009EED
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			ThrowHelper.ThrowIfNull(other, "other");
			return this.IsProperSubsetOfCore(other);
		}

		// Token: 0x06000464 RID: 1124
		private protected abstract bool IsProperSubsetOfCore(IEnumerable<T> other);

		// Token: 0x06000465 RID: 1125 RVA: 0x0000BD01 File Offset: 0x00009F01
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			ThrowHelper.ThrowIfNull(other, "other");
			return this.IsProperSupersetOfCore(other);
		}

		// Token: 0x06000466 RID: 1126
		private protected abstract bool IsProperSupersetOfCore(IEnumerable<T> other);

		// Token: 0x06000467 RID: 1127 RVA: 0x0000BD15 File Offset: 0x00009F15
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			ThrowHelper.ThrowIfNull(other, "other");
			return this.IsSubsetOfCore(other);
		}

		// Token: 0x06000468 RID: 1128
		private protected abstract bool IsSubsetOfCore(IEnumerable<T> other);

		// Token: 0x06000469 RID: 1129 RVA: 0x0000BD29 File Offset: 0x00009F29
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			ThrowHelper.ThrowIfNull(other, "other");
			return this.IsSupersetOfCore(other);
		}

		// Token: 0x0600046A RID: 1130
		private protected abstract bool IsSupersetOfCore(IEnumerable<T> other);

		// Token: 0x0600046B RID: 1131 RVA: 0x0000BD3D File Offset: 0x00009F3D
		public bool Overlaps(IEnumerable<T> other)
		{
			ThrowHelper.ThrowIfNull(other, "other");
			return this.OverlapsCore(other);
		}

		// Token: 0x0600046C RID: 1132
		private protected abstract bool OverlapsCore(IEnumerable<T> other);

		// Token: 0x0600046D RID: 1133 RVA: 0x0000BD51 File Offset: 0x00009F51
		public bool SetEquals(IEnumerable<T> other)
		{
			ThrowHelper.ThrowIfNull(other, "other");
			return this.SetEqualsCore(other);
		}

		// Token: 0x0600046E RID: 1134
		private protected abstract bool SetEqualsCore(IEnumerable<T> other);

		// Token: 0x020000C5 RID: 197
		[Nullable(0)]
		public struct Enumerator : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x06000863 RID: 2147 RVA: 0x0001623C File Offset: 0x0001443C
			internal Enumerator(T[] entries)
			{
				this._entries = entries;
				this._index = -1;
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x0001624C File Offset: 0x0001444C
			public bool MoveNext()
			{
				this._index++;
				if (this._index < this._entries.Length)
				{
					return true;
				}
				this._index = this._entries.Length;
				return false;
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001627D File Offset: 0x0001447D
			public readonly T Current
			{
				get
				{
					if (this._index >= this._entries.Length)
					{
						ThrowHelper.ThrowInvalidOperationException();
					}
					return this._entries[this._index];
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000866 RID: 2150 RVA: 0x000162A5 File Offset: 0x000144A5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x000162B2 File Offset: 0x000144B2
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x000162BB File Offset: 0x000144BB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000161 RID: 353
			private readonly T[] _entries;

			// Token: 0x04000162 RID: 354
			private int _index;
		}
	}
}
