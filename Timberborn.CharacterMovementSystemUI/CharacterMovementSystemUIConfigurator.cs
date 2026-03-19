using System;
using Bindito.Core;
using Timberborn.CharacterMovementSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.CharacterMovementSystemUI
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	public class CharacterMovementSystemUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<MovementSpeedBoostingBuildingDescriber>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(CharacterMovementSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<MovementSpeedBoostingBuildingSpec, MovementSpeedBoostingBuildingDescriber>();
			return builder.Build();
		}
	}
}
