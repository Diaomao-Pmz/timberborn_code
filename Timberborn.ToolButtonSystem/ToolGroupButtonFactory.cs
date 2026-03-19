using System;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000012 RID: 18
	public class ToolGroupButtonFactory
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002E7C File Offset: 0x0000107C
		public ToolGroupButtonFactory(EventBus eventBus, ToolGroupService toolGroupService, VisualElementLoader visualElementLoader, ToolButtonService toolButtonService, ILoc loc)
		{
			this._eventBus = eventBus;
			this._toolGroupService = toolGroupService;
			this._visualElementLoader = visualElementLoader;
			this._toolButtonService = toolButtonService;
			this._loc = loc;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EA9 File Offset: 0x000010A9
		public ToolGroupButton CreateGreen(ToolGroupSpec toolGroup)
		{
			return this.Create(toolGroup, ToolGroupButtonFactory.GreenClass);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002EB7 File Offset: 0x000010B7
		public ToolGroupButton CreateBlue(ToolGroupSpec toolGroup)
		{
			return this.Create(toolGroup, ToolGroupButtonFactory.BlueClass);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EC8 File Offset: 0x000010C8
		public ToolGroupButton Create(ToolGroupSpec toolGroup, string className)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/BottomBar/ToolGroupButton");
			UQueryExtensions.Q<VisualElement>(visualElement, "ToolGroupButtonWrapper", null).AddToClassList(className);
			this.InitializeElement(visualElement, toolGroup);
			ToolGroupButton toolGroupButton = new ToolGroupButton(this._loc, this._toolGroupService, toolGroup, visualElement, UQueryExtensions.Q<VisualElement>(visualElement, "ToolButtons", null), UQueryExtensions.Q<VisualElement>(visualElement, "ToolGroupButtonWrapper", null));
			this._eventBus.Register(toolGroupButton);
			this._toolButtonService.Add(toolGroupButton);
			return toolGroupButton;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F48 File Offset: 0x00001148
		public void InitializeElement(VisualElement root, ToolGroupSpec toolGroup)
		{
			Button button = UQueryExtensions.Q<Button>(root, "ToolGroupButton", null);
			Label tooltip = UQueryExtensions.Q<Label>(root, "Tooltip", null);
			button.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				tooltip.parent.ToggleDisplayStyle(true);
			}, 0);
			button.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				tooltip.parent.ToggleDisplayStyle(false);
			}, 0);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnButtonClick(tooltip, toolGroup);
			}, 0);
			button.style.backgroundImage = new StyleBackground(toolGroup.Icon.Asset);
			tooltip.parent.ToggleDisplayStyle(false);
			UQueryExtensions.Q<VisualElement>(root, "ToolButtons", null).ToggleDisplayStyle(false);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003002 File Offset: 0x00001202
		public void OnButtonClick(Label tooltip, ToolGroupSpec toolGroup)
		{
			if (this._toolGroupService.ActiveToolGroup == toolGroup)
			{
				this._toolGroupService.ExitToolGroup();
				return;
			}
			this._toolGroupService.EnterToolGroup(toolGroup);
			tooltip.parent.ToggleDisplayStyle(false);
		}

		// Token: 0x04000041 RID: 65
		public static readonly string GreenClass = "bottom-bar-button--green";

		// Token: 0x04000042 RID: 66
		public static readonly string BlueClass = "bottom-bar-button--blue";

		// Token: 0x04000043 RID: 67
		public readonly EventBus _eventBus;

		// Token: 0x04000044 RID: 68
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x04000045 RID: 69
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000046 RID: 70
		public readonly ToolButtonService _toolButtonService;

		// Token: 0x04000047 RID: 71
		public readonly ILoc _loc;
	}
}
