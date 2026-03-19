using System;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.ConstructionSites;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.GameFactionSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000012 RID: 18
	public class StartingBuildingSpawner : ILoadableSingleton
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002713 File Offset: 0x00000913
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000271B File Offset: 0x0000091B
		public Building StartingBuilding { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002724 File Offset: 0x00000924
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000272C File Offset: 0x0000092C
		public TemplateSpec StartingBuildingTemplateSpec { get; private set; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002735 File Offset: 0x00000935
		public StartingBuildingSpawner(FactionService factionService, TemplateNameMapper templateNameMapper, BlockObjectFactory blockObjectFactory, CameraTargeter cameraTargeter, EntityService entityService, StartingGoodsProvider startingGoodsProvider)
		{
			this._factionService = factionService;
			this._templateNameMapper = templateNameMapper;
			this._blockObjectFactory = blockObjectFactory;
			this._cameraTargeter = cameraTargeter;
			this._entityService = entityService;
			this._startingGoodsProvider = startingGoodsProvider;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000276C File Offset: 0x0000096C
		public void Load()
		{
			string startingBuildingId = this._factionService.Current.StartingBuildingId;
			this.StartingBuildingTemplateSpec = this._templateNameMapper.GetTemplate(startingBuildingId);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000279C File Offset: 0x0000099C
		public void Place(Placement? placement)
		{
			if (placement != null)
			{
				this.PlaceStartingBuilding(placement.Value);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027B4 File Offset: 0x000009B4
		public void DeleteStartingBuilding()
		{
			if (this.StartingBuilding)
			{
				this._startingGoodsProvider.RemoveStartingInventory(this.StartingBuilding);
				this._entityService.Delete(this.StartingBuilding);
			}
			this.StartingBuilding = null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027EC File Offset: 0x000009EC
		public void PlaceStartingBuilding(Placement placement)
		{
			Building startingBuilding = this.CreateStartingBuilding(placement);
			this._startingGoodsProvider.AddStartingInventory(startingBuilding);
			this.StartingBuilding = startingBuilding;
			this._cameraTargeter.CenterCameraOn(this.StartingBuilding.GetComponent<SelectableObject>());
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000282C File Offset: 0x00000A2C
		public Building CreateStartingBuilding(Placement placement)
		{
			BlockObjectSpec spec = this.StartingBuildingTemplateSpec.GetSpec<BlockObjectSpec>();
			BlockObject blockObject = this._blockObjectFactory.CreateUnfinished(spec, placement);
			blockObject.GetComponent<ConstructionSite>().FinishNow();
			return blockObject.GetComponent<Building>();
		}

		// Token: 0x04000030 RID: 48
		public readonly FactionService _factionService;

		// Token: 0x04000031 RID: 49
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000032 RID: 50
		public readonly BlockObjectFactory _blockObjectFactory;

		// Token: 0x04000033 RID: 51
		public readonly CameraTargeter _cameraTargeter;

		// Token: 0x04000034 RID: 52
		public readonly EntityService _entityService;

		// Token: 0x04000035 RID: 53
		public readonly StartingGoodsProvider _startingGoodsProvider;
	}
}
