using System;
using System.IO;
using Timberborn.SaveSystem;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.SaveThumbnail
{
	// Token: 0x02000006 RID: 6
	public class SaveThumbnailSaveEntryReader : ISaveEntryReader<Texture2D>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020FC File Offset: 0x000002FC
		public SaveThumbnailSaveEntryReader(SaveThumbnailConfiguration saveThumbnailConfiguration, ThumbnailSerializer thumbnailSerializer)
		{
			this._saveThumbnailConfiguration = saveThumbnailConfiguration;
			this._thumbnailSerializer = thumbnailSerializer;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002112 File Offset: 0x00000312
		public string EntryName
		{
			get
			{
				return this._saveThumbnailConfiguration.Name;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000211F File Offset: 0x0000031F
		public Texture2D ReadFromSaveEntryStream(Stream entryStream)
		{
			return this._thumbnailSerializer.ReadFromSaveEntryStream(entryStream, this._saveThumbnailConfiguration);
		}

		// Token: 0x04000006 RID: 6
		public readonly SaveThumbnailConfiguration _saveThumbnailConfiguration;

		// Token: 0x04000007 RID: 7
		public readonly ThumbnailSerializer _thumbnailSerializer;
	}
}
