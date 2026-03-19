using System;
using System.Collections.Generic;
using Timberborn.AssetSystem;
using Timberborn.BottomBarSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.Modding;
using Timberborn.Versioning;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.Options
{
	// Token: 0x02000007 RID: 7
	public class ShowOptionsButton : IBottomBarElementsProvider
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000210C File Offset: 0x0000030C
		public ShowOptionsButton(VisualElementLoader visualElementLoader, IOptionsBox optionsBox, IAssetLoader assetLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._optionsBox = optionsBox;
			this._assetLoader = assetLoader;
			this._loc = loc;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002131 File Offset: 0x00000331
		public IEnumerable<BottomBarElement> GetElements()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/BottomBar/GrouplessToolButton");
			visualElement.AddToClassList("bottom-bar-button--red");
			Sprite sprite = this._assetLoader.Load<Sprite>("Sprites/BottomBar/Options");
			UQueryExtensions.Q<VisualElement>(visualElement, "ToolImage", null).style.backgroundImage = new StyleBackground(sprite);
			Button button = UQueryExtensions.Q<Button>(visualElement, "ToolButton", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._optionsBox.Show();
			}, 0);
			UQueryExtensions.Q<TextElement>(button, "BottomText", null).text = (ModdedState.IsModded ? ("-" + GameVersions.CurrentVersion.Numeric + "-") : GameVersions.CurrentVersion.Numeric);
			this._tooltip = UQueryExtensions.Q<Label>(button, "Tooltip", null);
			this._tooltip.ToggleDisplayStyle(false);
			this._tooltip.text = this._loc.T(ShowOptionsButton.OptionsTooltipLocKey);
			visualElement.RegisterCallback<MouseOverEvent>(new EventCallback<MouseOverEvent>(this.ShowTooltip), 0);
			visualElement.RegisterCallback<MouseOutEvent>(new EventCallback<MouseOutEvent>(this.HideTooltip), 0);
			yield return BottomBarElement.CreateSingleLevel(visualElement);
			yield break;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002141 File Offset: 0x00000341
		public void ShowTooltip(MouseOverEvent mouseOverEvent)
		{
			this._tooltip.ToggleDisplayStyle(true);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214F File Offset: 0x0000034F
		public void HideTooltip(MouseOutEvent mouseOutEvent)
		{
			Label tooltip = this._tooltip;
			if (tooltip == null)
			{
				return;
			}
			tooltip.ToggleDisplayStyle(false);
		}

		// Token: 0x04000007 RID: 7
		public static readonly string OptionsTooltipLocKey = "Tool.Options.Tooltip";

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly IOptionsBox _optionsBox;

		// Token: 0x0400000A RID: 10
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public Label _tooltip;
	}
}
