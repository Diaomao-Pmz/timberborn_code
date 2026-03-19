using System;
using Timberborn.ConstructionMode;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SingletonSystem;
using Timberborn.StatusSystem;

namespace Timberborn.BuildingStatuses
{
	// Token: 0x02000006 RID: 6
	public class BuildingStatusIconUpdater : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000253B File Offset: 0x0000073B
		public BuildingStatusIconUpdater(EventBus eventBus, IStatusIconOffsetService statusIconOffsetService)
		{
			this._eventBus = eventBus;
			this._statusIconOffsetService = statusIconOffsetService;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002551 File Offset: 0x00000751
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000255F File Offset: 0x0000075F
		[OnEvent]
		public void OnConstructionModeChanged(ConstructionModeChangedEvent constructionModeChangedEvent)
		{
			if (constructionModeChangedEvent.InConstructionMode)
			{
				this._statusIconOffsetService.EnablePreviewMode();
			}
			else
			{
				this._statusIconOffsetService.DisablePreviewMode();
			}
			this._updateNextFrame = true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002588 File Offset: 0x00000788
		[OnEvent]
		public void OnMaxVisibleLevelChanged(MaxVisibleLevelChangedEvent maxVisibleLevelChangedEvent)
		{
			this._updateNextFrame = true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002591 File Offset: 0x00000791
		public void UpdateSingleton()
		{
			if (this._updateNextFrame)
			{
				this._statusIconOffsetService.RepositionAllIcons();
				this._updateNextFrame = false;
			}
		}

		// Token: 0x04000017 RID: 23
		public readonly EventBus _eventBus;

		// Token: 0x04000018 RID: 24
		public readonly IStatusIconOffsetService _statusIconOffsetService;

		// Token: 0x04000019 RID: 25
		public bool _updateNextFrame;
	}
}
