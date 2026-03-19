using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Debugging
{
	// Token: 0x02000009 RID: 9
	public class DevModeController : IPriorityInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002286 File Offset: 0x00000486
		public DevModeController(DevModeManager devModeManager, InputService inputService)
		{
			this._devModeManager = devModeManager;
			this._inputService = inputService;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000229C File Offset: 0x0000049C
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022AA File Offset: 0x000004AA
		public void ProcessInput()
		{
			if (this._inputService.IsKeyDown(DevModeController.ToggleDevModeKey))
			{
				this.Toggle();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022C4 File Offset: 0x000004C4
		public void Toggle()
		{
			if (this._devModeManager.Enabled)
			{
				this._devModeManager.Disable();
				return;
			}
			this._devModeManager.Enable();
		}

		// Token: 0x0400000F RID: 15
		public static readonly string ToggleDevModeKey = "ToggleDevMode";

		// Token: 0x04000010 RID: 16
		public readonly DevModeManager _devModeManager;

		// Token: 0x04000011 RID: 17
		public readonly InputService _inputService;
	}
}
