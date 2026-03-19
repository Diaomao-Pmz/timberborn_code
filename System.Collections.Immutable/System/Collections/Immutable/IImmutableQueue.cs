using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[CollectionBuilder(typeof(ImmutableQueue), "Create")]
	public interface IImmutableQueue<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C8 RID: 200
		bool IsEmpty { get; }

		// Token: 0x060000C9 RID: 201
		IImmutableQueue<T> Clear();

		// Token: 0x060000CA RID: 202
		T Peek();

		// Token: 0x060000CB RID: 203
		IImmutableQueue<T> Enqueue(T value);

		// Token: 0x060000CC RID: 204
		IImmutableQueue<T> Dequeue();
	}
}
