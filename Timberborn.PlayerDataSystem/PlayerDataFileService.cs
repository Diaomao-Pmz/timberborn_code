using System;
using System.IO;
using Timberborn.FileSystem;
using Timberborn.PlatformUtilities;

namespace Timberborn.PlayerDataSystem
{
	// Token: 0x02000005 RID: 5
	public class PlayerDataFileService
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020BE File Offset: 0x000002BE
		public PlayerDataFileService(IFileService fileService)
		{
			this._fileService = fileService;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020D0 File Offset: 0x000002D0
		public void CopyFile(string suffix)
		{
			if (this._fileService.HasDocumentsPermissions && this._fileService.FileExists(PlayerDataFileService.PlayerDataFilePath))
			{
				string destinationFileName = PlayerDataFileService.PlayerDataFilePath + "." + suffix;
				this._fileService.CopyFile(PlayerDataFileService.PlayerDataFilePath, destinationFileName);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000211E File Offset: 0x0000031E
		public void BackupFile()
		{
			if (this._fileService.HasDocumentsPermissions && this._fileService.FileExists(PlayerDataFileService.PlayerDataBackupFilePath))
			{
				this._fileService.DeleteFile(PlayerDataFileService.PlayerDataBackupFilePath);
			}
			this.CopyFile(PlayerDataFileService.PlayerDataBackupSuffix);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000215C File Offset: 0x0000035C
		public void RestoreFromBackup()
		{
			if (this._fileService.HasDocumentsPermissions)
			{
				if (this._fileService.FileExists(PlayerDataFileService.PlayerDataFilePath))
				{
					this._fileService.DeleteFile(PlayerDataFileService.PlayerDataFilePath);
				}
				if (this._fileService.FileExists(PlayerDataFileService.PlayerDataBackupFilePath))
				{
					this._fileService.CopyFile(PlayerDataFileService.PlayerDataBackupFilePath, PlayerDataFileService.PlayerDataFilePath);
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021BF File Offset: 0x000003BF
		public static string PlayerDataBackupFilePath
		{
			get
			{
				return PlayerDataFileService.PlayerDataFilePath + "." + PlayerDataFileService.PlayerDataBackupSuffix;
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string PlayerDataDirectory = Path.Combine(UserDataFolder.Folder, "PlayerData");

		// Token: 0x04000007 RID: 7
		public static readonly string PlayerDataFilePath = Path.Combine(PlayerDataFileService.PlayerDataDirectory, "player.data");

		// Token: 0x04000008 RID: 8
		public static readonly string PlayerDataBackupSuffix = "old";

		// Token: 0x04000009 RID: 9
		public readonly IFileService _fileService;
	}
}
