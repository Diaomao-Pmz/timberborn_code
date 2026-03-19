using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableExtensions
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000695C File Offset: 0x00004B5C
		[NullableContext(2)]
		internal static bool IsValueType<T>()
		{
			if (default(T) != null)
			{
				return true;
			}
			Type typeFromHandle = typeof(T);
			return typeFromHandle.IsConstructedGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000069A8 File Offset: 0x00004BA8
		internal static IOrderedCollection<T> AsOrderedCollection<[Nullable(2)] T>(this IEnumerable<T> sequence)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			IOrderedCollection<T> orderedCollection = sequence as IOrderedCollection<T>;
			if (orderedCollection != null)
			{
				return orderedCollection;
			}
			IList<T> list = sequence as IList<T>;
			if (list != null)
			{
				return new ImmutableExtensions.ListOfTWrapper<T>(list);
			}
			return new ImmutableExtensions.FallbackWrapper<T>(sequence);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000069E3 File Offset: 0x00004BE3
		internal static void ClearFastWhenEmpty<[Nullable(2)] T>(this Stack<T> stack)
		{
			if (stack.Count > 0)
			{
				stack.Clear();
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000069F4 File Offset: 0x00004BF4
		[return: Nullable(new byte[]
		{
			0,
			1,
			0
		})]
		internal static DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerableDisposable<[Nullable(2)] T, [Nullable(0)] TEnumerator>(this IEnumerable<T> enumerable) where TEnumerator : struct, IStrongEnumerator<T>, IEnumerator<T>
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			IStrongEnumerable<T, TEnumerator> strongEnumerable = enumerable as IStrongEnumerable<T, TEnumerator>;
			if (strongEnumerable != null)
			{
				return new DisposableEnumeratorAdapter<T, TEnumerator>(strongEnumerable.GetEnumerator());
			}
			return new DisposableEnumeratorAdapter<T, TEnumerator>(enumerable.GetEnumerator());
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006A2D File Offset: 0x00004C2D
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable<T> sequence, out int count)
		{
			return sequence.TryGetCount(out count);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006A38 File Offset: 0x00004C38
		internal static bool TryGetCount<[Nullable(2)] T>(this IEnumerable sequence, out int count)
		{
			ICollection collection = sequence as ICollection;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			ICollection<T> collection2 = sequence as ICollection<T>;
			if (collection2 != null)
			{
				count = collection2.Count;
				return true;
			}
			IReadOnlyCollection<T> readOnlyCollection = sequence as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				count = readOnlyCollection.Count;
				return true;
			}
			count = 0;
			return false;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006A88 File Offset: 0x00004C88
		internal static int GetCount<[Nullable(2)] T>(ref IEnumerable<T> sequence)
		{
			int count;
			if (!sequence.TryGetCount(out count))
			{
				List<T> list = sequence.ToList<T>();
				count = list.Count;
				sequence = list;
			}
			return count;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006AB4 File Offset: 0x00004CB4
		internal static bool TryCopyTo<[Nullable(2)] T>(this IEnumerable<T> sequence, T[] array, int arrayIndex)
		{
			if (sequence is IList<T>)
			{
				List<T> list = sequence as List<T>;
				if (list != null)
				{
					list.CopyTo(array, arrayIndex);
					return true;
				}
				if (sequence.GetType() == typeof(T[]))
				{
					T[] array2 = (T[])sequence;
					Array.Copy(array2, 0, array, arrayIndex, array2.Length);
					return true;
				}
				if (sequence is ImmutableArray<T>)
				{
					ImmutableArray<T> immutableArray = (ImmutableArray<T>)sequence;
					Array.Copy(immutableArray.array, 0, array, arrayIndex, immutableArray.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006B30 File Offset: 0x00004D30
		internal static T[] ToArray<[Nullable(2)] T>(this IEnumerable<T> sequence, int count)
		{
			Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
			Requires.Range(count >= 0, "count", null);
			if (count == 0)
			{
				return ImmutableArray<T>.Empty.array;
			}
			T[] array = new T[count];
			if (!sequence.TryCopyTo(array, 0))
			{
				int num = 0;
				foreach (T t in sequence)
				{
					Requires.Argument(num < count);
					array[num++] = t;
				}
				Requires.Argument(num == count);
			}
			return array;
		}

		// Token: 0x020000AE RID: 174
		private sealed class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060006B7 RID: 1719 RVA: 0x000113E7 File Offset: 0x0000F5E7
			internal ListOfTWrapper(IList<T> collection)
			{
				Requires.NotNull<IList<T>>(collection, "collection");
				this._collection = collection;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00011401 File Offset: 0x0000F601
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17000145 RID: 325
			public T this[int index]
			{
				get
				{
					return this._collection[index];
				}
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x0001141C File Offset: 0x0000F61C
			public IEnumerator<T> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x00011429 File Offset: 0x0000F629
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000100 RID: 256
			private readonly IList<T> _collection;
		}

		// Token: 0x020000AF RID: 175
		private sealed class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<!0>, IEnumerable
		{
			// Token: 0x060006BC RID: 1724 RVA: 0x00011431 File Offset: 0x0000F631
			internal FallbackWrapper(IEnumerable<T> sequence)
			{
				Requires.NotNull<IEnumerable<T>>(sequence, "sequence");
				this._sequence = sequence;
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001144C File Offset: 0x0000F64C
			public int Count
			{
				get
				{
					if (this._collection == null)
					{
						int result;
						if (this._sequence.TryGetCount(out result))
						{
							return result;
						}
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection.Count;
				}
			}

			// Token: 0x17000147 RID: 327
			public T this[int index]
			{
				get
				{
					if (this._collection == null)
					{
						this._collection = this._sequence.ToArray<T>();
					}
					return this._collection[index];
				}
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x000114B5 File Offset: 0x0000F6B5
			public IEnumerator<T> GetEnumerator()
			{
				return this._sequence.GetEnumerator();
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x000114C2 File Offset: 0x0000F6C2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000101 RID: 257
			private readonly IEnumerable<T> _sequence;

			// Token: 0x04000102 RID: 258
			private IList<T> _collection;
		}
	}
}
