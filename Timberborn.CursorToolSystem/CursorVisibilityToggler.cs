using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x0200000F RID: 15
	public class CursorVisibilityToggler : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002A30 File Offset: 0x00000C30
		public CursorVisibilityToggler(InputService inputService, MouseController mouseController)
		{
			this._inputService = inputService;
			this._mouseController = mouseController;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A46 File Offset: 0x00000C46
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A54 File Offset: 0x00000C54
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(CursorVisibilityToggler.ToggleCursorVisibilityKey))
			{
				this.ToggleCursorVisibility();
				return true;
			}
			return false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A71 File Offset: 0x00000C71
		public void ToggleCursorVisibility()
		{
			if (this._mouseController.IsCursorVisible)
			{
				this._mouseController.ForceHideCursor();
				return;
			}
			this._mouseController.ForceShowCursor();
		}

		// Token: 0x04000033 RID: 51
		public static readonly string ToggleCursorVisibilityKey = "ToggleCursorVisibility";

		// Token: 0x04000034 RID: 52
		public readonly InputService _inputService;

		// Token: 0x04000035 RID: 53
		public readonly MouseController _mouseController;
	}
}
