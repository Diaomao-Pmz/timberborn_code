using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	[Context("MapEditor")]
	public class GoodStackSystemConfigurator : Configurator
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B74 File Offset: 0x00000D74
		public override void Configure()
		{
			base.Bind<GoodStackRetrieverBehavior>().AsTransient();
			base.Bind<GoodStack>().AsTransient();
			base.Bind<GoodStackAccessible>().AsTransient();
			base.Bind<GoodStackModel>().AsTransient();
			base.Bind<GoodStackInventoryInitializer>().AsSingleton();
			base.Bind<GoodStackModelFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<GoodStackSystemTemplateModuleProvider>().AsSingleton();
		}
	}
}
