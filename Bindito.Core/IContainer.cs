using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Bindito.Core
{
	// Token: 0x02000072 RID: 114
	public interface IContainer
	{
		// Token: 0x060000F4 RID: 244
		T GetInstance<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] T>();

		// Token: 0x060000F5 RID: 245
		IEnumerable<T> GetInstances<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] T>();

		// Token: 0x060000F6 RID: 246
		object GetInstance(Type type);

		// Token: 0x060000F7 RID: 247
		IEnumerable<object> GetInstances(Type type);

		// Token: 0x060000F8 RID: 248
		IEnumerable<object> GetBoundInstances();

		// Token: 0x060000F9 RID: 249
		void Inject(object instance);

		// Token: 0x060000FA RID: 250
		IContainer CreateChildContainer(IEnumerable<IConfigurator> configurators);

		// Token: 0x060000FB RID: 251
		IContainer CreateChildContainer(params IConfigurator[] configurators);
	}
}
