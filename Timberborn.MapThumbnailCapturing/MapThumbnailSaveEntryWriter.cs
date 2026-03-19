using System;
using System.IO;
using Timberborn.ErrorReporting;
using Timberborn.MapThumbnail;
using Timberborn.MapThumbnailOverlaySystem;
using Timberborn.SaveSystem;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x0200000A RID: 10
	public class MapThumbnailSaveEntryWriter : ISaveEntryWriter
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002492 File Offset: 0x00000692
		public MapThumbnailSaveEntryWriter(MapThumbnailConfiguration mapThumbnailConfiguration, ThumbnailSaveEntryWriter thumbnailSaveEntryWriter, MapThumbnailOverlay mapThumbnailOverlay)
		{
			this._mapThumbnailConfiguration = mapThumbnailConfiguration;
			this._thumbnailSaveEntryWriter = thumbnailSaveEntryWriter;
			this._mapThumbnailOverlay = mapThumbnailOverlay;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024AF File Offset: 0x000006AF
		public string EntryName
		{
			get
			{
				return this._mapThumbnailConfiguration.Name;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024BC File Offset: 0x000006BC
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			if (!ErrorReporter.ErrorReported)
			{
				this._thumbnailSaveEntryWriter.WriteToSaveEntryStream(entryStream, this._mapThumbnailConfiguration, this._mapThumbnailOverlay.Overlay);
			}
		}

		// Token: 0x0400001B RID: 27
		public readonly MapThumbnailConfiguration _mapThumbnailConfiguration;

		// Token: 0x0400001C RID: 28
		public readonly ThumbnailSaveEntryWriter _thumbnailSaveEntryWriter;

		// Token: 0x0400001D RID: 29
		public readonly MapThumbnailOverlay _mapThumbnailOverlay;
	}
}
