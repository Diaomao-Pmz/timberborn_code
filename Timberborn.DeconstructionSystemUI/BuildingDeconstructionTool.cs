using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockObjectTools;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Localization;
using Timberborn.RecoverableGoodSystemUI;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainSystemRendering;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DeconstructionSystemUI
{
	// Token: 0x02000004 RID: 4
	public class BuildingDeconstructionTool : BlockObjectDeletionTool<BuildingSpec>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public BuildingDeconstructionTool(InputService inputService, AreaBlockObjectAndTerrainPicker areaBlockObjectAndTerrainPicker, EntityService entityService, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, CursorService cursorService, ILoc loc, BlockObjectModelBlockadeIgnorer blockObjectModelBlockadeIgnorer, ISpecService specService, ILevelVisibilityService levelVisibilityService, DialogBoxShower dialogBoxShower, RecoverableGoodElementFactory recoverableGoodElementFactory, RecoverableGoodTooltip recoverableGoodTooltip, TerrainDestroyer terrainDestroyer, TerrainHighlightingService terrainHighlightingService, IUndoRegistry undoRegistry) : base(inputService, areaBlockObjectAndTerrainPicker, entityService, blockObjectSelectionDrawerFactory, cursorService, blockObjectModelBlockadeIgnorer, specService, levelVisibilityService, dialogBoxShower, terrainDestroyer, terrainHighlightingService, undoRegistry)
		{
			this._loc = loc;
			this._recoverableGoodElementFactory = recoverableGoodElementFactory;
			this._recoverableGoodTooltip = recoverableGoodTooltip;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002116 File Offset: 0x00000316
		public void TogglePreview()
		{
			this._previewDisabled = !this._previewDisabled;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002127 File Offset: 0x00000327
		public override ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(BuildingDeconstructionTool.ToolTitleLocKey)).AddSection(this._loc.T(BuildingDeconstructionTool.DescriptionLocKey)).Build();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		public override void Enter()
		{
			base.Enter();
			this._recoverableGoodTooltip.Enable();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000216B File Offset: 0x0000036B
		public override void Exit()
		{
			base.Exit();
			this._recoverableGoodTooltip.Disable();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000217E File Offset: 0x0000037E
		public override string ToolPromptLocKey
		{
			get
			{
				return "DeletionTool.Prompt.Buildings";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002185 File Offset: 0x00000385
		public override string CursorKey
		{
			get
			{
				return "DeleteBuildingCursor";
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000218C File Offset: 0x0000038C
		public override void PostPreviewAction(IEnumerable<BlockObject> blockObjects)
		{
			this._recoverableGoodTooltip.SetRecoverableGoods(blockObjects);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219A File Offset: 0x0000039A
		public override VisualElement GetDialogBoxContent(IEnumerable<BlockObject> blockObjects)
		{
			return this._recoverableGoodElementFactory.Create(blockObjects);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A8 File Offset: 0x000003A8
		public override bool IsBlockObjectValid(BlockObject blockObject)
		{
			return !this.IsStackedDeletionBlocked(blockObject);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B4 File Offset: 0x000003B4
		public override void PreviewCallback(IEnumerable<BlockObject> blockObjects, IEnumerable<Vector3Int> terrainBlocks, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			if (!this._previewDisabled)
			{
				this.FillObjectsToDeconstruct(blockObjects);
				base.PreviewCallback(this._objectsToDeconstruct, terrainBlocks, start, end, selectionStarted, selectingArea);
				this._objectsToDeconstruct.Clear();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021E4 File Offset: 0x000003E4
		public void FillObjectsToDeconstruct(IEnumerable<BlockObject> blockObjects)
		{
			foreach (BlockObject blockObject in blockObjects)
			{
				this._objectsToDeconstruct.Add(blockObject);
				IRecoverableObjectAdder component = blockObject.GetComponent<IRecoverableObjectAdder>();
				if (component != null)
				{
					BlockObject additionalObjectToRecover = component.GetAdditionalObjectToRecover();
					if (additionalObjectToRecover)
					{
						this._objectsToDeconstruct.Add(additionalObjectToRecover);
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		public bool IsStackedDeletionBlocked(BlockObject blockObject)
		{
			blockObject.GetComponents<IBlockObjectDeletionBlocker>(this._deletionBlockers);
			bool result = this._deletionBlockers.Any((IBlockObjectDeletionBlocker blocker) => blocker.IsStackedDeletionBlocked);
			this._deletionBlockers.Clear();
			return result;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DescriptionLocKey = "DeletionTool.Description.Buildings";

		// Token: 0x04000007 RID: 7
		public static readonly string ToolTitleLocKey = "DeletionTool.Title.Buildings";

		// Token: 0x04000008 RID: 8
		public readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		public readonly RecoverableGoodElementFactory _recoverableGoodElementFactory;

		// Token: 0x0400000A RID: 10
		public readonly RecoverableGoodTooltip _recoverableGoodTooltip;

		// Token: 0x0400000B RID: 11
		public new readonly TerrainDestroyer _terrainDestroyer;

		// Token: 0x0400000C RID: 12
		public readonly HashSet<BlockObject> _objectsToDeconstruct = new HashSet<BlockObject>();

		// Token: 0x0400000D RID: 13
		public bool _previewDisabled;

		// Token: 0x0400000E RID: 14
		public readonly List<IBlockObjectDeletionBlocker> _deletionBlockers = new List<IBlockObjectDeletionBlocker>();
	}
}
