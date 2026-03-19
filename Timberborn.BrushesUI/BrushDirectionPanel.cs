using System;
using Timberborn.Brushes;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.KeyBindingSystemUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.BrushesUI
{
	// Token: 0x02000007 RID: 7
	public class BrushDirectionPanel : IToolFragment, IInputProcessor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BrushDirectionPanel(EventBus eventBus, VisualElementLoader visualElementLoader, InputService inputService, ILoc loc, KeyBindingShortcutService keyBindingShortcutService)
		{
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._inputService = inputService;
			this._loc = loc;
			this._keyBindingShortcutService = keyBindingShortcutService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/BrushDirectionPanel");
			this._togglesContainer = UQueryExtensions.Q<VisualElement>(this._root, "Toggles", null);
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(this._root, "Binding", null), BrushDirectionPanel.InverseBrushDirectionKey);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			this.InitializeToggles();
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021AB File Offset: 0x000003AB
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._brushWithDirection = (toolEnteredEvent.Tool as IBrushWithDirection);
			if (this._brushWithDirection != null)
			{
				this._inputService.AddInputProcessor(this);
				this.UpdateValue();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E4 File Offset: 0x000003E4
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			if (this._brushWithDirection != null)
			{
				this._inputService.RemoveInputProcessor(this);
				this._root.ToggleDisplayStyle(false);
				this._brushWithDirection.Inverse = false;
				this._brushWithDirection = null;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002219 File Offset: 0x00000419
		public bool ProcessInput()
		{
			this._brushWithDirection.Inverse = this._inputService.IsKeyHeld(BrushDirectionPanel.InverseBrushDirectionKey);
			this.UpdateValue();
			return false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000223D File Offset: 0x0000043D
		public void InitializeToggles()
		{
			this._increaseToggle = this.AddToggle(true);
			this._decreaseToggle = this.AddToggle(false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225C File Offset: 0x0000045C
		public Toggle AddToggle(bool increaseToggle)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/ToolPanelToggle");
			Toggle toggle = UQueryExtensions.Q<Toggle>(visualElement, "ToolPanelToggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> evt)
			{
				this.OnValueChanged(evt, increaseToggle);
			});
			toggle.text = this._loc.T(increaseToggle ? BrushDirectionPanel.IncreaseLocKey : BrushDirectionPanel.DecreaseLocKey);
			this._togglesContainer.Add(visualElement);
			return toggle;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022DE File Offset: 0x000004DE
		public void OnValueChanged(ChangeEvent<bool> changeEvent, bool isIncreaseToggle)
		{
			if (changeEvent.newValue)
			{
				this._brushWithDirection.Increase = isIncreaseToggle;
				return;
			}
			this._brushWithDirection.Increase = !isIncreaseToggle;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002304 File Offset: 0x00000504
		public void UpdateValue()
		{
			this._increaseToggle.SetValueWithoutNotify(this._brushWithDirection.IsIncreasing);
			this._decreaseToggle.SetValueWithoutNotify(!this._brushWithDirection.IsIncreasing);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string IncreaseLocKey = "MapEditor.Brush.Direction.Raise";

		// Token: 0x04000009 RID: 9
		public static readonly string DecreaseLocKey = "MapEditor.Brush.Direction.Lower";

		// Token: 0x0400000A RID: 10
		public static readonly string InverseBrushDirectionKey = "InverseBrushDirection";

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000D RID: 13
		public readonly InputService _inputService;

		// Token: 0x0400000E RID: 14
		public readonly ILoc _loc;

		// Token: 0x0400000F RID: 15
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x04000010 RID: 16
		public VisualElement _root;

		// Token: 0x04000011 RID: 17
		public VisualElement _togglesContainer;

		// Token: 0x04000012 RID: 18
		public IBrushWithDirection _brushWithDirection;

		// Token: 0x04000013 RID: 19
		public Toggle _increaseToggle;

		// Token: 0x04000014 RID: 20
		public Toggle _decreaseToggle;
	}
}
