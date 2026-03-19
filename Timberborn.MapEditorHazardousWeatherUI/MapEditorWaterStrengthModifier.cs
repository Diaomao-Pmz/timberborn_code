using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000009 RID: 9
	internal class MapEditorWaterStrengthModifier : BaseComponent, IStartableComponent, IWaterStrengthModifier
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000024BC File Offset: 0x000006BC
		public MapEditorWaterStrengthModifier(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024CB File Offset: 0x000006CB
		public void Start()
		{
			base.GetComponent<WaterSource>().AddWaterStrengthModifier(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D9 File Offset: 0x000006D9
		public float GetStrengthModifier()
		{
			if (!this._mapEditorHazardousWeatherSetter.IsDroughtWeather)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x04000017 RID: 23
		private readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;
	}
}
