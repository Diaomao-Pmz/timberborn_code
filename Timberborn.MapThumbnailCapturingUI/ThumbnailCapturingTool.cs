using System;
using Timberborn.Localization;
using Timberborn.MapThumbnail;
using Timberborn.MapThumbnailCapturing;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailCapturing;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;

namespace Timberborn.MapThumbnailCapturingUI
{
	// Token: 0x02000007 RID: 7
	public class ThumbnailCapturingTool : ITool, IToolDescriptor, IWaterIgnoringTool, ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000238F File Offset: 0x0000058F
		public ThumbnailCapturingTool(ILoc loc, MapThumbnailCameraMover mapThumbnailCameraMover, ThumbnailRenderer thumbnailRenderer, EventBus eventBus, ToolService toolService)
		{
			this._loc = loc;
			this._mapThumbnailCameraMover = mapThumbnailCameraMover;
			this._thumbnailRenderer = thumbnailRenderer;
			this._eventBus = eventBus;
			this._toolService = toolService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023BC File Offset: 0x000005BC
		public void Load()
		{
			this._toolDescription = new ToolDescription.Builder(this._loc.T(ThumbnailCapturingTool.TitleLocKey)).AddSection(this._loc.T(ThumbnailCapturingTool.DescriptionLocKey)).Build();
			this._eventBus.Register(this);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000240A File Offset: 0x0000060A
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				this._thumbnailRenderer.Render();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000241F File Offset: 0x0000061F
		public void Enter()
		{
			this._enabled = true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002428 File Offset: 0x00000628
		public void Exit()
		{
			this._enabled = false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002431 File Offset: 0x00000631
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002439 File Offset: 0x00000639
		public void ChangeThumbnail()
		{
			this._mapThumbnailCameraMover.MoveToMainCameraPosition();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002446 File Offset: 0x00000646
		public void ResetThumbnail()
		{
			this._mapThumbnailCameraMover.MoveToDefaultPosition();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002453 File Offset: 0x00000653
		[OnEvent]
		public void OnMapThumbnailChanged(MapThumbnailChangedEvent mapThumbnailChangedEvent)
		{
			this._toolService.SwitchTool(this);
		}

		// Token: 0x04000013 RID: 19
		public static readonly string TitleLocKey = "MapEditor.ThumbnailCapturing.Title";

		// Token: 0x04000014 RID: 20
		public static readonly string DescriptionLocKey = "MapEditor.ThumbnailCapturing.Description";

		// Token: 0x04000015 RID: 21
		public readonly ILoc _loc;

		// Token: 0x04000016 RID: 22
		public readonly MapThumbnailCameraMover _mapThumbnailCameraMover;

		// Token: 0x04000017 RID: 23
		public readonly ThumbnailRenderer _thumbnailRenderer;

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;

		// Token: 0x04000019 RID: 25
		public readonly ToolService _toolService;

		// Token: 0x0400001A RID: 26
		public ToolDescription _toolDescription;

		// Token: 0x0400001B RID: 27
		public bool _enabled;
	}
}
