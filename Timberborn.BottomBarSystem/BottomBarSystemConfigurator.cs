using System;
using Bindito.Core;

namespace Timberborn.BottomBarSystem
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	[Context("MapEditor")]
	public class BottomBarSystemConfigurator : Configurator
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000025B7 File Offset: 0x000007B7
		public override void Configure()
		{
			base.Bind<BottomBarPanel>().AsSingleton();
		}
	}
}
