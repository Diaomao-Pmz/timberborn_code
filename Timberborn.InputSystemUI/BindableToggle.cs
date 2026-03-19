using System;
using Timberborn.Common;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.InputSystemUI
{
	// Token: 0x02000007 RID: 7
	public class BindableToggle : IInputProcessor
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021E9 File Offset: 0x000003E9
		public BindableToggle(InputService inputService, Toggle toggle, string bindingKey, Action<bool> toggleAction, Func<bool> valueGetter)
		{
			this._inputService = inputService;
			this._toggle = toggle;
			this._bindingKey = bindingKey;
			this._toggleAction = toggleAction;
			this._valueGetter = valueGetter;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002216 File Offset: 0x00000416
		public void Bind()
		{
			Asserts.IsFalse<BindableToggle>(this, this._isBound, "_isBound");
			this._isBound = true;
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		public void Unbind()
		{
			this._isBound = false;
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002251 File Offset: 0x00000451
		public void Enable()
		{
			this._toggle.SetEnabled(true);
			this.Update();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002265 File Offset: 0x00000465
		public void Disable()
		{
			this._toggle.SetEnabled(false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002274 File Offset: 0x00000474
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(this._bindingKey) && this._toggle.enabledInHierarchy)
			{
				bool flag = !this._toggle.value;
				this._toggleAction(flag);
				this._toggle.SetValueWithoutNotify(flag);
				return true;
			}
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022CC File Offset: 0x000004CC
		public void Update()
		{
			bool flag = this._valueGetter();
			if (flag != this._toggle.value)
			{
				this._toggle.SetValueWithoutNotify(flag);
			}
		}

		// Token: 0x0400000E RID: 14
		public readonly InputService _inputService;

		// Token: 0x0400000F RID: 15
		public readonly Toggle _toggle;

		// Token: 0x04000010 RID: 16
		public readonly string _bindingKey;

		// Token: 0x04000011 RID: 17
		public readonly Action<bool> _toggleAction;

		// Token: 0x04000012 RID: 18
		public readonly Func<bool> _valueGetter;

		// Token: 0x04000013 RID: 19
		public bool _isBound;
	}
}
