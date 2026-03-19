using System;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000017 RID: 23
	public class ToolService : IInputProcessor, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002698 File Offset: 0x00000898
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000026A0 File Offset: 0x000008A0
		public ITool ActiveTool { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x000026A9 File Offset: 0x000008A9
		public ToolService(EventBus eventBus, InputService inputService, ToolGroupService toolGroupService, IDefaultToolProvider defaultToolProvider)
		{
			this._eventBus = eventBus;
			this._inputService = inputService;
			this._toolGroupService = toolGroupService;
			this._defaultToolProvider = defaultToolProvider;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000026CE File Offset: 0x000008CE
		public bool IsDefaultToolActive
		{
			get
			{
				return this.IsDefaultTool(this.ActiveTool);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026DC File Offset: 0x000008DC
		public void Load()
		{
			this._eventBus.Register(this);
			this._defaultTool = this._defaultToolProvider.DefaultTool;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000026FB File Offset: 0x000008FB
		public void PostLoad()
		{
			this.SwitchToDefaultTool();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002703 File Offset: 0x00000903
		public bool ProcessInput()
		{
			if (this._inputService.Cancel && !this.IsDefaultToolActive)
			{
				this.SwitchToDefaultTool();
				return true;
			}
			return false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002723 File Offset: 0x00000923
		public void SwitchToDefaultTool()
		{
			this.SwitchToolInternal(this._defaultTool, false, false);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002733 File Offset: 0x00000933
		public void SwitchTool(ITool tool)
		{
			this.SwitchToolInternal(tool, this.ShouldCloseGroup(tool), this.IsDefaultTool(tool));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000274C File Offset: 0x0000094C
		public void ExitTool()
		{
			if (this.ActiveTool != null)
			{
				ITool activeTool = this.ActiveTool;
				this.ActiveTool.Exit();
				this.ActiveTool = null;
				this._inputService.RemoveInputProcessor(this);
				this._eventBus.Post(new ToolExitedEvent(activeTool));
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002797 File Offset: 0x00000997
		public void SetTemporaryTool(ITool tool)
		{
			this._eventBus.Post(new TemporaryToolEnteredEvent(tool));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000027AA File Offset: 0x000009AA
		public void ClearTemporaryTool()
		{
			this._eventBus.Post(new TemporaryToolExitedEvent());
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027BC File Offset: 0x000009BC
		public bool ShouldCloseGroup(ITool tool)
		{
			return !(tool is IGroupIgnoringTool) && !this._toolGroupService.IsAssignedToAnyGroup(tool);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000027D7 File Offset: 0x000009D7
		public bool IsDefaultTool(ITool tool)
		{
			return tool == this._defaultTool;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027E4 File Offset: 0x000009E4
		public void SwitchToolInternal(ITool tool, bool shouldCloseGroup, bool forceSwitch)
		{
			if (this.ActiveTool != tool || forceSwitch)
			{
				this.ExitTool();
				this._inputService.AddInputProcessor(this);
				this.ActiveTool = tool;
				tool.Enter();
				this._eventBus.Post(new ToolEnteredEvent(tool, shouldCloseGroup));
			}
		}

		// Token: 0x0400001B RID: 27
		public readonly EventBus _eventBus;

		// Token: 0x0400001C RID: 28
		public readonly InputService _inputService;

		// Token: 0x0400001D RID: 29
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x0400001E RID: 30
		public readonly IDefaultToolProvider _defaultToolProvider;

		// Token: 0x0400001F RID: 31
		public ITool _defaultTool;
	}
}
