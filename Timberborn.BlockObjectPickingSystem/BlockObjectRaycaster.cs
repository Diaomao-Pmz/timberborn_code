using System;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000013 RID: 19
	public class BlockObjectRaycaster
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002FCD File Offset: 0x000011CD
		public BlockObjectRaycaster(SelectableObjectRaycaster selectableObjectRaycaster)
		{
			this._selectableObjectRaycaster = selectableObjectRaycaster;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FDC File Offset: 0x000011DC
		public bool TryHitBlockObject<T>(Ray ray, out BlockObjectHit blockObjectHit)
		{
			Ray worldSpaceRay = CoordinateSystem.GridToWorld(ray);
			SelectableObject selectableObject;
			RaycastHit hitInWorldSpace;
			if (this._selectableObjectRaycaster.TryHitSelectableObjectIncludeTerrainStump(worldSpaceRay, out selectableObject, out hitInWorldSpace))
			{
				if (selectableObject.GetComponent<T>() != null)
				{
					BlockObject component = selectableObject.GetComponent<BlockObject>();
					if (component != null)
					{
						Block hitBlock = BlockObjectRaycaster.ClosestBlock(component, hitInWorldSpace);
						Vector3Int hitProjectedOnGround;
						hitProjectedOnGround..ctor(hitBlock.Coordinates.x, hitBlock.Coordinates.y, Mathf.Min(component.CoordinatesAtBaseZ.z, hitBlock.Coordinates.z));
						blockObjectHit = new BlockObjectHit(component, hitBlock, hitProjectedOnGround);
						return true;
					}
				}
				GameObject gameObject = hitInWorldSpace.collider.gameObject;
				int layer = gameObject.layer;
				gameObject.layer = Layers.IgnoreRaycastMask;
				bool result = this.TryHitBlockObject<T>(ray, out blockObjectHit);
				gameObject.layer = layer;
				return result;
			}
			blockObjectHit = default(BlockObjectHit);
			return false;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030C4 File Offset: 0x000012C4
		public static Block ClosestBlock(BlockObject blockObject, RaycastHit hitInWorldSpace)
		{
			Vector3 point = hitInWorldSpace.point;
			Vector3 normal = hitInWorldSpace.normal;
			if (normal.x >= 0f && normal.y >= 0f && normal.z >= 0f)
			{
				if (BlockObjectRaycaster.IsEdgeHit(point.x))
				{
					point.x -= BlockObjectRaycaster.ColliderInwardOffset;
				}
				else if (BlockObjectRaycaster.IsEdgeHit(point.y))
				{
					point.y -= BlockObjectRaycaster.ColliderInwardOffset;
				}
				else if (BlockObjectRaycaster.IsEdgeHit(point.z))
				{
					point.z -= BlockObjectRaycaster.ColliderInwardOffset;
				}
			}
			Vector3 hitCoordinates = CoordinateSystem.WorldToGrid(point);
			return blockObject.PositionedBlocks.GetAllBlocks().Aggregate((Block minItem, Block nextItem) => BlockObjectRaycaster.CloserCoordinates(hitCoordinates, minItem, nextItem));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003191 File Offset: 0x00001391
		public static bool IsEdgeHit(float coordinate)
		{
			return coordinate % 1f == 0f;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031A4 File Offset: 0x000013A4
		public static Block CloserCoordinates(Vector3 reference, Block block1, Block block2)
		{
			Vector3Int coordinates = block1.Coordinates;
			Vector3Int coordinates2 = block2.Coordinates;
			if (BlockObjectRaycaster.SquaredDistance(reference, coordinates) >= BlockObjectRaycaster.SquaredDistance(reference, coordinates2))
			{
				return block2;
			}
			return block1;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000031D4 File Offset: 0x000013D4
		public static float SquaredDistance(Vector3 reference, Vector3Int block)
		{
			Vector3 vector;
			vector..ctor((float)block.x + 0.5f, (float)block.y + 0.5f, (float)block.z + 0.5f);
			return (reference - vector).sqrMagnitude;
		}

		// Token: 0x0400003B RID: 59
		public static readonly float ColliderInwardOffset = 0.001f;

		// Token: 0x0400003C RID: 60
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;
	}
}
