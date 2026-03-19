using System;
using Bindito.Core;

namespace Timberborn.NotificationSystem
{
	// Token: 0x02000008 RID: 8
	[Context("Game")]
	public class NotificationSystemConfigurator : Configurator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002399 File Offset: 0x00000599
		public override void Configure()
		{
			base.Bind<NotificationBus>().AsSingleton();
			base.Bind<NotificationValueSerializer>().AsSingleton();
			base.Bind<NotificationSaver>().AsSingleton();
		}
	}
}
