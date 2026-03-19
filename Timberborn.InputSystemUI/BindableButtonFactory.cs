using System;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.InputSystemUI
{
	// Token: 0x02000005 RID: 5
	public class BindableButtonFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002177 File Offset: 0x00000377
		public BindableButtonFactory(InputService inputService)
		{
			this._inputService = inputService;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		public BindableButton Create(VisualElement button, string bindingKey, Action action, bool blockInput = true)
		{
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				action();
			}, 0);
			return new BindableButton(this._inputService, button, bindingKey, action, blockInput);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021CA File Offset: 0x000003CA
		public BindableButton CreateAndBind(VisualElement button, string bindingKey, Action action)
		{
			BindableButton bindableButton = this.Create(button, bindingKey, action, true);
			bindableButton.Bind();
			return bindableButton;
		}

		// Token: 0x0400000C RID: 12
		public readonly InputService _inputService;
	}
}
