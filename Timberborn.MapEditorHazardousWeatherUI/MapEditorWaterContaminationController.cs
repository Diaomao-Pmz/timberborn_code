using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MapEditorTickSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorHazardousWeatherUI
{
	// Token: 0x02000008 RID: 8
	[MapEditorTickable]
	internal class MapEditorWaterContaminationController : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002474 File Offset: 0x00000674
		public MapEditorWaterContaminationController(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002483 File Offset: 0x00000683
		public void Awake()
		{
			this._waterSourceContamination = base.GetComponent<WaterSourceContamination>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002491 File Offset: 0x00000691
		public override void Tick()
		{
			if (this._mapEditorHazardousWeatherSetter.IsBadtideWeather)
			{
				this._waterSourceContamination.SetContamination(1f);
				return;
			}
			this._waterSourceContamination.ResetContamination();
		}

		// Token: 0x04000015 RID: 21
		private readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;

		// Token: 0x04000016 RID: 22
		private WaterSourceContamination _waterSourceContamination;
	}
}
