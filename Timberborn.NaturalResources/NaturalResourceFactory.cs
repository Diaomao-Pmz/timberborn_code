using System;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Demolishing;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.NaturalResources
{
	// Token: 0x0200000A RID: 10
	public class NaturalResourceFactory
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002292 File Offset: 0x00000492
		public NaturalResourceFactory(TemplateNameMapper templateNameMapper, SpawnValidationService spawnValidationService, BlockObjectFactory blockObjectFactory, EventBus eventBus)
		{
			this._templateNameMapper = templateNameMapper;
			this._spawnValidationService = spawnValidationService;
			this._blockObjectFactory = blockObjectFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022B7 File Offset: 0x000004B7
		public NaturalResource SpawnIgnoringConstraintsAndRandomizePosition(string resourceId, Vector3Int coordinates)
		{
			return this.SpawnIgnoringConstraints(resourceId, coordinates, true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C2 File Offset: 0x000004C2
		public NaturalResource SpawnIgnoringConstraints(string resourceId, Vector3Int coordinates)
		{
			return this.SpawnIgnoringConstraints(resourceId, coordinates, false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022D0 File Offset: 0x000004D0
		public void SpawnNew(string resourceId, Vector3Int coordinates, bool spawnMarkedForDemolish)
		{
			BlockObjectSpec spec = this._templateNameMapper.GetTemplate(resourceId).GetSpec<BlockObjectSpec>();
			if (this._spawnValidationService.CanSpawn(coordinates, spec, resourceId))
			{
				this.Create(coordinates, spec, true, spawnMarkedForDemolish);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230C File Offset: 0x0000050C
		public void PlantNew(string resourceId, Vector3Int coordinates)
		{
			BlockObjectSpec spec = this._templateNameMapper.GetTemplate(resourceId).GetSpec<BlockObjectSpec>();
			this.Create(coordinates, spec, false, false);
			this._eventBus.Post(new NaturalResourcePlantedEvent(spec));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002348 File Offset: 0x00000548
		public NaturalResource SpawnIgnoringConstraints(string resourceId, Vector3Int coordinates, bool randomPosition)
		{
			BlockObjectSpec spec = this._templateNameMapper.GetTemplate(resourceId).GetSpec<BlockObjectSpec>();
			if (!this._spawnValidationService.CanSpawnIgnoringConstraints(coordinates, spec))
			{
				return null;
			}
			return this.Create(coordinates, spec, randomPosition, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002384 File Offset: 0x00000584
		public NaturalResource Create(Vector3Int coordinates, BlockObjectSpec template, bool randomPosition, bool spawnMarkedForDemolish = false)
		{
			BlockObject blockObject = this._blockObjectFactory.CreateFinished(template, new Placement(coordinates));
			if (randomPosition)
			{
				blockObject.GetComponent<CoordinatesOffsetter>().SetRandomOffset();
			}
			if (spawnMarkedForDemolish)
			{
				blockObject.GetComponent<Demolishable>().Mark();
			}
			return blockObject.GetComponent<NaturalResource>();
		}

		// Token: 0x0400000E RID: 14
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x0400000F RID: 15
		public readonly SpawnValidationService _spawnValidationService;

		// Token: 0x04000010 RID: 16
		public readonly BlockObjectFactory _blockObjectFactory;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;
	}
}
