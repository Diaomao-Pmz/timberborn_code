using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x0200008D RID: 141
	public interface IBinder
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000166 RID: 358
		IReadOnlyDictionary<Type, Binding> Bindings { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000167 RID: 359
		IReadOnlyDictionary<Type, IReadOnlyList<Binding>> MultiBindings { get; }

		// Token: 0x06000168 RID: 360
		void Bind(Type type, Binding binding);

		// Token: 0x06000169 RID: 361
		void MultiBind(Type type, Binding binding);

		// Token: 0x0600016A RID: 362
		bool TryGetBinding(Type type, out Binding binding);

		// Token: 0x0600016B RID: 363
		bool TryGetExportedBinding(Type type, out Binding binding);

		// Token: 0x0600016C RID: 364
		IEnumerable<Binding> GetMultiBindings(Type type);
	}
}
