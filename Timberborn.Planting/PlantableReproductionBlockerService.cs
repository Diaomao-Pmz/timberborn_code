using System;
using Timberborn.BlockSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200000E RID: 14
	public class PlantableReproductionBlockerService : ILoadableSingleton
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000024FD File Offset: 0x000006FD
		public PlantableReproductionBlockerService(EventBus eventBus, IBlockService blockService)
		{
			this._eventBus = eventBus;
			this._blockService = blockService;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002513 File Offset: 0x00000713
		[OnEvent]
		public void OnPlantingCoordinatesSet(PlantingCoordinatesSetEvent plantingCoordinatesSetEvent)
		{
			this.BlockReproductionAt(plantingCoordinatesSetEvent.Coordinates);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002521 File Offset: 0x00000721
		[OnEvent]
		public void OnPlantingCoordinatesUnset(PlantingCoordinatesUnsetEvent plantingCoordinatesUnsetEvent)
		{
			this.UnblockReproductionAt(plantingCoordinatesUnsetEvent.Coordinates);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000252F File Offset: 0x0000072F
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002540 File Offset: 0x00000740
		public void BlockReproductionAt(Vector3Int coordinates)
		{
			PlantableReproductionBlocker bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<PlantableReproductionBlocker>(coordinates);
			if (bottomObjectComponentAt != null)
			{
				bottomObjectComponentAt.BlockReproduction();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002564 File Offset: 0x00000764
		public void UnblockReproductionAt(Vector3Int coordinates)
		{
			PlantableReproductionBlocker bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<PlantableReproductionBlocker>(coordinates);
			if (bottomObjectComponentAt != null)
			{
				bottomObjectComponentAt.UnblockReproduction();
			}
		}

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly IBlockService _blockService;
	}
}
