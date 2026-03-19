using System;
using Bindito.Core;

namespace Timberborn.MapEditorConstructionGuidelinesUI
{
	// Token: 0x02000004 RID: 4
	[Context("MapEditor")]
	internal class ConstructionGuidelinesUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		protected override void Configure()
		{
			base.Bind<MapEditorGuidelinesShower>().AsSingleton();
		}
	}
}
