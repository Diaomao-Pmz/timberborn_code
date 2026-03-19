using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.InputSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.UISound;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200003C RID: 60
	public class PanelStack : IInputProcessor
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00004632 File Offset: 0x00002832
		public PanelStack(UISoundController uiSoundController, InputService inputService, VisualElementLoader visualElementLoader, EventBus eventBus, RootVisualElementProvider rootVisualElementProvider)
		{
			this._uiSoundController = uiSoundController;
			this._inputService = inputService;
			this._visualElementLoader = visualElementLoader;
			this._eventBus = eventBus;
			this._rootVisualElementProvider = rootVisualElementProvider;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000466C File Offset: 0x0000286C
		public VisualElement Initialize(string visualTreeAssetName, string containerName)
		{
			UIDocument uidocument = this._rootVisualElementProvider.CreateEmpty("PanelStack", 1);
			VisualTreeAsset visualTreeAsset = this._visualElementLoader.LoadVisualTreeAsset(visualTreeAssetName);
			uidocument.visualTreeAsset = visualTreeAsset;
			PanelTextSettings textSettings = uidocument.panelSettings.textSettings;
			uidocument.panelSettings.textSettings = null;
			uidocument.panelSettings.textSettings = textSettings;
			this._root = uidocument.rootVisualElement;
			this._container = UQueryExtensions.Q<VisualElement>(this._root, containerName, null);
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000046F4 File Offset: 0x000028F4
		[OnEvent]
		public void OnUIVisibilityChanged(UIVisibilityChangedEvent uiVisibilityChangedEvent)
		{
			this._root.ToggleDisplayStyle(uiVisibilityChangedEvent.UIVisible);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004708 File Offset: 0x00002908
		public bool ProcessInput()
		{
			if (this._inputService.UICancel)
			{
				return this.ProcessUICancel();
			}
			if (this._inputService.UIConfirm)
			{
				return this.ProcessUIConfirm();
			}
			return this.TopPanel.IsOverlay;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000474B File Offset: 0x0000294B
		public void Push(IPanelController panelController)
		{
			this.Push(panelController, false, false, false, true);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004758 File Offset: 0x00002958
		public void PushOverlay(IPanelController panelController)
		{
			this.Push(panelController, false, true, false, true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004765 File Offset: 0x00002965
		public void PushDialog(IPanelController panelController)
		{
			this.Push(panelController, false, true, true, true);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004772 File Offset: 0x00002972
		public void HideAndPushDialog(IPanelController panelController)
		{
			this.Push(panelController, true, true, true, true);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000477F File Offset: 0x0000297F
		public void HideAndPush(IPanelController panelController)
		{
			this.Push(panelController, true, false, false, true);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000478C File Offset: 0x0000298C
		public void HideAndPushWithoutPause(IPanelController panelController)
		{
			this.Push(panelController, true, false, false, false);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004799 File Offset: 0x00002999
		public void HideAndPushOverlay(IPanelController panelController)
		{
			this.Push(panelController, true, true, false, true);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000047A8 File Offset: 0x000029A8
		public void Pop(IPanelController panelController)
		{
			StackedPanel panel = this._stack.Pop();
			if (panelController != panel.PanelController)
			{
				throw new ArgumentException(string.Format("{0} {1} is not on top of the stack!", "IPanelController", panelController));
			}
			this.Hide(panel);
			if (panel.TopHidden && this._stack.Any<StackedPanel>())
			{
				this.ShowTop();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004804 File Offset: 0x00002A04
		public bool ContainsPanelBlocker()
		{
			return this._stack.Any((StackedPanel stack) => stack.PanelController is IPanelBlocker);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004830 File Offset: 0x00002A30
		public bool IsPanelOnTop(IPanelController panelController)
		{
			return !this._stack.IsEmpty<StackedPanel>() && this.TopPanel.PanelController == panelController;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000485D File Offset: 0x00002A5D
		public StackedPanel TopPanel
		{
			get
			{
				return this._stack.Peek();
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000486C File Offset: 0x00002A6C
		public bool ProcessUICancel()
		{
			this.TopPanel.PanelController.OnUICancelled();
			this._uiSoundController.PlayCancelSound();
			return true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004898 File Offset: 0x00002A98
		public bool ProcessUIConfirm()
		{
			if (this.TopPanel.PanelController.OnUIConfirmed())
			{
				this._uiSoundController.PlayClickSound();
			}
			return true;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000048C6 File Offset: 0x00002AC6
		public VisualElement GetPanel(IPanelController panelController, bool showOverlay)
		{
			if (showOverlay)
			{
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(PanelStack.OverlayKey);
				visualElement.Add(panelController.GetPanel());
				return visualElement;
			}
			return panelController.GetPanel();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000048F0 File Offset: 0x00002AF0
		public void Push(IPanelController panelController, bool hideTop, bool showOverlay, bool isDialog = false, bool lockSpeed = true)
		{
			if (hideTop && this._stack.Any<StackedPanel>())
			{
				this.Hide(this.TopPanel);
			}
			VisualElement panel = this.GetPanel(panelController, showOverlay);
			StackedPanel panel2 = new StackedPanel(panelController, panel, hideTop, showOverlay, isDialog, lockSpeed);
			this.Push(panel2);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004938 File Offset: 0x00002B38
		public void Push(StackedPanel panel)
		{
			this._stack.Push(panel);
			this.Show(panel);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004950 File Offset: 0x00002B50
		public void Show(StackedPanel panel)
		{
			VisualElement visualElement = panel.VisualElement;
			Focusable focusedElement = this._container.focusController.focusedElement;
			if (focusedElement != null)
			{
				focusedElement.Blur();
			}
			this._container.Add(visualElement);
			this._inputService.FlushUIInput();
			this._inputService.AddInputProcessor(this);
			this._eventBus.Post(new PanelShownEvent(panel.IsDialog, panel.LockSpeed));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000049C4 File Offset: 0x00002BC4
		public void Hide(StackedPanel panel)
		{
			Focusable focusedElement = this._container.focusController.focusedElement;
			if (focusedElement != null)
			{
				focusedElement.Blur();
			}
			this._container.Remove(panel.VisualElement);
			this._inputService.RemoveInputProcessor(this);
			this._eventBus.Post(new PanelHiddenEvent(this._stack.Any<StackedPanel>(), this._stack.All((StackedPanel stack) => !stack.LockSpeed), panel.IsDialog));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A58 File Offset: 0x00002C58
		public void ShowTop()
		{
			StackedPanel stackedPanel = this._stack.Pop();
			IPanelController panelController = stackedPanel.PanelController;
			VisualElement panel = this.GetPanel(panelController, stackedPanel.IsOverlay);
			StackedPanel panel2 = new StackedPanel(panelController, panel, stackedPanel.TopHidden, stackedPanel.IsOverlay, stackedPanel.IsDialog, stackedPanel.LockSpeed);
			this.Push(panel2);
		}

		// Token: 0x0400007C RID: 124
		public static readonly string OverlayKey = "Core/Overlay";

		// Token: 0x0400007D RID: 125
		public readonly UISoundController _uiSoundController;

		// Token: 0x0400007E RID: 126
		public readonly InputService _inputService;

		// Token: 0x0400007F RID: 127
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000080 RID: 128
		public readonly EventBus _eventBus;

		// Token: 0x04000081 RID: 129
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x04000082 RID: 130
		public VisualElement _root;

		// Token: 0x04000083 RID: 131
		public VisualElement _container;

		// Token: 0x04000084 RID: 132
		public readonly Stack<StackedPanel> _stack = new Stack<StackedPanel>();
	}
}
