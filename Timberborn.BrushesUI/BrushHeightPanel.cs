using System;
using Timberborn.Brushes;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.BrushesUI
{
	// Token: 0x0200000C RID: 12
	public class BrushHeightPanel : IToolFragment
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000258F File Offset: 0x0000078F
		public BrushHeightPanel(EventBus eventBus, VisualElementLoader visualElementLoader, BindableButtonFactory bindableButtonFactory, KeyBindingShortcutService keyBindingShortcutService, MapSize mapSize)
		{
			this._eventBus = eventBus;
			this._visualElementLoader = visualElementLoader;
			this._bindableButtonFactory = bindableButtonFactory;
			this._keyBindingShortcutService = keyBindingShortcutService;
			this._mapSize = mapSize;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025BC File Offset: 0x000007BC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/BrushHeightPanel");
			this._brushHeightValue = UQueryExtensions.Q<Label>(this._root, "Height", null);
			this._increaseButton = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Plus", null), BrushHeightPanel.IncreaseBrushHeightKey, new Action(this.IncreaseBrushHeight), false);
			this._decreaseButton = this._bindableButtonFactory.Create(UQueryExtensions.Q<Button>(this._root, "Minus", null), BrushHeightPanel.DecreaseBrushHeightKey, new Action(this.DecreaseBrushHeight), false);
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(this._root, "Increase", null), BrushHeightPanel.IncreaseBrushHeightKey);
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(this._root, "Decrease", null), BrushHeightPanel.DecreaseBrushHeightKey);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026C0 File Offset: 0x000008C0
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._brushWithHeight = (toolEnteredEvent.Tool as IBrushWithHeight);
			if (this._brushWithHeight != null)
			{
				this._root.ToggleDisplayStyle(true);
				this._increaseButton.Bind();
				this._decreaseButton.Bind();
				this.UpdateBrushHeightValue();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000270E File Offset: 0x0000090E
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._root.ToggleDisplayStyle(false);
			this._increaseButton.Unbind();
			this._decreaseButton.Unbind();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002732 File Offset: 0x00000932
		public void IncreaseBrushHeight()
		{
			this._brushWithHeight.BrushHeight = Math.Min(this._brushWithHeight.BrushHeight + 1, this._mapSize.MaxMapEditorTerrainHeight);
			this.UpdateBrushHeightValue();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002762 File Offset: 0x00000962
		public void DecreaseBrushHeight()
		{
			this._brushWithHeight.BrushHeight = Math.Max(this._brushWithHeight.BrushHeight - 1, this._brushWithHeight.MinimumBrushHeight);
			this.UpdateBrushHeightValue();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002794 File Offset: 0x00000994
		public void UpdateBrushHeightValue()
		{
			this._brushHeightValue.text = this._brushWithHeight.BrushHeight.ToString();
		}

		// Token: 0x0400001C RID: 28
		public static readonly string IncreaseBrushHeightKey = "IncreaseBrushHeight";

		// Token: 0x0400001D RID: 29
		public static readonly string DecreaseBrushHeightKey = "DecreaseBrushHeight";

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000020 RID: 32
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000021 RID: 33
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x04000022 RID: 34
		public readonly MapSize _mapSize;

		// Token: 0x04000023 RID: 35
		public VisualElement _root;

		// Token: 0x04000024 RID: 36
		public Label _brushHeightValue;

		// Token: 0x04000025 RID: 37
		public IBrushWithHeight _brushWithHeight;

		// Token: 0x04000026 RID: 38
		public BindableButton _increaseButton;

		// Token: 0x04000027 RID: 39
		public BindableButton _decreaseButton;
	}
}
