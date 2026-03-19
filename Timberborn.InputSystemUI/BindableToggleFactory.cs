using System;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.InputSystemUI
{
	// Token: 0x02000008 RID: 8
	public class BindableToggleFactory
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022FF File Offset: 0x000004FF
		public BindableToggleFactory(InputService inputService)
		{
			this._inputService = inputService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002310 File Offset: 0x00000510
		public BindableToggle Create(Toggle toggle, string bindingKey, Action<bool> toggleAction, Func<bool> valueGetter)
		{
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> evt)
			{
				toggleAction(evt.newValue);
			});
			return new BindableToggle(this._inputService, toggle, bindingKey, toggleAction, valueGetter);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002352 File Offset: 0x00000552
		public BindableToggle CreateAndBind(Toggle toggle, string bindingKey, Action<bool> toggleAction, Func<bool> valueGetter)
		{
			BindableToggle bindableToggle = this.Create(toggle, bindingKey, toggleAction, valueGetter);
			bindableToggle.Bind();
			bindableToggle.Update();
			return bindableToggle;
		}

		// Token: 0x04000014 RID: 20
		public readonly InputService _inputService;
	}
}
