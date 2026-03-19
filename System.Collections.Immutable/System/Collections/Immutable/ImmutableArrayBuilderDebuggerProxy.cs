using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableArrayBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public ImmutableArrayBuilderDebuggerProxy(ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			this._builder = builder;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005BC2 File Offset: 0x00003DC2
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] A
		{
			get
			{
				return this._builder.ToArray();
			}
		}

		// Token: 0x0400002C RID: 44
		private readonly ImmutableArray<T>.Builder _builder;
	}
}
