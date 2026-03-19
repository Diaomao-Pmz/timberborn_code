using System;
using Bindito.Core;

namespace Timberborn.TemplateCollectionSystem
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class TemplateCollectionSystemConfigurator : Configurator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000024D5 File Offset: 0x000006D5
		public override void Configure()
		{
			base.Bind<TemplateCollectionService>().AsSingleton();
			base.MultiBind<ITemplateCollectionIdProvider>().To<CommonTemplateCollectionIdProvider>().AsSingleton();
		}
	}
}
