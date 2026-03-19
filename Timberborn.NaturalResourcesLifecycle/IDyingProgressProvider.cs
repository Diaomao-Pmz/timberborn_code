using System;

namespace Timberborn.NaturalResourcesLifecycle
{
	// Token: 0x02000006 RID: 6
	public interface IDyingProgressProvider
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000017 RID: 23
		// (remove) Token: 0x06000018 RID: 24
		event EventHandler StartedDying;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000019 RID: 25
		// (remove) Token: 0x0600001A RID: 26
		event EventHandler StoppedDying;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27
		DyingProgress DyingProgress { get; }
	}
}
