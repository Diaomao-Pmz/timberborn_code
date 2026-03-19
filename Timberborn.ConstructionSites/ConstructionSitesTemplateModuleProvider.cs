using System;
using Bindito.Core;
using Timberborn.BlockSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.Buildings;
using Timberborn.InventorySystem;
using Timberborn.SlotSystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000021 RID: 33
	public class ConstructionSitesTemplateModuleProvider : IProvider<TemplateModule>
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000423B File Offset: 0x0000243B
		public ConstructionSitesTemplateModuleProvider(ConstructionSiteInventoryInitializer constructionSiteInventoryInitializer)
		{
			this._constructionSiteInventoryInitializer = constructionSiteInventoryInitializer;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000424C File Offset: 0x0000244C
		public TemplateModule Get()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDedicatedDecorator<ConstructionSite, Inventory>(this._constructionSiteInventoryInitializer);
			builder.AddDecorator<ConstructionRegistrar, ConstructionSite>();
			builder.AddDecorator<ConstructionSite, ConstructionSiteModelUpdater>();
			builder.AddDecorator<ConstructionSite, GroundedConstructionSite>();
			builder.AddDecorator<ConstructionSite, ConstructionJob>();
			builder.AddDecorator<ConstructionSite, ConstructionSitePrioritizableEnabler>();
			builder.AddDecorator<ConstructionSite, ConstructionSiteRecoverableGoodMultiplier>();
			builder.AddDecorator<ConstructionSite, ConstructionSiteReservations>();
			builder.AddDecorator<ConstructionSitePrioritizableEnabler, BuilderPrioritizable>();
			builder.AddDecorator<ConstructionSiteSlotManagerSpec, ConstructionSiteSlotManager>();
			builder.AddDecorator<ConstructionSiteSlotManager, SlotManager>();
			builder.AddDecorator<BlockObject, PhysicallySupportedConstructionSiteUpdater>();
			builder.AddDecorator<ConstructionSiteBuildersLimiterSpec, ConstructionSiteBuildersLimiter>();
			builder.AddDecorator<ConstructionSiteProgressVisualizerSpec, ConstructionSiteProgressVisualizer>();
			builder.AddDecorator<Worker, Builder>();
			builder.AddDecorator<Worker, BuildBehavior>();
			builder.AddDecorator<BuildingSpec, ConstructionRegistrar>();
			return builder.Build();
		}

		// Token: 0x04000067 RID: 103
		public readonly ConstructionSiteInventoryInitializer _constructionSiteInventoryInitializer;
	}
}
