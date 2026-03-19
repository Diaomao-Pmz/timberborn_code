using System;
using Timberborn.BlueprintSystem;
using Timberborn.Brushes;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.BrushesUI
{
	// Token: 0x0200000F RID: 15
	public class BrushSizePanel : IToolFragment
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002AED File Offset: 0x00000CED
		public BrushSizePanel(EventBus eventBus, VisualElementLoader visualElementLoader, BindableButtonFactory bindableButtonFactory, KeyBindingShortcutService keyBindingShortcutService, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._bindableButtonFactory = bindableButtonFactory;
			this._keyBindingShortcutService = keyBindingShortcutService;
			this._specService = specService;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B1C File Offset: 0x00000D1C
		public VisualElement InitializeFragment()
		{
			BrushesSpec singleSpec = this._specService.GetSingleSpec<BrushesSpec>();
			this._maxBrushSize = singleSpec.MaxBrushSize;
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/BrushSizePanel");
			this._brushHeightValue = UQueryExtensions.Q<Label>(this._root, "Height", null);
			this._increaseButton = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Plus", null), BrushSizePanel.IncreaseBrushSizeKey, new Action(this.IncreaseBrushSize), false);
			this._decreaseButton = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Minus", null), BrushSizePanel.DecreaseBrushSizeKey, new Action(this.DecreaseBrushSize), false);
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(this._root, "Increase", null), BrushSizePanel.IncreaseBrushSizeKey);
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(this._root, "Decrease", null), BrushSizePanel.DecreaseBrushSizeKey);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C38 File Offset: 0x00000E38
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._brushWithSize = (toolEnteredEvent.Tool as IBrushWithSize);
			if (this._brushWithSize != null)
			{
				this._root.ToggleDisplayStyle(true);
				this._increaseButton.Bind();
				this._decreaseButton.Bind();
				this.UpdateBrushSizeValue();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C86 File Offset: 0x00000E86
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
			this._increaseButton.Unbind();
			this._decreaseButton.Unbind();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CAA File Offset: 0x00000EAA
		public void IncreaseBrushSize()
		{
			this._brushWithSize.BrushSize = Math.Min(this._brushWithSize.BrushSize + 1, this._maxBrushSize);
			this.UpdateBrushSizeValue();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public void DecreaseBrushSize()
		{
			this._brushWithSize.BrushSize = Math.Max(this._brushWithSize.BrushSize - 1, 1);
			this.UpdateBrushSizeValue();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CFC File Offset: 0x00000EFC
		public void UpdateBrushSizeValue()
		{
			this._brushHeightValue.text = this._brushWithSize.BrushSize.ToString();
		}

		// Token: 0x04000035 RID: 53
		public static readonly string IncreaseBrushSizeKey = "IncreaseBrushSize";

		// Token: 0x04000036 RID: 54
		public static readonly string DecreaseBrushSizeKey = "DecreaseBrushSize";

		// Token: 0x04000037 RID: 55
		public readonly EventBus _eventBus;

		// Token: 0x04000038 RID: 56
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000039 RID: 57
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400003A RID: 58
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x0400003B RID: 59
		public readonly ISpecService _specService;

		// Token: 0x0400003C RID: 60
		public VisualElement _root;

		// Token: 0x0400003D RID: 61
		public Label _brushHeightValue;

		// Token: 0x0400003E RID: 62
		public IBrushWithSize _brushWithSize;

		// Token: 0x0400003F RID: 63
		public BindableButton _increaseButton;

		// Token: 0x04000040 RID: 64
		public BindableButton _decreaseButton;

		// Token: 0x04000041 RID: 65
		public int _maxBrushSize;
	}
}
