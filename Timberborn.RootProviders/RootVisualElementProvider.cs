using System;
using Timberborn.AssetSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.RootProviders
{
	// Token: 0x02000006 RID: 6
	public class RootVisualElementProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020F0 File Offset: 0x000002F0
		public RootVisualElementProvider(IAssetLoader assetLoader, RootObjectProvider rootObjectProvider)
		{
			this._assetLoader = assetLoader;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public VisualElement Create(GameObject parent, string sourceAssetPath, int sortOrder, string panelSettingsPath = null)
		{
			UIDocument uidocument = this.CreateUIDocument(parent, sortOrder, panelSettingsPath);
			string path = "UI/Views/" + sourceAssetPath;
			uidocument.visualTreeAsset = this._assetLoader.Load<VisualTreeAsset>(path);
			return uidocument.rootVisualElement;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002144 File Offset: 0x00000344
		public VisualElement Create(string name, string sourceAssetPath, int sortOrder)
		{
			GameObject parent = this._rootObjectProvider.CreateRootObject(name);
			return this.Create(parent, sourceAssetPath, sortOrder, null);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002168 File Offset: 0x00000368
		public UIDocument CreateEmpty(string name, int sortOrder)
		{
			GameObject parent = this._rootObjectProvider.CreateRootObject(name);
			return this.CreateUIDocument(parent, sortOrder, null);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218B File Offset: 0x0000038B
		public UIDocument CreateUIDocument(GameObject parent, int sortOrder, string panelSettingsPath = null)
		{
			UIDocument uidocument = parent.AddComponent<UIDocument>();
			uidocument.panelSettings = (string.IsNullOrEmpty(panelSettingsPath) ? this._assetLoader.Load<PanelSettings>("UI/Views/Core/ScalablePanelSettings") : this._assetLoader.Load<PanelSettings>(panelSettingsPath));
			uidocument.sortingOrder = (float)sortOrder;
			return uidocument;
		}

		// Token: 0x04000006 RID: 6
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000007 RID: 7
		public readonly RootObjectProvider _rootObjectProvider;
	}
}
