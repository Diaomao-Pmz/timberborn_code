using System;
using Bindito.Core;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x0200000F RID: 15
	[Context("Game")]
	[Context("MapEditor")]
	public class ToolButtonSystemConfigurator : Configurator
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002C6F File Offset: 0x00000E6F
		public override void Configure()
		{
			base.Bind<ToolButtonFactory>().AsSingleton();
			base.Bind<ToolButtonService>().AsSingleton();
			base.Bind<ToolGroupButtonFactory>().AsSingleton();
			base.Bind<ToolButtonSelector>().AsSingleton();
			base.Bind<ToolbarButtonRetriever>().AsSingleton();
		}
	}
}
