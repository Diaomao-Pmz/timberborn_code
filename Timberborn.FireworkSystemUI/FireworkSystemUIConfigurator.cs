using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;
using Timberborn.FireworkSystem;
using Timberborn.TemplateInstantiation;

namespace Timberborn.FireworkSystemUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class FireworkSystemUIConfigurator : Configurator
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002654 File Offset: 0x00000854
		public override void Configure()
		{
			base.Bind<FireworkIdDropdownProvider>().AsTransient();
			base.Bind<FireworkLauncherFragment>().AsSingleton();
			base.Bind<FireworkLauncherInventoryFragment>().AsSingleton();
			base.MultiBind<TemplateModule>().ToProvider(new Func<TemplateModule>(FireworkSystemUIConfigurator.ProvideTemplateModule)).AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<FireworkSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026B3 File Offset: 0x000008B3
		public static TemplateModule ProvideTemplateModule()
		{
			TemplateModule.Builder builder = new TemplateModule.Builder();
			builder.AddDecorator<FireworkLauncher, FireworkIdDropdownProvider>();
			return builder.Build();
		}

		// Token: 0x02000008 RID: 8
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x0600001E RID: 30 RVA: 0x000026CD File Offset: 0x000008CD
			public EntityPanelModuleProvider(FireworkLauncherFragment fireworkLauncherFragment, FireworkLauncherInventoryFragment fireworkLauncherInventoryFragment)
			{
				this._fireworkLauncherFragment = fireworkLauncherFragment;
				this._fireworkLauncherInventoryFragment = fireworkLauncherInventoryFragment;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000026E3 File Offset: 0x000008E3
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddMiddleFragment(this._fireworkLauncherFragment, 0);
				builder.AddBottomFragment(this._fireworkLauncherInventoryFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400001F RID: 31
			public readonly FireworkLauncherFragment _fireworkLauncherFragment;

			// Token: 0x04000020 RID: 32
			public readonly FireworkLauncherInventoryFragment _fireworkLauncherInventoryFragment;
		}
	}
}
