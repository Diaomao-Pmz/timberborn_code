using System;
using Bindito.Core;
using Timberborn.Rendering;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TemplateAttachmentSystem
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class TemplateAttachmentSystemConfigurator : Configurator
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000029CE File Offset: 0x00000BCE
		public override void Configure()
		{
			base.Bind<TemplateAttachments>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TemplateAttachmentSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029F9 File Offset: 0x00000BF9
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TemplateAttachmentsSpec, TemplateAttachments>();
			builder.AddDecorator<TemplateAttachments, EntityMaterials>();
			return builder.Build();
		}
	}
}
