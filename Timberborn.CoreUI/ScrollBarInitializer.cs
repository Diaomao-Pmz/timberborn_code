using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200004B RID: 75
	public class ScrollBarInitializer : IVisualElementInitializer
	{
		// Token: 0x06000137 RID: 311 RVA: 0x000055F5 File Offset: 0x000037F5
		public ScrollBarInitializer(ScrollBarInitializationService scrollBarInitializationService)
		{
			this._scrollBarInitializationService = scrollBarInitializationService;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005604 File Offset: 0x00003804
		public void InitializeVisualElement(VisualElement visualElement)
		{
			this._scrollBarInitializationService.InitializeVisualElement(visualElement);
		}

		// Token: 0x040000A2 RID: 162
		public readonly ScrollBarInitializationService _scrollBarInitializationService;
	}
}
