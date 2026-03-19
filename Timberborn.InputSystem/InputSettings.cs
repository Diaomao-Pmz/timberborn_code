using System;
using System.Collections.Immutable;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.InputSystem
{
	// Token: 0x0200000E RID: 14
	public class InputSettings : ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public event EventHandler<SettingChangedEventArgs<bool>> LockCursorInWindowChanged;

		// Token: 0x0600005F RID: 95 RVA: 0x00002B09 File Offset: 0x00000D09
		public InputSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002B18 File Offset: 0x00000D18
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002B2B File Offset: 0x00000D2B
		public bool InvertZoom
		{
			get
			{
				return this._settings.GetBool(InputSettings.InvertZoomKey, false);
			}
			set
			{
				this._settings.SetBool(InputSettings.InvertZoomKey, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B3E File Offset: 0x00000D3E
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002B51 File Offset: 0x00000D51
		public bool SwapMouseCameraMovementWithRotation
		{
			get
			{
				return this._settings.GetBool(InputSettings.SwapMouseCameraMovementWithRotationKey, false);
			}
			set
			{
				this._settings.SetBool(InputSettings.SwapMouseCameraMovementWithRotationKey, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002B64 File Offset: 0x00000D64
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002B77 File Offset: 0x00000D77
		public bool DragCamera
		{
			get
			{
				return this._settings.GetBool(InputSettings.DragCameraKey, false);
			}
			set
			{
				this._settings.SetBool(InputSettings.DragCameraKey, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002B8A File Offset: 0x00000D8A
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002B9D File Offset: 0x00000D9D
		public bool LockCursorInWindow
		{
			get
			{
				return this._settings.GetBool(InputSettings.LockCursorInWindowKey, false);
			}
			set
			{
				this._settings.SetBool(InputSettings.LockCursorInWindowKey, value);
				EventHandler<SettingChangedEventArgs<bool>> lockCursorInWindowChanged = this.LockCursorInWindowChanged;
				if (lockCursorInWindowChanged == null)
				{
					return;
				}
				lockCursorInWindowChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002BC7 File Offset: 0x00000DC7
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002BDA File Offset: 0x00000DDA
		public bool EdgePanCamera
		{
			get
			{
				return this._settings.GetBool(InputSettings.EdgePanCameraKey, false);
			}
			set
			{
				this._settings.SetBool(InputSettings.EdgePanCameraKey, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002BED File Offset: 0x00000DED
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002C04 File Offset: 0x00000E04
		public float EdgePanCameraSpeed
		{
			get
			{
				return this._settings.GetFloat(InputSettings.EdgePanCameraSpeedKey, 0.4f);
			}
			set
			{
				this._settings.SetFloat(InputSettings.EdgePanCameraSpeedKey, value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002C17 File Offset: 0x00000E17
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002C2E File Offset: 0x00000E2E
		public float KeyboardCameraMovementSpeed
		{
			get
			{
				return this._settings.GetFloat(InputSettings.KeyboardCameraMovementSpeedKey, 0.4f);
			}
			set
			{
				this._settings.SetFloat(InputSettings.KeyboardCameraMovementSpeedKey, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002C41 File Offset: 0x00000E41
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002C58 File Offset: 0x00000E58
		public float KeyboardCameraRotationSpeed
		{
			get
			{
				return this._settings.GetFloat(InputSettings.KeyboardCameraRotationSpeedKey, 0.4f);
			}
			set
			{
				this._settings.SetFloat(InputSettings.KeyboardCameraRotationSpeedKey, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002C6B File Offset: 0x00000E6B
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002C82 File Offset: 0x00000E82
		public float KeyboardCameraZoomSpeed
		{
			get
			{
				return this._settings.GetFloat(InputSettings.KeyboardCameraZoomSpeedKey, 0.4f);
			}
			set
			{
				this._settings.SetFloat(InputSettings.KeyboardCameraZoomSpeedKey, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002C95 File Offset: 0x00000E95
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002CAC File Offset: 0x00000EAC
		public float MouseWheelCameraZoomSpeed
		{
			get
			{
				return this._settings.GetFloat(InputSettings.MouseWheelCameraZoomSpeedKey, 0.4f);
			}
			set
			{
				this._settings.SetFloat(InputSettings.MouseWheelCameraZoomSpeedKey, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002CBF File Offset: 0x00000EBF
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public float MouseCameraRotationSpeed
		{
			get
			{
				return this._settings.GetFloat(InputSettings.MouseCameraRotationSpeedKey, 0.4f);
			}
			set
			{
				this._settings.SetFloat(InputSettings.MouseCameraRotationSpeedKey, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002CE9 File Offset: 0x00000EE9
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002D00 File Offset: 0x00000F00
		public string OnScreenKeyboard
		{
			get
			{
				return this._settings.GetSafeString(InputSettings.OnScreenKeyboardKey, InputSettings.OnScreenKeyboardDefaultValue);
			}
			set
			{
				this._settings.SetString(InputSettings.OnScreenKeyboardKey, value);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D13 File Offset: 0x00000F13
		public void Load()
		{
			this._settings.ValidateString(InputSettings.OnScreenKeyboardKey, InputSettings.OnScreenKeyboardValues, InputSettings.OnScreenKeyboardDefaultValue);
		}

		// Token: 0x04000025 RID: 37
		public static readonly ImmutableArray<string> OnScreenKeyboardValues = ImmutableArray.Create<string>("Auto", "Enabled", "Disabled");

		// Token: 0x04000026 RID: 38
		public static readonly string OnScreenKeyboardDefaultValue = "Auto";

		// Token: 0x04000027 RID: 39
		public static readonly string InvertZoomKey = "InvertZoom";

		// Token: 0x04000028 RID: 40
		public static readonly string SwapMouseCameraMovementWithRotationKey = "SwapMouseCameraMovementWithRotation";

		// Token: 0x04000029 RID: 41
		public static readonly string DragCameraKey = "DragCamera";

		// Token: 0x0400002A RID: 42
		public static readonly string LockCursorInWindowKey = "LockCursorInWindow";

		// Token: 0x0400002B RID: 43
		public static readonly string EdgePanCameraKey = "EdgePanCamera";

		// Token: 0x0400002C RID: 44
		public static readonly string OnScreenKeyboardKey = "OnScreenKeyboard";

		// Token: 0x0400002D RID: 45
		public static readonly string MouseCameraRotationSpeedKey = "MouseCameraRotationSpeed";

		// Token: 0x0400002E RID: 46
		public static readonly string EdgePanCameraSpeedKey = "EdgePanCameraSpeed";

		// Token: 0x0400002F RID: 47
		public static readonly string KeyboardCameraMovementSpeedKey = "KeyboardCameraMovementSpeed";

		// Token: 0x04000030 RID: 48
		public static readonly string KeyboardCameraRotationSpeedKey = "KeyboardCameraRotationSpeed";

		// Token: 0x04000031 RID: 49
		public static readonly string KeyboardCameraZoomSpeedKey = "KeyboardCameraZoomSpeed";

		// Token: 0x04000032 RID: 50
		public static readonly string MouseWheelCameraZoomSpeedKey = "MouseWheelCameraZoomSpeed";

		// Token: 0x04000034 RID: 52
		public readonly ISettings _settings;
	}
}
