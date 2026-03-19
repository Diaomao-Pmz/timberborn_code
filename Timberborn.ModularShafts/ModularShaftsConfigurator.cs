using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	public class ModularShaftsConfigurator : Configurator
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000035B8 File Offset: 0x000017B8
		public override void Configure()
		{
			base.Bind<ModularShaftModelUpdater>().AsTransient();
			base.Bind<ModularShaft>().AsTransient();
			base.Bind<ModularShaftAnimator>().AsTransient();
			base.Bind<ModularShaftCover>().AsTransient();
			base.Bind<ModularShaftVariantFinder>().AsTransient();
			base.Bind<ShaftSoundEmitter>().AsTransient();
			base.Bind<ModularShaftModelService>().AsSingleton();
			base.Bind<ShaftModelFactory>().AsSingleton();
			base.Bind<ShaftFrameFactory>().AsSingleton();
			base.Bind<ModularShaftAnimatorUpdater>().AsSingleton();
			base.Bind<ShaftSoundController>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ModularShaftsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003666 File Offset: 0x00001866
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ModularShaftSpec, ModularShaft>();
			builder.AddDecorator<ModularShaft, ModularShaftModelUpdater>();
			builder.AddDecorator<ModularShaft, ModularShaftVariantFinder>();
			builder.AddDecorator<ModularShaft, ModularShaftAnimator>();
			builder.AddDecorator<ModularShaftCoverSpec, ModularShaftCover>();
			builder.AddDecorator<ShaftSoundEmitterSpec, ShaftSoundEmitter>();
			return builder.Build();
		}
	}
}
