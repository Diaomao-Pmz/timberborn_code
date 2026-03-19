using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.Planting;
using Timberborn.WorldPersistence;

namespace Timberborn.Fields
{
	// Token: 0x02000009 RID: 9
	public class FarmHouse : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<FarmHouse>, IDuplicable, IFinishedStateListener, IPlantingSpotValidator
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022CC File Offset: 0x000004CC
		public bool PlantingPrioritized { get; private set; } = true;

		// Token: 0x06000020 RID: 32 RVA: 0x000022D5 File Offset: 0x000004D5
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022DD File Offset: 0x000004DD
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022D5 File Offset: 0x000004D5
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022E5 File Offset: 0x000004E5
		public void PrioritizePlanting()
		{
			this.PlantingPrioritized = true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022EE File Offset: 0x000004EE
		public void UnprioritizePlanting()
		{
			this.PlantingPrioritized = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022F7 File Offset: 0x000004F7
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(FarmHouse.FarmHouseKey).Set(FarmHouse.PlantingPrioritizedKey, this.PlantingPrioritized);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002314 File Offset: 0x00000514
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(FarmHouse.FarmHouseKey);
			this.PlantingPrioritized = component.Get(FarmHouse.PlantingPrioritizedKey);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000233E File Offset: 0x0000053E
		public void DuplicateFrom(FarmHouse source)
		{
			this.PlantingPrioritized = source.PlantingPrioritized;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000234C File Offset: 0x0000054C
		public bool Validate(PlantingSpot spot)
		{
			return !spot.PlantingBlocker;
		}

		// Token: 0x0400000B RID: 11
		public static readonly ComponentKey FarmHouseKey = new ComponentKey("FarmHouse");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<bool> PlantingPrioritizedKey = new PropertyKey<bool>("PlantingPrioritized");
	}
}
