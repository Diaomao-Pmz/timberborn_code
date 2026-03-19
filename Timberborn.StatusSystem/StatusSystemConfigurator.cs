using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.CameraSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000027 RID: 39
	[Context("Game")]
	[Context("MapEditor")]
	public class StatusSystemConfigurator : Configurator
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000048AC File Offset: 0x00002AAC
		public override void Configure()
		{
			base.Bind<StatusIconCycler>().AsTransient();
			base.Bind<StatusSlotOccupier>().AsTransient();
			base.Bind<StatusSubject>().AsTransient();
			base.Bind<StatusInstanceFactory>().AsSingleton();
			base.Bind<StatusIconMaterials>().AsSingleton();
			base.Bind<StatusSpriteLoader>().AsSingleton();
			base.Bind<StatusIconCyclerUpdater>().AsSingleton();
			base.Bind<IStatusIconOffsetService>().To<StatusIconOffsetService>().AsSingleton();
			base.Bind<StatusIconCyclerFactory>().AsSingleton();
			base.Bind<StatusAggregator>().AsSingleton();
			base.Bind<DynamicStatusAggregator>().AsSingleton();
			base.Bind<StatusSlotUpdateService>().AsSingleton();
			base.Bind<StatusSlotsUpdater>().AsSingleton();
			base.Bind<StatusIconSlotFactory>().AsSingleton();
			base.Bind<StatusIconOffsetCalculator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(StatusSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000498F File Offset: 0x00002B8F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<StatusIconCycler, FacingCamera>();
			builder.AddDecorator<BlockObject, StatusSlotOccupier>();
			return builder.Build();
		}
	}
}
