using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MapEditorTickSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000009 RID: 9
	[MapEditorTickable]
	public class MapEditorWaterContaminationController : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000246C File Offset: 0x0000066C
		public MapEditorWaterContaminationController(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000247B File Offset: 0x0000067B
		public void Awake()
		{
			this._waterSourceContamination = base.GetComponent<WaterSourceContamination>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002489 File Offset: 0x00000689
		public override void Tick()
		{
			if (this._mapEditorHazardousWeatherSetter.IsBadtideWeather)
			{
				this._waterSourceContamination.SetContamination(1f);
				return;
			}
			this._waterSourceContamination.ResetContamination();
		}

		// Token: 0x0400001A RID: 26
		public readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;

		// Token: 0x0400001B RID: 27
		public WaterSourceContamination _waterSourceContamination;
	}
}
