using System;
using Timberborn.SingletonSystem;
using UnityEngine.InputSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000018 RID: 24
	public class KeyBindingDeviceUpdater : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000036FA File Offset: 0x000018FA
		public KeyBindingDeviceUpdater(KeyBindingRegistry keyBindingRegistry)
		{
			this._keyBindingRegistry = keyBindingRegistry;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003709 File Offset: 0x00001909
		public void Load()
		{
			InputSystem.onDeviceChange += this.OnDeviceChange;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000371C File Offset: 0x0000191C
		public void Unload()
		{
			InputSystem.onDeviceChange -= this.OnDeviceChange;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000372F File Offset: 0x0000192F
		public void OnDeviceChange(InputDevice inputDevice, InputDeviceChange inputDeviceChange)
		{
			if (inputDeviceChange == null)
			{
				this.NotifyDeviceAdded();
				return;
			}
			if (inputDeviceChange == 1)
			{
				this.NotifyDeviceRemoved(inputDevice);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003748 File Offset: 0x00001948
		public void NotifyDeviceAdded()
		{
			foreach (KeyBinding keyBinding in this._keyBindingRegistry.KeyBindings)
			{
				keyBinding.PrimaryInputBinding.DeviceAdded();
				keyBinding.SecondaryInputBinding.DeviceAdded();
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000037B0 File Offset: 0x000019B0
		public void NotifyDeviceRemoved(InputDevice inputDevice)
		{
			foreach (KeyBinding keyBinding in this._keyBindingRegistry.KeyBindings)
			{
				keyBinding.PrimaryInputBinding.DeviceRemoved(inputDevice);
				keyBinding.SecondaryInputBinding.DeviceRemoved(inputDevice);
			}
		}

		// Token: 0x0400004A RID: 74
		public readonly KeyBindingRegistry _keyBindingRegistry;
	}
}
