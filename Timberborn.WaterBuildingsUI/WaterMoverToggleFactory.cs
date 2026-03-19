using System;
using Timberborn.Goods;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000021 RID: 33
	public class WaterMoverToggleFactory
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000530A File Offset: 0x0000350A
		public WaterMoverToggleFactory(SliderToggleFactory sliderToggleFactory, IGoodService goodService, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._goodService = goodService;
			this._loc = loc;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005327 File Offset: 0x00003527
		public WaterMoverToggle Create(VisualElement parent)
		{
			WaterMoverToggle waterMoverToggle = new WaterMoverToggle(this._sliderToggleFactory, this._goodService, this._loc);
			waterMoverToggle.Initialize(parent);
			return waterMoverToggle;
		}

		// Token: 0x040000DC RID: 220
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x040000DD RID: 221
		public readonly IGoodService _goodService;

		// Token: 0x040000DE RID: 222
		public readonly ILoc _loc;
	}
}
