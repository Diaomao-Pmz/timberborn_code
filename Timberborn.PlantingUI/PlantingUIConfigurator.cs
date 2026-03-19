using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Planting;
using Timberborn.SelectionSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.ToolSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000025 RID: 37
	[Context("Game")]
	public class PlantingUIConfigurator : Configurator
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003F24 File Offset: 0x00002124
		public override void Configure()
		{
			base.Bind<PlantablePreview>().AsTransient();
			base.Bind<PlantablePrioritizerDropdownProvider>().AsTransient();
			base.Bind<PlantablePrioritizerFragment>().AsSingleton();
			base.Bind<PlantablePreviewService>().AsSingleton();
			base.Bind<PlantablePreviewFactory>().AsSingleton();
			base.Bind<PlantableDescriber>().AsSingleton();
			base.Bind<PlantingModeService>().AsSingleton();
			base.Bind<UnlockedPlantableService>().AsSingleton();
			base.Bind<PlantingSelectionService>().AsSingleton();
			base.Bind<DevModePlantableSpawner>().AsSingleton();
			base.Bind<PlantingToolButtonFactory>().AsSingleton();
			base.Bind<PlantablePrioritizerBatchControlRowItemFactory>().AsSingleton();
			base.Bind<UnlockedPlantableGroupsRegistry>().AsSingleton();
			base.MultiBind<IToolFinder>().To<PlantingToolFinder>().AsSingleton();
			base.MultiBind<IToolLocker>().To<PlantableToolLocker>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(PlantingUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<PlantingUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000401D File Offset: 0x0000221D
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<PlanterBuilding, PlantablePrioritizerDropdownProvider>();
			builder.AddDecorator<PlantablePreviewSpec, PlantablePreview>();
			builder.AddDecorator<PlantablePreview, HighlightableObject>();
			return builder.Build();
		}

		// Token: 0x02000026 RID: 38
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x060000CF RID: 207 RVA: 0x00004043 File Offset: 0x00002243
			public EntityPanelModuleProvider(PlantablePrioritizerFragment plantablePrioritizerFragment)
			{
				this._plantablePrioritizerFragment = plantablePrioritizerFragment;
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00004052 File Offset: 0x00002252
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._plantablePrioritizerFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000080 RID: 128
			public readonly PlantablePrioritizerFragment _plantablePrioritizerFragment;
		}
	}
}
