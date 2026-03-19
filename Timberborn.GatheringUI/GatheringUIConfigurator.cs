using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Gathering;
using Timberborn.SimpleOutputBuildingsUI;
using Timberborn.TemplateInstantiation;
using Timberborn.YielderFinding;

namespace Timberborn.GatheringUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	public class GatheringUIConfigurator : Configurator
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000029D0 File Offset: 0x00000BD0
		public override void Configure()
		{
			base.Bind<GatherablePrioritizerDropdownProvider>().AsTransient();
			base.Bind<GatherablePrioritizerFragment>().AsSingleton();
			base.Bind<GatherablePrioritizerBatchControlRowItemFactory>().AsSingleton();
			base.Bind<GatherableToolPanelItemFactory>().AsSingleton();
			base.Bind<GatherableFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GatheringUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<GatheringUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A47 File Offset: 0x00000C47
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GathererFlag, YieldStatus>();
			builder.AddDecorator<GathererFlag, SimpleOutputInventoryFragmentEnabler>();
			builder.AddDecorator<GathererFlag, GatherablePrioritizerDropdownProvider>();
			return builder.Build();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000030 RID: 48 RVA: 0x00002A6D File Offset: 0x00000C6D
			public EntityPanelModuleProvider(GatherablePrioritizerFragment gatherablePrioritizerFragment, GatherableFragment gatherableFragment)
			{
				this._gatherablePrioritizerFragment = gatherablePrioritizerFragment;
				this._gatherableFragment = gatherableFragment;
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00002A83 File Offset: 0x00000C83
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._gatherablePrioritizerFragment, 0);
				builder.AddMiddleFragment(this._gatherableFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000037 RID: 55
			public readonly GatherablePrioritizerFragment _gatherablePrioritizerFragment;

			// Token: 0x04000038 RID: 56
			public readonly GatherableFragment _gatherableFragment;
		}
	}
}
