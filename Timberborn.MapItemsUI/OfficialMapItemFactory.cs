using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.Debugging;
using Timberborn.Localization;
using Timberborn.MapMetadataSystem;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MapItemsUI
{
	// Token: 0x0200000F RID: 15
	public class OfficialMapItemFactory : ILoadableSingleton
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000272E File Offset: 0x0000092E
		public OfficialMapItemFactory(ILoc loc, MapDeserializer mapDeserializer, MapMetadataSerializer mapMetadataSerializer, MapRepository mapRepository, DevModeManager devModeManager, IAssetLoader assetLoader)
		{
			this._loc = loc;
			this._mapDeserializer = mapDeserializer;
			this._mapMetadataSerializer = mapMetadataSerializer;
			this._mapRepository = mapRepository;
			this._devModeManager = devModeManager;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002763 File Offset: 0x00000963
		public void Load()
		{
			this._devIcon = new MapIcon(this._assetLoader.Load<Sprite>("UI/Images/Core/dev-map-icon"), null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002784 File Offset: 0x00000984
		public IEnumerable<MapItem> Create()
		{
			return from mapItem in this._mapRepository.GetBuiltinMapNames().Select(new Func<string, MapItem>(this.Create))
			where !mapItem.IsDev || this._devModeManager.Enabled
			orderby mapItem.IsDev, !mapItem.IsRecommended, mapItem.IsUnconventional, mapItem.DisplayName
			select mapItem;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002850 File Offset: 0x00000A50
		public MapItem Create(string name)
		{
			MapFileReference mapFileReference = MapFileReference.FromResource(name);
			MapMetadata mapMetadata = this._mapDeserializer.ReadFromMapFile<MapMetadata>(mapFileReference, this._mapMetadataSerializer);
			return new MapItem(mapFileReference, this.GetDisplayName(mapFileReference, mapMetadata), this.GetDisplayDescription(mapMetadata), new Vector2Int?(new Vector2Int(mapMetadata.Width, mapMetadata.Height)), mapMetadata.IsRecommended, mapMetadata.IsUnconventional, false, mapMetadata.IsDev, mapMetadata.IsDev ? this._devIcon : null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028C6 File Offset: 0x00000AC6
		public string GetDisplayName(MapFileReference mapFileReference, MapMetadata mapMetadata)
		{
			if (!string.IsNullOrEmpty(mapMetadata.MapNameLocKey))
			{
				return this._loc.T(mapMetadata.MapNameLocKey);
			}
			return mapFileReference.Name;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028EE File Offset: 0x00000AEE
		public string GetDisplayDescription(MapMetadata mapMetadata)
		{
			if (!string.IsNullOrEmpty(mapMetadata.MapDescriptionLocKey))
			{
				return this._loc.T(mapMetadata.MapDescriptionLocKey);
			}
			return null;
		}

		// Token: 0x04000029 RID: 41
		public readonly ILoc _loc;

		// Token: 0x0400002A RID: 42
		public readonly MapDeserializer _mapDeserializer;

		// Token: 0x0400002B RID: 43
		public readonly MapMetadataSerializer _mapMetadataSerializer;

		// Token: 0x0400002C RID: 44
		public readonly MapRepository _mapRepository;

		// Token: 0x0400002D RID: 45
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400002E RID: 46
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400002F RID: 47
		public MapIcon _devIcon;
	}
}
