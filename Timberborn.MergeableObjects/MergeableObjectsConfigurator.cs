using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;

namespace Timberborn.MergeableObjects
{
	// Token: 0x0200000B RID: 11
	[Context("Game")]
	public class MergeableObjectsConfigurator : Configurator
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000026AC File Offset: 0x000008AC
		public override void Configure()
		{
			base.Bind<MergeableObjectModel>().AsTransient();
			base.Bind<MergeableObjectModelUpdater>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(MergeableObjectsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026E3 File Offset: 0x000008E3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MergeableObjectModelSpec, MergeableObjectModel>();
			builder.AddDecorator<MergeableObjectModel, MergeableObjectModelUpdater>();
			return builder.Build();
		}
	}
}
