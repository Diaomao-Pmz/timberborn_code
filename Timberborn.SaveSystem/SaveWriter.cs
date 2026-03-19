using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;

namespace Timberborn.SaveSystem
{
	// Token: 0x0200000B RID: 11
	public class SaveWriter
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000221A File Offset: 0x0000041A
		public SaveWriter(IEnumerable<ISaveEntryWriter> saveFileWriters)
		{
			this._saveEntryWriters = saveFileWriters.ToImmutableArray<ISaveEntryWriter>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002230 File Offset: 0x00000430
		public void WriteToSaveStream(Stream saveStream, bool leaveOpen = false)
		{
			using (ZipArchive zipArchive = new ZipArchive(saveStream, ZipArchiveMode.Update, leaveOpen))
			{
				foreach (ISaveEntryWriter saveEntryWriter in this._saveEntryWriters)
				{
					IOptionalSaveEntryWriter optionalSaveEntryWriter = saveEntryWriter as IOptionalSaveEntryWriter;
					if (optionalSaveEntryWriter == null || optionalSaveEntryWriter.ShouldWrite)
					{
						using (Stream stream = zipArchive.CreateEntry(saveEntryWriter.EntryName, CompressionLevel.Fastest).Open())
						{
							saveEntryWriter.WriteToSaveEntryStream(stream);
						}
					}
				}
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly ImmutableArray<ISaveEntryWriter> _saveEntryWriters;
	}
}
