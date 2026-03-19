using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.Characters;
using Timberborn.ConstructionSites;
using Timberborn.TemplateInstantiation;
using Timberborn.TerrainLevelValidation;

namespace Timberborn.Explosions
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	[Context("MapEditor")]
	public class ExplosionsConfigurator : Configurator
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000030D0 File Offset: 0x000012D0
		public override void Configure()
		{
			base.Bind<Dynamite>().AsTransient();
			base.Bind<ExplosionVulnerable>().AsTransient();
			base.Bind<Tunnel>().AsTransient();
			base.Bind<UnstableCore>().AsTransient();
			base.Bind<UnstableCoreLighting>().AsTransient();
			base.Bind<UnstableCoreEffectsSpawner>().AsTransient();
			base.Bind<UnstableCoreVisualisation>().AsTransient();
			base.Bind<UnstableCoreExplosionBlocker>().AsTransient();
			base.Bind<CharacterExploder>().AsSingleton();
			base.Bind<ExplosionSoundPlayer>().AsSingleton();
			base.Bind<ExplosionOutcomeGatherer>().AsSingleton();
			base.Bind<ExplosionVisualizerService>().AsSingleton();
			base.Bind<ExplosionService>().AsSingleton();
			base.Bind<ExplosionDataValueSerializer>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<NoGroundOnlyBlockAboveValidator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ExplosionsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031B4 File Offset: 0x000013B4
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Character, ExplosionVulnerable>();
			builder.AddDecorator<DynamiteSpec, Dynamite>();
			builder.AddDecorator<Dynamite, BottomTerrainLevelValidationConstraint>();
			builder.AddDecorator<TunnelSpec, Tunnel>();
			builder.AddDecorator<Tunnel, BottomTerrainLevelValidationConstraint>();
			builder.AddDecorator<Tunnel, DeleteOnFinishConstructionSite>();
			builder.AddDecorator<UnstableCoreSpec, UnstableCore>();
			builder.AddDecorator<UnstableCore, UnstableCoreVisualisation>();
			builder.AddDecorator<UnstableCore, UnstableCoreExplosionBlocker>();
			builder.AddDecorator<UnstableCoreLightingSpec, UnstableCoreLighting>();
			builder.AddDecorator<UnstableCoreEffectsSpawnerSpec, UnstableCoreEffectsSpawner>();
			return builder.Build();
		}
	}
}
