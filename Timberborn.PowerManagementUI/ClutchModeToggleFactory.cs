using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.PowerManagementUI
{
	// Token: 0x02000006 RID: 6
	public class ClutchModeToggleFactory
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024AC File Offset: 0x000006AC
		public ClutchModeToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024C2 File Offset: 0x000006C2
		public ClutchModeToggle Create(VisualElement parent)
		{
			ClutchModeToggle clutchModeToggle = new ClutchModeToggle(this._sliderToggleFactory, this._loc);
			clutchModeToggle.Initialize(parent);
			return clutchModeToggle;
		}

		// Token: 0x0400001C RID: 28
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400001D RID: 29
		public readonly ILoc _loc;
	}
}
