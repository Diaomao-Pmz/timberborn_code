using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200006F RID: 111
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal abstract class OrdinalStringFrozenDictionary<[Nullable(2)] TValue> : FrozenDictionary<string, TValue>
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		internal unsafe OrdinalStringFrozenDictionary(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex = -1, int hashCount = -1) : base(comparer)
		{
			this._keys = new string[keys.Length];
			this._values = new TValue[values.Length];
			this._minimumLength = minimumLength;
			this._maximumLengthDiff = maximumLengthDiff;
			this.HashIndex = hashIndex;
			this.HashCount = hashCount;
			int[] array = ArrayPool<int>.Shared.Rent(keys.Length);
			Span<int> hashCodes = MemoryExtensions.AsSpan<int>(array, 0, keys.Length);
			for (int i = 0; i < keys.Length; i++)
			{
				*hashCodes[i] = this.GetHashCode(keys[i]);
			}
			this._hashTable = FrozenHashTable.Create(hashCodes, false);
			for (int j = 0; j < hashCodes.Length; j++)
			{
				int num = *hashCodes[j];
				this._keys[num] = keys[j];
				this._values[num] = values[j];
			}
			ArrayPool<int>.Shared.Return(array, false);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000D7A7 File Offset: 0x0000B9A7
		private protected int HashIndex { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000D7AF File Offset: 0x0000B9AF
		private protected int HashCount { get; }

		// Token: 0x060004D0 RID: 1232
		[NullableContext(2)]
		private protected abstract bool Equals(string x, string y);

		// Token: 0x060004D1 RID: 1233
		[NullableContext(0)]
		private protected abstract bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y);

		// Token: 0x060004D2 RID: 1234
		private protected abstract int GetHashCode(string s);

		// Token: 0x060004D3 RID: 1235
		[NullableContext(0)]
		private protected abstract int GetHashCode(ReadOnlySpan<char> s);

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
		private protected virtual bool CheckLengthQuick(uint length)
		{
			return true;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000D7BA File Offset: 0x0000B9BA
		private protected override string[] KeysCore
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000D7C2 File Offset: 0x0000B9C2
		private protected override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000D7CA File Offset: 0x0000B9CA
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000D7DD File Offset: 0x0000B9DD
		private protected override int CountCore
		{
			get
			{
				return this._hashTable.Count;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			if (key.Length - this._minimumLength <= this._maximumLengthDiff && this.CheckLengthQuick((uint)key.Length))
			{
				int hashCode = this.GetHashCode(key);
				int i;
				int num;
				this._hashTable.FindMatchingEntries(hashCode, out i, out num);
				while (i <= num)
				{
					if (hashCode == this._hashTable.HashCodes[i] && this.Equals(key, this._keys[i]))
					{
						return ref this._values[i];
					}
					i++;
				}
			}
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x04000083 RID: 131
		private readonly FrozenHashTable _hashTable;

		// Token: 0x04000084 RID: 132
		private readonly string[] _keys;

		// Token: 0x04000085 RID: 133
		private readonly TValue[] _values;

		// Token: 0x04000086 RID: 134
		private readonly int _minimumLength;

		// Token: 0x04000087 RID: 135
		private readonly int _maximumLengthDiff;
	}
}
