using System;
using Timberborn.AssetSystem;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.FileBrowsing
{
	// Token: 0x02000007 RID: 7
	public class DiskSystemEntryElementFactory : ILoadableSingleton
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002688 File Offset: 0x00000888
		public DiskSystemEntryElementFactory(VisualElementLoader visualElementLoader, IAssetLoader assetLoader)
		{
			this._visualElementLoader = visualElementLoader;
			this._assetLoader = assetLoader;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000269E File Offset: 0x0000089E
		public void Load()
		{
			this._directoryIcon = this._assetLoader.Load<Sprite>("UI/Images/Core/directory-icon");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026B6 File Offset: 0x000008B6
		public VisualElement Create(EventCallback<ClickEvent> onClick)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/DiskSystemEntryElement");
			visualElement.RegisterCallback<ClickEvent>(onClick, 0);
			return visualElement;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026D0 File Offset: 0x000008D0
		public void Bind(VisualElement item, DiskSystemEntry diskSystemEntry, FileFilter fileFilter)
		{
			UQueryExtensions.Q<Label>(item, "Name", null).text = diskSystemEntry.Name;
			UQueryExtensions.Q<Image>(item, "Icon", null).sprite = (diskSystemEntry.IsDirectory ? this._directoryIcon : fileFilter.Icon);
		}

		// Token: 0x0400001A RID: 26
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001B RID: 27
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400001C RID: 28
		public Sprite _directoryIcon;
	}
}
