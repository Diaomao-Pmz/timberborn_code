using System;
using Bindito.Core;

namespace Timberborn.InputSystemUI
{
	// Token: 0x0200000A RID: 10
	[Context("Game")]
	[Context("MapEditor")]
	public class InputSystemUIConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000237E File Offset: 0x0000057E
		public override void Configure()
		{
			base.Bind<BindableButtonFactory>().AsSingleton();
			base.Bind<BindableToggleFactory>().AsSingleton();
			base.Bind<KeywordMatchNotifier>().AsSingleton();
		}
	}
}
