using System;
using Timberborn.BatchControl;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using Timberborn.WorkSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x0200001B RID: 27
	public class WorkplacePriorityBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003904 File Offset: 0x00001B04
		public VisualElement Root { get; }

		// Token: 0x0600008A RID: 138 RVA: 0x0000390C File Offset: 0x00001B0C
		public WorkplacePriorityBatchControlRowItem(VisualElement root, WorkplacePriority workplacePriority, Image image, WorkplacePrioritySpriteLoader workplacePrioritySpriteLoader, PriorityColors priorityColors)
		{
			this.Root = root;
			this._workplacePriority = workplacePriority;
			this._image = image;
			this._workplacePrioritySpriteLoader = workplacePrioritySpriteLoader;
			this._priorityColors = priorityColors;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000393C File Offset: 0x00001B3C
		public void UpdateRowItem()
		{
			Priority priority = this._workplacePriority.Priority;
			Sprite sprite = this._workplacePrioritySpriteLoader.LoadSprite(priority);
			Color buttonColor = this._priorityColors.GetButtonColor(priority);
			this._image.style.backgroundImage = new StyleBackground(sprite);
			this._image.style.unityBackgroundImageTintColor = buttonColor;
		}

		// Token: 0x04000082 RID: 130
		public readonly Image _image;

		// Token: 0x04000083 RID: 131
		public readonly WorkplacePriority _workplacePriority;

		// Token: 0x04000084 RID: 132
		public readonly WorkplacePrioritySpriteLoader _workplacePrioritySpriteLoader;

		// Token: 0x04000085 RID: 133
		public readonly PriorityColors _priorityColors;
	}
}
