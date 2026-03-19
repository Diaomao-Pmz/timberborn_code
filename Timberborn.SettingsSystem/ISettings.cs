using System;
using System.Collections.Immutable;

namespace Timberborn.SettingsSystem
{
	// Token: 0x02000004 RID: 4
	public interface ISettings
	{
		// Token: 0x06000003 RID: 3
		int GetInt(string key, int defaultValue);

		// Token: 0x06000004 RID: 4
		int GetSafeInt(string key, int defaultValue);

		// Token: 0x06000005 RID: 5
		void SetInt(string key, int value);

		// Token: 0x06000006 RID: 6
		bool GetBool(string key, bool defaultValue = false);

		// Token: 0x06000007 RID: 7
		bool GetSafeBool(string key, bool defaultValue = false);

		// Token: 0x06000008 RID: 8
		void SetBool(string key, bool value);

		// Token: 0x06000009 RID: 9
		float GetFloat(string key, float defaultValue);

		// Token: 0x0600000A RID: 10
		float GetSafeFloat(string key, float defaultValue);

		// Token: 0x0600000B RID: 11
		void SetFloat(string key, float value);

		// Token: 0x0600000C RID: 12
		string GetString(string key, string defaultValue);

		// Token: 0x0600000D RID: 13
		string GetSafeString(string key, string defaultValue);

		// Token: 0x0600000E RID: 14
		void SetString(string key, string value);

		// Token: 0x0600000F RID: 15
		void Clear(string key);

		// Token: 0x06000010 RID: 16
		void ValidateInt(string key, ImmutableArray<int> validValues, int defaultValue);

		// Token: 0x06000011 RID: 17
		void ValidateString(string key, ImmutableArray<string> validValues, string defaultValue);
	}
}
