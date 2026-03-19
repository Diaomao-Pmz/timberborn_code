using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000068 RID: 104
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class Int32FrozenDictionary<[Nullable(2)] TValue> : FrozenDictionary<int, TValue>
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000CA60 File Offset: 0x0000AC60
		internal unsafe Int32FrozenDictionary(Dictionary<int, TValue> source) : base(EqualityComparer<int>.Default)
		{
			KeyValuePair<int, TValue>[] array = new KeyValuePair<int, TValue>[source.Count];
			((ICollection<KeyValuePair<int, TValue>>)source).CopyTo(array, 0);
			this._values = new TValue[array.Length];
			int[] array2 = ArrayPool<int>.Shared.Rent(array.Length);
			Span<int> hashCodes = MemoryExtensions.AsSpan<int>(array2, 0, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				*hashCodes[i] = array[i].Key;
			}
			this._hashTable = FrozenHashTable.Create(hashCodes, true);
			for (int j = 0; j < hashCodes.Length; j++)
			{
				int num = *hashCodes[j];
				this._values[num] = array[j].Value;
			}
			ArrayPool<int>.Shared.Return(array2, false);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000CB2B File Offset: 0x0000AD2B
		private protected override int[] KeysCore
		{
			get
			{
				return this._hashTable.HashCodes;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000CB38 File Offset: 0x0000AD38
		private protected override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000CB40 File Offset: 0x0000AD40
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenDictionary<int, TValue>.Enumerator GetEnumeratorCore()
		{
			return new FrozenDictionary<int, TValue>.Enumerator(this._hashTable.HashCodes, this._values);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000CB58 File Offset: 0x0000AD58
		private protected override int CountCore
		{
			get
			{
				return this._hashTable.Count;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000CB68 File Offset: 0x0000AD68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override ref readonly TValue GetValueRefOrNullRefCore(int key)
		{
			int i;
			int num;
			this._hashTable.FindMatchingEntries(key, out i, out num);
			int[] hashCodes = this._hashTable.HashCodes;
			while (i <= num)
			{
				if (key == hashCodes[i])
				{
					return ref this._values[i];
				}
				i++;
			}
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x04000073 RID: 115
		private readonly FrozenHashTable _hashTable;

		// Token: 0x04000074 RID: 116
		private readonly TValue[] _values;
	}
}
