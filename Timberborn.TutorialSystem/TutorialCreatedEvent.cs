using System;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200000E RID: 14
	public class TutorialCreatedEvent
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000226C File Offset: 0x0000046C
		public TutorialConfiguration Configuration { get; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002274 File Offset: 0x00000474
		public TutorialCreatedEvent(TutorialConfiguration configuration)
		{
			this.Configuration = configuration;
		}
	}
}
