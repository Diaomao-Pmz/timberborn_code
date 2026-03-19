using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AllocFreeConcurrentStack<[Nullable(2)] T>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002F14 File Offset: 0x00001114
		public static void TryAdd(T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack.Count < 35)
			{
				threadLocalStack.Push(new RefAsValueType<T>(item));
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002F40 File Offset: 0x00001140
		public static bool TryTake([MaybeNullWhen(false)] out T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack != null && threadLocalStack.Count > 0)
			{
				item = threadLocalStack.Pop().Value;
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F7C File Offset: 0x0000117C
		[Nullable(new byte[]
		{
			1,
			0,
			1
		})]
		private static Stack<RefAsValueType<T>> ThreadLocalStack
		{
			get
			{
				Dictionary<Type, object> dictionary;
				if ((dictionary = AllocFreeConcurrentStack.t_stacks) == null)
				{
					dictionary = (AllocFreeConcurrentStack.t_stacks = new Dictionary<Type, object>());
				}
				Dictionary<Type, object> dictionary2 = dictionary;
				object obj;
				if (!dictionary2.TryGetValue(AllocFreeConcurrentStack<T>.s_typeOfT, out obj))
				{
					obj = new Stack<RefAsValueType<T>>(35);
					dictionary2.Add(AllocFreeConcurrentStack<T>.s_typeOfT, obj);
				}
				return (Stack<RefAsValueType<T>>)obj;
			}
		}

		// Token: 0x0400001D RID: 29
		private const int MaxSize = 35;

		// Token: 0x0400001E RID: 30
		private static readonly Type s_typeOfT = typeof(T);
	}
}
