using System;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200000F RID: 15
	public class InputBindingNameService
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002A9B File Offset: 0x00000C9B
		public InputBindingNameService(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AAA File Offset: 0x00000CAA
		public string GetName(InputBinding inputBinding)
		{
			if (!string.IsNullOrEmpty(inputBinding.DefaultName))
			{
				return inputBinding.DefaultName;
			}
			if (inputBinding.InputControl != null)
			{
				return this.GetInputControlName(inputBinding.InputControl);
			}
			return inputBinding.InputBindingSpec.Path;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public string GetInputControlName(InputControl inputControl)
		{
			string buttonName = this.GetButtonName(inputControl);
			string devicePrefix = InputBindingNameService.GetDevicePrefix(inputControl);
			if (!string.IsNullOrEmpty(devicePrefix))
			{
				return devicePrefix + ": " + buttonName;
			}
			return buttonName;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B14 File Offset: 0x00000D14
		public string GetInputModifierName(InputModifiers inputModifier)
		{
			bool flag = ApplicationPlatform.IsMacOS();
			switch (inputModifier)
			{
			case InputModifiers.None:
				throw new NotSupportedException();
			case InputModifiers.Ctrl:
				return "Ctrl";
			case InputModifiers.Alt:
				return flag ? "Option" : "Alt";
			case InputModifiers.Shift:
				return "Shift";
			case InputModifiers.Cmd:
				return "Cmd";
			}
			throw new ArgumentOutOfRangeException("inputModifier", inputModifier, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B98 File Offset: 0x00000D98
		public string GetButtonName(InputControl inputControl)
		{
			string key;
			if (InputBindingNameService.TryGetButtonLocKey(inputControl, out key))
			{
				return this._loc.T(key);
			}
			string result;
			if ((result = this.GetKeyCodeName(inputControl)) == null)
			{
				result = (inputControl.shortDisplayName ?? inputControl.displayName);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BD7 File Offset: 0x00000DD7
		public static bool TryGetButtonLocKey(InputControl inputControl, out string locKey)
		{
			return InputBindingNameService.TryGetKeyLocKey(inputControl, out locKey) || InputBindingNameService.TryGetMouseLocKey(inputControl, out locKey);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static bool TryGetKeyLocKey(InputControl inputControl, out string locKey)
		{
			KeyControl keyControl = inputControl as KeyControl;
			if (keyControl != null)
			{
				locKey = InputBindingNameService.GetKeyCodeLocKey(keyControl.keyCode);
				return locKey != null;
			}
			locKey = null;
			return false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C1C File Offset: 0x00000E1C
		public static bool TryGetMouseLocKey(InputControl inputControl, out string locKey)
		{
			Mouse mouse = inputControl.device as Mouse;
			if (mouse != null)
			{
				if (inputControl == mouse.scroll.down)
				{
					locKey = InputBindingNameService.MouseScrollDownLocKey;
					return true;
				}
				if (inputControl == mouse.scroll.left)
				{
					locKey = InputBindingNameService.MouseScrollLeftLocKey;
					return true;
				}
				if (inputControl == mouse.scroll.right)
				{
					locKey = InputBindingNameService.MouseScrollRightLocKey;
					return true;
				}
				if (inputControl == mouse.scroll.up)
				{
					locKey = InputBindingNameService.MouseScrollUpLocKey;
					return true;
				}
			}
			locKey = null;
			return false;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C98 File Offset: 0x00000E98
		public static string GetKeyCodeLocKey(Key keyCode)
		{
			string result;
			if (keyCode != 1)
			{
				switch (keyCode)
				{
				case 61:
					result = InputBindingNameService.KeyLeftArrowLocKey;
					break;
				case 62:
					result = InputBindingNameService.KeyRightArrowLocKey;
					break;
				case 63:
					result = InputBindingNameService.KeyUpArrowLocKey;
					break;
				case 64:
					result = InputBindingNameService.KeyDownArrowLocKey;
					break;
				default:
					result = null;
					break;
				}
			}
			else
			{
				result = InputBindingNameService.KeySpaceLocKey;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public string GetKeyCodeName(InputControl inputControl)
		{
			KeyControl keyControl = inputControl as KeyControl;
			if (keyControl != null)
			{
				bool flag = ApplicationPlatform.IsMacOS();
				string result;
				switch (keyControl.keyCode)
				{
				case 51:
					result = this.GetLeftInputModifierName(InputModifiers.Shift);
					break;
				case 52:
					result = this.GetRightInputModifierName(InputModifiers.Shift);
					break;
				case 53:
					result = this.GetLeftInputModifierName(InputModifiers.Alt);
					break;
				case 54:
					result = this.GetRightInputModifierName(InputModifiers.Alt);
					break;
				case 55:
					result = this.GetLeftInputModifierName(InputModifiers.Ctrl);
					break;
				case 56:
					result = this.GetRightInputModifierName(InputModifiers.Ctrl);
					break;
				case 57:
					result = (flag ? this.GetLeftInputModifierName(InputModifiers.Cmd) : this._loc.T("Key.LeftWindows"));
					break;
				case 58:
					result = (flag ? this.GetRightInputModifierName(InputModifiers.Cmd) : this._loc.T("Key.RightWindows"));
					break;
				default:
					result = null;
					break;
				}
				return result;
			}
			return null;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public string GetLeftInputModifierName(InputModifiers inputModifier)
		{
			return "L-" + this.GetInputModifierName(inputModifier);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public string GetRightInputModifierName(InputModifiers inputModifier)
		{
			return "R-" + this.GetInputModifierName(inputModifier);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public static string GetDevicePrefix(InputControl inputControl)
		{
			InputDevice device = inputControl.device;
			if (!(device is Keyboard) && !(device is Mouse))
			{
				return inputControl.device.displayName;
			}
			return string.Empty;
		}

		// Token: 0x04000026 RID: 38
		public static readonly string MouseScrollDownLocKey = "Mouse.ScrollDown";

		// Token: 0x04000027 RID: 39
		public static readonly string MouseScrollLeftLocKey = "Mouse.ScrollLeft";

		// Token: 0x04000028 RID: 40
		public static readonly string MouseScrollRightLocKey = "Mouse.ScrollRight";

		// Token: 0x04000029 RID: 41
		public static readonly string MouseScrollUpLocKey = "Mouse.ScrollUp";

		// Token: 0x0400002A RID: 42
		public static readonly string KeySpaceLocKey = "Key.Space";

		// Token: 0x0400002B RID: 43
		public static readonly string KeyLeftArrowLocKey = "Key.LeftArrow";

		// Token: 0x0400002C RID: 44
		public static readonly string KeyRightArrowLocKey = "Key.RightArrow";

		// Token: 0x0400002D RID: 45
		public static readonly string KeyUpArrowLocKey = "Key.UpArrow";

		// Token: 0x0400002E RID: 46
		public static readonly string KeyDownArrowLocKey = "Key.DownArrow";

		// Token: 0x0400002F RID: 47
		public readonly ILoc _loc;
	}
}
