using System;
using Bindito.Core;

namespace Timberborn.Intro
{
	// Token: 0x02000006 RID: 6
	[Context("MainMenu")]
	public class IntroConfigurator : Configurator
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022FC File Offset: 0x000004FC
		public override void Configure()
		{
			base.Bind<IntroBox>().AsSingleton();
		}
	}
}
