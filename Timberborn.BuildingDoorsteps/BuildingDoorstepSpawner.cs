using System;
using Timberborn.AssetSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BuildingDoorsteps
{
	// Token: 0x02000008 RID: 8
	public class BuildingDoorstepSpawner : ILoadableSingleton
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002111 File Offset: 0x00000311
		public BuildingDoorstepSpawner(OptimizedPrefabInstantiator optimizedPrefabInstantiator, EventBus eventBus, IAssetLoader assetLoader)
		{
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
			this._eventBus = eventBus;
			this._assetLoader = assetLoader;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212E File Offset: 0x0000032E
		public void Load()
		{
			this._doorstepPrefab = this._assetLoader.Load<GameObject>("ConstructionBases/Doorstep/Doorstep.Model");
			this._eventBus.Register(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			BuildingModel component = entityInitializedEvent.Entity.GetComponent<BuildingModel>();
			if (component)
			{
				this.SpawnDoorstep(component);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000217C File Offset: 0x0000037C
		public void SpawnDoorstep(BuildingModel buildingModel)
		{
			BlockObject component = buildingModel.GetComponent<BlockObject>();
			if (BuildingDoorstepSpawner.CanSpawnDoorstep(component))
			{
				GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(this._doorstepPrefab, buildingModel.UnfinishedModel.transform);
				Vector3 coordinates = component.Entrance.Coordinates + BuildingDoorstepSpawner.DoorstepModelOffset - new Vector3Int(0, 0, component.BaseZ);
				gameObject.transform.localPosition = CoordinateSystem.GridToWorld(coordinates);
				buildingModel.GetComponent<BlockObjectModelController>().UpdateAll();
				EntityMaterials component2 = buildingModel.GetComponent<EntityMaterials>();
				if (component2 != null)
				{
					component2.AddMaterials(gameObject);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002214 File Offset: 0x00000414
		public static bool CanSpawnDoorstep(BlockObject blockObject)
		{
			return blockObject.HasEntrance && blockObject.Entrance.Coordinates.z - blockObject.BaseZ == 0 && !blockObject.HasComponent<DoorstepSpawnDisablerSpec>();
		}

		// Token: 0x04000008 RID: 8
		public static readonly Vector3 DoorstepModelOffset = Vector3.up;

		// Token: 0x04000009 RID: 9
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public readonly IAssetLoader _assetLoader;

		// Token: 0x0400000C RID: 12
		public GameObject _doorstepPrefab;
	}
}
