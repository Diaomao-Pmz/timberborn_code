using System;
using Bindito.Core;
using Timberborn.BlockingSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterObjectsConfigurator : Configurator
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002C18 File Offset: 0x00000E18
		public override void Configure()
		{
			base.Bind<FinishableHorizontalWaterObstacle>().AsTransient();
			base.Bind<FinishableWaterObstacle>().AsTransient();
			base.Bind<HorizontalWaterObstacle>().AsTransient();
			base.Bind<WaterObject>().AsTransient();
			base.Bind<WaterObstacle>().AsTransient();
			base.Bind<FloodableObject>().AsTransient();
			base.Bind<BlockableFloodableObject>().AsTransient();
			base.Bind<WaterObjectService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterObjectsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CA2 File Offset: 0x00000EA2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<FinishableHorizontalWaterObstacleSpec, FinishableHorizontalWaterObstacle>();
			builder.AddDecorator<FinishableHorizontalWaterObstacle, HorizontalWaterObstacle>();
			builder.AddDecorator<FinishableWaterObstacleSpec, FinishableWaterObstacle>();
			builder.AddDecorator<IWaterObjectSpecification, WaterObject>();
			builder.AddDecorator<WaterObstacleSpec, WaterObstacle>();
			builder.AddDecorator<FloodableObjectSpec, FloodableObject>();
			builder.AddDecorator<BlockableFloodableObjectSpec, BlockableFloodableObject>();
			builder.AddDecorator<BlockableFloodableObject, BlockableObject>();
			return builder.Build();
		}
	}
}
