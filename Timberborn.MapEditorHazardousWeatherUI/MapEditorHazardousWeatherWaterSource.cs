using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000007 RID: 7
	internal class MapEditorHazardousWeatherWaterSource : BaseComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002408 File Offset: 0x00000608
		public MapEditorHazardousWeatherWaterSource(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002417 File Offset: 0x00000617
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
			this._spec = base.GetComponent<HazardousWeatherWaterSourceSpec>();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002431 File Offset: 0x00000631
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002440 File Offset: 0x00000640
		public float GetStrengthModifier()
		{
			string currentHazardousWeatherID = this._mapEditorHazardousWeatherSetter.GetCurrentHazardousWeatherID();
			return (float)(this._spec.ActiveInHazardousWeather.Contains(currentHazardousWeatherID) ? 1 : 0);
		}

		// Token: 0x04000012 RID: 18
		private readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;

		// Token: 0x04000013 RID: 19
		private WaterSource _waterSource;

		// Token: 0x04000014 RID: 20
		private HazardousWeatherWaterSourceSpec _spec;
	}
}
