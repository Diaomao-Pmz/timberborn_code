using System;
using Bindito.Core;
using Timberborn.Beavers;
using Timberborn.TemplateInstantiation;

namespace Timberborn.SleepSystem
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class SleepSystemConfigurator : Configurator
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002978 File Offset: 0x00000B78
		public override void Configure()
		{
			base.Bind<SleepNeedBehavior>().AsTransient();
			base.Bind<SleepSoundEmitter>().AsTransient();
			base.Bind<Sleeper>().AsTransient();
			base.Bind<SleepSoundController>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(SleepSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029D2 File Offset: 0x00000BD2
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BeaverSpec, SleepSoundEmitter>();
			builder.AddDecorator<SleeperSpec, Sleeper>();
			return builder.Build();
		}
	}
}
