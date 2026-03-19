using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;

namespace Timberborn.ConstructionMode
{
	// Token: 0x0200000A RID: 10
	public class ConstructionModeService : ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000224B File Offset: 0x0000044B
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002253 File Offset: 0x00000453
		public bool InConstructionMode { get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x0000225C File Offset: 0x0000045C
		public ConstructionModeService(EventBus eventBus, EntityComponentRegistry entityComponentRegistry, EntitySelectionService entitySelectionService, ToolGroupService toolGroupService, WaterOpacityService waterOpacityService)
		{
			this._eventBus = eventBus;
			this._entityComponentRegistry = entityComponentRegistry;
			this._entitySelectionService = entitySelectionService;
			this._toolGroupService = toolGroupService;
			this._waterOpacityService = waterOpacityService;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002289 File Offset: 0x00000489
		public void Load()
		{
			this._eventBus.Register(this);
			this._waterOpacityToggle = this._waterOpacityService.GetWaterOpacityToggle();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022A8 File Offset: 0x000004A8
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupEnteredEvent)
		{
			ToolGroupSpec toolGroup = toolGroupEnteredEvent.ToolGroup;
			if (toolGroup != null && toolGroup.HasSpec<ConstructionModeToolGroupSpec>())
			{
				this.EnterConstructionMode();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022C4 File Offset: 0x000004C4
		[OnEvent]
		public void OnToolGroupExited(ToolGroupExitedEvent toolGroupExitedEvent)
		{
			ToolGroupSpec toolGroup = toolGroupExitedEvent.ToolGroup;
			if (toolGroup != null && toolGroup.HasSpec<ConstructionModeToolGroupSpec>())
			{
				this.ExitConstructionMode();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E0 File Offset: 0x000004E0
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			if (toolEnteredEvent.Tool is IConstructionModeEnabler)
			{
				this.EnterConstructionMode();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022F5 File Offset: 0x000004F5
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			if (toolExitedEvent.Tool is IConstructionModeEnabler)
			{
				this.ExitConstructionMode();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000230A File Offset: 0x0000050A
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			if (ConstructionModeService.IsUnfinished(selectableObjectSelectedEvent.SelectableObject))
			{
				this.EnterConstructionMode();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000231F File Offset: 0x0000051F
		[OnEvent]
		public void OnSelectableObjectUnselected(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this.ExitConstructionMode();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002328 File Offset: 0x00000528
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			BlockObject blockObject = enteredFinishedStateEvent.BlockObject;
			if (this._entitySelectionService.IsSelected(blockObject.GetComponent<SelectableObject>()))
			{
				this.ExitConstructionMode();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002358 File Offset: 0x00000558
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedState)
		{
			if (this.InConstructionMode)
			{
				ConstructionModeModel component = enteredUnfinishedState.BlockObject.GetComponent<ConstructionModeModel>();
				if (component)
				{
					component.EnterConstructionMode();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002387 File Offset: 0x00000587
		public IEnumerable<ConstructionModeModel> ConstructionModeModels
		{
			get
			{
				return this._entityComponentRegistry.GetEnabled<ConstructionModeModel>();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002394 File Offset: 0x00000594
		public void EnterConstructionMode()
		{
			if (!this.InConstructionMode)
			{
				foreach (ConstructionModeModel constructionModeModel in this.ConstructionModeModels)
				{
					constructionModeModel.EnterConstructionMode();
				}
				this.ToggleConstructionMode(true);
				this._waterOpacityToggle.HideWater();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023F8 File Offset: 0x000005F8
		public void ExitConstructionMode()
		{
			if (this.CanExitConstructionMode())
			{
				foreach (ConstructionModeModel constructionModeModel in this.ConstructionModeModels)
				{
					constructionModeModel.ExitConstructionMode();
				}
				this.ToggleConstructionMode(false);
				this._waterOpacityToggle.ShowWater();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000245C File Offset: 0x0000065C
		public bool CanExitConstructionMode()
		{
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			bool flag = selectedObject && ConstructionModeService.IsUnfinished(selectedObject);
			ToolGroupSpec activeToolGroup = this._toolGroupService.ActiveToolGroup;
			bool flag2 = activeToolGroup != null && activeToolGroup.HasSpec<ConstructionModeToolGroupSpec>();
			return this.InConstructionMode && !flag2 && !flag;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024B0 File Offset: 0x000006B0
		public static bool IsUnfinished(BaseComponent baseComponent)
		{
			BlockObject component = baseComponent.GetComponent<BlockObject>();
			return component && component.IsUnfinished;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024D4 File Offset: 0x000006D4
		public void ToggleConstructionMode(bool inConstructionMode)
		{
			this.InConstructionMode = inConstructionMode;
			this._eventBus.Post(new ConstructionModeChangedEvent(this.InConstructionMode));
		}

		// Token: 0x0400000C RID: 12
		public readonly EventBus _eventBus;

		// Token: 0x0400000D RID: 13
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400000E RID: 14
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400000F RID: 15
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x04000010 RID: 16
		public readonly WaterOpacityService _waterOpacityService;

		// Token: 0x04000011 RID: 17
		public WaterOpacityToggle _waterOpacityToggle;
	}
}
