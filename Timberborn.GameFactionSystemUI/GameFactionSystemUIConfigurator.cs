using System;
using Bindito.Core;
using Timberborn.AlertPanelSystem;

namespace Timberborn.GameFactionSystemUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class GameFactionSystemUIConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021DB File Offset: 0x000003DB
		public override void Configure()
		{
			base.Bind<FactionUnlockedAlertFragment>().AsSingleton();
			base.MultiBind<AlertPanelModule>().ToProvider<GameFactionSystemUIConfigurator.AlertPanelModuleProvider>().AsSingleton();
		}

		// Token: 0x02000006 RID: 6
		public class AlertPanelModuleProvider : IProvider<AlertPanelModule>
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00002202 File Offset: 0x00000402
			public AlertPanelModuleProvider(FactionUnlockedAlertFragment factionUnlockedAlertFragment)
			{
				this._factionUnlockedAlertFragment = factionUnlockedAlertFragment;
			}

			// Token: 0x0600000C RID: 12 RVA: 0x00002211 File Offset: 0x00000411
			public AlertPanelModule Get()
			{
				AlertPanelModule.Builder builder = new AlertPanelModule.Builder();
				builder.AddAlertFragment(this._factionUnlockedAlertFragment, 2);
				return builder.Build();
			}

			// Token: 0x0400000D RID: 13
			public readonly FactionUnlockedAlertFragment _factionUnlockedAlertFragment;
		}
	}
}
