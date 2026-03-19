using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEngine;

namespace Timberborn.SaveSystem
{
	// Token: 0x02000008 RID: 8
	public class SaveReader
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020C0 File Offset: 0x000002C0
		public T ReadFromSaveStream<T>(Stream saveStream, ISaveEntryReader<T> saveEntryReader)
		{
			T result;
			try
			{
				result = this.ReadFromSaveStreamUnsafe<T>(saveStream, saveEntryReader);
			}
			catch (Exception ex)
			{
				string str = "Failed to read save entry ";
				string entryName = saveEntryReader.EntryName;
				string str2 = ": ";
				Exception ex2 = ex;
				Debug.LogWarning(str + entryName + str2 + ((ex2 != null) ? ex2.ToString() : null));
				result = default(T);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public T ReadFromSaveStreamUnsafe<T>(Stream saveStream, ISaveEntryReader<T> saveEntryReader)
		{
			T result;
			using (ZipArchive zipArchive = new ZipArchive(saveStream, ZipArchiveMode.Read))
			{
				ZipArchiveEntry zipArchiveEntry = zipArchive.Entries.FirstOrDefault((ZipArchiveEntry entry) => entry.Name == saveEntryReader.EntryName);
				if (zipArchiveEntry != null)
				{
					using (Stream stream = zipArchiveEntry.Open())
					{
						return saveEntryReader.ReadFromSaveEntryStream(stream);
					}
				}
				result = SaveReader.BackwardCompatibleRead<T>(saveStream, saveEntryReader);
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		public static T BackwardCompatibleRead<T>(Stream saveStream, ISaveEntryReader<T> saveEntryReader)
		{
			IBackwardCompatibleSaveEntryReader<T> backwardCompatibleSaveEntryReader = saveEntryReader as IBackwardCompatibleSaveEntryReader<T>;
			if (backwardCompatibleSaveEntryReader != null)
			{
				return backwardCompatibleSaveEntryReader.BackwardCompatibleRead(saveStream);
			}
			return default(T);
		}
	}
}
