using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableEnumerableDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00006916 File Offset: 0x00004B16
		public ImmutableEnumerableDebuggerProxy(IEnumerable<T> enumerable)
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006930 File Offset: 0x00004B30
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				T[] result;
				if ((result = this._cachedContents) == null)
				{
					result = (this._cachedContents = this._enumerable.ToArray<T>());
				}
				return result;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly IEnumerable<T> _enumerable;

		// Token: 0x04000035 RID: 53
		private T[] _cachedContents;
	}
}
