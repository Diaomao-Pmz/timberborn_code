using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArray
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00003FA7 File Offset: 0x000021A7
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<T>()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003FAE File Offset: 0x000021AE
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item)
		{
			return new ImmutableArray<T>(new T[]
			{
				item
			});
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003FC3 File Offset: 0x000021C3
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2)
		{
			return new ImmutableArray<T>(new T[]
			{
				item1,
				item2
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003FE0 File Offset: 0x000021E0
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2, T item3)
		{
			return new ImmutableArray<T>(new T[]
			{
				item1,
				item2,
				item3
			});
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004005 File Offset: 0x00002205
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T item1, T item2, T item3, T item4)
		{
			return new ImmutableArray<T>(new T[]
			{
				item1,
				item2,
				item3,
				item4
			});
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004032 File Offset: 0x00002232
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			if (items.IsEmpty)
			{
				return ImmutableArray<T>.Empty;
			}
			return new ImmutableArray<T>(items.ToArray());
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000404F File Offset: 0x0000224F
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> items)
		{
			return ImmutableArray.Create<T>(items);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000405C File Offset: 0x0000225C
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> ToImmutableArray<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> items)
		{
			return ImmutableArray.Create<T>(items);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004064 File Offset: 0x00002264
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> ToImmutableArray<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> items)
		{
			return ImmutableArray.Create<T>(items);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004074 File Offset: 0x00002274
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			IImmutableArray immutableArray = items as IImmutableArray;
			if (immutableArray != null)
			{
				Array array = immutableArray.Array;
				if (array == null)
				{
					throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
				}
				return new ImmutableArray<T>((T[])array);
			}
			else
			{
				int count;
				if (items.TryGetCount(out count))
				{
					return new ImmutableArray<T>(items.ToArray(count));
				}
				return new ImmutableArray<T>(items.ToArray<T>());
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000040D8 File Offset: 0x000022D8
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[]
		{
			2,
			1
		})] params T[] items)
		{
			if (items == null || items.Length == 0)
			{
				return ImmutableArray<T>.Empty;
			}
			T[] array = new T[items.Length];
			Array.Copy(items, array, items.Length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000410C File Offset: 0x0000230C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<[Nullable(2)] T>(T[] items, int start, int length)
		{
			Requires.NotNull<T[]>(items, "items");
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			T[] array = new T[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = items[start + i];
			}
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004190 File Offset: 0x00002390
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> Create<T>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> items, int start, int length)
		{
			Requires.Range(start >= 0 && start <= items.Length, "start", null);
			Requires.Range(length >= 0 && start + length <= items.Length, "length", null);
			if (length == 0)
			{
				return ImmutableArray.Create<T>();
			}
			if (start == 0 && length == items.Length)
			{
				return items;
			}
			T[] array = new T[length];
			Array.Copy(items.array, start, array, 0, length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004214 File Offset: 0x00002414
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TSource> items, [Nullable(1)] Func<TSource, TResult> selector)
		{
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004270 File Offset: 0x00002470
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<TResult> CreateRange<TSource, TResult>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TSource> items, int start, int length, [Nullable(1)] Func<TSource, TResult> selector)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i + start]);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004300 File Offset: 0x00002500
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TSource> items, [Nullable(1)] Func<TSource, TArg, TResult> selector, [Nullable(1)] TArg arg)
		{
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			int length = items.Length;
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000435C File Offset: 0x0000255C
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<TResult> CreateRange<TSource, TArg, TResult>([Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TSource> items, int start, int length, [Nullable(1)] Func<TSource, TArg, TResult> selector, [Nullable(1)] TArg arg)
		{
			int length2 = items.Length;
			Requires.Range(start >= 0 && start <= length2, "start", null);
			Requires.Range(length >= 0 && start + length <= length2, "length", null);
			Requires.NotNull<Func<TSource, TArg, TResult>>(selector, "selector");
			if (length == 0)
			{
				return ImmutableArray.Create<TResult>();
			}
			TResult[] array = new TResult[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = selector(items[i + start], arg);
			}
			return new ImmutableArray<TResult>(array);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000043F0 File Offset: 0x000025F0
		public static ImmutableArray<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableArray.Create<T>().ToBuilder();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000440A File Offset: 0x0000260A
		public static ImmutableArray<T>.Builder CreateBuilder<[Nullable(2)] T>(int initialCapacity)
		{
			return new ImmutableArray<T>.Builder(initialCapacity);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004412 File Offset: 0x00002612
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<TSource> ToImmutableArray<[Nullable(2)] TSource>(this IEnumerable<TSource> items)
		{
			if (items is ImmutableArray<TSource>)
			{
				return (ImmutableArray<TSource>)items;
			}
			return ImmutableArray.CreateRange<TSource>(items);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004429 File Offset: 0x00002629
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<TSource> ToImmutableArray<[Nullable(2)] TSource>(this ImmutableArray<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000443C File Offset: 0x0000263C
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> array, T value)
		{
			return Array.BinarySearch<T>(array.array, value);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000444A File Offset: 0x0000264A
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> array, T value, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, value, comparer);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004459 File Offset: 0x00002659
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array.array, index, length, value);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004469 File Offset: 0x00002669
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> array, int index, int length, T value, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(array.array, index, length, value, comparer);
		}

		// Token: 0x04000029 RID: 41
		internal static readonly byte[] TwoElementArray = new byte[2];
	}
}
