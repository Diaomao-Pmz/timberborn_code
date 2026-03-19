using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.EntityUndoSystem;
using Timberborn.InventorySystem;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;
using Timberborn.StockpileVisualization;
using Timberborn.UndoSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorStockpilesUI
{
	// Token: 0x02000004 RID: 4
	internal class FixedStockpileFragment : IEntityPanelFragment, ILoadableSingleton
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021B5 File Offset: 0x000003B5
		public FixedStockpileFragment(EventBus eventBus, VisualElementLoader visualElementLoader, DropdownItemsSetter dropdownItemsSetter, EntityChangeRecorderFactory entityChangeRecorderFactory)
		{
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._entityChangeRecorderFactory = entityChangeRecorderFactory;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021DA File Offset: 0x000003DA
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		public VisualElement InitializeFragment()
		{
			string elementName = "MapEditor/EntityPanel/FixedStockpileFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._goods = this._root.Q("Goods", null);
			this._amount = this._root.Q("Amount", null);
			this._amount.RegisterValueChangedCallback(new EventCallback<ChangeEvent<int>>(this.OnGoodAmountChanged));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002268 File Offset: 0x00000468
		public void ShowFragment(BaseComponent entity)
		{
			Stockpile component = entity.GetComponent<Stockpile>();
			if (component != null)
			{
				this._fixedStockpileDropdownProvider = component.GetComponent<FixedStockpileDropdownProvider>();
				this._fixedStockpileInventorySetter = component.GetComponent<FixedStockpileInventorySetter>();
				this._stockpileVisualizationUpdater = component.GetComponent<StockpileVisualizationUpdater>();
				this._inventory = component.Inventory;
				this._amount.SetValueWithoutNotify(this._inventory.TotalAmountInStock);
				this._amount.isDelayed = true;
				this._dropdownItemsSetter.SetItems(this._goods, this._fixedStockpileDropdownProvider);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F4 File Offset: 0x000004F4
		public void ClearFragment()
		{
			this._inventory = null;
			this._fixedStockpileDropdownProvider = null;
			this._fixedStockpileInventorySetter = null;
			this._stockpileVisualizationUpdater = null;
			this._root.ToggleDisplayStyle(false);
			this._goods.ClearItems();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002329 File Offset: 0x00000529
		public void UpdateFragment()
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000232B File Offset: 0x0000052B
		[OnEvent]
		public void OnUndoStateChanged(UndoStateChangedEvent undoStateChangedEvent)
		{
			if (this._inventory)
			{
				this._goods.UpdateSelectedValue();
				this._amount.SetValueWithoutNotify(this._inventory.TotalAmountInStock);
				this._stockpileVisualizationUpdater.UpdateVisualization();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002368 File Offset: 0x00000568
		private void OnGoodAmountChanged(ChangeEvent<int> changeEvent)
		{
			int amount = Math.Clamp(changeEvent.newValue, 0, this._inventory.Capacity);
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(this._fixedStockpileInventorySetter))
			{
				this._fixedStockpileInventorySetter.SetAmount(amount);
			}
		}

		// Token: 0x04000007 RID: 7
		private readonly EventBus _eventBus;

		// Token: 0x04000008 RID: 8
		private readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		private readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400000A RID: 10
		private readonly EntityChangeRecorderFactory _entityChangeRecorderFactory;

		// Token: 0x0400000B RID: 11
		private FixedStockpileDropdownProvider _fixedStockpileDropdownProvider;

		// Token: 0x0400000C RID: 12
		private FixedStockpileInventorySetter _fixedStockpileInventorySetter;

		// Token: 0x0400000D RID: 13
		private StockpileVisualizationUpdater _stockpileVisualizationUpdater;

		// Token: 0x0400000E RID: 14
		private Inventory _inventory;

		// Token: 0x0400000F RID: 15
		private VisualElement _root;

		// Token: 0x04000010 RID: 16
		private Dropdown _goods;

		// Token: 0x04000011 RID: 17
		private IntegerField _amount;
	}
}
