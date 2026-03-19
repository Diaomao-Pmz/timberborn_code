using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.NavigationUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class NavigationUIConfigurator : Configurator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002106 File Offset: 0x00000306
		public override void Configure()
		{
			base.Bind<NavigationDebuggingPanel>().AsSingleton();
			base.MultiBind<IDevModule>().To<NavMeshDrawerController>().AsSingleton();
		}
	}
}
