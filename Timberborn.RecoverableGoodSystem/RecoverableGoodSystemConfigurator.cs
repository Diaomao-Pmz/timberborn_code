using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.TemplateInstantiation;

namespace Timberborn.RecoverableGoodSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class RecoverableGoodSystemConfigurator : Configurator
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002503 File Offset: 0x00000703
		public override void Configure()
		{
			base.Bind<RecoverableGoodProvider>().AsTransient();
			base.Bind<GoodRecoveryRateService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(RecoverableGoodSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000253A File Offset: 0x0000073A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, RecoverableGoodProvider>();
			return builder.Build();
		}
	}
}
