using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using UnityEngine;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x0200000B RID: 11
	public class GoodStackModel : BaseComponent
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023F0 File Offset: 0x000005F0
		public GoodStackModel(GoodIconVisualizer goodIconVisualizer, IGoodService goodService)
		{
			this._goodIconVisualizer = goodIconVisualizer;
			this._goodService = goodService;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002414 File Offset: 0x00000614
		public void Initialize(GameObject root, GoodStackModelSpec spec)
		{
			this._root = root;
			this._log = this._root.FindChild(spec.LogObjectName);
			this._barrel = this._root.FindChild(spec.BarrelObjectName);
			this._box = this._root.FindChild(spec.BoxObjectName);
			this._bag = this._root.FindChild(spec.BagObjectName);
			this._boxMeshRenderer = this._box.GetComponentInChildren<MeshRenderer>();
			this._barrelMeshRenderer = this._barrel.GetComponentInChildren<MeshRenderer>();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024A8 File Offset: 0x000006A8
		public void UpdateModel(Inventory inventory)
		{
			this._root.SetActive(true);
			foreach (GoodAmount goodAmount in inventory.Stock)
			{
				this._goods.Add(this._goodService.GetGood(goodAmount.GoodId));
			}
			this.ToggleActive(this._log, VisibleContainer.Log);
			this.ToggleActive(this._barrel, VisibleContainer.Barrel);
			this.ToggleActive(this._box, VisibleContainer.Box);
			this.ToggleActive(this._bag, VisibleContainer.Bag);
			this.UpdateIcon(this._boxMeshRenderer, VisibleContainer.Box);
			this.UpdateIcon(this._barrelMeshRenderer, VisibleContainer.Barrel);
			this._goods.Clear();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000257C File Offset: 0x0000077C
		public void ToggleActive(GameObject item, VisibleContainer visibleContainer)
		{
			bool state = this._goods.Any((GoodSpec good) => good.VisibleContainer == visibleContainer);
			GoodStackModel.ToggleActive(item, state);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025B5 File Offset: 0x000007B5
		public static void ToggleActive(GameObject item, bool state)
		{
			item.SetActive(state);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025C0 File Offset: 0x000007C0
		public void UpdateIcon(MeshRenderer meshRenderer, VisibleContainer visibleContainer)
		{
			GoodSpec goodSpec = this._goods.FirstOrDefault((GoodSpec good) => good.VisibleContainer == visibleContainer);
			if (goodSpec != null)
			{
				this._goodIconVisualizer.ShowIcon(meshRenderer.material, goodSpec);
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly GoodIconVisualizer _goodIconVisualizer;

		// Token: 0x04000015 RID: 21
		public readonly IGoodService _goodService;

		// Token: 0x04000016 RID: 22
		public readonly List<GoodSpec> _goods = new List<GoodSpec>();

		// Token: 0x04000017 RID: 23
		public MeshRenderer _boxMeshRenderer;

		// Token: 0x04000018 RID: 24
		public MeshRenderer _barrelMeshRenderer;

		// Token: 0x04000019 RID: 25
		public GameObject _root;

		// Token: 0x0400001A RID: 26
		public GameObject _log;

		// Token: 0x0400001B RID: 27
		public GameObject _barrel;

		// Token: 0x0400001C RID: 28
		public GameObject _box;

		// Token: 0x0400001D RID: 29
		public GameObject _bag;
	}
}
