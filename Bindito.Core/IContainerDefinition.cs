using System;
using JetBrains.Annotations;

namespace Bindito.Core
{
	// Token: 0x02000073 RID: 115
	public interface IContainerDefinition
	{
		// Token: 0x060000FC RID: 252
		ISingleBindingBuilder<T> Bind<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] T>() where T : class;

		// Token: 0x060000FD RID: 253
		IMultiBindingBuilder<T> MultiBind<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] T>() where T : class;

		// Token: 0x060000FE RID: 254
		void AddProvisionListener(IProvisionListener provisionListener);

		// Token: 0x060000FF RID: 255
		void AddInjectionListener(IInjectionListener injectionListener);

		// Token: 0x06000100 RID: 256
		void Install(IConfigurator configurator);

		// Token: 0x06000101 RID: 257
		void InstallAll(string contextName);
	}
}
