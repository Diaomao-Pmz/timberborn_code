using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedDictionary
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00008086 File Offset: 0x00006286
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000808D File Offset: 0x0000628D
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000809A File Offset: 0x0000629A
		public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000080A8 File Offset: 0x000062A8
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000080B5 File Offset: 0x000062B5
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000080C8 File Offset: 0x000062C8
		public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
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
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000080DC File Offset: 0x000062DC
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>()
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000080E8 File Offset: 0x000062E8
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000080F5 File Offset: 0x000062F5
		public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008104 File Offset: 0x00006304
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<TSource>>(source, "source");
			Requires.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<TSource, TValue>>(elementSelector, "elementSelector");
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(from element in source
			select new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)));
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008174 File Offset: 0x00006374
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>(this ImmutableSortedDictionary<TKey, TValue>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008187 File Offset: 0x00006387
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, keyComparer, null);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008193 File Offset: 0x00006393
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector)
		{
			return source.ToImmutableSortedDictionary(keySelector, elementSelector, null, null);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000081A0 File Offset: 0x000063A0
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TValue> valueComparer)
		{
			Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, "source");
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = source as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.WithComparers(keyComparer, valueComparer);
			}
			return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000081DD File Offset: 0x000063DD
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TKey> keyComparer)
		{
			return source.ToImmutableSortedDictionary(keyComparer, null);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000081E7 File Offset: 0x000063E7
		public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			return source.ToImmutableSortedDictionary(null, null);
		}
	}
}
