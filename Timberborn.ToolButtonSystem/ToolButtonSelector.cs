using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.UILayoutSystem;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000009 RID: 9
	public class ToolButtonSelector : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002744 File Offset: 0x00000944
		public ToolButtonSelector(InputService inputService, ToolButtonService toolButtonService, ToolService toolService, ToolUnlockingService toolUnlockingService, UILayout uiLayout, EventBus eventBus)
		{
			this._inputService = inputService;
			this._toolButtonService = toolButtonService;
			this._toolService = toolService;
			this._toolUnlockingService = toolUnlockingService;
			this._uiLayout = uiLayout;
			this._eventBus = eventBus;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002779 File Offset: 0x00000979
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002787 File Offset: 0x00000987
		public bool ProcessInput()
		{
			return this._uiLayout.BottomBarVisible && this.ProcessToolbarInput();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000279E File Offset: 0x0000099E
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027AC File Offset: 0x000009AC
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupEntered)
		{
			this._toolService.SwitchToDefaultTool();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027BC File Offset: 0x000009BC
		public bool ProcessToolbarInput()
		{
			IToolbarButton toolbarButton;
			if (this._inputService.IsKeyDown(ToolButtonSelector.NextRootButtonKey) && this._toolButtonService.TryGetNextRootButton(out toolbarButton))
			{
				toolbarButton.Select();
				return true;
			}
			IToolbarButton toolbarButton2;
			if (this._inputService.IsKeyDown(ToolButtonSelector.PreviousRootButtonKey) && this._toolButtonService.TryGetPreviousRootButton(out toolbarButton2))
			{
				toolbarButton2.Select();
				return true;
			}
			IToolbarButton toolbarButton3;
			if (this._inputService.IsKeyDown(ToolButtonSelector.NextToolKey) && this._toolButtonService.TryGetNextToolButton(out toolbarButton3))
			{
				toolbarButton3.Select();
				return true;
			}
			IToolbarButton toolbarButton4;
			if (this._inputService.IsKeyDown(ToolButtonSelector.PreviousToolKey) && this._toolButtonService.TryGetPreviousToolButton(out toolbarButton4))
			{
				toolbarButton4.Select();
				return true;
			}
			if (this._inputService.IsKeyDown(ToolButtonSelector.UnlockToolKey) && this._toolUnlockingService.IsLocked(this._toolService.ActiveTool))
			{
				this._toolUnlockingService.TryToUnlock(this._toolService.ActiveTool);
				return true;
			}
			return false;
		}

		// Token: 0x0400001F RID: 31
		public static readonly string NextRootButtonKey = "NextRootButton";

		// Token: 0x04000020 RID: 32
		public static readonly string PreviousRootButtonKey = "PreviousRootButton";

		// Token: 0x04000021 RID: 33
		public static readonly string NextToolKey = "NextTool";

		// Token: 0x04000022 RID: 34
		public static readonly string PreviousToolKey = "PreviousTool";

		// Token: 0x04000023 RID: 35
		public static readonly string UnlockToolKey = "UnlockTool";

		// Token: 0x04000024 RID: 36
		public readonly InputService _inputService;

		// Token: 0x04000025 RID: 37
		public readonly ToolButtonService _toolButtonService;

		// Token: 0x04000026 RID: 38
		public readonly ToolService _toolService;

		// Token: 0x04000027 RID: 39
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000028 RID: 40
		public readonly UILayout _uiLayout;

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;
	}
}
