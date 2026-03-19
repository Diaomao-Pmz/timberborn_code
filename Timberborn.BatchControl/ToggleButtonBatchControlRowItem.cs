using System;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000026 RID: 38
	public class ToggleButtonBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003E4D File Offset: 0x0000204D
		public VisualElement Root { get; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00003E55 File Offset: 0x00002055
		public ToggleButtonBatchControlRowItem(VisualElement root, Button button, Func<bool> stateGetter)
		{
			this.Root = root;
			this._button = button;
			this._stateGetter = stateGetter;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E72 File Offset: 0x00002072
		public void UpdateRowItem()
		{
			this._button.EnableInClassList(ToggleButtonBatchControlRowItem.ActiveClass, this._stateGetter());
		}

		// Token: 0x0400006D RID: 109
		public static readonly string ActiveClass = "toggle-active";

		// Token: 0x0400006F RID: 111
		public readonly Button _button;

		// Token: 0x04000070 RID: 112
		public readonly Func<bool> _stateGetter;
	}
}
