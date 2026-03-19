using System;
using System.Text;
using Timberborn.Common;
using Timberborn.Localization;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000010 RID: 16
	public class KeyBindingTooltipFactory
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002B99 File Offset: 0x00000D99
		public KeyBindingTooltipFactory(ILoc loc, KeyBindingDescriber keyBindingDescriber)
		{
			this._loc = loc;
			this._keyBindingDescriber = keyBindingDescriber;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BBA File Offset: 0x00000DBA
		public string Create(string headerLocKey, string toggleBindingKey, string holdBindingKey)
		{
			this._tooltip.AppendLine(this._loc.T(headerLocKey));
			this.AddKeyBindingInfo(toggleBindingKey, KeyBindingTooltipFactory.ToggleLocKey);
			this.AddKeyBindingInfo(holdBindingKey, KeyBindingTooltipFactory.HoldLocKey);
			return this._tooltip.ToStringWithoutNewLineEndAndClean();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void AddKeyBindingInfo(string keyBindingKey, string actionTypeLocKey)
		{
			string str;
			if (this._keyBindingDescriber.TryGetKeyBindingText(keyBindingKey, out str))
			{
				this._tooltip.AppendLine(this._loc.T(actionTypeLocKey) + " " + str);
			}
		}

		// Token: 0x0400002E RID: 46
		public static readonly string ToggleLocKey = "KeyBinding.Toggle";

		// Token: 0x0400002F RID: 47
		public static readonly string HoldLocKey = "KeyBinding.Hold";

		// Token: 0x04000030 RID: 48
		public readonly ILoc _loc;

		// Token: 0x04000031 RID: 49
		public readonly KeyBindingDescriber _keyBindingDescriber;

		// Token: 0x04000032 RID: 50
		public readonly StringBuilder _tooltip = new StringBuilder();
	}
}
