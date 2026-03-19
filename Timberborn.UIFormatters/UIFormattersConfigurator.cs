using System;
using Bindito.Core;

namespace Timberborn.UIFormatters
{
	// Token: 0x02000008 RID: 8
	[Context("MainMenu")]
	[Context("Game")]
	[Context("MapEditor")]
	public class UIFormattersConfigurator : Configurator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002415 File Offset: 0x00000615
		public override void Configure()
		{
			base.Bind<ResourceAmountFormatter>().AsSingleton();
			base.Bind<DescribedAmountFactory>().AsSingleton();
			base.Bind<TimestampFormatter>().AsSingleton();
		}
	}
}
