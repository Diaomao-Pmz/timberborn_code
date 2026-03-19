using System;
using Bindito.Core;
using Timberborn.Navigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.PathSystem
{
	// Token: 0x0200001A RID: 26
	[Context("Game")]
	public class PathSystemConfigurator : Configurator
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000384C File Offset: 0x00001A4C
		public override void Configure()
		{
			base.Bind<DrivewayModel>().AsTransient();
			base.Bind<DynamicPathModel>().AsTransient();
			base.Bind<DynamicPathModelUpdater>().AsTransient();
			base.Bind<PathModelTypeEnforcer>().AsTransient();
			base.Bind<SpiralStairsHeightProvider>().AsTransient();
			base.Bind<StraightStairsHeightProvider>().AsTransient();
			base.Bind<DrivewayModelInstantiator>().AsSingleton();
			base.Bind<IPathService>().To<PathService>().AsSingleton();
			base.Bind<IConnectionService>().To<ConnectionService>().AsSingleton();
			base.MultiBind<IPathTransformer>().To<StairsPathTransformer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PathSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000038FD File Offset: 0x00001AFD
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DrivewayModelSpec, DrivewayModel>();
			builder.AddDecorator<DynamicPathModelSpec, DynamicPathModel>();
			builder.AddDecorator<StraightStairsSpec, StraightStairsHeightProvider>();
			builder.AddDecorator<SpiralStairsSpec, SpiralStairsHeightProvider>();
			builder.AddDecorator<PathModelTypeEnforcerSpec, PathModelTypeEnforcer>();
			return builder.Build();
		}
	}
}
