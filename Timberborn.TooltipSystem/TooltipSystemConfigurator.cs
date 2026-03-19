using System;
using Bindito.Core;

namespace Timberborn.TooltipSystem
{
	// Token: 0x02000015 RID: 21
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class TooltipSystemConfigurator : Configurator
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002B70 File Offset: 0x00000D70
		public override void Configure()
		{
			base.Bind<MouseTooltipPositioner>().AsSingleton();
			base.Bind<TooltipBlocker>().AsSingleton();
			base.Bind<ITooltipRegistrar>().To<TooltipRegistrar>().AsSingleton();
			base.Bind<Tooltip>().AsSingleton();
			base.Bind<TooltipContainer>().AsSingleton();
		}
	}
}
