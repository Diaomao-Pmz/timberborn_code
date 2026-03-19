using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	internal interface IImmutableListQueries<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x060000B4 RID: 180
		ImmutableList<TOutput> ConvertAll<[Nullable(2)] TOutput>(Func<T, TOutput> converter);

		// Token: 0x060000B5 RID: 181
		void ForEach(Action<T> action);

		// Token: 0x060000B6 RID: 182
		ImmutableList<T> GetRange(int index, int count);

		// Token: 0x060000B7 RID: 183
		void CopyTo(T[] array);

		// Token: 0x060000B8 RID: 184
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x060000B9 RID: 185
		void CopyTo(int index, T[] array, int arrayIndex, int count);

		// Token: 0x060000BA RID: 186
		bool Exists(Predicate<T> match);

		// Token: 0x060000BB RID: 187
		[return: Nullable(2)]
		T Find(Predicate<T> match);

		// Token: 0x060000BC RID: 188
		ImmutableList<T> FindAll(Predicate<T> match);

		// Token: 0x060000BD RID: 189
		int FindIndex(Predicate<T> match);

		// Token: 0x060000BE RID: 190
		int FindIndex(int startIndex, Predicate<T> match);

		// Token: 0x060000BF RID: 191
		int FindIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x060000C0 RID: 192
		[return: Nullable(2)]
		T FindLast(Predicate<T> match);

		// Token: 0x060000C1 RID: 193
		int FindLastIndex(Predicate<T> match);

		// Token: 0x060000C2 RID: 194
		int FindLastIndex(int startIndex, Predicate<T> match);

		// Token: 0x060000C3 RID: 195
		int FindLastIndex(int startIndex, int count, Predicate<T> match);

		// Token: 0x060000C4 RID: 196
		bool TrueForAll(Predicate<T> match);

		// Token: 0x060000C5 RID: 197
		int BinarySearch(T item);

		// Token: 0x060000C6 RID: 198
		int BinarySearch(T item, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer);

		// Token: 0x060000C7 RID: 199
		int BinarySearch(int index, int count, T item, [Nullable(new byte[]
		{
			2,
			1
		})] IComparer<T> comparer);
	}
}
