using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.MapItemsUI;
using Timberborn.MapMetadataSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using Timberborn.SteamWorkshopContent;
using UnityEngine;

namespace Timberborn.SteamWorkshopMapDownloading
{
	// Token: 0x02000006 RID: 6
	public class SteamWorkshopMapItemFactory : ICustomMapItemFactory, ILoadableSingleton
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002116 File Offset: 0x00000316
		public SteamWorkshopMapItemFactory(MapRepository mapRepository, MapDeserializer mapDeserializer, MapMetadataSerializer mapMetadataSerializer, SteamWorkshopContentProvider steamWorkshopContentProvider, IAssetLoader assetLoader)
		{
			this._mapRepository = mapRepository;
			this._mapDeserializer = mapDeserializer;
			this._mapMetadataSerializer = mapMetadataSerializer;
			this._steamWorkshopContentProvider = steamWorkshopContentProvider;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002143 File Offset: 0x00000343
		public void Load()
		{
			this._steamMapIcon = new MapIcon(this._assetLoader.Load<Sprite>(SteamWorkshopMapItemFactory.CloudIconPath), SteamWorkshopMapItemFactory.SteamMapLocKey);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002165 File Offset: 0x00000365
		public IEnumerable<MapItem> Create()
		{
			return from item in this.CreateInternal()
			orderby item.DisplayName
			select item;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002191 File Offset: 0x00000391
		public IEnumerable<MapItem> CreateInternal()
		{
			foreach (DirectoryInfo directoryInfo in this._steamWorkshopContentProvider.GetContentDirectories())
			{
				foreach (FileInfo mapFile in this._mapRepository.GetMapFilesFromDirectory(directoryInfo))
				{
					yield return this.Create(mapFile);
				}
				IEnumerator<FileInfo> enumerator2 = null;
			}
			IEnumerator<DirectoryInfo> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A4 File Offset: 0x000003A4
		public MapItem Create(FileInfo mapFile)
		{
			MapFileReference mapFileReference = MapFileReference.FromDisk(mapFile.FullName);
			MapMetadata mapMetadata = this._mapDeserializer.ReadFromMapFile<MapMetadata>(mapFileReference, this._mapMetadataSerializer);
			return new MapItem(mapFileReference, mapFileReference.Name, (mapMetadata != null) ? mapMetadata.MapDescription : null, SteamWorkshopMapItemFactory.GetSize(mapMetadata), false, false, false, false, this._steamMapIcon);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FC File Offset: 0x000003FC
		public static Vector2Int? GetSize(MapMetadata mapMetadata)
		{
			if (mapMetadata != null)
			{
				return new Vector2Int?(new Vector2Int(mapMetadata.Width, mapMetadata.Height));
			}
			return null;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string CloudIconPath = "UI/Images/Core/cloud-file-icon";

		// Token: 0x04000009 RID: 9
		public static readonly string SteamMapLocKey = "SteamWorkshop.SteamMapTooltip";

		// Token: 0x0400000A RID: 10
		public readonly MapRepository _mapRepository;

		// Token: 0x0400000B RID: 11
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x0400000C RID: 12
		public readonly MapMetadataSerializer _mapMetadataSerializer;

		// Token: 0x0400000D RID: 13
		public readonly SteamWorkshopContentProvider _steamWorkshopContentProvider;

		// Token: 0x0400000E RID: 14
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000F RID: 15
		public MapIcon _steamMapIcon;
	}
}
