using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableArrayExtensions
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000024AD File Offset: 0x000006AD
		public static IEnumerable<TResult> Select<[Nullable(2)] T, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, TResult> selector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Select(selector);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024C4 File Offset: 0x000006C4
		public static IEnumerable<TResult> SelectMany<[Nullable(2)] TSource, [Nullable(2)] TCollection, [Nullable(2)] TResult>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (collectionSelector == null || resultSelector == null)
			{
				return immutableArray.SelectMany(collectionSelector, resultSelector);
			}
			if (immutableArray.Length != 0)
			{
				return immutableArray.SelectManyIterator(collectionSelector, resultSelector);
			}
			return Enumerable.Empty<TResult>();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002505 File Offset: 0x00000705
		public static IEnumerable<T> Where<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Where(predicate);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000251A File Offset: 0x0000071A
		[NullableContext(2)]
		public static bool Any<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			return immutableArray.Length > 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002528 File Offset: 0x00000728
		public static bool Any<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T arg in immutableArray.array)
			{
				if (predicate(arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002570 File Offset: 0x00000770
		public static bool All<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T arg in immutableArray.array)
			{
				if (!predicate(arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000025B8 File Offset: 0x000007B8
		[NullableContext(0)]
		public static bool SequenceEqual<TDerived, [Nullable(2)] TBase>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<TBase> immutableArray, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TDerived> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			for (int i = 0; i < immutableArray.Length; i++)
			{
				if (!comparer.Equals(immutableArray.array[i], (TBase)((object)items.array[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002640 File Offset: 0x00000840
		public static bool SequenceEqual<[Nullable(0)] TDerived, [Nullable(2)] TBase>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<TBase> immutableArray, IEnumerable<TDerived> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TBase> comparer = null) where TDerived : TBase
		{
			Requires.NotNull<IEnumerable<TDerived>>(items, "items");
			if (comparer == null)
			{
				comparer = EqualityComparer<TBase>.Default;
			}
			int num = 0;
			int length = immutableArray.Length;
			foreach (TDerived tderived in items)
			{
				if (num == length)
				{
					return false;
				}
				if (!comparer.Equals(immutableArray[num], (TBase)((object)tderived)))
				{
					return false;
				}
				num++;
			}
			return num == length;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026D4 File Offset: 0x000008D4
		public static bool SequenceEqual<[Nullable(0)] TDerived, [Nullable(2)] TBase>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<TBase> immutableArray, [Nullable(new byte[]
		{
			0,
			1
		})] ImmutableArray<TDerived> items, Func<TBase, TBase, bool> predicate) where TDerived : TBase
		{
			Requires.NotNull<Func<TBase, TBase, bool>>(predicate, "predicate");
			immutableArray.ThrowNullRefIfNotInitialized();
			items.ThrowNullRefIfNotInitialized();
			if (immutableArray.array == items.array)
			{
				return true;
			}
			if (immutableArray.Length != items.Length)
			{
				return false;
			}
			int i = 0;
			int length = immutableArray.Length;
			while (i < length)
			{
				if (!predicate(immutableArray[i], (TBase)((object)items[i])))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002758 File Offset: 0x00000958
		[NullableContext(2)]
		public static T Aggregate<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, T, T> func)
		{
			Requires.NotNull<Func<T, T, T>>(func, "func");
			if (immutableArray.Length == 0)
			{
				return default(T);
			}
			T t = immutableArray[0];
			int i = 1;
			int length = immutableArray.Length;
			while (i < length)
			{
				t = func(t, immutableArray[i]);
				i++;
			}
			return t;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027B4 File Offset: 0x000009B4
		public static TAccumulate Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
		{
			Requires.NotNull<Func<TAccumulate, T, TAccumulate>>(func, "func");
			TAccumulate taccumulate = seed;
			foreach (T arg in immutableArray.array)
			{
				taccumulate = func(taccumulate, arg);
			}
			return taccumulate;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000027F5 File Offset: 0x000009F5
		public static TResult Aggregate<[Nullable(2)] TAccumulate, [Nullable(2)] TResult, [Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			Requires.NotNull<Func<TAccumulate, TResult>>(resultSelector, "resultSelector");
			return resultSelector(immutableArray.Aggregate(seed, func));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002810 File Offset: 0x00000A10
		public static T ElementAt<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, int index)
		{
			return immutableArray[index];
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000281C File Offset: 0x00000A1C
		[NullableContext(2)]
		public static T ElementAtOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, int index)
		{
			if (index < 0 || index >= immutableArray.Length)
			{
				return default(T);
			}
			return immutableArray[index];
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000284C File Offset: 0x00000A4C
		public static T First<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return Enumerable.Empty<T>().First<T>();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002896 File Offset: 0x00000A96
		public static T First<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.First<T>();
			}
			return immutableArray[0];
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028B8 File Offset: 0x00000AB8
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.array.Length == 0)
			{
				return default(T);
			}
			return immutableArray.array[0];
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028E4 File Offset: 0x00000AE4
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000292D File Offset: 0x00000B2D
		public static T Last<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			if (immutableArray.Length <= 0)
			{
				return immutableArray.array.Last<T>();
			}
			return immutableArray[immutableArray.Length - 1];
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002958 File Offset: 0x00000B58
		public static T Last<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return Enumerable.Empty<T>().Last<T>();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029A7 File Offset: 0x00000BA7
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.LastOrDefault<T>();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029BC File Offset: 0x00000BBC
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				if (predicate(immutableArray[i]))
				{
					return immutableArray[i];
				}
			}
			return default(T);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A0A File Offset: 0x00000C0A
		public static T Single<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.Single<T>();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A20 File Offset: 0x00000C20
		public static T Single<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T result = default(T);
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					if (!flag)
					{
						ImmutableArray.TwoElementArray.Single<byte>();
					}
					flag = false;
					result = t;
				}
			}
			if (flag)
			{
				Enumerable.Empty<T>().Single<T>();
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A8C File Offset: 0x00000C8C
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			return immutableArray.array.SingleOrDefault<T>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AA0 File Offset: 0x00000CA0
		[NullableContext(2)]
		public static T SingleOrDefault<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, [Nullable(1)] Func<T, bool> predicate)
		{
			Requires.NotNull<Func<T, bool>>(predicate, "predicate");
			bool flag = true;
			T result = default(T);
			foreach (T t in immutableArray.array)
			{
				if (predicate(t))
				{
					if (!flag)
					{
						ImmutableArray.TwoElementArray.Single<byte>();
					}
					flag = false;
					result = t;
				}
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AFE File Offset: 0x00000CFE
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector)
		{
			return immutableArray.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B0C File Offset: 0x00000D0C
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
		{
			return immutableArray.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B1C File Offset: 0x00000D1C
		public static Dictionary<TKey, T> ToDictionary<TKey, [Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Dictionary<TKey, T> dictionary = new Dictionary<TKey, T>(immutableArray.Length, comparer);
			foreach (T t in immutableArray)
			{
				dictionary.Add(keySelector(t), t);
			}
			return dictionary;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B6C File Offset: 0x00000D6C
		public static Dictionary<TKey, TElement> ToDictionary<TKey, [Nullable(2)] TElement, [Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray, Func<T, TKey> keySelector, Func<T, TElement> elementSelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer)
		{
			Requires.NotNull<Func<T, TKey>>(keySelector, "keySelector");
			Requires.NotNull<Func<T, TElement>>(elementSelector, "elementSelector");
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(immutableArray.Length, comparer);
			foreach (T arg in immutableArray.array)
			{
				dictionary.Add(keySelector(arg), elementSelector(arg));
			}
			return dictionary;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BCF File Offset: 0x00000DCF
		public static T[] ToArray<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ImmutableArray<T> immutableArray)
		{
			immutableArray.ThrowNullRefIfNotInitialized();
			if (immutableArray.array.Length == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			return (T[])immutableArray.array.Clone();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BFC File Offset: 0x00000DFC
		public static T First<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[0];
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C20 File Offset: 0x00000E20
		[NullableContext(2)]
		public static T FirstOrDefault<T>([Nullable(1)] this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[0];
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C51 File Offset: 0x00000E51
		public static T Last<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				throw new InvalidOperationException();
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C7C File Offset: 0x00000E7C
		[NullableContext(2)]
		public static T LastOrDefault<T>([Nullable(1)] this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			if (!builder.Any<T>())
			{
				return default(T);
			}
			return builder[builder.Count - 1];
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public static bool Any<[Nullable(2)] T>(this ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			return builder.Count > 0;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002CCA File Offset: 0x00000ECA
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(this ImmutableArray<TSource> immutableArray, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			foreach (TSource item in immutableArray.array)
			{
				foreach (TCollection arg in collectionSelector(item))
				{
					yield return resultSelector(item, arg);
				}
				IEnumerator<TCollection> enumerator = null;
				item = default(TSource);
			}
			TSource[] array = null;
			yield break;
			yield break;
		}
	}
}
