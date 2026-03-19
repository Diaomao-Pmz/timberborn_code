using System;
using Timberborn.Common;
using Timberborn.MapEditorPersistence;
using Timberborn.MapRepositorySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.SteamWorkshop;
using Timberborn.WorldPersistence;

namespace Timberborn.SteamWorkshopMapUploadingUI
{
	// Token: 0x02000005 RID: 5
	public class SteamWorkshopMapDataService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022C2 File Offset: 0x000004C2
		public SteamWorkshopItem SteamWorkshopItem { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000022CB File Offset: 0x000004CB
		public SteamWorkshopMapDataService(MapEditorMapLoader mapEditorMapLoader, SteamWorkshopItemSerializer steamWorkshopItemSerializer, ISingletonLoader singletonLoader)
		{
			this._mapEditorMapLoader = mapEditorMapLoader;
			this._steamWorkshopItemSerializer = steamWorkshopItemSerializer;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E8 File Offset: 0x000004E8
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this.SteamWorkshopItem != null)
			{
				singletonSaver.GetSingleton(SteamWorkshopMapDataService.SteamWorkshopMapDataServiceKey).Set<SteamWorkshopItem>(SteamWorkshopMapDataService.SteamWorkshopItemKey, this.SteamWorkshopItem, this._steamWorkshopItemSerializer);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002314 File Offset: 0x00000514
		[BackwardCompatible(2024, 5, 7, Compatibility.Map)]
		public void Load()
		{
			MapFileReference? loadedMap = this._mapEditorMapLoader.LoadedMap;
			IObjectLoader objectLoader;
			if (loadedMap != null && loadedMap.GetValueOrDefault().UserFolder && this._singletonLoader.TryGetSingleton(SteamWorkshopMapDataService.SteamWorkshopMapDataServiceKey, out objectLoader))
			{
				if (objectLoader.Has<SteamWorkshopItem>(SteamWorkshopMapDataService.SteamWorkshopItemKey))
				{
					this.SteamWorkshopItem = objectLoader.Get<SteamWorkshopItem>(SteamWorkshopMapDataService.SteamWorkshopItemKey, this._steamWorkshopItemSerializer);
					return;
				}
				PropertyKey<SteamWorkshopItem> key = new PropertyKey<SteamWorkshopItem>("SteamWorkshopItemData");
				if (objectLoader.Has<SteamWorkshopItem>(key))
				{
					this.SteamWorkshopItem = objectLoader.Get<SteamWorkshopItem>(key, this._steamWorkshopItemSerializer);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A8 File Offset: 0x000005A8
		public void SetMapData(SteamWorkshopItem item)
		{
			this.SteamWorkshopItem = item;
		}

		// Token: 0x0400000E RID: 14
		public static readonly SingletonKey SteamWorkshopMapDataServiceKey = new SingletonKey("SteamWorkshopMapDataService");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<SteamWorkshopItem> SteamWorkshopItemKey = new PropertyKey<SteamWorkshopItem>("SteamWorkshopItem");

		// Token: 0x04000011 RID: 17
		public readonly MapEditorMapLoader _mapEditorMapLoader;

		// Token: 0x04000012 RID: 18
		public readonly SteamWorkshopItemSerializer _steamWorkshopItemSerializer;

		// Token: 0x04000013 RID: 19
		public readonly ISingletonLoader _singletonLoader;
	}
}
