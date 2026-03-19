using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000010 RID: 16
	public class HorizontalWaterObstacle : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000028DA File Offset: 0x00000ADA
		public HorizontalWaterObstacle(IWaterService waterService)
		{
			this._waterService = waterService;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000028F4 File Offset: 0x00000AF4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002904 File Offset: 0x00000B04
		public void AddToWaterService(IEnumerable<Vector3Int> obstacles)
		{
			if (this._addedObstacles.Count == 0 && this._blockObject.AddedToService)
			{
				this._addedObstacles.AddRange(obstacles);
				foreach (Vector3Int coordinates in this._addedObstacles)
				{
					Vector3Int vector3Int = this._blockObject.TransformCoordinates(coordinates);
					Vector3Int coordinatesToAdd;
					coordinatesToAdd..ctor(vector3Int.x, vector3Int.y, this._blockObject.Coordinates.z + coordinates.z);
					this._waterService.AddHorizontalObstacle(coordinatesToAdd);
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000029C8 File Offset: 0x00000BC8
		public void RemoveFromWaterService()
		{
			if (this._addedObstacles.Count > 0)
			{
				foreach (Vector3Int coordinates in this._addedObstacles)
				{
					Vector3Int vector3Int = this._blockObject.TransformCoordinates(coordinates);
					Vector3Int coordinatesToRemove;
					coordinatesToRemove..ctor(vector3Int.x, vector3Int.y, this._blockObject.Coordinates.z + coordinates.z);
					this._waterService.RemoveHorizontalObstacle(coordinatesToRemove);
				}
				this._addedObstacles.Clear();
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly IWaterService _waterService;

		// Token: 0x04000016 RID: 22
		public BlockObject _blockObject;

		// Token: 0x04000017 RID: 23
		public readonly List<Vector3Int> _addedObstacles = new List<Vector3Int>();
	}
}
