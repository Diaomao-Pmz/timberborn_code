using System;
using Timberborn.PlatformUtilities;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Timberborn.InputSystem
{
	// Token: 0x02000017 RID: 23
	public class MouseController : ILoadableSingleton, IPostLoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000031B0 File Offset: 0x000013B0
		public MouseController(InputSettings inputSettings)
		{
			this._inputSettings = inputSettings;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000031BF File Offset: 0x000013BF
		public Vector3 Position
		{
			get
			{
				return Mouse.current.position.ReadValue();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000031D8 File Offset: 0x000013D8
		public Vector2 XYAxes
		{
			get
			{
				if (this._ignoredMovementFrames <= 0)
				{
					return MouseController.XYAxesInternal;
				}
				return default(Vector2);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000031FD File Offset: 0x000013FD
		public bool IsCursorVisible
		{
			get
			{
				return Cursor.visible;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003204 File Offset: 0x00001404
		public void Load()
		{
			this._inputSettings.LockCursorInWindowChanged += this.OnLockCursorInWindowChanged;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000321D File Offset: 0x0000141D
		public void PostLoad()
		{
			this.UpdateCursorLockState();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003228 File Offset: 0x00001428
		public void LateUpdateSingleton()
		{
			if (this._ignoredMovementFrames > 0 && MouseController.XYAxesInternal != default(Vector2))
			{
				this._ignoredMovementFrames--;
			}
			if (this._lockedPosition != null)
			{
				Mouse.current.WarpCursorPosition(this._lockedPosition.Value);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003283 File Offset: 0x00001483
		public void HideCursor()
		{
			Cursor.visible = false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000328B File Offset: 0x0000148B
		public void ShowCursor()
		{
			if (!this._cursorIsForceHidden)
			{
				Cursor.visible = true;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000329B File Offset: 0x0000149B
		public void ForceHideCursor()
		{
			this.HideCursor();
			this._cursorIsForceHidden = true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000032AA File Offset: 0x000014AA
		public void ForceShowCursor()
		{
			this._cursorIsForceHidden = false;
			this.ShowCursor();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000032B9 File Offset: 0x000014B9
		public void LockCursor()
		{
			this._lockedPosition = new Vector2?(this.Position);
			Cursor.lockState = 2;
			this._ignoredMovementFrames = (ApplicationPlatform.IsMacOS() ? MouseController.MovementFramesToIgnoreAfterLockOnMacOS : 0);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000032EC File Offset: 0x000014EC
		public void UnlockCursor()
		{
			this._lockedPosition = null;
			this.UpdateCursorLockState();
			this._ignoredMovementFrames = 0;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003307 File Offset: 0x00001507
		public static Vector2 XYAxesInternal
		{
			get
			{
				return Mouse.current.delta.ReadValue() * (ApplicationPlatform.IsMacOS() ? MouseController.MacOsMouseDeltaMultiplier : 1f);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003330 File Offset: 0x00001530
		public void OnLockCursorInWindowChanged(object sender, SettingChangedEventArgs<bool> e)
		{
			if (Cursor.lockState != 1)
			{
				this.UpdateCursorLockState();
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003340 File Offset: 0x00001540
		public void UpdateCursorLockState()
		{
			Cursor.lockState = (this._inputSettings.LockCursorInWindow ? 2 : 0);
		}

		// Token: 0x0400003F RID: 63
		public static readonly int MovementFramesToIgnoreAfterLockOnMacOS = 3;

		// Token: 0x04000040 RID: 64
		public static readonly float MacOsMouseDeltaMultiplier = 7.5f;

		// Token: 0x04000041 RID: 65
		public readonly InputSettings _inputSettings;

		// Token: 0x04000042 RID: 66
		public int _ignoredMovementFrames;

		// Token: 0x04000043 RID: 67
		public bool _cursorIsForceHidden;

		// Token: 0x04000044 RID: 68
		public Vector2? _lockedPosition;
	}
}
