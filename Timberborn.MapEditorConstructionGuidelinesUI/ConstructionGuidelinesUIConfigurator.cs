using System;
using Bindito.Core;

namespace Timberborn.MapEditorConstructionGuidelinesUI
{
	// Token: 0x02000005 RID: 5
	[Context("MapEditor")]
	public class ConstructionGuidelinesUIConfigurator : Configurator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Configure()
		{
			base.Bind<MapEditorGuidelinesShower>().AsSingleton();
		}
	}
}
