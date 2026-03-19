using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x0200000D RID: 13
	[Context("Game")]
	[Context("MapEditor")]
	public class TerrainLevelValidationConfigurator : Configurator
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023A8 File Offset: 0x000005A8
		public override void Configure()
		{
			base.Bind<ContinuousTerrainConstraint>().AsTransient();
			base.Bind<BottomTerrainLevelValidationConstraint>().AsTransient();
			base.Bind<TopTerrainLevelValidationConstraint>().AsTransient();
			base.Bind<ContinuousTerrainConstraintValidator>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<TerrainLevelValidator>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<UndergroundTerrainValidator>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().ToExisting<ContinuousTerrainConstraintValidator>();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TerrainLevelValidationConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002430 File Offset: 0x00000630
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ContinuousTerrainConstraintSpec, ContinuousTerrainConstraint>();
			return builder.Build();
		}
	}
}
