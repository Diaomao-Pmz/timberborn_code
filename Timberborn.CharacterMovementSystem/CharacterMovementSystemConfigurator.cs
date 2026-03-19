using System;
using Bindito.Core;
using Timberborn.CharacterModelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class CharacterMovementSystemConfigurator : Configurator
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002464 File Offset: 0x00000664
		public override void Configure()
		{
			base.Bind<AnimatedPathFollower>().AsTransient();
			base.Bind<CharacterRotator>().AsTransient();
			base.Bind<MovementAnimator>().AsTransient();
			base.Bind<RunningProhibitor>().AsTransient();
			base.Bind<PathFollowerFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharacterMovementSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024CA File Offset: 0x000006CA
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<CharacterAnimator, RunningProhibitor>();
			builder.AddDecorator<CharacterAnimator, CharacterRotator>();
			builder.AddDecorator<MovementAnimatorSpec, MovementAnimator>();
			return builder.Build();
		}
	}
}
