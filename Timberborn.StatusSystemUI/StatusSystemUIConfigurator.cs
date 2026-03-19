using System;
using Bindito.Core;
using Timberborn.AlertPanelSystem;
using Timberborn.Debugging;
using Timberborn.EntityPanelSystem;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	public class StatusSystemUIConfigurator : Configurator
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002D4C File Offset: 0x00000F4C
		public override void Configure()
		{
			base.Bind<StatusAlertFragmentRowFactory>().AsTransient();
			base.Bind<StatusBatchControlRowItemFactory>().AsSingleton();
			base.Bind<StatusListFragment>().AsSingleton();
			base.Bind<StatusAlertFragment>().AsSingleton();
			base.Bind<DynamicStatusAlertFragment>().AsSingleton();
			base.Bind<StatusAlertRowBlinker>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<StatusSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
			base.MultiBind<AlertPanelModule>().ToProvider<StatusSystemUIConfigurator.AlertPanelModuleProvider>().AsSingleton();
			base.MultiBind<IDevModule>().To<StatusSystemSlotsDrawer>().AsSingleton();
		}

		// Token: 0x02000010 RID: 16
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000044 RID: 68 RVA: 0x00002DDC File Offset: 0x00000FDC
			public EntityPanelModuleProvider(StatusListFragment statusListFragment)
			{
				this._statusListFragment = statusListFragment;
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00002DEB File Offset: 0x00000FEB
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddFooterFragment(this._statusListFragment, 0);
				return builder.Build();
			}

			// Token: 0x04000038 RID: 56
			public readonly StatusListFragment _statusListFragment;
		}

		// Token: 0x02000011 RID: 17
		public class AlertPanelModuleProvider : IProvider<AlertPanelModule>
		{
			// Token: 0x06000046 RID: 70 RVA: 0x00002E04 File Offset: 0x00001004
			public AlertPanelModuleProvider(StatusAlertFragment statusAlertFragment, DynamicStatusAlertFragment dynamicStatusAlertFragment)
			{
				this._statusAlertFragment = statusAlertFragment;
				this._dynamicStatusAlertFragment = dynamicStatusAlertFragment;
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00002E1A File Offset: 0x0000101A
			public AlertPanelModule Get()
			{
				AlertPanelModule.Builder builder = new AlertPanelModule.Builder();
				builder.AddAlertFragment(this._statusAlertFragment, 4);
				builder.AddAlertFragment(this._dynamicStatusAlertFragment, 3);
				return builder.Build();
			}

			// Token: 0x04000039 RID: 57
			public readonly StatusAlertFragment _statusAlertFragment;

			// Token: 0x0400003A RID: 58
			public readonly DynamicStatusAlertFragment _dynamicStatusAlertFragment;
		}
	}
}
