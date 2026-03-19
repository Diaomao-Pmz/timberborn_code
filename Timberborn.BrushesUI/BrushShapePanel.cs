using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
	// Token: 0x0200000D RID: 13
	public class BrushShapePanel : IToolFragment, IInputProcessor
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000027D5 File Offset: 0x000009D5
		public BrushShapePanel(EventBus eventBus, VisualElementLoader visualElementLoader, InputService inputService, ILoc loc, KeyBindingShortcutService keyBindingShortcutService)
		{
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._inputService = inputService;
			this._loc = loc;
			this._keyBindingShortcutService = keyBindingShortcutService;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002810 File Offset: 0x00000A10
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/BrushShapePanel");
			this._togglesContainer = UQueryExtensions.Q<VisualElement>(this._root, "Toggles", null);
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(this._root, "Binding", null), BrushShapePanel.ToggleBrushShapeKey);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			this._brushShapeValues = Enum.GetValues(typeof(BrushShape)).Cast<BrushShape>().ToImmutableArray<BrushShape>();
			this.InitializeToggles();
			return this._root;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028AE File Offset: 0x00000AAE
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._brushWithShape = (toolEnteredEvent.Tool as IBrushWithShape);
			if (this._brushWithShape != null)
			{
				this._inputService.AddInputProcessor(this);
				this.UpdateValue();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028E7 File Offset: 0x00000AE7
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			if (this._brushWithShape != null)
			{
				this._inputService.RemoveInputProcessor(this);
				this._root.ToggleDisplayStyle(false);
				this._brushWithShape = null;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002910 File Offset: 0x00000B10
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(BrushShapePanel.ToggleBrushShapeKey))
			{
				this.ToggleBrushShape();
				this.UpdateValue();
			}
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002934 File Offset: 0x00000B34
		public void InitializeToggles()
		{
			foreach (BrushShape brushShape in this._brushShapeValues)
			{
				this.AddToggle(brushShape);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002968 File Offset: 0x00000B68
		public void AddToggle(BrushShape brushShape)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/ToolPanelToggle");
			Toggle toggle = UQueryExtensions.Q<Toggle>(visualElement, "ToolPanelToggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> evt)
			{
				this.OnValueChanged(evt, brushShape);
			});
			toggle.text = this._loc.T(brushShape.GetLocKey());
			this._toggles.Add(brushShape, toggle);
			this._togglesContainer.Add(visualElement);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029F4 File Offset: 0x00000BF4
		public void OnValueChanged(ChangeEvent<bool> changeEvent, BrushShape brushShape)
		{
			if (changeEvent.newValue)
			{
				this._brushWithShape.BrushShape = brushShape;
			}
			this.UpdateValue();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A10 File Offset: 0x00000C10
		public void UpdateValue()
		{
			foreach (BrushShape brushShape in this._toggles.Keys)
			{
				this._toggles[brushShape].SetValueWithoutNotify(this._brushWithShape.BrushShape == brushShape);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A80 File Offset: 0x00000C80
		public void ToggleBrushShape()
		{
			BrushShape brushShape = this._brushWithShape.BrushShape;
			int index = (this._brushShapeValues.IndexOf(brushShape) + 1) % this._brushShapeValues.Length;
			BrushShape brushShape2 = this._brushShapeValues[index];
			this._brushWithShape.BrushShape = brushShape2;
		}

		// Token: 0x04000028 RID: 40
		public static readonly string ToggleBrushShapeKey = "ToggleBrushShape";

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly InputService _inputService;

		// Token: 0x0400002C RID: 44
		public readonly ILoc _loc;

		// Token: 0x0400002D RID: 45
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x0400002E RID: 46
		public VisualElement _root;

		// Token: 0x0400002F RID: 47
		public VisualElement _togglesContainer;

		// Token: 0x04000030 RID: 48
		public IBrushWithShape _brushWithShape;

		// Token: 0x04000031 RID: 49
		public readonly Dictionary<BrushShape, Toggle> _toggles = new Dictionary<BrushShape, Toggle>();

		// Token: 0x04000032 RID: 50
		public ImmutableArray<BrushShape> _brushShapeValues;
	}
}
