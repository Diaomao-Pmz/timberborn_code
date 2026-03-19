using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ConstructionMode
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class ConstructionModeConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002115 File Offset: 0x00000315
		public override void Configure()
		{
			base.Bind<ConstructionModeModel>().AsTransient();
			base.Bind<ConstructionModeService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ConstructionModeConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000214C File Offset: 0x0000034C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, ConstructionModeModel>();
			return builder.Build();
		}
	}
}
