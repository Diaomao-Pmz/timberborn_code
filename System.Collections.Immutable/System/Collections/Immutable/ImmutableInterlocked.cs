using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableInterlocked
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00006CDC File Offset: 0x00004EDC
		public static bool Update<[Nullable(2)] T>(ref T location, Func<T, T> transformer) where T : class
		{
			Requires.NotNull<Func<T, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006D34 File Offset: 0x00004F34
		public static bool Update<[Nullable(2)] T, [Nullable(2)] TArg>(ref T location, Func<T, TArg, T> transformer, TArg transformerArgument) where T : class
		{
			Requires.NotNull<Func<T, TArg, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t, transformerArgument);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006D8C File Offset: 0x00004F8C
		[NullableContext(2)]
		public static bool Update<T>([Nullable(new byte[]
		{
			0,
			1
		})] ref ImmutableArray<T> location, [Nullable(new byte[]
		{
			1,
			0,
			1,
			0,
			1
		})] Func<ImmutableArray<T>, ImmutableArray<T>> transformer)
		{
			Requires.NotNull<Func<ImmutableArray<T>, ImmutableArray<T>>>(transformer, "transformer");
			T[] array = Volatile.Read<T[]>(Unsafe.AsRef<T[]>(location.array));
			for (;;)
			{
				ImmutableArray<T> immutableArray = transformer(new ImmutableArray<T>(array));
				if (array == immutableArray.array)
				{
					break;
				}
				T[] array2 = Interlocked.CompareExchange<T[]>(Unsafe.AsRef<T[]>(location.array), immutableArray.array, array);
				bool flag = array == array2;
				array = array2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006DF0 File Offset: 0x00004FF0
		[NullableContext(2)]
		public static bool Update<T, TArg>([Nullable(new byte[]
		{
			0,
			1
		})] ref ImmutableArray<T> location, [Nullable(new byte[]
		{
			1,
			0,
			1,
			1,
			0,
			1
		})] Func<ImmutableArray<T>, TArg, ImmutableArray<T>> transformer, [Nullable(1)] TArg transformerArgument)
		{
			Requires.NotNull<Func<ImmutableArray<T>, TArg, ImmutableArray<T>>>(transformer, "transformer");
			T[] array = Volatile.Read<T[]>(Unsafe.AsRef<T[]>(location.array));
			for (;;)
			{
				ImmutableArray<T> immutableArray = transformer(new ImmutableArray<T>(array), transformerArgument);
				if (array == immutableArray.array)
				{
					break;
				}
				T[] array2 = Interlocked.CompareExchange<T[]>(Unsafe.AsRef<T[]>(location.array), immutableArray.array, array);
				bool flag = array == array2;
				array = array2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006E55 File Offset: 0x00005055
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> InterlockedExchange<T>([Nullable(new byte[]
		{
			0,
			1
		})] ref ImmutableArray<T> location, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> value)
		{
			return new ImmutableArray<T>(Interlocked.Exchange<T[]>(Unsafe.AsRef<T[]>(location.array), value.array));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006E72 File Offset: 0x00005072
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ImmutableArray<T> InterlockedCompareExchange<T>([Nullable(new byte[]
		{
			0,
			1
		})] ref ImmutableArray<T> location, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> value, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> comparand)
		{
			return new ImmutableArray<T>(Interlocked.CompareExchange<T[]>(Unsafe.AsRef<T[]>(location.array), value.array, comparand.array));
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006E98 File Offset: 0x00005098
		[NullableContext(2)]
		public static bool InterlockedInitialize<T>([Nullable(new byte[]
		{
			0,
			1
		})] ref ImmutableArray<T> location, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<T> value)
		{
			return ImmutableInterlocked.InterlockedCompareExchange<T>(ref location, value, default(ImmutableArray<T>)).IsDefault;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006EC0 File Offset: 0x000050C0
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue, [Nullable(2)] TArg>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			Requires.NotNull<Func<TKey, TArg, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key, factoryArgument);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006F08 File Offset: 0x00005108
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> valueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006F50 File Offset: 0x00005150
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue result;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.TryGetValue(key, out result))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return value;
				}
			}
			return result;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006F9C File Offset: 0x0000519C
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(addValueFactory, "addValueFactory");
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue2;
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
				}
				else
				{
					tvalue2 = addValueFactory(key);
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, tvalue2);
				if (immutableDictionary == immutableDictionary2)
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return tvalue2;
				}
			}
			return tvalue;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007018 File Offset: 0x00005218
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue2;
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
				}
				else
				{
					tvalue2 = addValue;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, tvalue2);
				if (immutableDictionary == immutableDictionary2)
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return tvalue2;
				}
			}
			return tvalue;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007084 File Offset: 0x00005284
		public static bool TryAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.ContainsKey(key))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000070CC File Offset: 0x000052CC
		public static bool TryUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue newValue, TValue comparisonValue)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue x;
				if (!immutableDictionary.TryGetValue(key, out x) || !@default.Equals(x, comparisonValue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, newValue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000712C File Offset: 0x0000532C
		public static bool TryRemove<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (!immutableDictionary.TryGetValue(key, out value))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Remove(key);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007174 File Offset: 0x00005374
		public static bool TryPop<[Nullable(2)] T>(ref ImmutableStack<T> location, [MaybeNullWhen(false)] out T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				if (immutableStack.IsEmpty)
				{
					break;
				}
				ImmutableStack<T> value2 = immutableStack.Pop(out value);
				ImmutableStack<T> immutableStack2 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, value2, immutableStack);
				bool flag = immutableStack == immutableStack2;
				immutableStack = immutableStack2;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000071C0 File Offset: 0x000053C0
		public static void Push<[Nullable(2)] T>(ref ImmutableStack<T> location, T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				ImmutableStack<T> value2 = immutableStack.Push(value);
				ImmutableStack<T> immutableStack2 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, value2, immutableStack);
				flag = (immutableStack == immutableStack2);
				immutableStack = immutableStack2;
			}
			while (!flag);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000071FC File Offset: 0x000053FC
		public static bool TryDequeue<[Nullable(2)] T>(ref ImmutableQueue<T> location, [MaybeNullWhen(false)] out T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				if (immutableQueue.IsEmpty)
				{
					break;
				}
				ImmutableQueue<T> value2 = immutableQueue.Dequeue(out value);
				ImmutableQueue<T> immutableQueue2 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, value2, immutableQueue);
				bool flag = immutableQueue == immutableQueue2;
				immutableQueue = immutableQueue2;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007248 File Offset: 0x00005448
		public static void Enqueue<[Nullable(2)] T>(ref ImmutableQueue<T> location, T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				ImmutableQueue<T> value2 = immutableQueue.Enqueue(value);
				ImmutableQueue<T> immutableQueue2 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, value2, immutableQueue);
				flag = (immutableQueue == immutableQueue2);
				immutableQueue = immutableQueue2;
			}
			while (!flag);
		}
	}
}
