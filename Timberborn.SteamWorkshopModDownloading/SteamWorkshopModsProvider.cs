using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.Modding;
using Timberborn.SteamWorkshopContent;
using Timberborn.Versioning;

namespace Timberborn.SteamWorkshopModDownloading
{
	// Token: 0x02000005 RID: 5
	public class SteamWorkshopModsProvider : IModsProvider
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D9 File Offset: 0x000002D9
		public SteamWorkshopModsProvider(SteamWorkshopContentProvider steamWorkshopContentProvider, ModLoader modLoader)
		{
			this._steamWorkshopContentProvider = steamWorkshopContentProvider;
			this._modLoader = modLoader;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EF File Offset: 0x000002EF
		public IEnumerable<ModDirectory> GetModDirectories()
		{
			foreach (DirectoryInfo directory in this._steamWorkshopContentProvider.GetContentDirectories())
			{
				if (this._modLoader.IsModDirectory(directory))
				{
					yield return new ModDirectory(directory, false, "Steam Workshop", GameVersions.CurrentVersion, false);
				}
			}
			IEnumerator<DirectoryInfo> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public readonly SteamWorkshopContentProvider _steamWorkshopContentProvider;

		// Token: 0x04000007 RID: 7
		public readonly ModLoader _modLoader;
	}
}
