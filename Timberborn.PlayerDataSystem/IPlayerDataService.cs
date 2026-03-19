using System;

namespace Timberborn.PlayerDataSystem
{
	// Token: 0x02000004 RID: 4
	public interface IPlayerDataService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		bool DataLoadSuccessful { get; }

		// Token: 0x06000004 RID: 4
		bool HasKey(string key);

		// Token: 0x06000005 RID: 5
		bool GetBool(string key, bool defaultValue);

		// Token: 0x06000006 RID: 6
		string GetString(string key, string defaultValue);

		// Token: 0x06000007 RID: 7
		void SetBool(string key, bool value);

		// Token: 0x06000008 RID: 8
		void SetString(string key, string value);

		// Token: 0x06000009 RID: 9
		void Remove(string key);

		// Token: 0x0600000A RID: 10
		void RemoveAll();
	}
}
