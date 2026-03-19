using System;
using Bindito.Core;
using Timberborn.BuildingRange;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.LaborSystem;
using Timberborn.SimpleOutputBuildings;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.BuilderHubSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	[Context("MapEditor")]
	public class BuilderHubSystemConfigurator : Configurator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000021C8 File Offset: 0x000003C8
		public override void Configure()
		{
			base.Bind<BuilderHubWorkplaceBehavior>().AsTransient();
			base.MultiBind<IBuilderJobProvider>().To<BuildingJobProvider>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(BuilderHubSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002204 File Offset: 0x00000404
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<BuilderHubSpec, BuildingWithRoadSpillRange>();
			builder.AddDecorator<BuilderHubSpec, AutoEmptiable>();
			builder.AddDecorator<BuilderHubSpec, Emptiable>();
			builder.AddDecorator<BuilderHubSpec, HaulCandidate>();
			builder.AddDecorator<BuilderHubSpec, SimpleOutputInventoryHaulBehaviorProvider>();
			BuilderHubSystemConfigurator.AddDecoratingBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002234 File Offset: 0x00000434
		public static void AddDecoratingBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<BuilderHubSpec, BuilderHubWorkplaceBehavior>();
			builder.AddDecorator<BuilderHubSpec, EmptyOutputWorkplaceBehavior>();
			builder.AddDecorator<BuilderHubSpec, RemoveUnwantedStockWorkplaceBehavior>();
			builder.AddDecorator<BuilderHubSpec, EmptyInventoriesWorkplaceBehavior>();
			builder.AddDecorator<BuilderHubSpec, LaborWorkplaceBehavior>();
			builder.AddDecorator<BuilderHubSpec, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
