using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;
using Timberborn.GameDistricts;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000007 RID: 7
	public class CitizenDistrictTintChanger : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public void Awake()
		{
			this._districtPopulation = base.GetComponent<DistrictPopulation>();
			base.GetComponent<NamedEntity>().EntityNameChanged += delegate(object _, EventArgs _)
			{
				this.UpdatePopulationTint();
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public void Start()
		{
			this._districtPopulation.CitizenAssigned += delegate(object _, CitizenAssignedEventArgs e)
			{
				CitizenDistrictTintChanger.UpdateCitizenTint(e.Citizen);
			};
			this._districtPopulation.CitizenUnassigned += delegate(object _, CitizenUnassignedEventArgs e)
			{
				CitizenDistrictTintChanger.UpdateCitizenTint(e.Citizen);
			};
			this.UpdatePopulationTint();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		public void UpdatePopulationTint()
		{
			for (int i = 0; i < this._districtPopulation.Beavers.Count; i++)
			{
				CitizenDistrictTintChanger.UpdateCitizenTint(this._districtPopulation.Beavers[i]);
			}
			for (int j = 0; j < this._districtPopulation.Bots.Count; j++)
			{
				CitizenDistrictTintChanger.UpdateCitizenTint(this._districtPopulation.Bots[j]);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220C File Offset: 0x0000040C
		public static void UpdateCitizenTint(BaseComponent citizen)
		{
			CitizenTint component = citizen.GetComponent<CitizenTint>();
			if (component)
			{
				component.UpdateTint();
			}
		}

		// Token: 0x04000008 RID: 8
		public DistrictPopulation _districtPopulation;
	}
}
