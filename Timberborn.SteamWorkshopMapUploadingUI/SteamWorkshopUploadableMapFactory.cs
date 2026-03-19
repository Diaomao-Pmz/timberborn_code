using System;
using Timberborn.FileSystem;
using Timberborn.MapEditorPersistenceUI;
using Timberborn.MapMetadataSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.MapThumbnail;
using UnityEngine;

namespace Timberborn.SteamWorkshopMapUploadingUI
{
	// Token: 0x02000008 RID: 8
	public class SteamWorkshopUploadableMapFactory
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002634 File Offset: 0x00000834
		public SteamWorkshopUploadableMapFactory(MapDeserializer mapDeserializer, MapMetadataSerializer mapMetadataSerializer, FilenameValidator filenameValidator, SteamWorkshopMapDataService steamWorkshopMapDataService, MapThumbnailCache mapThumbnailCache, IFileService fileService, MapRepository mapRepository, MapSaverLoader mapSaverLoader)
		{
			this._mapDeserializer = mapDeserializer;
			this._mapMetadataSerializer = mapMetadataSerializer;
			this._filenameValidator = filenameValidator;
			this._steamWorkshopMapDataService = steamWorkshopMapDataService;
			this._mapThumbnailCache = mapThumbnailCache;
			this._fileService = fileService;
			this._mapRepository = mapRepository;
			this._mapSaverLoader = mapSaverLoader;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002684 File Offset: 0x00000884
		public SteamWorkshopUploadableMap Create(MapFileReference mapFileReference)
		{
			this._mapThumbnailCache.Clear();
			MapMetadata mapMetadata = this._mapDeserializer.ReadFromMapFileUnsafe<MapMetadata>(mapFileReference, this._mapMetadataSerializer);
			Texture2D thumbnail = this._mapThumbnailCache.GetThumbnail(mapFileReference);
			SteamWorkshopMapContent steamWorkshopMapContent = new SteamWorkshopMapContent(this._fileService, this._mapRepository, thumbnail, mapFileReference);
			return new SteamWorkshopUploadableMap(this._steamWorkshopMapDataService, this._filenameValidator, this._mapSaverLoader, steamWorkshopMapContent, mapFileReference, mapMetadata);
		}

		// Token: 0x0400001D RID: 29
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x0400001E RID: 30
		public readonly MapMetadataSerializer _mapMetadataSerializer;

		// Token: 0x0400001F RID: 31
		public readonly FilenameValidator _filenameValidator;

		// Token: 0x04000020 RID: 32
		public readonly SteamWorkshopMapDataService _steamWorkshopMapDataService;

		// Token: 0x04000021 RID: 33
		public readonly MapThumbnailCache _mapThumbnailCache;

		// Token: 0x04000022 RID: 34
		public readonly IFileService _fileService;

		// Token: 0x04000023 RID: 35
		public readonly MapRepository _mapRepository;

		// Token: 0x04000024 RID: 36
		public readonly MapSaverLoader _mapSaverLoader;
	}
}
