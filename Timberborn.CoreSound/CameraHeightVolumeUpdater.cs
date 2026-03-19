using System;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSystem;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.CoreSound
{
	// Token: 0x02000009 RID: 9
	public class CameraHeightVolumeUpdater : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002365 File Offset: 0x00000565
		public CameraHeightVolumeUpdater(ISoundSystem soundSystem, CameraService cameraService, ISpecService specService)
		{
			this._soundSystem = soundSystem;
			this._cameraService = cameraService;
			this._specService = specService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002382 File Offset: 0x00000582
		public float CameraHeight
		{
			get
			{
				return this._cameraService.NormalizedDefaultZoomLevel;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000238F File Offset: 0x0000058F
		public void Load()
		{
			this._spec = this._specService.GetSingleSpec<CoreSoundSpec>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A4 File Offset: 0x000005A4
		public void LateUpdateSingleton()
		{
			float distance = Vector3.Distance(this._soundSystem.ListenerPosition, this._cameraService.Transform.position);
			this.SetVolume(MixerNames.BuildingMixerNameKey, distance, this._spec.MinBuildingFadeDistance, this._spec.MaxBuildingFadeDistance);
			this.SetVolume(MixerNames.AmbientMixerNameKey, this.CameraHeight, this._spec.MinAmbientFade, this._spec.MaxAmbientFade);
			this.SetVolume(MixerNames.WindMixerNameKey, this.CameraHeight, this._spec.MinWindFade, this._spec.MaxWindFade);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002444 File Offset: 0x00000644
		public void SetVolume(string mixerName, float distance, float min, float max)
		{
			string text = mixerName + "_Volume";
			this._soundSystem.SetMixerVolume(text, this.GetVolume(text, distance, min, max));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002474 File Offset: 0x00000674
		public float GetVolume(string parameterName, float distance, float min, float max)
		{
			float num = 1f - (distance - min) / (max - min);
			float mixerVolume = this._soundSystem.GetMixerVolume(parameterName);
			return Mathf.Clamp01(Mathf.Clamp(num, mixerVolume - 0.05f, mixerVolume + 0.05f));
		}

		// Token: 0x0400000D RID: 13
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400000E RID: 14
		public readonly CameraService _cameraService;

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;

		// Token: 0x04000010 RID: 16
		public CoreSoundSpec _spec;
	}
}
