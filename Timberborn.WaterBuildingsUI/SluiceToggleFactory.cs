using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200000F RID: 15
	public class SluiceToggleFactory
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003A61 File Offset: 0x00001C61
		public SluiceToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003A77 File Offset: 0x00001C77
		public SluiceToggle Create(VisualElement parent)
		{
			SluiceToggle sluiceToggle = new SluiceToggle(this._sliderToggleFactory, this._loc);
			sluiceToggle.Initialize(parent);
			return sluiceToggle;
		}

		// Token: 0x0400006E RID: 110
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400006F RID: 111
		public readonly ILoc _loc;
	}
}
