using System;
using Timberborn.Common;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.InputSystemUI
{
	// Token: 0x02000004 RID: 4
	public class BindableButton : IInputProcessor
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BindableButton(InputService inputService, VisualElement button, string bindingKey, Action action, bool blockInput)
		{
			this._inputService = inputService;
			this._button = button;
			this._bindingKey = bindingKey;
			this._action = action;
			this._blockInput = blockInput;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EB File Offset: 0x000002EB
		public void Bind()
		{
			Asserts.IsFalse<BindableButton>(this, this._isBound, "_isBound");
			this._isBound = true;
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002111 File Offset: 0x00000311
		public void Unbind()
		{
			this._isBound = false;
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002126 File Offset: 0x00000326
		public void Enable()
		{
			this._button.SetEnabled(true);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002134 File Offset: 0x00000334
		public void Disable()
		{
			this._button.SetEnabled(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002142 File Offset: 0x00000342
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(this._bindingKey) && this._button.enabledInHierarchy)
			{
				this._action();
				return this._blockInput;
			}
			return false;
		}

		// Token: 0x04000006 RID: 6
		public readonly InputService _inputService;

		// Token: 0x04000007 RID: 7
		public readonly VisualElement _button;

		// Token: 0x04000008 RID: 8
		public readonly string _bindingKey;

		// Token: 0x04000009 RID: 9
		public readonly Action _action;

		// Token: 0x0400000A RID: 10
		public readonly bool _blockInput;

		// Token: 0x0400000B RID: 11
		public bool _isBound;
	}
}
