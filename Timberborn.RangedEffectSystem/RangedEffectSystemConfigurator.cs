using System;
using Bindito.Core;
using Timberborn.EnterableSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000017 RID: 23
	[Context("Game")]
	public class RangedEffectSystemConfigurator : Configurator
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003488 File Offset: 0x00001688
		public override void Configure()
		{
			base.Bind<RangedEffectBuilding>().AsTransient();
			base.Bind<RangedEffectSubject>().AsTransient();
			base.Bind<ContinuousEffectBuilding>().AsTransient();
			base.Bind<ContinuousEffectBuildingDescriber>().AsTransient();
			base.Bind<RangedEffectApplier>().AsTransient();
			base.Bind<RangedEffectsAffectingEnterable>().AsTransient();
			base.Bind<RangedEffectService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RangedEffectSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003506 File Offset: 0x00001706
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ContinuousEffectBuildingSpec, ContinuousEffectBuilding>();
			builder.AddDecorator<ContinuousEffectBuilding, ContinuousEffectBuildingDescriber>();
			builder.AddDecorator<Enterer, RangedEffectSubject>();
			builder.AddDecorator<Enterable, RangedEffectsAffectingEnterable>();
			builder.AddDecorator<RangedEffectBuildingSpec, RangedEffectBuilding>();
			builder.AddDecorator<RangedEffectBuilding, RangeEnterableHighlighter>();
			builder.AddDecorator<RangedEffectBuilding, RangedEffectApplier>();
			return builder.Build();
		}
	}
}
