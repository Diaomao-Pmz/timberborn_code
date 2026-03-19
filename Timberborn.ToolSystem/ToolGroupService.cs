using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000014 RID: 20
	public class ToolGroupService : ILoadableSingleton, IInputProcessor
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002184 File Offset: 0x00000384
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x000021BC File Offset: 0x000003BC
		public event EventHandler<ITool> ToolGroupAssigned;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021F1 File Offset: 0x000003F1
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000021F9 File Offset: 0x000003F9
		public ToolGroupSpec ActiveToolGroup { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x00002202 File Offset: 0x00000402
		public ToolGroupService(EventBus eventBus, ISpecService specService, InputService inputService, IDefaultToolProvider defaultToolProvider)
		{
			this._eventBus = eventBus;
			this._specService = specService;
			this._inputService = inputService;
			this._defaultToolProvider = defaultToolProvider;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002240 File Offset: 0x00000440
		public void Load()
		{
			foreach (ToolGroupSpec toolGroupSpec in this._specService.GetSpecs<ToolGroupSpec>())
			{
				this.RegisterGroup(toolGroupSpec);
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022A0 File Offset: 0x000004A0
		public bool ProcessInput()
		{
			if (this._inputService.Cancel && this.ActiveToolGroup != null)
			{
				this.ExitToolGroup();
				return true;
			}
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022C8 File Offset: 0x000004C8
		public ToolGroupSpec GetGroup(string id)
		{
			ToolGroupSpec result;
			if (!this._toolGroups.TryGetValue(id, out result))
			{
				throw new KeyNotFoundException("Unknown ToolGroupSpec: " + id + ".");
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022FC File Offset: 0x000004FC
		public void RegisterGroup(ToolGroupSpec toolGroupSpec)
		{
			this._toolGroups.Add(toolGroupSpec.Id, toolGroupSpec);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002310 File Offset: 0x00000510
		public bool IsAssignedToAnyGroup(ITool tool)
		{
			return this._assignedToolGroups.ContainsKey(tool);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002320 File Offset: 0x00000520
		public bool IsAssignedToGroup(ITool tool, ToolGroupSpec toolGroup)
		{
			ToolGroupSpec right;
			return this._assignedToolGroups.TryGetValue(tool, out right) && toolGroup == right;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002346 File Offset: 0x00000546
		public void AssignToGroup(ToolGroupSpec toolGroup, ITool tool)
		{
			Asserts.FieldIsNotNull<ToolGroupService>(this, toolGroup, "toolGroup");
			this._assignedToolGroups[tool] = toolGroup;
			EventHandler<ITool> toolGroupAssigned = this.ToolGroupAssigned;
			if (toolGroupAssigned == null)
			{
				return;
			}
			toolGroupAssigned(this, tool);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002373 File Offset: 0x00000573
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			if (toolEnteredEvent.ShouldCloseGroup)
			{
				this.ExitToolGroupInternal();
			}
			if (toolEnteredEvent.Tool == this._defaultToolProvider.DefaultTool)
			{
				this.PutAsTopInputProcessor();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000239C File Offset: 0x0000059C
		public void EnterToolGroup(ToolGroupSpec toolGroup)
		{
			if (toolGroup != this.ActiveToolGroup)
			{
				this.ExitToolGroupInternal();
				this.EnterToolGroupInternal(toolGroup);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023B9 File Offset: 0x000005B9
		public void ExitToolGroup()
		{
			this.EnterToolGroup(null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023C2 File Offset: 0x000005C2
		public void EnterToolGroupInternal(ToolGroupSpec toolGroup)
		{
			this.ActiveToolGroup = toolGroup;
			this._eventBus.Post(new ToolGroupEnteredEvent(toolGroup));
			if (toolGroup != null)
			{
				this._inputService.AddInputProcessor(this);
				return;
			}
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002400 File Offset: 0x00000600
		public void ExitToolGroupInternal()
		{
			ToolGroupSpec activeToolGroup = this.ActiveToolGroup;
			this.ActiveToolGroup = null;
			this._eventBus.Post(new ToolGroupExitedEvent(activeToolGroup));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000242C File Offset: 0x0000062C
		public void PutAsTopInputProcessor()
		{
			if (this.ActiveToolGroup != null)
			{
				this._inputService.RemoveInputProcessor(this);
				this._inputService.AddInputProcessor(this);
			}
		}

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public readonly ISpecService _specService;

		// Token: 0x04000012 RID: 18
		public readonly InputService _inputService;

		// Token: 0x04000013 RID: 19
		public readonly IDefaultToolProvider _defaultToolProvider;

		// Token: 0x04000014 RID: 20
		public readonly Dictionary<string, ToolGroupSpec> _toolGroups = new Dictionary<string, ToolGroupSpec>();

		// Token: 0x04000015 RID: 21
		public readonly Dictionary<ITool, ToolGroupSpec> _assignedToolGroups = new Dictionary<ITool, ToolGroupSpec>();
	}
}
