using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.WaterSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x02000016 RID: 22
	[Context("Game")]
	[Context("MapEditor")]
	public class SoilMoistureSystemConfigurator : Configurator
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004500 File Offset: 0x00002700
		public override void Configure()
		{
			base.Bind<DryObject>().AsTransient();
			base.Bind<DryObjectModel>().AsTransient();
			base.Bind<SoilMoistureSimulator>().AsSingleton();
			base.Bind<ISoilMoistureService>().To<SoilMoistureService>().AsSingleton();
			base.Bind<SoilMoistureSimulationTaskStarter>().AsSingleton();
			base.Bind<WaterEvaporationMap>().AsSingleton();
			base.Bind<WaterEvaporationMapHelper>().AsSingleton();
			base.Bind<IThreadSafeWaterEvaporationMap>().ToExisting<WaterEvaporationMap>();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(SoilMoistureSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000458F File Offset: 0x0000278F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DryObjectModelSpec, DryObjectModel>();
			builder.AddDecorator<DryObjectSpec, DryObject>();
			return builder.Build();
		}
	}
}
