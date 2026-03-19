using System;
using Bindito.Core;
using Timberborn.EnterableSystem;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Attractions
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class AttractionsConfigurator : Configurator
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002C68 File Offset: 0x00000E68
		public override void Configure()
		{
			base.Bind<Attraction>().AsTransient();
			base.Bind<AttractionAttender>().AsTransient();
			base.Bind<GoodConsumingAttraction>().AsTransient();
			base.Bind<AttractionNeedBehavior>().AsTransient();
			base.Bind<AttractionFire>().AsTransient();
			base.Bind<AttractionLoadRate>().AsTransient();
			base.Bind<GoodConsumingAttractionSurfaceController>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(AttractionsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Enterer, AttractionAttender>();
			builder.AddDecorator<AttractionSpec, Attraction>();
			builder.AddDecorator<Attraction, AttractionNeedBehavior>();
			builder.AddDecorator<Attraction, EnterableSounds>();
			builder.AddDecorator<Attraction, AttractionLoadRate>();
			builder.AddDecorator<AttractionFireSpec, AttractionFire>();
			builder.AddDecorator<GoodConsumingAttractionSpec, GoodConsumingAttraction>();
			builder.AddDecorator<GoodConsumingAttractionSurfaceControllerSpec, GoodConsumingAttractionSurfaceController>();
			builder.AddDecorator<GoodConsumingAttractionSurfaceController, ParticlesCache>();
			return builder.Build();
		}
	}
}
