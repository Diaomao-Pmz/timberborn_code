using System;
using System.Collections.Generic;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200002E RID: 46
	public class UnassignedCitizenRegistry
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000489D File Offset: 0x00002A9D
		public void Add(Citizen citizen)
		{
			this._unassignedCitizens.Add(citizen);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000048AC File Offset: 0x00002AAC
		public void Remove(Citizen citizen)
		{
			this._unassignedCitizens.Remove(citizen);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000048BC File Offset: 0x00002ABC
		public void GetUnassignedCitizens(List<Citizen> unassignedCitizens)
		{
			foreach (Citizen item in this._unassignedCitizens)
			{
				unassignedCitizens.Add(item);
			}
		}

		// Token: 0x0400006C RID: 108
		public readonly HashSet<Citizen> _unassignedCitizens = new HashSet<Citizen>();
	}
}
