using System;
using Timberborn.EntityNaming;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000009 RID: 9
	public class CitizenNameTintChanger : ILoadableSingleton
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002264 File Offset: 0x00000464
		public CitizenNameTintChanger(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002273 File Offset: 0x00000473
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002284 File Offset: 0x00000484
		[OnEvent]
		public void OnEntityNameChanged(EntityNameChangedEvent entityNameChangedEvent)
		{
			CitizenTint component = entityNameChangedEvent.Entity.GetComponent<CitizenTint>();
			if (component)
			{
				component.UpdateTint();
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly EventBus _eventBus;
	}
}
