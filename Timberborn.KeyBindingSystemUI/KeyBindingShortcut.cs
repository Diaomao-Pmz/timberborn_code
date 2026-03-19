using System;
using Timberborn.KeyBindingSystem;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x0200000C RID: 12
	public class KeyBindingShortcut
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000297B File Offset: 0x00000B7B
		public KeyBindingShortcut(InputBindingDescriber inputBindingDescriber, DefinableInputBinding definableInputBinding, ShortcutTextElement shortcutTextElement)
		{
			this._inputBindingDescriber = inputBindingDescriber;
			this._definableInputBinding = definableInputBinding;
			this._shortcutTextElement = shortcutTextElement;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002998 File Offset: 0x00000B98
		public string KeyBindingId
		{
			get
			{
				return this._definableInputBinding.KeyBinding.Id;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029AC File Offset: 0x00000BAC
		public void Update()
		{
			InputBinding inputBinding;
			if (this._definableInputBinding.TryGetDefinedInputBinding(out inputBinding))
			{
				this._shortcutTextElement.SetShortcut(this._inputBindingDescriber.GetInputBindingText(inputBinding));
				return;
			}
			this._shortcutTextElement.SetUndefinedShortcut();
		}

		// Token: 0x04000026 RID: 38
		public readonly InputBindingDescriber _inputBindingDescriber;

		// Token: 0x04000027 RID: 39
		public readonly DefinableInputBinding _definableInputBinding;

		// Token: 0x04000028 RID: 40
		public readonly ShortcutTextElement _shortcutTextElement;
	}
}
