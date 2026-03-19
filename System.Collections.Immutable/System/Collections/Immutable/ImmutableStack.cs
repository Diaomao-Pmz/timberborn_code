using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableStack
	{
		// Token: 0x0600038F RID: 911 RVA: 0x000097A6 File Offset: 0x000079A6
		public static ImmutableStack<T> Create<[Nullable(2)] T>()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000097AD File Offset: 0x000079AD
		public static ImmutableStack<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableStack<T>.Empty.Push(item);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000097BC File Offset: 0x000079BC
		public static ImmutableStack<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T value in items)
			{
				immutableStack = immutableStack.Push(value);
			}
			return immutableStack;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009818 File Offset: 0x00007A18
		public static ImmutableStack<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			return ImmutableStack.Create<T>(items);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00009830 File Offset: 0x00007A30
		public unsafe static ImmutableStack<T> Create<[Nullable(2)] T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> items)
		{
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			ReadOnlySpan<T> readOnlySpan = items;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T value = *readOnlySpan[i];
				immutableStack = immutableStack.Push(value);
			}
			return immutableStack;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000986E File Offset: 0x00007A6E
		public static IImmutableStack<T> Pop<[Nullable(2)] T>(this IImmutableStack<T> stack, out T value)
		{
			Requires.NotNull<IImmutableStack<T>>(stack, "stack");
			value = stack.Peek();
			return stack.Pop();
		}
	}
}
