using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.TemplateInstantiation;

namespace Timberborn.DeconstructionSystem
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class DeconstructionSystemConfigurator : Configurator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002A14 File Offset: 0x00000C14
		public override void Configure()
		{
			base.Bind<Deconstructible>().AsTransient();
			base.Bind<DeconstructionParticleFactory>().AsSingleton();
			base.Bind<DeconstructionNotifier>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(DeconstructionSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A62 File Offset: 0x00000C62
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, Deconstructible>();
			return builder.Build();
		}
	}
}
