using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200000E RID: 14
	public class InputBindingListener : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002680 File Offset: 0x00000880
		public InputBindingListener(InputBindingNameService inputBindingNameService, InputModifiersService inputModifiersService, KeyBindingRegistry keyBindingRegistry)
		{
			this._inputBindingNameService = inputBindingNameService;
			this._inputModifiersService = inputModifiersService;
			this._keyBindingRegistry = keyBindingRegistry;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026A8 File Offset: 0x000008A8
		public void Load()
		{
			this._cancelKeyBinding = this._keyBindingRegistry.Get(InputBindingListener.CancelKey);
			this._confirmKeyBinding = this._keyBindingRegistry.Get(InputBindingListener.ConfirmKey);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026D6 File Offset: 0x000008D6
		public void WaitForInput(Action<CustomInputBinding> callback)
		{
			Asserts.FieldIsNull<InputBindingListener>(this, this._callback, "_callback");
			this._callback = callback;
			InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000270B File Offset: 0x0000090B
		public void FinishListening()
		{
			Asserts.FieldIsNotNull<InputBindingListener>(this, this._callback, "_callback");
			this._callback = null;
			this._pressedModifiers.Clear();
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000274B File Offset: 0x0000094B
		public void Unload()
		{
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002768 File Offset: 0x00000968
		public void OnInputSystemEvent(InputEventPtr inputEvent, InputDevice device)
		{
			if (inputEvent.IsAnyStateEvent())
			{
				foreach (InputControl inputControl in InputControlExtensions.EnumerateChangedControls(inputEvent, null, 0f))
				{
					if (this.ValidateInput(inputEvent, inputControl))
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027D4 File Offset: 0x000009D4
		public bool ValidateInput(InputEventPtr inputEvent, InputControl inputControl)
		{
			bool flag = inputControl.WasPressedInEvent(inputEvent);
			if (this._inputModifiersService.IsModifier(inputControl))
			{
				return this.ValidateModifierKey(inputControl as KeyControl, flag);
			}
			if (flag && this.IsValidInput(inputControl))
			{
				this.NotifyAndFinishListening(InputBindingListener.GetInputToNotify(inputEvent, inputControl), this._inputModifiersService.PressedModifiers());
				return true;
			}
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000282C File Offset: 0x00000A2C
		public bool ValidateModifierKey(KeyControl keyControl, bool wasPressed)
		{
			if (wasPressed)
			{
				this._pressedModifiers.Add(keyControl.keyCode);
				return false;
			}
			this._pressedModifiers.Remove(keyControl.keyCode);
			if (this._pressedModifiers.Count == 0)
			{
				this.NotifyAndFinishListening(keyControl, InputModifiers.None);
				return true;
			}
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000287A File Offset: 0x00000A7A
		public bool IsValidInput(InputControl inputControl)
		{
			return (InputBindingListener.IsButton(inputControl) && this.IsNotCancelConfirmButton(inputControl) && InputBindingListener.IsNotMainMouseButton(inputControl)) || InputBindingListener.IsMouseScroll(inputControl);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000289D File Offset: 0x00000A9D
		public static InputControl GetInputToNotify(InputEventPtr inputEvent, InputControl inputControl)
		{
			if (!InputBindingListener.IsMouseScroll(inputControl))
			{
				return inputControl;
			}
			return InputBindingListener.ConvertMouseScroll(inputEvent, inputControl);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void NotifyAndFinishListening(InputControl inputControl, InputModifiers inputModifiers)
		{
			string inputControlName = this._inputBindingNameService.GetInputControlName(inputControl);
			this._callback(new CustomInputBinding(inputControl.path, inputModifiers, inputControlName));
			this.FinishListening();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028E8 File Offset: 0x00000AE8
		public static bool IsButton(InputControl inputControl)
		{
			return inputControl is ButtonControl && !inputControl.synthetic;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002900 File Offset: 0x00000B00
		public bool IsNotCancelConfirmButton(InputControl inputControl)
		{
			return this._cancelKeyBinding.PrimaryInputBinding.InputControl != inputControl && this._cancelKeyBinding.SecondaryInputBinding.InputControl != inputControl && this._confirmKeyBinding.PrimaryInputBinding.InputControl != inputControl && this._confirmKeyBinding.SecondaryInputBinding.InputControl != inputControl;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002960 File Offset: 0x00000B60
		public static bool IsNotMainMouseButton(InputControl inputControl)
		{
			Mouse mouse = inputControl.device as Mouse;
			return mouse == null || (mouse.leftButton != inputControl && mouse.rightButton != inputControl);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002998 File Offset: 0x00000B98
		public static bool IsMouseScroll(InputControl inputControl)
		{
			Mouse mouse = inputControl.device as Mouse;
			return mouse != null && mouse.scroll.children.Contains(inputControl);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029CC File Offset: 0x00000BCC
		public static InputControl ConvertMouseScroll(InputEventPtr inputEvent, InputControl inputControl)
		{
			Mouse mouse = inputControl.device as Mouse;
			if (mouse != null)
			{
				if (inputControl == mouse.scroll.y)
				{
					float num = InputControlExtensions.ReadValueFromEvent<float>(mouse.scroll.y, inputEvent);
					if (num > 0f)
					{
						return mouse.scroll.up;
					}
					if (num < 0f)
					{
						return mouse.scroll.down;
					}
				}
				if (inputControl == mouse.scroll.x)
				{
					float num2 = InputControlExtensions.ReadValueFromEvent<float>(mouse.scroll.x, inputEvent);
					if (num2 > 0f)
					{
						return mouse.scroll.right;
					}
					if (num2 < 0f)
					{
						return mouse.scroll.left;
					}
				}
			}
			throw new InvalidOperationException("Failed to convert mouse scroll to input control");
		}

		// Token: 0x0400001D RID: 29
		public static readonly string CancelKey = "Cancel";

		// Token: 0x0400001E RID: 30
		public static readonly string ConfirmKey = "Confirm";

		// Token: 0x0400001F RID: 31
		public readonly InputBindingNameService _inputBindingNameService;

		// Token: 0x04000020 RID: 32
		public readonly InputModifiersService _inputModifiersService;

		// Token: 0x04000021 RID: 33
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x04000022 RID: 34
		public readonly HashSet<Key> _pressedModifiers = new HashSet<Key>();

		// Token: 0x04000023 RID: 35
		public Action<CustomInputBinding> _callback;

		// Token: 0x04000024 RID: 36
		public KeyBinding _cancelKeyBinding;

		// Token: 0x04000025 RID: 37
		public KeyBinding _confirmKeyBinding;
	}
}
