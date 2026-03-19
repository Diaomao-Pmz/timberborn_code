using System;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000026 RID: 38
	public interface IConstructionSiteValidator
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000FF RID: 255
		// (remove) Token: 0x06000100 RID: 256
		event EventHandler ValidationStateChanged;

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000101 RID: 257
		bool IsValid { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000102 RID: 258
		bool IsModelValid { get; }

		// Token: 0x06000103 RID: 259
		void Validate();
	}
}
