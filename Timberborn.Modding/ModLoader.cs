using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Timberborn.Modding
{
	// Token: 0x02000011 RID: 17
	public class ModLoader
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public ModLoader(ManifestLoader manifestLoader)
		{
			this._manifestLoader = manifestLoader;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public bool TryLoadMod(ModDirectory modDirectory, out Mod mod)
		{
			ModManifest modManifest;
			if (this._manifestLoader.TryLoadManifest(modDirectory.Path, out modManifest))
			{
				if (ModLoader.ValidateId(modManifest, modDirectory))
				{
					bool isEnabled = ModPlayerPrefsHelper.IsModEnabled(modDirectory, modManifest);
					mod = new Mod(modDirectory, modManifest, isEnabled);
					return true;
				}
			}
			else
			{
				Debug.LogWarning("No manifest file found in " + modDirectory.Path);
			}
			mod = null;
			return false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C1A File Offset: 0x00000E1A
		public bool IsModDirectory(DirectoryInfo directory)
		{
			return directory.EnumerateFiles(ManifestLoader.ManifestFileName, SearchOption.AllDirectories).Any<FileInfo>();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C2D File Offset: 0x00000E2D
		public static bool ValidateId(ModManifest manifest, ModDirectory modDirectory)
		{
			if (string.IsNullOrWhiteSpace(manifest.Id))
			{
				Debug.LogWarning("Mod id from directory \"" + modDirectory.Path + "\" is empty");
				return false;
			}
			return true;
		}

		// Token: 0x0400002A RID: 42
		public readonly ManifestLoader _manifestLoader;
	}
}
