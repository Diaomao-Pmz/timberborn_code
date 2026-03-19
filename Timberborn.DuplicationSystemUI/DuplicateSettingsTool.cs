using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.ConstructionMode;
using Timberborn.DuplicationSystem;
using Timberborn.EntityUndoSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UISound;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x02000009 RID: 9
	public class DuplicateSettingsTool : ITool, IToolDescriptor, IConstructionModeEnabler, ILoadableSingleton, IInputProcessor
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002344 File Offset: 0x00000544
		public DuplicateSettingsTool(InputService inputService, ToolService toolService, SelectableObjectRaycaster selectableObjectRaycaster, Highlighter highlighter, RollingHighlighter rollingHighlighter, ISpecService specService, Duplicator duplicator, CursorService cursorService, ILoc loc, UISoundController uiSoundController, EntityChangeRecorderFactory entityChangeRecorderFactory, DuplicationValidator duplicationValidator)
		{
			this._inputService = inputService;
			this._toolService = toolService;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
			this._highlighter = highlighter;
			this._rollingHighlighter = rollingHighlighter;
			this._specService = specService;
			this._duplicator = duplicator;
			this._cursorService = cursorService;
			this._loc = loc;
			this._uiSoundController = uiSoundController;
			this._entityChangeRecorderFactory = entityChangeRecorderFactory;
			this._duplicationValidator = duplicationValidator;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023B4 File Offset: 0x000005B4
		public void Load()
		{
			this._duplicationSystemColorsSpec = this._specService.GetSingleSpec<DuplicationSystemColorsSpec>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023C7 File Offset: 0x000005C7
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(DuplicateSettingsTool.CursorKey);
			this._highlighter.HighlightPrimary(this._source, this._duplicationSystemColorsSpec.SourceColor);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002401 File Offset: 0x00000601
		public void Exit()
		{
			this._source = null;
			this._inputService.RemoveInputProcessor(this);
			this._cursorService.ResetCursor();
			this._highlighter.UnhighlightAllPrimary();
			this._rollingHighlighter.UnhighlightAllPrimary();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002437 File Offset: 0x00000637
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder().AddPrioritizedSection(this._loc.T(DuplicateSettingsTool.DescriptionLocKey)).Build();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002458 File Offset: 0x00000658
		public bool ProcessInput()
		{
			if (!this._source)
			{
				this._toolService.SwitchToDefaultTool();
				return true;
			}
			SelectableObject selectableObject;
			if (this._selectableObjectRaycaster.TryHitSelectableObject(out selectableObject) && this._duplicationValidator.CanDuplicateSettings(selectableObject))
			{
				if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
				{
					this.DuplicateTo(selectableObject);
					this._uiSoundController.PlayClickSound();
					return true;
				}
				if (this._inputService.MainMouseButtonHeld)
				{
					this._rollingHighlighter.UnhighlightAllPrimary();
				}
				else
				{
					this._rollingHighlighter.HighlightPrimary(selectableObject, this._duplicationSystemColorsSpec.TargetColor);
				}
			}
			else
			{
				this._rollingHighlighter.UnhighlightAllPrimary();
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002509 File Offset: 0x00000709
		public void ActivateWithSource(BaseComponent source)
		{
			this._source = source;
			this._toolService.SwitchTool(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002520 File Offset: 0x00000720
		public void DuplicateTo(BaseComponent target)
		{
			using (this._entityChangeRecorderFactory.CreateChangeRecorder(target))
			{
				this._duplicator.Duplicate(this._source, target);
			}
		}

		// Token: 0x0400001B RID: 27
		public static readonly string DescriptionLocKey = "Duplication.DuplicateSettingsToolDescription";

		// Token: 0x0400001C RID: 28
		public static readonly string CursorKey = "DuplicateSettingsCursor";

		// Token: 0x0400001D RID: 29
		public readonly InputService _inputService;

		// Token: 0x0400001E RID: 30
		public readonly ToolService _toolService;

		// Token: 0x0400001F RID: 31
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;

		// Token: 0x04000020 RID: 32
		public readonly Highlighter _highlighter;

		// Token: 0x04000021 RID: 33
		public readonly RollingHighlighter _rollingHighlighter;

		// Token: 0x04000022 RID: 34
		public readonly ISpecService _specService;

		// Token: 0x04000023 RID: 35
		public readonly Duplicator _duplicator;

		// Token: 0x04000024 RID: 36
		public readonly CursorService _cursorService;

		// Token: 0x04000025 RID: 37
		public readonly ILoc _loc;

		// Token: 0x04000026 RID: 38
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000027 RID: 39
		public readonly EntityChangeRecorderFactory _entityChangeRecorderFactory;

		// Token: 0x04000028 RID: 40
		public readonly DuplicationValidator _duplicationValidator;

		// Token: 0x04000029 RID: 41
		public DuplicationSystemColorsSpec _duplicationSystemColorsSpec;

		// Token: 0x0400002A RID: 42
		public BaseComponent _source;

		// Token: 0x0400002B RID: 43
		public BaseComponent _lastTarget;
	}
}
