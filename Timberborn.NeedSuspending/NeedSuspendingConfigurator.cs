using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NeedSuspending
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class NeedSuspendingConfigurator : Configurator
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000249B File Offset: 0x0000069B
		public override void Configure()
		{
			base.Bind<EntererNeedSuspendingBuilding>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NeedSuspendingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024C6 File Offset: 0x000006C6
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<EntererNeedSuspendingBuildingSpec, EntererNeedSuspendingBuilding>();
			return builder.Build();
		}
	}
}
