using System;
using Bindito.Core;

namespace Timberborn.NotificationSystemUI
{
	// Token: 0x02000006 RID: 6
	[Context("Game")]
	public class NotificationSystemUIConfigurator : Configurator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000243B File Offset: 0x0000063B
		public override void Configure()
		{
			base.Bind<NotificationPanel>().AsSingleton();
		}
	}
}
