using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000018 RID: 24
	public class DistrictCitizenLifecycleNotifier : BaseComponent
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060000B1 RID: 177 RVA: 0x0000397C File Offset: 0x00001B7C
		// (remove) Token: 0x060000B2 RID: 178 RVA: 0x000039B4 File Offset: 0x00001BB4
		public event EventHandler<Citizen> BeaverBorn;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000B3 RID: 179 RVA: 0x000039EC File Offset: 0x00001BEC
		// (remove) Token: 0x060000B4 RID: 180 RVA: 0x00003A24 File Offset: 0x00001C24
		public event EventHandler<Citizen> BeaverDied;

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A59 File Offset: 0x00001C59
		public void AddNewCitizen(Citizen citizen)
		{
			if (citizen.GetComponent<Child>())
			{
				EventHandler<Citizen> beaverBorn = this.BeaverBorn;
				if (beaverBorn == null)
				{
					return;
				}
				beaverBorn(this, citizen);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A7A File Offset: 0x00001C7A
		public void RemoveDiedCitizen(Citizen citizen)
		{
			if (citizen.HasComponent<BeaverSpec>())
			{
				EventHandler<Citizen> beaverDied = this.BeaverDied;
				if (beaverDied == null)
				{
					return;
				}
				beaverDied(this, citizen);
			}
		}
	}
}
