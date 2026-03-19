using System;
using Bindito.Core;

namespace Timberborn.GridTraversing
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class GridTraversingConfigurator : Configurator
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002673 File Offset: 0x00000873
		public override void Configure()
		{
			base.Bind<GridTraversal>().AsSingleton();
		}
	}
}
