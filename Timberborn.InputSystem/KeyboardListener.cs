using System;
using Timberborn.KeyBindingSystem;
using Timberborn.SingletonSystem;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace Timberborn.InputSystem
{
	// Token: 0x02000012 RID: 18
	public class KeyboardListener : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000082 RID: 130 RVA: 0x00002EDC File Offset: 0x000010DC
		// (remove) Token: 0x06000083 RID: 131 RVA: 0x00002F14 File Offset: 0x00001114
		public event EventHandler<KeyPressedEvent> KeyPressed;

		// Token: 0x06000084 RID: 132 RVA: 0x00002F49 File Offset: 0x00001149
		public void Load()
		{
			InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F66 File Offset: 0x00001166
		public void Unload()
		{
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002F83 File Offset: 0x00001183
		public void OnInputSystemEvent(InputEventPtr inputEvent, InputDevice inputDevice)
		{
			if (inputDevice == Keyboard.current && inputEvent.IsAnyStateEvent())
			{
				this.CollectKeys(inputEvent);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002F9C File Offset: 0x0000119C
		public void CollectKeys(InputEventPtr inputEvent)
		{
			ReadOnlyArray<KeyControl> allKeys = Keyboard.current.allKeys;
			for (int i = 0; i < allKeys.Count; i++)
			{
				KeyControl keyControl = allKeys[i];
				if (keyControl != null)
				{
					int isPressed = keyControl.isPressed ? 1 : 0;
					bool flag = keyControl.IsValueConsideredPressed(InputControlExtensions.ReadValueFromEvent<float>(keyControl, inputEvent));
					if (isPressed == 0 && flag)
					{
						EventHandler<KeyPressedEvent> keyPressed = this.KeyPressed;
						if (keyPressed != null)
						{
							keyPressed(this, new KeyPressedEvent(keyControl.displayName.ToUpper()));
						}
					}
				}
			}
		}
	}
}
