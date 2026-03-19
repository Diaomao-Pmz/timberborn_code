using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000022 RID: 34
	internal static class AllocFreeConcurrentStack
	{
		// Token: 0x0400001F RID: 31
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		[ThreadStatic]
		internal static Dictionary<Type, object> t_stacks;
	}
}
