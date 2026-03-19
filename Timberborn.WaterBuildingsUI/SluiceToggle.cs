using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200000E RID: 14
	public class SluiceToggle
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003872 File Offset: 0x00001A72
		public SluiceToggle(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003888 File Offset: 0x00001A88
		public void Initialize(VisualElement parent)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(SluiceToggle.AutoLocKey), SluiceToggle.AutoClass, delegate()
			{
				this._sluiceState.SetAuto();
			}, () => this._sluiceState.AutoMode);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(SluiceToggle.OpenLocKey), SluiceToggle.OpenClass, delegate()
			{
				this._sluiceState.Open();
			}, () => !this._sluiceState.AutoMode && this._sluiceState.IsOpen);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => this._loc.T(SluiceToggle.ClosedLocKey), SluiceToggle.CloseClass, delegate()
			{
				this._sluiceState.Close();
			}, () => !this._sluiceState.AutoMode && !this._sluiceState.IsOpen);
			this._sliderToggle = this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003952 File Offset: 0x00001B52
		public void Show(SluiceState sluiceState)
		{
			this._sluiceState = sluiceState;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000395B File Offset: 0x00001B5B
		public void Update()
		{
			if (this._sluiceState)
			{
				this._sliderToggle.Update();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003975 File Offset: 0x00001B75
		public void Clear()
		{
			this._sluiceState = null;
		}

		// Token: 0x04000064 RID: 100
		public static readonly string AutoClass = "sluice-toggle__icon--auto";

		// Token: 0x04000065 RID: 101
		public static readonly string OpenClass = "sluice-toggle__icon--open";

		// Token: 0x04000066 RID: 102
		public static readonly string CloseClass = "sluice-toggle__icon--close";

		// Token: 0x04000067 RID: 103
		public static readonly string AutoLocKey = "Building.Sluice.Mode.Auto";

		// Token: 0x04000068 RID: 104
		public static readonly string OpenLocKey = "Building.Sluice.Mode.Open";

		// Token: 0x04000069 RID: 105
		public static readonly string ClosedLocKey = "Building.Sluice.Mode.Closed";

		// Token: 0x0400006A RID: 106
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400006B RID: 107
		public readonly ILoc _loc;

		// Token: 0x0400006C RID: 108
		public SluiceState _sluiceState;

		// Token: 0x0400006D RID: 109
		public SliderToggle _sliderToggle;
	}
}
