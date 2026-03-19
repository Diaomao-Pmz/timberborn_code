using System;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSettingsSystem;
using Timberborn.Coordinates;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200000C RID: 12
	public class CameraService : ISaveableSingleton, ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000026 RID: 38 RVA: 0x000026E8 File Offset: 0x000008E8
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x00002720 File Offset: 0x00000920
		public event EventHandler BeforeCameraUpdate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000028 RID: 40 RVA: 0x00002758 File Offset: 0x00000958
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x00002790 File Offset: 0x00000990
		public event EventHandler CameraPositionOrRotationChanged;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000027C5 File Offset: 0x000009C5
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000027CD File Offset: 0x000009CD
		public Vector3 Target { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027D6 File Offset: 0x000009D6
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000027DE File Offset: 0x000009DE
		public float VerticalAngle { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027E7 File Offset: 0x000009E7
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000027EF File Offset: 0x000009EF
		public float HorizontalAngle { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027F8 File Offset: 0x000009F8
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002800 File Offset: 0x00000A00
		public float ZoomLevel { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002809 File Offset: 0x00000A09
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002811 File Offset: 0x00000A11
		public bool FreeMode { get; set; }

		// Token: 0x06000034 RID: 52 RVA: 0x0000281A File Offset: 0x00000A1A
		public CameraService(ISingletonLoader singletonLoader, MapSize mapSize, MapEditorMode mapEditorMode, CameraStateSerializer cameraStateSerializer, ISpecService specService, CameraFactory cameraFactory, CameraSettings cameraSettings)
		{
			this._singletonLoader = singletonLoader;
			this._mapSize = mapSize;
			this._mapEditorMode = mapEditorMode;
			this._cameraStateSerializer = cameraStateSerializer;
			this._specService = specService;
			this._cameraFactory = cameraFactory;
			this._cameraSettings = cameraSettings;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002857 File Offset: 0x00000A57
		public Vector3 OffsetFromTarget
		{
			get
			{
				return this.Rotation * Vector3.back * this.DistanceFromTarget;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002874 File Offset: 0x00000A74
		public Matrix4x4 ProjectionMatrix
		{
			get
			{
				return this._camera.projectionMatrix;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002881 File Offset: 0x00000A81
		public Transform Transform
		{
			get
			{
				return this._camera.transform;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002890 File Offset: 0x00000A90
		public float NormalizedDefaultZoomLevel
		{
			get
			{
				FloatLimitsSpec defaultZoomLimits = this._cameraServiceSpec.DefaultZoomLimits;
				return Mathf.Clamp01((this.ZoomLevel - defaultZoomLimits.Min) / (defaultZoomLimits.Max - defaultZoomLimits.Min));
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000028CC File Offset: 0x00000ACC
		public float ZoomSpeedScale
		{
			get
			{
				if (this.ZoomLevel < 0f)
				{
					return 1f - Mathf.Abs(this.ZoomLevel / -9f);
				}
				if (this.ZoomLevel > 0f)
				{
					return Math.Max(this.ZoomLevel / 2f, 1f);
				}
				return 1f;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002927 File Offset: 0x00000B27
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002934 File Offset: 0x00000B34
		public float FieldOfView
		{
			get
			{
				return this._camera.fieldOfView;
			}
			set
			{
				this._camera.fieldOfView = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002942 File Offset: 0x00000B42
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000294F File Offset: 0x00000B4F
		public float NearClipPlane
		{
			get
			{
				return this._camera.nearClipPlane;
			}
			set
			{
				this._camera.nearClipPlane = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000295D File Offset: 0x00000B5D
		public Quaternion FacingCamera
		{
			get
			{
				if (this._camera)
				{
					return Quaternion.AngleAxis(this._camera.transform.eulerAngles.y, Vector3.up);
				}
				return Quaternion.identity;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002991 File Offset: 0x00000B91
		public void LateUpdateSingleton()
		{
			EventHandler beforeCameraUpdate = this.BeforeCameraUpdate;
			if (beforeCameraUpdate != null)
			{
				beforeCameraUpdate(this, EventArgs.Empty);
			}
			this.UpdatePositionAndRotation();
			this.CheckPositionOrRotationChange();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029B6 File Offset: 0x00000BB6
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				singletonSaver.GetSingleton(CameraService.CameraServiceKey).Set<CameraState>(CameraService.CameraStateKey, this.GetCurrentState(), this._cameraStateSerializer);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029E8 File Offset: 0x00000BE8
		public void Load()
		{
			this._cameraServiceSpec = this._specService.GetSingleSpec<CameraServiceSpec>();
			this._camera = this._cameraFactory.Create("CameraService");
			this.VerticalAngle = this._cameraServiceSpec.VerticalAngle;
			this.HorizontalAngle = this._cameraServiceSpec.HorizontalAngle;
			this.ZoomLevel = this._cameraServiceSpec.ZoomLevel;
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(CameraService.CameraServiceKey, out objectLoader))
			{
				CameraState cameraState = objectLoader.Get<CameraState>(CameraService.CameraStateKey, this._cameraStateSerializer);
				this.RestoreState(cameraState);
			}
			this.UpdatePositionAndRotation();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A84 File Offset: 0x00000C84
		public void MoveCameraBy(Vector3 delta)
		{
			Vector3 vector = Quaternion.AngleAxis(this.HorizontalAngle, Vector3.up) * delta;
			this.MoveTargetTo(this.Target + vector);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void MoveTargetTo(Vector3 point)
		{
			Vector3 coordinates = CoordinateSystem.WorldToGrid(point);
			this.Target = CoordinateSystem.GridToWorld(this.ClampTarget(coordinates));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public void SetZoomLevel(float distanceFromTarget, float delta)
		{
			float baseDistance = this._cameraServiceSpec.BaseDistance;
			float zoomBase = this._cameraServiceSpec.ZoomBase;
			float num = Mathf.Log(distanceFromTarget / baseDistance) / Mathf.Log(zoomBase);
			this.ZoomLevel = Mathf.Clamp(num + delta, this.ZoomLimitsSpec.Min, this.ZoomLimitsSpec.Max);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B3D File Offset: 0x00000D3D
		public void ModifyZoomLevel(float scroll)
		{
			this.ZoomLevel += this.GetZoomDelta(this.ZoomLevel, scroll, 0.5f);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B5E File Offset: 0x00000D5E
		public void ModifyHorizontalAngle(float delta)
		{
			this.HorizontalAngle += delta;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B70 File Offset: 0x00000D70
		public void ModifyVerticalAngle(float delta)
		{
			float num = this.VerticalAngle + delta;
			this.VerticalAngle = (this.FreeMode ? num : Mathf.Clamp(num, this._cameraServiceSpec.VerticalAngleLimits.Min, this._cameraServiceSpec.VerticalAngleLimits.Max));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BBD File Offset: 0x00000DBD
		public Vector3 WorldSpaceToPanelSpace(VisualElement panel, Vector3 position)
		{
			return RuntimePanelUtils.CameraTransformWorldToPanel(panel.panel, position, this._camera);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public Ray ScreenPointToPreciseRayInWorldSpace(Vector2 screenPoint)
		{
			Vector2 vector = new Vector2(screenPoint.x / (float)Screen.width, screenPoint.y / (float)Screen.height);
			float num = vector.x * 2f - 1f;
			float num2 = vector.y * 2f - 1f;
			Vector4 vector2;
			vector2..ctor(num, num2, -1f, 1f);
			Vector4 vector3 = this._camera.projectionMatrix.inverse * vector2;
			vector3..ctor(vector3.x, vector3.y, -1f, 0f);
			Vector4 normalized = (this._camera.cameraToWorldMatrix * vector3).normalized;
			return new Ray(this._camera.transform.position, normalized);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CAC File Offset: 0x00000EAC
		public Ray ScreenPointToRayInWorldSpace(Vector2 screenPoint)
		{
			return this._camera.ScreenPointToRay(screenPoint);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002CBF File Offset: 0x00000EBF
		public Ray ScreenPointToRayInGridSpace(Vector2 screenPoint)
		{
			return CoordinateSystem.WorldToGrid(this.ScreenPointToRayInWorldSpace(screenPoint));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CCD File Offset: 0x00000ECD
		public CameraState GetCurrentState()
		{
			return new CameraState(this.Target, this.ZoomLevel, this.HorizontalAngle, this.VerticalAngle);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CEC File Offset: 0x00000EEC
		public void RestoreState(CameraState cameraState)
		{
			this.MoveTargetTo(cameraState.Target);
			this.ZoomLevel = cameraState.ZoomLevel;
			this.HorizontalAngle = cameraState.HorizontalAngle;
			this.VerticalAngle = cameraState.VerticalAngle;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D22 File Offset: 0x00000F22
		public void SetProjectionMatrix(Matrix4x4 projectionMatrix)
		{
			this._camera.projectionMatrix = projectionMatrix;
			EventHandler cameraPositionOrRotationChanged = this.CameraPositionOrRotationChanged;
			if (cameraPositionOrRotationChanged == null)
			{
				return;
			}
			cameraPositionOrRotationChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D46 File Offset: 0x00000F46
		public void ResetProjectionMatrix()
		{
			this._camera.ResetProjectionMatrix();
			EventHandler cameraPositionOrRotationChanged = this.CameraPositionOrRotationChanged;
			if (cameraPositionOrRotationChanged == null)
			{
				return;
			}
			cameraPositionOrRotationChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D6C File Offset: 0x00000F6C
		public bool IsInFront(Vector3 point)
		{
			Vector3 forward = this._camera.transform.forward;
			Vector3 vector = point - this._camera.transform.position;
			return Vector3.Angle(forward, vector) < 90f;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DAD File Offset: 0x00000FAD
		public float DistanceFromTarget
		{
			get
			{
				return Mathf.Pow(this._cameraServiceSpec.ZoomBase, this.ZoomLevel) * this._cameraServiceSpec.BaseDistance;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public Quaternion Rotation
		{
			get
			{
				return Quaternion.AngleAxis(this.HorizontalAngle, Vector3.up) * Quaternion.AngleAxis(this.VerticalAngle, Vector3.right);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public FloatLimitsSpec ZoomLimitsSpec
		{
			get
			{
				if (this.FreeMode)
				{
					return this._cameraServiceSpec.FreeModeZoomLimits;
				}
				if (this._cameraSettings.UnlockZoom)
				{
					return this._cameraServiceSpec.UnlockedZoomLimits;
				}
				if (this._mapEditorMode.IsMapEditor)
				{
					return this._cameraServiceSpec.MapEditorZoomLimits;
				}
				return this._cameraServiceSpec.DefaultZoomLimits;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E58 File Offset: 0x00001058
		public void UpdatePositionAndRotation()
		{
			Vector3 vector = this.Target + this.OffsetFromTarget;
			this._camera.transform.SetPositionAndRotation(vector, this.Rotation);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E90 File Offset: 0x00001090
		public void CheckPositionOrRotationChange()
		{
			if (!this._lastFramePosition.Equals(this._camera.transform.position) || !this._lastFrameRotation.Equals(this._camera.transform.rotation))
			{
				EventHandler cameraPositionOrRotationChanged = this.CameraPositionOrRotationChanged;
				if (cameraPositionOrRotationChanged != null)
				{
					cameraPositionOrRotationChanged(this, EventArgs.Empty);
				}
			}
			this._camera.transform.GetPositionAndRotation(ref this._lastFramePosition, ref this._lastFrameRotation);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F0A File Offset: 0x0000110A
		public float GetZoomDelta(float zoomLevel, float scrollDelta, float zoomDeltaLimit)
		{
			return Math.Min(Mathf.Clamp(zoomLevel - scrollDelta * this._cameraServiceSpec.ZoomSpeed, this.ZoomLimitsSpec.Min, this.ZoomLimitsSpec.Max) - zoomLevel, zoomDeltaLimit);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F40 File Offset: 0x00001140
		public Vector3 ClampTarget(Vector3 coordinates)
		{
			Vector3Int terrainSize = this._mapSize.TerrainSize;
			float num = this.FreeMode ? this._cameraServiceSpec.FreeModeMapMargin : this._cameraServiceSpec.MapMargin;
			return new Vector3(Mathf.Clamp(coordinates.x, -num, (float)terrainSize.x + num), Mathf.Clamp(coordinates.y, -num, (float)terrainSize.y + num), Mathf.Clamp(coordinates.z, -num, (float)terrainSize.z + num));
		}

		// Token: 0x04000021 RID: 33
		public static readonly SingletonKey CameraServiceKey = new SingletonKey("CameraService");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<CameraState> CameraStateKey = new PropertyKey<CameraState>("CameraState");

		// Token: 0x0400002A RID: 42
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400002B RID: 43
		public readonly MapSize _mapSize;

		// Token: 0x0400002C RID: 44
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400002D RID: 45
		public readonly CameraStateSerializer _cameraStateSerializer;

		// Token: 0x0400002E RID: 46
		public readonly ISpecService _specService;

		// Token: 0x0400002F RID: 47
		public readonly CameraFactory _cameraFactory;

		// Token: 0x04000030 RID: 48
		public readonly CameraSettings _cameraSettings;

		// Token: 0x04000031 RID: 49
		public Camera _camera;

		// Token: 0x04000032 RID: 50
		public Vector3 _lastFramePosition;

		// Token: 0x04000033 RID: 51
		public Quaternion _lastFrameRotation;

		// Token: 0x04000034 RID: 52
		public CameraServiceSpec _cameraServiceSpec;
	}
}
