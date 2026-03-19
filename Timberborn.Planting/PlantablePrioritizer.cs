using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Planting
{
	// Token: 0x0200000B RID: 11
	public class PlantablePrioritizer : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<PlantablePrioritizer>, IDuplicable, IFinishedStateListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000015 RID: 21 RVA: 0x000022DC File Offset: 0x000004DC
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x00002314 File Offset: 0x00000514
		public event EventHandler PrioritizedPlantableChanged;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002349 File Offset: 0x00000549
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002351 File Offset: 0x00000551
		public PlantableSpec PrioritizedPlantableSpec { get; private set; }

		// Token: 0x06000019 RID: 25 RVA: 0x0000235A File Offset: 0x0000055A
		public void Awake()
		{
			this._planterBuilding = base.GetComponent<PlanterBuilding>();
			base.DisableComponent();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000236E File Offset: 0x0000056E
		public void PrioritizePlantable(PlantableSpec plantableSpec)
		{
			this.PrioritizedPlantableSpec = plantableSpec;
			EventHandler prioritizedPlantableChanged = this.PrioritizedPlantableChanged;
			if (prioritizedPlantableChanged == null)
			{
				return;
			}
			prioritizedPlantableChanged(this, EventArgs.Empty);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000238D File Offset: 0x0000058D
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002395 File Offset: 0x00000595
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000239D File Offset: 0x0000059D
		public void Save(IEntitySaver entitySaver)
		{
			if (this.PrioritizedPlantableSpec != null)
			{
				entitySaver.GetComponent(PlantablePrioritizer.PlantablePrioritizerKey).Set(PlantablePrioritizer.PrioritizedPlantableKey, this.PrioritizedPlantableSpec.TemplateName);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023D0 File Offset: 0x000005D0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(PlantablePrioritizer.PlantablePrioritizerKey, out objectLoader))
			{
				string templateName = objectLoader.Get(PlantablePrioritizer.PrioritizedPlantableKey);
				this.PrioritizedPlantableSpec = this._planterBuilding.AllowedPlantables.SingleOrDefault((PlantableSpec plantable) => plantable.TemplateName == templateName);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002428 File Offset: 0x00000628
		public void DuplicateFrom(PlantablePrioritizer source)
		{
			PlantableSpec prioritizedPlantableSpec = source.PrioritizedPlantableSpec;
			if (prioritizedPlantableSpec == null || this._planterBuilding.AllowedPlantables.Contains(prioritizedPlantableSpec))
			{
				this.PrioritizePlantable(prioritizedPlantableSpec);
			}
		}

		// Token: 0x0400000E RID: 14
		public static readonly ComponentKey PlantablePrioritizerKey = new ComponentKey("PlantablePrioritizer");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<string> PrioritizedPlantableKey = new PropertyKey<string>("PrioritizedPlantable");

		// Token: 0x04000012 RID: 18
		public PlanterBuilding _planterBuilding;
	}
}
