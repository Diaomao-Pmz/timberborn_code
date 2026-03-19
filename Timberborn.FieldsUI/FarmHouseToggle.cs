using System;
using Timberborn.Fields;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.FieldsUI
{
	// Token: 0x02000007 RID: 7
	public class FarmHouseToggle
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002201 File Offset: 0x00000401
		public FarmHouseToggle(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002218 File Offset: 0x00000418
		public void Initialize(VisualElement parent)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(FarmHouseToggle.PlantingLocKey), FarmHouseToggle.PlantingClass, delegate()
			{
				this._farmHouse.PrioritizePlanting();
			}, () => this._farmHouse.PlantingPrioritized);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(FarmHouseToggle.HarvestingLocKey), FarmHouseToggle.HarvestingClass, delegate()
			{
				this._farmHouse.UnprioritizePlanting();
			}, () => !this._farmHouse.PlantingPrioritized);
			this._sliderToggle = this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022AB File Offset: 0x000004AB
		public void Show(FarmHouse farmHouse)
		{
			this._farmHouse = farmHouse;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B4 File Offset: 0x000004B4
		public void Update()
		{
			if (this._farmHouse)
			{
				this._sliderToggle.Update();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022CE File Offset: 0x000004CE
		public void Clear()
		{
			this._farmHouse = null;
		}

		// Token: 0x0400000E RID: 14
		public static readonly string HarvestingClass = "farmhouse-toggle__icon--harvesting";

		// Token: 0x0400000F RID: 15
		public static readonly string PlantingClass = "farmhouse-toggle__icon--planting";

		// Token: 0x04000010 RID: 16
		public static readonly string HarvestingLocKey = "Fields.Harvesting";

		// Token: 0x04000011 RID: 17
		public static readonly string PlantingLocKey = "Fields.Planting";

		// Token: 0x04000012 RID: 18
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;

		// Token: 0x04000014 RID: 20
		public FarmHouse _farmHouse;

		// Token: 0x04000015 RID: 21
		public SliderToggle _sliderToggle;
	}
}
