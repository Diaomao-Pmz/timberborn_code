using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000010 RID: 16
	public class ToolGroupButton : IToolbarButton
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002CB5 File Offset: 0x00000EB5
		public VisualElement Root { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002CBD File Offset: 0x00000EBD
		public VisualElement ToolButtonsElement { get; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public ToolGroupButton(ILoc loc, ToolGroupService toolGroupService, ToolGroupSpec toolGroup, VisualElement root, VisualElement toolButtons, VisualElement buttonWrapper)
		{
			this._loc = loc;
			this._toolGroupService = toolGroupService;
			this._toolGroup = toolGroup;
			this.Root = root;
			this.ToolButtonsElement = toolButtons;
			this._toolGroupButtonWrapper = buttonWrapper;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D05 File Offset: 0x00000F05
		public ReadOnlyList<ToolButton> ToolButtons
		{
			get
			{
				return this._toolButtons.AsReadOnlyList<ToolButton>();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002D12 File Offset: 0x00000F12
		public bool IsVisible
		{
			get
			{
				return this._toolButtons.Any((ToolButton button) => button.ToolEnabled);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D3E File Offset: 0x00000F3E
		public bool IsActive
		{
			get
			{
				return this._toolGroupService.ActiveToolGroup == this._toolGroup;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D58 File Offset: 0x00000F58
		public void PostLoad()
		{
			UQueryExtensions.Q<Label>(this.Root, "Tooltip", null).text = this._loc.T(this._toolGroup.DisplayNameLocKey);
			UQueryExtensions.Q<VisualElement>(this.ToolButtonsElement, "EndSpacer", null).BringToFront();
			this._toolGroupButtonWrapper.ToggleDisplayStyle(this.IsVisible);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public void AddTool(ToolButton button)
		{
			this._toolButtons.Add(button);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DC6 File Offset: 0x00000FC6
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupOpenedEvent)
		{
			if (toolGroupOpenedEvent.ToolGroup == this._toolGroup)
			{
				this.ToolButtonsElement.ToggleDisplayStyle(true);
				this._toolGroupButtonWrapper.AddToClassList(ToolGroupButton.ActiveClassName);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DF7 File Offset: 0x00000FF7
		[OnEvent]
		public void OnToolGroupExited(ToolGroupExitedEvent toolGroupExitedEvent)
		{
			if (toolGroupExitedEvent.ToolGroup == this._toolGroup)
			{
				this.ToolButtonsElement.ToggleDisplayStyle(false);
				this._toolGroupButtonWrapper.RemoveFromClassList(ToolGroupButton.ActiveClassName);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E28 File Offset: 0x00001028
		[OnEvent]
		public void OnDevModeToggledEvent(DevModeToggledEvent devModeToggledEvent)
		{
			this._toolGroupButtonWrapper.ToggleDisplayStyle(this.IsVisible);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E3B File Offset: 0x0000103B
		public bool HasToolButton(ToolButton toolButton)
		{
			return this._toolButtons.Contains(toolButton);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E49 File Offset: 0x00001049
		public void Select()
		{
			this._toolGroupService.EnterToolGroup(this._toolGroup);
		}

		// Token: 0x04000037 RID: 55
		public static readonly string ActiveClassName = "button--active";

		// Token: 0x0400003A RID: 58
		public readonly ILoc _loc;

		// Token: 0x0400003B RID: 59
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x0400003C RID: 60
		public readonly VisualElement _toolGroupButtonWrapper;

		// Token: 0x0400003D RID: 61
		public readonly ToolGroupSpec _toolGroup;

		// Token: 0x0400003E RID: 62
		public readonly List<ToolButton> _toolButtons = new List<ToolButton>();
	}
}
