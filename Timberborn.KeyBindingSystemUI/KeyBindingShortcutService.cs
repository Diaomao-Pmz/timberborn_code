using System;
using Timberborn.KeyBindingSystem;
using UnityEngine.UIElements;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x0200000D RID: 13
	public class KeyBindingShortcutService
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000029EB File Offset: 0x00000BEB
		public KeyBindingShortcutService(InputBindingDescriber inputBindingDescriber, KeyBindingRegistry keyBindingRegistry, KeyBindingShortcutUpdater keyBindingShortcutUpdater)
		{
			this._inputBindingDescriber = inputBindingDescriber;
			this._keyBindingRegistry = keyBindingRegistry;
			this._keyBindingShortcutUpdater = keyBindingShortcutUpdater;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A08 File Offset: 0x00000C08
		public void CreateSingle(TextElement textElement, DefinableInputBinding definableInputBinding)
		{
			this.AddShortcut(new KeyBindingShortcut(this._inputBindingDescriber, definableInputBinding, new ShortcutTextElement(textElement, true)));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A24 File Offset: 0x00000C24
		public void CreateAny(TextElement textElement, string keyId)
		{
			this.AddShortcut(new KeyBindingShortcut(this._inputBindingDescriber, new DefinableInputBinding(this._keyBindingRegistry.Get(keyId), null), new ShortcutTextElement(textElement, false)));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A63 File Offset: 0x00000C63
		public void AddShortcut(KeyBindingShortcut keyBindingShortcut)
		{
			this._keyBindingShortcutUpdater.AddShortcut(keyBindingShortcut);
		}

		// Token: 0x04000029 RID: 41
		public readonly InputBindingDescriber _inputBindingDescriber;

		// Token: 0x0400002A RID: 42
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x0400002B RID: 43
		public readonly KeyBindingShortcutUpdater _keyBindingShortcutUpdater;
	}
}
