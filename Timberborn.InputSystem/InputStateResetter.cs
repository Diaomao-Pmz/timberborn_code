using System;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Timberborn.InputSystem
{
	// Token: 0x0200000F RID: 15
	public class InputStateResetter : ILoadableSingleton, IUnloadableSingleton, IInputStateResetter
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public void Load()
		{
			Application.focusChanged += this.OnFocusChanged;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002DEB File Offset: 0x00000FEB
		public void Unload()
		{
			Application.focusChanged -= this.OnFocusChanged;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E00 File Offset: 0x00001000
		public void ResetInputState()
		{
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				InputSystem.ResetDevice(inputDevice, true);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E54 File Offset: 0x00001054
		public void OnFocusChanged(bool focus)
		{
			if (focus)
			{
				this.ResetInputState();
			}
		}
	}
}
