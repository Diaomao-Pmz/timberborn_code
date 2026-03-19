using System;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000008 RID: 8
	public class AlternateClickableFactory
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000218E File Offset: 0x0000038E
		public AlternateClickableFactory(InputService inputService)
		{
			this._inputService = inputService;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A0 File Offset: 0x000003A0
		public AlternateClickable Create(VisualElement visualElement, Action mainAction, Action alternateAction)
		{
			AlternateClickable alternateClickable = new AlternateClickable(this._inputService, visualElement, mainAction, alternateAction);
			visualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(alternateClickable.OnClick), 0);
			return alternateClickable;
		}

		// Token: 0x0400000E RID: 14
		public readonly InputService _inputService;
	}
}
