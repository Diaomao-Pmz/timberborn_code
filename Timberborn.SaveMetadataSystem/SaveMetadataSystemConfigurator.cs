using System;
using Bindito.Core;

namespace Timberborn.SaveMetadataSystem
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	[Context("Game")]
	public class SaveMetadataSystemConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002357 File Offset: 0x00000557
		public override void Configure()
		{
			base.Bind<SaveMetadataSerializer>().AsSingleton();
			base.Bind<ModReferenceSerializer>().AsSingleton();
		}
	}
}
