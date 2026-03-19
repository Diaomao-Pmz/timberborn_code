using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.SliderToggleSystem
{
	// Token: 0x02000006 RID: 6
	public class SliderToggleButtonFactory
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002346 File Offset: 0x00000546
		public SliderToggleButtonFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002364 File Offset: 0x00000564
		public SliderToggleButton Create(VisualElement parent, SliderToggleItem sliderToggleItem)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/SliderToggleButton");
			Image image = UQueryExtensions.Q<Image>(visualElement, "Icon", null);
			if (sliderToggleItem.Icon != null)
			{
				image.sprite = sliderToggleItem.Icon;
			}
			else
			{
				image.AddToClassList(sliderToggleItem.IconClass);
			}
			Button button = UQueryExtensions.Q<Button>(visualElement, "Button", null);
			if (sliderToggleItem.SelectedClass != null)
			{
				button.AddToClassList(sliderToggleItem.SelectedClass);
			}
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				sliderToggleItem.ClickAction();
			}, 0);
			this._tooltipRegistrar.Register(button, () => this.GetTooltip(sliderToggleItem));
			parent.Add(visualElement);
			return new SliderToggleButton(button, sliderToggleItem.StateGetter, sliderToggleItem.ClickAction);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002454 File Offset: 0x00000654
		public TooltipContent GetTooltip(SliderToggleItem sliderToggleItem)
		{
			TooltipContent content = sliderToggleItem.TooltipGetter();
			if (content.VisualElement != null)
			{
				return content;
			}
			bool flag = sliderToggleItem.StateGetter() == SliderToggleState.Active;
			string suffix = flag ? (" " + this._loc.T(SliderToggleButtonFactory.ToggleSelectedLocKey)) : string.Empty;
			return TooltipContent.Create(() => content.BaseText + suffix);
		}

		// Token: 0x04000010 RID: 16
		public static readonly string ToggleSelectedLocKey = "Toggle.Selected";

		// Token: 0x04000011 RID: 17
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000012 RID: 18
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;
	}
}
