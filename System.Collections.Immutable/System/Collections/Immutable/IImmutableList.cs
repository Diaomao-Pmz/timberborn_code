using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[CollectionBuilder(typeof(ImmutableList), "Create")]
	public interface IImmutableList<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x060000A6 RID: 166
		IImmutableList<T> Clear();

		// Token: 0x060000A7 RID: 167
		int IndexOf(T item, int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer);

		// Token: 0x060000A8 RID: 168
		int LastIndexOf(T item, int index, int count, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer);

		// Token: 0x060000A9 RID: 169
		IImmutableList<T> Add(T value);

		// Token: 0x060000AA RID: 170
		IImmutableList<T> AddRange(IEnumerable<T> items);

		// Token: 0x060000AB RID: 171
		IImmutableList<T> Insert(int index, T element);

		// Token: 0x060000AC RID: 172
		IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

		// Token: 0x060000AD RID: 173
		IImmutableList<T> Remove(T value, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer);

		// Token: 0x060000AE RID: 174
		IImmutableList<T> RemoveAll(Predicate<T> match);

		// Token: 0x060000AF RID: 175
		IImmutableList<T> RemoveRange(IEnumerable<T> items, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer);

		// Token: 0x060000B0 RID: 176
		IImmutableList<T> RemoveRange(int index, int count);

		// Token: 0x060000B1 RID: 177
		IImmutableList<T> RemoveAt(int index);

		// Token: 0x060000B2 RID: 178
		IImmutableList<T> SetItem(int index, T value);

		// Token: 0x060000B3 RID: 179
		IImmutableList<T> Replace(T oldValue, T newValue, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer);
	}
}
