using System;

namespace Timberborn.CommandLine
{
	// Token: 0x02000006 RID: 6
	public interface ICommandLineArguments
	{
		// Token: 0x0600000B RID: 11
		bool Has(string key);

		// Token: 0x0600000C RID: 12
		int GetInt(string key);

		// Token: 0x0600000D RID: 13
		string GetString(string key);
	}
}
