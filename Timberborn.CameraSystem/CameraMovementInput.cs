using System;
using Timberborn.InputSystem;
using Timberborn.PlatformUtilities;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200000B RID: 11
	public class CameraMovementInput
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002447 File Offset: 0x00000647
		public CameraMovementInput(InputService inputService)
		{
			this._inputService = inputService;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002456 File Offset: 0x00000656
		public bool MoveCameraFast
		{
			get
			{
				return this._inputService.IsKeyHeld(CameraMovementInput.MoveCameraFastKey);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002468 File Offset: 0x00000668
		public Vector2 CameraMovementAxes
		{
			get
			{
				return new Vector2((float)this.GetHorizontalAxis(), (float)this.GetVerticalAxis());
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002480 File Offset: 0x00000680
		public ScreenEdges GetMouseScreenEdges()
		{
			ScreenEdges screenEdges = ScreenEdges.None;
			if (Application.isFocused)
			{
				Vector3 mousePosition = this._inputService.MousePosition;
				float x = mousePosition.x;
				float y = mousePosition.y;
				float num = ApplicationPlatform.IsMacOS() ? 64f : 0f;
				if (y >= -num && y <= 1f)
				{
					screenEdges |= ScreenEdges.Down;
				}
				if (x >= -num && x <= 1f)
				{
					screenEdges |= ScreenEdges.Left;
				}
				int height = Screen.height;
				if (y >= (float)height - 1f && y <= (float)height + num)
				{
					screenEdges |= ScreenEdges.Up;
				}
				int width = Screen.width;
				if (x >= (float)width - 1f && x <= (float)width + num)
				{
					screenEdges |= ScreenEdges.Right;
				}
			}
			return screenEdges;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002528 File Offset: 0x00000728
		public Vector2 GetCameraRotationAxes()
		{
			if (this._inputService.IsKeyHeld(CameraMovementInput.RotateCameraRightKey))
			{
				return Vector2.right;
			}
			if (this._inputService.IsKeyHeld(CameraMovementInput.RotateCameraLeftKey))
			{
				return Vector2.left;
			}
			if (this._inputService.IsKeyHeld(CameraMovementInput.RotateCameraUpKey))
			{
				return Vector2.up;
			}
			if (this._inputService.IsKeyHeld(CameraMovementInput.RotateCameraDownKey))
			{
				return Vector2.down;
			}
			return Vector2.zero;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000259A File Offset: 0x0000079A
		public Vector2 GetCameraJumpRotationAxes()
		{
			if (this._inputService.IsKeyDown(CameraMovementInput.RotateCameraRight90Key))
			{
				return Vector2.right;
			}
			if (this._inputService.IsKeyDown(CameraMovementInput.RotateCameraLeft90Key))
			{
				return Vector2.left;
			}
			return Vector2.zero;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025D1 File Offset: 0x000007D1
		public bool AxisKeyUp
		{
			get
			{
				return this._inputService.IsKeyHeld(CameraMovementInput.MoveCameraUpKey);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025E3 File Offset: 0x000007E3
		public bool AxisKeyDown
		{
			get
			{
				return this._inputService.IsKeyHeld(CameraMovementInput.MoveCameraDownKey);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025F5 File Offset: 0x000007F5
		public bool AxisKeyLeft
		{
			get
			{
				return this._inputService.IsKeyHeld(CameraMovementInput.MoveCameraLeftKey);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002607 File Offset: 0x00000807
		public bool AxisKeyRight
		{
			get
			{
				return this._inputService.IsKeyHeld(CameraMovementInput.MoveCameraRightKey);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002619 File Offset: 0x00000819
		public int GetHorizontalAxis()
		{
			if (this.AxisKeyRight && this.AxisKeyLeft)
			{
				return 0;
			}
			if (this.AxisKeyRight)
			{
				return 1;
			}
			if (this.AxisKeyLeft)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002642 File Offset: 0x00000842
		public int GetVerticalAxis()
		{
			if (this.AxisKeyUp && this.AxisKeyDown)
			{
				return 0;
			}
			if (this.AxisKeyUp)
			{
				return 1;
			}
			if (this.AxisKeyDown)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04000015 RID: 21
		public static readonly string RotateCameraRightKey = "RotateCameraRight";

		// Token: 0x04000016 RID: 22
		public static readonly string RotateCameraLeftKey = "RotateCameraLeft";

		// Token: 0x04000017 RID: 23
		public static readonly string RotateCameraUpKey = "RotateCameraUp";

		// Token: 0x04000018 RID: 24
		public static readonly string RotateCameraDownKey = "RotateCameraDown";

		// Token: 0x04000019 RID: 25
		public static readonly string RotateCameraRight90Key = "RotateCameraRight90";

		// Token: 0x0400001A RID: 26
		public static readonly string RotateCameraLeft90Key = "RotateCameraLeft90";

		// Token: 0x0400001B RID: 27
		public static readonly string MoveCameraFastKey = "MoveCameraFast";

		// Token: 0x0400001C RID: 28
		public static readonly string MoveCameraUpKey = "MoveCameraUp";

		// Token: 0x0400001D RID: 29
		public static readonly string MoveCameraDownKey = "MoveCameraDown";

		// Token: 0x0400001E RID: 30
		public static readonly string MoveCameraLeftKey = "MoveCameraLeft";

		// Token: 0x0400001F RID: 31
		public static readonly string MoveCameraRightKey = "MoveCameraRight";

		// Token: 0x04000020 RID: 32
		public readonly InputService _inputService;
	}
}
