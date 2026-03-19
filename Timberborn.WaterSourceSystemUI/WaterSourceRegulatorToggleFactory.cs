using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x0200000B RID: 11
	public class WaterSourceRegulatorToggleFactory
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002891 File Offset: 0x00000A91
		public WaterSourceRegulatorToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028A7 File Offset: 0x00000AA7
		public WaterSourceRegulatorToggle Create(VisualElement parent, Label label)
		{
			WaterSourceRegulatorToggle waterSourceRegulatorToggle = new WaterSourceRegulatorToggle(this._sliderToggleFactory, this._loc);
			waterSourceRegulatorToggle.Initialize(parent, label);
			return waterSourceRegulatorToggle;
		}

		// Token: 0x0400002D RID: 45
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400002E RID: 46
		public readonly ILoc _loc;
	}
}
