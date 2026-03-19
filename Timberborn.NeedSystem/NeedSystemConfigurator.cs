using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.TemplateInstantiation;

namespace Timberborn.NeedSystem
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class NeedSystemConfigurator : Configurator
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003002 File Offset: 0x00001202
		public override void Configure()
		{
			base.Bind<NeedManager>().AsTransient();
			base.Bind<SerializedNeedValueSerializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(NeedSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003039 File Offset: 0x00001239
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, NeedManager>();
			return builder.Build();
		}
	}
}
