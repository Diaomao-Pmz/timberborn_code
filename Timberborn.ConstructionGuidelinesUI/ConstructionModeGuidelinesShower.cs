using System;
using Timberborn.ConstructionGuidelines;
using Timberborn.ConstructionMode;
using Timberborn.SingletonSystem;

namespace Timberborn.ConstructionGuidelinesUI
{
	// Token: 0x02000006 RID: 6
	public class ConstructionModeGuidelinesShower : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002272 File Offset: 0x00000472
		public ConstructionModeGuidelinesShower(EventBus eventBus, ConstructionModeService constructionModeService, ConstructionGuidelinesRenderingService constructionGuidelinesRenderingService)
		{
			this._eventBus = eventBus;
			this._constructionModeService = constructionModeService;
			this._constructionGuidelinesRenderingService = constructionGuidelinesRenderingService;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000228F File Offset: 0x0000048F
		public void Load()
		{
			this._eventBus.Register(this);
			this._constructionGuidelinesToggle = this._constructionGuidelinesRenderingService.GetConstructionGuidelinesToggle();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022AE File Offset: 0x000004AE
		[OnEvent]
		public void OnConstructionModeChanged(ConstructionModeChangedEvent constructionModeChangedEvent)
		{
			if (this._constructionModeService.InConstructionMode)
			{
				this._constructionGuidelinesToggle.ShowGuidelines();
				return;
			}
			this._constructionGuidelinesToggle.HideGuidelines();
		}

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly ConstructionModeService _constructionModeService;

		// Token: 0x04000018 RID: 24
		public readonly ConstructionGuidelinesRenderingService _constructionGuidelinesRenderingService;

		// Token: 0x04000019 RID: 25
		public ConstructionGuidelinesToggle _constructionGuidelinesToggle;
	}
}
