using System;
using Timberborn.SingletonSystem;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000015 RID: 21
	public class InputUpdater : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000031E3 File Offset: 0x000013E3
		public InputUpdater(InputModifiersService inputModifiersService, KeyBindingRegistry keyBindingRegistry)
		{
			this._inputModifiersService = inputModifiersService;
			this._keyBindingRegistry = keyBindingRegistry;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031FC File Offset: 0x000013FC
		public void Update()
		{
			InputModifiers inputModifiers = this._inputModifiersService.PressedModifiers();
			foreach (KeyBinding keyBinding in this._keyBindingRegistry.KeyBindings)
			{
				keyBinding.UpdateKeyState(inputModifiers);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003264 File Offset: 0x00001464
		public void Load()
		{
			InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003281 File Offset: 0x00001481
		public void Unload()
		{
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputSystemEvent);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032A0 File Offset: 0x000014A0
		public void OnInputSystemEvent(InputEventPtr inputEvent, InputDevice device)
		{
			if (inputEvent.IsAnyStateEvent())
			{
				InputModifiers inputModifiers = this._inputModifiersService.PressedModifiers();
				foreach (InputControl changedControl in InputControlExtensions.EnumerateChangedControls(inputEvent, null, 0f))
				{
					this.UpdateEventStates(inputEvent, changedControl, inputModifiers);
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003314 File Offset: 0x00001514
		public void UpdateEventStates(InputEventPtr inputEvent, InputControl changedControl, InputModifiers inputModifiers)
		{
			foreach (KeyBinding keyBinding in this._keyBindingRegistry.KeyBindings)
			{
				keyBinding.UpdateEventState(inputEvent, changedControl, inputModifiers);
			}
		}

		// Token: 0x04000039 RID: 57
		public readonly InputModifiersService _inputModifiersService;

		// Token: 0x0400003A RID: 58
		public readonly KeyBindingRegistry _keyBindingRegistry;
	}
}
