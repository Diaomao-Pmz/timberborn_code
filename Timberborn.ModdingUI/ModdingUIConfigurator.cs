using System;
using Bindito.Core;

namespace Timberborn.ModdingUI
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	public class ModdingUIConfigurator : Configurator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<ModListView>().AsSingleton();
		}
	}
}
