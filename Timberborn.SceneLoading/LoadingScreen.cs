using System;
using Timberborn.Localization;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SceneLoading
{
	// Token: 0x02000008 RID: 8
	public class LoadingScreen : ILoadableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000F RID: 15 RVA: 0x00002118 File Offset: 0x00000318
		// (remove) Token: 0x06000010 RID: 16 RVA: 0x00002150 File Offset: 0x00000350
		public event EventHandler LoadingScreenEnabled;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000011 RID: 17 RVA: 0x00002188 File Offset: 0x00000388
		// (remove) Token: 0x06000012 RID: 18 RVA: 0x000021C0 File Offset: 0x000003C0
		public event EventHandler LoadingScreenDisabled;

		// Token: 0x06000013 RID: 19 RVA: 0x000021F5 File Offset: 0x000003F5
		public LoadingScreen(ILoc loc, RootObjectProvider rootObjectProvider, RootVisualElementProvider rootVisualElementProvider)
		{
			this._loc = loc;
			this._rootObjectProvider = rootObjectProvider;
			this._rootVisualElementProvider = rootVisualElementProvider;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002214 File Offset: 0x00000414
		public void Load()
		{
			GameObject gameObject = this._rootObjectProvider.CreateRootObject("LoadingScreen");
			Object.DontDestroyOnLoad(gameObject);
			this._root = this._rootVisualElementProvider.Create(gameObject, "LoadingScreen/LoadingScreen", 10000, "UI/Views/LoadingScreen/LoadingScreenPanelSettings");
			this.Hide();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002260 File Offset: 0x00000460
		public void Enable(string tip)
		{
			this.Show();
			UQueryExtensions.Q<Label>(this._root, "LoadingLabel", null).text = this._loc.T(LoadingScreen.LoadingLocKey);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this._root, "TipWrapper", null);
			Label label = UQueryExtensions.Q<Label>(this._root, "TipText", null);
			if (string.IsNullOrEmpty(tip))
			{
				visualElement.style.display = 1;
			}
			else
			{
				label.text = tip;
				visualElement.style.display = 0;
			}
			EventHandler loadingScreenEnabled = this.LoadingScreenEnabled;
			if (loadingScreenEnabled == null)
			{
				return;
			}
			loadingScreenEnabled(this, EventArgs.Empty);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002306 File Offset: 0x00000506
		public void Disable()
		{
			EventHandler loadingScreenDisabled = this.LoadingScreenDisabled;
			if (loadingScreenDisabled != null)
			{
				loadingScreenDisabled(this, EventArgs.Empty);
			}
			this.Hide();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002325 File Offset: 0x00000525
		public void Show()
		{
			this._root.style.display = 0;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000233D File Offset: 0x0000053D
		public void Hide()
		{
			this._root.style.display = 1;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string LoadingLocKey = "Core.Loading";

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400000D RID: 13
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;
	}
}
