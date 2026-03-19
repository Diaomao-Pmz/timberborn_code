using System;
using Bindito.Core;

namespace Timberborn.ReservableSystem
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	[Context("MapEditor")]
	public class ReservableSystemConfigurator : Configurator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002111 File Offset: 0x00000311
		public override void Configure()
		{
			base.Bind<WalkToReservableExecutor>().AsTransient();
			base.Bind<Reservable>().AsTransient();
		}
	}
}
