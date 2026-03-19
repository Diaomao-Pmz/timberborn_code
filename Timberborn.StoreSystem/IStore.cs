using System;

namespace Timberborn.StoreSystem
{
	// Token: 0x02000004 RID: 4
	public interface IStore
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		bool GameIsAllowedToStart { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		string Language { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5
		string ShortUpdateUrl { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6
		string FullUpdateUrl { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7
		string UpdateInfoTextLocKey { get; }

		// Token: 0x06000008 RID: 8
		string GetCompatibilityMessage();
	}
}
