using System;
using Timberborn.Goods;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200001F RID: 31
	public class WaterMoverToggle
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00005085 File Offset: 0x00003285
		public WaterMoverToggle(SliderToggleFactory sliderToggleFactory, IGoodService goodService, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._goodService = goodService;
			this._loc = loc;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000050A4 File Offset: 0x000032A4
		public void Initialize(VisualElement parent)
		{
			GoodSpec cleanWater = this._goodService.GetGood(WaterMoverToggle.CleanWaterGoodId);
			GoodSpec contaminatedWater = this._goodService.GetGood(WaterMoverToggle.ContaminatedWaterGoodId);
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(WaterMoverToggle.UnfilteredWaterLocKey), WaterMoverToggle.UnfilteredWaterClass, delegate()
			{
				this.SetWaterMovement(true, true);
			}, () => this._waterMover.CleanWaterMovement && this._waterMover.ContaminatedWaterMovement);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => cleanWater.DisplayName.Value, cleanWater.IconSmall.Value, delegate()
			{
				this.SetWaterMovement(true, false);
			}, () => this._waterMover.CleanWaterMovement && !this._waterMover.ContaminatedWaterMovement);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => contaminatedWater.DisplayName.Value, contaminatedWater.IconSmall.Value, delegate()
			{
				this.SetWaterMovement(false, true);
			}, () => !this._waterMover.CleanWaterMovement && this._waterMover.ContaminatedWaterMovement);
			this._sliderToggle = this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000051BD File Offset: 0x000033BD
		public void Show(WaterMover waterMover)
		{
			this._waterMover = waterMover;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000051C6 File Offset: 0x000033C6
		public void Update()
		{
			if (this._waterMover)
			{
				this._sliderToggle.Update();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000051E0 File Offset: 0x000033E0
		public void Clear()
		{
			this._waterMover = null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000051E9 File Offset: 0x000033E9
		public void SetWaterMovement(bool moveCleanWater, bool moveContaminatedWater)
		{
			this._waterMover.CleanWaterMovement = moveCleanWater;
			this._waterMover.ContaminatedWaterMovement = moveContaminatedWater;
		}

		// Token: 0x040000D0 RID: 208
		public static readonly string UnfilteredWaterLocKey = "WaterMover.Unfiltered";

		// Token: 0x040000D1 RID: 209
		public static readonly string UnfilteredWaterClass = "water-mover-toggle__icon--unfiltered";

		// Token: 0x040000D2 RID: 210
		public static readonly string CleanWaterGoodId = "Water";

		// Token: 0x040000D3 RID: 211
		public static readonly string ContaminatedWaterGoodId = "Badwater";

		// Token: 0x040000D4 RID: 212
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x040000D5 RID: 213
		public readonly IGoodService _goodService;

		// Token: 0x040000D6 RID: 214
		public readonly ILoc _loc;

		// Token: 0x040000D7 RID: 215
		public WaterMover _waterMover;

		// Token: 0x040000D8 RID: 216
		public SliderToggle _sliderToggle;
	}
}
