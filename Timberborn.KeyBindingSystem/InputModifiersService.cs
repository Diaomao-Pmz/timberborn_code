using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000014 RID: 20
	public class InputModifiersService
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003150 File Offset: 0x00001350
		public bool IsModifier(InputControl inputControl)
		{
			KeyControl keyControl = inputControl as KeyControl;
			if (keyControl != null)
			{
				Key keyCode = keyControl.keyCode;
				if (keyCode - 51 <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000317C File Offset: 0x0000137C
		public InputModifiers PressedModifiers()
		{
			InputModifiers inputModifiers = InputModifiers.None;
			Keyboard current = Keyboard.current;
			if (current.ctrlKey.isPressed)
			{
				inputModifiers |= InputModifiers.Ctrl;
			}
			if (current.altKey.isPressed)
			{
				inputModifiers |= InputModifiers.Alt;
			}
			if (current.shiftKey.isPressed)
			{
				inputModifiers |= InputModifiers.Shift;
			}
			if (current.leftCommandKey.isPressed || current.rightCommandKey.isPressed)
			{
				inputModifiers |= InputModifiers.Cmd;
			}
			return inputModifiers;
		}
	}
}
