using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.InventorySystem;
using Timberborn.StatusSystemUI;
using Timberborn.Stockpiles;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000019 RID: 25
	public class StockpileGoodSelectionBox : IInputProcessor
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000074 RID: 116 RVA: 0x000033C4 File Offset: 0x000015C4
		// (remove) Token: 0x06000075 RID: 117 RVA: 0x000033FC File Offset: 0x000015FC
		public event EventHandler SelectionBoxClosed;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003431 File Offset: 0x00001631
		public VisualElement Root { get; }

		// Token: 0x06000077 RID: 119 RVA: 0x00003439 File Offset: 0x00001639
		public StockpileGoodSelectionBox(InputService inputService, StatusListFragment statusListFragment, StockpileGoodSelectionBoxItemsFactory stockpileGoodSelectionBoxItemsFactory, VisualElement root)
		{
			this._inputService = inputService;
			this._statusListFragment = statusListFragment;
			this._stockpileGoodSelectionBoxItemsFactory = stockpileGoodSelectionBoxItemsFactory;
			this.Root = root;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000346C File Offset: 0x0000166C
		public void Initialize()
		{
			this._goodSelection = UQueryExtensions.Q<VisualElement>(this.Root, "GoodSelection", null);
			this.Root.ToggleDisplayStyle(false);
			this.Root.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this._isMouseOverElement = true;
			}, 0);
			this.Root.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this._isMouseOverElement = false;
			}, 0);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034CC File Offset: 0x000016CC
		public bool ProcessInput()
		{
			if (this._inputService.UICancel || (this._inputService.MainMouseButtonDown && this.ShouldProcessInput))
			{
				this.HideGoodSelection();
				return this._inputService.UICancel;
			}
			return false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003503 File Offset: 0x00001703
		public void Update()
		{
			if (this.IsShown)
			{
				this.UpdateRows();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003513 File Offset: 0x00001713
		public void ToggleGoodSelection(Stockpile stockpile)
		{
			if (this.IsShown)
			{
				this.HideGoodSelection();
				return;
			}
			this.ShowGoodSelection(stockpile);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000352B File Offset: 0x0000172B
		public void Hide()
		{
			if (this.IsShown)
			{
				this.HideGoodSelection();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000353B File Offset: 0x0000173B
		public void DisableInput()
		{
			this._ignoreInput = true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003544 File Offset: 0x00001744
		public void EnableInput()
		{
			this._ignoreInput = false;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000354D File Offset: 0x0000174D
		public bool IsShown
		{
			get
			{
				return this.Root.IsDisplayed();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000355A File Offset: 0x0000175A
		public bool ShouldProcessInput
		{
			get
			{
				return !this._isMouseOverElement && !this._ignoreInput;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003570 File Offset: 0x00001770
		public void HideGoodSelection()
		{
			this.Root.ToggleDisplayStyle(false);
			this._inputService.RemoveInputProcessor(this);
			this._goodSelection.Clear();
			this._rows.Clear();
			if (this._singleGoodAllower)
			{
				this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
			}
			this._singleGoodAllower = null;
			EventHandler selectionBoxClosed = this.SelectionBoxClosed;
			if (selectionBoxClosed == null)
			{
				return;
			}
			selectionBoxClosed(this, EventArgs.Empty);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035EC File Offset: 0x000017EC
		public void ShowGoodSelection(Stockpile stockpile)
		{
			this._ignoreInput = true;
			this._isMouseOverElement = false;
			this._singleGoodAllower = stockpile.GetComponent<SingleGoodAllower>();
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			this.AddItems(stockpile);
			this.Root.ToggleDisplayStyle(true);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003649 File Offset: 0x00001849
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.UpdateSelection();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003654 File Offset: 0x00001854
		public void AddItems(Stockpile stockpile)
		{
			IEnumerable<GoodSelectionBoxRow> collection = this._stockpileGoodSelectionBoxItemsFactory.CreateItems(stockpile, new Action<string>(this.SetGood), this._goodSelection);
			this._rows.AddRange(collection);
			this._rows.Last<GoodSelectionBoxRow>().Root.AddToClassList(StockpileGoodSelectionBox.NoMarginClass);
			this.UpdateSelection();
			this.UpdateRows();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036B2 File Offset: 0x000018B2
		public void SetGood(string value)
		{
			this._singleGoodAllower.Allow(value);
			this.HideGoodSelection();
			this._statusListFragment.UpdateFragment();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036D4 File Offset: 0x000018D4
		public void UpdateSelection()
		{
			string selectedGoodId = this._singleGoodAllower.AllowedGood ?? StockpileOptionsService.NothingSelectedLocKey;
			foreach (GoodSelectionBoxRow goodSelectionBoxRow in this._rows)
			{
				goodSelectionBoxRow.UpdateSelectedState(selectedGoodId);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000373C File Offset: 0x0000193C
		public void UpdateRows()
		{
			for (int i = 0; i < this._rows.Count; i++)
			{
				this._rows[i].Update();
			}
		}

		// Token: 0x04000058 RID: 88
		public static readonly string NoMarginClass = "good-selection-box-row--no-margin";

		// Token: 0x0400005B RID: 91
		public readonly InputService _inputService;

		// Token: 0x0400005C RID: 92
		public readonly StatusListFragment _statusListFragment;

		// Token: 0x0400005D RID: 93
		public readonly StockpileGoodSelectionBoxItemsFactory _stockpileGoodSelectionBoxItemsFactory;

		// Token: 0x0400005E RID: 94
		public VisualElement _goodSelection;

		// Token: 0x0400005F RID: 95
		public readonly List<GoodSelectionBoxRow> _rows = new List<GoodSelectionBoxRow>();

		// Token: 0x04000060 RID: 96
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000061 RID: 97
		public bool _isMouseOverElement;

		// Token: 0x04000062 RID: 98
		public bool _ignoreInput;
	}
}
