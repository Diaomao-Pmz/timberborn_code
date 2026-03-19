using System;
using Bindito.Core;
using Timberborn.TemplateInstantiation;
using Timberborn.Timbermesh;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000014 RID: 20
	[Context("Game")]
	[Context("MapEditor")]
	public class TimbermeshAnimationsConfigurator : Configurator
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002EE0 File Offset: 0x000010E0
		public override void Configure()
		{
			base.Bind<TimbermeshAnimatorController>().AsTransient();
			base.Bind<VertexAnimationTextureGenerator>().AsSingleton();
			base.Bind<NodeAnimationCache>().AsSingleton();
			base.Bind<AnimatorRegistry>().AsSingleton();
			base.Bind<VertexAnimationInitializer>().AsSingleton();
			base.Bind<NodeAnimationInitializer>().AsSingleton();
			base.MultiBind<IModelPostprocessor>().To<AnimationInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TimbermeshAnimationsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F63 File Offset: 0x00001163
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<TimbermeshAnimatorControllerSpec, TimbermeshAnimatorController>();
			return builder.Build();
		}
	}
}
