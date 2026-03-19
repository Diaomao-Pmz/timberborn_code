using System;
using Bindito.Core;
using Timberborn.BuildingsNavigation;
using Timberborn.Emptying;
using Timberborn.Illumination;
using Timberborn.InventorySystem;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;
using Timberborn.Workshops;
using Timberborn.WorkSystem;

namespace Timberborn.Wonders
{
	// Token: 0x0200001F RID: 31
	[Context("Game")]
	public class WondersConfigurator : Configurator
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003678 File Offset: 0x00001878
		public override void Configure()
		{
			base.Bind<WaitForInactiveWonderWorkplaceBehavior>().AsTransient();
			base.Bind<AlreadyActivatedWonderBlocker>().AsTransient();
			base.Bind<BuildingBlockedWonderBlocker>().AsTransient();
			base.Bind<NotEnoughWorkersWonderBlocker>().AsTransient();
			base.Bind<UnreachableBuildingWonderBlocker>().AsTransient();
			base.Bind<Wonder>().AsTransient();
			base.Bind<WonderAnimationController>().AsTransient();
			base.Bind<WonderAccessProvider>().AsTransient();
			base.Bind<WonderDeactivationTimer>().AsTransient();
			base.Bind<WonderEffectBuildingDescriber>().AsTransient();
			base.Bind<WonderEffectController>().AsTransient();
			base.Bind<WonderInputChecker>().AsTransient();
			base.Bind<WonderInventory>().AsTransient();
			base.Bind<WonderIlluminator>().AsTransient();
			base.Bind<WonderParticleController>().AsTransient();
			base.Bind<WonderUnselector>().AsTransient();
			base.Bind<WonderInventoryInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<WondersConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000020 RID: 32
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x060000C2 RID: 194 RVA: 0x0000376A File Offset: 0x0000196A
			public TemplateModuleProvider(WonderInventoryInitializer wonderInventoryInitializer)
			{
				this._wonderInventoryInitializer = wonderInventoryInitializer;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x0000377C File Offset: 0x0000197C
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				WondersConfigurator.TemplateModuleProvider.InitializeBehaviors(builder);
				builder.AddDedicatedDecorator<WonderInventory, Inventory>(this._wonderInventoryInitializer);
				builder.AddDecorator<WonderSpec, Wonder>();
				builder.AddDecorator<Wonder, WonderUnselector>();
				builder.AddDecorator<Wonder, WonderAnimationController>();
				builder.AddDecorator<Wonder, WonderAccessProvider>();
				builder.AddDecorator<Wonder, AlreadyActivatedWonderBlocker>();
				builder.AddDecorator<Wonder, BuildingBlockedWonderBlocker>();
				builder.AddDecorator<Wonder, UnreachableBuildingWonderBlocker>();
				builder.AddDecorator<Wonder, PathMeshHider>();
				builder.AddDecorator<WonderEffectControllerSpec, WonderEffectController>();
				builder.AddDecorator<WonderEffectController, WonderEffectBuildingDescriber>();
				builder.AddDecorator<WonderInventorySpec, WonderInventory>();
				builder.AddDecorator<WonderInventory, WonderInputChecker>();
				builder.AddDecorator<WonderInputChecker, LackOfResourcesStatus>();
				builder.AddDecorator<WonderIlluminator, Illuminator>();
				builder.AddDecorator<WonderDeactivationTimerSpec, WonderDeactivationTimer>();
				builder.AddDecorator<WonderParticleControllerSpec, WonderParticleController>();
				builder.AddDecorator<WonderParticleController, ParticlesCache>();
				return builder.Build();
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x0000380B File Offset: 0x00001A0B
			public static void InitializeBehaviors(TemplateModule.Builder builder)
			{
				builder.AddDecorator<Wonder, WaitForInactiveWonderWorkplaceBehavior>();
				builder.AddDecorator<Wonder, FillInputWorkplaceBehavior>();
				builder.AddDecorator<Wonder, RemoveUnwantedStockWorkplaceBehavior>();
				builder.AddDecorator<Wonder, WaitInsideIdlyWorkplaceBehavior>();
			}

			// Token: 0x04000050 RID: 80
			public readonly WonderInventoryInitializer _wonderInventoryInitializer;
		}
	}
}
