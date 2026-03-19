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
using Timberborn.SingletonSystem;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainSystemRendering;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000007 RID: 7
	public abstract class BlockObjectDeletionTool<T> : ITool, IToolDescriptor, ILoadableSingleton, IInputProcessor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BlockObjectDeletionTool(InputService inputService, AreaBlockObjectAndTerrainPicker areaBlockObjectAndTerrainPicker, EntityService entityService, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, CursorService cursorService, BlockObjectModelBlockadeIgnorer blockObjectModelBlockadeIgnorer, ISpecService specService, ILevelVisibilityService levelVisibilityService, DialogBoxShower dialogBoxShower, TerrainDestroyer terrainDestroyer, TerrainHighlightingService terrainHighlightingService, IUndoRegistry undoRegistry)
		{
			this._inputService = inputService;
			this._areaBlockObjectAndTerrainPicker = areaBlockObjectAndTerrainPicker;
			this._entityService = entityService;
			this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
			this._cursorService = cursorService;
			this._blockObjectModelBlockadeIgnorer = blockObjectModelBlockadeIgnorer;
			this._specService = specService;
			this._levelVisibilityService = levelVisibilityService;
			this._dialogBoxShower = dialogBoxShower;
			this._terrainDestroyer = terrainDestroyer;
			this._terrainHighlightingService = terrainHighlightingService;
			this._undoRegistry = undoRegistry;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
		public void Load()
		{
			this._blockObjectDeletionToolSpec = this._specService.GetSingleSpec<BlockObjectDeletionToolSpec>();
			this._blockObjectSelectionDrawer = this._blockObjectSelectionDrawerFactory.Create(this._blockObjectDeletionToolSpec.DeletedObjectHighlightColor, this._blockObjectDeletionToolSpec.DeletedAreaTileColor, this._blockObjectDeletionToolSpec.DeletedAreaSideColor);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D8 File Offset: 0x000003D8
		public bool ProcessInput()
		{
			if (!this._paused)
			{
				this._skipConfirmation = (this._inputService.IsKeyHeld(BlockObjectDeletionTool<T>.SkipDeleteConfirmationKey) || this._undoRegistry.UndoAllowed);
				return this._areaBlockObjectAndTerrainPicker.PickBlockObjectsAndTerrain<T>(new AreaBlockObjectAndTerrainPicker.Callback(this.PreviewCallback), new AreaBlockObjectAndTerrainPicker.Callback(this.ActionCallback), new Action(this.ShowNoneCallback), new Func<BlockObject, bool>(this.IsBlockObjectValid));
			}
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002252 File Offset: 0x00000452
		public virtual void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(this.CursorKey);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002271 File Offset: 0x00000471
		public virtual void Exit()
		{
			this._cursorService.ResetCursor();
			this._areaBlockObjectAndTerrainPicker.Reset();
			this._blockObjectSelectionDrawer.StopDrawing();
			this._inputService.RemoveInputProcessor(this);
			this._terrainHighlightingService.ClearHighlight();
		}

		// Token: 0x0600000C RID: 12
		public abstract ToolDescription DescribeTool();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13
		public abstract string ToolPromptLocKey { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14
		public abstract string CursorKey { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x000022AB File Offset: 0x000004AB
		public virtual VisualElement GetDialogBoxContent(IEnumerable<BlockObject> blockObjects)
		{
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022AE File Offset: 0x000004AE
		public virtual void PostPreviewAction(IEnumerable<BlockObject> blockObjects)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022B0 File Offset: 0x000004B0
		public virtual void PreviewCallback(IEnumerable<BlockObject> blockObjects, IEnumerable<Vector3Int> terrainCoords, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			this._temporaryBlockObjects.AddRange(blockObjects);
			this._blockObjectSelectionDrawer.Draw(this._temporaryBlockObjects, start, end, selectingArea);
			this._terrainHighlightingService.UpdateHighlight(terrainCoords);
			this.PostPreviewAction(this._temporaryBlockObjects);
			this._temporaryBlockObjects.Clear();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002302 File Offset: 0x00000502
		public virtual bool IsBlockObjectValid(BlockObject blockObject)
		{
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002305 File Offset: 0x00000505
		public void ShowNoneCallback()
		{
			this._blockObjectSelectionDrawer.StopDrawing();
			this._terrainHighlightingService.ClearHighlight();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002320 File Offset: 0x00000520
		public void ActionCallback(IEnumerable<BlockObject> blockObjects, IEnumerable<Vector3Int> terrainCoords, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			this._temporaryBlockObjects.AddRange(from blockObject in blockObjects
			orderby blockObject.Transform.position.y descending
			select blockObject);
			this._temporaryTerrainCoords.AddRange(terrainCoords);
			if (this._temporaryBlockObjects.Count > 0)
			{
				if (this._skipConfirmation)
				{
					this.DeleteBlockObjects();
					return;
				}
				this._blockObjectSelectionDrawer.Draw(this._temporaryBlockObjects, start, end, selectingArea);
				this._blockObjectModelBlockadeIgnorer.IgnoreModelBlockades(this._temporaryBlockObjects);
				this._maxVisibleLevelToReset = this._levelVisibilityService.MaxVisibleLevel;
				this.SetVisibleLayerToShowAllObjects();
				this.Pause();
				this._dialogBoxShower.Create().SetLocalizedMessage(this.ToolPromptLocKey).SetConfirmButton(new Action(this.OnDeleteConfirmed)).SetCancelButton(new Action(this.OnDeleteCanceled)).SetOffset(new Vector2Int(0, -200)).AddContent(this.GetDialogBoxContent(this._temporaryBlockObjects)).Show();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000242C File Offset: 0x0000062C
		public void SetVisibleLayerToShowAllObjects()
		{
			int num = 0;
			foreach (Vector3Int vector3Int in this._temporaryTerrainCoords)
			{
				num = Math.Max(num, vector3Int.z);
			}
			foreach (BlockObject blockObject in this._temporaryBlockObjects)
			{
				foreach (Vector3Int vector3Int2 in blockObject.PositionedBlocks.GetAllCoordinates())
				{
					num = Math.Max(num, vector3Int2.z);
				}
			}
			if (num > this._levelVisibilityService.MaxVisibleLevel)
			{
				this._levelVisibilityService.SetMaxVisibleLevel(num);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000252C File Offset: 0x0000072C
		public void OnDeleteConfirmed()
		{
			this.DeleteBlockObjects();
			this.Unpause();
			this._levelVisibilityService.SetMaxVisibleLevel(this._maxVisibleLevelToReset);
			this._blockObjectModelBlockadeIgnorer.Clear();
			this._terrainHighlightingService.ClearHighlight();
			this._areaBlockObjectAndTerrainPicker.Reset();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000256C File Offset: 0x0000076C
		public void OnDeleteCanceled()
		{
			this.Unpause();
			this._levelVisibilityService.SetMaxVisibleLevel(this._maxVisibleLevelToReset);
			this._blockObjectModelBlockadeIgnorer.UnignoreModelBlockades();
			this._temporaryBlockObjects.Clear();
			this._temporaryTerrainCoords.Clear();
			this._terrainHighlightingService.ClearHighlight();
			this._areaBlockObjectAndTerrainPicker.Reset();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025C8 File Offset: 0x000007C8
		public void DeleteBlockObjects()
		{
			foreach (BlockObject blockObject in this._temporaryBlockObjects)
			{
				if (blockObject)
				{
					this._entityService.Delete(blockObject);
				}
			}
			foreach (Vector3Int coordinates in this._temporaryTerrainCoords)
			{
				this._terrainDestroyer.DestroyTerrain(coordinates);
			}
			this._temporaryTerrainCoords.Clear();
			this._temporaryBlockObjects.Clear();
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002690 File Offset: 0x00000890
		public void Pause()
		{
			this._paused = true;
			this._cursorService.ResetCursor();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026A4 File Offset: 0x000008A4
		public void Unpause()
		{
			this._paused = false;
			this._cursorService.SetCursor(this.CursorKey);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string SkipDeleteConfirmationKey = "SkipDeleteConfirmation";

		// Token: 0x04000009 RID: 9
		public readonly AreaBlockObjectAndTerrainPicker _areaBlockObjectAndTerrainPicker;

		// Token: 0x0400000A RID: 10
		public readonly InputService _inputService;

		// Token: 0x0400000B RID: 11
		public readonly EntityService _entityService;

		// Token: 0x0400000C RID: 12
		public readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

		// Token: 0x0400000D RID: 13
		public readonly CursorService _cursorService;

		// Token: 0x0400000E RID: 14
		public readonly BlockObjectModelBlockadeIgnorer _blockObjectModelBlockadeIgnorer;

		// Token: 0x0400000F RID: 15
		public readonly ISpecService _specService;

		// Token: 0x04000010 RID: 16
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000011 RID: 17
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000012 RID: 18
		public readonly TerrainDestroyer _terrainDestroyer;

		// Token: 0x04000013 RID: 19
		public readonly TerrainHighlightingService _terrainHighlightingService;

		// Token: 0x04000014 RID: 20
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000015 RID: 21
		public readonly List<BlockObject> _temporaryBlockObjects = new List<BlockObject>();

		// Token: 0x04000016 RID: 22
		public readonly List<Vector3Int> _temporaryTerrainCoords = new List<Vector3Int>();

		// Token: 0x04000017 RID: 23
		public BlockObjectDeletionToolSpec _blockObjectDeletionToolSpec;

		// Token: 0x04000018 RID: 24
		public BlockObjectSelectionDrawer _blockObjectSelectionDrawer;

		// Token: 0x04000019 RID: 25
		public bool _skipConfirmation;

		// Token: 0x0400001A RID: 26
		public bool _paused;

		// Token: 0x0400001B RID: 27
		public int _maxVisibleLevelToReset;
	}
}
