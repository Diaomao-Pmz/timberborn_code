using System;
using System.Collections.Generic;
using Timberborn.KeyBindingSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Timberborn.InputSystem
{
	// Token: 0x0200000D RID: 13
	public class InputService : IUpdatableSingleton
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002627 File Offset: 0x00000827
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000262F File Offset: 0x0000082F
		public bool MouseOverUI { get; private set; }

		// Token: 0x06000035 RID: 53 RVA: 0x00002638 File Offset: 0x00000838
		public InputService(InputSettings inputSettings, InputUpdater inputUpdater, MouseController mouse, KeyBindingRegistry keyBindingRegistry, InputBlocker inputBlocker)
		{
			this._inputSettings = inputSettings;
			this._inputUpdater = inputUpdater;
			this._mouse = mouse;
			this._keyBindingRegistry = keyBindingRegistry;
			this._inputBlocker = inputBlocker;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002691 File Offset: 0x00000891
		public string MouseRotateCameraKey
		{
			get
			{
				if (!this._inputSettings.SwapMouseCameraMovementWithRotation)
				{
					return InputService.MouseRightKey;
				}
				return InputService.MouseMiddleKey;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026AB File Offset: 0x000008AB
		public string MouseMoveCameraKey
		{
			get
			{
				if (!this._inputSettings.SwapMouseCameraMovementWithRotation)
				{
					return InputService.MouseMiddleKey;
				}
				return InputService.MouseRightKey;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000026C5 File Offset: 0x000008C5
		public bool UIConfirm
		{
			get
			{
				return this.IsKeyDown(InputService.ConfirmKey);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000026D2 File Offset: 0x000008D2
		public bool UICancel
		{
			get
			{
				return this.KeyboardCancel;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026DA File Offset: 0x000008DA
		public bool Cancel
		{
			get
			{
				return this.MouseCancel || this.KeyboardCancel;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000026EC File Offset: 0x000008EC
		public Vector3 MousePosition
		{
			get
			{
				return this._mouse.Position;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000026F9 File Offset: 0x000008F9
		public bool MainMouseButtonDown
		{
			get
			{
				return this.IsKeyDown(InputService.MouseLeftKey);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002706 File Offset: 0x00000906
		public bool MainMouseButtonHeld
		{
			get
			{
				return this.IsKeyHeld(InputService.MouseLeftKey);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002713 File Offset: 0x00000913
		public bool MainMouseButtonUp
		{
			get
			{
				return this.IsKeyUp(InputService.MouseLeftKey);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002720 File Offset: 0x00000920
		public bool MoveButtonHeld
		{
			get
			{
				return this.IsKeyHeld(this.MouseMoveCameraKey);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000272E File Offset: 0x0000092E
		public bool RotateButtonDown
		{
			get
			{
				return this.IsKeyDown(this.MouseRotateCameraKey);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000273C File Offset: 0x0000093C
		public bool RotateButtonHeld
		{
			get
			{
				return this.IsKeyHeld(this.MouseRotateCameraKey);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000274A File Offset: 0x0000094A
		public bool RotateButtonLongHeld
		{
			get
			{
				return this.IsKeyLongHeld(this.MouseRotateCameraKey);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002758 File Offset: 0x00000958
		public bool RotateButtonUp
		{
			get
			{
				return this.IsKeyUp(this.MouseRotateCameraKey);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002766 File Offset: 0x00000966
		public bool WasConfirmPressedLastFrame
		{
			get
			{
				return this.IsKeyHeld(InputService.ConfirmKey) || this.IsKeyUp(InputService.ConfirmKey);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002782 File Offset: 0x00000982
		public bool WasCancelPressedLastFrame
		{
			get
			{
				return this.IsKeyHeld(InputService.CancelKey) || this.IsKeyUp(InputService.CancelKey);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000279E File Offset: 0x0000099E
		public bool ScrollWheelActive
		{
			get
			{
				return this.MouseZoom != 0f;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000027B0 File Offset: 0x000009B0
		public Vector2 MouseXYAxes
		{
			get
			{
				return this._mouse.XYAxes;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000027C0 File Offset: 0x000009C0
		public float MouseZoom
		{
			get
			{
				float num = this._inputSettings.InvertZoom ? (-this._inputSettings.MouseWheelCameraZoomSpeed) : this._inputSettings.MouseWheelCameraZoomSpeed;
				float num2 = InputService.ProcessScrollWheelAxis(this._keyBindingRegistry.GetRawValue("MouseZoomIn"));
				if (num2 > 0f)
				{
					return num2 * num;
				}
				return InputService.ProcessScrollWheelAxis(this._keyBindingRegistry.GetRawValue("MouseZoomOut")) * -1f * num;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002834 File Offset: 0x00000A34
		public Vector2 MousePositionNdc
		{
			get
			{
				Vector3 position = this._mouse.Position;
				return new Vector2(position.x / (float)Screen.width, position.y / (float)Screen.height);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000286C File Offset: 0x00000A6C
		public bool IsKeyDown(string keyId)
		{
			return this._keyBindingRegistry.IsDown(keyId);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000287A File Offset: 0x00000A7A
		public bool IsKeyUp(string keyId)
		{
			return this._keyBindingRegistry.IsUp(keyId);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002888 File Offset: 0x00000A88
		public bool IsKeyHeld(string keyId)
		{
			return this._keyBindingRegistry.IsHeld(keyId);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002896 File Offset: 0x00000A96
		public void UpdateSingleton()
		{
			this.MouseOverUI = EventSystem.current.IsPointerOverGameObject();
			if (!this._inputBlocker.IsBlocked)
			{
				this.CallInputProcessors();
			}
			this._inputUpdater.Update();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028C6 File Offset: 0x00000AC6
		public void HideCursor()
		{
			this._mouse.HideCursor();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028D3 File Offset: 0x00000AD3
		public void ShowCursor()
		{
			this._mouse.ShowCursor();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028E0 File Offset: 0x00000AE0
		public void LockCursor()
		{
			this._mouse.LockCursor();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000028ED File Offset: 0x00000AED
		public void UnlockCursor()
		{
			this._mouse.UnlockCursor();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028FA File Offset: 0x00000AFA
		public void FlushUIInput()
		{
			this._keyBindingRegistry.Get(InputService.ConfirmKey).Flush();
			this._keyBindingRegistry.Get(InputService.CancelKey).Flush();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002926 File Offset: 0x00000B26
		public void AddInputProcessor(IInputProcessor inputProcessor)
		{
			this._inputProcessors.Add(inputProcessor);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002934 File Offset: 0x00000B34
		public void AddInputProcessor(IPriorityInputProcessor inputProcessor)
		{
			this._priorityInputProcessors.Add(inputProcessor);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002944 File Offset: 0x00000B44
		public void RemoveInputProcessor(IInputProcessor inputProcessor)
		{
			for (int i = this._inputProcessors.Count - 1; i >= 0; i--)
			{
				if (this._inputProcessors[i] == inputProcessor)
				{
					this._inputProcessors.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002985 File Offset: 0x00000B85
		public bool MouseCancel
		{
			get
			{
				return this.IsKeyUpAfterShortHeld(InputService.MouseRightKey);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002992 File Offset: 0x00000B92
		public bool KeyboardCancel
		{
			get
			{
				return this.IsKeyDown(InputService.CancelKey);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000299F File Offset: 0x00000B9F
		public bool IsKeyLongHeld(string keyId)
		{
			return this._keyBindingRegistry.IsLongHeld(keyId);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029AD File Offset: 0x00000BAD
		public bool IsKeyUpAfterShortHeld(string keyId)
		{
			return this._keyBindingRegistry.IsUpAfterShortHeld(keyId);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029BB File Offset: 0x00000BBB
		public static float ProcessScrollWheelAxis(float rawAxisValue)
		{
			if (rawAxisValue < 0.001f)
			{
				return 0f;
			}
			return rawAxisValue / ScrollWheelSpeed.NormalizedScrollAxis.Value;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000029D8 File Offset: 0x00000BD8
		public void CallInputProcessors()
		{
			this._inputProcessorsCopy.AddRange(this._inputProcessors);
			try
			{
				for (int i = 0; i < this._priorityInputProcessors.Count; i++)
				{
					this._priorityInputProcessors[i].ProcessInput();
				}
				for (int j = this._inputProcessorsCopy.Count - 1; j >= 0; j--)
				{
					if (this._inputProcessorsCopy[j].ProcessInput())
					{
						break;
					}
				}
			}
			finally
			{
				this._inputProcessorsCopy.Clear();
			}
		}

		// Token: 0x04000017 RID: 23
		public static readonly string MouseLeftKey = "MouseLeft";

		// Token: 0x04000018 RID: 24
		public static readonly string MouseMiddleKey = "MouseMiddle";

		// Token: 0x04000019 RID: 25
		public static readonly string MouseRightKey = "MouseRight";

		// Token: 0x0400001A RID: 26
		public static readonly string ConfirmKey = "Confirm";

		// Token: 0x0400001B RID: 27
		public static readonly string CancelKey = "Cancel";

		// Token: 0x0400001D RID: 29
		public readonly InputSettings _inputSettings;

		// Token: 0x0400001E RID: 30
		public readonly InputUpdater _inputUpdater;

		// Token: 0x0400001F RID: 31
		public readonly MouseController _mouse;

		// Token: 0x04000020 RID: 32
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x04000021 RID: 33
		public readonly InputBlocker _inputBlocker;

		// Token: 0x04000022 RID: 34
		public readonly List<IPriorityInputProcessor> _priorityInputProcessors = new List<IPriorityInputProcessor>();

		// Token: 0x04000023 RID: 35
		public readonly List<IInputProcessor> _inputProcessors = new List<IInputProcessor>();

		// Token: 0x04000024 RID: 36
		public readonly List<IInputProcessor> _inputProcessorsCopy = new List<IInputProcessor>();
	}
}
