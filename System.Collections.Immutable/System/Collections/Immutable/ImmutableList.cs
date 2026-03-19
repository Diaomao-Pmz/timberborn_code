using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableList
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00007282 File Offset: 0x00005482
		public static ImmutableList<T> Create<[Nullable(2)] T>()
		{
			return ImmutableList<T>.Empty;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007289 File Offset: 0x00005489
		public static ImmutableList<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableList<T>.Empty.Add(item);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007296 File Offset: 0x00005496
		public static ImmutableList<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000072A3 File Offset: 0x000054A3
		public static ImmutableList<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableList.Create<T>(items);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000072BB File Offset: 0x000054BB
		public static ImmutableList<T> Create<[Nullable(2)] T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			return ImmutableList<T>.Empty.AddRange(items);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000072C8 File Offset: 0x000054C8
		public static ImmutableList<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableList.Create<T>().ToBuilder();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000072D4 File Offset: 0x000054D4
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			ImmutableList<TSource> immutableList = source as ImmutableList<TSource>;
			if (immutableList != null)
			{
				return immutableList;
			}
			return ImmutableList<TSource>.Empty.AddRange(source);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000072F8 File Offset: 0x000054F8
		public static ImmutableList<TSource> ToImmutableList<[Nullable(2)] TSource>(this ImmutableList<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableList<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000730B File Offset: 0x0000550B
		public static IImmutableList<T> Replace<[Nullable(2)] T>(this IImmutableList<T> list, T oldValue, T newValue)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007325 File Offset: 0x00005525
		public static IImmutableList<T> Remove<[Nullable(2)] T>(this IImmutableList<T> list, T value)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.Remove(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000733E File Offset: 0x0000553E
		public static IImmutableList<T> RemoveRange<[Nullable(2)] T>(this IImmutableList<T> list, IEnumerable<T> items)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007357 File Offset: 0x00005557
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007377 File Offset: 0x00005577
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, 0, list.Count, equalityComparer);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007393 File Offset: 0x00005593
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, list.Count - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000073B5 File Offset: 0x000055B5
		public static int IndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000073D0 File Offset: 0x000055D0
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, EqualityComparer<T>.Default);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007401 File Offset: 0x00005601
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000742E File Offset: 0x0000562E
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			if (list.Count == 0 && startIndex == 0)
			{
				return -1;
			}
			return list.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007458 File Offset: 0x00005658
		public static int LastIndexOf<[Nullable(2)] T>(this IImmutableList<T> list, T item, int startIndex, int count)
		{
			Requires.NotNull<IImmutableList<T>>(list, "list");
			return list.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}
	}
}
