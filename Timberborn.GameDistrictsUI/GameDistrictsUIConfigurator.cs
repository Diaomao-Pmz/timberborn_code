using System;
using Bindito.Core;
using Timberborn.BatchControl;
using Timberborn.BlockSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000018 RID: 24
	[Context("Game")]
	public class GameDistrictsUIConfigurator : Configurator
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003C6C File Offset: 0x00001E6C
		public override void Configure()
		{
			base.Bind<CitizenDistrictTintChanger>().AsTransient();
			base.Bind<CitizenTint>().AsTransient();
			base.Bind<DistrictBuildingEntityBadge>().AsTransient();
			base.Bind<DistrictCenterEntityBadge>().AsTransient();
			base.Bind<PreviewDistrictObstacle>().AsTransient();
			base.Bind<SelectableDistrictBuilding>().AsTransient();
			base.Bind<DistrictCenterFragment>().AsSingleton();
			base.Bind<DistrictContextService>().AsSingleton();
			base.Bind<DistrictListPanel>().AsSingleton();
			base.Bind<DistrictPanel>().AsSingleton();
			base.Bind<IHideableByBatchControl>().ToExisting<DistrictPanel>();
			base.Bind<CitizenNameTintChanger>().AsSingleton();
			base.Bind<DistrictConnectionDrawingService>().AsSingleton();
			base.Bind<DistrictConnectionLineRotator>().AsSingleton();
			base.Bind<DistrictConnectionLineRenderer>().AsSingleton();
			base.MultiBind<IBlockObjectValidator>().To<DistrictPreviewsValidator>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<GameDistrictsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GameDistrictsUIConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003D6C File Offset: 0x00001F6C
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DistrictBuilding, SelectableDistrictBuilding>();
			builder.AddDecorator<DistrictBuilding, DistrictBuildingEntityBadge>();
			builder.AddDecorator<DistrictCenter, DistrictCenterEntityBadge>();
			builder.AddDecorator<DistrictCenter, CitizenDistrictTintChanger>();
			builder.AddDecorator<DistrictObstacle, PreviewDistrictObstacle>();
			builder.AddDecorator<Citizen, CitizenTint>();
			return builder.Build();
		}

		// Token: 0x02000019 RID: 25
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x060000B4 RID: 180 RVA: 0x00003DA4 File Offset: 0x00001FA4
			public EntityPanelModuleProvider(DistrictCenterFragment districtCenterFragment)
			{
				this._districtCenterFragment = districtCenterFragment;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00003DB3 File Offset: 0x00001FB3
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._districtCenterFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000069 RID: 105
			public readonly DistrictCenterFragment _districtCenterFragment;
		}
	}
}
