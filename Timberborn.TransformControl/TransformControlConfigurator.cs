using System;
using Bindito.Core;

namespace Timberborn.TransformControl
{
	// Token: 0x02000007 RID: 7
	[Context("Game")]
	[Context("MapEditor")]
	public class TransformControlConfigurator : Configurator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021C4 File Offset: 0x000003C4
		public override void Configure()
		{
			base.Bind<TransformController>().AsTransient();
		}
	}
}
