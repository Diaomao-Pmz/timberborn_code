using System;
using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x0200000C RID: 12
	[Context("Game")]
	[Context("MapEditor")]
	public class WaterSourceSystemUIConfigurator : Configurator
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000028C4 File Offset: 0x00000AC4
		public override void Configure()
		{
			base.Bind<WaterSettingFactory>().AsSingleton();
			base.Bind<WaterSourceFragment>().AsSingleton();
			base.Bind<WaterSourceRegulatorFragment>().AsSingleton();
			base.Bind<WaterSourceRegulatorToggleFactory>().AsSingleton();
			base.MultiBind<EntityPanelModule>().ToProvider<WaterSourceSystemUIConfigurator.EntityPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x0200000D RID: 13
		public class EntityPanelModuleProvider : IProvider<EntityPanelModule>
		{
			// Token: 0x06000036 RID: 54 RVA: 0x0000291A File Offset: 0x00000B1A
			public EntityPanelModuleProvider(WaterSourceFragment waterSourceFragment, WaterSourceRegulatorFragment waterSourceRegulatorFragment)
			{
				this._waterSourceFragment = waterSourceFragment;
				this._waterSourceRegulatorFragment = waterSourceRegulatorFragment;
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002930 File Offset: 0x00000B30
			public EntityPanelModule Get()
			{
				EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
				builder.AddTopFragment(this._waterSourceRegulatorFragment, 0);
				builder.AddMiddleFragment(this._waterSourceFragment, 0);
				return builder.Build();
			}

			// Token: 0x0400002F RID: 47
			public readonly WaterSourceFragment _waterSourceFragment;

			// Token: 0x04000030 RID: 48
			public readonly WaterSourceRegulatorFragment _waterSourceRegulatorFragment;
		}
	}
}
