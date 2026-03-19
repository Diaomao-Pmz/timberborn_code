using System;
using System.IO;
using Timberborn.SaveSystem;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.MapThumbnail
{
	// Token: 0x02000008 RID: 8
	public class MapThumbnailSaveEntryReader : ISaveEntryReader<Texture2D>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000216E File Offset: 0x0000036E
		public MapThumbnailSaveEntryReader(MapThumbnailConfiguration mapThumbnailConfiguration, ThumbnailSerializer thumbnailSerializer)
		{
			this._mapThumbnailConfiguration = mapThumbnailConfiguration;
			this._thumbnailSerializer = thumbnailSerializer;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002184 File Offset: 0x00000384
		public string EntryName
		{
			get
			{
				return this._mapThumbnailConfiguration.Name;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002191 File Offset: 0x00000391
		public Texture2D ReadFromSaveEntryStream(Stream entryStream)
		{
			return this._thumbnailSerializer.ReadFromSaveEntryStream(entryStream, this._mapThumbnailConfiguration);
		}

		// Token: 0x04000009 RID: 9
		public readonly MapThumbnailConfiguration _mapThumbnailConfiguration;

		// Token: 0x0400000A RID: 10
		public readonly ThumbnailSerializer _thumbnailSerializer;
	}
}
