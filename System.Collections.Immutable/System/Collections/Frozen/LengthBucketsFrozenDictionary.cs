using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200006D RID: 109
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class LengthBucketsFrozenDictionary<[Nullable(2)] TValue> : FrozenDictionary<string, TValue>
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D480 File Offset: 0x0000B680
		private LengthBucketsFrozenDictionary(string[] keys, TValue[] values, int[] lengthBuckets, int minLength, IEqualityComparer<string> comparer) : base(comparer)
		{
			this._keys = keys;
			this._values = values;
			this._lengthBuckets = lengthBuckets;
			this._minLength = minLength;
			this._ignoreCase = (comparer == StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		internal static LengthBucketsFrozenDictionary<TValue> CreateLengthBucketsFrozenDictionaryIfAppropriate(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minLength, int maxLength)
		{
			int[] array = LengthBuckets.CreateLengthBucketsArrayIfAppropriate(keys, comparer, minLength, maxLength);
			if (array == null)
			{
				return null;
			}
			return new LengthBucketsFrozenDictionary<TValue>(keys, values, array, minLength, comparer);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000D4DF File Offset: 0x0000B6DF
		private protected override string[] KeysCore
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000D4E7 File Offset: 0x0000B6E7
		private protected override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000D4EF File Offset: 0x0000B6EF
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		private protected override FrozenDictionary<string, TValue>.Enumerator GetEnumeratorCore()
		{
			return new FrozenDictionary<string, TValue>.Enumerator(this._keys, this._values);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000D502 File Offset: 0x0000B702
		private protected override int CountCore
		{
			get
			{
				return this._keys.Length;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000D50C File Offset: 0x0000B70C
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			int i = (key.Length - this._minLength) * 5;
			int num = i + 5;
			int[] lengthBuckets = this._lengthBuckets;
			if (i >= 0 && num <= lengthBuckets.Length)
			{
				string[] keys = this._keys;
				TValue[] values = this._values;
				if (!this._ignoreCase)
				{
					while (i < num)
					{
						int num2 = lengthBuckets[i];
						if (num2 >= keys.Length)
						{
							break;
						}
						if (key == keys[num2])
						{
							return ref values[num2];
						}
						i++;
					}
				}
				else
				{
					while (i < num)
					{
						int num3 = lengthBuckets[i];
						if (num3 >= keys.Length)
						{
							break;
						}
						if (StringComparer.OrdinalIgnoreCase.Equals(key, keys[num3]))
						{
							return ref values[num3];
						}
						i++;
					}
				}
			}
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x0400007A RID: 122
		private readonly int[] _lengthBuckets;

		// Token: 0x0400007B RID: 123
		private readonly int _minLength;

		// Token: 0x0400007C RID: 124
		private readonly string[] _keys;

		// Token: 0x0400007D RID: 125
		private readonly TValue[] _values;

		// Token: 0x0400007E RID: 126
		private readonly bool _ignoreCase;
	}
}
