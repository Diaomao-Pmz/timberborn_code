using System;
using Timberborn.ConstructionGuidelines;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorConstructionGuidelinesUI
{
	// Token: 0x02000005 RID: 5
	internal class MapEditorGuidelinesShower : ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public MapEditorGuidelinesShower(ConstructionGuidelinesRenderingService constructionGuidelinesRenderingService, EventBus eventBus)
		{
			this._constructionGuidelinesRenderingService = constructionGuidelinesRenderingService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EA File Offset: 0x000002EA
		public void Load()
		{
			this._eventBus.Register(this);
			this._constructionGuidelinesToggle = this._constructionGuidelinesRenderingService.GetConstructionGuidelinesToggle();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002109 File Offset: 0x00000309
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			if (toolEnteredEvent.Tool is IBrushWithGuidelines)
			{
				this.ChangeGuidelinesVisibility(true);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this.ChangeGuidelinesVisibility(false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		private void ChangeGuidelinesVisibility(bool show)
		{
			if (show)
			{
				this._constructionGuidelinesToggle.ShowGuidelines();
				return;
			}
			this._constructionGuidelinesToggle.HideGuidelines();
		}

		// Token: 0x04000001 RID: 1
		private readonly ConstructionGuidelinesRenderingService _constructionGuidelinesRenderingService;

		// Token: 0x04000002 RID: 2
		private readonly EventBus _eventBus;

		// Token: 0x04000003 RID: 3
		private ConstructionGuidelinesToggle _constructionGuidelinesToggle;

		// Token: 0x04000004 RID: 4
		private VisualElement _root;
	}
}
