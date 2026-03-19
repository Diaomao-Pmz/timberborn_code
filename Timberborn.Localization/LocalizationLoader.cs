using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LINQtoCSV;
using Timberborn.AssetSystem;
using UnityEngine;

namespace Timberborn.Localization
{
	// Token: 0x02000010 RID: 16
	public class LocalizationLoader
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000028DB File Offset: 0x00000ADB
		public LocalizationLoader(ILocalizationCsvValidator localizationCsvValidator, IAssetLoader assetLoader)
		{
			this._localizationCsvValidator = localizationCsvValidator;
			this._assetLoader = assetLoader;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028F4 File Offset: 0x00000AF4
		public Dictionary<string, string> GetLocalization(string localizationKey, bool isExperimental = false)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> localizationRecords = this.GetLocalizationRecords(localizationKey);
			Dictionary<string, string> referenceLocalization = this.GetReferenceLocalization();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (LocalizationRecord localizationRecord in this.GetDefaultLocalization())
			{
				string id = localizationRecord.Id;
				string b;
				bool flag = (referenceLocalization.TryGetValue(id, out b) && localizationRecord.Text == b) || !localizationRecord.IsBuiltIn;
				string text;
				bool flag2 = localizationRecords.TryGetValue(id, out text) && !string.IsNullOrEmpty(text);
				if (flag && flag2)
				{
					dictionary[id] = TextColors.ColorizeText(text);
				}
				else
				{
					if (flag)
					{
						stringBuilder.AppendLine("Missing or empty localization key " + id + " in " + localizationKey);
					}
					else if (!Application.isEditor && !isExperimental && !localizationRecord.HideWarning)
					{
						stringBuilder.AppendLine("Text mismatch in localization key " + id + " in " + localizationKey);
					}
					dictionary[id] = TextColors.ColorizeText(localizationRecord.Text);
				}
			}
			if (stringBuilder.Length > 0)
			{
				string arg = "Localization issues in " + localizationKey + ":\n\n";
				Debug.LogWarning(string.Format("{0}{1}\n", arg, stringBuilder));
			}
			return dictionary;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A58 File Offset: 0x00000C58
		public IEnumerable<string> GetLocalizationNames()
		{
			return (from localizationFile in this.GetLocalizationFiles()
			select LocalizationLoader.LocalizationNameFromFileName(localizationFile.Asset.name) into assetName
			where assetName != LocalizationCodes.Reference
			select assetName).Distinct<string>();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public Dictionary<string, string> GetLocalizationRecords(string localization)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (LocalizationRecord localizationRecord in this.GetLocalizationRecordsInternal(localization, false))
			{
				if (localizationRecord.IsBuiltIn)
				{
					if (!dictionary.TryAdd(localizationRecord.Id, localizationRecord.Text))
					{
						throw new InvalidOperationException("Duplicate localization key " + localizationRecord.Id + " in " + localization);
					}
				}
				else
				{
					dictionary[localizationRecord.Id] = localizationRecord.Text;
				}
			}
			return dictionary;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B54 File Offset: 0x00000D54
		public IEnumerable<LocalizationRecord> GetDefaultLocalization()
		{
			return this.GetLocalizationRecordsInternal(LocalizationCodes.Default, true);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B62 File Offset: 0x00000D62
		public Dictionary<string, string> GetReferenceLocalization()
		{
			return this.GetLocalizationRecords(LocalizationCodes.Reference);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B70 File Offset: 0x00000D70
		public IEnumerable<LocalizationRecord> GetLocalizationRecordsInternal(string localization, bool validate = false)
		{
			string localizationName = LocalizationLoader.LocalizationNameOrDefault(localization);
			IEnumerable<LoadedAsset<TextAsset>> loadedLocalizations = from asset in this.GetLocalizationFiles()
			where LocalizationLoader.LocalizationNameFromFileName(asset.Asset.name) == localizationName
			select asset;
			return this.ReadLocalizationFiles(loadedLocalizations, localization, validate);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public IEnumerable<LocalizationRecord> ReadLocalizationFiles(IEnumerable<LoadedAsset<TextAsset>> loadedLocalizations, string localization, bool validate = false)
		{
			foreach (LoadedAsset<TextAsset> loadedLocalization in loadedLocalizations)
			{
				TextAsset asset = loadedLocalization.Asset;
				if (validate)
				{
					this._localizationCsvValidator.Validate(asset);
				}
				bool hideWarning = asset.name.EndsWith(LocalizationLoader.WipFilenameSuffix);
				using (MemoryStream stream = new MemoryStream(asset.bytes))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						IEnumerable<LocalizationRecord> enumerable = LocalizationLoader.ReadRecords(localization, reader);
						foreach (LocalizationRecord localizationRecord in enumerable)
						{
							localizationRecord.HideWarning = hideWarning;
							localizationRecord.IsBuiltIn = loadedLocalization.IsBuiltIn;
							yield return localizationRecord;
						}
						IEnumerator<LocalizationRecord> enumerator2 = null;
					}
				}
				MemoryStream stream = null;
				StreamReader reader = null;
				loadedLocalization = default(LoadedAsset<TextAsset>);
			}
			IEnumerator<LoadedAsset<TextAsset>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public static IEnumerable<LocalizationRecord> ReadRecords(string localization, StreamReader reader)
		{
			IEnumerable<LocalizationRecord> result;
			try
			{
				result = new CsvContext().Read<LocalizationRecord>(reader);
			}
			catch (Exception ex)
			{
				string text = "Unable to parse file for " + localization + ".";
				AggregatedException ex2 = ex as AggregatedException;
				if (ex2 != null)
				{
					text = text + " First error: " + ex2.m_InnerExceptionsList[0].Message;
				}
				if (localization == LocalizationCodes.Default)
				{
					throw new InvalidDataException(text, ex);
				}
				Debug.LogError(text);
				result = Enumerable.Empty<LocalizationRecord>();
			}
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C60 File Offset: 0x00000E60
		public static string LocalizationNameOrDefault(string localizationName)
		{
			if (string.IsNullOrEmpty(localizationName))
			{
				Debug.LogError("localizationName can't be empty.Returning default localization: " + LocalizationCodes.Default);
				return LocalizationCodes.Default;
			}
			return localizationName;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C85 File Offset: 0x00000E85
		public IEnumerable<LoadedAsset<TextAsset>> GetLocalizationFiles()
		{
			return this._assetLoader.LoadAll<TextAsset>(LocalizationLoader.LocalizationsDirectory);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C98 File Offset: 0x00000E98
		public static string LocalizationNameFromFileName(string assetName)
		{
			int num = assetName.IndexOf('_');
			if (num != -1)
			{
				return assetName.Substring(0, num);
			}
			return assetName;
		}

		// Token: 0x04000028 RID: 40
		public static readonly string WipFilenameSuffix = "_wip";

		// Token: 0x04000029 RID: 41
		public static readonly string LocalizationsDirectory = "Localizations";

		// Token: 0x0400002A RID: 42
		public readonly ILocalizationCsvValidator _localizationCsvValidator;

		// Token: 0x0400002B RID: 43
		public readonly IAssetLoader _assetLoader;
	}
}
