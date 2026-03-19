using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.CameraSystem;
using Timberborn.GridTraversing;
using Timberborn.InputSystem;
using Timberborn.SelectionSystem;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x02000007 RID: 7
	public class CursorCoordinatesPicker
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002223 File Offset: 0x00000423
		public CursorCoordinatesPicker(CameraService cameraService, TerrainPicker terrainPicker, InputService inputService, SelectableObjectRaycaster selectableObjectRaycaster, ITerrainService terrainService)
		{
			this._cameraService = cameraService;
			this._terrainPicker = terrainPicker;
			this._inputService = inputService;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
			this._terrainService = terrainService;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000225B File Offset: 0x0000045B
		public CursorCoordinates? Pick()
		{
			return this.PickCoordinates(false);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002264 File Offset: 0x00000464
		public CursorCoordinates? PickOnFinished()
		{
			return this.PickCoordinates(true);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002270 File Offset: 0x00000470
		public CursorCoordinates? PickCoordinates(bool ignoreUnfinished)
		{
			Ray ray = this._cameraService.ScreenPointToRayInGridSpace(this._inputService.MousePosition);
			CursorCoordinates? result;
			if (this.TryGetBlockObjectCoordinates(ray, ignoreUnfinished, out result))
			{
				return result;
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinates(ray);
			if (traversedCoordinates != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
				Vector3Int vector3Int = valueOrDefault.Coordinates + valueOrDefault.Face;
				if (this._terrainService.Contains(vector3Int))
				{
					return new CursorCoordinates?(new CursorCoordinates(valueOrDefault.Intersection, vector3Int));
				}
			}
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002308 File Offset: 0x00000508
		public bool TryGetBlockObjectCoordinates(Ray ray, bool ignoreUnfinished, out CursorCoordinates? foundCoordinates)
		{
			SelectableObject selectableObject;
			if (this._selectableObjectRaycaster.TryHitSelectableObject(out selectableObject))
			{
				BlockObject component = selectableObject.GetComponent<BlockObject>();
				if (component != null && !component.IsPreview && (!ignoreUnfinished || component.IsFinished))
				{
					CursorCoordinates? floorOrPathCoordinatesHit = CursorCoordinatesPicker.GetFloorOrPathCoordinatesHit(ray, component);
					foundCoordinates = ((floorOrPathCoordinatesHit != null) ? floorOrPathCoordinatesHit : this.GetStackableCoordinatesHit(ray, component));
					return foundCoordinates != null;
				}
			}
			foundCoordinates = null;
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002374 File Offset: 0x00000574
		public static CursorCoordinates? GetFloorOrPathCoordinatesHit(Ray ray, BlockObject blockObject)
		{
			if (blockObject.PositionedBlocks.GetAllBlocks().Any(delegate(Block block)
			{
				BlockOccupations occupation = block.Occupation;
				return occupation == BlockOccupations.Floor || occupation == BlockOccupations.Path;
			}))
			{
				Vector3? vector = GridSpaceRaycasting.HitHorizontalPlane(ray, (float)blockObject.Coordinates.z);
				if (vector != null)
				{
					return new CursorCoordinates?(new CursorCoordinates(vector.Value, CursorCoordinatesPicker.GetTile(vector.Value)));
				}
			}
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023F8 File Offset: 0x000005F8
		public CursorCoordinates? GetStackableCoordinatesHit(Ray ray, BlockObject blockObject)
		{
			this.AddStackableCoordinates(blockObject);
			if (this._stackableCoordinatesCache.Count == 0)
			{
				return null;
			}
			int num = this._stackableCoordinatesCache.Max((Vector3Int coords) => coords.z);
			Vector3? vector = GridSpaceRaycasting.HitHorizontalPlane(ray, (float)(num + 1));
			if (vector != null)
			{
				Vector3Int tile = CursorCoordinatesPicker.GetTile(vector.Value);
				if (this._stackableCoordinatesCache.Contains(tile - new Vector3Int(0, 0, 1)))
				{
					this.ClearStackableCoordinates();
					return new CursorCoordinates?(new CursorCoordinates(vector.Value, tile));
				}
			}
			this.ClearStackableCoordinates();
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024B2 File Offset: 0x000006B2
		public static Vector3Int GetTile(Vector3 intersection)
		{
			return new Vector3Int(Mathf.FloorToInt(intersection.x), Mathf.FloorToInt(intersection.y), Mathf.RoundToInt(intersection.z));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024DC File Offset: 0x000006DC
		public void AddStackableCoordinates(BlockObject blockObject)
		{
			IEnumerable<Vector3Int> collection = from block in blockObject.PositionedBlocks.GetAllBlocks()
			where block.Stackable > BlockStackable.None
			select block.Coordinates;
			this._stackableCoordinatesCache.AddRange(collection);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002549 File Offset: 0x00000749
		public void ClearStackableCoordinates()
		{
			this._stackableCoordinatesCache.Clear();
		}

		// Token: 0x04000011 RID: 17
		public readonly CameraService _cameraService;

		// Token: 0x04000012 RID: 18
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000013 RID: 19
		public readonly InputService _inputService;

		// Token: 0x04000014 RID: 20
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;

		// Token: 0x04000015 RID: 21
		public readonly ITerrainService _terrainService;

		// Token: 0x04000016 RID: 22
		public readonly List<Vector3Int> _stackableCoordinatesCache = new List<Vector3Int>();
	}
}
