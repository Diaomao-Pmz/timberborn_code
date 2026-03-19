using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilePriorityUISystem
{
	// Token: 0x02000008 RID: 8
	public class StockpilePriorityToggleFactory
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000025BD File Offset: 0x000007BD
		public StockpilePriorityToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025D3 File Offset: 0x000007D3
		public StockpilePriorityToggle Create(VisualElement parent)
		{
			return this.CreateBindable(parent, null);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025DD File Offset: 0x000007DD
		public StockpilePriorityToggle CreateBindable(VisualElement parent, string toggleBindingKey)
		{
			StockpilePriorityToggle stockpilePriorityToggle = new StockpilePriorityToggle(this._sliderToggleFactory, this._loc);
			stockpilePriorityToggle.Initialize(parent, toggleBindingKey);
			return stockpilePriorityToggle;
		}

		// Token: 0x04000022 RID: 34
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000023 RID: 35
		public readonly ILoc _loc;
	}
}
