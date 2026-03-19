using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.MapEditorHazardousWeatherUI;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000003 RID: 3
	internal class BadwaterFlowStopper : BaseComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BadwaterFlowStopper(MapEditorHazardousWeatherSetter mapEditorHazardousWeatherSetter)
		{
			this._mapEditorHazardousWeatherSetter = mapEditorHazardousWeatherSetter;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DB File Offset: 0x000002DB
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E9 File Offset: 0x000002E9
		public float GetStrengthModifier()
		{
			return (float)(this._mapEditorHazardousWeatherSetter.IsBadtideWeather ? 0 : 1);
		}

		// Token: 0x04000001 RID: 1
		private readonly MapEditorHazardousWeatherSetter _mapEditorHazardousWeatherSetter;

		// Token: 0x04000002 RID: 2
		private WaterSource _waterSource;
	}
}
