using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.TopBarSystem
{
	// Token: 0x0200000D RID: 13
	public class TopBarPanel : IUpdatableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000273C File Offset: 0x0000093C
		public TopBarPanel(VisualElementLoader visualElementLoader, UILayout uiLayout, GoodsGroupSpecService goodsGroupSpecService, IGoodService goodService, TopBarCounterFactory topBarCounterFactory, DistrictContextService districtContextService, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._goodsGroupSpecService = goodsGroupSpecService;
			this._goodService = goodService;
			this._topBarCounterFactory = topBarCounterFactory;
			this._districtContextService = districtContextService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000278F File Offset: 0x0000098F
		public void UpdateSingleton()
		{
			this.UpdatePanel();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002798 File Offset: 0x00000998
		public void PostLoad()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/TopBar/TopBarPanel");
			foreach (GoodGroupSpec goodGroupSpec in this._goodsGroupSpecService.GoodGroupSpecs)
			{
				this._counters.Add(this.CreateCounter(goodGroupSpec));
			}
			this.UpdatePanel();
			this._eventBus.Register(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002828 File Offset: 0x00000A28
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopBar(this._root);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000283C File Offset: 0x00000A3C
		public ITopBarCounter CreateCounter(GoodGroupSpec goodGroupSpec)
		{
			if (goodGroupSpec.SingleResourceGroup)
			{
				string goodId = this._goodService.Goods.Single((string good) => this.IsGroupGood(goodGroupSpec, good));
				return this._topBarCounterFactory.CreateSimpleCounter(goodGroupSpec, goodId, this._root);
			}
			IEnumerable<string> goods = from good in this._goodService.Goods
			where this.IsGroupGood(goodGroupSpec, good)
			select good;
			return this._topBarCounterFactory.CreateExtendableCounter(goodGroupSpec, goods, this._root);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028DF File Offset: 0x00000ADF
		public void UpdatePanel()
		{
			this.UpdateCounters();
			this._root.EnableInClassList(TopBarPanel.PanelDistrictClass, this._districtContextService.SelectedDistrict);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002908 File Offset: 0x00000B08
		public void UpdateCounters()
		{
			foreach (ITopBarCounter topBarCounter in this._counters)
			{
				topBarCounter.UpdateValues();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002958 File Offset: 0x00000B58
		public bool IsGroupGood(GoodGroupSpec goodGroupSpec, string good)
		{
			return this._goodService.GetGood(good).GoodGroupId == goodGroupSpec.Id;
		}

		// Token: 0x0400002C RID: 44
		public static readonly string PanelDistrictClass = "panel--district";

		// Token: 0x0400002D RID: 45
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002E RID: 46
		public readonly UILayout _uiLayout;

		// Token: 0x0400002F RID: 47
		public readonly GoodsGroupSpecService _goodsGroupSpecService;

		// Token: 0x04000030 RID: 48
		public readonly IGoodService _goodService;

		// Token: 0x04000031 RID: 49
		public readonly TopBarCounterFactory _topBarCounterFactory;

		// Token: 0x04000032 RID: 50
		public readonly DistrictContextService _districtContextService;

		// Token: 0x04000033 RID: 51
		public readonly EventBus _eventBus;

		// Token: 0x04000034 RID: 52
		public VisualElement _root;

		// Token: 0x04000035 RID: 53
		public readonly List<ITopBarCounter> _counters = new List<ITopBarCounter>();
	}
}
