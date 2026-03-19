using System;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;

namespace Timberborn.MapEditorStartup
{
	// Token: 0x02000003 RID: 3
	public class MapEditorInitializer : IUpdatableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public MapEditorInitializer(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CA File Offset: 0x000002CA
		public void UpdateSingleton()
		{
			if (!this._alreadyInitialized)
			{
				this._eventBus.Post(new ShowPrimaryUIEvent());
				this._alreadyInitialized = true;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool _alreadyInitialized;

		// Token: 0x04000002 RID: 2
		private readonly EventBus _eventBus;
	}
}
