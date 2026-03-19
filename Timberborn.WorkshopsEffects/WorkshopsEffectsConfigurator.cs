using System;
using Bindito.Core;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;
using Timberborn.Workshops;
using Timberborn.WorkSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000016 RID: 22
	[Context("Game")]
	public class WorkshopsEffectsConfigurator : Configurator
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000034A8 File Offset: 0x000016A8
		public override void Configure()
		{
			base.Bind<WorkshopSounds>().AsTransient();
			base.Bind<WorkshopWorkerHider>().AsTransient();
			base.Bind<ManufactoryProgressVisualizer>().AsTransient();
			base.Bind<ManufactoryRecipeVisualizer>().AsTransient();
			base.Bind<ObservatoryAnimator>().AsTransient();
			base.Bind<WorkerWorkshopSpeedNotifier>().AsTransient();
			base.Bind<WorkshopAnimationController>().AsTransient();
			base.Bind<WorkshopParticleController>().AsTransient();
			base.Bind<WorkshopWorker>().AsTransient();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(WorkshopsEffectsConfigurator.ProvideTemplateModule)).AsSingleton();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003540 File Offset: 0x00001740
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<Workshop, WorkshopSounds>();
			builder.AddDecorator<Worker, WorkshopWorker>();
			builder.AddDecorator<IWorkshopAnimationSpeedModifier, WorkerWorkshopSpeedNotifier>();
			builder.AddDecorator<ManufactoryProgressVisualizerSpec, ManufactoryProgressVisualizer>();
			builder.AddDecorator<ManufactoryRecipeVisualizerSpec, ManufactoryRecipeVisualizer>();
			builder.AddDecorator<ObservatoryAnimatorSpec, ObservatoryAnimator>();
			builder.AddDecorator<WorkshopParticleControllerSpec, WorkshopParticleController>();
			builder.AddDecorator<WorkshopParticleController, ParticlesCache>();
			builder.AddDecorator<WorkshopWorkerHiderSpec, WorkshopWorkerHider>();
			builder.AddDecorator<WorkshopAnimationControllerSpec, WorkshopAnimationController>();
			return builder.Build();
		}
	}
}
