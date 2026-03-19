using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Localization;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainSystemRendering;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200001B RID: 27
	public class EntityBlockObjectDeletionTool : BlockObjectDeletionTool<EntityComponent>, IDevModeTool, ITool
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000349C File Offset: 0x0000169C
		public EntityBlockObjectDeletionTool(InputService inputService, AreaBlockObjectAndTerrainPicker areaBlockObjectAndTerrainPicker, EntityService entityService, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, CursorService cursorService, ILoc loc, BlockObjectModelBlockadeIgnorer blockObjectModelBlockadeIgnorer, ISpecService specService, ILevelVisibilityService levelVisibilityService, DialogBoxShower dialogBoxShower, TerrainDestroyer terrainDestroyer, TerrainHighlightingService terrainHighlightingService, IUndoRegistry undoRegistry) : base(inputService, areaBlockObjectAndTerrainPicker, entityService, blockObjectSelectionDrawerFactory, cursorService, blockObjectModelBlockadeIgnorer, specService, levelVisibilityService, dialogBoxShower, terrainDestroyer, terrainHighlightingService, undoRegistry)
		{
			this._loc = loc;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002302 File Offset: 0x00000502
		public bool IsDevMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000034D7 File Offset: 0x000016D7
		public override ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(EntityBlockObjectDeletionTool.ToolTitleLocKey)).AddSection(this._loc.T(EntityBlockObjectDeletionTool.ToolDescriptionLocKey)).Build();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003508 File Offset: 0x00001708
		public override string ToolPromptLocKey
		{
			get
			{
				return "DeletionTool.Prompt.Objects";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000350F File Offset: 0x0000170F
		public override string CursorKey
		{
			get
			{
				return "DeleteBuildingCursor";
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003516 File Offset: 0x00001716
		public override bool IsBlockObjectValid(BlockObject blockObject)
		{
			return !this.IsStackedDeletionBlocked(blockObject);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003524 File Offset: 0x00001724
		public bool IsStackedDeletionBlocked(BlockObject blockObject)
		{
			blockObject.GetComponents<IBlockObjectDeletionBlocker>(this._deletionBlockers);
			bool result = (from blocker in this._deletionBlockers
			where blocker.NoForcedDelete
			select blocker).Any((IBlockObjectDeletionBlocker blocker) => blocker.IsStackedDeletionBlocked);
			this._deletionBlockers.Clear();
			return result;
		}

		// Token: 0x04000058 RID: 88
		public static readonly string ToolDescriptionLocKey = "DeletionTool.Description.Objects";

		// Token: 0x04000059 RID: 89
		public static readonly string ToolTitleLocKey = "DeletionTool.Title.Objects";

		// Token: 0x0400005A RID: 90
		public readonly ILoc _loc;

		// Token: 0x0400005B RID: 91
		public readonly List<IBlockObjectDeletionBlocker> _deletionBlockers = new List<IBlockObjectDeletionBlocker>();
	}
}
