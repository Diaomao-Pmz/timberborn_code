using System;
using System.Collections.Generic;
using System.IO;
using Timberborn.FileSystem;
using Timberborn.PlatformUtilities;
using Timberborn.Versioning;

namespace Timberborn.Modding
{
	// Token: 0x0200001E RID: 30
	public class UserFolderModsProvider : IModsProvider
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003A1B File Offset: 0x00001C1B
		public UserFolderModsProvider(IFileService fileService)
		{
			this._fileService = fileService;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003A2A File Offset: 0x00001C2A
		public IEnumerable<ModDirectory> GetModDirectories()
		{
			if (this._fileService.HasDocumentsPermissions)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(UserDataFolder.Folder, UserFolderModsProvider.ModsDirectoryName));
				this._fileService.CreateDirectory(directoryInfo.FullName);
				foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
				{
					yield return new ModDirectory(directory, true, "Local", GameVersions.CurrentVersion, false);
				}
				DirectoryInfo[] array = null;
			}
			yield break;
		}

		// Token: 0x04000061 RID: 97
		public static readonly string ModsDirectoryName = "Mods";

		// Token: 0x04000062 RID: 98
		public readonly IFileService _fileService;
	}
}
