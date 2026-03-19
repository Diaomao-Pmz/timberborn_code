using System;
using Bindito.Core;
using Timberborn.Emptying;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.Particles;
using Timberborn.TemplateInstantiation;
using Timberborn.Workshops;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000014 RID: 20
	[Context("Game")]
	public class FireworkSystemConfigurator : Configurator
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003614 File Offset: 0x00001814
		public override void Configure()
		{
			base.Bind<Firework>().AsTransient();
			base.Bind<FireworkLauncher>().AsTransient();
			base.Bind<FireworkLauncherModel>().AsTransient();
			base.Bind<FireworkLauncherStatus>().AsTransient();
			base.Bind<FireworkSpawner>().AsSingleton();
			base.Bind<FireworkLaunchService>().AsSingleton();
			base.Bind<FireworkSpecService>().AsSingleton();
			base.Bind<FireworkLauncherInventoryInitializer>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider<FireworkSystemConfigurator.TemplateModuleProvider>().AsSingleton();
		}

		// Token: 0x02000015 RID: 21
		public class TemplateModuleProvider : IProvider<TemplateModule>
		{
			// Token: 0x06000087 RID: 135 RVA: 0x0000369A File Offset: 0x0000189A
			public TemplateModuleProvider(FireworkLauncherInventoryInitializer fireworkLauncherInventoryInitializer)
			{
				this._fireworkLauncherInventoryInitializer = fireworkLauncherInventoryInitializer;
			}

			// Token: 0x06000088 RID: 136 RVA: 0x000036AC File Offset: 0x000018AC
			public TemplateModule Get()
			{
				TemplateModule.Builder builder = new TemplateModule.Builder();
				builder.AddDecorator<FireworkSpec, Firework>();
				builder.AddDecorator<Firework, ParticlesCache>();
				builder.AddDecorator<FireworkLauncherSpec, FireworkLauncher>();
				builder.AddDecorator<FireworkLauncher, FireworkLauncherModel>();
				builder.AddDecorator<FireworkLauncher, FireworkLauncherStatus>();
				builder.AddDecorator<FireworkLauncher, AutoEmptiable>();
				builder.AddDecorator<FireworkLauncher, NoHaulingPostStatus>();
				builder.AddDecorator<FireworkLauncher, LackOfResourcesStatus>();
				builder.AddDecorator<FireworkLauncher, Emptiable>();
				builder.AddDecorator<FireworkLauncher, FillInputHaulBehaviorProvider>();
				builder.AddDecorator<FireworkLauncher, EmptyInventoriesWorkplaceBehavior>();
				builder.AddDecorator<FireworkLauncher, FillInputWorkplaceBehavior>();
				builder.AddDecorator<FireworkLauncher, RemoveUnwantedStockWorkplaceBehavior>();
				builder.AddDedicatedDecorator<FireworkLauncher, Inventory>(this._fireworkLauncherInventoryInitializer);
				return builder.Build();
			}

			// Token: 0x04000060 RID: 96
			public readonly FireworkLauncherInventoryInitializer _fireworkLauncherInventoryInitializer;
		}
	}
}
