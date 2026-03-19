using System;
using Bindito.Core;

namespace Timberborn.BaseComponentSystem
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	[Context("MapEditor")]
	public class BaseComponentSystemConfigurator : Configurator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022EB File Offset: 0x000004EB
		public override void Configure()
		{
			base.Bind<BaseInstantiator>().AsSingleton();
			base.Bind<ComponentCacheService>().AsSingleton();
			base.Bind<TypeBlacklist>().AsSingleton();
		}
	}
}
