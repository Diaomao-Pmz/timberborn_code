using System;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;
using Timberborn.SteamWorkshop;

namespace Timberborn.SteamWorkshopMapDownloading
{
	// Token: 0x02000004 RID: 4
	public class SteamMapRepositoryChangeNotifier : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public SteamMapRepositoryChangeNotifier(EventBus eventBus, MapRepository mapRepository)
		{
			this._eventBus = eventBus;
			this._mapRepository = mapRepository;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E2 File Offset: 0x000002E2
		[OnEvent]
		public void OnItemInstalled(ItemInstalledEvent itemInstalledEvent)
		{
			this._mapRepository.NotifyMapRepositoryChanged();
		}

		// Token: 0x04000006 RID: 6
		public readonly EventBus _eventBus;

		// Token: 0x04000007 RID: 7
		public readonly MapRepository _mapRepository;
	}
}
