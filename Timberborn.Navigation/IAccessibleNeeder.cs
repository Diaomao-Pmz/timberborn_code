using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000028 RID: 40
	public interface IAccessibleNeeder
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600010C RID: 268
		string AccessibleComponentName { get; }

		// Token: 0x0600010D RID: 269
		void SetAccessible(Accessible accessible);
	}
}
