using System;
using System.Collections.Generic;

namespace Timberborn.ErrorReporting
{
	// Token: 0x0200000A RID: 10
	public interface ILoadingIssueService
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42
		bool HasAnyIssues { get; }

		// Token: 0x0600002B RID: 43
		IEnumerable<ValueTuple<LoadingIssueMessage, int>> GetIssues();

		// Token: 0x0600002C RID: 44
		void AddIssue(string warningText, string messageLocKey, string messageParam = null, bool paramIsLocKey = false);
	}
}
