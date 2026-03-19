using System;
using Bindito.Core;
using Timberborn.Options;

namespace Timberborn.OptionsGame
{
	// Token: 0x02000005 RID: 5
	[Context("Game")]
	public class OptionsGameConfigurator : Configurator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002367 File Offset: 0x00000567
		public override void Configure()
		{
			base.Bind<IOptionsBox>().To<GameOptionsBox>().AsSingleton();
		}
	}
}
