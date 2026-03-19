using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000018 RID: 24
	public class InputBox : IPanelController
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002FDF File Offset: 0x000011DF
		public InputBox(PanelStack panelStack, Action<string> confirmButtonCallback, VisualElement root, TextField input)
		{
			this._confirmButtonCallback = confirmButtonCallback;
			this._panelStack = panelStack;
			this._root = root;
			this._input = input;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003004 File Offset: 0x00001204
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000300C File Offset: 0x0000120C
		public bool OnUIConfirmed()
		{
			this._panelStack.Pop(this);
			this._confirmButtonCallback(this._input.text);
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003031 File Offset: 0x00001231
		public void OnUICancelled()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x04000033 RID: 51
		public readonly PanelStack _panelStack;

		// Token: 0x04000034 RID: 52
		public readonly Action<string> _confirmButtonCallback;

		// Token: 0x04000035 RID: 53
		public readonly VisualElement _root;

		// Token: 0x04000036 RID: 54
		public readonly TextField _input;
	}
}
