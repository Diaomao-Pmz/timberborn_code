using System;
using Bindito.Core;

namespace Timberborn.SaveSystem
{
	// Token: 0x0200000A RID: 10
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class SaveSystemConfigurator : Configurator
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021F8 File Offset: 0x000003F8
		public override void Configure()
		{
			base.Bind<SaveReader>().AsSingleton();
			base.Bind<SaveWriter>().AsSingleton();
		}
	}
}
