using System;
using Bindito.Core;

namespace Timberborn.WalkingSystemUI
{
	// Token: 0x02000009 RID: 9
	[Context("Game")]
	public class WalkingSystemUIConfigurator : Configurator
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002789 File Offset: 0x00000989
		public override void Configure()
		{
			base.Bind<WalkerDebugger>().AsSingleton();
		}
	}
}
