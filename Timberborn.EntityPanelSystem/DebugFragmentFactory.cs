using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000006 RID: 6
	public class DebugFragmentFactory
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000214B File Offset: 0x0000034B
		public DebugFragmentFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000215A File Offset: 0x0000035A
		public VisualElement Create(string title)
		{
			VisualElement rootAndInitialize = this.GetRootAndInitialize(title);
			UQueryExtensions.Q<VisualElement>(rootAndInitialize, "Buttons", null).ToggleDisplayStyle(false);
			return rootAndInitialize;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002178 File Offset: 0x00000378
		public VisualElement Create(string title, DebugFragmentButton debugFragmentButton)
		{
			VisualElement rootAndInitialize = this.GetRootAndInitialize(title);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(rootAndInitialize, "Buttons", null);
			visualElement.AddToClassList(DebugFragmentFactory.MarginClass);
			this.CreateButton(debugFragmentButton, visualElement);
			return rootAndInitialize;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021B0 File Offset: 0x000003B0
		public VisualElement Create(DebugFragmentButton debugFragmentButton)
		{
			VisualElement rootAndInitialize = this.GetRootAndInitialize(null);
			VisualElement root = UQueryExtensions.Q<VisualElement>(rootAndInitialize, "Buttons", null);
			this.CreateButton(debugFragmentButton, root);
			return rootAndInitialize;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021DC File Offset: 0x000003DC
		public VisualElement Create(params DebugFragmentButton[] debugFragmentButtons)
		{
			VisualElement rootAndInitialize = this.GetRootAndInitialize(null);
			VisualElement root = UQueryExtensions.Q<VisualElement>(rootAndInitialize, "Buttons", null);
			for (int i = 0; i < debugFragmentButtons.Length; i++)
			{
				Button button = this.CreateButton(debugFragmentButtons[i], root);
				if (i > 0)
				{
					button.AddToClassList(DebugFragmentFactory.MarginClass);
				}
			}
			return rootAndInitialize;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000222C File Offset: 0x0000042C
		public VisualElement GetRootAndInitialize(string title = null)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DebugFragment");
			UQueryExtensions.Q<Label>(visualElement, "Title", null).text = title;
			UQueryExtensions.Q<VisualElement>(visualElement, "TitleWrapper", null).ToggleDisplayStyle(title != null);
			DebugFragmentFactory.InitializeCallbacks(visualElement);
			DebugFragmentFactory.ToggleVisibility(visualElement, false);
			return visualElement;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002280 File Offset: 0x00000480
		public Button CreateButton(DebugFragmentButton debugFragmentButton, VisualElement root)
		{
			Button button = (Button)this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DebugButton");
			root.Add(button);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				debugFragmentButton.Action();
			}, 0);
			button.text = debugFragmentButton.Text;
			return button;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022DC File Offset: 0x000004DC
		public static void InitializeCallbacks(VisualElement root)
		{
			UQueryExtensions.Q<Button>(root, "Show", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				DebugFragmentFactory.ToggleVisibility(root, true);
			}, 0);
			UQueryExtensions.Q<Button>(root, "Hide", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				DebugFragmentFactory.ToggleVisibility(root, false);
			}, 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000233C File Offset: 0x0000053C
		public static void ToggleVisibility(VisualElement root, bool visible)
		{
			UQueryExtensions.Q<Button>(root, "Show", null).ToggleDisplayStyle(!visible);
			UQueryExtensions.Q<Button>(root, "Hide", null).ToggleDisplayStyle(visible);
			UQueryExtensions.Q<Label>(root, "Text", null).ToggleDisplayStyle(visible);
			UQueryExtensions.Q<VisualElement>(root, "Content", null).ToggleDisplayStyle(visible);
		}

		// Token: 0x0400000C RID: 12
		public static readonly string MarginClass = "debug-fragment--margin";

		// Token: 0x0400000D RID: 13
		public readonly VisualElementLoader _visualElementLoader;
	}
}
