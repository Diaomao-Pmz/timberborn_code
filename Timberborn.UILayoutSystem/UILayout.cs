using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.UILayoutSystem
{
	// Token: 0x02000007 RID: 7
	public class UILayout : ILoadableSingleton
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000223C File Offset: 0x0000043C
		public UILayout(PanelStack panelStack)
		{
			this._panelStack = panelStack;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002256 File Offset: 0x00000456
		public bool BottomBarVisible
		{
			get
			{
				return this._bottomBar.IsDisplayed();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002264 File Offset: 0x00000464
		public void Load()
		{
			VisualElement visualElement = this._panelStack.Initialize(UILayout.VisualTreeAssetName, UILayout.ContainerName);
			this._topLeft = UQueryExtensions.Q<VisualElement>(visualElement, "Top-left", null);
			this._topRight = UQueryExtensions.Q<VisualElement>(visualElement, "Top-right", null);
			this._topBar = UQueryExtensions.Q<VisualElement>(visualElement, "Top-bar", null);
			this._bottomLeft = UQueryExtensions.Q<VisualElement>(visualElement, "Bottom-left", null);
			this._bottomRight = UQueryExtensions.Q<VisualElement>(visualElement, "Bottom-right", null);
			this._bottomBar = UQueryExtensions.Q<VisualElement>(visualElement, "Bottom-bar", null);
			this._absoluteItems = UQueryExtensions.Q<VisualElement>(visualElement, "Absolute-items", null);
			this._topRightButtons = new VisualElement();
			this._topRightButtons.AddToClassList(UILayout.TopRightButtonsClass);
			this.AddTopRight(this._topRightButtons, UILayout.TopRightButtonsOrder);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002331 File Offset: 0x00000531
		public void AddTopLeft(VisualElement visualElement, int order)
		{
			this.AddOrderablePanel(this._topLeft, visualElement, order);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002341 File Offset: 0x00000541
		public void AddTopRight(VisualElement visualElement, int order)
		{
			this.AddOrderablePanel(this._topRight, visualElement, order);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002351 File Offset: 0x00000551
		public void AddTopRightButton(VisualElement visualElement, int order)
		{
			this.AddOrderablePanel(this._topRightButtons, visualElement, order);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002361 File Offset: 0x00000561
		public void AddTopBar(VisualElement visualElement)
		{
			UILayout.AddPanel(this._topBar, visualElement);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000236F File Offset: 0x0000056F
		public void AddBottomLeft(VisualElement visualElement, int order)
		{
			this.AddOrderablePanel(this._bottomLeft, visualElement, order);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000237F File Offset: 0x0000057F
		public void AddBottomRight(VisualElement visualElement, int order)
		{
			this.AddOrderablePanel(this._bottomRight, visualElement, order);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000238F File Offset: 0x0000058F
		public void AddBottomBar(VisualElement visualElement, int order)
		{
			this.AddOrderablePanel(this._bottomBar, visualElement, order);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000239F File Offset: 0x0000059F
		public void AddAbsoluteItem(VisualElement visualElement)
		{
			UILayout.AddPanel(this._absoluteItems, visualElement);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023AD File Offset: 0x000005AD
		public void ShowLeftAndCenterItems()
		{
			this._topLeft.ToggleDisplayStyle(true);
			this._topBar.ToggleDisplayStyle(true);
			this._bottomLeft.ToggleDisplayStyle(true);
			this._bottomBar.ToggleDisplayStyle(true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023DF File Offset: 0x000005DF
		public void HideLeftAndCenterItems()
		{
			this._topLeft.ToggleDisplayStyle(false);
			this._topBar.ToggleDisplayStyle(false);
			this._bottomLeft.ToggleDisplayStyle(false);
			this._bottomBar.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002411 File Offset: 0x00000611
		public static void AddPanel(VisualElement root, VisualElement item)
		{
			root.Add(item);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000241C File Offset: 0x0000061C
		public void AddOrderablePanel(VisualElement root, VisualElement visualElement, int order)
		{
			int num = this.FindInsertionIndex(root, order);
			root.Insert(num, visualElement);
			this._elementOrder[visualElement] = order;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002448 File Offset: 0x00000648
		public int FindInsertionIndex(VisualElement root, int order)
		{
			for (int i = 0; i < root.childCount; i++)
			{
				if (this._elementOrder[root[i]] > order)
				{
					return i;
				}
			}
			return root.childCount;
		}

		// Token: 0x0400000D RID: 13
		public static readonly string VisualTreeAssetName = "Common/GameUI";

		// Token: 0x0400000E RID: 14
		public static readonly string ContainerName = "Panels";

		// Token: 0x0400000F RID: 15
		public static readonly string TopRightButtonsClass = "game-ui__top-right-buttons";

		// Token: 0x04000010 RID: 16
		public static readonly int TopRightButtonsOrder = 7;

		// Token: 0x04000011 RID: 17
		public readonly PanelStack _panelStack;

		// Token: 0x04000012 RID: 18
		public VisualElement _topLeft;

		// Token: 0x04000013 RID: 19
		public VisualElement _topRight;

		// Token: 0x04000014 RID: 20
		public VisualElement _topRightButtons;

		// Token: 0x04000015 RID: 21
		public VisualElement _topBar;

		// Token: 0x04000016 RID: 22
		public VisualElement _bottomLeft;

		// Token: 0x04000017 RID: 23
		public VisualElement _bottomRight;

		// Token: 0x04000018 RID: 24
		public VisualElement _bottomBar;

		// Token: 0x04000019 RID: 25
		public VisualElement _absoluteItems;

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<VisualElement, int> _elementOrder = new Dictionary<VisualElement, int>();
	}
}
