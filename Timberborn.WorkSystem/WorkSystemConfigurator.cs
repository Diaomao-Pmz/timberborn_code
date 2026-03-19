using System;
using Bindito.Core;
using Timberborn.Buildings;
using Timberborn.GameDistricts;
using Timberborn.Metrics;
using Timberborn.SlotSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200002E RID: 46
	[Context("Game")]
	public class WorkSystemConfigurator : Configurator
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00005248 File Offset: 0x00003448
		public override void Configure()
		{
			base.Bind<WaitInsideIdlyWorkplaceBehavior>().AsTransient();
			base.Bind<WorkerRootBehavior>().AsTransient();
			base.Bind<UnreachableWorkplaceUnassigner>().AsTransient();
			base.Bind<DistrictWorkplaceAssigner>().AsTransient();
			base.Bind<Worker>().AsTransient();
			base.Bind<Workplace>().AsTransient();
			base.Bind<WorkerWorkingHours>().AsTransient();
			base.Bind<DistrictDefaultWorkerType>().AsTransient();
			base.Bind<NothingToDoInRangeStatus>().AsTransient();
			base.Bind<WorkingHoursBell>().AsTransient();
			base.Bind<WorkplaceBonuses>().AsTransient();
			base.Bind<WorkplacePriority>().AsTransient();
			base.Bind<WorkplaceSlotManager>().AsTransient();
			base.Bind<WorkplaceWorkerType>().AsTransient();
			base.Bind<WorkplaceWorkingHours>().AsTransient();
			base.Bind<WorkRefuser>().AsTransient();
			base.Bind<TimerMetricCache<WorkplaceBehavior>>().AsTransient();
			base.Bind<WorkingHoursManager>().AsSingleton();
			base.Bind<UnlockableWorkerTypeSerializer>().AsSingleton();
			base.Bind<WorkplaceUnlockingService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WorkSystemConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005364 File Offset: 0x00003564
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<DistrictCenter, DistrictWorkplaceAssigner>();
			builder.AddDecorator<DistrictCenter, DistrictDefaultWorkerType>();
			builder.AddDecorator<WorkplaceSpec, Workplace>();
			builder.AddDecorator<Workplace, NothingToDoInRangeStatus>();
			builder.AddDecorator<Workplace, WorkplacePriority>();
			builder.AddDecorator<Workplace, WorkplaceWorkerType>();
			builder.AddDecorator<WorkerSpec, Worker>();
			builder.AddDecorator<Worker, WorkRefuser>();
			builder.AddDecorator<WorkplaceSlotManagerSpec, WorkplaceSlotManager>();
			builder.AddDecorator<WorkplaceSlotManager, SlotManager>();
			builder.AddDecorator<Workplace, WorkplaceWorkingHours>();
			builder.AddDecorator<Worker, WorkerWorkingHours>();
			builder.AddDecorator<Worker, UnreachableWorkplaceUnassigner>();
			builder.AddDecorator<WorkplaceBonusesSpec, WorkplaceBonuses>();
			builder.AddDecorator<WorkingHoursBellSpec, WorkingHoursBell>();
			builder.AddDecorator<WorkingHoursBell, BuildingSoundController>();
			return builder.Build();
		}
	}
}
