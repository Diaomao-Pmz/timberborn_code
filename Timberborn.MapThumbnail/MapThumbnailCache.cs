using System;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.MapThumbnail
{
	// Token: 0x02000004 RID: 4
	public class MapThumbnailCache : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public MapThumbnailCache(MapDeserializer mapDeserializer, MapThumbnailSaveEntryReader mapThumbnailSaveEntryReader)
		{
			this._mapDeserializer = mapDeserializer;
			this._mapThumbnailSaveEntryReader = mapThumbnailSaveEntryReader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Load()
		{
			this._thumbnailCache = new ThumbnailCache<MapFileReference>((MapFileReference reference) => this._mapDeserializer.ReadFromMapFile<Texture2D>(reference, this._mapThumbnailSaveEntryReader));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020ED File Offset: 0x000002ED
		public void Unload()
		{
			this.Clear();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F5 File Offset: 0x000002F5
		public void Clear()
		{
			this._thumbnailCache.Clear();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002102 File Offset: 0x00000302
		public Texture2D GetThumbnail(MapFileReference mapFileReference)
		{
			return this._thumbnailCache.GetThumbnail(mapFileReference);
		}

		// Token: 0x04000006 RID: 6
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x04000007 RID: 7
		public readonly MapThumbnailSaveEntryReader _mapThumbnailSaveEntryReader;

		// Token: 0x04000008 RID: 8
		public ThumbnailCache<MapFileReference> _thumbnailCache;
	}
}
