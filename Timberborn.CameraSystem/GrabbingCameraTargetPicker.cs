using System;
using Timberborn.Coordinates;
using Timberborn.CoreUI;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200001A RID: 26
	public class GrabbingCameraTargetPicker : ILoadableSingleton
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004899 File Offset: 0x00002A99
		public GrabbingCameraTargetPicker(InputService inputService, ICameraAnchorPicker cameraAnchorPicker, CameraService cameraService, CursorService cursorService, EventBus eventBus)
		{
			this._inputService = inputService;
			this._cameraAnchorPicker = cameraAnchorPicker;
			this._cameraService = cameraService;
			this._cursorService = cursorService;
			this._eventBus = eventBus;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000048C6 File Offset: 0x00002AC6
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000048D4 File Offset: 0x00002AD4
		public Vector3 PickCameraTarget()
		{
			if (this._inputService.MoveButtonHeld)
			{
				if (this._startingLevel == null)
				{
					this.StartGrabbing();
				}
				else
				{
					Vector3? vector = this.DeltaFromStartingTarget(this._startingLevel.Value);
					if (vector != null)
					{
						Vector3 valueOrDefault = vector.GetValueOrDefault();
						return this._startingTarget + valueOrDefault;
					}
				}
			}
			else if (this._startedGrabbing)
			{
				this.StopGrabbing();
			}
			return this._cameraService.Target;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000494C File Offset: 0x00002B4C
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			if (this._startedGrabbing)
			{
				this.StopGrabbing();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000495C File Offset: 0x00002B5C
		public void StartGrabbing()
		{
			this._startedGrabbing = true;
			this._cursorService.SetTemporaryCursor(GrabbingCameraTargetPicker.CursorKey);
			this._startingMousePosition = this._inputService.MousePosition;
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(this._startingMousePosition);
			Vector3? vector = this._cameraAnchorPicker.PickAnchorPoint(ray);
			if (vector != null)
			{
				this._startingLevel = new float?(CoordinateSystem.GridToWorld(vector.Value).y);
			}
			else
			{
				this._startingLevel = new float?(0f);
			}
			this._startingTarget = this._cameraService.Target;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000049FD File Offset: 0x00002BFD
		public void StopGrabbing()
		{
			this._cursorService.ResetTemporaryCursor();
			this._startingLevel = null;
			this._startedGrabbing = false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004A20 File Offset: 0x00002C20
		public Vector3? DeltaFromStartingTarget(float startingLevel)
		{
			Ray ray = this._cameraService.ScreenPointToPreciseRayInWorldSpace(this._startingMousePosition);
			if (this.IntersectsWithLevel(ray, startingLevel))
			{
				Plane plane;
				plane..ctor(Vector3.down, startingLevel);
				Ray ray2 = this._cameraService.ScreenPointToPreciseRayInWorldSpace(this._inputService.MousePosition);
				Vector3? vector = GrabbingCameraTargetPicker.IntersectionWithPlane(ray, plane);
				if (vector != null)
				{
					Vector3 valueOrDefault = vector.GetValueOrDefault();
					vector = GrabbingCameraTargetPicker.IntersectionWithPlane(ray2, plane);
					if (vector != null)
					{
						Vector3 valueOrDefault2 = vector.GetValueOrDefault();
						Vector3 vector2 = valueOrDefault - valueOrDefault2;
						return new Vector3?(new Vector3(vector2.x, 0f, vector2.z));
					}
				}
			}
			return null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004ADC File Offset: 0x00002CDC
		public bool IntersectsWithLevel(Ray ray, float level)
		{
			return GridSpaceRaycasting.HitHorizontalPlane(CoordinateSystem.WorldToGrid(ray), level) != null;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004B00 File Offset: 0x00002D00
		public static Vector3? IntersectionWithPlane(Ray ray, Plane plane)
		{
			float num;
			if (plane.Raycast(ray, ref num))
			{
				return new Vector3?(ray.GetPoint(num));
			}
			return null;
		}

		// Token: 0x0400007B RID: 123
		public static readonly string CursorKey = "GrabbingCursor";

		// Token: 0x0400007C RID: 124
		public readonly InputService _inputService;

		// Token: 0x0400007D RID: 125
		public readonly ICameraAnchorPicker _cameraAnchorPicker;

		// Token: 0x0400007E RID: 126
		public readonly CameraService _cameraService;

		// Token: 0x0400007F RID: 127
		public readonly CursorService _cursorService;

		// Token: 0x04000080 RID: 128
		public readonly EventBus _eventBus;

		// Token: 0x04000081 RID: 129
		public float? _startingLevel;

		// Token: 0x04000082 RID: 130
		public Vector3 _startingTarget;

		// Token: 0x04000083 RID: 131
		public Vector2 _startingMousePosition;

		// Token: 0x04000084 RID: 132
		public bool _startedGrabbing;
	}
}
