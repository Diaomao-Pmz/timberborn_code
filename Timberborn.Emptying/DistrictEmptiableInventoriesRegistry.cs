using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.InventorySystem;

namespace Timberborn.Emptying
{
	// Token: 0x02000005 RID: 5
	public class DistrictEmptiableInventoriesRegistry : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002167 File Offset: 0x00000367
		public ReadOnlyList<Inventories> EmptiableInventories
		{
			get
			{
				return this._emptiableInventories.AsReadOnlyList<Inventories>();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public void Awake()
		{
			DistrictBuildingRegistry component = base.GetComponent<DistrictBuildingRegistry>();
			component.FinishedBuildingRegistered += this.OnFinishedBuildingRegistered;
			component.FinishedBuildingUnregistered += this.OnFinishedBuildingUnregistered;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A0 File Offset: 0x000003A0
		public void OnFinishedBuildingRegistered(object sender, FinishedBuildingRegisteredEventArgs e)
		{
			Emptiable component = e.Building.GetComponent<Emptiable>();
			if (component != null)
			{
				component.MarkedForEmptying += this.OnMarkedForEmptying;
				component.UnmarkedForEmptying += this.OnUnmarkedForEmptying;
				if (component.IsMarkedForEmptying)
				{
					this.Add(component);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F0 File Offset: 0x000003F0
		public void OnFinishedBuildingUnregistered(object sender, FinishedBuildingUnregisteredEventArgs e)
		{
			Emptiable component = e.Building.GetComponent<Emptiable>();
			if (component != null)
			{
				component.MarkedForEmptying -= this.OnMarkedForEmptying;
				component.UnmarkedForEmptying -= this.OnUnmarkedForEmptying;
				this.Remove(component);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002237 File Offset: 0x00000437
		public void OnMarkedForEmptying(object sender, EventArgs e)
		{
			this.Add((Emptiable)sender);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002245 File Offset: 0x00000445
		public void OnUnmarkedForEmptying(object sender, EventArgs e)
		{
			this.Remove((Emptiable)sender);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
		public void Add(Emptiable emptiable)
		{
			Inventories component = emptiable.GetComponent<Inventories>();
			if (!this._emptiableInventories.Contains(component))
			{
				this._emptiableInventories.Add(component);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002282 File Offset: 0x00000482
		public void Remove(Emptiable emptiable)
		{
			this._emptiableInventories.Remove(emptiable.GetComponent<Inventories>());
		}

		// Token: 0x04000008 RID: 8
		public readonly List<Inventories> _emptiableInventories = new List<Inventories>();
	}
}
