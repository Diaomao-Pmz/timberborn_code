using System;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.RootProviders;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x02000013 RID: 19
	public class Sun : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000032EB File Offset: 0x000014EB
		public Sun(CameraService cameraService, DayStageCycle dayStageCycle, ISpecService specService, GraphicsQualitySettings graphicsQualitySettings, RootObjectProvider rootObjectProvider)
		{
			this._cameraService = cameraService;
			this._dayStageCycle = dayStageCycle;
			this._specService = specService;
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003318 File Offset: 0x00001518
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000331F File Offset: 0x0000151F
		public bool Fog
		{
			get
			{
				return RenderSettings.fog;
			}
			set
			{
				RenderSettings.fog = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003327 File Offset: 0x00001527
		public Transform Transform
		{
			get
			{
				return this._sun.transform;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003334 File Offset: 0x00001534
		public void Load()
		{
			RenderSettings.ambientMode = 1;
			GameObject gameObject = this._rootObjectProvider.CreateRootObject("Sun");
			this._sunSpec = this._specService.GetSingleSpec<SunSpec>();
			this._sun = Object.Instantiate<Light>(this._sunSpec.SunPrefab.Asset, gameObject.transform);
			this._graphicsQualitySettings.ShadowQualityChanged += delegate(object _, SettingChangedEventArgs<int> _)
			{
				this.UpdateShadows();
			};
			this.UpdateShadows();
			this.UpdateColorsAndRotation();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000033AD File Offset: 0x000015AD
		public void LateUpdateSingleton()
		{
			this.UpdateColorsAndRotation();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000033B5 File Offset: 0x000015B5
		public void ToggleSunRotation()
		{
			this._rotationStopped = !this._rotationStopped;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000033C8 File Offset: 0x000015C8
		public float GetCameraYAngle(Transform cameraTransform)
		{
			return cameraTransform.localRotation.eulerAngles.y + this._sunSpec.RotateWithCameraOffset;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000033F4 File Offset: 0x000015F4
		public void UpdateShadows()
		{
			Light sun = this._sun;
			LightShadows shadows;
			if (this._graphicsQualitySettings.ShadowQuality == 0)
			{
				shadows = 0;
			}
			else
			{
				shadows = 2;
			}
			sun.shadows = shadows;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003420 File Offset: 0x00001620
		public void UpdateColorsAndRotation()
		{
			DayStageTransition currentTransition = this._dayStageCycle.GetCurrentTransition();
			this.UpdateColors(currentTransition);
			this.UpdateRotation(currentTransition);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003448 File Offset: 0x00001648
		public void UpdateRotation(DayStageTransition dayStageTransition)
		{
			DayStageColorsSpec dayStageColorsSpec = this.DayStageColors(dayStageTransition.CurrentDayStage);
			DayStageColorsSpec dayStageColorsSpec2 = this.DayStageColors(dayStageTransition.NextDayStage);
			float transitionProgress = dayStageTransition.TransitionProgress;
			float num = Mathf.Lerp(dayStageColorsSpec.SunXAngle, dayStageColorsSpec2.SunXAngle, transitionProgress);
			float num2 = this._rotationStopped ? this._sun.transform.localRotation.eulerAngles.y : this.GetCameraYAngle(this._cameraService.Transform);
			this._sun.transform.localRotation = Quaternion.Euler(num, num2, this._sun.transform.localRotation.eulerAngles.z);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000034FC File Offset: 0x000016FC
		public void UpdateColors(DayStageTransition dayStageTransition)
		{
			DayStageColorsSpec dayStageColorsSpec = this.DayStageColors(dayStageTransition.CurrentDayStage);
			DayStageColorsSpec dayStageColorsSpec2 = this.DayStageColors(dayStageTransition.NextDayStage);
			float transitionProgress = dayStageTransition.TransitionProgress;
			this._sun.color = Color.Lerp(dayStageColorsSpec.SunColor, dayStageColorsSpec2.SunColor, transitionProgress);
			this._sun.intensity = Mathf.Lerp(dayStageColorsSpec.SunIntensity, dayStageColorsSpec2.SunIntensity, transitionProgress);
			this._sun.shadowStrength = Mathf.Lerp(dayStageColorsSpec.ShadowStrength, dayStageColorsSpec2.ShadowStrength, transitionProgress);
			RenderSettings.ambientSkyColor = Color.Lerp(dayStageColorsSpec.AmbientSkyColor, dayStageColorsSpec2.AmbientSkyColor, transitionProgress);
			RenderSettings.ambientEquatorColor = Color.Lerp(dayStageColorsSpec.AmbientEquatorColor, dayStageColorsSpec2.AmbientEquatorColor, transitionProgress);
			RenderSettings.ambientGroundColor = Color.Lerp(dayStageColorsSpec.AmbientGroundColor, dayStageColorsSpec2.AmbientGroundColor, transitionProgress);
			RenderSettings.reflectionIntensity = Mathf.Lerp(dayStageColorsSpec.ReflectionsIntensity, dayStageColorsSpec2.ReflectionsIntensity, transitionProgress);
			FogSettingsSpec fogSettings = Sun.GetFogSettings(dayStageTransition.CurrentDayStageHazardousWeatherId, dayStageColorsSpec);
			FogSettingsSpec fogSettings2 = Sun.GetFogSettings(dayStageTransition.NextDayStageHazardousWeatherId, dayStageColorsSpec2);
			RenderSettings.fogColor = Color.Lerp(fogSettings.FogColor, fogSettings2.FogColor, transitionProgress);
			RenderSettings.fogDensity = Mathf.Lerp(fogSettings.FogDensity, fogSettings2.FogDensity, transitionProgress);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003628 File Offset: 0x00001828
		public DayStageColorsSpec DayStageColors(DayStage dayStage)
		{
			DayStageColorsSpec result;
			switch (dayStage)
			{
			case DayStage.Sunrise:
				result = this._sunSpec.SunriseColors;
				break;
			case DayStage.Day:
				result = this._sunSpec.DayColors;
				break;
			case DayStage.Sunset:
				result = this._sunSpec.SunsetColors;
				break;
			case DayStage.Night:
				result = this._sunSpec.NightColors;
				break;
			default:
				throw new ArgumentOutOfRangeException("dayStage", dayStage, null);
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003698 File Offset: 0x00001898
		public static FogSettingsSpec GetFogSettings(string hazardousWeatherId, DayStageColorsSpec dayStageColorsSpec)
		{
			if (string.IsNullOrEmpty(hazardousWeatherId))
			{
				return dayStageColorsSpec.TemperateWeatherFog;
			}
			foreach (HazardousWeatherFogSettingsSpec hazardousWeatherFogSettingsSpec in dayStageColorsSpec.HazardousWeatherFogs)
			{
				if (hazardousWeatherFogSettingsSpec.HazardousWeatherId == hazardousWeatherId)
				{
					return hazardousWeatherFogSettingsSpec.FogSettings;
				}
			}
			throw new ArgumentException("Weather fog settings not found for " + hazardousWeatherId);
		}

		// Token: 0x04000034 RID: 52
		public readonly CameraService _cameraService;

		// Token: 0x04000035 RID: 53
		public readonly DayStageCycle _dayStageCycle;

		// Token: 0x04000036 RID: 54
		public readonly ISpecService _specService;

		// Token: 0x04000037 RID: 55
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000038 RID: 56
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000039 RID: 57
		public Light _sun;

		// Token: 0x0400003A RID: 58
		public SunSpec _sunSpec;

		// Token: 0x0400003B RID: 59
		public bool _rotationStopped;
	}
}
