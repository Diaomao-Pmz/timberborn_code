using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.Buildings;
using Timberborn.CharactersGame;
using Timberborn.DwellingSystem;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Reproduction
{
	// Token: 0x02000010 RID: 16
	public class NewbornSpawner
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002D65 File Offset: 0x00000F65
		public NewbornSpawner(BeaverFactory beaverFactory, EventBus eventBus)
		{
			this._beaverFactory = beaverFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D7B File Offset: 0x00000F7B
		public void SpawnAdult(BaseComponent spawner)
		{
			this.SpawnBeaver(spawner, new Func<Vector3, Beaver>(this._beaverFactory.CreateNewbornAdult));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D95 File Offset: 0x00000F95
		public void SpawnChild(BaseComponent spawner)
		{
			this.SpawnBeaver(spawner, new Func<Vector3, Beaver>(this._beaverFactory.CreateNewbornChild));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public void SpawnBeaver(BaseComponent spawner, Func<Vector3, Beaver> beaverFactory)
		{
			Vector3? unblockedSingleAccess = spawner.GetComponent<BuildingAccessible>().Accessible.UnblockedSingleAccess;
			if (unblockedSingleAccess != null)
			{
				Vector3 valueOrDefault = unblockedSingleAccess.GetValueOrDefault();
				Beaver beaver = beaverFactory(valueOrDefault);
				NewbornSpawner.AssignToDwelling(spawner, beaver);
				DistrictCenter district = spawner.GetComponent<DistrictBuilding>().District;
				if (district)
				{
					beaver.GetComponent<Citizen>().AssignInitialDistrict(district);
				}
				beaver.GetComponent<CharacterBirthNotifier>().EnableNotification();
				this._eventBus.Post(new BeaverBornEvent());
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E2C File Offset: 0x0000102C
		public static void AssignToDwelling(BaseComponent baseComponent, Beaver newBeaver)
		{
			Dwelling component = baseComponent.GetComponent<Dwelling>();
			if (component)
			{
				component.AssignDweller(newBeaver.GetComponent<Dweller>());
				newBeaver.GetComponent<Enterer>().Enter(baseComponent.GetComponent<Enterable>());
			}
		}

		// Token: 0x04000029 RID: 41
		public readonly BeaverFactory _beaverFactory;

		// Token: 0x0400002A RID: 42
		public readonly EventBus _eventBus;
	}
}
