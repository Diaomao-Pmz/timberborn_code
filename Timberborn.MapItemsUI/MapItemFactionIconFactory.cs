using System;
using Timberborn.CoreUI;
using Timberborn.FactionSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MapItemsUI
{
	// Token: 0x0200000B RID: 11
	public class MapItemFactionIconFactory
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000261B File Offset: 0x0000081B
		public MapItemFactionIconFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000262C File Offset: 0x0000082C
		public VisualElement Create(FactionSpec factionSpec)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/MapItemFactionIcon");
			Sprite asset = factionSpec.Logo.Asset;
			visualElement.style.backgroundImage = new StyleBackground(asset);
			return visualElement;
		}

		// Token: 0x04000023 RID: 35
		public readonly VisualElementLoader _visualElementLoader;
	}
}
