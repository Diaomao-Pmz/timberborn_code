using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Timberborn.AssetSystem;
using Timberborn.Common;
using Timberborn.FileSystem;
using Timberborn.Modding;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x02000008 RID: 8
	public class ModCreator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002599 File Offset: 0x00000799
		public ModCreator(IFileService fileService, FilenameValidator filenameValidator, ModTemplateDropdownProvider modTemplateDropdownProvider, IAssetLoader assetLoader)
		{
			this._fileService = fileService;
			this._filenameValidator = filenameValidator;
			this._modTemplateDropdownProvider = modTemplateDropdownProvider;
			this._assetLoader = assetLoader;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025C0 File Offset: 0x000007C0
		public DirectoryCreationResult CreateMod(string modName, string localizationCode, out string destinationPath)
		{
			if (this._filenameValidator.NameIsInvalid(modName))
			{
				destinationPath = null;
				return DirectoryCreationResult.NameInvalid;
			}
			string sourcePath = Path.Combine(ModCreator.TemplatesPath, this._modTemplateDropdownProvider.GetDirectory());
			destinationPath = Path.Combine(UserDataFolder.Folder, UserFolderModsProvider.ModsDirectoryName, modName);
			DirectoryCreationResult directoryCreationResult = this._fileService.CreateDirectoryIfValid(destinationPath);
			if (directoryCreationResult == DirectoryCreationResult.OK)
			{
				this.CopyDirectory(sourcePath, destinationPath, modName);
				if (!string.IsNullOrEmpty(localizationCode))
				{
					this.CreateLocalizationFiles(modName, localizationCode, destinationPath);
				}
			}
			return directoryCreationResult;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002634 File Offset: 0x00000834
		public void CopyDirectory(string sourcePath, string targetPath, string modName)
		{
			Directory.CreateDirectory(targetPath);
			foreach (FileInfo fileInfo in new DirectoryInfo(sourcePath).GetFiles())
			{
				if (fileInfo.Extension != ".meta")
				{
					string text = Path.Combine(targetPath, fileInfo.Name);
					if (fileInfo.Name == ManifestLoader.ManifestFileName)
					{
						string manifestContent = this.GetManifestContent(File.ReadAllText(fileInfo.FullName), modName);
						this._fileService.WriteTextToFile(text, manifestContent);
					}
					else
					{
						this._fileService.CopyFile(fileInfo.FullName, text);
					}
				}
			}
			foreach (string text2 in Directory.GetDirectories(sourcePath))
			{
				string fileName = Path.GetFileName(text2);
				string targetPath2 = Path.Combine(targetPath, fileName);
				this.CopyDirectory(text2, targetPath2, modName);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002708 File Offset: 0x00000908
		public string GetManifestContent(string original, string modName)
		{
			string value = this._modTemplateDropdownProvider.GetValue();
			JObject jobject = JObject.Parse(original);
			jobject["Name"] = modName;
			jobject["Id"] = value.ToPascalCase() + "." + modName.ToPascalCase();
			return jobject.ToString();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002764 File Offset: 0x00000964
		public void CreateLocalizationFiles(string modName, string localizationCode, string destinationPath)
		{
			string text = Path.Combine(destinationPath, ModCreator.LocalizationsDirectory);
			Directory.CreateDirectory(text);
			foreach (LoadedAsset<TextAsset> loadedAsset in this._assetLoader.LoadAll<TextAsset>(ModCreator.LocalizationsDirectory ?? ""))
			{
				if (loadedAsset.Asset.name.StartsWith(ModCreator.BaseLanguageCode))
				{
					this.CreateLocalizationFile(text, loadedAsset.Asset, modName, localizationCode, loadedAsset.IsBuiltIn);
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002800 File Offset: 0x00000A00
		public void CreateLocalizationFile(string destinationPath, TextAsset original, string modName, string localizationCode, bool isBuiltIn)
		{
			string text = original.name.Replace(ModCreator.BaseLanguageCode, localizationCode);
			if (!isBuiltIn)
			{
				text += ModCreator.ModPostfix;
			}
			string path = Path.Combine(destinationPath, text + ModCreator.LocalizationExtension);
			int num = 1;
			while (File.Exists(path))
			{
				path = Path.Combine(destinationPath, string.Format("{0}{1}{2}", text, num++, ModCreator.LocalizationExtension));
			}
			string text2 = original.name.EndsWith(ModCreator.DoNotTranslatePostfix) ? original.text.Replace(ModCreator.BaseLanguageName, modName) : original.text;
			this._fileService.WriteTextToFile(path, text2);
		}

		// Token: 0x0400001F RID: 31
		public static readonly string TemplatesPath = Path.Combine(Application.streamingAssetsPath, "Modding", "ModTemplates");

		// Token: 0x04000020 RID: 32
		public static readonly string LocalizationsDirectory = "Localizations";

		// Token: 0x04000021 RID: 33
		public static readonly string BaseLanguageName = "English (English)";

		// Token: 0x04000022 RID: 34
		public static readonly string BaseLanguageCode = "enUS";

		// Token: 0x04000023 RID: 35
		public static readonly string DoNotTranslatePostfix = "_donottranslate";

		// Token: 0x04000024 RID: 36
		public static readonly string ModPostfix = "_mod";

		// Token: 0x04000025 RID: 37
		public static readonly string LocalizationExtension = ".csv";

		// Token: 0x04000026 RID: 38
		public readonly IFileService _fileService;

		// Token: 0x04000027 RID: 39
		public readonly FilenameValidator _filenameValidator;

		// Token: 0x04000028 RID: 40
		public readonly ModTemplateDropdownProvider _modTemplateDropdownProvider;

		// Token: 0x04000029 RID: 41
		public readonly IAssetLoader _assetLoader;
	}
}
