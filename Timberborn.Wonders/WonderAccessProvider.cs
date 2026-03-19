using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.BuildingsNavigation;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Wonders
{
	// Token: 0x0200000E RID: 14
	public class WonderAccessProvider : BaseComponent, IAwakableComponent, IConstructionSiteAccessProvider
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000250D File Offset: 0x0000070D
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000251B File Offset: 0x0000071B
		public IEnumerable<Vector3> GetAccesses()
		{
			return Enumerables.One<Vector3>(this._buildingAccessible.CalculateAccess());
		}

		// Token: 0x0400001A RID: 26
		public BuildingAccessible _buildingAccessible;
	}
}
