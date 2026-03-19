using System;
using Timberborn.Debugging;
using Timberborn.MapRepositorySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000004 RID: 4
	public class DevModeMapRepositoryChangeNotifier : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public DevModeMapRepositoryChangeNotifier(EventBus eventBus, MapRepository mapRepository)
		{
			this._eventBus = eventBus;
			this._mapRepository = mapRepository;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D6 File Offset: 0x000002D6
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		[OnEvent]
		public void OnDevModeToggled(DevModeToggledEvent devModeToggledEvent)
		{
			this._mapRepository.NotifyMapRepositoryChanged();
		}

		// Token: 0x04000006 RID: 6
		public readonly EventBus _eventBus;

		// Token: 0x04000007 RID: 7
		public readonly MapRepository _mapRepository;
	}
}
