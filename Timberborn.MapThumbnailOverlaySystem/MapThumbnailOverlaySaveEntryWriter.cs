using System;
using System.IO;
using Timberborn.SaveSystem;

namespace Timberborn.MapThumbnailOverlaySystem
{
	// Token: 0x02000006 RID: 6
	public class MapThumbnailOverlaySaveEntryWriter : IOptionalSaveEntryWriter, ISaveEntryWriter
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000023EB File Offset: 0x000005EB
		public MapThumbnailOverlaySaveEntryWriter(MapThumbnailOverlay mapThumbnailOverlay, MapThumbnailOverlaySerializer mapThumbnailOverlaySerializer)
		{
			this._mapThumbnailOverlay = mapThumbnailOverlay;
			this._mapThumbnailOverlaySerializer = mapThumbnailOverlaySerializer;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002401 File Offset: 0x00000601
		public string EntryName
		{
			get
			{
				return this._mapThumbnailOverlaySerializer.EntryName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000240E File Offset: 0x0000060E
		public bool ShouldWrite
		{
			get
			{
				return this._mapThumbnailOverlay.Overlay;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002420 File Offset: 0x00000620
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			this._mapThumbnailOverlaySerializer.WriteToSaveEntryStream(entryStream, this._mapThumbnailOverlay.Overlay);
		}

		// Token: 0x04000011 RID: 17
		public readonly MapThumbnailOverlay _mapThumbnailOverlay;

		// Token: 0x04000012 RID: 18
		public readonly MapThumbnailOverlaySerializer _mapThumbnailOverlaySerializer;
	}
}
