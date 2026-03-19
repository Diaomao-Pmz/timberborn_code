using System;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.ConstructionSites;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Terraforming
{
	// Token: 0x02000011 RID: 17
	public class GroundRaisingService : ILoadableSingleton
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000031DB File Offset: 0x000013DB
		public GroundRaisingService(ITerrainService terrainService, IBlockService blockService, EventBus eventBus)
		{
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000031F8 File Offset: 0x000013F8
		public void Load()
		{
			this._eventBus.Register(this);
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003220 File Offset: 0x00001420
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			GroundRaiser component = entityDeletedEvent.Entity.GetComponent<GroundRaiser>();
			if (component && component.ShouldRaiseTerrain)
			{
				this._terrainService.SetTerrain(component.Coordinates, 1);
				component.GetComponent<GroundedConstructionSite>().UpdateConstructionSitesAtop();
				component.GetComponent<PhysicallySupportedConstructionSiteUpdater>().UpdateNeighbours();
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003274 File Offset: 0x00001474
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			if (!change.SetTerrain)
			{
				for (int i = change.From; i <= change.To; i++)
				{
					Vector3Int vector3Int = change.Coordinates.ToVector3Int(i);
					foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
					{
						this.ValidateConstructionSite(vector3Int + vector3Int2);
					}
				}
				this.ValidateConstructionSite(change.Coordinates.ToVector3Int(change.To + 1));
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003304 File Offset: 0x00001504
		public void ValidateConstructionSite(Vector3Int coordinates)
		{
			PhysicallySupportedConstructionSite bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<PhysicallySupportedConstructionSite>(coordinates);
			if (bottomObjectComponentAt)
			{
				bottomObjectComponentAt.Validate();
			}
		}

		// Token: 0x04000038 RID: 56
		public readonly ITerrainService _terrainService;

		// Token: 0x04000039 RID: 57
		public readonly IBlockService _blockService;

		// Token: 0x0400003A RID: 58
		public readonly EventBus _eventBus;
	}
}
