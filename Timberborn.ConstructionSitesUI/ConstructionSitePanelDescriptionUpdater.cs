using System;
using Timberborn.BlockSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x02000008 RID: 8
	public class ConstructionSitePanelDescriptionUpdater : ILoadableSingleton
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000025A8 File Offset: 0x000007A8
		public ConstructionSitePanelDescriptionUpdater(IEntityPanel entityPanel, EventBus eventBus)
		{
			this._entityPanel = entityPanel;
			this._eventBus = eventBus;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025BE File Offset: 0x000007BE
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025CC File Offset: 0x000007CC
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BlockObject blockObject = enteredFinishedStateEvent.BlockObject;
			this._entityPanel.ReloadDescription(blockObject.GetComponent<EntityComponent>());
		}

		// Token: 0x0400001F RID: 31
		public readonly IEntityPanel _entityPanel;

		// Token: 0x04000020 RID: 32
		public readonly EventBus _eventBus;
	}
}
