using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.LifeSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Beavers
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	public class BeaversConfigurator : Configurator
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002814 File Offset: 0x00000A14
		public override void Configure()
		{
			base.Bind<ChildRootBehavior>().AsTransient();
			base.Bind<Child>().AsTransient();
			base.Bind<BeaverLongevity>().AsTransient();
			base.Bind<BeaverTextureSetter>().AsTransient();
			base.Bind<Beaver>().AsTransient();
			base.Bind<BeaverEntityNamer>().AsTransient();
			base.Bind<BeaverFactory>().AsSingleton();
			base.Bind<BeaverNameService>().AsSingleton();
			base.Bind<BeaverPopulation>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BeaversConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028AA File Offset: 0x00000AAA
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, Beaver>();
			builder.AddDecorator<BeaverSpec, Character>();
			builder.AddDecorator<BeaverSpec, BeaverEntityNamer>();
			builder.AddDecorator<BeaverSpec, LifeProgressor>();
			builder.AddDecorator<BeaverSpec, BeaverTextureSetter>();
			builder.AddDecorator<BeaverSpec, BeaverLongevity>();
			builder.AddDecorator<ChildSpec, Child>();
			return builder.Build();
		}
	}
}
