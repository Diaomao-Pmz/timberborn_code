using System;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x0200000D RID: 13
	public class DistrictBeaverContaminationStatisticsProvider : BaseComponent, IAwakableComponent, IContaminationStatisticsProvider
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003F RID: 63 RVA: 0x0000287C File Offset: 0x00000A7C
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x000028B4 File Offset: 0x00000AB4
		public event EventHandler ContaminationStatisticsChanged;

		// Token: 0x06000041 RID: 65 RVA: 0x000028E9 File Offset: 0x00000AE9
		public void Awake()
		{
			DistrictPopulation component = base.GetComponent<DistrictPopulation>();
			component.CitizenAssigned += this.OnCitizenAssigned;
			component.CitizenUnassigned += this.OnCitizenUnassigned;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002914 File Offset: 0x00000B14
		public BeaverContaminationStatistics GetContaminationStatistics()
		{
			return new BeaverContaminationStatistics(this._beaverContaminationRegistry.NumberOfContaminatedAdults, this._beaverContaminationRegistry.NumberOfContaminatedChildren);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002934 File Offset: 0x00000B34
		public void OnCitizenAssigned(object sender, CitizenAssignedEventArgs citizenAssignedEventArgs)
		{
			Contaminable component = citizenAssignedEventArgs.Citizen.GetComponent<Contaminable>();
			if (component != null)
			{
				this._beaverContaminationRegistry.AddContaminable(component);
				component.ContaminationChanged += this.OnContaminationChanged;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002970 File Offset: 0x00000B70
		public void OnCitizenUnassigned(object sender, CitizenUnassignedEventArgs citizenUnassignedEventArgs)
		{
			Contaminable component = citizenUnassignedEventArgs.Citizen.GetComponent<Contaminable>();
			if (component != null)
			{
				this._beaverContaminationRegistry.RemoveContaminable(component);
				component.ContaminationChanged -= this.OnContaminationChanged;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029AA File Offset: 0x00000BAA
		public void OnContaminationChanged(object sender, EventArgs e)
		{
			EventHandler contaminationStatisticsChanged = this.ContaminationStatisticsChanged;
			if (contaminationStatisticsChanged == null)
			{
				return;
			}
			contaminationStatisticsChanged(this, EventArgs.Empty);
		}

		// Token: 0x04000024 RID: 36
		public readonly BeaverContaminationRegistry _beaverContaminationRegistry = new BeaverContaminationRegistry();
	}
}
