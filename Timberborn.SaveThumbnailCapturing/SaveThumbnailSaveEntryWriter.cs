using System;
using System.IO;
using Timberborn.ErrorReporting;
using Timberborn.SaveSystem;
using Timberborn.SaveThumbnail;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.SaveThumbnailCapturing
{
	// Token: 0x02000007 RID: 7
	public class SaveThumbnailSaveEntryWriter : ISaveEntryWriter
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002170 File Offset: 0x00000370
		public SaveThumbnailSaveEntryWriter(SaveThumbnailConfiguration saveThumbnailConfiguration, ThumbnailSaveEntryWriter thumbnailSaveEntryWriter)
		{
			this._saveThumbnailConfiguration = saveThumbnailConfiguration;
			this._thumbnailSaveEntryWriter = thumbnailSaveEntryWriter;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002186 File Offset: 0x00000386
		public string EntryName
		{
			get
			{
				return this._saveThumbnailConfiguration.Name;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002193 File Offset: 0x00000393
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			if (!ErrorReporter.ErrorReported)
			{
				this._thumbnailSaveEntryWriter.WriteToSaveEntryStream(entryStream, this._saveThumbnailConfiguration, null);
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly SaveThumbnailConfiguration _saveThumbnailConfiguration;

		// Token: 0x04000009 RID: 9
		public readonly ThumbnailSaveEntryWriter _thumbnailSaveEntryWriter;
	}
}
