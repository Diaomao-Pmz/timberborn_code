using System;
using UnityEngine;

namespace Timberborn.Modding
{
	// Token: 0x02000013 RID: 19
	public static class ModPlayerPrefsHelper
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public static bool IsModEnabled(Mod mod)
		{
			string modEnabledKey = ModPlayerPrefsHelper.GetModEnabledKey(mod);
			return !PlayerPrefs.HasKey(modEnabledKey) || PlayerPrefs.GetInt(modEnabledKey) == 1;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D10 File Offset: 0x00000F10
		public static bool IsModEnabled(ModDirectory modDirectory, ModManifest modManifest)
		{
			string modEnabledKey = ModPlayerPrefsHelper.GetModEnabledKey(modDirectory, modManifest);
			return !PlayerPrefs.HasKey(modEnabledKey) || PlayerPrefs.GetInt(modEnabledKey) == 1;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D38 File Offset: 0x00000F38
		public static void ToggleMod(bool enabled, Mod mod)
		{
			PlayerPrefs.SetInt(ModPlayerPrefsHelper.GetModEnabledKey(mod), enabled ? 1 : 0);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static int GetModPriority(Mod mod)
		{
			string modPriorityKey = ModPlayerPrefsHelper.GetModPriorityKey(mod);
			if (!PlayerPrefs.HasKey(modPriorityKey))
			{
				return 0;
			}
			return PlayerPrefs.GetInt(modPriorityKey);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D70 File Offset: 0x00000F70
		public static void IncreaseModPriority(Mod mod)
		{
			ModPlayerPrefsHelper.SetModPriority(mod, ModPlayerPrefsHelper.GetModPriority(mod) + 1);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D80 File Offset: 0x00000F80
		public static void DecreaseModPriority(Mod mod)
		{
			ModPlayerPrefsHelper.SetModPriority(mod, ModPlayerPrefsHelper.GetModPriority(mod) - 1);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D90 File Offset: 0x00000F90
		public static void SetModPriority(Mod mod, int priority)
		{
			PlayerPrefs.SetInt(ModPlayerPrefsHelper.GetModPriorityKey(mod), priority);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D9E File Offset: 0x00000F9E
		public static void ResetModPriority(Mod mod)
		{
			PlayerPrefs.DeleteKey(ModPlayerPrefsHelper.GetModPriorityKey(mod));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DAB File Offset: 0x00000FAB
		public static string GetModEnabledKey(Mod mod)
		{
			return string.Format(ModPlayerPrefsHelper.ModEnabledFormat, ModPlayerPrefsHelper.GetModKey(mod.ModDirectory, mod.Manifest));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public static string GetModEnabledKey(ModDirectory modDirectory, ModManifest modManifest)
		{
			return string.Format(ModPlayerPrefsHelper.ModEnabledFormat, ModPlayerPrefsHelper.GetModKey(modDirectory, modManifest));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002DDB File Offset: 0x00000FDB
		public static string GetModPriorityKey(Mod mod)
		{
			return string.Format(ModPlayerPrefsHelper.ModPriorityFormat, ModPlayerPrefsHelper.GetModKey(mod.ModDirectory, mod.Manifest));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static string GetModKey(ModDirectory modDirectory, ModManifest modManifest)
		{
			return string.Concat(new string[]
			{
				modDirectory.DisplaySource,
				".",
				modDirectory.OriginName,
				".",
				modManifest.Id
			});
		}

		// Token: 0x04000032 RID: 50
		public static readonly string ModEnabledFormat = "ModEnabled.{0}";

		// Token: 0x04000033 RID: 51
		public static readonly string ModPriorityFormat = "ModPriority.{0}";
	}
}
