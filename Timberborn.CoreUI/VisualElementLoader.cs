using System;
using Timberborn.AssetSystem;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200005C RID: 92
	public class VisualElementLoader
	{
		// Token: 0x0600017F RID: 383 RVA: 0x0000601D File Offset: 0x0000421D
		public VisualElementLoader(IAssetLoader assetLoader, VisualElementInitializer visualElementInitializer)
		{
			this._assetLoader = assetLoader;
			this._visualElementInitializer = visualElementInitializer;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006033 File Offset: 0x00004233
		public VisualElement LoadVisualElement(string elementName)
		{
			return this.LoadVisualElement(this.LoadVisualTreeAsset(elementName));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006044 File Offset: 0x00004244
		public VisualTreeAsset LoadVisualTreeAsset(string elementName)
		{
			string path = VisualElementLoader.ViewsDirectory + "/" + elementName;
			return this._assetLoader.Load<VisualTreeAsset>(path);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006070 File Offset: 0x00004270
		public VisualElement LoadVisualElement(VisualTreeAsset visualTreeAsset)
		{
			VisualElement visualElement = visualTreeAsset.CloneTree().ElementAt(0);
			this._visualElementInitializer.InitializeVisualElement(visualElement);
			return visualElement;
		}

		// Token: 0x040000CF RID: 207
		public static readonly string ViewsDirectory = "UI/Views";

		// Token: 0x040000D0 RID: 208
		public readonly IAssetLoader _assetLoader;

		// Token: 0x040000D1 RID: 209
		public readonly VisualElementInitializer _visualElementInitializer;
	}
}
