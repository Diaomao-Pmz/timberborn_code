using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[CollectionBuilder(typeof(ImmutableStack), "Create")]
	public interface IImmutableStack<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DC RID: 220
		bool IsEmpty { get; }

		// Token: 0x060000DD RID: 221
		IImmutableStack<T> Clear();

		// Token: 0x060000DE RID: 222
		IImmutableStack<T> Push(T value);

		// Token: 0x060000DF RID: 223
		IImmutableStack<T> Pop();

		// Token: 0x060000E0 RID: 224
		T Peek();
	}
}
