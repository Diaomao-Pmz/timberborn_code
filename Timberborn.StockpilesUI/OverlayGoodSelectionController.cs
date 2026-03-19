using System;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;
using Timberborn.TickSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000010 RID: 16
	public class OverlayGoodSelectionController : ILoadableSingleton, ILateUpdatableSingleton, ITickableSingleton
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B01 File Offset: 0x00000D01
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002B09 File Offset: 0x00000D09
		public VisualElement SelectedItem { get; private set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002B12 File Offset: 0x00000D12
		public OverlayGoodSelectionController(UILayout uiLayout, StockpileGoodSelectionBoxFactory stockpileGoodSelectionBoxFactory)
		{
			this._uiLayout = uiLayout;
			this._stockpileGoodSelectionBoxFactory = stockpileGoodSelectionBoxFactory;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B28 File Offset: 0x00000D28
		public void Load()
		{
			this._stockpileGoodSelectionBox = this._stockpileGoodSelectionBoxFactory.Create();
			this._stockpileGoodSelectionBox.SelectionBoxClosed += this.OnSelectionBoxClosed;
			this._uiLayout.AddAbsoluteItem(this._stockpileGoodSelectionBox.Root);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B68 File Offset: 0x00000D68
		public void Tick()
		{
			this._stockpileGoodSelectionBox.Update();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B75 File Offset: 0x00000D75
		public void LateUpdateSingleton()
		{
			if (this._isDirty)
			{
				this.UpdatePosition();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B85 File Offset: 0x00000D85
		public void ToggleGoodSelection(Stockpile stockpile, VisualElement item)
		{
			this.SetSelectedItem(item);
			this._stockpileGoodSelectionBox.ToggleGoodSelection(stockpile);
			this._stockpileGoodSelectionBox.Root.visible = false;
			this._isDirty = true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BB2 File Offset: 0x00000DB2
		public void HideBox()
		{
			this.ClearSelectedItem();
			this._stockpileGoodSelectionBox.Hide();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public IResolvedStyle BoxResolvedStyle
		{
			get
			{
				return this._stockpileGoodSelectionBox.Root.resolvedStyle;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BD7 File Offset: 0x00000DD7
		public void OnSelectionBoxClosed(object sender, EventArgs e)
		{
			this.ClearSelectedItem();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BDF File Offset: 0x00000DDF
		public void SetSelectedItem(VisualElement selectedItem)
		{
			this.ClearSelectedItem();
			this.SelectedItem = selectedItem;
			this.SelectedItem.RegisterCallback<MouseEnterEvent>(new EventCallback<MouseEnterEvent>(this.OnMouseEnterSelectedItem), 0);
			this.SelectedItem.RegisterCallback<MouseLeaveEvent>(new EventCallback<MouseLeaveEvent>(this.OnMouseLeaveSelectedItem), 0);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C20 File Offset: 0x00000E20
		public void ClearSelectedItem()
		{
			if (this.SelectedItem != null)
			{
				this.SelectedItem.UnregisterCallback<MouseEnterEvent>(new EventCallback<MouseEnterEvent>(this.OnMouseEnterSelectedItem), 0);
				this.SelectedItem.UnregisterCallback<MouseLeaveEvent>(new EventCallback<MouseLeaveEvent>(this.OnMouseLeaveSelectedItem), 0);
				this.SelectedItem = null;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C6C File Offset: 0x00000E6C
		public void OnMouseEnterSelectedItem(MouseEnterEvent evt)
		{
			this._stockpileGoodSelectionBox.DisableInput();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C79 File Offset: 0x00000E79
		public void OnMouseLeaveSelectedItem(MouseLeaveEvent evt)
		{
			this._stockpileGoodSelectionBox.EnableInput();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C88 File Offset: 0x00000E88
		public void UpdatePosition()
		{
			if (this.SelectedItem != null)
			{
				this._stockpileGoodSelectionBox.Root.style.left = this.CalculateHorizontalPosition();
				this._stockpileGoodSelectionBox.Root.style.top = this.CalculateVerticalPosition();
				this._stockpileGoodSelectionBox.Root.visible = true;
			}
			this._isDirty = false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public float CalculateHorizontalPosition()
		{
			float width = this.BoxResolvedStyle.width;
			float x = this.SelectedItem.worldBound.x;
			float num = x - width - OverlayGoodSelectionController.HorizontalBoxSpacing;
			if (num > 0f)
			{
				return num;
			}
			return x + this.SelectedItem.resolvedStyle.width + OverlayGoodSelectionController.HorizontalBoxSpacing;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D54 File Offset: 0x00000F54
		public float CalculateVerticalPosition()
		{
			float height = this._stockpileGoodSelectionBox.Root.parent.resolvedStyle.height;
			float height2 = this.BoxResolvedStyle.height;
			return Math.Min(this.SelectedItem.worldBound.y, height - height2 - OverlayGoodSelectionController.BottomVerticalOffset);
		}

		// Token: 0x04000031 RID: 49
		public static readonly float BottomVerticalOffset = 5f;

		// Token: 0x04000032 RID: 50
		public static readonly float HorizontalBoxSpacing = 3f;

		// Token: 0x04000034 RID: 52
		public readonly UILayout _uiLayout;

		// Token: 0x04000035 RID: 53
		public readonly StockpileGoodSelectionBoxFactory _stockpileGoodSelectionBoxFactory;

		// Token: 0x04000036 RID: 54
		public StockpileGoodSelectionBox _stockpileGoodSelectionBox;

		// Token: 0x04000037 RID: 55
		public bool _isDirty;
	}
}
