using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000044 RID: 68
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedSet
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00008A3A File Offset: 0x00006C3A
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableSortedSet<T>.Empty;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008A41 File Offset: 0x00006C41
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008A4E File Offset: 0x00006C4E
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableSortedSet<T>.Empty.Add(item);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008A5B File Offset: 0x00006C5B
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer, T item)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008A6E File Offset: 0x00006C6E
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008A7B File Offset: 0x00006C7B
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer, IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008A8E File Offset: 0x00006C8E
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableSortedSet.Create<T>(items);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008AA6 File Offset: 0x00006CA6
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008AB3 File Offset: 0x00006CB3
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer, params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableSortedSet.Create<T>(comparer, items);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008ACC File Offset: 0x00006CCC
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer, [ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008ADF File Offset: 0x00006CDF
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableSortedSet.Create<T>().ToBuilder();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008AEB File Offset: 0x00006CEB
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<TSource> comparer)
		{
			ImmutableSortedSet<TSource> immutableSortedSet = source as ImmutableSortedSet<TSource>;
			if (immutableSortedSet != null)
			{
				return immutableSortedSet.WithComparer(comparer);
			}
			return ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008B28 File Offset: 0x00006D28
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableSortedSet(null);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008B31 File Offset: 0x00006D31
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this ImmutableSortedSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}
	}
}
