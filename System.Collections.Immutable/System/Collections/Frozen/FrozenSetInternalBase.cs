using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200005D RID: 93
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal abstract class FrozenSetInternalBase<[Nullable(2)] T, [Nullable(0)] TThisWrapper> : FrozenSet<T> where TThisWrapper : struct, FrozenSetInternalBase<T, TThisWrapper>.IGenericSpecializedWrapper
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0000BD76 File Offset: 0x00009F76
		protected FrozenSetInternalBase(IEqualityComparer<T> comparer) : base(comparer)
		{
			this._thisSet = default(TThisWrapper);
			this._thisSet.Store(this);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		private protected override bool IsProperSubsetOfCore(IEnumerable<T> other)
		{
			ICollection<T> collection = other as ICollection<!0>;
			TThisWrapper thisSet;
			if (collection != null)
			{
				int count = collection.Count;
				if (count == 0)
				{
					return false;
				}
				IReadOnlySet<T> readOnlySet = other as IReadOnlySet<T>;
				if (readOnlySet != null && this.ComparersAreCompatible(readOnlySet))
				{
					thisSet = this._thisSet;
					return thisSet.Count < count && this.IsSubsetOfSetWithCompatibleComparer(readOnlySet);
				}
			}
			int num;
			int num2;
			this.CheckUniqueAndUnfoundElements(other, false).Deconstruct(out num, out num2);
			int num3 = num;
			int num4 = num2;
			thisSet = this._thisSet;
			return num3 == thisSet.Count && num4 > 0;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000BE2C File Offset: 0x0000A02C
		private protected override bool IsProperSupersetOfCore(IEnumerable<T> other)
		{
			ICollection<T> collection = other as ICollection<!0>;
			TThisWrapper thisSet;
			if (collection != null)
			{
				int count = collection.Count;
				if (count == 0)
				{
					return true;
				}
				IReadOnlySet<T> readOnlySet = other as IReadOnlySet<T>;
				if (readOnlySet != null && this.ComparersAreCompatible(readOnlySet))
				{
					thisSet = this._thisSet;
					return thisSet.Count > count && this.ContainsAllElements(readOnlySet);
				}
			}
			int num;
			int num2;
			this.CheckUniqueAndUnfoundElements(other, true).Deconstruct(out num, out num2);
			int num3 = num;
			int num4 = num2;
			thisSet = this._thisSet;
			return num3 < thisSet.Count && num4 == 0;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		private protected override bool IsSubsetOfCore(IEnumerable<T> other)
		{
			IReadOnlySet<T> readOnlySet = other as IReadOnlySet<T>;
			TThisWrapper thisSet;
			if (readOnlySet != null && this.ComparersAreCompatible(readOnlySet))
			{
				thisSet = this._thisSet;
				return thisSet.Count <= readOnlySet.Count && this.IsSubsetOfSetWithCompatibleComparer(readOnlySet);
			}
			int num;
			int num2;
			this.CheckUniqueAndUnfoundElements(other, false).Deconstruct(out num, out num2);
			int num3 = num;
			int num4 = num2;
			thisSet = this._thisSet;
			return num3 == thisSet.Count && num4 >= 0;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000BF34 File Offset: 0x0000A134
		private protected override bool IsSupersetOfCore(IEnumerable<T> other)
		{
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				int count = collection.Count;
				if (count == 0)
				{
					return true;
				}
				IReadOnlySet<T> readOnlySet = other as IReadOnlySet<T>;
				if (readOnlySet != null)
				{
					int num = count;
					TThisWrapper thisSet = this._thisSet;
					if (num > thisSet.Count && this.ComparersAreCompatible(readOnlySet))
					{
						return false;
					}
				}
			}
			return this.ContainsAllElements(other);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000BF8C File Offset: 0x0000A18C
		private protected override bool OverlapsCore(IEnumerable<T> other)
		{
			foreach (T item in other)
			{
				TThisWrapper thisSet = this._thisSet;
				if (thisSet.FindItemIndex(item) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		private protected override bool SetEqualsCore(IEnumerable<T> other)
		{
			IReadOnlySet<T> readOnlySet = other as IReadOnlySet<T>;
			TThisWrapper thisSet;
			if (readOnlySet != null && this.ComparersAreCompatible(readOnlySet))
			{
				thisSet = this._thisSet;
				return thisSet.Count == readOnlySet.Count && this.ContainsAllElements(readOnlySet);
			}
			int num;
			int num2;
			this.CheckUniqueAndUnfoundElements(other, true).Deconstruct(out num, out num2);
			int num3 = num;
			int num4 = num2;
			thisSet = this._thisSet;
			return num3 == thisSet.Count && num4 == 0;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000C064 File Offset: 0x0000A264
		private bool ComparersAreCompatible(IReadOnlySet<T> other)
		{
			HashSet<T> hashSet = other as HashSet<T>;
			bool result;
			if (hashSet == null)
			{
				SortedSet<T> sortedSet = other as SortedSet<T>;
				if (sortedSet == null)
				{
					ImmutableHashSet<T> immutableHashSet = other as ImmutableHashSet<T>;
					if (immutableHashSet == null)
					{
						ImmutableSortedSet<T> immutableSortedSet = other as ImmutableSortedSet<T>;
						if (immutableSortedSet == null)
						{
							FrozenSet<T> frozenSet = other as FrozenSet<T>;
							if (frozenSet == null)
							{
								result = false;
							}
							else
							{
								TThisWrapper thisSet = this._thisSet;
								result = thisSet.Comparer.Equals(frozenSet.Comparer);
							}
						}
						else
						{
							TThisWrapper thisSet = this._thisSet;
							result = thisSet.Comparer.Equals(immutableSortedSet.KeyComparer);
						}
					}
					else
					{
						TThisWrapper thisSet = this._thisSet;
						result = thisSet.Comparer.Equals(immutableHashSet.KeyComparer);
					}
				}
				else
				{
					TThisWrapper thisSet = this._thisSet;
					result = thisSet.Comparer.Equals(sortedSet.Comparer);
				}
			}
			else
			{
				TThisWrapper thisSet = this._thisSet;
				result = thisSet.Comparer.Equals(hashSet.Comparer);
			}
			return result;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000C170 File Offset: 0x0000A370
		private unsafe KeyValuePair<int, int> CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			TThisWrapper thisSet = this._thisSet;
			int num = thisSet.Count / 32 + 1;
			int[] array = null;
			Span<int> span;
			if (num <= 128)
			{
				span = new Span<int>(stackalloc byte[(UIntPtr)512], 128);
			}
			else
			{
				span = (array = ArrayPool<int>.Shared.Rent(num));
			}
			Span<int> span2 = span;
			span2 = span2.Slice(0, num);
			span2.Clear();
			int num2 = 0;
			int num3 = 0;
			foreach (T item in other)
			{
				thisSet = this._thisSet;
				int num4 = thisSet.FindItemIndex(item);
				if (num4 >= 0)
				{
					if ((*span2[num4 / 32] & 1 << num4) == 0)
					{
						*span2[num4 / 32] |= 1 << num4;
						num3++;
					}
				}
				else
				{
					num2++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			if (array != null)
			{
				ArrayPool<int>.Shared.Return(array, false);
			}
			return new KeyValuePair<int, int>(num3, num2);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000C294 File Offset: 0x0000A494
		private bool ContainsAllElements(IEnumerable<T> other)
		{
			foreach (T item in other)
			{
				TThisWrapper thisSet = this._thisSet;
				if (thisSet.FindItemIndex(item) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		private bool IsSubsetOfSetWithCompatibleComparer(IReadOnlySet<T> other)
		{
			TThisWrapper thisSet = this._thisSet;
			foreach (T item in thisSet)
			{
				if (!other.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000062 RID: 98
		private readonly TThisWrapper _thisSet;

		// Token: 0x020000C6 RID: 198
		internal interface IGenericSpecializedWrapper
		{
			// Token: 0x06000869 RID: 2153
			void Store(FrozenSet<T> @this);

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x0600086A RID: 2154
			int Count { get; }

			// Token: 0x0600086B RID: 2155
			int FindItemIndex(T item);

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x0600086C RID: 2156
			IEqualityComparer<T> Comparer { get; }

			// Token: 0x0600086D RID: 2157
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			FrozenSet<T>.Enumerator GetEnumerator();
		}
	}
}
