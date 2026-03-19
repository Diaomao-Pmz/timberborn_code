using System;
using JetBrains.Annotations;

namespace Bindito.Core
{
	// Token: 0x02000070 RID: 112
	public interface IBindingBuilder<TBound> where TBound : class
	{
		// Token: 0x060000ED RID: 237
		IScopeAssignee To<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] TImplementation>() where TImplementation : class, TBound;

		// Token: 0x060000EE RID: 238
		IScopeAssignee ToProvider<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] TProvider>() where TProvider : IProvider<TBound>;

		// Token: 0x060000EF RID: 239
		IScopeAssignee ToProvider(IProvider<TBound> provider);

		// Token: 0x060000F0 RID: 240
		IScopeAssignee ToProvider(Func<TBound> provider);

		// Token: 0x060000F1 RID: 241
		IExportAssignee ToInstance(TBound instance);

		// Token: 0x060000F2 RID: 242
		IExportAssignee ToExisting<TExisting>() where TExisting : TBound;
	}
}
