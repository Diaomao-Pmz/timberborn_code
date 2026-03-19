using System;
using Timberborn.Localization;
using Timberborn.PowerManagement;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.PowerManagementUI
{
	// Token: 0x02000005 RID: 5
	public class ClutchModeToggle
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000022D2 File Offset: 0x000004D2
		public ClutchModeToggle(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022E8 File Offset: 0x000004E8
		public void Initialize(VisualElement parent)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(ClutchModeToggle.EngagedLocKey), ClutchModeToggle.EngagedClass, delegate()
			{
				this._clutch.SetMode(ClutchMode.Engaged);
			}, () => this._clutch.Mode == ClutchMode.Engaged);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(ClutchModeToggle.DisengagedLocKey), ClutchModeToggle.DisengagedClass, delegate()
			{
				this._clutch.SetMode(ClutchMode.Disengaged);
			}, () => this._clutch.Mode == ClutchMode.Disengaged);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => this._loc.T(ClutchModeToggle.AutomatedLocKey), ClutchModeToggle.AutomatedClass, delegate()
			{
				this._clutch.SetMode(ClutchMode.Automated);
			}, () => this._clutch.Mode == ClutchMode.Automated);
			this._sliderToggle = this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023B2 File Offset: 0x000005B2
		public void Show(Clutch clutch)
		{
			this._clutch = clutch;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023BB File Offset: 0x000005BB
		public void Update()
		{
			if (this._clutch)
			{
				this._sliderToggle.Update();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023D5 File Offset: 0x000005D5
		public void Clear()
		{
			this._clutch = null;
		}

		// Token: 0x04000012 RID: 18
		public static readonly string EngagedClass = "clutch-toggle__icon--engaged";

		// Token: 0x04000013 RID: 19
		public static readonly string DisengagedClass = "clutch-toggle__icon--disengaged";

		// Token: 0x04000014 RID: 20
		public static readonly string AutomatedClass = "clutch-toggle__icon--automated";

		// Token: 0x04000015 RID: 21
		public static readonly string EngagedLocKey = "Building.Clutch.Mode.Engaged";

		// Token: 0x04000016 RID: 22
		public static readonly string DisengagedLocKey = "Building.Clutch.Mode.Disengaged";

		// Token: 0x04000017 RID: 23
		public static readonly string AutomatedLocKey = "Automation.Mode.Automated";

		// Token: 0x04000018 RID: 24
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;

		// Token: 0x0400001A RID: 26
		public Clutch _clutch;

		// Token: 0x0400001B RID: 27
		public SliderToggle _sliderToggle;
	}
}
