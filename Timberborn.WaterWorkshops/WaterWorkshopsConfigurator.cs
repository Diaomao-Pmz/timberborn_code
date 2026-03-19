using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WaterWorkshops
{
	// Token: 0x02000014 RID: 20
	[Context("Game")]
	public class WaterWorkshopsConfigurator : Configurator
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002934 File Offset: 0x00000B34
		public override void Configure()
		{
			base.Bind<ManufactoryWaterConsumer>().AsTransient();
			base.Bind<ManufactoryWaterContaminationConsumer>().AsTransient();
			base.Bind<ManufactoryWaterContaminationProducer>().AsTransient();
			base.Bind<ManufactoryWaterProducer>().AsTransient();
			base.Bind<WaterInputContaminationManufactoryLimiter>().AsTransient();
			base.Bind<WaterInputManufactoryLimiter>().AsTransient();
			base.Bind<WaterOutputManufactoryLimiter>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WaterWorkshopsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000029B2 File Offset: 0x00000BB2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ManufactoryWaterConsumerSpec, ManufactoryWaterConsumer>();
			builder.AddDecorator<ManufactoryWaterConsumer, WaterInputManufactoryLimiter>();
			builder.AddDecorator<ManufactoryWaterContaminationConsumerSpec, ManufactoryWaterContaminationConsumer>();
			builder.AddDecorator<ManufactoryWaterContaminationConsumer, WaterInputContaminationManufactoryLimiter>();
			builder.AddDecorator<ManufactoryWaterProducerSpec, ManufactoryWaterProducer>();
			builder.AddDecorator<ManufactoryWaterProducer, WaterOutputManufactoryLimiter>();
			builder.AddDecorator<ManufactoryWaterContaminationProducerSpec, ManufactoryWaterContaminationProducer>();
			return builder.Build();
		}
	}
}
