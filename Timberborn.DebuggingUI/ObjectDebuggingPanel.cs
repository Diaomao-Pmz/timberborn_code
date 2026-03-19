using System;
using Timberborn.CoreUI;
using Timberborn.Debugging;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x0200000E RID: 14
	public class ObjectDebuggingPanel : ILoadableSingleton
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000030EB File Offset: 0x000012EB
		public ObjectDebuggingPanel(VisualElementLoader visualElementLoader, EventBus eventBus, UILayout uiLayout, DebugModeManager debugModeManager, DebugPanelMover debugPanelMover, ObjectSelector objectSelector, ObjectViewer objectViewer)
		{
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._uiLayout = uiLayout;
			this._debugModeManager = debugModeManager;
			this._debugPanelMover = debugPanelMover;
			this._objectSelector = objectSelector;
			this._objectViewer = objectViewer;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003128 File Offset: 0x00001328
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/DebuggingPanel/ObjectDebuggingPanel");
			this._eventBus.Register(this);
			this._uiLayout.AddAbsoluteItem(this._root);
			this._debugPanelMover.Initialize("ObjectDebuggingPanel", this._root, UQueryExtensions.Q<VisualElement>(this._root, "Wrapper", null));
			this._objectSelector.Initialize(UQueryExtensions.Q<VisualElement>(this._root, "ObjectSelector", null));
			this._objectSelector.SelectedObjectChanged += this.OnSelectedObjectChanged;
			this._objectSelector.ContextChanged += this.OnContextChanged;
			this._objectViewer.Initialize(UQueryExtensions.Q<ScrollView>(this._root, "ObjectViewer", null));
			this.UpdateEnabledState();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000031FB File Offset: 0x000013FB
		[OnEvent]
		public void OnDebugModeToggled(DebugModeToggledEvent debugModeToggledEvent)
		{
			this.UpdateEnabledState();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003203 File Offset: 0x00001403
		public void ResetPanelPosition()
		{
			this._debugPanelMover.ResetPanelPosition();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003210 File Offset: 0x00001410
		public void OnSelectedObjectChanged(object sender, object selectedObject)
		{
			this._objectViewer.SetObject(selectedObject);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000321E File Offset: 0x0000141E
		public void OnContextChanged(object sender, EventArgs e)
		{
			this._objectViewer.Clear();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000322C File Offset: 0x0000142C
		public void UpdateEnabledState()
		{
			if (this._debugModeManager.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				this._objectSelector.Enable();
				return;
			}
			this._root.ToggleDisplayStyle(false);
			this._objectSelector.Disable();
			this._objectViewer.Clear();
		}

		// Token: 0x0400003C RID: 60
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003D RID: 61
		public readonly EventBus _eventBus;

		// Token: 0x0400003E RID: 62
		public readonly UILayout _uiLayout;

		// Token: 0x0400003F RID: 63
		public readonly DebugModeManager _debugModeManager;

		// Token: 0x04000040 RID: 64
		public readonly DebugPanelMover _debugPanelMover;

		// Token: 0x04000041 RID: 65
		public readonly ObjectSelector _objectSelector;

		// Token: 0x04000042 RID: 66
		public readonly ObjectViewer _objectViewer;

		// Token: 0x04000043 RID: 67
		public VisualElement _root;
	}
}
