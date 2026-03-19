using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000041 RID: 65
	public interface IBlockObjectDeletionBlocker
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D1 RID: 465
		bool NoForcedDelete { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D2 RID: 466
		bool IsStackedDeletionBlocked { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D3 RID: 467
		bool IsDeletionBlocked { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D4 RID: 468
		string ReasonLocKey { get; }
	}
}
