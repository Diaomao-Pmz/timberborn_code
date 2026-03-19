using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000004 RID: 4
	public class DebuggingPanel : IUpdatableSingleton, ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public DebuggingPanel(UILayout uiLayout, VisualElementLoader visualElementLoader, EventBus eventBus, DebugModeManager debugModeManager, ISettings settings, DebugPanelMover debugPanelMover)
		{
			this._uiLayout = uiLayout;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._debugModeManager = debugModeManager;
			this._settings = settings;
			this._debugPanelMover = debugPanelMover;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002100 File Offset: 0x00000300
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/DebuggingPanel/DebuggingPanel");
			this._root.ToggleDisplayStyle(this._debugModeManager.Enabled);
			this._eventBus.Register(this);
			this._uiLayout.AddAbsoluteItem(this._root);
			this._content = UQueryExtensions.Q<VisualElement>(this._root, "Content", null);
			this._debugPanelMover.Initialize("DebuggingPanel", this._root, this._content);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000218C File Offset: 0x0000038C
		public void UpdateSingleton()
		{
			if (this._debugModeManager.Enabled)
			{
				foreach (DebuggingPanelItem debuggingPanelItem in this._debuggingPanelItems)
				{
					debuggingPanelItem.UpdateText();
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021EC File Offset: 0x000003EC
		public void AddDebuggingPanel(IDebuggingPanel debuggingPanel, string title)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/DebuggingPanel/DebuggingPanelItem");
			this._content.Add(visualElement);
			DebuggingPanelItem debuggingPanelItem = new DebuggingPanelItem(this._settings, debuggingPanel, visualElement, title);
			debuggingPanelItem.Initialize();
			this._debuggingPanelItems.Add(debuggingPanelItem);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002237 File Offset: 0x00000437
		[OnEvent]
		public void OnDebugModeToggled(DebugModeToggledEvent debugModeToggledEvent)
		{
			this._root.ToggleDisplayStyle(debugModeToggledEvent.Enabled);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000224A File Offset: 0x0000044A
		public void ResetPanelPosition()
		{
			this._debugPanelMover.ResetPanelPosition();
		}

		// Token: 0x04000006 RID: 6
		public readonly UILayout _uiLayout;

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x0400000A RID: 10
		public readonly ISettings _settings;

		// Token: 0x0400000B RID: 11
		public readonly DebugPanelMover _debugPanelMover;

		// Token: 0x0400000C RID: 12
		public VisualElement _root;

		// Token: 0x0400000D RID: 13
		public VisualElement _content;

		// Token: 0x0400000E RID: 14
		public readonly List<DebuggingPanelItem> _debuggingPanelItems = new List<DebuggingPanelItem>();
	}
}
