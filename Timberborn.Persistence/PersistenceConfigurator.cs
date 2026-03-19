using System;
using Bindito.Core;

namespace Timberborn.Persistence
{
	// Token: 0x02000010 RID: 16
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class PersistenceConfigurator : Configurator
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000029E3 File Offset: 0x00000BE3
		public override void Configure()
		{
			base.Bind<InvariantDateTimeSerializer>().AsSingleton();
		}
	}
}
