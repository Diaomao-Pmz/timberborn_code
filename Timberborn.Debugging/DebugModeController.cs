using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Debugging
{
	// Token: 0x02000005 RID: 5
	public class DebugModeController : IPriorityInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002127 File Offset: 0x00000327
		public DebugModeController(DebugModeManager debugModeManager, InputService inputService)
		{
			this._debugModeManager = debugModeManager;
			this._inputService = inputService;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000213D File Offset: 0x0000033D
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000214B File Offset: 0x0000034B
		public void ProcessInput()
		{
			if (this._inputService.IsKeyDown(DebugModeController.ToggleDebugModeKey))
			{
				this.Toggle();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002165 File Offset: 0x00000365
		public void Toggle()
		{
			if (this._debugModeManager.Enabled)
			{
				this._debugModeManager.Disable();
				return;
			}
			this._debugModeManager.Enable();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ToggleDebugModeKey = "ToggleDebugMode";

		// Token: 0x04000007 RID: 7
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x04000008 RID: 8
		public readonly InputService _inputService;
	}
}
