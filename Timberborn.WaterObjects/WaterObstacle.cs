using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000016 RID: 22
	public class WaterObstacle : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002EAD File Offset: 0x000010AD
		public WaterObstacle(IWaterService waterService)
		{
			this._waterService = waterService;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002EBC File Offset: 0x000010BC
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._waterObstacleSpec = base.GetComponent<WaterObstacleSpec>();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002ED8 File Offset: 0x000010D8
		public void AddToWaterService(float height)
		{
			if (!this._wasAdded && this._blockObject.AddedToService)
			{
				this._height = height;
				int z = this._blockObject.CoordinatesAtBaseZ.z;
				foreach (Vector2Int tile in this._waterObstacleSpec.Coordinates)
				{
					Vector2Int vector2Int = this._blockObject.TransformTile(tile);
					int num = 0;
					while ((float)num < height)
					{
						Vector3Int coordinates;
						coordinates..ctor(vector2Int.x, vector2Int.y, z + num);
						float num2 = this._height % 1f;
						if ((float)(num + 1) > height && num2 > 0f)
						{
							this._waterService.UpdateInflowLimiter(coordinates, num2);
						}
						else
						{
							this._waterService.AddFullObstacle(coordinates);
						}
						num++;
					}
				}
				this._wasAdded = true;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002FC4 File Offset: 0x000011C4
		public void RemoveFromWaterService()
		{
			if (this._wasAdded)
			{
				int z = this._blockObject.CoordinatesAtBaseZ.z;
				foreach (Vector2Int tile in this._waterObstacleSpec.Coordinates)
				{
					Vector2Int vector2Int = this._blockObject.TransformTile(tile);
					int num = Mathf.CeilToInt(this._height - 1f);
					for (int i = num; i >= 0; i--)
					{
						Vector3Int coordinates;
						coordinates..ctor(vector2Int.x, vector2Int.y, z + i);
						if (i == num && this._height % 1f > 0f)
						{
							this._waterService.RemoveInflowLimiter(coordinates);
						}
						else
						{
							this._waterService.RemoveFullObstacle(coordinates);
						}
					}
				}
				this._wasAdded = false;
			}
		}

		// Token: 0x04000021 RID: 33
		public readonly IWaterService _waterService;

		// Token: 0x04000022 RID: 34
		public BlockObject _blockObject;

		// Token: 0x04000023 RID: 35
		public WaterObstacleSpec _waterObstacleSpec;

		// Token: 0x04000024 RID: 36
		public bool _wasAdded;

		// Token: 0x04000025 RID: 37
		public float _height;
	}
}
