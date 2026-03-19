using System;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlueprintSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PrioritySystem;
using Timberborn.ToolButtonSystem;
using Timberborn.UISound;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x02000009 RID: 9
	public class BuilderPrioritiesButtonFactory
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002280 File Offset: 0x00000480
		public BuilderPrioritiesButtonFactory(BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, BuilderPrioritizableHighlighter builderPrioritizableHighlighter, AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, BuilderPrioritySpriteLoader builderPrioritySpriteLoader, UISoundController uiSoundController, ToolButtonFactory toolButtonFactory, CursorService cursorService, InputService inputService, ISpecService specService, ILoc loc)
		{
			this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
			this._builderPrioritizableHighlighter = builderPrioritizableHighlighter;
			this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
			this._builderPrioritySpriteLoader = builderPrioritySpriteLoader;
			this._uiSoundController = uiSoundController;
			this._toolButtonFactory = toolButtonFactory;
			this._cursorService = cursorService;
			this._inputService = inputService;
			this._specService = specService;
			this._loc = loc;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		public ToolButton CreateButton(Priority priority, VisualElement parent)
		{
			BuilderPriorityTool tool = this.CreateTool(priority);
			Sprite toolImage = this._builderPrioritySpriteLoader.LoadButtonSprite(priority);
			return this._toolButtonFactory.Create(tool, toolImage, parent);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
		public BuilderPriorityTool CreateTool(Priority priority)
		{
			BuilderPriorityTool builderPriorityTool = new BuilderPriorityTool(this._areaBlockObjectPickerFactory, this._inputService, this._blockObjectSelectionDrawerFactory, this._cursorService, this._loc, this._builderPrioritizableHighlighter, this._uiSoundController);
			BuilderPriorityToolSpec singleSpec = this._specService.GetSingleSpec<BuilderPriorityToolSpec>();
			builderPriorityTool.Initialize(priority, singleSpec);
			return builderPriorityTool;
		}

		// Token: 0x04000010 RID: 16
		public readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

		// Token: 0x04000011 RID: 17
		public readonly BuilderPrioritizableHighlighter _builderPrioritizableHighlighter;

		// Token: 0x04000012 RID: 18
		public readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

		// Token: 0x04000013 RID: 19
		public readonly BuilderPrioritySpriteLoader _builderPrioritySpriteLoader;

		// Token: 0x04000014 RID: 20
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000015 RID: 21
		public readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x04000016 RID: 22
		public readonly CursorService _cursorService;

		// Token: 0x04000017 RID: 23
		public readonly InputService _inputService;

		// Token: 0x04000018 RID: 24
		public readonly ISpecService _specService;

		// Token: 0x04000019 RID: 25
		public readonly ILoc _loc;
	}
}
