using System;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSystem;
using Timberborn.Coordinates;
using Timberborn.GridTraversing;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using Timberborn.TerrainQueryingSystem;
using UnityEngine;

namespace Timberborn.CoreSound
{
	// Token: 0x0200000D RID: 13
	public class SoundListener : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000029EC File Offset: 0x00000BEC
		public SoundListener(TerrainPicker terrainPicker, CameraService cameraService, ISoundSystem soundSystem, BlockObjectRaycaster blockObjectRaycaster, ISpecService specService)
		{
			this._terrainPicker = terrainPicker;
			this._cameraService = cameraService;
			this._soundSystem = soundSystem;
			this._blockObjectRaycaster = blockObjectRaycaster;
			this._specService = specService;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void Load()
		{
			CoreSoundSpec singleSpec = this._specService.GetSingleSpec<CoreSoundSpec>();
			this._maxVerticalListenerPositionAboveGround = singleSpec.MaxVerticalListenerPositionAboveGround;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A44 File Offset: 0x00000C44
		public void LateUpdateSingleton()
		{
			Vector3 vector;
			if (this.TryGetScreenCenterPosition(out vector))
			{
				Quaternion rotation = this.GetRotation();
				vector = Vector3.Lerp(this._soundSystem.ListenerPosition, vector, 0.1f);
				this._soundSystem.SetListenerPosition(vector, rotation);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A88 File Offset: 0x00000C88
		public bool TryGetScreenCenterPosition(out Vector3 position)
		{
			Vector2 screenPoint = new Vector2((float)Screen.width, (float)Screen.height) * 0.5f;
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(screenPoint);
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			BlockObjectHit blockObjectHit;
			bool flag = this._blockObjectRaycaster.TryHitBlockObject<BlockObject>(ray, out blockObjectHit);
			if (traversedCoordinates != null)
			{
				float num = (float)this._maxVerticalListenerPositionAboveGround * this._cameraService.NormalizedDefaultZoomLevel;
				Vector3 intersection = traversedCoordinates.Value.Intersection;
				Vector3 position2 = this._cameraService.Transform.position;
				position = CoordinateSystem.GridToWorld(intersection) + new Vector3(0f, num, 0f);
				if (flag)
				{
					Vector3 vector = CoordinateSystem.GridToWorld(blockObjectHit.HitBlock.Coordinates);
					if (Vector3.Distance(position, position2) > Vector3.Distance(vector, position2))
					{
						position = vector;
					}
				}
				return true;
			}
			position = (flag ? CoordinateSystem.GridToWorld(blockObjectHit.HitBlock.Coordinates) : default(Vector3));
			return flag;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BAC File Offset: 0x00000DAC
		public Quaternion GetRotation()
		{
			return Quaternion.AngleAxis(this._cameraService.Transform.rotation.eulerAngles.y, Vector3.up);
		}

		// Token: 0x0400001E RID: 30
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x0400001F RID: 31
		public readonly CameraService _cameraService;

		// Token: 0x04000020 RID: 32
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000021 RID: 33
		public readonly BlockObjectRaycaster _blockObjectRaycaster;

		// Token: 0x04000022 RID: 34
		public readonly ISpecService _specService;

		// Token: 0x04000023 RID: 35
		public int _maxVerticalListenerPositionAboveGround;
	}
}
