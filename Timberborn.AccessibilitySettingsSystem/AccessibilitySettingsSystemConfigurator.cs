using System;
using Bindito.Core;

namespace Timberborn.AccessibilitySettingsSystem
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class AccessibilitySettingsSystemConfigurator : Configurator
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002134 File Offset: 0x00000334
		public override void Configure()
		{
			base.Bind<AccessibilitySettings>().AsSingleton();
		}
	}
}
