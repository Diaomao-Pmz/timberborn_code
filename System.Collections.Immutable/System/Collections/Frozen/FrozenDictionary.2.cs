using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000059 RID: 89
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	[DebuggerDisplay("Count = {Count}")]
	public abstract class FrozenDictionary<TKey, [Nullable(2)] TValue> : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000AD00 File Offset: 0x00008F00
		private protected FrozenDictionary(IEqualityComparer<TKey> comparer)
		{
			this.Comparer = comparer;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000AD0F File Offset: 0x00008F0F
		public static FrozenDictionary<TKey, TValue> Empty { get; } = new EmptyFrozenDictionary<TKey, TValue>(EqualityComparer<TKey>.Default);

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000AD16 File Offset: 0x00008F16
		public IEqualityComparer<TKey> Comparer { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000AD1E File Offset: 0x00008F1E
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<TKey> Keys
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return ImmutableCollectionsMarshal.AsImmutableArray<TKey>(this.KeysCore);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600040C RID: 1036
		private protected abstract TKey[] KeysCore { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000AD2C File Offset: 0x00008F2C
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				ImmutableArray<TKey> keys = this.Keys;
				if (keys.Length <= 0)
				{
					return Array.Empty<TKey>();
				}
				return keys;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000AD5A File Offset: 0x00008F5A
		IEnumerable<TKey> IReadOnlyDictionary<!0, !1>.Keys
		{
			get
			{
				return ((IDictionary<!0, !1>)this).Keys;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000AD62 File Offset: 0x00008F62
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000AD6F File Offset: 0x00008F6F
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public ImmutableArray<TValue> Values
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return ImmutableCollectionsMarshal.AsImmutableArray<TValue>(this.ValuesCore);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000411 RID: 1041
		private protected abstract TValue[] ValuesCore { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000AD7C File Offset: 0x00008F7C
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				ImmutableArray<TValue> values = this.Values;
				if (values.Length <= 0)
				{
					return Array.Empty<TValue>();
				}
				return values;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000ADAA File Offset: 0x00008FAA
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000ADB7 File Offset: 0x00008FB7
		IEnumerable<TValue> IReadOnlyDictionary<!0, !1>.Values
		{
			get
			{
				return ((IDictionary<!0, !1>)this).Values;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000ADBF File Offset: 0x00008FBF
		public int Count
		{
			get
			{
				return this.CountCore;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000416 RID: 1046
		private protected abstract int CountCore { get; }

		// Token: 0x06000417 RID: 1047 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		public void CopyTo([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue>[] destination, int destinationIndex)
		{
			ThrowHelper.ThrowIfNull(destination, "destination");
			this.CopyTo(MemoryExtensions.AsSpan<KeyValuePair<TKey, TValue>>(destination, destinationIndex));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		public unsafe void CopyTo([Nullable(new byte[]
		{
			0,
			0,
			1,
			1
		})] Span<KeyValuePair<TKey, TValue>> destination)
		{
			if (destination.Length < this.Count)
			{
				ThrowHelper.ThrowIfDestinationTooSmall();
			}
			TKey[] keysCore = this.KeysCore;
			TValue[] valuesCore = this.ValuesCore;
			for (int i = 0; i < keysCore.Length; i++)
			{
				*destination[i] = new KeyValuePair<TKey, TValue>(keysCore[i], valuesCore[i]);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000AE44 File Offset: 0x00009044
		void ICollection.CopyTo(Array array, int index)
		{
			ThrowHelper.ThrowIfNull(array, "array");
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Arg_RankMultiDimNotSupported, "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException(SR.Arg_NonZeroLowerBound, "array");
			}
			if (index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(SR.Arg_ArrayPlusOffTooSmall, "array");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				using (FrozenDictionary<TKey, TValue>.Enumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array2[index++] = new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (FrozenDictionary<TKey, TValue>.Enumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair2 = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair2.Key, keyValuePair2.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				throw new ArgumentException(SR.Argument_IncompatibleArrayType, "array");
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair3 in this)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair3.Key, keyValuePair3.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException(SR.Argument_IncompatibleArrayType, "array");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000B030 File Offset: 0x00009230
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000B033 File Offset: 0x00009233
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000B036 File Offset: 0x00009236
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000B039 File Offset: 0x00009239
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000B03C File Offset: 0x0000923C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000C4 RID: 196
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			get
			{
				ThrowHelper.ThrowIfNull(key, "key");
				if (key is TKey)
				{
					TKey key2 = (TKey)((object)key);
					TValue tvalue;
					if (this.TryGetValue(key2, out tvalue))
					{
						return tvalue;
					}
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000B081 File Offset: 0x00009281
		public ref readonly TValue GetValueRefOrNullRef(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException("key");
			}
			return this.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x06000422 RID: 1058
		private protected abstract ref readonly TValue GetValueRefOrNullRefCore(TKey key);

		// Token: 0x06000423 RID: 1059 RVA: 0x0000B09C File Offset: 0x0000929C
		private protected virtual ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x170000C5 RID: 197
		public TValue this[TKey key]
		{
			get
			{
				ref readonly TValue valueRefOrNullRef = ref this.GetValueRefOrNullRef(key);
				if (Unsafe.IsNullRef<TValue>(Unsafe.AsRef<TValue>(valueRefOrNullRef)))
				{
					ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
				}
				return ref valueRefOrNullRef;
			}
		}

		// Token: 0x170000C6 RID: 198
		unsafe TValue IDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return *this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000C7 RID: 199
		unsafe TValue IReadOnlyDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return *this[key];
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000B0E2 File Offset: 0x000092E2
		public bool ContainsKey(TKey key)
		{
			return !Unsafe.IsNullRef<TValue>(Unsafe.AsRef<TValue>(this.GetValueRefOrNullRef(key)));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000B0F8 File Offset: 0x000092F8
		bool IDictionary.Contains(object key)
		{
			ThrowHelper.ThrowIfNull(key, "key");
			if (key is TKey)
			{
				TKey key2 = (TKey)((object)key);
				return this.ContainsKey(key2);
			}
			return false;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000B128 File Offset: 0x00009328
		bool ICollection<KeyValuePair<!0, !1>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			TValue x;
			return this.TryGetValue(item.Key, out x) && EqualityComparer<TValue>.Default.Equals(x, item.Value);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000B15C File Offset: 0x0000935C
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ref readonly TValue valueRefOrNullRef = ref this.GetValueRefOrNullRef(key);
			if (!Unsafe.IsNullRef<TValue>(Unsafe.AsRef<TValue>(valueRefOrNullRef)))
			{
				value = valueRefOrNullRef;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000B194 File Offset: 0x00009394
		[NullableContext(0)]
		public FrozenDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this.GetEnumeratorCore();
		}

		// Token: 0x0600042D RID: 1069
		[NullableContext(0)]
		private protected abstract FrozenDictionary<TKey, TValue>.Enumerator GetEnumeratorCore();

		// Token: 0x0600042E RID: 1070 RVA: 0x0000B19C File Offset: 0x0000939C
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			if (this.Count != 0)
			{
				return this.GetEnumerator();
			}
			return ((IEnumerable<KeyValuePair<!0, !1>>)Array.Empty<KeyValuePair<TKey, TValue>>()).GetEnumerator();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000B1CC File Offset: 0x000093CC
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.Count != 0)
			{
				return this.GetEnumerator();
			}
			return Array.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000B1F9 File Offset: 0x000093F9
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000B20B File Offset: 0x0000940B
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000B212 File Offset: 0x00009412
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000B219 File Offset: 0x00009419
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000B220 File Offset: 0x00009420
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000B227 File Offset: 0x00009427
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000B22E File Offset: 0x0000942E
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000B235 File Offset: 0x00009435
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000B23C File Offset: 0x0000943C
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x020000C2 RID: 194
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600085C RID: 2140 RVA: 0x0001618A File Offset: 0x0001438A
			[NullableContext(1)]
			internal Enumerator(TKey[] keys, TValue[] values)
			{
				this._keys = keys;
				this._values = values;
				this._index = -1;
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x000161A1 File Offset: 0x000143A1
			public bool MoveNext()
			{
				this._index++;
				if (this._index < this._keys.Length)
				{
					return true;
				}
				this._index = this._keys.Length;
				return false;
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x0600085E RID: 2142 RVA: 0x000161D2 File Offset: 0x000143D2
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public readonly KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				get
				{
					if (this._index >= this._keys.Length)
					{
						ThrowHelper.ThrowInvalidOperationException();
					}
					return new KeyValuePair<TKey, TValue>(this._keys[this._index], this._values[this._index]);
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x0600085F RID: 2143 RVA: 0x00016210 File Offset: 0x00014410
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x0001621D File Offset: 0x0001441D
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x00016226 File Offset: 0x00014426
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000158 RID: 344
			private readonly TKey[] _keys;

			// Token: 0x04000159 RID: 345
			private readonly TValue[] _values;

			// Token: 0x0400015A RID: 346
			private int _index;
		}
	}
}
