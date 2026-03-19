using System;
using System.Collections.Generic;

namespace Timberborn.DropdownSystem
{
	// Token: 0x02000013 RID: 19
	public interface IDropdownProvider
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005B RID: 91
		IReadOnlyList<string> Items { get; }

		// Token: 0x0600005C RID: 92
		string GetValue();

		// Token: 0x0600005D RID: 93
		void SetValue(string value);
	}
}
