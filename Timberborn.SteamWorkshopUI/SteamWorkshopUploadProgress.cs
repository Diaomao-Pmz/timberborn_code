using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.SteamWorkshop;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x02000008 RID: 8
	public class SteamWorkshopUploadProgress : ILoadableSingleton, IUpdatableSingleton, IPanelController
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000251D File Offset: 0x0000071D
		public SteamWorkshopUploadProgress(VisualElementLoader visualElementLoader, PanelStack panelStack, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._loc = loc;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000253C File Offset: 0x0000073C
		public void Load()
		{
			string elementName = "Common/SteamWorkshop/SteamWorkshopUploadProgress";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._progressLabel = UQueryExtensions.Q<Label>(this._root, "ProgressLabel", null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000258F File Offset: 0x0000078F
		public void Open()
		{
			this._panelStack.PushOverlay(this);
			this._progress = 0f;
			this.UpdateProgressBar();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025AE File Offset: 0x000007AE
		public void Close()
		{
			this._panelStack.Pop(this);
			this._steamWorkshopUpdateHandle = null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025C3 File Offset: 0x000007C3
		public void SetUpdateHandle(SteamWorkshopUpdateHandle steamWorkshopUpdateHandle)
		{
			this._steamWorkshopUpdateHandle = steamWorkshopUpdateHandle;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025CC File Offset: 0x000007CC
		public void UpdateSingleton()
		{
			if (this._steamWorkshopUpdateHandle != null)
			{
				float progress = this._steamWorkshopUpdateHandle.GetProgress();
				if (progress > this._progress)
				{
					this._progress = progress;
					this.UpdateProgressBar();
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002603 File Offset: 0x00000803
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000260B File Offset: 0x0000080B
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000260E File Offset: 0x0000080E
		public void OnUICancelled()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002610 File Offset: 0x00000810
		public void UpdateProgressBar()
		{
			this._progressBar.SetProgress(this._progress);
			this._progressLabel.text = this._loc.T<string>(SteamWorkshopUploadProgress.UploadProgressLocKey, NumberFormatter.FormatAsPercentFloored((double)this._progress));
		}

		// Token: 0x04000018 RID: 24
		public static readonly string UploadProgressLocKey = "SteamWorkshop.UploadProgress";

		// Token: 0x04000019 RID: 25
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001A RID: 26
		public readonly PanelStack _panelStack;

		// Token: 0x0400001B RID: 27
		public readonly ILoc _loc;

		// Token: 0x0400001C RID: 28
		public VisualElement _root;

		// Token: 0x0400001D RID: 29
		public ProgressBar _progressBar;

		// Token: 0x0400001E RID: 30
		public Label _progressLabel;

		// Token: 0x0400001F RID: 31
		public float _progress;

		// Token: 0x04000020 RID: 32
		public SteamWorkshopUpdateHandle _steamWorkshopUpdateHandle;
	}
}
