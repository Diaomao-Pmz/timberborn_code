using System;
using Bindito.Core;

namespace Timberborn.LanguageUI
{
	// Token: 0x02000009 RID: 9
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class LanguageUIConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002611 File Offset: 0x00000811
		public override void Configure()
		{
			base.Bind<ChangeLanguageBox>().AsSingleton();
		}
	}
}
