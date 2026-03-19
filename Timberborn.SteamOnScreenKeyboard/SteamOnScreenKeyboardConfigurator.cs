using System;
using Bindito.Core;
using Timberborn.CoreUI;

namespace Timberborn.SteamOnScreenKeyboard
{
	// Token: 0x02000004 RID: 4
	[Context("Game")]
	[Context("MapEditor")]
	[Context("MainMenu")]
	public class SteamOnScreenKeyboardConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.MultiBind<IVisualElementInitializer>().To<SteamOnScreenKeyboardController>().AsSingleton();
		}
	}
}
