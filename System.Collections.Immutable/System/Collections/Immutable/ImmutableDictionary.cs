using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableDictionary
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00005BCF File Offset: 0x00003DCF
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005BE3 File Offset: 0x00003DE3
		public static ImmutableDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005BF1 File Offset: 0x00003DF1
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005BFE File Offset: 0x00003DFE
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005C11 File Offset: 0x00003E11
		public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer, [Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005C25 File Offset: 0x00003E25
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005C31 File Offset: 0x00003E31
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005C3E File Offset: 0x00003E3E
		public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005C4C File Offset: 0x00003E4C
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(from element in source
			select new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005CBC File Offset: 0x00003EBC
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>(this ImmutableDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005CCF File Offset: 0x00003ECF
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005CDB File Offset: 0x00003EDB
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToImmutableDictionary(keySelector, (TSource v) => v, null, null);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005D05 File Offset: 0x00003F05
		public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keySelector, (TSource v) => v, keyComparer, null);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005D2F File Offset: 0x00003F2F
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005D3C File Offset: 0x00003F3C
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableDictionary<TKey, TValue> immutableDictionary = source as ImmutableDictionary<TKey, TValue>;
			if (immutableDictionary != null)
			{
				return immutableDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005D79 File Offset: 0x00003F79
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> keyComparer)
		{
			return source.ToImmutableDictionary(keyComparer, null);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005D83 File Offset: 0x00003F83
		public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableDictionary(null, null);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005D8D File Offset: 0x00003F8D
		public static bool Contains<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> map, TKey key, TValue value)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(map, "map");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			return map.Contains(new KeyValuePair<TKey, TValue>(key, value));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005DB4 File Offset: 0x00003FB4
		[return: Nullable(2)]
		public static TValue GetValueOrDefault<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public static TValue GetValueOrDefault<TKey, [Nullable(2)] TValue>(this IImmutableDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, "dictionary");
			Requires.NotNullAllowStructs<TKey>(key, "key");
			TValue result;
			if (dictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return defaultValue;
		}
	}
}
