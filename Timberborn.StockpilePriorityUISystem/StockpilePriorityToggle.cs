using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.StockpilePrioritySystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilePriorityUISystem
{
	// Token: 0x02000007 RID: 7
	public class StockpilePriorityToggle
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000230B File Offset: 0x0000050B
		public StockpilePriorityToggle(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002324 File Offset: 0x00000524
		public void Initialize(VisualElement parent, string toggleBindingKey = null)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(StockpilePriorityToggle.AcceptLocKey), StockpilePriorityToggle.AcceptClass, delegate()
			{
				this._stockpilePriority.Accept();
			}, () => this._stockpilePriority.IsAcceptActive);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(StockpilePriorityToggle.EmptyLocKey), StockpilePriorityToggle.EmptyClass, delegate()
			{
				this._stockpilePriority.Empty();
			}, () => this._stockpilePriority.IsEmptyActive);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => this._loc.T(StockpilePriorityToggle.ObtainLocKey), StockpilePriorityToggle.ObtainClass, delegate()
			{
				this._stockpilePriority.Obtain();
			}, () => this._stockpilePriority.IsObtainActive);
			SliderToggleItem sliderToggleItem4 = SliderToggleItem.Create(() => this._loc.T(StockpilePriorityToggle.SupplyLocKey), StockpilePriorityToggle.SupplyClass, delegate()
			{
				this._stockpilePriority.Supply();
			}, () => this._stockpilePriority.IsSupplyActive);
			this._sliderToggle = (string.IsNullOrWhiteSpace(toggleBindingKey) ? this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem3,
				sliderToggleItem4,
				sliderToggleItem2
			}) : this._sliderToggleFactory.CreateBindable(parent, toggleBindingKey, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem3,
				sliderToggleItem4,
				sliderToggleItem2
			}));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002462 File Offset: 0x00000662
		public void Show(StockpilePriority stockpilePriority)
		{
			this._stockpilePriority = stockpilePriority;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000246B File Offset: 0x0000066B
		public void Update()
		{
			if (!this._sliderToggle.IsBound)
			{
				this._sliderToggle.Bind();
			}
			this._sliderToggle.Update();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002490 File Offset: 0x00000690
		public void Clear()
		{
			this._sliderToggle.Unbind();
			this._sliderToggle.Clear();
			this._stockpilePriority = null;
		}

		// Token: 0x04000016 RID: 22
		public static readonly string AcceptClass = "stockpile-priority-toggle__icon--accept";

		// Token: 0x04000017 RID: 23
		public static readonly string EmptyClass = "stockpile-priority-toggle__icon--empty";

		// Token: 0x04000018 RID: 24
		public static readonly string ObtainClass = "stockpile-priority-toggle__icon--obtain";

		// Token: 0x04000019 RID: 25
		public static readonly string SupplyClass = "stockpile-priority-toggle__icon--supply";

		// Token: 0x0400001A RID: 26
		public static readonly string AcceptLocKey = "StockpilePriority.Accept";

		// Token: 0x0400001B RID: 27
		public static readonly string EmptyLocKey = "StockpilePriority.Empty";

		// Token: 0x0400001C RID: 28
		public static readonly string ObtainLocKey = "StockpilePriority.Obtain";

		// Token: 0x0400001D RID: 29
		public static readonly string SupplyLocKey = "StockpilePriority.Supply";

		// Token: 0x0400001E RID: 30
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;

		// Token: 0x04000020 RID: 32
		public SliderToggle _sliderToggle;

		// Token: 0x04000021 RID: 33
		public StockpilePriority _stockpilePriority;
	}
}
