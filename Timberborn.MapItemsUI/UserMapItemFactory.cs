using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.MapMetadataSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MapItemsUI
{
	// Token: 0x02000011 RID: 17
	public class UserMapItemFactory : ILoadableSingleton
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002956 File Offset: 0x00000B56
		public UserMapItemFactory(MapDeserializer mapDeserializer, MapMetadataSerializer mapMetadataSerializer, MapRepository mapRepository, IAssetLoader assetLoader)
		{
			this._mapDeserializer = mapDeserializer;
			this._mapMetadataSerializer = mapMetadataSerializer;
			this._mapRepository = mapRepository;
			this._assetLoader = assetLoader;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000297B File Offset: 0x00000B7B
		public void Load()
		{
			this._userMapIcon = new MapIcon(this._assetLoader.Load<Sprite>("UI/Images/Core/local-file-icon"), UserMapItemFactory.LocalMapLocKey);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000299D File Offset: 0x00000B9D
		public IEnumerable<MapItem> Create()
		{
			return this._mapRepository.GetUserMapNames().Select(new Func<string, MapItem>(this.Create));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029BC File Offset: 0x00000BBC
		public MapItem Create(string name)
		{
			MapFileReference mapFileReference = MapFileReference.FromUserFolder(name);
			MapMetadata mapMetadata = this._mapDeserializer.ReadFromMapFile<MapMetadata>(mapFileReference, this._mapMetadataSerializer);
			return new MapItem(mapFileReference, mapFileReference.Name, (mapMetadata != null) ? mapMetadata.MapDescription : null, UserMapItemFactory.GetSize(mapMetadata), false, false, true, false, this._userMapIcon);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A0C File Offset: 0x00000C0C
		public static Vector2Int? GetSize(MapMetadata mapMetadata)
		{
			if (mapMetadata != null)
			{
				return new Vector2Int?(new Vector2Int(mapMetadata.Width, mapMetadata.Height));
			}
			return null;
		}

		// Token: 0x04000035 RID: 53
		public static readonly string LocalMapLocKey = "MapSelection.LocalMap";

		// Token: 0x04000036 RID: 54
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x04000037 RID: 55
		public readonly MapMetadataSerializer _mapMetadataSerializer;

		// Token: 0x04000038 RID: 56
		public readonly MapRepository _mapRepository;

		// Token: 0x04000039 RID: 57
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400003A RID: 58
		public MapIcon _userMapIcon;
	}
}
