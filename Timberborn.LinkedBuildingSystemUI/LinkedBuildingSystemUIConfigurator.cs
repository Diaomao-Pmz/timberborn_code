using System;
using Bindito.Core;
using Timberborn.LinkedBuildingSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.LinkedBuildingSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class LinkedBuildingSystemUIConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020F5 File Offset: 0x000002F5
		public override void Configure()
		{
			base.Bind<LinkedBuildingRecoverableObjectAdder>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(LinkedBuildingSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<LinkedBuilding, LinkedBuildingRecoverableObjectAdder>();
			return builder.Build();
		}
	}
}
