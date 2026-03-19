using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;
using UnityEngine;

namespace Timberborn.Modding
{
	// Token: 0x02000014 RID: 20
	public class ModRepository : ILoadableSingleton
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002E48 File Offset: 0x00001048
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002E50 File Offset: 0x00001050
		public ImmutableArray<Mod> Mods { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00002E59 File Offset: 0x00001059
		public ModRepository(ModLoader modLoader, ModSorter modSorter, IEnumerable<IModsProvider> modsProviders)
		{
			this._modLoader = modLoader;
			this._modSorter = modSorter;
			this._modsProviders = modsProviders.ToImmutableArray<IModsProvider>();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002E7B File Offset: 0x0000107B
		public IEnumerable<Mod> EnabledMods
		{
			get
			{
				return from mod in this.Mods
				where ModdedState.IsModded && mod.IsEnabled
				select mod;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EA7 File Offset: 0x000010A7
		public IEnumerable<Mod> UserMods
		{
			get
			{
				return from mod in this.Mods
				where mod.ModDirectory.IsUserMod
				select mod;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002ED3 File Offset: 0x000010D3
		public void Load()
		{
			this.Mods = this._modSorter.Sort(this.GetMods()).ToImmutableArray<Mod>();
			this.MarkDuplicatedIds();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002EF8 File Offset: 0x000010F8
		public bool ModIsNotEnabled(string modId)
		{
			return this.EnabledMods.All((Mod mod) => mod.Manifest.Id != modId);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F2C File Offset: 0x0000112C
		public bool ModIsOnDifferentVersion(string modId, string version)
		{
			return !this.EnabledMods.Any((Mod mod) => mod.Manifest.Id == modId && mod.Manifest.Version.Full == version);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F67 File Offset: 0x00001167
		public IEnumerable<Mod> GetMods()
		{
			foreach (IModsProvider modsProvider in this._modsProviders)
			{
				using (IEnumerator<ModDirectory> enumerator2 = modsProvider.GetModDirectories().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ModDirectory modDirectory;
						Mod mod;
						if (ModRepository.TryGetModDirectory(enumerator2.Current, out modDirectory) && this._modLoader.TryLoadMod(modDirectory, out mod))
						{
							yield return mod;
						}
					}
				}
				IEnumerator<ModDirectory> enumerator2 = null;
			}
			ImmutableArray<IModsProvider>.Enumerator enumerator = default(ImmutableArray<IModsProvider>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F78 File Offset: 0x00001178
		public static bool TryGetModDirectory(ModDirectory modDirectory, out ModDirectory versionedModDirectory)
		{
			bool result;
			try
			{
				versionedModDirectory = ModRepository.GetModDirectory(modDirectory);
				result = true;
			}
			catch (Exception arg)
			{
				Debug.LogWarning(string.Format("Failed to load mod directory from {0}: {1}", modDirectory.Path, arg));
				versionedModDirectory = default(ModDirectory);
				result = false;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FCC File Offset: 0x000011CC
		public static ModDirectory GetModDirectory(ModDirectory modDirectory)
		{
			DirectoryInfo[] directories = modDirectory.Directory.GetDirectories(ModRepository.DirectoryVersionPrefix + "*");
			if (directories.Length == 0)
			{
				return modDirectory;
			}
			return ModRepository.GetDirectoryForCurrentVersion(ModRepository.GetModSubdirectories(directories, modDirectory), modDirectory);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003008 File Offset: 0x00001208
		public static IEnumerable<ModDirectory> GetModSubdirectories(IEnumerable<DirectoryInfo> directoryInfos, ModDirectory original)
		{
			foreach (DirectoryInfo directoryInfo in directoryInfos)
			{
				string name = directoryInfo.Name;
				int length = ModRepository.DirectoryVersionPrefix.Length;
				string text = name.Substring(length, name.Length - length);
				Version gameVersion = string.IsNullOrEmpty(text) ? GameVersions.CurrentVersion : Version.Create(text);
				yield return new ModDirectory(directoryInfo, original.IsUserMod, original.DisplaySource, gameVersion, true);
			}
			IEnumerator<DirectoryInfo> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003020 File Offset: 0x00001220
		public static ModDirectory GetDirectoryForCurrentVersion(IEnumerable<ModDirectory> subdirectories, ModDirectory original)
		{
			Version currentVersion = GameVersions.CurrentVersion;
			ModDirectory? modDirectory = null;
			foreach (ModDirectory value in subdirectories)
			{
				if ((currentVersion.IsEqualOrHigherThan(value.GameVersion, null) || currentVersion.IsDevelopmentVersion) && (modDirectory == null || value.GameVersion.IsEqualOrHigherThan(modDirectory.Value.GameVersion, null)))
				{
					modDirectory = new ModDirectory?(value);
				}
			}
			if (modDirectory == null)
			{
				throw new InvalidOperationException(string.Format("No matching version directory found for version {0} in {1}", currentVersion, original.Path));
			}
			return modDirectory.Value;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030FC File Offset: 0x000012FC
		public void MarkDuplicatedIds()
		{
			foreach (Mod mod2 in (from mod in this.Mods
			group mod by new
			{
				mod.Manifest.Id,
				mod.ModDirectory.IsUserMod
			} into @group
			where @group.Count<Mod>() > 1
			select @group).SelectMany(group => group))
			{
				mod2.MarkIdAsDuplicated();
			}
		}

		// Token: 0x04000034 RID: 52
		public static readonly string DirectoryVersionPrefix = "version-";

		// Token: 0x04000036 RID: 54
		public readonly ModLoader _modLoader;

		// Token: 0x04000037 RID: 55
		public readonly ModSorter _modSorter;

		// Token: 0x04000038 RID: 56
		public readonly ImmutableArray<IModsProvider> _modsProviders;
	}
}
