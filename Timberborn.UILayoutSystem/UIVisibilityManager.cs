using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.UILayoutSystem
{
	// Token: 0x02000009 RID: 9
	public class UIVisibilityManager : IInputProcessor, ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024E8 File Offset: 0x000006E8
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000024F0 File Offset: 0x000006F0
		public bool GUIVisible { get; private set; } = true;

		// Token: 0x06000024 RID: 36 RVA: 0x000024F9 File Offset: 0x000006F9
		public UIVisibilityManager(InputService inputService, EventBus eventBus)
		{
			this._inputService = inputService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002516 File Offset: 0x00000716
		public void Load()
		{
			this._eventBus.Register(this);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002530 File Offset: 0x00000730
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(UIVisibilityManager.ToggleGUIKey))
			{
				this.ToggleGUIVisibility();
				return true;
			}
			if (!this.GUIVisible && this._inputService.IsKeyDown(UIVisibilityManager.CancelKey))
			{
				this.ToggleGUIVisibility();
				return true;
			}
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000256F File Offset: 0x0000076F
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			if (!this.GUIVisible)
			{
				this.ToggleGUIVisibility();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002580 File Offset: 0x00000780
		public void ToggleGUIVisibility()
		{
			this.GUIVisible = !this.GUIVisible;
			Camera main = Camera.main;
			int cullingMask = main.cullingMask;
			LayerMask uimask = Layers.UIMask;
			if (this.GUIVisible)
			{
				main.cullingMask = (cullingMask | uimask);
			}
			else
			{
				main.cullingMask = (cullingMask & ~uimask);
			}
			this._eventBus.Post(new UIVisibilityChangedEvent(this.GUIVisible));
		}

		// Token: 0x0400001B RID: 27
		public static readonly string ToggleGUIKey = "ToggleGUI";

		// Token: 0x0400001C RID: 28
		public static readonly string CancelKey = "Cancel";

		// Token: 0x0400001E RID: 30
		public readonly InputService _inputService;

		// Token: 0x0400001F RID: 31
		public readonly EventBus _eventBus;
	}
}
