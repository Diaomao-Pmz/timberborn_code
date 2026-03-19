using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000088 RID: 136
	[NullableContext(1)]
	internal interface IReadOnlySet<[Nullable(2)] T> : IReadOnlyCollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600057F RID: 1407
		bool Contains(T item);

		// Token: 0x06000580 RID: 1408
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x06000581 RID: 1409
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x06000582 RID: 1410
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x06000583 RID: 1411
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x06000584 RID: 1412
		bool Overlaps(IEnumerable<T> other);

		// Token: 0x06000585 RID: 1413
		bool SetEquals(IEnumerable<T> other);
	}
}
