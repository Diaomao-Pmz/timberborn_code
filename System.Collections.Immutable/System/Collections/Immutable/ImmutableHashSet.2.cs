using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableHashSet
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00006BD0 File Offset: 0x00004DD0
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableHashSet<T>.Empty;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006BD7 File Offset: 0x00004DD7
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableHashSet<T>.Empty.Add(item);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006BF1 File Offset: 0x00004DF1
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer, T item)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006C04 File Offset: 0x00004E04
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006C11 File Offset: 0x00004E11
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer, IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006C24 File Offset: 0x00004E24
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableHashSet.Create<T>(items);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006C3C File Offset: 0x00004E3C
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006C49 File Offset: 0x00004E49
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer, params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableHashSet.Create<T>(equalityComparer, items);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006C62 File Offset: 0x00004E62
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer, [ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006C75 File Offset: 0x00004E75
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableHashSet.Create<T>().ToBuilder();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006C81 File Offset: 0x00004E81
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006C90 File Offset: 0x00004E90
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TSource> equalityComparer)
		{
			ImmutableHashSet<TSource> immutableHashSet = source as ImmutableHashSet<TSource>;
			if (immutableHashSet != null)
			{
				return immutableHashSet.WithComparer(equalityComparer);
			}
			return ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this ImmutableHashSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableHashSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006CD3 File Offset: 0x00004ED3
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableHashSet(null);
		}
	}
}
