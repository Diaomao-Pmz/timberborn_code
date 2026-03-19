using System;
using System.Collections.Generic;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using Timberborn.Stockpiles;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000023 RID: 35
	public class StockpileOverlay : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000040F0 File Offset: 0x000022F0
		public StockpileOverlay(CameraService cameraService, OverlayGoodSelectionController overlayGoodSelectionController, UISettings uiSettings, Underlay underlay)
		{
			this._cameraService = cameraService;
			this._overlayGoodSelectionController = overlayGoodSelectionController;
			this._uiSettings = uiSettings;
			this._underlay = underlay;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000412B File Offset: 0x0000232B
		public void Load()
		{
			this._cameraService.CameraPositionOrRotationChanged += delegate(object _, EventArgs _)
			{
				this.UpdatePosition();
			};
			this._uiSettings.UIScaleFactorChanged += delegate(object _, SettingChangedEventArgs<float> _)
			{
				this.UpdatePosition();
			};
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000415B File Offset: 0x0000235B
		public void LateUpdateSingleton()
		{
			if (this._isDirty)
			{
				this.UpdatePosition();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000416C File Offset: 0x0000236C
		public StockpileOverlayToggle GetStockpileOverlayToggle()
		{
			StockpileOverlayToggle stockpileOverlayToggle = new StockpileOverlayToggle();
			this._toggles.Add(stockpileOverlayToggle);
			stockpileOverlayToggle.StateChanged += delegate(object _, EventArgs _)
			{
				this.UpdateOverlay();
			};
			return stockpileOverlayToggle;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000419E File Offset: 0x0000239E
		public void Add(VisualElement element, Vector3 anchor)
		{
			if (this._items.TryAdd(element, anchor) && this._enabled)
			{
				this._underlay.Add(element);
				this._isDirty = true;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000041CA File Offset: 0x000023CA
		public void Remove(VisualElement element)
		{
			if (this._items.Remove(element) && this._enabled)
			{
				this._underlay.Remove(element);
				if (element == this._overlayGoodSelectionController.SelectedItem)
				{
					this._overlayGoodSelectionController.HideBox();
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004207 File Offset: 0x00002407
		public void ToggleGoodSelection(Stockpile stockpile, VisualElement item)
		{
			if (this._enabled)
			{
				this._overlayGoodSelectionController.ToggleGoodSelection(stockpile, item);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004220 File Offset: 0x00002420
		public void UpdatePosition()
		{
			if (this._enabled)
			{
				foreach (KeyValuePair<VisualElement, Vector3> keyValuePair in this._items)
				{
					VisualElement visualElement;
					Vector3 vector;
					keyValuePair.Deconstruct(ref visualElement, ref vector);
					VisualElement item = visualElement;
					Vector3 anchor = vector;
					this.UpdatePosition(item, anchor);
				}
				this._overlayGoodSelectionController.HideBox();
			}
			this._isDirty = false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000042A0 File Offset: 0x000024A0
		public void UpdateOverlay()
		{
			bool flag;
			if (this._toggles.FastAny((StockpileOverlayToggle toggle) => toggle.Enabled))
			{
				flag = this._toggles.FastAll((StockpileOverlayToggle toggle) => !toggle.Hidden);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2 && !this._enabled)
			{
				this.Enable();
				return;
			}
			if (!flag2 && this._enabled)
			{
				this.Disable();
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000432C File Offset: 0x0000252C
		public void Enable()
		{
			this._enabled = true;
			foreach (VisualElement element in this._items.Keys)
			{
				this._underlay.Add(element);
			}
			this._isDirty = true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004398 File Offset: 0x00002598
		public void Disable()
		{
			this._enabled = false;
			foreach (VisualElement element in this._items.Keys)
			{
				this._underlay.Remove(element);
			}
			this._overlayGoodSelectionController.HideBox();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004408 File Offset: 0x00002608
		public void UpdatePosition(VisualElement item, Vector3 anchor)
		{
			VisualElement root = this._underlay.Root;
			if (item.panel != null)
			{
				bool flag = this._cameraService.IsInFront(anchor);
				item.ToggleDisplayStyle(flag);
				if (flag)
				{
					Vector3 vector = this._cameraService.WorldSpaceToPanelSpace(root, anchor);
					item.style.translate = new Vector2(vector.x - root.layout.width / 2f, vector.y - root.layout.height / 2f);
				}
			}
		}

		// Token: 0x04000090 RID: 144
		public readonly CameraService _cameraService;

		// Token: 0x04000091 RID: 145
		public readonly OverlayGoodSelectionController _overlayGoodSelectionController;

		// Token: 0x04000092 RID: 146
		public readonly UISettings _uiSettings;

		// Token: 0x04000093 RID: 147
		public readonly Underlay _underlay;

		// Token: 0x04000094 RID: 148
		public readonly Dictionary<VisualElement, Vector3> _items = new Dictionary<VisualElement, Vector3>();

		// Token: 0x04000095 RID: 149
		public readonly List<StockpileOverlayToggle> _toggles = new List<StockpileOverlayToggle>();

		// Token: 0x04000096 RID: 150
		public bool _enabled;

		// Token: 0x04000097 RID: 151
		public bool _isDirty;
	}
}
