using System;
using Bindito.Core;
using Timberborn.GameDistricts;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Reproduction
{
	// Token: 0x02000013 RID: 19
	[Context("Game")]
	public class ReproductionConfigurator : Configurator
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003070 File Offset: 0x00001270
		public override void Configure()
		{
			base.Bind<BringNutrientBehavior>().AsTransient();
			base.Bind<BringNutrientWorkplaceBehavior>().AsTransient();
			base.Bind<DistrictBreedingPodService>().AsTransient();
			base.Bind<BreedingPod>().AsTransient();
			base.Bind<BringNutrientHaulBehaviorProvider>().AsTransient();
			base.Bind<ProcreationHouse>().AsTransient();
			base.Bind<BreedingPodInventoryInitializer>().AsSingleton();
			base.Bind<NewbornSpawner>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<ReproductionConfigurator.ReproductionTemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000014 RID: 20
		public class ReproductionTemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x0600006D RID: 109 RVA: 0x000030F6 File Offset: 0x000012F6
			public ReproductionTemplateModuleProvider(BreedingPodInventoryInitializer breedingPodInventoryInitializer)
			{
				this._breedingPodInventoryInitializer = breedingPodInventoryInitializer;
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00003108 File Offset: 0x00001308
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDecorator<DistrictCenter, DistrictBreedingPodService>();
				builder.AddDecorator<BreedingPodSpec, BreedingPod>();
				builder.AddDecorator<BreedingPod, HaulCandidate>();
				builder.AddDecorator<BreedingPod, BringNutrientWorkplaceBehavior>();
				builder.AddDecorator<BreedingPod, BringNutrientHaulBehaviorProvider>();
				builder.AddDecorator<Worker, BringNutrientBehavior>();
				builder.AddDecorator<ProcreationHouseSpec, ProcreationHouse>();
				builder.AddDedicatedDecorator<BreedingPod, Inventory>(this._breedingPodInventoryInitializer);
				return builder.Build();
			}

			// Token: 0x04000030 RID: 48
			public readonly BreedingPodInventoryInitializer _breedingPodInventoryInitializer;
		}
	}
}
