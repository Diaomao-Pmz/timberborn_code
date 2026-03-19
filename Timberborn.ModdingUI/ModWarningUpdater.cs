using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Modding;
using Timberborn.Versioning;

namespace Timberborn.ModdingUI
{
	// Token: 0x0200000C RID: 12
	public class ModWarningUpdater
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000296C File Offset: 0x00000B6C
		public void Update(Dictionary<Mod, ModItem> modItems)
		{
			foreach (ModItem modItem in modItems.Values)
			{
				if (!ModPlayerPrefsHelper.IsModEnabled(modItem.Mod) || (ModWarningUpdater.ValidateRequiredMods(modItem, modItems) && ModWarningUpdater.ValidateMinimumGameVersion(modItem)))
				{
					modItem.SetWarning(ModWarningReason.None, string.Empty);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029E4 File Offset: 0x00000BE4
		public static bool ValidateRequiredMods(ModItem modItem, Dictionary<Mod, ModItem> modItems)
		{
			foreach (VersionedMod versionedMod in modItem.ModManifest.RequiredMods)
			{
				if (ModWarningUpdater.IsRequiredModNotInstalled(modItems, versionedMod))
				{
					modItem.SetWarning(ModWarningReason.MissingRequiredMod, versionedMod.Id);
					return false;
				}
				if (!ModWarningUpdater.ValidateRequiredMod(modItem, modItems, versionedMod))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A40 File Offset: 0x00000C40
		public static bool ValidateRequiredMod(ModItem modItem, Dictionary<Mod, ModItem> modItems, VersionedMod requiredModDefinition)
		{
			foreach (Mod mod2 in from mod in modItems.Keys
			where mod.Manifest.Id == requiredModDefinition.Id
			select mod)
			{
				if (ModWarningUpdater.IsRequiredModDisabled(mod2))
				{
					modItem.SetWarning(ModWarningReason.RequiredModNotEnabled, mod2.DisplayName);
				}
				else if (ModWarningUpdater.IsRequiredModBelowMinimumVersion(mod2, requiredModDefinition))
				{
					modItem.SetWarning(ModWarningReason.RequiredModInvalidVersion, mod2.DisplayName);
				}
				else
				{
					if (!ModWarningUpdater.IsRequiredModBelowInLoadOrder(modItem, modItems[mod2]))
					{
						return true;
					}
					modItem.SetWarning(ModWarningReason.RequiredModInvalidOrder, mod2.DisplayName);
				}
			}
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B00 File Offset: 0x00000D00
		public static bool IsRequiredModNotInstalled(Dictionary<Mod, ModItem> modItems, VersionedMod requiredMod)
		{
			return modItems.Keys.All((Mod mod) => mod.Manifest.Id != requiredMod.Id);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B31 File Offset: 0x00000D31
		public static bool IsRequiredModDisabled(Mod requiredModInstance)
		{
			return !ModPlayerPrefsHelper.IsModEnabled(requiredModInstance);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B3C File Offset: 0x00000D3C
		public static bool IsRequiredModBelowMinimumVersion(Mod requiredMod, VersionedMod requiredModDefinition)
		{
			return !requiredMod.Manifest.Version.IsEqualOrHigherThan(requiredModDefinition.MinimumVersion, null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B6E File Offset: 0x00000D6E
		public static bool IsRequiredModBelowInLoadOrder(ModItem modItem, ModItem requiredMod)
		{
			return ModWarningUpdater.GetModLoadOrder(requiredMod) > ModWarningUpdater.GetModLoadOrder(modItem);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B7E File Offset: 0x00000D7E
		public static int GetModLoadOrder(ModItem modItem)
		{
			return modItem.Root.parent.IndexOf(modItem.Root);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B98 File Offset: 0x00000D98
		public static bool ValidateMinimumGameVersion(ModItem modItem)
		{
			if (GameVersions.CurrentVersion.IsEqualOrHigherThan(modItem.ModManifest.MinimumGameVersion, null) || GameVersions.CurrentVersion.IsDevelopmentVersion)
			{
				return true;
			}
			modItem.SetWarning(ModWarningReason.InvalidGameVersion, modItem.ModManifest.MinimumGameVersion.Formatted);
			return false;
		}
	}
}
