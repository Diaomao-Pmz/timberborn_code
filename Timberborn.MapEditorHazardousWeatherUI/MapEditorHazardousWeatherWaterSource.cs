using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000008 RID: 8
	public class MapEditorHazardousWeatherWaterSource : BaseComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002400 File Offset: 0x00000600
		public MapEditorHazardousWeatherWaterSource(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000240F File Offset: 0x0000060F
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
			this._spec = base.GetComponent<HazardousWeatherWaterSourceSpec>();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002429 File Offset: 0x00000629
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002438 File Offset: 0x00000638
		public float GetStrengthModifier()
		{
			string currentHazardousWeatherID = this._mapEditorHazardousWeatherSetter.GetCurrentHazardousWeatherID();
			return (float)(this._spec.ActiveInHazardousWeather.Contains(currentHazardousWeatherID) ? 1 : 0);
		}

		// Token: 0x04000017 RID: 23
		public readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;

		// Token: 0x04000018 RID: 24
		public WaterSource _waterSource;

		// Token: 0x04000019 RID: 25
		public HazardousWeatherWaterSourceSpec _spec;
	}
}
