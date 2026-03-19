using System;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000012 RID: 18
	public class ConstructionSiteBuildTimeCalculator
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000034C0 File Offset: 0x000016C0
		public float GetConstructionTimeInHours(ConstructionSite constructionSite)
		{
			ConstructionSiteBuildTimeSpec component = constructionSite.GetComponent<ConstructionSiteBuildTimeSpec>();
			if (component != null)
			{
				return component.ConstructionTimeInHours;
			}
			int capacity = constructionSite.Inventory.Capacity;
			return (float)((capacity == 0) ? ConstructionSiteBuildTimeCalculator.DefaultTimeInHours : Mathf.CeilToInt((float)capacity / ConstructionSiteBuildTimeCalculator.MaterialToHourRatio));
		}

		// Token: 0x04000047 RID: 71
		public static readonly int DefaultTimeInHours = 1;

		// Token: 0x04000048 RID: 72
		public static readonly float MaterialToHourRatio = 20f;
	}
}
