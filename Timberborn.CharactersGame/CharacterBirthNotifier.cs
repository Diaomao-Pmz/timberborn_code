using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.NotificationSystem;

namespace Timberborn.CharactersGame
{
	// Token: 0x02000007 RID: 7
	public class CharacterBirthNotifier : BaseComponent, IPostInitializableEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public CharacterBirthNotifier(NotificationBus notificationBus, ILoc loc)
		{
			this._notificationBus = notificationBus;
			this._loc = loc;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002111 File Offset: 0x00000311
		public void PostInitializeEntity()
		{
			if (this._notificationEnabled)
			{
				this._notificationBus.Post(this._loc.T<string>(base.GetComponent<CharacterBirthNotifierSpec>().NotificationLocKey, base.GetComponent<NamedEntity>().EntityName), this);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public void EnableNotification()
		{
			this._notificationEnabled = true;
		}

		// Token: 0x04000008 RID: 8
		public readonly NotificationBus _notificationBus;

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;

		// Token: 0x0400000A RID: 10
		public bool _notificationEnabled;
	}
}
