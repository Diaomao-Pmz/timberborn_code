using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.TemplateAttachmentSystem;
using UnityEngine;

namespace Timberborn.Carrying
{
	// Token: 0x02000012 RID: 18
	public class GoodCarrierModel : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000030BD File Offset: 0x000012BD
		public GoodCarrierModel(GoodIconVisualizer goodIconVisualizer, IGoodService goodService)
		{
			this._goodIconVisualizer = goodIconVisualizer;
			this._goodService = goodService;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030E9 File Offset: 0x000012E9
		public void Awake()
		{
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._backpackCarrier = base.GetComponent<BackpackCarrier>();
			this._templateAttachments = base.GetComponent<TemplateAttachments>();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000310F File Offset: 0x0000130F
		public void InitializeEntity()
		{
			this.InitializeItems();
			this.UpdateItemsVisibility();
			this._backpackCarrier.BackpackChanged += this.OnBackpackChanged;
			this._goodCarrier.CarriedGoodsChanged += this.OnCarriedGoodsChanged;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000314C File Offset: 0x0000134C
		public void InitializeItems()
		{
			GoodCarrierModelSpec component = base.GetComponent<GoodCarrierModelSpec>();
			TemplateAttachment orCreateAttachment = this._templateAttachments.GetOrCreateAttachment(component.CarriedInHandsAttachmentName);
			TemplateAttachment orCreateAttachment2 = this._templateAttachments.GetOrCreateAttachment(component.BackpackAttachmentName);
			this._carriedInHandsToggle = orCreateAttachment.GetVisibilityToggle();
			this._backpackToggle = orCreateAttachment2.GetVisibilityToggle();
			this._itemsInHands.AddRange(this.GetItems(orCreateAttachment));
			this._itemsInBackpack.AddRange(this.GetItems(orCreateAttachment2));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031C0 File Offset: 0x000013C0
		public IEnumerable<CarriedItem> GetItems(TemplateAttachment templateAttachment)
		{
			Transform root = templateAttachment.Transform.GetChild(0);
			int num;
			for (int i = 0; i < root.childCount; i = num)
			{
				CarriedItem carriedItem = CarriedItem.CreateLinkedToObject(root.GetChild(i).gameObject, this._goodIconVisualizer);
				carriedItem.Hide();
				yield return carriedItem;
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000031D7 File Offset: 0x000013D7
		public void OnBackpackChanged(object sender, EventArgs e)
		{
			this.UpdateItemsVisibility();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000031D7 File Offset: 0x000013D7
		public void OnCarriedGoodsChanged(object sender, CarriedGoodsChangedEventArgs e)
		{
			this.UpdateItemsVisibility();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000031E0 File Offset: 0x000013E0
		public void UpdateItemsVisibility()
		{
			if (this._backpackCarrier.IsBackpackEnabled)
			{
				this._carriedInHandsToggle.Hide();
				this._backpackToggle.Show();
				this.UpdateItems(this._itemsInBackpack);
				return;
			}
			this._backpackToggle.Hide();
			this._carriedInHandsToggle.Show();
			this.UpdateItems(this._itemsInHands);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003240 File Offset: 0x00001440
		public void UpdateItems(IEnumerable<CarriedItem> items)
		{
			GoodSpec goodSpec = this._goodCarrier.IsCarrying ? this._goodService.GetGood(this._goodCarrier.CarriedGoods.GoodId) : null;
			VisibleContainer visibleContainer = (goodSpec != null) ? goodSpec.VisibleContainer : VisibleContainer.None;
			foreach (CarriedItem carriedItem in items)
			{
				if (carriedItem.VisibleContainer == visibleContainer)
				{
					GoodCarrierModel.ShowItem(carriedItem, goodSpec);
				}
				else
				{
					carriedItem.Hide();
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032DC File Offset: 0x000014DC
		public static void ShowItem(CarriedItem item, GoodSpec goodSpec)
		{
			if (goodSpec != null)
			{
				item.Show(goodSpec);
				return;
			}
			item.Show();
		}

		// Token: 0x04000035 RID: 53
		public readonly GoodIconVisualizer _goodIconVisualizer;

		// Token: 0x04000036 RID: 54
		public readonly IGoodService _goodService;

		// Token: 0x04000037 RID: 55
		public GoodCarrier _goodCarrier;

		// Token: 0x04000038 RID: 56
		public BackpackCarrier _backpackCarrier;

		// Token: 0x04000039 RID: 57
		public TemplateAttachments _templateAttachments;

		// Token: 0x0400003A RID: 58
		public TemplateAttachmentVisibilityToggle _carriedInHandsToggle;

		// Token: 0x0400003B RID: 59
		public TemplateAttachmentVisibilityToggle _backpackToggle;

		// Token: 0x0400003C RID: 60
		public readonly List<CarriedItem> _itemsInHands = new List<CarriedItem>();

		// Token: 0x0400003D RID: 61
		public readonly List<CarriedItem> _itemsInBackpack = new List<CarriedItem>();
	}
}
