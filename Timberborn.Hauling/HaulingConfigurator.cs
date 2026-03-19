using System;
using Bindito.Core;
using Timberborn.Carrying;
using Timberborn.GameDistricts;
using Timberborn.TemplateInstantiation;
using Timberborn.WorkSystem;

namespace Timberborn.Hauling
{
	// Token: 0x0200000E RID: 14
	[Context("Game")]
	public class HaulingConfigurator : Configurator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002884 File Offset: 0x00000A84
		public override void Configure()
		{
			base.Bind<HaulWorkplaceBehavior>().AsTransient();
			base.Bind<HaulingBlocker>().AsTransient();
			base.Bind<DistrictHaulCandidates>().AsTransient();
			base.Bind<HaulCandidate>().AsTransient();
			base.Bind<Hauler>().AsTransient();
			base.Bind<HaulingCenter>().AsTransient();
			base.Bind<HaulPrioritizable>().AsTransient();
			base.Bind<NoHaulingPostStatus>().AsTransient();
			base.Bind<WorkplaceWithBackpacks>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(HaulingConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000291A File Offset: 0x00000B1A
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<GoodCarrier, Hauler>();
			builder.AddDecorator<HaulCandidate, HaulPrioritizable>();
			builder.AddDecorator<HaulingCenterSpec, HaulingCenter>();
			builder.AddDecorator<HaulingCenter, WorkplaceWithBackpacks>();
			builder.AddDecorator<DistrictCenter, DistrictHaulCandidates>();
			HaulingConfigurator.InitializeBehaviors(builder);
			return builder.Build();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000294A File Offset: 0x00000B4A
		public static void InitializeBehaviors(TemplateModule.Builder builder)
		{
			builder.AddDecorator<HaulingCenter, HaulWorkplaceBehavior>();
			builder.AddDecorator<HaulingCenter, WaitInsideIdlyWorkplaceBehavior>();
		}
	}
}
