using System;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x0200000E RID: 14
	public class ExportThresholdSlider
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000282C File Offset: 0x00000A2C
		public ExportThresholdSlider(ILoc loc, TooltipBlocker tooltipBlocker, GoodDistributionSetting setting, Slider slider, VisualElement tooltip)
		{
			this._loc = loc;
			this._tooltipBlocker = tooltipBlocker;
			this._setting = setting;
			this._slider = slider;
			this._tooltip = tooltip;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000285C File Offset: 0x00000A5C
		public void Initialize()
		{
			this._tooltipLabel = UQueryExtensions.Q<Label>(this._tooltip, "TooltipLabel", null);
			VisualElement visualElement = UQueryExtensions.Q(this._slider, null, BaseSlider<float>.draggerUssClassName);
			visualElement.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), 1);
			visualElement.RegisterCallback<MouseEnterEvent>(new EventCallback<MouseEnterEvent>(this.OnMouseEnter), 0);
			visualElement.RegisterCallback<MouseLeaveEvent>(new EventCallback<MouseLeaveEvent>(this.OnMouseLeave), 0);
			VisualElement visualElement2 = UQueryExtensions.Q(this._slider, null, BaseSlider<float>.dragContainerUssClassName);
			visualElement2.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), 1);
			visualElement2.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), 0);
			this._slider.lowValue = 0f;
			this._slider.highValue = 1f;
			this._slider.value = this._setting.ExportThreshold;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._slider, new EventCallback<ChangeEvent<float>>(this.OnSliderChanged));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000294D File Offset: 0x00000B4D
		public void Update()
		{
			this.UpdateSliderPosition();
			this.UpdateTooltipState();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000295B File Offset: 0x00000B5B
		public void Clear()
		{
			if (this._isTooltipShown)
			{
				this._tooltipBlocker.RemoveBlocker(this);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002971 File Offset: 0x00000B71
		public bool ShouldShowTooltip
		{
			get
			{
				return this._isDragged || this._isHovered;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002983 File Offset: 0x00000B83
		public void OnMouseEnter(MouseEnterEvent evt)
		{
			if (evt.pressedButtons == 0)
			{
				this._isHovered = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002994 File Offset: 0x00000B94
		public void OnMouseLeave(MouseLeaveEvent evt)
		{
			this._isHovered = false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000299D File Offset: 0x00000B9D
		public void OnMouseDown(MouseDownEvent evt)
		{
			if (evt.button == ExportThresholdSlider.DragMouseButton)
			{
				this._isDragged = true;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029B3 File Offset: 0x00000BB3
		public void OnMouseUp(MouseUpEvent evt)
		{
			if (evt.button == ExportThresholdSlider.DragMouseButton)
			{
				this._isDragged = false;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029CC File Offset: 0x00000BCC
		public void OnSliderChanged(ChangeEvent<float> evt)
		{
			float exportThreshold = (float)Math.Round((double)(evt.newValue / ExportThresholdSlider.ExportThresholdSliderScale)) * ExportThresholdSlider.ExportThresholdSliderScale;
			this._setting.SetExportThreshold(exportThreshold);
			this.UpdateSliderPosition();
			this.UpdateTooltipLabel();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A0C File Offset: 0x00000C0C
		public void UpdateTooltipLabel()
		{
			string param = NumberFormatter.FormatAsPercentRounded((double)this._setting.ExportThreshold);
			this._tooltipLabel.text = this._loc.T<string>(ExportThresholdSlider.TooltipLocKey, param);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A47 File Offset: 0x00000C47
		public void UpdateSliderPosition()
		{
			if (Math.Abs(this._slider.value - this._setting.ExportThreshold) > 0.0001f)
			{
				this._slider.SetValueWithoutNotify(this._setting.ExportThreshold);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A82 File Offset: 0x00000C82
		public void UpdateTooltipState()
		{
			if (this.ShouldShowTooltip && !this._isTooltipShown)
			{
				this.ShowTooltip();
				return;
			}
			if (!this.ShouldShowTooltip && this._isTooltipShown)
			{
				this.HideTooltip();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public void ShowTooltip()
		{
			this.UpdateTooltipLabel();
			this._tooltip.ToggleDisplayStyle(true);
			this._isTooltipShown = true;
			this._tooltipBlocker.AddBlocker(this);
			this._slider.AddToClassList(ExportThresholdSlider.HighlightClass);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void HideTooltip()
		{
			this._tooltip.ToggleDisplayStyle(false);
			this._isTooltipShown = false;
			this._tooltipBlocker.RemoveBlocker(this);
			this._slider.RemoveFromClassList(ExportThresholdSlider.HighlightClass);
		}

		// Token: 0x04000023 RID: 35
		public static readonly int DragMouseButton = 0;

		// Token: 0x04000024 RID: 36
		public static readonly float ExportThresholdSliderScale = 0.05f;

		// Token: 0x04000025 RID: 37
		public static readonly string TooltipLocKey = "Distribution.ExportThreshold";

		// Token: 0x04000026 RID: 38
		public static readonly string HighlightClass = "export-threshold-slider--highlighted";

		// Token: 0x04000027 RID: 39
		public readonly ILoc _loc;

		// Token: 0x04000028 RID: 40
		public readonly TooltipBlocker _tooltipBlocker;

		// Token: 0x04000029 RID: 41
		public readonly GoodDistributionSetting _setting;

		// Token: 0x0400002A RID: 42
		public readonly Slider _slider;

		// Token: 0x0400002B RID: 43
		public readonly VisualElement _tooltip;

		// Token: 0x0400002C RID: 44
		public Label _tooltipLabel;

		// Token: 0x0400002D RID: 45
		public bool _isDragged;

		// Token: 0x0400002E RID: 46
		public bool _isHovered;

		// Token: 0x0400002F RID: 47
		public bool _isTooltipShown;
	}
}
