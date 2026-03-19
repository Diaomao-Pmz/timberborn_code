using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ImmutableDictionaryDebuggerProxy<, >))]
	public sealed class ImmutableDictionary<TKey, [Nullable(2)] TValue> : IImmutableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IImmutableDictionaryInternal<TKey, TValue>, IHashKeyCollection<TKey>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00005E05 File Offset: 0x00004005
		private ImmutableDictionary(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count) : this(Requires.NotNullPassthrough<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers"))
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			root.Freeze(ImmutableDictionary<TKey, TValue>.s_FreezeBucketAction);
			this._root = root;
			this._count = count;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005E3C File Offset: 0x0000403C
		private ImmutableDictionary(ImmutableDictionary<TKey, TValue>.Comparers comparers = null)
		{
			this._comparers = (comparers ?? ImmutableDictionary<TKey, TValue>.Comparers.Get(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default));
			this._root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005E69 File Offset: 0x00004069
		public ImmutableDictionary<TKey, TValue> Clear()
		{
			if (!this.IsEmpty)
			{
				return ImmutableDictionary<TKey, TValue>.EmptyWithComparers(this._comparers);
			}
			return this;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00005E80 File Offset: 0x00004080
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005E88 File Offset: 0x00004088
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005E93 File Offset: 0x00004093
		public IEqualityComparer<TKey> KeyComparer
		{
			get
			{
				return this._comparers.KeyComparer;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005EA0 File Offset: 0x000040A0
		public IEqualityComparer<TValue> ValueComparer
		{
			get
			{
				return this._comparers.ValueComparer;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005EAD File Offset: 0x000040AD
		public IEnumerable<TKey> Keys
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Key;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005EBD File Offset: 0x000040BD
		public IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair in this._root)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair2 in keyValuePair.Value)
					{
						yield return keyValuePair2.Value;
					}
					ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator enumerator2 = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				}
				SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator enumerator = default(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005ECD File Offset: 0x000040CD
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005ED5 File Offset: 0x000040D5
		ICollection<TKey> IDictionary<!0, !1>.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00005EDD File Offset: 0x000040DD
		ICollection<TValue> IDictionary<!0, !1>.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005EE5 File Offset: 0x000040E5
		[Nullable(0)]
		private ImmutableDictionary<TKey, TValue>.MutationInput Origin
		{
			get
			{
				return new ImmutableDictionary<TKey, TValue>.MutationInput(this);
			}
		}

		// Token: 0x1700005B RID: 91
		public TValue this[TKey key]
		{
			get
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (!this.TryGetValue(key, out result))
				{
					ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
				}
				return result;
			}
		}

		// Token: 0x1700005C RID: 92
		TValue IDictionary<!0, !1>.this[TKey key]
		{
			get
			{
				return this[key];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005F2A File Offset: 0x0000412A
		bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005F2D File Offset: 0x0000412D
		[return: Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		public ImmutableDictionary<TKey, TValue>.Builder ToBuilder()
		{
			return new ImmutableDictionary<TKey, TValue>.Builder(this);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005F38 File Offset: 0x00004138
		public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin).Finalize(this);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005F67 File Offset: 0x00004167
		public ImmutableDictionary<TKey, TValue> AddRange([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			return this.AddRange(pairs, false);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005F7C File Offset: 0x0000417C
		public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin).Finalize(this);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005FAC File Offset: 0x000041AC
		public ImmutableDictionary<TKey, TValue> SetItems([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			return ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue).Finalize(this);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005FDC File Offset: 0x000041DC
		public ImmutableDictionary<TKey, TValue> Remove(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin).Finalize(this);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000600C File Offset: 0x0000420C
		public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
		{
			Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
			int num = this._count;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = this._root;
			foreach (TKey tkey in keys)
			{
				int hashCode = this.KeyComparer.GetHashCode(tkey);
				ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
				if (sortedInt32KeyNode.TryGetValue(hashCode, out hashBucket))
				{
					ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
					ImmutableDictionary<TKey, TValue>.HashBucket newBucket = hashBucket.Remove(tkey, this._comparers.KeyOnlyComparer, out operationResult);
					sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket, this._comparers.HashBucketEqualityComparer);
					if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
					{
						num--;
					}
				}
			}
			return this.Wrap(sortedInt32KeyNode, num);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000060C0 File Offset: 0x000042C0
		public bool ContainsKey(TKey key)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000060D9 File Offset: 0x000042D9
		public bool Contains([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> pair)
		{
			return ImmutableDictionary<TKey, TValue>.Contains(pair, this.Origin);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000060E7 File Offset: 0x000042E7
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006101 File Offset: 0x00004301
		public bool TryGetKey(TKey equalKey, out TKey actualKey)
		{
			Requires.NotNullAllowStructs<TKey>(equalKey, "equalKey");
			return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000611C File Offset: 0x0000431C
		public ImmutableDictionary<TKey, TValue> WithComparers([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				keyComparer = EqualityComparer<TKey>.Default;
			}
			if (valueComparer == null)
			{
				valueComparer = EqualityComparer<TValue>.Default;
			}
			if (this.KeyComparer != keyComparer)
			{
				return new ImmutableDictionary<TKey, TValue>(ImmutableDictionary<TKey, TValue>.Comparers.Get(keyComparer, valueComparer)).AddRange(this, true);
			}
			if (this.ValueComparer == valueComparer)
			{
				return this;
			}
			ImmutableDictionary<TKey, TValue>.Comparers comparers = this._comparers.WithValueComparer(valueComparer);
			return new ImmutableDictionary<TKey, TValue>(this._root, comparers, this._count);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006184 File Offset: 0x00004384
		public ImmutableDictionary<TKey, TValue> WithComparers([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer)
		{
			return this.WithComparers(keyComparer, this._comparers.ValueComparer);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006198 File Offset: 0x00004398
		public bool ContainsValue(TValue value)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				if (this.ValueComparer.Equals(value, keyValuePair.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000061FC File Offset: 0x000043FC
		[NullableContext(0)]
		public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, null);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000620A File Offset: 0x0000440A
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			return this.Add(key, value);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006214 File Offset: 0x00004414
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItem(TKey key, TValue value)
		{
			return this.SetItem(key, value);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000621E File Offset: 0x0000441E
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return this.SetItems(items);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006227 File Offset: 0x00004427
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
		{
			return this.AddRange(pairs);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006230 File Offset: 0x00004430
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.RemoveRange(IEnumerable<TKey> keys)
		{
			return this.RemoveRange(keys);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006239 File Offset: 0x00004439
		IImmutableDictionary<TKey, TValue> IImmutableDictionary<!0, !1>.Remove(TKey key)
		{
			return this.Remove(key);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006242 File Offset: 0x00004442
		void IDictionary<!0, !1>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006249 File Offset: 0x00004449
		bool IDictionary<!0, !1>.Remove(TKey key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006250 File Offset: 0x00004450
		void ICollection<KeyValuePair<!0, !1>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006257 File Offset: 0x00004457
		void ICollection<KeyValuePair<!0, !1>>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000625E File Offset: 0x0000445E
		bool ICollection<KeyValuePair<!0, !1>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006268 File Offset: 0x00004468
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000062F4 File Offset: 0x000044F4
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000062F7 File Offset: 0x000044F7
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000062FA File Offset: 0x000044FA
		ICollection IDictionary.Keys
		{
			get
			{
				return new KeysCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006302 File Offset: 0x00004502
		ICollection IDictionary.Values
		{
			get
			{
				return new ValuesCollectionAccessor<TKey, TValue>(this);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000630A File Offset: 0x0000450A
		[Nullable(new byte[]
		{
			1,
			0,
			0,
			0
		})]
		internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
		{
			[return: Nullable(new byte[]
			{
				1,
				0,
				0,
				0
			})]
			get
			{
				return this._root;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006312 File Offset: 0x00004512
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006319 File Offset: 0x00004519
		bool IDictionary.Contains(object key)
		{
			return this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006327 File Offset: 0x00004527
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006339 File Offset: 0x00004539
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000063 RID: 99
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)((object)key)];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000635A File Offset: 0x0000455A
		void IDictionary.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006364 File Offset: 0x00004564
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			Requires.NotNull<Array>(array, "array");
			Requires.Range(arrayIndex >= 0, "arrayIndex", null);
			Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00006414 File Offset: 0x00004614
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00006417 File Offset: 0x00004617
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000641C File Offset: 0x0000461C
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return this.GetEnumerator();
			}
			return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006449 File Offset: 0x00004649
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006456 File Offset: 0x00004656
		private static ImmutableDictionary<TKey, TValue> EmptyWithComparers(ImmutableDictionary<TKey, TValue>.Comparers comparers)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			if (ImmutableDictionary<TKey, TValue>.Empty._comparers != comparers)
			{
				return new ImmutableDictionary<TKey, TValue>(comparers);
			}
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000647C File Offset: 0x0000467C
		private static bool TryCastToImmutableMap(IEnumerable<KeyValuePair<TKey, TValue>> sequence, [NotNullWhen(true)] out ImmutableDictionary<TKey, TValue> other)
		{
			other = (sequence as ImmutableDictionary<TKey, TValue>);
			if (other != null)
			{
				return true;
			}
			ImmutableDictionary<TKey, TValue>.Builder builder = sequence as ImmutableDictionary<TKey, TValue>.Builder;
			if (builder != null)
			{
				other = builder.ToImmutable();
				return true;
			}
			return false;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000064AC File Offset: 0x000046AC
		private static bool ContainsKey(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue tvalue;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(key, origin.Comparers, out tvalue);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000064EC File Offset: 0x000046EC
		private static bool Contains(KeyValuePair<TKey, TValue> keyValuePair, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			TValue x;
			return origin.Root.TryGetValue(hashCode, out hashBucket) && hashBucket.TryGetValue(keyValuePair.Key, origin.Comparers, out x) && origin.ValueComparer.Equals(x, keyValuePair.Value);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006550 File Offset: 0x00004750
		private static bool TryGetValue(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin, [MaybeNullWhen(false)] out TValue value)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetValue(key, origin.Comparers, out value);
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006598 File Offset: 0x00004798
		private static bool TryGetKey(TKey equalKey, ImmutableDictionary<TKey, TValue>.MutationInput origin, out TKey actualKey)
		{
			int hashCode = origin.KeyComparer.GetHashCode(equalKey);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				return hashBucket.TryGetKey(equalKey, origin.Comparers, out actualKey);
			}
			actualKey = equalKey;
			return false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000065E0 File Offset: 0x000047E0
		private static ImmutableDictionary<TKey, TValue>.MutationResult Add(TKey key, TValue value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			Requires.NotNullAllowStructs<TKey>(key, "key");
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
			ImmutableDictionary<TKey, TValue>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode).Add(key, value, origin.KeyOnlyComparer, origin.ValueComparer, behavior, out operationResult);
			if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired)
			{
				return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, newBucket, origin.HashBucketComparer), (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? 1 : 0);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006658 File Offset: 0x00004858
		private static ImmutableDictionary<TKey, TValue>.MutationResult AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items, ImmutableDictionary<TKey, TValue>.MutationInput origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior collisionBehavior = ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, "items");
			int num = 0;
			SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> sortedInt32KeyNode = origin.Root;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				Requires.NotNullAllowStructs<TKey>(keyValuePair.Key, "Key");
				int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				ImmutableDictionary<TKey, TValue>.HashBucket newBucket = sortedInt32KeyNode.GetValueOrDefault(hashCode).Add(keyValuePair.Key, keyValuePair.Value, origin.KeyOnlyComparer, origin.ValueComparer, collisionBehavior, out operationResult);
				sortedInt32KeyNode = ImmutableDictionary<TKey, TValue>.UpdateRoot(sortedInt32KeyNode, hashCode, newBucket, origin.HashBucketComparer);
				if (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
				{
					num++;
				}
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(sortedInt32KeyNode, num);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000672C File Offset: 0x0000492C
		private static ImmutableDictionary<TKey, TValue>.MutationResult Remove(TKey key, ImmutableDictionary<TKey, TValue>.MutationInput origin)
		{
			int hashCode = origin.KeyComparer.GetHashCode(key);
			ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
			if (origin.Root.TryGetValue(hashCode, out hashBucket))
			{
				ImmutableDictionary<TKey, TValue>.OperationResult operationResult;
				return new ImmutableDictionary<TKey, TValue>.MutationResult(ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, hashBucket.Remove(key, origin.KeyOnlyComparer, out operationResult), origin.HashBucketComparer), (operationResult == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged) ? -1 : 0);
			}
			return new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006794 File Offset: 0x00004994
		private static SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> UpdateRoot(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int hashCode, ImmutableDictionary<TKey, TValue>.HashBucket newBucket, IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> hashBucketComparer)
		{
			bool flag;
			if (newBucket.IsEmpty)
			{
				return root.Remove(hashCode, out flag);
			}
			bool flag2;
			return root.SetItem(hashCode, newBucket, hashBucketComparer, out flag, out flag2);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000067C1 File Offset: 0x000049C1
		private static ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers, int count)
		{
			Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, "comparers");
			Requires.Range(count >= 0, "count", null);
			return new ImmutableDictionary<TKey, TValue>(root, comparers, count);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000067F3 File Offset: 0x000049F3
		private ImmutableDictionary<TKey, TValue> Wrap(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int adjustedCountIfDifferentRoot)
		{
			if (root == null)
			{
				return this.Clear();
			}
			if (this._root == root)
			{
				return this;
			}
			if (!root.IsEmpty)
			{
				return new ImmutableDictionary<TKey, TValue>(root, this._comparers, adjustedCountIfDifferentRoot);
			}
			return this.Clear();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006828 File Offset: 0x00004A28
		private ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs, bool avoidToHashMap)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, "pairs");
			ImmutableDictionary<TKey, TValue> immutableDictionary;
			if (this.IsEmpty && !avoidToHashMap && ImmutableDictionary<TKey, TValue>.TryCastToImmutableMap(pairs, out immutableDictionary))
			{
				return immutableDictionary.WithComparers(this.KeyComparer, this.ValueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.AddRange(pairs, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent).Finalize(this);
		}

		// Token: 0x0400002D RID: 45
		public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>(null);

		// Token: 0x0400002E RID: 46
		private static readonly Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>> s_FreezeBucketAction = delegate(KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> kv)
		{
			kv.Value.Freeze();
		};

		// Token: 0x0400002F RID: 47
		private readonly int _count;

		// Token: 0x04000030 RID: 48
		private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

		// Token: 0x04000031 RID: 49
		private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;

		// Token: 0x020000A2 RID: 162
		[Nullable(0)]
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
		public sealed class Builder : IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDictionary, ICollection
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x0001017C File Offset: 0x0000E37C
			internal Builder(ImmutableDictionary<TKey, TValue> map)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, "map");
				this._root = map._root;
				this._count = map._count;
				this._comparers = map._comparers;
				this._immutable = map;
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x000101D0 File Offset: 0x0000E3D0
			// (set) Token: 0x0600063F RID: 1599 RVA: 0x000101E0 File Offset: 0x0000E3E0
			public IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TKey>>(value, "value");
					if (value != this.KeyComparer)
					{
						ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(value, this.ValueComparer);
						ImmutableDictionary<TKey, TValue>.MutationInput origin = new ImmutableDictionary<TKey, TValue>.MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode, comparers);
						ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange(this, origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
						this._immutable = null;
						this._comparers = comparers;
						this._count = mutationResult.CountAdjustment;
						this.Root = mutationResult.Root;
					}
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001024C File Offset: 0x0000E44C
			// (set) Token: 0x06000641 RID: 1601 RVA: 0x00010259 File Offset: 0x0000E459
			public IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
				set
				{
					Requires.NotNull<IEqualityComparer<TValue>>(value, "value");
					if (value != this.ValueComparer)
					{
						this._comparers = this._comparers.WithValueComparer(value);
						this._immutable = null;
					}
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x00010288 File Offset: 0x0000E488
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x00010290 File Offset: 0x0000E490
			bool ICollection<KeyValuePair<!0, !1>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x00010293 File Offset: 0x0000E493
			public IEnumerable<TKey> Keys
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Key;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000645 RID: 1605 RVA: 0x000102A3 File Offset: 0x0000E4A3
			ICollection<TKey> IDictionary<!0, !1>.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000646 RID: 1606 RVA: 0x000102B6 File Offset: 0x0000E4B6
			public IEnumerable<TValue> Values
			{
				get
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
					{
						yield return keyValuePair.Value;
					}
					ImmutableDictionary<TKey, TValue>.Enumerator enumerator = default(ImmutableDictionary<TKey, TValue>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000647 RID: 1607 RVA: 0x000102C6 File Offset: 0x0000E4C6
			ICollection<TValue> IDictionary<!0, !1>.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000648 RID: 1608 RVA: 0x000102D9 File Offset: 0x0000E4D9
			bool IDictionary.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000649 RID: 1609 RVA: 0x000102DC File Offset: 0x0000E4DC
			bool IDictionary.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x0600064A RID: 1610 RVA: 0x000102DF File Offset: 0x0000E4DF
			ICollection IDictionary.Keys
			{
				get
				{
					return this.Keys.ToArray(this.Count);
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x0600064B RID: 1611 RVA: 0x000102F2 File Offset: 0x0000E4F2
			ICollection IDictionary.Values
			{
				get
				{
					return this.Values.ToArray(this.Count);
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x0600064C RID: 1612 RVA: 0x00010305 File Offset: 0x0000E505
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

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600064D RID: 1613 RVA: 0x00010327 File Offset: 0x0000E527
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x0001032A File Offset: 0x0000E52A
			void IDictionary.Add(object key, object value)
			{
				this.Add((TKey)((object)key), (TValue)((object)value));
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x0001033E File Offset: 0x0000E53E
			bool IDictionary.Contains(object key)
			{
				return this.ContainsKey((TKey)((object)key));
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0001034C File Offset: 0x0000E54C
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				return new DictionaryEnumerator<TKey, TValue>(this.GetEnumerator());
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0001035E File Offset: 0x0000E55E
			void IDictionary.Remove(object key)
			{
				this.Remove((TKey)((object)key));
			}

			// Token: 0x1700012A RID: 298
			[Nullable(2)]
			object IDictionary.this[object key]
			{
				get
				{
					return this[(TKey)((object)key)];
				}
				set
				{
					this[(TKey)((object)key)] = (TValue)((object)value);
				}
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x00010394 File Offset: 0x0000E594
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				Requires.NotNull<Array>(array, "array");
				Requires.Range(arrayIndex >= 0, "arrayIndex", null);
				Requires.Range(array.Length >= arrayIndex + this.Count, "arrayIndex", null);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array.SetValue(new DictionaryEntry(keyValuePair.Key, keyValuePair.Value), arrayIndex++);
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000655 RID: 1621 RVA: 0x00010444 File Offset: 0x0000E644
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001044C File Offset: 0x0000E64C
			[Nullable(0)]
			private ImmutableDictionary<TKey, TValue>.MutationInput Origin
			{
				get
				{
					return new ImmutableDictionary<TKey, TValue>.MutationInput(this.Root, this._comparers);
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001045F File Offset: 0x0000E65F
			// (set) Token: 0x06000658 RID: 1624 RVA: 0x00010467 File Offset: 0x0000E667
			[Nullable(new byte[]
			{
				1,
				0,
				0,
				0
			})]
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
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

			// Token: 0x1700012E RID: 302
			public TValue this[TKey key]
			{
				get
				{
					TValue result;
					if (!this.TryGetValue(key, out result))
					{
						ThrowHelper.ThrowKeyNotFoundException<TKey>(key);
					}
					return result;
				}
				set
				{
					ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin);
					this.Apply(result);
				}
			}

			// Token: 0x0600065B RID: 1627 RVA: 0x000104D4 File Offset: 0x0000E6D4
			public void AddRange([Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEnumerable<KeyValuePair<TKey, TValue>> items)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent);
				this.Apply(result);
			}

			// Token: 0x0600065C RID: 1628 RVA: 0x000104F8 File Offset: 0x0000E6F8
			public void RemoveRange(IEnumerable<TKey> keys)
			{
				Requires.NotNull<IEnumerable<TKey>>(keys, "keys");
				foreach (TKey key in keys)
				{
					this.Remove(key);
				}
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x0001054C File Offset: 0x0000E74C
			[NullableContext(0)]
			public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.Enumerator(this._root, this);
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x0001055C File Offset: 0x0000E75C
			[return: Nullable(2)]
			public TValue GetValueOrDefault(TKey key)
			{
				return this.GetValueOrDefault(key, default(TValue));
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x0001057C File Offset: 0x0000E77C
			public TValue GetValueOrDefault(TKey key, TValue defaultValue)
			{
				Requires.NotNullAllowStructs<TKey>(key, "key");
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				return defaultValue;
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x000105A4 File Offset: 0x0000E7A4
			public ImmutableDictionary<TKey, TValue> ToImmutable()
			{
				ImmutableDictionary<TKey, TValue> result;
				if ((result = this._immutable) == null)
				{
					result = (this._immutable = ImmutableDictionary<TKey, TValue>.Wrap(this._root, this._comparers, this._count));
				}
				return result;
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x000105DC File Offset: 0x0000E7DC
			public void Add(TKey key, TValue value)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin);
				this.Apply(result);
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x00010600 File Offset: 0x0000E800
			public bool ContainsKey(TKey key)
			{
				return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x00010610 File Offset: 0x0000E810
			public bool ContainsValue(TValue value)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					if (this.ValueComparer.Equals(value, keyValuePair.Value))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00010674 File Offset: 0x0000E874
			public bool Remove(TKey key)
			{
				ImmutableDictionary<TKey, TValue>.MutationResult result = ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin);
				return this.Apply(result);
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x00010695 File Offset: 0x0000E895
			public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x000106A4 File Offset: 0x0000E8A4
			public bool TryGetKey(TKey equalKey, out TKey actualKey)
			{
				return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x000106B3 File Offset: 0x0000E8B3
			public void Add([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x000106C9 File Offset: 0x0000E8C9
			public void Clear()
			{
				this.Root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;
				this._count = 0;
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x000106DD File Offset: 0x0000E8DD
			public bool Contains([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> item)
			{
				return ImmutableDictionary<TKey, TValue>.Contains(item, this.Origin);
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x000106EC File Offset: 0x0000E8EC
			void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, "array");
				foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
				{
					array[arrayIndex++] = keyValuePair;
				}
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x0001074C File Offset: 0x0000E94C
			public bool Remove([Nullable(new byte[]
			{
				0,
				1,
				1
			})] KeyValuePair<TKey, TValue> item)
			{
				return this.Contains(item) && this.Remove(item.Key);
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x00010766 File Offset: 0x0000E966
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x00010773 File Offset: 0x0000E973
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x00010780 File Offset: 0x0000E980
			private bool Apply(ImmutableDictionary<TKey, TValue>.MutationResult result)
			{
				this.Root = result.Root;
				this._count += result.CountAdjustment;
				return result.CountAdjustment != 0;
			}

			// Token: 0x040000D5 RID: 213
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root = SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.EmptyNode;

			// Token: 0x040000D6 RID: 214
			private ImmutableDictionary<TKey, TValue>.Comparers _comparers;

			// Token: 0x040000D7 RID: 215
			private int _count;

			// Token: 0x040000D8 RID: 216
			private ImmutableDictionary<TKey, TValue> _immutable;

			// Token: 0x040000D9 RID: 217
			private int _version;

			// Token: 0x040000DA RID: 218
			private object _syncRoot;
		}

		// Token: 0x020000A3 RID: 163
		[Nullable(0)]
		internal sealed class Comparers : IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>, IEqualityComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600066F RID: 1647 RVA: 0x000107AD File Offset: 0x0000E9AD
			internal Comparers(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				this._keyComparer = keyComparer;
				this._valueComparer = valueComparer;
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000670 RID: 1648 RVA: 0x000107D9 File Offset: 0x0000E9D9
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._keyComparer;
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000671 RID: 1649 RVA: 0x000107E1 File Offset: 0x0000E9E1
			[Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})]
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				[return: Nullable(new byte[]
				{
					1,
					0,
					1,
					1
				})]
				get
				{
					return this;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000672 RID: 1650 RVA: 0x000107E4 File Offset: 0x0000E9E4
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._valueComparer;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000673 RID: 1651 RVA: 0x000107EC File Offset: 0x0000E9EC
			[Nullable(new byte[]
			{
				1,
				0,
				0,
				0
			})]
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketEqualityComparer
			{
				[return: Nullable(new byte[]
				{
					1,
					0,
					0,
					0
				})]
				get
				{
					return this;
				}
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000107F0 File Offset: 0x0000E9F0
			[NullableContext(0)]
			public bool Equals(ImmutableDictionary<TKey, TValue>.HashBucket x, ImmutableDictionary<TKey, TValue>.HashBucket y)
			{
				return x.AdditionalElements == y.AdditionalElements && this.KeyComparer.Equals(x.FirstValue.Key, y.FirstValue.Key) && this.ValueComparer.Equals(x.FirstValue.Value, y.FirstValue.Value);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x00010864 File Offset: 0x0000EA64
			[NullableContext(0)]
			public int GetHashCode(ImmutableDictionary<TKey, TValue>.HashBucket obj)
			{
				return this.KeyComparer.GetHashCode(obj.FirstValue.Key);
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0001088B File Offset: 0x0000EA8B
			bool IEqualityComparer<KeyValuePair<!0, !1>>.Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this._keyComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x000108A6 File Offset: 0x0000EAA6
			int IEqualityComparer<KeyValuePair<!0, !1>>.GetHashCode(KeyValuePair<TKey, TValue> obj)
			{
				return this._keyComparer.GetHashCode(obj.Key);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x000108BA File Offset: 0x0000EABA
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal static ImmutableDictionary<TKey, TValue>.Comparers Get(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, "keyComparer");
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (keyComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.KeyComparer || valueComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.ValueComparer)
				{
					return new ImmutableDictionary<TKey, TValue>.Comparers(keyComparer, valueComparer);
				}
				return ImmutableDictionary<TKey, TValue>.Comparers.Default;
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x000108F9 File Offset: 0x0000EAF9
			[return: Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal ImmutableDictionary<TKey, TValue>.Comparers WithValueComparer(IEqualityComparer<TValue> valueComparer)
			{
				Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
				if (this._valueComparer != valueComparer)
				{
					return ImmutableDictionary<TKey, TValue>.Comparers.Get(this.KeyComparer, valueComparer);
				}
				return this;
			}

			// Token: 0x040000DB RID: 219
			[Nullable(new byte[]
			{
				1,
				0,
				0
			})]
			internal static readonly ImmutableDictionary<TKey, TValue>.Comparers Default = new ImmutableDictionary<TKey, TValue>.Comparers(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);

			// Token: 0x040000DC RID: 220
			private readonly IEqualityComparer<TKey> _keyComparer;

			// Token: 0x040000DD RID: 221
			private readonly IEqualityComparer<TValue> _valueComparer;
		}

		// Token: 0x020000A4 RID: 164
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600067B RID: 1659 RVA: 0x00010933 File Offset: 0x0000EB33
			internal Enumerator([Nullable(new byte[]
			{
				1,
				0,
				0,
				0
			})] SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, [Nullable(new byte[]
			{
				2,
				0,
				0
			})] ImmutableDictionary<TKey, TValue>.Builder builder = null)
			{
				this._builder = builder;
				this._mapEnumerator = new SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator(root);
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
				this._enumeratingBuilderVersion = ((builder != null) ? builder.Version : -1);
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x00010966 File Offset: 0x0000EB66
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public KeyValuePair<TKey, TValue> Current
			{
				[return: Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				get
				{
					this._mapEnumerator.ThrowIfDisposed();
					return this._bucketEnumerator.Current;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001097E File Offset: 0x0000EB7E
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x0001098C File Offset: 0x0000EB8C
			public bool MoveNext()
			{
				this.ThrowIfChanged();
				if (this._bucketEnumerator.MoveNext())
				{
					return true;
				}
				if (this._mapEnumerator.MoveNext())
				{
					KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> keyValuePair = this._mapEnumerator.Current;
					this._bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(keyValuePair.Value);
					return this._bucketEnumerator.MoveNext();
				}
				return false;
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x000109E6 File Offset: 0x0000EBE6
			public void Reset()
			{
				this._enumeratingBuilderVersion = ((this._builder != null) ? this._builder.Version : -1);
				this._mapEnumerator.Reset();
				this._bucketEnumerator.Dispose();
				this._bucketEnumerator = default(ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator);
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x00010A26 File Offset: 0x0000EC26
			public void Dispose()
			{
				this._mapEnumerator.Dispose();
				this._bucketEnumerator.Dispose();
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x00010A3E File Offset: 0x0000EC3E
			private void ThrowIfChanged()
			{
				if (this._builder != null && this._builder.Version != this._enumeratingBuilderVersion)
				{
					throw new InvalidOperationException(SR.CollectionModifiedDuringEnumeration);
				}
			}

			// Token: 0x040000DE RID: 222
			private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;

			// Token: 0x040000DF RID: 223
			private SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator _mapEnumerator;

			// Token: 0x040000E0 RID: 224
			private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator _bucketEnumerator;

			// Token: 0x040000E1 RID: 225
			private int _enumeratingBuilderVersion;
		}

		// Token: 0x020000A5 RID: 165
		[NullableContext(0)]
		internal readonly struct HashBucket : IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
		{
			// Token: 0x06000682 RID: 1666 RVA: 0x00010A66 File Offset: 0x0000EC66
			private HashBucket(KeyValuePair<TKey, TValue> firstElement, ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = null)
			{
				this._firstValue = firstElement;
				this._additionalElements = (additionalElements ?? ImmutableList<KeyValuePair<TKey, TValue>>.Node.EmptyNode);
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000683 RID: 1667 RVA: 0x00010A7F File Offset: 0x0000EC7F
			internal bool IsEmpty
			{
				get
				{
					return this._additionalElements == null;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000684 RID: 1668 RVA: 0x00010A8A File Offset: 0x0000EC8A
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			internal KeyValuePair<TKey, TValue> FirstValue
			{
				[return: Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				get
				{
					if (this.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._firstValue;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000685 RID: 1669 RVA: 0x00010AA0 File Offset: 0x0000ECA0
			[Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})]
			internal ImmutableList<KeyValuePair<TKey, TValue>>.Node AdditionalElements
			{
				[return: Nullable(new byte[]
				{
					1,
					0,
					1,
					1
				})]
				get
				{
					return this._additionalElements;
				}
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x00010AA8 File Offset: 0x0000ECA8
			public ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator GetEnumerator()
			{
				return new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this);
			}

			// Token: 0x06000687 RID: 1671 RVA: 0x00010AB5 File Offset: 0x0000ECB5
			IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x00010AC2 File Offset: 0x0000ECC2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x00010ACF File Offset: 0x0000ECCF
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x00010AD6 File Offset: 0x0000ECD6
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x00010AE0 File Offset: 0x0000ECE0
			internal ImmutableDictionary<TKey, TValue>.HashBucket Add([Nullable(1)] TKey key, [Nullable(1)] TValue value, [Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, [Nullable(1)] IEqualityComparer<TValue> valueComparer, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, null);
				}
				if (keyOnlyComparer.Equals(keyValuePair, this._firstValue))
				{
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, this._additionalElements);
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
						if (!valueComparer.Equals(this._firstValue.Value, value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					default:
						throw new InvalidOperationException();
					}
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.Add(keyValuePair));
					}
					switch (behavior)
					{
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
						return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.ReplaceAt(num, keyValuePair));
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
					{
						ref readonly KeyValuePair<TKey, TValue> ptr = ref this._additionalElements.ItemRef(num);
						KeyValuePair<TKey, TValue> keyValuePair2 = ptr;
						if (!valueComparer.Equals(keyValuePair2.Value, value))
						{
							throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
						}
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
						throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
					default:
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x00010C7C File Offset: 0x0000EE7C
			internal ImmutableDictionary<TKey, TValue>.HashBucket Remove([Nullable(1)] TKey key, [Nullable(new byte[]
			{
				1,
				0,
				1,
				1
			})] IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer, out ImmutableDictionary<TKey, TValue>.OperationResult result)
			{
				if (this.IsEmpty)
				{
					result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
					return this;
				}
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
				if (keyOnlyComparer.Equals(this._firstValue, keyValuePair))
				{
					if (this._additionalElements.IsEmpty)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
						return default(ImmutableDictionary<TKey, TValue>.HashBucket);
					}
					int count = this._additionalElements.Left.Count;
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._additionalElements.Key, this._additionalElements.RemoveAt(count));
				}
				else
				{
					int num = this._additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
					if (num < 0)
					{
						result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
						return this;
					}
					result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
					return new ImmutableDictionary<TKey, TValue>.HashBucket(this._firstValue, this._additionalElements.RemoveAt(num));
				}
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00010D40 File Offset: 0x0000EF40
			[NullableContext(1)]
			internal unsafe bool TryGetValue(TKey key, [Nullable(new byte[]
			{
				1,
				0,
				0
			})] ImmutableDictionary<TKey, TValue>.Comparers comparers, [MaybeNullWhen(false)] out TValue value)
			{
				if (this.IsEmpty)
				{
					value = default(TValue);
					return false;
				}
				if (comparers.KeyComparer.Equals(this._firstValue.Key, key))
				{
					value = this._firstValue.Value;
					return true;
				}
				KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, default(TValue));
				int num = this._additionalElements.IndexOf(item, comparers.KeyOnlyComparer);
				if (num < 0)
				{
					value = default(TValue);
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair = *this._additionalElements.ItemRef(num);
				value = keyValuePair.Value;
				return true;
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x00010DE4 File Offset: 0x0000EFE4
			[NullableContext(1)]
			internal unsafe bool TryGetKey(TKey equalKey, [Nullable(new byte[]
			{
				1,
				0,
				0
			})] ImmutableDictionary<TKey, TValue>.Comparers comparers, out TKey actualKey)
			{
				if (this.IsEmpty)
				{
					actualKey = equalKey;
					return false;
				}
				if (comparers.KeyComparer.Equals(this._firstValue.Key, equalKey))
				{
					actualKey = this._firstValue.Key;
					return true;
				}
				KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(equalKey, default(TValue));
				int num = this._additionalElements.IndexOf(item, comparers.KeyOnlyComparer);
				if (num < 0)
				{
					actualKey = equalKey;
					return false;
				}
				KeyValuePair<TKey, TValue> keyValuePair = *this._additionalElements.ItemRef(num);
				actualKey = keyValuePair.Key;
				return true;
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x00010E85 File Offset: 0x0000F085
			internal void Freeze()
			{
				ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = this._additionalElements;
				if (additionalElements == null)
				{
					return;
				}
				additionalElements.Freeze();
			}

			// Token: 0x040000E2 RID: 226
			private readonly KeyValuePair<TKey, TValue> _firstValue;

			// Token: 0x040000E3 RID: 227
			private readonly ImmutableList<KeyValuePair<TKey, TValue>>.Node _additionalElements;

			// Token: 0x020000DA RID: 218
			internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
			{
				// Token: 0x060008CE RID: 2254 RVA: 0x00016C33 File Offset: 0x00014E33
				internal Enumerator(ImmutableDictionary<TKey, TValue>.HashBucket bucket)
				{
					this._bucket = bucket;
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
					this._additionalEnumerator = default(ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator);
				}

				// Token: 0x170001D3 RID: 467
				// (get) Token: 0x060008CF RID: 2255 RVA: 0x00016C4F File Offset: 0x00014E4F
				[Nullable(1)]
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x170001D4 RID: 468
				// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00016C5C File Offset: 0x00014E5C
				[Nullable(new byte[]
				{
					0,
					1,
					1
				})]
				public KeyValuePair<TKey, TValue> Current
				{
					[return: Nullable(new byte[]
					{
						0,
						1,
						1
					})]
					get
					{
						ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position currentPosition = this._currentPosition;
						KeyValuePair<TKey, TValue> result;
						if (currentPosition != ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First)
						{
							if (currentPosition != ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional)
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

				// Token: 0x060008D1 RID: 2257 RVA: 0x00016CA0 File Offset: 0x00014EA0
				public bool MoveNext()
				{
					if (this._bucket.IsEmpty)
					{
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
						return false;
					}
					switch (this._currentPosition)
					{
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst:
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First;
						return true;
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First:
						if (this._bucket._additionalElements.IsEmpty)
						{
							this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
							return false;
						}
						this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional;
						this._additionalEnumerator = new ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator(this._bucket._additionalElements, null, -1, -1, false);
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional:
						return this._additionalEnumerator.MoveNext();
					case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End:
						return false;
					default:
						throw new InvalidOperationException();
					}
				}

				// Token: 0x060008D2 RID: 2258 RVA: 0x00016D46 File Offset: 0x00014F46
				public void Reset()
				{
					this._additionalEnumerator.Dispose();
					this._currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
				}

				// Token: 0x060008D3 RID: 2259 RVA: 0x00016D5A File Offset: 0x00014F5A
				public void Dispose()
				{
					this._additionalEnumerator.Dispose();
				}

				// Token: 0x04000189 RID: 393
				private readonly ImmutableDictionary<TKey, TValue>.HashBucket _bucket;

				// Token: 0x0400018A RID: 394
				private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position _currentPosition;

				// Token: 0x0400018B RID: 395
				private ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator _additionalEnumerator;

				// Token: 0x020000DD RID: 221
				private enum Position
				{
					// Token: 0x04000195 RID: 405
					BeforeFirst,
					// Token: 0x04000196 RID: 406
					First,
					// Token: 0x04000197 RID: 407
					Additional,
					// Token: 0x04000198 RID: 408
					End
				}
			}
		}

		// Token: 0x020000A6 RID: 166
		private readonly struct MutationInput
		{
			// Token: 0x06000690 RID: 1680 RVA: 0x00010E97 File Offset: 0x0000F097
			internal MutationInput(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, ImmutableDictionary<TKey, TValue>.Comparers comparers)
			{
				this._root = root;
				this._comparers = comparers;
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x00010EA7 File Offset: 0x0000F0A7
			internal MutationInput(ImmutableDictionary<TKey, TValue> map)
			{
				this._root = map._root;
				this._comparers = map._comparers;
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000692 RID: 1682 RVA: 0x00010EC1 File Offset: 0x0000F0C1
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000693 RID: 1683 RVA: 0x00010EC9 File Offset: 0x0000F0C9
			internal ImmutableDictionary<TKey, TValue>.Comparers Comparers
			{
				get
				{
					return this._comparers;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000694 RID: 1684 RVA: 0x00010ED1 File Offset: 0x0000F0D1
			internal IEqualityComparer<TKey> KeyComparer
			{
				get
				{
					return this._comparers.KeyComparer;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000695 RID: 1685 RVA: 0x00010EDE File Offset: 0x0000F0DE
			internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
			{
				get
				{
					return this._comparers.KeyOnlyComparer;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000696 RID: 1686 RVA: 0x00010EEB File Offset: 0x0000F0EB
			internal IEqualityComparer<TValue> ValueComparer
			{
				get
				{
					return this._comparers.ValueComparer;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000697 RID: 1687 RVA: 0x00010EF8 File Offset: 0x0000F0F8
			internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketComparer
			{
				get
				{
					return this._comparers.HashBucketEqualityComparer;
				}
			}

			// Token: 0x040000E4 RID: 228
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x040000E5 RID: 229
			private readonly ImmutableDictionary<TKey, TValue>.Comparers _comparers;
		}

		// Token: 0x020000A7 RID: 167
		private readonly struct MutationResult
		{
			// Token: 0x06000698 RID: 1688 RVA: 0x00010F05 File Offset: 0x0000F105
			internal MutationResult(ImmutableDictionary<TKey, TValue>.MutationInput unchangedInput)
			{
				this._root = unchangedInput.Root;
				this._countAdjustment = 0;
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x00010F1B File Offset: 0x0000F11B
			internal MutationResult(SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> root, int countAdjustment)
			{
				Requires.NotNull<SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket>>(root, "root");
				this._root = root;
				this._countAdjustment = countAdjustment;
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x0600069A RID: 1690 RVA: 0x00010F36 File Offset: 0x0000F136
			internal SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> Root
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x0600069B RID: 1691 RVA: 0x00010F3E File Offset: 0x0000F13E
			internal int CountAdjustment
			{
				get
				{
					return this._countAdjustment;
				}
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x00010F46 File Offset: 0x0000F146
			internal ImmutableDictionary<TKey, TValue> Finalize(ImmutableDictionary<TKey, TValue> priorMap)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(priorMap, "priorMap");
				return priorMap.Wrap(this.Root, priorMap._count + this.CountAdjustment);
			}

			// Token: 0x040000E6 RID: 230
			private readonly SortedInt32KeyNode<ImmutableDictionary<TKey, TValue>.HashBucket> _root;

			// Token: 0x040000E7 RID: 231
			private readonly int _countAdjustment;
		}

		// Token: 0x020000A8 RID: 168
		[NullableContext(0)]
		internal enum KeyCollisionBehavior
		{
			// Token: 0x040000E9 RID: 233
			SetValue,
			// Token: 0x040000EA RID: 234
			Skip,
			// Token: 0x040000EB RID: 235
			ThrowIfValueDifferent,
			// Token: 0x040000EC RID: 236
			ThrowAlways
		}

		// Token: 0x020000A9 RID: 169
		[NullableContext(0)]
		internal enum OperationResult
		{
			// Token: 0x040000EE RID: 238
			AppliedWithoutSizeChange,
			// Token: 0x040000EF RID: 239
			SizeChanged,
			// Token: 0x040000F0 RID: 240
			NoChangeRequired
		}
	}
}
