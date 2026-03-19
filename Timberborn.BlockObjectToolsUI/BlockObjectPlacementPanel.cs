using System;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using Timberborn.SingletonSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using UnityEngine.UIElements;

namespace Timberborn.BlockObjectToolsUI
{
	// Token: 0x02000004 RID: 4
	public class BlockObjectPlacementPanel : IToolFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public BlockObjectPlacementPanel(BindableButtonFactory bindableButtonFactory, EventBus eventBus, KeyBindingShortcutService keyBindingShortcutService, VisualElementLoader visualElementLoader, PreviewPlacement previewPlacement)
		{
			this._bindableButtonFactory = bindableButtonFactory;
			this._eventBus = eventBus;
			this._keyBindingShortcutService = keyBindingShortcutService;
			this._visualElementLoader = visualElementLoader;
			this._previewPlacement = previewPlacement;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public VisualElement InitializeFragment()
		{
			string elementName = "Common/ToolPanel/BlockObjectPlacementPanel";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._rotateClockwiseBindableButton = this.CreateBindableButton(UQueryExtensions.Q<Button>(this._root, "RotateClockwise", null), BlockObjectPlacementPanel.RotateClockwiseKey, new Action(this.RotateClockwise));
			this._rotateCounterclockwiseBindableButton = this.CreateBindableButton(UQueryExtensions.Q<Button>(this._root, "RotateCounterclockwise", null), BlockObjectPlacementPanel.RotateCounterclockwiseKey, new Action(this.RotateCounterclockwise));
			this._flipButton = UQueryExtensions.Q<Button>(this._root, "Flip", null);
			this._flipIcon = UQueryExtensions.Q<VisualElement>(this._root, "FlipIcon", null);
			this._flipBindableButton = this.CreateBindableButton(this._flipButton, BlockObjectPlacementPanel.FlipKey, new Action(this.Flip));
			this._eventBus.Register(this);
			this.Hide();
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021D8 File Offset: 0x000003D8
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			this._tool = (toolEnteredEvent.Tool as BlockObjectTool);
			this._root.ToggleDisplayStyle(this._tool != null);
			if (this._tool != null)
			{
				this._rotateClockwiseBindableButton.Bind();
				this._rotateCounterclockwiseBindableButton.Bind();
				bool flippable = this._tool.Template.GetSpec<BlockObjectSpec>().Flippable;
				if (flippable)
				{
					this._previewPlacement.EnableFlipping();
					this._flipBindableButton.Bind();
				}
				else
				{
					this._previewPlacement.DisableFlipping();
				}
				this._flipButton.ToggleDisplayStyle(flippable);
				this.UpdateFlipIcon();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002276 File Offset: 0x00000476
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this.Hide();
			this._rotateClockwiseBindableButton.Unbind();
			this._rotateCounterclockwiseBindableButton.Unbind();
			this._flipBindableButton.Unbind();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000229F File Offset: 0x0000049F
		public BindableButton CreateBindableButton(Button button, string key, Action action)
		{
			this._keyBindingShortcutService.CreateAny(UQueryExtensions.Q<Label>(button, "Binding", null), key);
			return this._bindableButtonFactory.Create(button, key, action, true);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022C8 File Offset: 0x000004C8
		public void RotateClockwise()
		{
			this._previewPlacement.RotateClockwise();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022D5 File Offset: 0x000004D5
		public void RotateCounterclockwise()
		{
			this._previewPlacement.RotateCounterclockwise();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E2 File Offset: 0x000004E2
		public void Flip()
		{
			this._previewPlacement.Flip();
			this.UpdateFlipIcon();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022F5 File Offset: 0x000004F5
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002304 File Offset: 0x00000504
		public void UpdateFlipIcon()
		{
			this._flipIcon.EnableInClassList(BlockObjectPlacementPanel.UnflippedIconClass, this._previewPlacement.FlipMode.IsUnflipped);
			this._flipIcon.EnableInClassList(BlockObjectPlacementPanel.FlippedIconClass, this._previewPlacement.FlipMode.IsFlipped);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string RotateClockwiseKey = "RotateClockwise";

		// Token: 0x04000007 RID: 7
		public static readonly string RotateCounterclockwiseKey = "RotateCounterclockwise";

		// Token: 0x04000008 RID: 8
		public static readonly string FlipKey = "Flip";

		// Token: 0x04000009 RID: 9
		public static readonly string UnflippedIconClass = "block-object-placement-panel__button-image--unflipped";

		// Token: 0x0400000A RID: 10
		public static readonly string FlippedIconClass = "block-object-placement-panel__button-image--flipped";

		// Token: 0x0400000B RID: 11
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400000C RID: 12
		public readonly EventBus _eventBus;

		// Token: 0x0400000D RID: 13
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly PreviewPlacement _previewPlacement;

		// Token: 0x04000010 RID: 16
		public BlockObjectTool _tool;

		// Token: 0x04000011 RID: 17
		public VisualElement _root;

		// Token: 0x04000012 RID: 18
		public BindableButton _rotateClockwiseBindableButton;

		// Token: 0x04000013 RID: 19
		public BindableButton _rotateCounterclockwiseBindableButton;

		// Token: 0x04000014 RID: 20
		public BindableButton _flipBindableButton;

		// Token: 0x04000015 RID: 21
		public Button _flipButton;

		// Token: 0x04000016 RID: 22
		public VisualElement _flipIcon;
	}
}
