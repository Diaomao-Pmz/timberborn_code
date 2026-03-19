using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.StockpilePrioritySystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilePriorityUISystem
{
	// Token: 0x02000006 RID: 6
	public class StockpilePriorityFragment : IEntityPanelFragment
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002147 File Offset: 0x00000347
		public StockpilePriorityFragment(VisualElementLoader visualElementLoader, StockpilePriorityToggleFactory stockpilePriorityToggleFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._stockpilePriorityToggleFactory = stockpilePriorityToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002164 File Offset: 0x00000364
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/StockpilePriorityFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._description = UQueryExtensions.Q<Label>(this._root, "Description", null);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(this._root, "ToggleWrapper", null);
			this._sliderToggle = this._stockpilePriorityToggleFactory.CreateBindable(parent, StockpilePriorityFragment.ToggleStockpilePriorityKey);
			return this._root;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D0 File Offset: 0x000003D0
		public void ShowFragment(BaseComponent entity)
		{
			StockpilePriority component = entity.GetComponent<StockpilePriority>();
			if (component != null)
			{
				this._stockpilePriority = component;
				this._sliderToggle.Show(this._stockpilePriority);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FF File Offset: 0x000003FF
		public void ClearFragment()
		{
			this._sliderToggle.Clear();
			this._stockpilePriority = null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._stockpilePriority);
			if (this._stockpilePriority)
			{
				this._sliderToggle.Update();
				this._description.text = this.GetDescription();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002260 File Offset: 0x00000460
		public string GetDescription()
		{
			if (this._stockpilePriority.IsEmptyActive)
			{
				return this._loc.T(StockpilePriorityFragment.EmptyLongLocKey);
			}
			if (this._stockpilePriority.IsObtainActive)
			{
				return this._loc.T(StockpilePriorityFragment.ObtainLongLocKey);
			}
			if (this._stockpilePriority.IsSupplyActive)
			{
				return this._loc.T(StockpilePriorityFragment.SupplyLongLocKey);
			}
			return this._loc.T(StockpilePriorityFragment.AcceptLongLocKey);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string AcceptLongLocKey = "StockpilePriority.Accept.Long";

		// Token: 0x0400000B RID: 11
		public static readonly string EmptyLongLocKey = "StockpilePriority.Empty.Long";

		// Token: 0x0400000C RID: 12
		public static readonly string ObtainLongLocKey = "StockpilePriority.Obtain.Long";

		// Token: 0x0400000D RID: 13
		public static readonly string SupplyLongLocKey = "StockpilePriority.Supply.Long";

		// Token: 0x0400000E RID: 14
		public static readonly string ToggleStockpilePriorityKey = "ToggleStockpilePriority";

		// Token: 0x0400000F RID: 15
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000010 RID: 16
		public readonly StockpilePriorityToggleFactory _stockpilePriorityToggleFactory;

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;

		// Token: 0x04000012 RID: 18
		public StockpilePriority _stockpilePriority;

		// Token: 0x04000013 RID: 19
		public VisualElement _root;

		// Token: 0x04000014 RID: 20
		public StockpilePriorityToggle _sliderToggle;

		// Token: 0x04000015 RID: 21
		public Label _description;
	}
}
