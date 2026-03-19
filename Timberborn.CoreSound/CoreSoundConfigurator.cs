using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CoreSound
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class CoreSoundConfigurator : Configurator
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000024B4 File Offset: 0x000006B4
		public override void Configure()
		{
			base.Bind<BasicSelectionSound>().AsTransient();
			base.Bind<SoundListener>().AsSingleton();
			base.Bind<CameraHeightVolumeUpdater>().AsSingleton();
			base.Bind<WindAmbientSound>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CoreSoundConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000250E File Offset: 0x0000070E
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BasicSelectionSoundSpec, BasicSelectionSound>();
			return builder.Build();
		}
	}
}
