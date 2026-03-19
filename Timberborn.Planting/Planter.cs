using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000011 RID: 17
	public class Planter : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000290D File Offset: 0x00000B0D
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002915 File Offset: 0x00000B15
		public Vector3Int? PlantingCoordinates { get; private set; }

		// Token: 0x06000049 RID: 73 RVA: 0x0000291E File Offset: 0x00000B1E
		public Planter(PlantingService plantingService)
		{
			this._plantingService = plantingService;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000292D File Offset: 0x00000B2D
		public void Awake()
		{
			base.GetComponent<Worker>().GotUnemployed += delegate(object _, EventArgs _)
			{
				this.Unreserve();
			};
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002946 File Offset: 0x00000B46
		public void DeleteEntity()
		{
			this.Unreserve();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000294E File Offset: 0x00000B4E
		public void Reserve(Vector3Int plantingCoordinates)
		{
			this.PlantingCoordinates = new Vector3Int?(plantingCoordinates);
			this._plantingService.ReservePlantingCoordinates(plantingCoordinates);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002968 File Offset: 0x00000B68
		public void Unreserve()
		{
			if (this.PlantingCoordinates != null)
			{
				this._plantingService.UnreservePlantingCoordinates(this.PlantingCoordinates.Value);
				this.PlantingCoordinates = null;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void Save(IEntitySaver entitySaver)
		{
			if (this.PlantingCoordinates != null)
			{
				entitySaver.GetComponent(Planter.PlanterKey).Set(Planter.PlantingCoordinatesKey, this.PlantingCoordinates.Value);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029F0 File Offset: 0x00000BF0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Planter.PlanterKey, out objectLoader))
			{
				this.Reserve(objectLoader.Get(Planter.PlantingCoordinatesKey));
			}
		}

		// Token: 0x0400001F RID: 31
		public static readonly ComponentKey PlanterKey = new ComponentKey("Planter");

		// Token: 0x04000020 RID: 32
		public static readonly PropertyKey<Vector3Int> PlantingCoordinatesKey = new PropertyKey<Vector3Int>("PlantingCoordinates");

		// Token: 0x04000022 RID: 34
		public readonly PlantingService _plantingService;
	}
}
