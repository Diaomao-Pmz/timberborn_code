using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.Illumination;
using Timberborn.TemplateInstantiation;
using Timberborn.WellbeingUI;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000025 RID: 37
	[Context("Game")]
	public class WorkSystemUIConfigurator : Configurator
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003E88 File Offset: 0x00002088
		public override void Configure()
		{
			base.Bind<WorkplaceIlluminator>().AsTransient();
			base.Bind<NoUnemployedStatus>().AsTransient();
			base.Bind<WorkerTypeIlluminator>().AsTransient();
			base.Bind<WorkplaceBonusesDescriber>().AsTransient();
			base.Bind<WorkplaceDescriber>().AsTransient();
			base.Bind<WorkingHoursPanel>().AsSingleton();
			base.Bind<WorkerViewFactory>().AsSingleton();
			base.Bind<WorkplaceFragment>().AsSingleton();
			base.Bind<WorkplaceBatchControlRowItemFactory>().AsSingleton();
			base.Bind<WorkplacePrioritySpriteLoader>().AsSingleton();
			base.Bind<WorkplacePriorityToggleGroupFactory>().AsSingleton();
			base.Bind<WorkplacePriorityBatchControlRowItemFactory>().AsSingleton();
			base.Bind<WorkplaceWorkerTypeBatchControlRowItemFactory>().AsSingleton();
			base.Bind<WorkerTypeToggleFactory>().AsSingleton();
			base.Bind<WorkplaceUnlockingDialogService>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WorkSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WorkSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<INeedEffectDescriber>().To<WorkSystemNeedEffectDescriber>().AsSingleton();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F88 File Offset: 0x00002188
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Workplace, WorkplaceDescriber>();
			builder.AddDecorator<Workplace, NoUnemployedStatus>();
			builder.AddDecorator<WorkplaceBonuses, WorkplaceBonusesDescriber>();
			builder.AddDecorator<WorkplaceIlluminatorSpec, WorkplaceIlluminator>();
			builder.AddDecorator<WorkplaceIlluminator, WorkerTypeIlluminator>();
			builder.AddDecorator<WorkplaceIlluminator, Illuminator>();
			return builder.Build();
		}

		// Token: 0x02000026 RID: 38
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x060000B2 RID: 178 RVA: 0x00003FC0 File Offset: 0x000021C0
			public EntityPanelModuleProvider(WorkplaceFragment workplaceFragment)
			{
				this._workplaceFragment = workplaceFragment;
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x00003FCF File Offset: 0x000021CF
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._workplaceFragment, 0);
				return builder.Build();
			}

			// Token: 0x040000A4 RID: 164
			public readonly WorkplaceFragment _workplaceFragment;
		}
	}
}
