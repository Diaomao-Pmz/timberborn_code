using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Newtonsoft.Json;
using Timberborn.Common;
using Timberborn.FileSystem;
using UnityEngine;

namespace Timberborn.PlayerDataSystem
{
	// Token: 0x02000006 RID: 6
	public class PlayerDataSerializer
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002209 File Offset: 0x00000409
		public PlayerDataSerializer(IFileService fileService)
		{
			this._fileService = fileService;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002218 File Offset: 0x00000418
		public void SaveData(Dictionary<string, string> data)
		{
			try
			{
				if (this._fileService.HasDocumentsPermissions)
				{
					this._fileService.CreateDirectory(PlayerDataFileService.PlayerDataDirectory);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						PlayerDataSerializer.SaveToStream(data, memoryStream);
						this.SaveToFile(memoryStream);
					}
				}
			}
			catch (Exception ex) when (ex is IOException || ex is ArgumentException || ex is JsonException)
			{
				throw new InvalidOperationException("Failed saving player data.", ex);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022BC File Offset: 0x000004BC
		public Dictionary<string, string> LoadData(out bool success)
		{
			success = this._fileService.HasDocumentsPermissions;
			if (this._fileService.HasDocumentsPermissions && this._fileService.FileExists(PlayerDataFileService.PlayerDataFilePath))
			{
				try
				{
					using (Stream stream = this._fileService.OpenFile(PlayerDataFileService.PlayerDataFilePath))
					{
						return PlayerDataSerializer.LoadFromStream(stream);
					}
				}
				catch (Exception ex) when (ex is IOException || ex is ArgumentException || ex is JsonException || ex is InvalidDataException)
				{
					Debug.LogWarning(string.Format("Failed loading player data. Details: {0}", ex));
					success = false;
				}
			}
			return new Dictionary<string, string>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000238C File Offset: 0x0000058C
		public static void SaveToStream(Dictionary<string, string> data, MemoryStream memoryStream)
		{
			string value = JsonConvert.SerializeObject(data, Formatting.Indented);
			using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
			{
				using (Stream stream = zipArchive.CreateEntry(PlayerDataSerializer.PlayerDataEntryName, CompressionLevel.Fastest).Open())
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						streamWriter.Write(value);
					}
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002414 File Offset: 0x00000614
		public void SaveToFile(MemoryStream memoryStream)
		{
			using (Stream stream = this._fileService.CreateFile(PlayerDataFileService.PlayerDataFilePath))
			{
				memoryStream.Position = 0L;
				memoryStream.CopyTo(stream);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002460 File Offset: 0x00000660
		public static Dictionary<string, string> LoadFromStream(Stream stream)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> result;
			using (ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
			{
				ZipArchiveEntry zipArchiveEntry = zipArchive.Entries.FirstOrDefault((ZipArchiveEntry entry) => entry.Name == PlayerDataSerializer.PlayerDataEntryName);
				if (zipArchiveEntry != null)
				{
					using (Stream stream2 = zipArchiveEntry.Open())
					{
						using (StreamReader streamReader = new StreamReader(stream2))
						{
							string value = streamReader.ReadToEnd();
							dictionary.AddRange(JsonConvert.DeserializeObject<Dictionary<string, string>>(value));
						}
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0400000A RID: 10
		public static readonly string PlayerDataEntryName = "data.json";

		// Token: 0x0400000B RID: 11
		public readonly IFileService _fileService;
	}
}
