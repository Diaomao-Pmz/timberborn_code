using System;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000007 RID: 7
	public class AlternateClickable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public VisualElement Root { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public AlternateClickable(InputService inputService, VisualElement root, Action mainAction, Action alternateAction)
		{
			this._inputService = inputService;
			this.Root = root;
			this._mainAction = mainAction;
			this._alternateAction = alternateAction;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212D File Offset: 0x0000032D
		public void Update()
		{
			this.Root.EnableInClassList(AlternateClickable.AlternateClass, this.IsAlternating);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002145 File Offset: 0x00000345
		public void OnClick(ClickEvent evt)
		{
			if (this.IsAlternating)
			{
				this._alternateAction();
				return;
			}
			this._mainAction();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002166 File Offset: 0x00000366
		public bool IsAlternating
		{
			get
			{
				return this._inputService.IsKeyHeld(AlternateClickable.AlternateClickableActionKey);
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly string AlternateClass = "clickable--alternate";

		// Token: 0x04000009 RID: 9
		public static readonly string AlternateClickableActionKey = "AlternateClickableAction";

		// Token: 0x0400000B RID: 11
		public readonly InputService _inputService;

		// Token: 0x0400000C RID: 12
		public readonly Action _mainAction;

		// Token: 0x0400000D RID: 13
		public readonly Action _alternateAction;
	}
}
