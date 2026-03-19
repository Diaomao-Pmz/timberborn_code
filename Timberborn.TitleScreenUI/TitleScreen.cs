using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.TitleScreenUI
{
	// Token: 0x02000006 RID: 6
	public class TitleScreen
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021C7 File Offset: 0x000003C7
		public TitleScreen(PanelStack panelStack, TitleScreenFooter titleScreenFooter, VisualElementInitializer visualElementInitializer)
		{
			this._panelStack = panelStack;
			this._titleScreenFooter = titleScreenFooter;
			this._visualElementInitializer = visualElementInitializer;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E4 File Offset: 0x000003E4
		public void Initialize()
		{
			VisualElement visualElement = this._panelStack.Initialize(TitleScreen.TitleScreenName, TitleScreen.ContainerName);
			this._titleScreenFooter.Initialize(visualElement);
			this._visualElementInitializer.InitializeVisualElement(visualElement);
			this._root = UQueryExtensions.Q<VisualElement>(visualElement, TitleScreen.RootName, null);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002231 File Offset: 0x00000431
		public void HideBackground()
		{
			this._root.RemoveFromClassList(TitleScreen.BackgroundClass);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002243 File Offset: 0x00000443
		public void ShowBackground()
		{
			this._root.AddToClassList(TitleScreen.BackgroundClass);
		}

		// Token: 0x0400000C RID: 12
		public static readonly string TitleScreenName = "MainMenu/TitleScreen";

		// Token: 0x0400000D RID: 13
		public static readonly string ContainerName = "TitleScreenContent";

		// Token: 0x0400000E RID: 14
		public static readonly string RootName = "TitleScreen";

		// Token: 0x0400000F RID: 15
		public static readonly string BackgroundClass = "title-screen-background";

		// Token: 0x04000010 RID: 16
		public readonly PanelStack _panelStack;

		// Token: 0x04000011 RID: 17
		public readonly TitleScreenFooter _titleScreenFooter;

		// Token: 0x04000012 RID: 18
		public readonly VisualElementInitializer _visualElementInitializer;

		// Token: 0x04000013 RID: 19
		public VisualElement _root;
	}
}
