using System;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200001E RID: 30
	public class PlantingCoordinatesUnsetter : ILoadableSingleton
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003566 File Offset: 0x00001766
		public PlantingCoordinatesUnsetter(EventBus eventBus, TemplateNameRetriever templateNameRetriever, TemplateNameMapper templateNameMapper, PlantingService plantingService, ITerrainService terrainService)
		{
			this._eventBus = eventBus;
			this._templateNameRetriever = templateNameRetriever;
			this._templateNameMapper = templateNameMapper;
			this._plantingService = plantingService;
			this._terrainService = terrainService;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003594 File Offset: 0x00001794
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			BlockObject blockObject = blockObjectSetEvent.BlockObject;
			if (!blockObject.Overridable && blockObject.HasComponent<TemplateSpec>())
			{
				this.UnsetCoordinatesIntersectingBlockObject(blockObject);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035BF File Offset: 0x000017BF
		public void Load()
		{
			this._eventBus.Register(this);
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035E4 File Offset: 0x000017E4
		public void UnsetCoordinatesIntersectingBlockObject(BlockObject blockObject)
		{
			string templateName = this._templateNameRetriever.GetTemplateName(blockObject);
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedBlocks())
			{
				this.UnsetCoordinatesIfBlockIsIntersectingPlantable(block, templateName);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003644 File Offset: 0x00001844
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			if (!change.SetTerrain)
			{
				Vector3Int coordinates = change.Coordinates.ToVector3Int(change.To + 1);
				this._plantingService.UnsetPlantingCoordinates(coordinates);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003684 File Offset: 0x00001884
		public void UnsetCoordinatesIfBlockIsIntersectingPlantable(Block block, string templateName)
		{
			Vector3Int coordinates = block.Coordinates;
			string resourceAt = this._plantingService.GetResourceAt(coordinates);
			if (resourceAt != null && resourceAt != templateName && this.IsBlockIntersectingPlantable(block, coordinates, resourceAt))
			{
				this._plantingService.UnsetPlantingCoordinates(block.Coordinates);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036CF File Offset: 0x000018CF
		public bool IsBlockIntersectingPlantable(Block block, Vector3Int plantableCoordinates, string plantableName)
		{
			return this._templateNameMapper.GetTemplate(plantableName).GetSpec<BlockObjectSpec>().GetBlocks(new Placement(plantableCoordinates)).Any(new Func<Block, bool>(block.IsIntersecting));
		}

		// Token: 0x0400004E RID: 78
		public readonly EventBus _eventBus;

		// Token: 0x0400004F RID: 79
		public readonly TemplateNameRetriever _templateNameRetriever;

		// Token: 0x04000050 RID: 80
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000051 RID: 81
		public readonly PlantingService _plantingService;

		// Token: 0x04000052 RID: 82
		public readonly ITerrainService _terrainService;
	}
}
