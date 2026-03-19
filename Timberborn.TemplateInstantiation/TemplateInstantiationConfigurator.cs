using System;
using Bindito.Core;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class TemplateInstantiationConfigurator : Configurator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002176 File Offset: 0x00000376
		public override void Configure()
		{
			base.Bind<TemplateInstantiator>().ToProvider<TemplateInstantiatorProvider>().AsSingleton();
		}
	}
}
