using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x0200000F RID: 15
	public class SkyboxPositioner : ILoadableSingleton, IUpdatableSingleton, IUnloadableSingleton
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D9E File Offset: 0x00000F9E
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public Material SkyboxMaterial { get; private set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00002DAF File Offset: 0x00000FAF
		public SkyboxPositioner(DayStageCycle dayStageCycle, ISpecService specService)
		{
			this._dayStageCycle = dayStageCycle;
			this._specService = specService;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DC5 File Offset: 0x00000FC5
		public void Load()
		{
			this._skyboxPositionerSpec = this._specService.GetSingleSpec<SkyboxPositionerSpec>();
			this.SkyboxMaterial = new Material(this._skyboxPositionerSpec.Skybox.Asset);
			this.UpdateDayProgress();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public void UpdateSingleton()
		{
			this.UpdateDayProgress();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E01 File Offset: 0x00001001
		public void Unload()
		{
			Object.Destroy(this.SkyboxMaterial);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E10 File Offset: 0x00001010
		public void UpdateDayProgress()
		{
			DayStageTransition currentTransition = this._dayStageCycle.GetCurrentTransition();
			float num = SkyboxPositioner.LerpDayProgressWrappingFrom1To0(this.DayProgress(currentTransition.CurrentDayStage), this.DayProgress(currentTransition.NextDayStage), currentTransition.TransitionProgress);
			this.SkyboxMaterial.SetFloat(SkyboxPositioner.DayProgressProperty, num);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E64 File Offset: 0x00001064
		public static float LerpDayProgressWrappingFrom1To0(float previousValue, float nextValue, float transitionProgress)
		{
			if (previousValue > nextValue)
			{
				nextValue += 1f;
			}
			float num = Mathf.Lerp(previousValue, nextValue, transitionProgress);
			if (num > 1f)
			{
				num -= 1f;
			}
			return num;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E98 File Offset: 0x00001098
		public float DayProgress(DayStage dayStage)
		{
			float result;
			switch (dayStage)
			{
			case DayStage.Sunrise:
				result = this._skyboxPositionerSpec.DayProgressSunrise;
				break;
			case DayStage.Day:
				result = this._skyboxPositionerSpec.DayProgressDay;
				break;
			case DayStage.Sunset:
				result = this._skyboxPositionerSpec.DayProgressSunset;
				break;
			case DayStage.Night:
				result = this._skyboxPositionerSpec.DayProgressNight;
				break;
			default:
				throw new ArgumentOutOfRangeException("dayStage", dayStage, null);
			}
			return result;
		}

		// Token: 0x04000029 RID: 41
		public static readonly int DayProgressProperty = Shader.PropertyToID("_DayProgress");

		// Token: 0x0400002B RID: 43
		public readonly DayStageCycle _dayStageCycle;

		// Token: 0x0400002C RID: 44
		public readonly ISpecService _specService;

		// Token: 0x0400002D RID: 45
		public SkyboxPositionerSpec _skyboxPositionerSpec;
	}
}
