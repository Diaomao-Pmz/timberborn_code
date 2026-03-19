using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.Workshops;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x0200001F RID: 31
	[Context("Game")]
	public class WorkshopsUIConfigurator : Configurator
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public override void Configure()
		{
			base.Bind<ManufactoryDescriber>().AsTransient();
			base.Bind<ManufactoryDropdownProvider>().AsTransient();
			base.Bind<ProductionProgressFragment>().AsSingleton();
			base.Bind<ManufactoryFragment>().AsSingleton();
			base.Bind<ManufactoryInventoryFragment>().AsSingleton();
			base.Bind<ManufactoryBatchControlRowItemFactory>().AsSingleton();
			base.Bind<ProductivityBatchControlRowItemFactory>().AsSingleton();
			base.Bind<ManufactoryRecipeSliderToggleFactory>().AsSingleton();
			base.Bind<ManufactoryTogglableRecipesFragment>().AsSingleton();
			base.Bind<ManufactoryTogglableRecipesBatchControlRowItemFactory>().AsSingleton();
			base.Bind<ProductivityFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WorkshopsUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WorkshopsUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C5F File Offset: 0x00001E5F
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Manufactory, ManufactoryDescriber>();
			builder.AddDecorator<Manufactory, ManufactoryDropdownProvider>();
			return builder.Build();
		}

		// Token: 0x02000020 RID: 32
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600009C RID: 156 RVA: 0x00003C7F File Offset: 0x00001E7F
			public EntityPanelModuleProvider(ManufactoryFragment manufactoryFragment, ManufactoryTogglableRecipesFragment manufactoryTogglableRecipesFragment, ProductionProgressFragment productionProgressFragment, ProductivityFragment productivityFragment, ManufactoryInventoryFragment manufactoryInventoryFragment)
			{
				this._manufactoryFragment = manufactoryFragment;
				this._manufactoryTogglableRecipesFragment = manufactoryTogglableRecipesFragment;
				this._productionProgressFragment = productionProgressFragment;
				this._productivityFragment = productivityFragment;
				this._manufactoryInventoryFragment = manufactoryInventoryFragment;
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00003CAC File Offset: 0x00001EAC
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._manufactoryFragment, 0);
				builder.AddMiddleFragment(this._manufactoryTogglableRecipesFragment, 0);
				builder.AddMiddleFragment(this._manufactoryInventoryFragment, 20);
				builder.AddMiddleFragment(this._productionProgressFragment, 0);
				builder.AddMiddleFragment(this._productivityFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000089 RID: 137
			public readonly ManufactoryFragment _manufactoryFragment;

			// Token: 0x0400008A RID: 138
			public readonly ManufactoryTogglableRecipesFragment _manufactoryTogglableRecipesFragment;

			// Token: 0x0400008B RID: 139
			public readonly ProductionProgressFragment _productionProgressFragment;

			// Token: 0x0400008C RID: 140
			public readonly ProductivityFragment _productivityFragment;

			// Token: 0x0400008D RID: 141
			public readonly ManufactoryInventoryFragment _manufactoryInventoryFragment;
		}
	}
}
