using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200001F RID: 31
	[Context("Game")]
	[Context("MapEditor")]
	public class MechanicalSystemConfigurator : Configurator
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004248 File Offset: 0x00002448
		public override void Configure()
		{
			base.Bind<MechanicalBuilding>().AsTransient();
			base.Bind<MechanicalNodeParticlesController>().AsTransient();
			base.Bind<MechanicalNode>().AsTransient();
			base.Bind<MechanicalNodeActuals>().AsTransient();
			base.Bind<TransputProvider>().AsTransient();
			base.Bind<MechanicalNodeTransformHeight>().AsTransient();
			base.Bind<MechanicalGraphManager>().AsSingleton();
			base.Bind<MechanicalGraphFactory>().AsSingleton();
			base.Bind<MechanicalGraphReorganizer>().AsSingleton();
			base.Bind<MechanicalGraphRegistry>().AsSingleton();
			base.Bind<BatteryCharger>().AsSingleton();
			base.Bind<BatteryDischarger>().AsSingleton();
			base.Bind<BatteryService>().AsSingleton();
			base.Bind<TransputMap>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MechanicalSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000431A File Offset: 0x0000251A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MechanicalNodeSpec, MechanicalNode>();
			builder.AddDecorator<MechanicalNode, MechanicalNodeActuals>();
			builder.AddDecorator<TransputProviderSpec, MechanicalNode>();
			builder.AddDecorator<TransputProviderSpec, TransputProvider>();
			builder.AddDecorator<MechanicalNodeTransformHeightSpec, MechanicalNodeTransformHeight>();
			builder.AddDecorator<MechanicalNodeParticlesControllerSpec, MechanicalNodeParticlesController>();
			builder.AddDecorator<MechanicalNodeParticlesController, ParticlesCache>();
			builder.AddDecorator<MechanicalBuildingSpec, MechanicalBuilding>();
			return builder.Build();
		}
	}
}
