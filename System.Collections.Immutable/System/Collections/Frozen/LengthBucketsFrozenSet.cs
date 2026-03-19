using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200006E RID: 110
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class LengthBucketsFrozenSet : FrozenSetInternalBase<string, LengthBucketsFrozenSet.GSW>
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000D5BB File Offset: 0x0000B7BB
		private LengthBucketsFrozenSet(string[] items, int[] lengthBuckets, int minLength, IEqualityComparer<string> comparer) : base(comparer)
		{
			this._items = items;
			this._lengthBuckets = lengthBuckets;
			this._minLength = minLength;
			this._ignoreCase = (comparer == StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		[return: Nullable(2)]
		internal static LengthBucketsFrozenSet CreateLengthBucketsFrozenSetIfAppropriate(string[] items, IEqualityComparer<string> comparer, int minLength, int maxLength)
		{
			int[] array = LengthBuckets.CreateLengthBucketsArrayIfAppropriate(items, comparer, minLength, maxLength);
			if (array == null)
			{
				return null;
			}
			return new LengthBucketsFrozenSet(items, array, minLength, comparer);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000D611 File Offset: 0x0000B811
		private protected override string[] ItemsCore
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000D619 File Offset: 0x0000B819
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenSet<string>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<string>.Enumerator(this._items);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000D626 File Offset: 0x0000B826
		private protected override int CountCore
		{
			get
			{
				return this._items.Length;
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000D630 File Offset: 0x0000B830
		[NullableContext(2)]
		private protected override int FindItemIndex(string item)
		{
			if (item != null)
			{
				int i = (item.Length - this._minLength) * 5;
				int num = i + 5;
				int[] lengthBuckets = this._lengthBuckets;
				if (i >= 0 && num <= lengthBuckets.Length)
				{
					string[] items = this._items;
					if (!this._ignoreCase)
					{
						while (i < num)
						{
							int num2 = lengthBuckets[i];
							if (num2 >= items.Length)
							{
								break;
							}
							if (item == items[num2])
							{
								return num2;
							}
							i++;
						}
					}
					else
					{
						while (i < num)
						{
							int num3 = lengthBuckets[i];
							if (num3 >= items.Length)
							{
								break;
							}
							if (StringComparer.OrdinalIgnoreCase.Equals(item, items[num3]))
							{
								return num3;
							}
							i++;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x0400007F RID: 127
		private readonly int[] _lengthBuckets;

		// Token: 0x04000080 RID: 128
		private readonly int _minLength;

		// Token: 0x04000081 RID: 129
		private readonly string[] _items;

		// Token: 0x04000082 RID: 130
		private readonly bool _ignoreCase;

		// Token: 0x020000D3 RID: 211
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<string, LengthBucketsFrozenSet.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x060008A5 RID: 2213 RVA: 0x0001669C File Offset: 0x0001489C
			public void Store(FrozenSet<string> set)
			{
				this._set = (LengthBucketsFrozenSet)set;
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000166AA File Offset: 0x000148AA
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x060008A7 RID: 2215 RVA: 0x000166B7 File Offset: 0x000148B7
			public IEqualityComparer<string> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x000166C4 File Offset: 0x000148C4
			public int FindItemIndex(string item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x000166D2 File Offset: 0x000148D2
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<string>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000175 RID: 373
			private LengthBucketsFrozenSet _set;
		}
	}
}
