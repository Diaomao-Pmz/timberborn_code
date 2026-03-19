using System;
using Bindito.Core;

namespace Timberborn.UndoSystem
{
	// Token: 0x0200000A RID: 10
	[Context("MapEditor")]
	public class UndoSystemConfigurator : Configurator
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002368 File Offset: 0x00000568
		public override void Configure()
		{
			base.Bind<IUndoRegistry>().To<UndoRegistry>().AsSingleton();
		}
	}
}
