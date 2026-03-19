using System;
using Bindito.Core;
using Timberborn.CoreUI;

namespace Timberborn.DropdownSystem
{
	// Token: 0x0200000F RID: 15
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class DropdownSystemConfigurator : Configurator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002EA8 File Offset: 0x000010A8
		public override void Configure()
		{
			base.Bind<DropdownItemsSetter>().AsSingleton();
			base.Bind<DropdownListDrawer>().AsSingleton();
			base.Bind<EnumDropdownProviderFactory>().AsSingleton();
			base.MultiBind<IVisualElementInitializer>().To<DropdownInitializer>().AsSingleton();
		}
	}
}
