using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000010 RID: 16
	[Context("Game")]
	[Context("MapEditor")]
	public class TerrainPhysicsConfigurator : Configurator
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002A70 File Offset: 0x00000C70
		public override void Configure()
		{
			base.Bind<TerrainPhysicsDeletionBlocker>().AsTransient();
			base.Bind<ITerrainPhysicsService>().To<TerrainPhysicsService>().AsSingleton();
			base.Bind<TerrainPhysicsValidatorFactory>().AsSingleton();
			base.Bind<TerrainPhysicsUpdater>().AsSingleton();
			base.Bind<TerrainDestroyer>().AsSingleton();
			base.Bind<TerrainAndBlockObjectsToDeleteFinder>().AsSingleton();
			base.Bind<TerrainOnBlockObjectFinder>().AsSingleton();
			base.Bind<SupportsToBeDeleted>().AsSingleton();
			base.Bind<TerrainPhysicsValidationEnabler>().AsSingleton();
			base.Bind<TerrainPhysicsPostLoader>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<TerrainPhysicsBlockObjectValidator>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(TerrainPhysicsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B28 File Offset: 0x00000D28
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BlockObject, TerrainPhysicsDeletionBlocker>();
			return builder.Build();
		}
	}
}
