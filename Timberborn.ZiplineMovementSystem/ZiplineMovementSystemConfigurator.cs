using System;
using Bindito.Core;
using Timberborn.Characters;
using Timberborn.Navigation;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ZiplineMovementSystem
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class ZiplineMovementSystemConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002558 File Offset: 0x00000758
		public override void Configure()
		{
			base.Bind<ZiplineCharacterAnimator>().AsTransient();
			base.Bind<ZiplineHarnessModel>().AsTransient();
			base.Bind<ZiplinePathTracker>().AsTransient();
			base.Bind<ZiplineSwimmingBlocker>().AsTransient();
			base.Bind<ZiplineVisitor>().AsTransient();
			base.Bind<ZiplineVisitorBoundsScaler>().AsTransient();
			base.Bind<ZiplineWaterPenaltyModifier>().AsTransient();
			base.MultiBind<IPathTransformer>().To<ZiplinePathTransformer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ZiplineMovementSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025E7 File Offset: 0x000007E7
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, ZiplinePathTracker>();
			builder.AddDecorator<Character, ZiplineVisitor>();
			builder.AddDecorator<ZiplineVisitor, ZiplineCharacterAnimator>();
			builder.AddDecorator<ZiplineVisitor, ZiplineVisitorBoundsScaler>();
			builder.AddDecorator<ZiplineVisitor, ZiplineSwimmingBlocker>();
			builder.AddDecorator<ZiplineVisitor, ZiplineWaterPenaltyModifier>();
			builder.AddDecorator<ZiplineHarnessModelSpec, ZiplineHarnessModel>();
			return builder.Build();
		}
	}
}
