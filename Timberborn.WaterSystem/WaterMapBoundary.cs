using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200002E RID: 46
	public class WaterMapBoundary : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000049F9 File Offset: 0x00002BF9
		public WaterMapBoundary(WaterMapBoundaryService waterMapBoundaryService)
		{
			this._waterMapBoundaryService = waterMapBoundaryService;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004A08 File Offset: 0x00002C08
		public void Awake()
		{
			this._waterSource = base.GetComponent<IWaterSource>();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004A16 File Offset: 0x00002C16
		public void InitializeEntity()
		{
			this.BlockNearbyBoundaries();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004A1E File Offset: 0x00002C1E
		public void DeleteEntity()
		{
			this.UnblockNearbyBoundaries();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004A26 File Offset: 0x00002C26
		public void BlockNearbyBoundaries()
		{
			this.SetCellBlockAtNearbyBoundaries(true);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004A2F File Offset: 0x00002C2F
		public void UnblockNearbyBoundaries()
		{
			this.SetCellBlockAtNearbyBoundaries(false);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004A38 File Offset: 0x00002C38
		public void SetCellBlockAtNearbyBoundaries(bool block)
		{
			foreach (Vector2Int vector2Int in Deltas.Neighbors4Vector2Int)
			{
				for (int j = 0; j < this._waterSource.Coordinates.Length; j++)
				{
					Vector3Int value = this._waterSource.Coordinates[j];
					if (block)
					{
						this._waterMapBoundaryService.FullyBlockCell(value.XY() + vector2Int);
					}
					else
					{
						this._waterMapBoundaryService.FullyUnblockCell(value.XY() + vector2Int);
					}
				}
			}
		}

		// Token: 0x040000AC RID: 172
		public readonly WaterMapBoundaryService _waterMapBoundaryService;

		// Token: 0x040000AD RID: 173
		public IWaterSource _waterSource;
	}
}
