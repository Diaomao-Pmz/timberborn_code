using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableQueue
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00007DAA File Offset: 0x00005FAA
		public static ImmutableQueue<T> Create<[Nullable(2)] T>()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007DB1 File Offset: 0x00005FB1
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableQueue<T>.Empty.Enqueue(item);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public static ImmutableQueue<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			T[] array = items as T[];
			if (array != null)
			{
				return ImmutableQueue.Create<T>(array);
			}
			ImmutableQueue<T> result;
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = ImmutableQueue<T>.Empty;
				}
				else
				{
					ImmutableStack<T> forwards = ImmutableStack.Create<T>(enumerator.Current);
					ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
					while (enumerator.MoveNext())
					{
						T value = enumerator.Current;
						immutableStack = immutableStack.Push(value);
					}
					result = new ImmutableQueue<T>(forwards, immutableStack);
				}
			}
			return result;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007E54 File Offset: 0x00006054
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableQueue.Create<T>(items);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00007E6C File Offset: 0x0000606C
		public unsafe static ImmutableQueue<T> Create<[Nullable(2)] T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			if (items.IsEmpty)
			{
				return ImmutableQueue<T>.Empty;
			}
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			for (int i = items.Length - 1; i >= 0; i--)
			{
				immutableStack = immutableStack.Push(*items[i]);
			}
			return new ImmutableQueue<T>(immutableStack, ImmutableStack<T>.Empty);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007EC1 File Offset: 0x000060C1
		public static IImmutableQueue<T> Dequeue<[Nullable(2)] T>(this IImmutableQueue<T> queue, out T value)
		{
			Requires.NotNull<IImmutableQueue<T>>(queue, "queue");
			value = queue.Peek();
			return queue.Dequeue();
		}
	}
}
