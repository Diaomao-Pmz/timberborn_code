using System;
using Bindito.Core;
using Timberborn.Autosaving;

namespace Timberborn.AutosavingUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class AutosavingUIConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021CB File Offset: 0x000003CB
		public override void Configure()
		{
			base.Bind<AutosaveNotifier>().AsSingleton();
			base.MultiBind<IAutosaveBlocker>().To<SettingsAutosaveBlocker>().AsSingleton();
			base.MultiBind<IAutosaveBlocker>().To<PanelAutosaveBlocker>().AsSingleton();
		}
	}
}
