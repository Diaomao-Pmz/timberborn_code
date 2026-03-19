using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200000E RID: 14
	public class DialogBox : IPanelController
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024BE File Offset: 0x000006BE
		public DialogBox(PanelStack panelStack, Action confirmButtonCallback, Action cancelButtonCallback, VisualElement root)
		{
			this._confirmButtonCallback = confirmButtonCallback;
			this._cancelButtonCallback = cancelButtonCallback;
			this._panelStack = panelStack;
			this._root = root;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024E3 File Offset: 0x000006E3
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024EB File Offset: 0x000006EB
		public bool OnUIConfirmed()
		{
			this.Close();
			this._confirmButtonCallback();
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024FF File Offset: 0x000006FF
		public void OnUICancelled()
		{
			this.Close();
			if (this.IsDialog)
			{
				this._cancelButtonCallback();
				return;
			}
			this._confirmButtonCallback();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002526 File Offset: 0x00000726
		public void Close()
		{
			this._panelStack.Pop(this);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002534 File Offset: 0x00000734
		public bool IsDialog
		{
			get
			{
				return this._cancelButtonCallback != null;
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly PanelStack _panelStack;

		// Token: 0x04000015 RID: 21
		public readonly Action _confirmButtonCallback;

		// Token: 0x04000016 RID: 22
		public readonly Action _cancelButtonCallback;

		// Token: 0x04000017 RID: 23
		public readonly VisualElement _root;
	}
}
