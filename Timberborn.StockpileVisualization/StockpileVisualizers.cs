using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Persistence;
using Timberborn.SelectionSystem;
using Timberborn.Stockpiles;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000021 RID: 33
	public class StockpileVisualizers : BaseComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004841 File Offset: 0x00002A41
		public StockpileVisualizers(IGoodService goodService, SerializedGoodValueSerializer serializedGoodValueSerializer)
		{
			this._goodService = goodService;
			this._serializedGoodValueSerializer = serializedGoodValueSerializer;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004862 File Offset: 0x00002A62
		public void Awake()
		{
			base.GetComponents<IStockpileVisualizer>(this._visualizers);
			this._inventory = base.GetComponent<Stockpile>().Inventory;
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._highlightableObject = base.GetComponent<HighlightableObject>();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000489C File Offset: 0x00002A9C
		public void OnEnterFinishedState()
		{
			this._inventory.InventoryChanged += this.OnInventoryChanged;
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			if (this._singleGoodAllower.HasAllowedGood)
			{
				this.SetAwaitingOrCurrentVisualizer(this._singleGoodAllower.AllowedGood);
			}
			if (this._currentGoodId != null)
			{
				this.SetCurrentVisualizer(this._currentGoodId);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004909 File Offset: 0x00002B09
		public void OnExitFinishedState()
		{
			this._inventory.InventoryChanged -= this.OnInventoryChanged;
			this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004939 File Offset: 0x00002B39
		public void Save(IEntitySaver entitySaver)
		{
			if (this._currentGoodId != null)
			{
				entitySaver.GetComponent(StockpileVisualizers.StockpileVisualizersKey).Set<SerializedGood>(StockpileVisualizers.CurrentGoodKey, new SerializedGood(this._currentGoodId), this._serializedGoodValueSerializer);
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000496C File Offset: 0x00002B6C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			SerializedGood serializedGood;
			if (entityLoader.TryGetComponent(StockpileVisualizers.StockpileVisualizersKey, out objectLoader) && objectLoader.GetObsoletable<SerializedGood>(StockpileVisualizers.CurrentGoodKey, this._serializedGoodValueSerializer, out serializedGood))
			{
				this._currentGoodId = serializedGood.Id;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000049AC File Offset: 0x00002BAC
		public void SetCurrentVisualizer(string goodId)
		{
			this._currentGoodId = goodId;
			GoodSpec good = this._goodService.GetGood(goodId);
			IStockpileVisualizer currentVisualizer = this._currentVisualizer;
			if (currentVisualizer != null)
			{
				currentVisualizer.Clear();
			}
			this._currentVisualizer = this.GetVisualizer(good);
			if (this._currentVisualizer != null)
			{
				this._currentVisualizer.Initialize(good, this._inventory.Capacity);
				this._currentVisualizer.UpdateAmount(this._inventory.TotalAmountInStock);
			}
			this._highlightableObject.UpdateColorAndHighlight();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004A2C File Offset: 0x00002C2C
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			IStockpileVisualizer currentVisualizer = this._currentVisualizer;
			if (currentVisualizer != null)
			{
				currentVisualizer.UpdateAmount(this._inventory.TotalAmountInStock);
			}
			if (!string.IsNullOrEmpty(this._awaitingGoodId) && this._inventory.UnwantedStockAmount() == 0)
			{
				this.ResetAwaitingAndSetCurrentVisualizer(this._awaitingGoodId);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A7B File Offset: 0x00002C7B
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.SetAwaitingOrCurrentVisualizer(e.GoodId);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004A8A File Offset: 0x00002C8A
		public void SetAwaitingOrCurrentVisualizer(string goodId)
		{
			if (this._inventory.UnwantedStockAmount() > 0)
			{
				this._awaitingGoodId = goodId;
				return;
			}
			this.ResetAwaitingAndSetCurrentVisualizer(goodId);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004AA9 File Offset: 0x00002CA9
		public void ResetAwaitingAndSetCurrentVisualizer(string goodId)
		{
			this._awaitingGoodId = null;
			if (this._currentGoodId != goodId)
			{
				this.SetCurrentVisualizer(goodId);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public IStockpileVisualizer GetVisualizer(GoodSpec goodSpec)
		{
			foreach (IStockpileVisualizer stockpileVisualizer in this._visualizers)
			{
				if (stockpileVisualizer.CanVisualize(goodSpec.StockpileVisualization))
				{
					return stockpileVisualizer;
				}
			}
			Debug.LogWarning("Unable to visualize " + goodSpec.Id + " in " + base.Name);
			return null;
		}

		// Token: 0x0400007B RID: 123
		public static readonly ComponentKey StockpileVisualizersKey = new ComponentKey("StockpileVisualizers");

		// Token: 0x0400007C RID: 124
		public static readonly PropertyKey<SerializedGood> CurrentGoodKey = new PropertyKey<SerializedGood>("CurrentGood");

		// Token: 0x0400007D RID: 125
		public readonly IGoodService _goodService;

		// Token: 0x0400007E RID: 126
		public readonly SerializedGoodValueSerializer _serializedGoodValueSerializer;

		// Token: 0x0400007F RID: 127
		public readonly List<IStockpileVisualizer> _visualizers = new List<IStockpileVisualizer>();

		// Token: 0x04000080 RID: 128
		public Inventory _inventory;

		// Token: 0x04000081 RID: 129
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000082 RID: 130
		public HighlightableObject _highlightableObject;

		// Token: 0x04000083 RID: 131
		public IStockpileVisualizer _currentVisualizer;

		// Token: 0x04000084 RID: 132
		public string _currentGoodId;

		// Token: 0x04000085 RID: 133
		public string _awaitingGoodId;
	}
}
