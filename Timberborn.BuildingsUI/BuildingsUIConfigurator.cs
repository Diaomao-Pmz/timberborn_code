using System;
using Bindito.Core;
using Timberborn.AreaSelectionSystem;
using Timberborn.Buildings;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.BuildingsUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class BuildingsUIConfigurator : Configurator
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000292C File Offset: 0x00000B2C
		public override void Configure()
		{
			base.Bind<BuildingAreaBoundsDrawingBlocker>().AsTransient();
			base.Bind<DeleteBuildingFragment>().AsSingleton();
			base.Bind<PausableBuildingFragment>().AsSingleton();
			base.Bind<BuildingBatchControlRowItemFactory>().AsSingleton();
			base.Bind<AccessibleDebugger>().AsSingleton();
			base.Bind<BuildingSoundControllerFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuildingsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<BuildingsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<IDevModule>().To<BuildingsModelToggler>().AsSingleton();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029C0 File Offset: 0x00000BC0
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuildingSpec, AreaBoundsDrawingBlocker>();
			builder.AddDecorator<BuildingSpec, BuildingAreaBoundsDrawingBlocker>();
			return builder.Build();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002E RID: 46 RVA: 0x000029E0 File Offset: 0x00000BE0
			public EntityPanelModuleProvider(DeleteBuildingFragment deleteBuildingFragment, PausableBuildingFragment pausableBuildingFragment, BuildingSoundControllerFragment buildingSoundControllerFragment)
			{
				this._deleteBuildingFragment = deleteBuildingFragment;
				this._pausableBuildingFragment = pausableBuildingFragment;
				this._buildingSoundControllerFragment = buildingSoundControllerFragment;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x000029FD File Offset: 0x00000BFD
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleHeaderFragment(this._pausableBuildingFragment);
				builder.AddLeftHeaderFragment(this._deleteBuildingFragment, 0);
				builder.AddBottomFragment(this._buildingSoundControllerFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400002C RID: 44
			public readonly DeleteBuildingFragment _deleteBuildingFragment;

			// Token: 0x0400002D RID: 45
			public readonly PausableBuildingFragment _pausableBuildingFragment;

			// Token: 0x0400002E RID: 46
			public readonly BuildingSoundControllerFragment _buildingSoundControllerFragment;
		}
	}
}
