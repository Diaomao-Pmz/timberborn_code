using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableSortedSetBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x0600038D RID: 909 RVA: 0x00009774 File Offset: 0x00007974
		public ImmutableSortedSetBuilderDebuggerProxy(ImmutableSortedSet<T>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, "builder");
			this._set = builder;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000978E File Offset: 0x0000798E
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				return this._set.ToArray(this._set.Count);
			}
		}

		// Token: 0x04000047 RID: 71
		private readonly ImmutableSortedSet<T>.Builder _set;
	}
}
