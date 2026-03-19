using System;
using Timberborn.Goods;
using UnityEngine;

namespace Timberborn.Carrying
{
	// Token: 0x02000009 RID: 9
	public class CarriedItem
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021C8 File Offset: 0x000003C8
		public VisibleContainer VisibleContainer { get; }

		// Token: 0x06000012 RID: 18 RVA: 0x000021D0 File Offset: 0x000003D0
		public CarriedItem(GameObject itemObject, GoodIconVisualizer goodIconVisualizer, VisibleContainer visibleContainer)
		{
			this._itemObject = itemObject;
			this._goodIconVisualizer = goodIconVisualizer;
			this._meshRenderer = this._itemObject.GetComponent<MeshRenderer>();
			this.VisibleContainer = visibleContainer;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002200 File Offset: 0x00000400
		public static CarriedItem CreateLinkedToObject(GameObject itemObject, GoodIconVisualizer goodIconVisualizer)
		{
			VisibleContainer visibleContainer = CarriedItem.ParseGoodContainerFromName(itemObject);
			return new CarriedItem(itemObject, goodIconVisualizer, visibleContainer);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000221C File Offset: 0x0000041C
		public void Show(GoodSpec goodSpec)
		{
			this.Show();
			this.UpdateProperties(goodSpec);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000222B File Offset: 0x0000042B
		public void Show()
		{
			this.SetVisibility(true);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002234 File Offset: 0x00000434
		public void Hide()
		{
			this.SetVisibility(false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000223D File Offset: 0x0000043D
		public void SetVisibility(bool show)
		{
			this._itemObject.SetActive(show);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000224B File Offset: 0x0000044B
		public void UpdateProperties(GoodSpec goodSpec)
		{
			this._goodIconVisualizer.ShowIcon(this._meshRenderer.material, goodSpec);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002264 File Offset: 0x00000464
		public static VisibleContainer ParseGoodContainerFromName(Object itemObject)
		{
			string name = itemObject.name;
			int num = name.LastIndexOf(CarriedItem.NameSeparator, StringComparison.Ordinal);
			if (num < 0)
			{
				throw new ArgumentException("Invalid carried object name: " + name);
			}
			string value = name.Substring(num + CarriedItem.NameSeparator.Length);
			return (VisibleContainer)Enum.Parse(typeof(VisibleContainer), value);
		}

		// Token: 0x0400000B RID: 11
		public static readonly string NameSeparator = ".";

		// Token: 0x0400000D RID: 13
		public readonly GameObject _itemObject;

		// Token: 0x0400000E RID: 14
		public readonly MeshRenderer _meshRenderer;

		// Token: 0x0400000F RID: 15
		public readonly GoodIconVisualizer _goodIconVisualizer;
	}
}
