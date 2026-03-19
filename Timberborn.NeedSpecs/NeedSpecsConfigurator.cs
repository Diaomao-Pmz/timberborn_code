using System;
using Bindito.Core;

namespace Timberborn.NeedSpecs
{
	// Token: 0x02000014 RID: 20
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class NeedSpecsConfigurator : Configurator
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000039ED File Offset: 0x00001BED
		public override void Configure()
		{
			base.Bind<NeedGroupSpecService>().AsSingleton();
			base.Bind<NeedSpecFormatter>().AsSingleton();
		}
	}
}
