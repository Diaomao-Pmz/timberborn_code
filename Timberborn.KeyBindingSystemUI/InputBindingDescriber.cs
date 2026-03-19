using System;
using System.Text;
using Timberborn.Common;
using Timberborn.KeyBindingSystem;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000005 RID: 5
	public class InputBindingDescriber
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002223 File Offset: 0x00000423
		public InputBindingDescriber(InputBindingNameService inputBindingNameService, KeyBindingRegistry keyBindingRegistry)
		{
			this._inputBindingNameService = inputBindingNameService;
			this._keyBindingRegistry = keyBindingRegistry;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002244 File Offset: 0x00000444
		public string GetInputBindingText(string keyBindingId)
		{
			KeyBinding keyBinding = this._keyBindingRegistry.Get(keyBindingId);
			InputBinding inputBinding = keyBinding.PrimaryInputBinding.IsDefined ? keyBinding.PrimaryInputBinding : keyBinding.SecondaryInputBinding;
			return this.GetInputBindingText(inputBinding);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002284 File Offset: 0x00000484
		public string GetInputBindingText(InputBinding inputBinding)
		{
			this.AddModifierIfUsed(inputBinding, InputModifiers.Ctrl);
			this.AddModifierIfUsed(inputBinding, InputModifiers.Cmd);
			this.AddModifierIfUsed(inputBinding, InputModifiers.Shift);
			this.AddModifierIfUsed(inputBinding, InputModifiers.Alt);
			this._inputBindingText.Append(this._inputBindingNameService.GetName(inputBinding));
			return this._inputBindingText.ToStringAndClear();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022D4 File Offset: 0x000004D4
		public string GetKeyBindingDisplayName(string keyBindingId)
		{
			return this._keyBindingRegistry.Get(keyBindingId).DisplayName;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022E8 File Offset: 0x000004E8
		public void AddModifierIfUsed(InputBinding inputBinding, InputModifiers inputModifier)
		{
			if (inputBinding.HasModifier(inputModifier))
			{
				string inputModifierName = this._inputBindingNameService.GetInputModifierName(inputModifier);
				this._inputBindingText.Append(inputModifierName);
				this._inputBindingText.Append(InputBindingDescriber.KeySeparator);
			}
		}

		// Token: 0x04000007 RID: 7
		public static readonly string KeySeparator = " + ";

		// Token: 0x04000008 RID: 8
		public readonly InputBindingNameService _inputBindingNameService;

		// Token: 0x04000009 RID: 9
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x0400000A RID: 10
		public readonly StringBuilder _inputBindingText = new StringBuilder();
	}
}
