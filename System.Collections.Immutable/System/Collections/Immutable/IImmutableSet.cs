using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[CollectionBuilder(typeof(ImmutableHashSet), "Create")]
	public interface IImmutableSet<[Nullable(2)] T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000CD RID: 205
		IImmutableSet<T> Clear();

		// Token: 0x060000CE RID: 206
		bool Contains(T value);

		// Token: 0x060000CF RID: 207
		IImmutableSet<T> Add(T value);

		// Token: 0x060000D0 RID: 208
		IImmutableSet<T> Remove(T value);

		// Token: 0x060000D1 RID: 209
		bool TryGetValue(T equalValue, out T actualValue);

		// Token: 0x060000D2 RID: 210
		IImmutableSet<T> Intersect(IEnumerable<T> other);

		// Token: 0x060000D3 RID: 211
		IImmutableSet<T> Except(IEnumerable<T> other);

		// Token: 0x060000D4 RID: 212
		IImmutableSet<T> SymmetricExcept(IEnumerable<T> other);

		// Token: 0x060000D5 RID: 213
		IImmutableSet<T> Union(IEnumerable<T> other);

		// Token: 0x060000D6 RID: 214
		bool SetEquals(IEnumerable<T> other);

		// Token: 0x060000D7 RID: 215
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x060000D8 RID: 216
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x060000D9 RID: 217
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x060000DA RID: 218
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x060000DB RID: 219
		bool Overlaps(IEnumerable<T> other);
	}
}
