using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Carrying;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.CarryingUI
{
	// Token: 0x02000006 RID: 6
	public class GoodCarrierFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000210D File Offset: 0x0000030D
		public GoodCarrierFragment(VisualElementLoader visualElementLoader, GoodDescriber goodDescriber, ILoc loc, IGoodService goodService)
		{
			this._visualElementLoader = visualElementLoader;
			this._goodDescriber = goodDescriber;
			this._loc = loc;
			this._goodService = goodService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/GoodCarrierFragment");
			this._carryText = UQueryExtensions.Q<Label>(this._root, "GoodCarrierFragment", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000380
		public void ShowFragment(BaseComponent entity)
		{
			this._goodCarrier = entity.GetComponent<GoodCarrier>();
			this._contaminable = entity.GetComponent<Contaminable>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219A File Offset: 0x0000039A
		public void ClearFragment()
		{
			this._root.ToggleDisplayStyle(false);
			this._goodCarrier = null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B0 File Offset: 0x000003B0
		public void UpdateFragment()
		{
			if (this._goodCarrier)
			{
				GoodAmount carriedGoods = this._goodCarrier.CarriedGoods;
				int liftingCapacity = this._goodCarrier.LiftingCapacity;
				this._root.ToggleDisplayStyle(true);
				if (carriedGoods.Amount > 0)
				{
					int weight = this._goodService.GetGood(carriedGoods.GoodId).Weight;
					int param = carriedGoods.Amount * weight;
					string param2 = this._goodDescriber.Describe(carriedGoods);
					this._carryText.text = this._loc.T<string, int, int>(GoodCarrierFragment.CarryLocKey, param2, param, liftingCapacity);
					return;
				}
				Contaminable contaminable = this._contaminable;
				if (contaminable != null && contaminable.IsContaminated)
				{
					this._root.ToggleDisplayStyle(false);
					return;
				}
				string param3 = this._loc.T(GoodCarrierFragment.NothingLocKey);
				this._carryText.text = this._loc.T<string, int, int>(GoodCarrierFragment.CarryLocKey, param3, 0, liftingCapacity);
			}
		}

		// Token: 0x04000007 RID: 7
		public static readonly string CarryLocKey = "Carrying.Carry";

		// Token: 0x04000008 RID: 8
		public static readonly string NothingLocKey = "Carrying.Nothing";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public readonly IGoodService _goodService;

		// Token: 0x0400000D RID: 13
		public GoodCarrier _goodCarrier;

		// Token: 0x0400000E RID: 14
		public Contaminable _contaminable;

		// Token: 0x0400000F RID: 15
		public Label _carryText;

		// Token: 0x04000010 RID: 16
		public VisualElement _root;
	}
}
