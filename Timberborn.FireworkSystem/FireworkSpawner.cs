using System;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.FireworkSystem
{
	// Token: 0x02000010 RID: 16
	public class FireworkSpawner
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000315F File Offset: 0x0000135F
		public FireworkSpawner(FireworkSpecService fireworkSpecService, EntityService entityService)
		{
			this._fireworkSpecService = fireworkSpecService;
			this._entityService = entityService;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003178 File Offset: 0x00001378
		public void SpawnFirework(FireworkLauncher fireworkLauncher)
		{
			FireworkSpec fireworkSpec = this._fireworkSpecService.GetFireworkSpec(fireworkLauncher.FireworkId);
			EntityComponent entityComponent = this._entityService.Instantiate(fireworkSpec.Blueprint);
			Transform barrelTransform = fireworkLauncher.GetComponent<FireworkLauncherModel>().GetBarrelTransform();
			entityComponent.GameObject.SetActive(true);
			entityComponent.GetComponent<Firework>().Launch(barrelTransform.position, barrelTransform.rotation, (float)fireworkLauncher.FlightDistance);
		}

		// Token: 0x04000051 RID: 81
		public readonly FireworkSpecService _fireworkSpecService;

		// Token: 0x04000052 RID: 82
		public readonly EntityService _entityService;

		// Token: 0x04000053 RID: 83
		public GameObject _prefab;
	}
}
