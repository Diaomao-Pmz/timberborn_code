using System;
using Bindito.Core;

namespace Timberborn.CameraSettingsSystem
{
	// Token: 0x02000005 RID: 5
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class CameraSettingsSystemConfigurator : Configurator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		public override void Configure()
		{
			base.Bind<CameraSettings>().AsSingleton();
		}
	}
}
