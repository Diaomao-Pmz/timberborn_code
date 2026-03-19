using System;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockObjectTools;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Localization;
using Timberborn.RecoveredGoodSystem;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainSystemRendering;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;

namespace Timberborn.RecoveredGoodSystemUI
{
	// Token: 0x02000005 RID: 5
	public class RecoveredGoodStackDeletionTool : BlockObjectDeletionTool<RecoveredGoodStack>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000224C File Offset: 0x0000044C
		public RecoveredGoodStackDeletionTool(InputService inputService, AreaBlockObjectAndTerrainPicker areaBlockObjectAndTerrainPicker, EntityService entityService, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, CursorService cursorService, ILoc loc, BlockObjectModelBlockadeIgnorer blockObjectModelBlockadeIgnorer, ISpecService specService, ILevelVisibilityService levelVisibilityService, DialogBoxShower dialogBoxShower, TerrainDestroyer terrainDestroyer, TerrainHighlightingService terrainHighlightingService, IUndoRegistry undoRegistry) : base(inputService, areaBlockObjectAndTerrainPicker, entityService, blockObjectSelectionDrawerFactory, cursorService, blockObjectModelBlockadeIgnorer, specService, levelVisibilityService, dialogBoxShower, terrainDestroyer, terrainHighlightingService, undoRegistry)
		{
			this._loc = loc;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000227C File Offset: 0x0000047C
		public override ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(RecoveredGoodStackDeletionTool.ToolTitleLocKey)).AddSection(this._loc.T(RecoveredGoodStackDeletionTool.ToolDescriptionLocKey)).Build();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000022AD File Offset: 0x000004AD
		public override string CursorKey
		{
			get
			{
				return "DemolishResourcesCursor";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022B4 File Offset: 0x000004B4
		public override string ToolPromptLocKey
		{
			get
			{
				return "RecoveredGoodStack.DeletePrompt";
			}
		}

		// Token: 0x04000011 RID: 17
		public static readonly string ToolDescriptionLocKey = "DeletionTool.Description.RecoveredGoodStack";

		// Token: 0x04000012 RID: 18
		public static readonly string ToolTitleLocKey = "DeletionTool.Title.RecoveredGoodStack";

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;
	}
}
