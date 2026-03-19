using System;
using System.Collections.Generic;

namespace Timberborn.Localization
{
	// Token: 0x02000004 RID: 4
	public interface ILoc
	{
		// Token: 0x06000003 RID: 3
		void Initialize(Dictionary<string, string> localization);

		// Token: 0x06000004 RID: 4
		IEnumerable<string> GetRawTexts();

		// Token: 0x06000005 RID: 5
		string T(string key);

		// Token: 0x06000006 RID: 6
		string T<T1>(string key, T1 param1);

		// Token: 0x06000007 RID: 7
		string T<T1, T2>(string key, T1 param1, T2 param2);

		// Token: 0x06000008 RID: 8
		string T<T1, T2, T3>(string key, T1 param1, T2 param2, T3 param3);

		// Token: 0x06000009 RID: 9
		string T(Phrase phrase);

		// Token: 0x0600000A RID: 10
		string T<T1>(Phrase phrase, T1 param1);

		// Token: 0x0600000B RID: 11
		string T<T1, T2>(Phrase phrase, T1 param1, T2 param2);

		// Token: 0x0600000C RID: 12
		string T<T1, T2, T3>(Phrase phrase, T1 param1, T2 param2, T3 param3);
	}
}
