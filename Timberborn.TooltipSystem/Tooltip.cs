using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.TooltipSystem
{
	// Token: 0x02000006 RID: 6
	public class Tooltip : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002232 File Offset: 0x00000432
		public Tooltip(TooltipBlocker tooltipBlocker, TooltipContainer tooltipContainer, VisualElementLoader visualElementLoader)
		{
			this._tooltipBlocker = tooltipBlocker;
			this._tooltipContainer = tooltipContainer;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002250 File Offset: 0x00000450
		public void Load()
		{
			this._tooltipRoot = this._visualElementLoader.LoadVisualElement("Core/Tooltip");
			this._tooltipLabel = UQueryExtensions.Q<Label>(this._tooltipRoot, "Description", null);
			this._keyBindingLabel = UQueryExtensions.Q<Label>(this._tooltipRoot, "KeyBinding", null);
			this._keyBindingRoot = UQueryExtensions.Q<VisualElement>(this._tooltipRoot, "KeyBindingRoot", null);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B8 File Offset: 0x000004B8
		public void UpdateSingleton()
		{
			float unscaledTime = Time.unscaledTime;
			bool flag = this._enabled && this._tooltipBlocker.IsUnblocked && unscaledTime > this._showTimestamp && unscaledTime < this._hideTimestamp && (!this._tooltipContent.UpdatableContent || this._tooltipContent.HasContent());
			if (!this._wasVisibleLastUpdate && flag)
			{
				this._tooltipContainer.Show(this._tooltipRoot);
			}
			else if (this._wasVisibleLastUpdate && !flag)
			{
				this._tooltipContainer.Clear();
			}
			if (flag && this._tooltipContent.UpdatableContent)
			{
				this.UpdateTooltipContent();
			}
			this._wasVisibleLastUpdate = flag;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002364 File Offset: 0x00000564
		public void RegisterTooltip(VisualElement visualElement, Func<TooltipContent> tooltipContentGetter)
		{
			visualElement.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.Enable(tooltipContentGetter());
			}, 0);
			visualElement.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.Disable();
			}, 0);
			visualElement.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnPointerMove), 0);
			visualElement.RegisterCallback<MouseUpEvent>(delegate(MouseUpEvent _)
			{
				this.Disable();
			}, 0);
			visualElement.RegisterCallback<DetachFromPanelEvent>(delegate(DetachFromPanelEvent _)
			{
				this.Disable();
			}, 0);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023E4 File Offset: 0x000005E4
		public void Enable(TooltipContent tooltipContent)
		{
			if (tooltipContent.HasContent() || tooltipContent.UpdatableContent)
			{
				this._tooltipContent = tooltipContent;
				this.UpdateTooltipContent();
				this._enabled = true;
				if (!this._wasVisibleLastUpdate)
				{
					this.UpdateShowTimestamp();
					this.UpdateHideTimestamp();
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002420 File Offset: 0x00000620
		public void UpdateTooltipContent()
		{
			this._tooltipLabel.Clear();
			this._tooltipLabel.text = this._tooltipContent.BaseText;
			this._tooltipLabel.Add(this._tooltipContent.VisualElement);
			string text;
			if (this._tooltipContent.TryGetKeyBinding(out text))
			{
				this._keyBindingLabel.text = text;
				this._keyBindingRoot.ToggleDisplayStyle(true);
			}
			else
			{
				this._keyBindingRoot.ToggleDisplayStyle(false);
			}
			Tooltip.IgnorePicking(this._tooltipLabel);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024A4 File Offset: 0x000006A4
		public static void IgnorePicking(VisualElement visualElement)
		{
			visualElement.pickingMode = 1;
			foreach (VisualElement visualElement2 in visualElement.Children())
			{
				Tooltip.IgnorePicking(visualElement2);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024F8 File Offset: 0x000006F8
		public void Disable()
		{
			this._enabled = false;
			this._hideTimestamp = Time.unscaledTime + Tooltip.NextTooltipTiming;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002514 File Offset: 0x00000714
		public void OnPointerMove(MouseMoveEvent mouseMoveEvent)
		{
			if (this._enabled)
			{
				if (this.ShouldResetTooltip(mouseMoveEvent.mouseDelta.sqrMagnitude))
				{
					this.UpdateShowTimestamp();
				}
				this.UpdateHideTimestamp();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000254B File Offset: 0x0000074B
		public bool ShouldResetTooltip(float mouseDistanceSqr)
		{
			return (mouseDistanceSqr > Tooltip.HideTooltipMinDistanceSqr && this._tooltipContent.Delay > 0f) || !this._wasVisibleLastUpdate;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002572 File Offset: 0x00000772
		public void UpdateShowTimestamp()
		{
			this._showTimestamp = Time.unscaledTime + this._tooltipContent.Delay;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000258B File Offset: 0x0000078B
		public void UpdateHideTimestamp()
		{
			this._hideTimestamp = this._showTimestamp + Tooltip.TooltipVisibilityTime;
		}

		// Token: 0x04000009 RID: 9
		public static readonly float HideTooltipMinDistanceSqr = 2f;

		// Token: 0x0400000A RID: 10
		public static readonly float NextTooltipTiming = 0.2f;

		// Token: 0x0400000B RID: 11
		public static readonly float TooltipVisibilityTime = 15f;

		// Token: 0x0400000C RID: 12
		public readonly TooltipBlocker _tooltipBlocker;

		// Token: 0x0400000D RID: 13
		public readonly TooltipContainer _tooltipContainer;

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public TooltipContent _tooltipContent;

		// Token: 0x04000010 RID: 16
		public VisualElement _tooltipRoot;

		// Token: 0x04000011 RID: 17
		public VisualElement _keyBindingRoot;

		// Token: 0x04000012 RID: 18
		public Label _tooltipLabel;

		// Token: 0x04000013 RID: 19
		public Label _keyBindingLabel;

		// Token: 0x04000014 RID: 20
		public bool _wasVisibleLastUpdate;

		// Token: 0x04000015 RID: 21
		public bool _enabled;

		// Token: 0x04000016 RID: 22
		public float _showTimestamp;

		// Token: 0x04000017 RID: 23
		public float _hideTimestamp;
	}
}
