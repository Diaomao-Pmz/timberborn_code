using System;
using Bindito.Core;
using Timberborn.Debugging;

namespace Timberborn.CharactersUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	public class CharactersUIConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000024C1 File Offset: 0x000006C1
		public override void Configure()
		{
			base.Bind<CharacterButtonFactory>().AsSingleton();
			base.Bind<CharacterBatchControlRowItemFactory>().AsSingleton();
			base.MultiBind<IDevModule>().To<CharactersModelToggler>().AsSingleton();
		}
	}
}
