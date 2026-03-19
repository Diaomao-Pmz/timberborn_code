using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ModelHiding
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	[Context("MapEditor")]
	public class ModelHidingConfigurator : Configurator
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002A44 File Offset: 0x00000C44
		public override void Configure()
		{
			base.Bind<HidabilityPositionUpdater>().AsTransient();
			base.Bind<ModelHider>().AsSingleton();
			base.Bind<HidableModels>().AsSingleton();
			base.Bind<UndergroundModelHider>().AsSingleton();
			base.Bind<FloorModelHider>().AsSingleton();
			base.Bind<UncoveredModelHider>().AsSingleton();
			base.Bind<IModelAdder>().ToExisting<ModelHider>();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ModelHidingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<HidabilityPositionUpdaterSpec, HidabilityPositionUpdater>();
			return builder.Build();
		}
	}
}
