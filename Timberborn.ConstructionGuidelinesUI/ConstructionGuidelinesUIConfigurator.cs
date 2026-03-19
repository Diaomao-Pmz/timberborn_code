using System;
using Bindito.Core;

namespace Timberborn.ConstructionGuidelinesUI
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	[Context("MapEditor")]
	public class ConstructionGuidelinesUIConfigurator : Configurator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		public override void Configure()
		{
			base.Bind<ConstructionGuidelinesTogglePanel>().AsSingleton();
			base.Bind<ConstructionModeGuidelinesShower>().AsSingleton();
		}
	}
}
