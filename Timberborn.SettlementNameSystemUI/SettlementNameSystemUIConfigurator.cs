using System;
using Bindito.Core;
using Timberborn.GameStartup;

namespace Timberborn.SettlementNameSystemUI
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	public class SettlementNameSystemUIConfigurator : Configurator
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023FD File Offset: 0x000005FD
		public override void Configure()
		{
			base.Bind<ISettlementNamePromptShower>().To<SettlementNameBoxShower>().AsSingleton();
		}
	}
}
