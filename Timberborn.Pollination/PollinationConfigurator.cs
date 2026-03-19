using System;
using Bindito.Core;
using Timberborn.Fields;
using Timberborn.TemplateInstantiation;

namespace Timberborn.Pollination
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class PollinationConfigurator : Configurator
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002978 File Offset: 0x00000B78
		public override void Configure()
		{
			base.Bind<Hive>().AsTransient();
			base.Bind<Pollinatee>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PollinationConfigurator.ProvidePollinationModule)).AsSingleton();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029AF File Offset: 0x00000BAF
		public static TemplateModule ProvidePollinationModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Crop, Pollinatee>();
			builder.AddDecorator<HiveSpec, Hive>();
			return builder.Build();
		}
	}
}
