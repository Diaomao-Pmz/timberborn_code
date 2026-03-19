using System;
using JetBrains.Annotations;

namespace Bindito.Core
{
	// Token: 0x0200006E RID: 110
	[UsedImplicitly]
	public abstract class Configurator : IConfigurator
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000295D File Offset: 0x00000B5D
		public void Configure(IContainerDefinition containerDefinition)
		{
			this._containerDefinition = containerDefinition;
			this.Configure();
			this._containerDefinition = null;
		}

		// Token: 0x060000E3 RID: 227
		protected abstract void Configure();

		// Token: 0x060000E4 RID: 228 RVA: 0x00002973 File Offset: 0x00000B73
		[UsedImplicitly]
		protected ISingleBindingBuilder<T> Bind<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] T>() where T : class
		{
			return this._containerDefinition.Bind<T>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002980 File Offset: 0x00000B80
		[UsedImplicitly]
		protected IMultiBindingBuilder<T> MultiBind<[MeansImplicitUse(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)] T>() where T : class
		{
			return this._containerDefinition.MultiBind<T>();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000298D File Offset: 0x00000B8D
		[UsedImplicitly]
		protected void AddProvisionListener(IProvisionListener provisionListener)
		{
			this._containerDefinition.AddProvisionListener(provisionListener);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000299B File Offset: 0x00000B9B
		[UsedImplicitly]
		protected void AddInjectionListener(IInjectionListener injectionListener)
		{
			this._containerDefinition.AddInjectionListener(injectionListener);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000029A9 File Offset: 0x00000BA9
		[UsedImplicitly]
		protected void Install(IConfigurator configurator)
		{
			this._containerDefinition.Install(configurator);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000029B7 File Offset: 0x00000BB7
		[UsedImplicitly]
		protected void InstallAll(string contextName)
		{
			this._containerDefinition.InstallAll(contextName);
		}

		// Token: 0x04000068 RID: 104
		private IContainerDefinition _containerDefinition;
	}
}
