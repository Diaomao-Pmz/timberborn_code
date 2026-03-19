using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x0200000A RID: 10
	public class MapEditorWaterStrengthModifier : BaseComponent, IStartableComponent, IWaterStrengthModifier
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000024B4 File Offset: 0x000006B4
		public MapEditorWaterStrengthModifier(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024C3 File Offset: 0x000006C3
		public void Start()
		{
			base.GetComponent<WaterSource>().AddWaterStrengthModifier(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D1 File Offset: 0x000006D1
		public float GetStrengthModifier()
		{
			if (!this._mapEditorHazardousWeatherSetter.IsDroughtWeather)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x0400001C RID: 28
		public readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;
	}
}
