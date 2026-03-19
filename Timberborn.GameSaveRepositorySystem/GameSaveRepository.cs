using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.Debugging;
using Timberborn.ExperimentalModeSystem;
using Timberborn.FileSystem;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.GameSaveRepositorySystem
{
	// Token: 0x02000008 RID: 8
	public class GameSaveRepository
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000220C File Offset: 0x0000040C
		public GameSaveRepository(IFileService fileService, FilenameValidator filenameValidator, ExperimentalMode experimentalMode, DevModeManager devModeManager = null)
		{
			this._fileService = fileService;
			this._filenameValidator = filenameValidator;
			this._experimentalMode = experimentalMode;
			this._devModeManager = devModeManager;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002231 File Offset: 0x00000431
		public string DefaultSaveDirectory
		{
			get
			{
				return this.SaveDirectories()[0];
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223C File Offset: 0x0000043C
		public Stream CreateSaveSkippingNameValidation(SaveReference saveReference)
		{
			this.CreateDirectoryForSettlement(saveReference.SettlementReference);
			string fileName = this.SaveNameToFileName(saveReference);
			return this._fileService.CreateFile(fileName);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000226C File Offset: 0x0000046C
		public Stream CreateSave(SaveReference saveReference)
		{
			if (this.NameIsInvalid(saveReference.SettlementReference.SettlementName))
			{
				throw new ArgumentException(saveReference.SettlementReference.SettlementName + " contains an illegal character");
			}
			if (this.NameIsInvalid(saveReference.SaveName))
			{
				throw new ArgumentException(saveReference.SaveName + " contains an illegal character");
			}
			return this.CreateSaveSkippingNameValidation(saveReference);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D2 File Offset: 0x000004D2
		public bool NameIsInvalid(string name)
		{
			return this._filenameValidator.NameIsInvalid(name);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022E0 File Offset: 0x000004E0
		public Stream OpenSave(SaveReference saveReference)
		{
			return this.OpenSaveInternal(saveReference, true);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022EA File Offset: 0x000004EA
		public Stream OpenSaveWithoutLogging(SaveReference saveReference)
		{
			return this.OpenSaveInternal(saveReference, false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F4 File Offset: 0x000004F4
		public IEnumerable<SaveReference> GetAllSaves()
		{
			return this.GetAllSettlements().SelectMany(new Func<SettlementReference, IEnumerable<SaveReference>>(this.GetSaves));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002310 File Offset: 0x00000510
		public IEnumerable<SaveReference> GetSaves(SettlementReference settlementReference)
		{
			this.CreateSaveDirectories();
			this.CreateDirectoryForSettlement(settlementReference);
			return from saveName in (from file in Directory.GetFiles(this.SettlementReferenceIntoDirectoryName(settlementReference))
			where Path.GetExtension(file) == GameSaveRepository.SaveExtension
			orderby new FileInfo(file).LastWriteTime.ToUniversalTime() descending
			select file).Select(new Func<string, string>(Path.GetFileNameWithoutExtension))
			select new SaveReference(saveName, settlementReference);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023B8 File Offset: 0x000005B8
		public IEnumerable<SettlementReference> GetAllSettlements()
		{
			this.CreateSaveDirectories();
			List<string> list = new List<string>();
			foreach (string path in this.SaveDirectories())
			{
				list.AddRange(Directory.GetDirectories(path).Where(new Func<string, bool>(this.DirectoryExistsAndNotEmpty)));
			}
			return list.OrderByDescending(new Func<string, DateTime>(GameSaveRepository.GetMostRecentSaveTime)).Select(new Func<string, SettlementReference>(this.DirectoryNameIntoSettlementReference));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000242A File Offset: 0x0000062A
		public SaveReference GetMostRecentSave()
		{
			return this.GetAllSaves().FirstOrDefault<SaveReference>();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002437 File Offset: 0x00000637
		public bool SaveExists(SaveReference saveReference)
		{
			return saveReference != null && this._fileService.FileExists(this.SaveNameToFileName(saveReference));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002458 File Offset: 0x00000658
		public DateTime GetSaveLastWriteTime(SaveReference saveReference)
		{
			string fileName = this.SaveNameToFileName(saveReference);
			return this._fileService.GetFileInfo(fileName).LastWriteTime.ToUniversalTime();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002488 File Offset: 0x00000688
		public void DeleteSave(SaveReference saveReference)
		{
			string fileName = this.SaveNameToFileName(saveReference);
			this._fileService.DeleteFile(fileName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024AC File Offset: 0x000006AC
		public bool DeleteSaveSafely(SaveReference saveReference)
		{
			if (this.SaveExists(saveReference))
			{
				string text = this.SaveNameToFileName(saveReference);
				try
				{
					this._fileService.DeleteFile(text);
				}
				catch (Exception ex)
				{
					Debug.LogError("Failed to delete " + text + " due to " + ex.Message);
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000250C File Offset: 0x0000070C
		public void DeleteSettlement(SettlementReference settlementReference)
		{
			string directoryName = this.SettlementReferenceIntoDirectoryName(settlementReference);
			this._fileService.DeleteDirectory(directoryName);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000252D File Offset: 0x0000072D
		public DirectoryCreationResult CreateDirectoryForSettlement(string settlementName)
		{
			return this.CreateDirectoryForSettlement(new SettlementReference(settlementName, this.DefaultSaveDirectory));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002541 File Offset: 0x00000741
		public string SettlementReferenceIntoDirectoryName(SettlementReference settlementReference)
		{
			return this._fileService.CombineIntoPath(settlementReference.SaveDirectory, settlementReference.SettlementName, "");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002560 File Offset: 0x00000760
		public string SaveNameToFileName(SaveReference saveReference)
		{
			string path = this.SettlementReferenceIntoDirectoryName(saveReference.SettlementReference);
			return this._fileService.CombineIntoPath(path, saveReference.SaveName, GameSaveRepository.SaveExtension);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002591 File Offset: 0x00000791
		public bool DevelopmentSettlementExists()
		{
			return (from settlement in this.GetAllSettlements()
			select settlement.SettlementName).Contains(GameSaveRepository.DevelopmentSettlementName);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025C8 File Offset: 0x000007C8
		public DirectoryCreationResult CreateDirectoryForSettlement(SettlementReference settlementReference)
		{
			if (this._filenameValidator.NameIsInvalid(settlementReference.SettlementName))
			{
				return DirectoryCreationResult.NameInvalid;
			}
			string directoryPath = this.SettlementReferenceIntoDirectoryName(settlementReference);
			return this._fileService.CreateDirectoryIfValid(directoryPath);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002600 File Offset: 0x00000800
		public string[] SaveDirectories()
		{
			if (!Application.isEditor)
			{
				DevModeManager devModeManager = this._devModeManager;
				if (devModeManager == null || !devModeManager.Enabled)
				{
					if (!this._experimentalMode.IsExperimental)
					{
						return new string[]
						{
							Path.Combine(UserDataFolder.Folder, GameSaveRepository.DefaultSavesDir)
						};
					}
					return new string[]
					{
						Path.Combine(UserDataFolder.Folder, GameSaveRepository.ExperimentalSavesDir)
					};
				}
			}
			if (this._experimentalMode.IsExperimental)
			{
				return new string[]
				{
					Path.Combine(UserDataFolder.Folder, GameSaveRepository.ExperimentalSavesDir),
					Path.Combine(UserDataFolder.Folder, GameSaveRepository.DefaultSavesDir)
				};
			}
			return new string[]
			{
				Path.Combine(UserDataFolder.Folder, GameSaveRepository.DefaultSavesDir),
				Path.Combine(UserDataFolder.Folder, GameSaveRepository.ExperimentalSavesDir)
			};
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026C8 File Offset: 0x000008C8
		public Stream OpenSaveInternal(SaveReference saveReference, bool logOpening)
		{
			string text = this.SaveNameToFileName(saveReference);
			if (logOpening)
			{
				Debug.Log("Opening file: " + text);
			}
			return this._fileService.OpenFile(text);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026FC File Offset: 0x000008FC
		public bool DirectoryExistsAndNotEmpty(string directoryName)
		{
			return this._fileService.DirectoryExistsAndNotEmpty(directoryName, GameSaveRepository.SaveExtension);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002710 File Offset: 0x00000910
		public static DateTime GetMostRecentSaveTime(string directoryName)
		{
			return (from file in Directory.GetFiles(directoryName)
			where Path.GetExtension(file) == GameSaveRepository.SaveExtension
			select file).Max((string file) => new FileInfo(file).LastWriteTime.ToUniversalTime());
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000276C File Offset: 0x0000096C
		public SettlementReference DirectoryNameIntoSettlementReference(string directoryName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
			string name = directoryInfo.Name;
			DirectoryInfo parent = directoryInfo.Parent;
			return new SettlementReference(name, (parent != null) ? parent.FullName : null);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027A0 File Offset: 0x000009A0
		public void CreateSaveDirectories()
		{
			foreach (string directoryName in this.SaveDirectories())
			{
				this._fileService.CreateDirectory(directoryName);
			}
		}

		// Token: 0x0400000B RID: 11
		public static readonly string AutosaveNameSuffix = ".autosave";

		// Token: 0x0400000C RID: 12
		public static readonly string DevelopmentSettlementName = "Unity Editor Settlements";

		// Token: 0x0400000D RID: 13
		public static readonly string SaveExtension = ".timber";

		// Token: 0x0400000E RID: 14
		public static readonly string DefaultSavesDir = "Saves";

		// Token: 0x0400000F RID: 15
		public static readonly string ExperimentalSavesDir = "ExperimentalSaves";

		// Token: 0x04000010 RID: 16
		public readonly IFileService _fileService;

		// Token: 0x04000011 RID: 17
		public readonly FilenameValidator _filenameValidator;

		// Token: 0x04000012 RID: 18
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000013 RID: 19
		public readonly ExperimentalMode _experimentalMode;
	}
}
