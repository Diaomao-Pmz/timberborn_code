using System;
using Bindito.Core;
using Timberborn.ConstructionSites;
using Timberborn.EntityPanelSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class ConstructionSitesUIConfigurator : Configurator
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002858 File Offset: 0x00000A58
		public override void Configure()
		{
			base.Bind<ConstructionSiteDescriber>().AsTransient();
			base.Bind<ConstructionSiteDebugFragment>().AsSingleton();
			base.Bind<ConstructionSiteFragment>().AsSingleton();
			base.Bind<ConstructionSiteFragmentInventory>().AsSingleton();
			base.Bind<ConstructionSitePanelDescriptionUpdater>().AsSingleton();
			base.Bind<ConstructionSitePriorityBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(ConstructionSitesUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<ConstructionSitesUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028DB File Offset: 0x00000ADB
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<ConstructionSite, ConstructionSiteDescriber>();
			return builder.Build();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600002D RID: 45 RVA: 0x000028F5 File Offset: 0x00000AF5
			public EntityPanelModuleProvider(ConstructionSiteDebugFragment constructionSiteDebugFragment, ConstructionSiteFragment constructionSiteFragment)
			{
				this._constructionSiteDebugFragment = constructionSiteDebugFragment;
				this._constructionSiteFragment = constructionSiteFragment;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x0000290B File Offset: 0x00000B0B
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._constructionSiteFragment, 0);
				builder.AddDiagnosticFragment(this._constructionSiteDebugFragment);
				return builder.Build();
			}

			// Token: 0x0400002F RID: 47
			public readonly ConstructionSiteDebugFragment _constructionSiteDebugFragment;

			// Token: 0x04000030 RID: 48
			public readonly ConstructionSiteFragment _constructionSiteFragment;
		}
	}
}
