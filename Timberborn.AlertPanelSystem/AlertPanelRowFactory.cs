using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.StatusSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.AlertPanelSystem
{
	// Token: 0x02000008 RID: 8
	public class AlertPanelRowFactory
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002303 File Offset: 0x00000503
		public AlertPanelRowFactory(ILoc loc, StatusSpriteLoader statusSpriteLoader, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._statusSpriteLoader = statusSpriteLoader;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002320 File Offset: 0x00000520
		public VisualElement CreateClosable(string statusIconName)
		{
			string elementName = "Common/AlertPanel/ClosableAlertPanelRow";
			VisualElement root = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Image>(root, "Icon", null).sprite = this._statusSpriteLoader.LoadSprite(statusIconName);
			Button button = UQueryExtensions.Q<Button>(root, "Close", null);
			root.AddToClassList(AlertPanelRowFactory.EnableHoverClass);
			button.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				root.RemoveFromClassList(AlertPanelRowFactory.EnableHoverClass);
			}, 0);
			button.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				root.AddToClassList(AlertPanelRowFactory.EnableHoverClass);
			}, 0);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				root.ToggleDisplayStyle(false);
			}, 0);
			root.ToggleDisplayStyle(false);
			return root;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D8 File Offset: 0x000005D8
		public VisualElement Create(string labelLocKey, string statusIconName)
		{
			return this.CreateInternal(this._loc.T(labelLocKey), this._statusSpriteLoader.LoadSprite(statusIconName));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023F8 File Offset: 0x000005F8
		public VisualElement Create(Sprite sprite)
		{
			return this.CreateInternal("", sprite);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002408 File Offset: 0x00000608
		public VisualElement CreateInternal(string label, Sprite sprite)
		{
			string elementName = "Common/AlertPanel/AlertPanelRow";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Image>(visualElement, "Icon", null).sprite = sprite;
			UQueryExtensions.Q<Button>(visualElement, "Button", null).text = label;
			visualElement.ToggleDisplayStyle(false);
			return visualElement;
		}

		// Token: 0x04000011 RID: 17
		public static readonly string EnableHoverClass = "hover-enabled";

		// Token: 0x04000012 RID: 18
		public readonly ILoc _loc;

		// Token: 0x04000013 RID: 19
		public readonly StatusSpriteLoader _statusSpriteLoader;

		// Token: 0x04000014 RID: 20
		public readonly VisualElementLoader _visualElementLoader;
	}
}
