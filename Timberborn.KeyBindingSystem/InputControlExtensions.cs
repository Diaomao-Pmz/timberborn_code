using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000011 RID: 17
	public static class InputControlExtensions
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000030FC File Offset: 0x000012FC
		public static bool WasPressedInEvent(this InputControl inputControl, InputEventPtr inputEvent)
		{
			InputControl<float> inputControl2 = inputControl as InputControl<float>;
			if (inputControl2 == null)
			{
				return false;
			}
			float num = InputControlExtensions.ReadValueFromEvent<float>(inputControl2, inputEvent);
			ButtonControl buttonControl = inputControl as ButtonControl;
			if (buttonControl != null)
			{
				return buttonControl.IsValueConsideredPressed(num);
			}
			return num != 0f;
		}
	}
}
