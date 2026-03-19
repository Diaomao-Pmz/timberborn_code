using System;
using Timberborn.CoreUI;
using Timberborn.FileBrowsing;
using Timberborn.MapThumbnail;
using Timberborn.MapThumbnailOverlaySystem;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailCapturing;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapThumbnailCapturingUI
{
	// Token: 0x02000006 RID: 6
	public class ThumbnailCapturingPanel : IToolFragment, IPostLoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000211D File Offset: 0x0000031D
		public ThumbnailCapturingPanel(EventBus eventBus, IThumbnailRenderTextureProvider thumbnailRenderTextureProvider, VisualElementLoader visualElementLoader, FileBrowser fileBrowser, ToolService toolService, MapThumbnailOverlay mapThumbnailOverlay, FileFilterProvider fileFilterProvider)
		{
			this._eventBus = eventBus;
			this._thumbnailRenderTextureProvider = thumbnailRenderTextureProvider;
			this._visualElementLoader = visualElementLoader;
			this._fileBrowser = fileBrowser;
			this._toolService = toolService;
			this._mapThumbnailOverlay = mapThumbnailOverlay;
			this._fileFilterProvider = fileFilterProvider;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/ThumbnailCapturingPanel");
			UQueryExtensions.Q<Button>(this._root, "Update", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._thumbnailCapturingTool.ChangeThumbnail();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "Reset", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._thumbnailCapturingTool.ResetThumbnail();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "SelectOverlay", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SelectOverlay), 0);
			this._clearButton = UQueryExtensions.Q<Button>(this._root, "ClearOverlay", null);
			this._clearButton.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ClearOverlay), 0);
			this._overlayImage = UQueryExtensions.Q<Image>(this._root, "Overlay", null);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		public void PostLoad()
		{
			UQueryExtensions.Q<Image>(this._root, "Preview", null).image = this._thumbnailRenderTextureProvider.RenderTexture;
			this.UpdateOverlayImage();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002275 File Offset: 0x00000475
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._thumbnailCapturingTool = (toolEnteredEvent.Tool as ThumbnailCapturingTool);
			if (this._thumbnailCapturingTool != null)
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000229C File Offset: 0x0000049C
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022AA File Offset: 0x000004AA
		[OnEvent]
		public void OnPanelHidden(PanelHiddenEvent panelHiddenEvent)
		{
			if (!panelHiddenEvent.AnyPanelShown && this._toolService.ActiveTool is ThumbnailCapturingTool)
			{
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022D2 File Offset: 0x000004D2
		[OnEvent]
		public void OnMapThumbnailChanged(MapThumbnailChangedEvent mapThumbnailChangedEvent)
		{
			this.UpdateOverlayImage();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022DA File Offset: 0x000004DA
		public void SelectOverlay(ClickEvent evt)
		{
			this._root.ToggleDisplayStyle(false);
			this._fileBrowser.Open(new Action<string>(this.OverlayChosenCallback), this._fileFilterProvider.Images, ThumbnailCapturingPanel.OverlayTipLocKey);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000230F File Offset: 0x0000050F
		public void OverlayChosenCallback(string path)
		{
			this._mapThumbnailOverlay.LoadFromFile(path);
			this.UpdateOverlayImage();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002323 File Offset: 0x00000523
		public void ClearOverlay(ClickEvent evt)
		{
			this._mapThumbnailOverlay.Clear();
			this.UpdateOverlayImage();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002336 File Offset: 0x00000536
		public void UpdateOverlayImage()
		{
			this._overlayImage.image = this._mapThumbnailOverlay.Overlay;
			this._clearButton.ToggleDisplayStyle(this._mapThumbnailOverlay.Overlay);
		}

		// Token: 0x04000007 RID: 7
		public static readonly string OverlayTipLocKey = "MapEditor.ThumbnailCapturing.OverlayTip";

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly IThumbnailRenderTextureProvider _thumbnailRenderTextureProvider;

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly FileBrowser _fileBrowser;

		// Token: 0x0400000C RID: 12
		public readonly ToolService _toolService;

		// Token: 0x0400000D RID: 13
		public readonly MapThumbnailOverlay _mapThumbnailOverlay;

		// Token: 0x0400000E RID: 14
		public readonly FileFilterProvider _fileFilterProvider;

		// Token: 0x0400000F RID: 15
		public VisualElement _root;

		// Token: 0x04000010 RID: 16
		public ThumbnailCapturingTool _thumbnailCapturingTool;

		// Token: 0x04000011 RID: 17
		public Image _overlayImage;

		// Token: 0x04000012 RID: 18
		public Button _clearButton;
	}
}
