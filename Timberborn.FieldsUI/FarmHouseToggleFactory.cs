using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.FieldsUI
{
	// Token: 0x02000008 RID: 8
	public class FarmHouseToggleFactory
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000235C File Offset: 0x0000055C
		public FarmHouseToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002372 File Offset: 0x00000572
		public FarmHouseToggle Create(VisualElement parent)
		{
			FarmHouseToggle farmHouseToggle = new FarmHouseToggle(this._sliderToggleFactory, this._loc);
			farmHouseToggle.Initialize(parent);
			return farmHouseToggle;
		}

		// Token: 0x04000016 RID: 22
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000017 RID: 23
		public readonly ILoc _loc;
	}
}
