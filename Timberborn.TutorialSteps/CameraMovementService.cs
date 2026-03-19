using System;
using Timberborn.CameraSystem;
using Timberborn.SingletonSystem;
using Timberborn.TutorialSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000013 RID: 19
	public class CameraMovementService : ILoadableSingleton
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002D53 File Offset: 0x00000F53
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002D5B File Offset: 0x00000F5B
		public float UpMovement { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002D64 File Offset: 0x00000F64
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002D6C File Offset: 0x00000F6C
		public float DownMovement { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002D75 File Offset: 0x00000F75
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002D7D File Offset: 0x00000F7D
		public float LeftMovement { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002D86 File Offset: 0x00000F86
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002D8E File Offset: 0x00000F8E
		public float RightMovement { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002D97 File Offset: 0x00000F97
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002D9F File Offset: 0x00000F9F
		public float LeftRotation { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002DA8 File Offset: 0x00000FA8
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public float RightRotation { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002DB9 File Offset: 0x00000FB9
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public float ZoomIn { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002DCA File Offset: 0x00000FCA
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public float ZoomOut { get; private set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00002DDB File Offset: 0x00000FDB
		public CameraMovementService(CameraService cameraService, EventBus eventBus)
		{
			this._cameraService = cameraService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002DFF File Offset: 0x00000FFF
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._lastCameraState = this._cameraService.GetCurrentState();
			this._cameraService.BeforeCameraUpdate += this.OnBeforeCameraUpdate;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E2C File Offset: 0x0000102C
		[OnEvent]
		public void OnTutorialStageStarted(TutorialStageStartedEvent tutorialStageStartedEvent)
		{
			this.UpMovement = 0f;
			this.DownMovement = 0f;
			this.LeftMovement = 0f;
			this.RightMovement = 0f;
			this.LeftRotation = 0f;
			this.RightRotation = 0f;
			this.ZoomIn = 0f;
			this.ZoomOut = 0f;
			this._lastCameraState = this._cameraService.GetCurrentState();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002EA4 File Offset: 0x000010A4
		public void OnBeforeCameraUpdate(object sender, EventArgs e)
		{
			CameraState currentState = this._cameraService.GetCurrentState();
			if (currentState != this._lastCameraState)
			{
				this.Update(currentState);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002ED4 File Offset: 0x000010D4
		public void Update(CameraState currentCameraState)
		{
			Vector3 vector = currentCameraState.Target - this._lastCameraState.Target;
			float num = currentCameraState.ZoomLevel - this._lastCameraState.ZoomLevel;
			float num2 = currentCameraState.HorizontalAngle - this._lastCameraState.HorizontalAngle;
			Vector3 vector2 = Quaternion.Euler(0f, -currentCameraState.HorizontalAngle, 0f) * vector;
			this.UpMovement += ((vector2.z > 0f) ? vector2.z : 0f);
			this.DownMovement += ((vector2.z < 0f) ? (-vector2.z) : 0f);
			this.LeftMovement += ((vector2.x < 0f) ? (-vector2.x) : 0f);
			this.RightMovement += ((vector2.x > 0f) ? vector2.x : 0f);
			this.RightRotation += ((num2 < 0f) ? (-num2) : 0f);
			this.LeftRotation += ((num2 > 0f) ? num2 : 0f);
			this.ZoomIn += ((num < 0f) ? (-num) : 0f);
			this.ZoomOut += ((num > 0f) ? num : 0f);
			this._lastCameraState = currentCameraState;
		}

		// Token: 0x04000039 RID: 57
		public readonly CameraService _cameraService;

		// Token: 0x0400003A RID: 58
		public readonly EventBus _eventBus;

		// Token: 0x0400003B RID: 59
		public CameraState _lastCameraState;
	}
}
