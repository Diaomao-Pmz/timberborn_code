using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.CommandLine;
using UnityEngine;

namespace Timberborn.SettingsSystem
{
	// Token: 0x02000006 RID: 6
	public class Settings : ISettings
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000020D5 File Offset: 0x000002D5
		public Settings(ICommandLineArguments commandLineArguments)
		{
			this._commandLineArguments = commandLineArguments;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020EF File Offset: 0x000002EF
		public int GetInt(string key, int defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000020F8 File Offset: 0x000002F8
		public int GetSafeInt(string key, int defaultValue)
		{
			this.DeleteIfSafeMode(key);
			return this.GetInt(key, defaultValue);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002109 File Offset: 0x00000309
		public void SetInt(string key, int value)
		{
			this._touchedSafeKeys.Add(key);
			PlayerPrefs.SetInt(key, value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002120 File Offset: 0x00000320
		public bool GetBool(string key, bool defaultValue = false)
		{
			int num = defaultValue ? 1 : 0;
			return PlayerPrefs.GetInt(key, num) == 1;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000213F File Offset: 0x0000033F
		public bool GetSafeBool(string key, bool defaultValue = false)
		{
			this.DeleteIfSafeMode(key);
			return this.GetBool(key, defaultValue);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002150 File Offset: 0x00000350
		public void SetBool(string key, bool value)
		{
			this._touchedSafeKeys.Add(key);
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000216C File Offset: 0x0000036C
		public float GetFloat(string key, float defaultValue)
		{
			return PlayerPrefs.GetFloat(key, defaultValue);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002175 File Offset: 0x00000375
		public float GetSafeFloat(string key, float defaultValue)
		{
			this.DeleteIfSafeMode(key);
			return this.GetFloat(key, defaultValue);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002186 File Offset: 0x00000386
		public void SetFloat(string key, float value)
		{
			this._touchedSafeKeys.Add(key);
			PlayerPrefs.SetFloat(key, value);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000219C File Offset: 0x0000039C
		public string GetString(string key, string defaultValue)
		{
			return PlayerPrefs.GetString(key, defaultValue);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021A5 File Offset: 0x000003A5
		public string GetSafeString(string key, string defaultValue)
		{
			this.DeleteIfSafeMode(key);
			return this.GetString(key, defaultValue);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021B6 File Offset: 0x000003B6
		public void SetString(string key, string value)
		{
			this._touchedSafeKeys.Add(key);
			PlayerPrefs.SetString(key, value);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000021CC File Offset: 0x000003CC
		public void Clear(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000021D4 File Offset: 0x000003D4
		public void ValidateInt(string key, ImmutableArray<int> validValues, int defaultValue)
		{
			int @int = this.GetInt(key, defaultValue);
			if (validValues.IndexOf(@int) == -1)
			{
				Debug.LogWarning(string.Format("Invalid setting value for key\"{0}\": {1}. Changing to {2}", key, @int, defaultValue));
				this.SetInt(key, defaultValue);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000221C File Offset: 0x0000041C
		public void ValidateString(string key, ImmutableArray<string> validValues, string defaultValue)
		{
			string safeString = this.GetSafeString(key, defaultValue);
			if (validValues.IndexOf(safeString) == -1)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Invalid setting value for key\"",
					key,
					"\": ",
					safeString,
					". Changing to ",
					defaultValue
				}));
				this.SetString(key, defaultValue);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002279 File Offset: 0x00000479
		public void DeleteIfSafeMode(string key)
		{
			if (this._commandLineArguments.Has(Settings.SafeModeKey) && this._touchedSafeKeys.Add(key))
			{
				this.Clear(key);
			}
		}

		// Token: 0x04000007 RID: 7
		public static readonly string SafeModeKey = "safe";

		// Token: 0x04000008 RID: 8
		public readonly ICommandLineArguments _commandLineArguments;

		// Token: 0x04000009 RID: 9
		public readonly HashSet<string> _touchedSafeKeys = new HashSet<string>();
	}
}
