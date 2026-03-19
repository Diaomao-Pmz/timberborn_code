using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000008 RID: 8
	public class DynamicStatusAlertAddedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000245E File Offset: 0x0000065E
		public StatusInstance StatusInstance { get; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002466 File Offset: 0x00000666
		public DynamicStatusAlertAddedEvent(StatusInstance statusInstance)
		{
			this.StatusInstance = statusInstance;
		}
	}
}
