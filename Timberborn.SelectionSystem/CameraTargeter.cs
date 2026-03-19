using System;
using Timberborn.CameraSystem;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200000A RID: 10
	public class CameraTargeter : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002449 File Offset: 0x00000649
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002451 File Offset: 0x00000651
		public SelectableObject FollowedTarget { get; private set; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000245A File Offset: 0x0000065A
		public CameraTargeter(InputService inputService, EventBus eventBus, CameraService cameraService)
		{
			this._inputService = inputService;
			this._eventBus = eventBus;
			this._cameraService = cameraService;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002477 File Offset: 0x00000677
		public void Load()
		{
			this._eventBus.Register(this);
			this._cameraService.BeforeCameraUpdate += this.OnBeforeCameraUpdate;
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A8 File Offset: 0x000006A8
		[OnEvent]
		public void OnSelectableObjectUnselectedEvent(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			if (this.FollowedTarget && this.FollowedTarget == selectableObjectUnselectedEvent.SelectableObject)
			{
				this.StopFollowing();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024CC File Offset: 0x000006CC
		public void Follow(SelectableObject targetToFollow)
		{
			this.FollowedTarget = targetToFollow;
			this._previousZoomLevel = this._cameraService.ZoomLevel;
			this.CenterCameraOnFollowedTarget(false);
			Vector3 vector = this._cameraService.Target + this._cameraService.OffsetFromTarget;
			this._distanceToTarget = Vector3.Distance(vector, this.FollowedTarget.CameraTargetPosition);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000252B File Offset: 0x0000072B
		public void StopFollowing()
		{
			this.FollowedTarget = null;
			this._zoomDelta = 0f;
			this._distanceToTarget = 0f;
			this._previousZoomLevel = 0f;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002555 File Offset: 0x00000755
		public void CenterCameraOn(SelectableObject target)
		{
			this.CenterCameraOn(target.CameraTargetPosition, false);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002564 File Offset: 0x00000764
		public bool ProcessInput()
		{
			if (this._inputService.Cancel)
			{
				this.StopFollowing();
			}
			return false;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000257C File Offset: 0x0000077C
		public bool OtherControllerModifiedCameraTarget
		{
			get
			{
				return !this._cameraService.Target.Equals(this._lastPositionCenteredOn);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025A5 File Offset: 0x000007A5
		public void OnBeforeCameraUpdate(object sender, EventArgs e)
		{
			if (this.FollowedTarget)
			{
				if (this.OtherControllerModifiedCameraTarget)
				{
					this.StopFollowing();
					return;
				}
				this.CenterCameraOnFollowedTarget(true);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025CA File Offset: 0x000007CA
		public void CenterCameraOnFollowedTarget(bool updateZoom)
		{
			this.CenterCameraOn(this.FollowedTarget.CameraTargetPosition, updateZoom);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025E0 File Offset: 0x000007E0
		public void CenterCameraOn(Vector3 targetPosition, bool updateZoom)
		{
			Vector3 vector = targetPosition + this._cameraService.OffsetFromTarget;
			Ray ray;
			ray..ctor(vector, targetPosition - vector);
			Plane plane;
			plane..ctor(Vector3.down, 0f);
			float num;
			if (plane.Raycast(ray, ref num))
			{
				Vector3 point = ray.GetPoint(num);
				this._cameraService.MoveTargetTo(point);
				this._lastPositionCenteredOn = this._cameraService.Target;
				if (updateZoom)
				{
					this.UpdateZoom(targetPosition, point);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002660 File Offset: 0x00000860
		public void UpdateZoom(Vector3 targetPosition, Vector3 hitPoint)
		{
			this._zoomDelta += this._cameraService.ZoomLevel - this._previousZoomLevel;
			float distanceFromTarget = Vector3.Distance(hitPoint, targetPosition) + this._distanceToTarget;
			this._cameraService.SetZoomLevel(distanceFromTarget, this._zoomDelta);
			this._previousZoomLevel = this._cameraService.ZoomLevel;
		}

		// Token: 0x04000012 RID: 18
		public readonly InputService _inputService;

		// Token: 0x04000013 RID: 19
		public readonly EventBus _eventBus;

		// Token: 0x04000014 RID: 20
		public readonly CameraService _cameraService;

		// Token: 0x04000015 RID: 21
		public Vector3 _lastPositionCenteredOn;

		// Token: 0x04000016 RID: 22
		public float _distanceToTarget;

		// Token: 0x04000017 RID: 23
		public float _previousZoomLevel;

		// Token: 0x04000018 RID: 24
		public float _zoomDelta;
	}
}
