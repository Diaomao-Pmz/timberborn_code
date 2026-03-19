using System;
using System.IO;
using Timberborn.MapMetadataSystem;
using Timberborn.SaveSystem;

namespace Timberborn.MapMetadataSystemUI
{
	// Token: 0x02000006 RID: 6
	public class MapMetadataSaveEntryWriter : ISaveEntryWriter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002551 File Offset: 0x00000751
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002559 File Offset: 0x00000759
		public MapMetadata CurrentMapMetadata { get; private set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002562 File Offset: 0x00000762
		public MapMetadataSaveEntryWriter(MapMetadataSerializer mapMetadataSerializer)
		{
			this._mapMetadataSerializer = mapMetadataSerializer;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002571 File Offset: 0x00000771
		public string EntryName
		{
			get
			{
				return this._mapMetadataSerializer.EntryName;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000257E File Offset: 0x0000077E
		public void SetCurrentMapMetadata(MapMetadata mapMetadata)
		{
			this.CurrentMapMetadata = mapMetadata;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002587 File Offset: 0x00000787
		public void WriteToSaveEntryStream(Stream entryStream)
		{
			this._mapMetadataSerializer.WriteToSaveEntryStream(entryStream, this.CurrentMapMetadata);
		}

		// Token: 0x0400001D RID: 29
		public readonly MapMetadataSerializer _mapMetadataSerializer;
	}
}
