using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000030 RID: 48
	internal interface IStrongEnumerable<[Nullable(2)] out T, TEnumerator> where TEnumerator : struct, IStrongEnumerator<T>
	{
		// Token: 0x06000125 RID: 293
		TEnumerator GetEnumerator();
	}
}
