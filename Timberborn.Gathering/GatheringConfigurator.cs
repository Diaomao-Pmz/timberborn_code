using System;
using Bindito.Core;
using Timberborn.BuildingRange;
using Timberborn.Emptying;
using Timberborn.GoodStackSystem;
using Timberborn.Hauling;
using Timberborn.LaborSystem;
using Timberborn.SimpleOutputBuildings;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Gathering
{
	// Token: 0x02000011 RID: 17
	[Context("Game")]
	[Context("MapEditor")]
	public class GatheringConfigurator : Configurator
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002C24 File Offset: 0x00000E24
		public override void Configure()
		{
			base.Bind<GatherWorkplaceBehavior>().AsTransient();
			base.Bind<Gatherable>().AsTransient();
			base.Bind<GatherablePrioritizer>().AsTransient();
			base.Bind<GatherableYieldGrower>().AsTransient();
			base.Bind<GathererFlag>().AsTransient();
			base.Bind<GathererFlagYielderRetriever>().AsTransient();
			base.Bind<GatherableModel>().AsTransient();
			base.Bind<GoodStackService<GathererFlag>>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(GatheringConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GatherableSpec, Gatherable>();
			builder.AddDecorator<Gatherable, GatherableYieldGrower>();
			builder.AddDecorator<Gatherable, GoodStack>();
			builder.AddDecorator<Gatherable, GatherableModel>();
			builder.AddDecorator<GathererFlagSpec, GathererFlag>();
			builder.AddDecorator<GathererFlag, BuildingWithTerrainRange>();
			builder.AddDecorator<GatherWorkplaceBehavior, GatherablePrioritizer>();
			builder.AddDecorator<GathererFlag, AutoEmptiable>();
			builder.AddDecorator<GathererFlag, Emptiable>();
			builder.AddDecorator<GathererFlag, HaulCandidate>();
			builder.AddDecorator<GathererFlag, SimpleOutputInventoryHaulBehaviorProvider>();
			builder.AddDecorator<GathererFlag, GathererFlagYielderRetriever>();
			GatheringConfigurator.AddDecoratingBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D15 File Offset: 0x00000F15
		public static void AddDecoratingBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<GathererFlag, RemoveUnwantedStockWorkplaceBehavior>();
			builder.AddDecorator<GathererFlag, GatherWorkplaceBehavior>();
			builder.AddDecorator<GathererFlag, EmptyOutputWorkplaceBehavior>();
			builder.AddDecorator<GathererFlag, EmptyInventoriesWorkplaceBehavior>();
			builder.AddDecorator<GathererFlag, LaborWorkplaceBehavior>();
			builder.AddDecorator<GathererFlag, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
