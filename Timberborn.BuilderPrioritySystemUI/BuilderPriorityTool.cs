using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.Common;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PrioritySystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UISound;
using UnityEngine;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x02000010 RID: 16
	public class BuilderPriorityTool : ITool, IToolDescriptor, IInputProcessor
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000026E8 File Offset: 0x000008E8
		public BuilderPriorityTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, InputService inputService, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, CursorService cursorService, ILoc loc, BuilderPrioritizableHighlighter builderPrioritizableHighlighter, UISoundController uiSoundController)
		{
			this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
			this._inputService = inputService;
			this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
			this._cursorService = cursorService;
			this._loc = loc;
			this._builderPrioritizableHighlighter = builderPrioritizableHighlighter;
			this._uiSoundController = uiSoundController;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000273C File Offset: 0x0000093C
		public void Initialize(Priority priority, BuilderPriorityToolSpec spec)
		{
			this._priority = priority;
			this._areaBlockObjectPicker = this._areaBlockObjectPickerFactory.CreatePickingDownwards();
			this._highlightSelectionDrawer = this._blockObjectSelectionDrawerFactory.Create(spec.PriorityHighlightColor, spec.PriorityTileColor, spec.PrioritySideColor);
			this._actionSelectionDrawer = this._blockObjectSelectionDrawerFactory.Create(spec.PriorityActionColor, spec.PriorityTileColor, spec.PrioritySideColor);
			this.InitializeToolDescription();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027AD File Offset: 0x000009AD
		public bool ProcessInput()
		{
			return this._areaBlockObjectPicker.PickBlockObjects<BuilderPrioritizable>(new AreaBlockObjectPicker.Callback(this.PreviewCallback), new AreaBlockObjectPicker.Callback(this.ActionCallback), new Action(this.ShowNoneCallback), null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027DF File Offset: 0x000009DF
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(BuilderPriorityTool.CursorKey);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027FD File Offset: 0x000009FD
		public void Exit()
		{
			this._cursorService.ResetCursor();
			this._areaBlockObjectPicker.Reset();
			this._highlightSelectionDrawer.StopDrawing();
			this._actionSelectionDrawer.StopDrawing();
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002837 File Offset: 0x00000A37
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002840 File Offset: 0x00000A40
		public void PreviewCallback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			IEnumerable<BlockObject> blockObjects2 = blockObjects.Where(delegate(BlockObject bo)
			{
				BuilderPrioritizable component = bo.GetComponent<BuilderPrioritizable>();
				return component != null && component.Enabled;
			});
			if (selectionStarted && !selectingArea)
			{
				this._actionSelectionDrawer.Draw(blockObjects2, start, end, false);
				return;
			}
			if (selectingArea)
			{
				this._actionSelectionDrawer.Draw(blockObjects2, start, end, true);
				return;
			}
			this._highlightSelectionDrawer.Draw(blockObjects2, start, end, false);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028B0 File Offset: 0x00000AB0
		public void ActionCallback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			IEnumerable<ValueTuple<BlockObject, BuilderPrioritizable>> collection = from bo in blockObjects
			select new ValueTuple<BlockObject, BuilderPrioritizable>(bo, bo.GetComponent<BuilderPrioritizable>()) into tuple
			where tuple.Item2 && tuple.Item2.Enabled
			select tuple;
			this._builderPrioritizables.AddRange(collection);
			foreach (ValueTuple<BlockObject, BuilderPrioritizable> valueTuple in this._builderPrioritizables)
			{
				valueTuple.Item2.SetPriority(this._priority);
			}
			if (!this._builderPrioritizables.IsEmpty<ValueTuple<BlockObject, BuilderPrioritizable>>())
			{
				this._builderPrioritizableHighlighter.HighlightAll();
				this._actionSelectionDrawer.Draw(from tuple in this._builderPrioritizables
				select tuple.Item1, start, end, selectingArea);
				this._uiSoundController.PlayClickSound();
			}
			this._builderPrioritizables.Clear();
			this.ClearHighlights();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029D0 File Offset: 0x00000BD0
		public void ShowNoneCallback()
		{
			this.ClearHighlights();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029D8 File Offset: 0x00000BD8
		public void ClearHighlights()
		{
			this._highlightSelectionDrawer.StopDrawing();
			this._actionSelectionDrawer.StopDrawing();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029F0 File Offset: 0x00000BF0
		public void InitializeToolDescription()
		{
			string title = this._loc.T("Priorities." + this._priority.ToString());
			this._toolDescription = new ToolDescription.Builder(title).AddSection(this._loc.T(BuilderPriorityTool.DescriptionKey)).AddPrioritizedSection(this._loc.T(BuilderPriorityTool.TipKey)).Build();
		}

		// Token: 0x0400002A RID: 42
		public static readonly string CursorKey = "PriorityCursor";

		// Token: 0x0400002B RID: 43
		public static readonly string DescriptionKey = "BuilderPriorityTool.Description";

		// Token: 0x0400002C RID: 44
		public static readonly string TipKey = "BuilderPriorityTool.Tip";

		// Token: 0x0400002D RID: 45
		public readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

		// Token: 0x0400002E RID: 46
		public readonly InputService _inputService;

		// Token: 0x0400002F RID: 47
		public readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

		// Token: 0x04000030 RID: 48
		public readonly CursorService _cursorService;

		// Token: 0x04000031 RID: 49
		public readonly ILoc _loc;

		// Token: 0x04000032 RID: 50
		public readonly BuilderPrioritizableHighlighter _builderPrioritizableHighlighter;

		// Token: 0x04000033 RID: 51
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000034 RID: 52
		public BlockObjectSelectionDrawer _highlightSelectionDrawer;

		// Token: 0x04000035 RID: 53
		public BlockObjectSelectionDrawer _actionSelectionDrawer;

		// Token: 0x04000036 RID: 54
		public AreaBlockObjectPicker _areaBlockObjectPicker;

		// Token: 0x04000037 RID: 55
		public ToolDescription _toolDescription;

		// Token: 0x04000038 RID: 56
		public Priority _priority;

		// Token: 0x04000039 RID: 57
		public readonly List<ValueTuple<BlockObject, BuilderPrioritizable>> _builderPrioritizables = new List<ValueTuple<BlockObject, BuilderPrioritizable>>();
	}
}
