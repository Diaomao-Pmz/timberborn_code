using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableListBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00007D58 File Offset: 0x00005F58
		public ImmutableListBuilderDebuggerProxy(ImmutableList<T>.Builder builder)
		{
			Requires.NotNull<ImmutableList<T>.Builder>(builder, "builder");
			this._list = builder;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00007D74 File Offset: 0x00005F74
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				T[] result;
				if ((result = this._cachedContents) == null)
				{
					result = (this._cachedContents = this._list.ToArray(this._list.Count));
				}
				return result;
			}
		}

		// Token: 0x04000038 RID: 56
		private readonly ImmutableList<T>.Builder _list;

		// Token: 0x04000039 RID: 57
		private T[] _cachedContents;
	}
}
