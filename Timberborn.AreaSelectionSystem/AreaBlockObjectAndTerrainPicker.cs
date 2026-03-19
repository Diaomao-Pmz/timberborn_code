using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TerrainPhysics;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000007 RID: 7
	public class AreaBlockObjectAndTerrainPicker
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AreaBlockObjectAndTerrainPicker(AreaSelectionController areaSelectionController, ITerrainPhysicsService physicsService, AreaSelector areaSelector, BlockObjectPicker blockObjectPicker)
		{
			this._areaSelectionController = areaSelectionController;
			this._terrainPhysicsService = physicsService;
			this._areaSelector = areaSelector;
			this._blockObjectPicker = blockObjectPicker;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213C File Offset: 0x0000033C
		public bool PickBlockObjectsAndTerrain<T>(AreaBlockObjectAndTerrainPicker.Callback previewCallback, AreaBlockObjectAndTerrainPicker.Callback actionCallback, Action showNoneCallback, Func<BlockObject, bool> blockObjectFilter = null)
		{
			return this._areaSelectionController.ProcessInput(delegate(Ray startRay, Ray endRay, bool selectionStarted)
			{
				AreaBlockObjectAndTerrainPicker.PickingResult pickingResult = this.PickBlockObjectsAndTerrain<T>(startRay, endRay, blockObjectFilter);
				previewCallback(pickingResult.BlockObjects, pickingResult.TerrainBlocks, pickingResult.Start, pickingResult.End, selectionStarted, pickingResult.SelectingArea);
				this._terrainBlocks.Clear();
				this._blockObjects.Clear();
			}, delegate(Ray startRay, Ray endRay, bool selectionStarted)
			{
				AreaBlockObjectAndTerrainPicker.PickingResult pickingResult = this.PickBlockObjectsAndTerrain<T>(startRay, endRay, blockObjectFilter);
				actionCallback(pickingResult.BlockObjects, pickingResult.TerrainBlocks, pickingResult.Start, pickingResult.End, selectionStarted, pickingResult.SelectingArea);
				this._terrainBlocks.Clear();
				this._blockObjects.Clear();
			}, showNoneCallback);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		public void Reset()
		{
			this._areaSelectionController.Reset();
			this._selectionStart = null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AC File Offset: 0x000003AC
		public AreaBlockObjectAndTerrainPicker.PickingResult PickBlockObjectsAndTerrain<T>(Ray startRay, Ray endRay, Func<BlockObject, bool> blockObjectFilter)
		{
			AreaBlockObjectAndTerrainPicker.PickingResult blockObjects = this.GetBlockObjects<T>(startRay, endRay, blockObjectFilter);
			this._blockObjects.AddRange(blockObjects.BlockObjects);
			if (!this._blockObjects.IsEmpty<BlockObject>())
			{
				this._terrainPhysicsService.GetTerrainAndBlockObjectStack(this._blockObjects, this._terrainBlocks, this._blockObjects);
				return new AreaBlockObjectAndTerrainPicker.PickingResult(this._blockObjects.AsReadOnlyEnumerable<BlockObject>(), this._terrainBlocks.AsReadOnlyEnumerable<Vector3Int>(), blockObjects.Start, blockObjects.End, blockObjects.SelectingArea);
			}
			return blockObjects;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002234 File Offset: 0x00000434
		public AreaBlockObjectAndTerrainPicker.PickingResult GetBlockObjects<T>(Ray startRay, Ray endRay, Func<BlockObject, bool> blockObjectFilter)
		{
			bool flag = !startRay.Equals(endRay);
			SelectionStart? selectionStart = this.GetSelectionStart<BlockObject>(startRay, flag);
			if (selectionStart != null)
			{
				SelectionStart valueOrDefault = selectionStart.GetValueOrDefault();
				Vector3Int vector3Int = flag ? this._areaSelector.GetSelectionEnd(valueOrDefault, endRay) : valueOrDefault.Coordinates;
				return new AreaBlockObjectAndTerrainPicker.PickingResult(from blockObject in this._blockObjectPicker.PickBlockObjects(valueOrDefault, vector3Int, BlockObjectPickingMode.InsideArea, blockObjectFilter, flag)
				where blockObject.GetComponent<T>() != null
				select blockObject, Enumerable.Empty<Vector3Int>(), valueOrDefault.Coordinates, vector3Int, flag);
			}
			return new AreaBlockObjectAndTerrainPicker.PickingResult(Enumerable.Empty<BlockObject>(), Enumerable.Empty<Vector3Int>(), default(Vector3Int), default(Vector3Int), flag);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F8 File Offset: 0x000004F8
		public SelectionStart? GetSelectionStart<T>(Ray startRay, bool selectingArea)
		{
			if (!selectingArea)
			{
				this._selectionStart = this._areaSelector.GetSelectionStart<T>(startRay);
			}
			return this._selectionStart;
		}

		// Token: 0x04000008 RID: 8
		public readonly AreaSelectionController _areaSelectionController;

		// Token: 0x04000009 RID: 9
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x0400000A RID: 10
		public readonly AreaSelector _areaSelector;

		// Token: 0x0400000B RID: 11
		public readonly BlockObjectPicker _blockObjectPicker;

		// Token: 0x0400000C RID: 12
		public readonly HashSet<BlockObject> _blockObjects = new HashSet<BlockObject>();

		// Token: 0x0400000D RID: 13
		public readonly HashSet<Vector3Int> _terrainBlocks = new HashSet<Vector3Int>();

		// Token: 0x0400000E RID: 14
		public SelectionStart? _selectionStart;

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x0600000E RID: 14
		public delegate void Callback(IEnumerable<BlockObject> blockObjects, IEnumerable<Vector3Int> terrainBlocks, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea);

		// Token: 0x02000009 RID: 9
		public readonly struct PickingResult
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000011 RID: 17 RVA: 0x00002315 File Offset: 0x00000515
			public IEnumerable<BlockObject> BlockObjects { get; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000012 RID: 18 RVA: 0x0000231D File Offset: 0x0000051D
			public IEnumerable<Vector3Int> TerrainBlocks { get; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000013 RID: 19 RVA: 0x00002325 File Offset: 0x00000525
			public Vector3Int Start { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000014 RID: 20 RVA: 0x0000232D File Offset: 0x0000052D
			public Vector3Int End { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000015 RID: 21 RVA: 0x00002335 File Offset: 0x00000535
			public bool SelectingArea { get; }

			// Token: 0x06000016 RID: 22 RVA: 0x0000233D File Offset: 0x0000053D
			public PickingResult(IEnumerable<BlockObject> blockObjects, IEnumerable<Vector3Int> terrainBlocks, Vector3Int start, Vector3Int end, bool selectingArea)
			{
				this.BlockObjects = blockObjects;
				this.TerrainBlocks = terrainBlocks;
				this.Start = start;
				this.End = end;
				this.SelectingArea = selectingArea;
			}
		}
	}
}
