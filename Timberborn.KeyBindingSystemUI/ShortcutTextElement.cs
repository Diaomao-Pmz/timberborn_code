using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000014 RID: 20
	public class ShortcutTextElement
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000030AB File Offset: 0x000012AB
		public ShortcutTextElement(TextElement textElement, bool alwaysVisible)
		{
			this._textElement = textElement;
			this._alwaysVisible = alwaysVisible;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030C1 File Offset: 0x000012C1
		public void SetShortcut(string shortcut)
		{
			this._textElement.text = shortcut;
			this._textElement.ToggleDisplayStyle(true);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000030DB File Offset: 0x000012DB
		public void SetUndefinedShortcut()
		{
			this._textElement.text = string.Empty;
			this._textElement.ToggleDisplayStyle(this._alwaysVisible);
		}

		// Token: 0x04000047 RID: 71
		public readonly TextElement _textElement;

		// Token: 0x04000048 RID: 72
		public readonly bool _alwaysVisible;
	}
}
