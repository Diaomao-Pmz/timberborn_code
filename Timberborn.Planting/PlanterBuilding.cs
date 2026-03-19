using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.NaturalResources;
using Timberborn.TemplateSystem;

namespace Timberborn.Planting
{
	// Token: 0x02000012 RID: 18
	public class PlanterBuilding : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A3D File Offset: 0x00000C3D
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002A45 File Offset: 0x00000C45
		public ImmutableArray<PlantableSpec> AllowedPlantables { get; private set; }

		// Token: 0x06000054 RID: 84 RVA: 0x00002A4E File Offset: 0x00000C4E
		public PlanterBuilding(TemplateService templateService)
		{
			this._templateService = templateService;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A60 File Offset: 0x00000C60
		public void Awake()
		{
			this.AllowedPlantables = this.GetAllowedPlantables().ToImmutableArray<PlantableSpec>();
			this._allowedPlantables = (from plantable in this.AllowedPlantables
			select plantable.TemplateName).ToFrozenSet(null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public bool CanPlant(string plantable)
		{
			return this._allowedPlantables.Contains(plantable);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public IEnumerable<PlantableSpec> GetAllowedPlantables()
		{
			PlanterBuildingSpec planterBuildingSpec = base.GetComponent<PlanterBuildingSpec>();
			return from plantable in this._templateService.GetAll<PlantableSpec>()
			where planterBuildingSpec.PlantableResourceGroup == plantable.ResourceGroup && plantable.GetSpec<NaturalResourceSpec>().UsableWithCurrentFeatureToggles
			orderby plantable.GetSpec<NaturalResourceSpec>().Order
			select plantable;
		}

		// Token: 0x04000024 RID: 36
		public readonly TemplateService _templateService;

		// Token: 0x04000025 RID: 37
		public FrozenSet<string> _allowedPlantables;
	}
}
