using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000013 RID: 19
	public class GateToggleFactory
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000035D7 File Offset: 0x000017D7
		public GateToggleFactory(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000035ED File Offset: 0x000017ED
		public GateToggle Create(VisualElement parent, Label label)
		{
			GateToggle gateToggle = new GateToggle(this._sliderToggleFactory, this._loc);
			gateToggle.Initialize(parent, label);
			return gateToggle;
		}

		// Token: 0x04000073 RID: 115
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000074 RID: 116
		public readonly ILoc _loc;
	}
}
