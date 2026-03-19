using System;
using Bindito.Core;
using Timberborn.BuildingsNavigation;
using Timberborn.Characters;
using Timberborn.Illumination;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000014 RID: 20
	[Context("Game")]
	public class TubeSystemConfigurator : Configurator
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public override void Configure()
		{
			base.Bind<Tube>().AsTransient();
			base.Bind<TubeAccessiblePathModifier>().AsTransient();
			base.Bind<TubeIlluminator>().AsTransient();
			base.Bind<TubeModel>().AsTransient();
			base.Bind<TubePlatform>().AsTransient();
			base.Bind<TubeTracker>().AsTransient();
			base.Bind<TubeVisitor>().AsTransient();
			base.Bind<TubeVisitorRegistrar>().AsTransient();
			base.Bind<TubeMap>().AsSingleton();
			base.Bind<TubeVisitorUpdater>().AsSingleton();
			base.Bind<TubeVisitorRegistry>().AsSingleton();
			base.Bind<TubeConnectionService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TubeSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002E90 File Offset: 0x00001090
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TubeSpec, Tube>();
			builder.AddDecorator<Tube, TubeIlluminator>();
			builder.AddDecorator<Tube, TubeAccessiblePathModifier>();
			builder.AddDecorator<Tube, PathMeshHider>();
			builder.AddDecorator<TubeStationSpec, PathMeshHider>();
			builder.AddDecorator<TubeModelSpec, TubeModel>();
			builder.AddDecorator<TubePlatformSpec, TubePlatform>();
			builder.AddDecorator<TubeIlluminator, Illuminator>();
			builder.AddDecorator<Character, TubeTracker>();
			builder.AddDecorator<Character, TubeVisitor>();
			builder.AddDecorator<TubeVisitor, TubeVisitorRegistrar>();
			return builder.Build();
		}
	}
}
