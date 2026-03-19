using System;
using Timberborn.KeyBindingSystem;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000006 RID: 6
	public class KeyBindingDescriber
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002335 File Offset: 0x00000535
		public KeyBindingDescriber(KeyBindingRegistry keyBindingRegistry, InputBindingDescriber inputBindingDescriber)
		{
			this._keyBindingRegistry = keyBindingRegistry;
			this._inputBindingDescriber = inputBindingDescriber;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000234C File Offset: 0x0000054C
		public bool TryGetKeyBindingText(string keyBindingKey, out string keyBindingText)
		{
			if (keyBindingKey != null)
			{
				KeyBinding keyBinding = this._keyBindingRegistry.Get(keyBindingKey);
				if (keyBinding.PrimaryInputBinding.IsDefined || keyBinding.SecondaryInputBinding.IsDefined)
				{
					InputBinding inputBinding = keyBinding.PrimaryInputBinding.IsDefined ? keyBinding.PrimaryInputBinding : keyBinding.SecondaryInputBinding;
					keyBindingText = this._inputBindingDescriber.GetInputBindingText(inputBinding);
					return true;
				}
			}
			keyBindingText = null;
			return false;
		}

		// Token: 0x0400000B RID: 11
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x0400000C RID: 12
		public readonly InputBindingDescriber _inputBindingDescriber;
	}
}
