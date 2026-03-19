using System;
using Timberborn.BatchControl;
using Timberborn.BuilderPrioritySystem;
using Timberborn.BuilderPrioritySystemUI;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ConstructionSitesUI
{
	// Token: 0x02000009 RID: 9
	public class ConstructionSitePriorityBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025F1 File Offset: 0x000007F1
		public VisualElement Root { get; }

		// Token: 0x0600001E RID: 30 RVA: 0x000025F9 File Offset: 0x000007F9
		public ConstructionSitePriorityBatchControlRowItem(BuilderPrioritySpriteLoader builderPrioritySpriteLoader, VisualElement root, BuilderPrioritizable builderPrioritizable, ConstructionSite constructionSite, Image image, PriorityColors priorityColors)
		{
			this._builderPrioritySpriteLoader = builderPrioritySpriteLoader;
			this.Root = root;
			this._builderPrioritizable = builderPrioritizable;
			this._constructionSite = constructionSite;
			this._image = image;
			this._priorityColors = priorityColors;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002630 File Offset: 0x00000830
		public void UpdateRowItem()
		{
			if (this._constructionSite.Enabled)
			{
				this.Root.ToggleDisplayStyle(true);
				Priority priority = this._builderPrioritizable.Priority;
				Sprite sprite = this._builderPrioritySpriteLoader.LoadSprite(priority);
				Color buttonColor = this._priorityColors.GetButtonColor(priority);
				this._image.style.backgroundImage = new StyleBackground(sprite);
				this._image.style.unityBackgroundImageTintColor = buttonColor;
				return;
			}
			this.Root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000022 RID: 34
		public readonly BuilderPrioritySpriteLoader _builderPrioritySpriteLoader;

		// Token: 0x04000023 RID: 35
		public readonly Image _image;

		// Token: 0x04000024 RID: 36
		public readonly ConstructionSite _constructionSite;

		// Token: 0x04000025 RID: 37
		public readonly BuilderPrioritizable _builderPrioritizable;

		// Token: 0x04000026 RID: 38
		public readonly PriorityColors _priorityColors;
	}
}
