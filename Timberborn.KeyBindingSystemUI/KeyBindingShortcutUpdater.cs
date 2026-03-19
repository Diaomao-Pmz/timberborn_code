using System;
using System.Collections.Generic;
using Timberborn.KeyBindingSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x0200000E RID: 14
	public class KeyBindingShortcutUpdater : ILoadableSingleton
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002A71 File Offset: 0x00000C71
		public KeyBindingShortcutUpdater(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A8B File Offset: 0x00000C8B
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A99 File Offset: 0x00000C99
		public void AddShortcut(KeyBindingShortcut keyBindingShortcut)
		{
			keyBindingShortcut.Update();
			this._keyBindingShortcuts.Add(keyBindingShortcut);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[OnEvent]
		public void OnKeyRebound(KeyReboundEvent keyReboundEvent)
		{
			foreach (KeyBindingShortcut keyBindingShortcut in this._keyBindingShortcuts)
			{
				if (keyBindingShortcut.KeyBindingId == keyReboundEvent.KeyBindingId)
				{
					keyBindingShortcut.Update();
				}
			}
		}

		// Token: 0x0400002C RID: 44
		public readonly EventBus _eventBus;

		// Token: 0x0400002D RID: 45
		public readonly List<KeyBindingShortcut> _keyBindingShortcuts = new List<KeyBindingShortcut>();
	}
}
