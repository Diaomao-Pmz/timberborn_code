using System;
using Timberborn.Localization;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200000C RID: 12
	public class DistanceToDistrictDescriber
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000268E File Offset: 0x0000088E
		public DistanceToDistrictDescriber(NavigationDistance navigationDistance, ILoc loc)
		{
			this._navigationDistance = navigationDistance;
			this._loc = loc;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026A4 File Offset: 0x000008A4
		public string DescribeDistance(float distance)
		{
			int num = Mathf.RoundToInt(distance);
			string text = "<align=\"center\">" + this._loc.T<int>(DistanceToDistrictDescriber.DistanceLocKey, num) + "</align>";
			string str = "<color=\"red\">" + this._loc.T<int>(DistanceToDistrictDescriber.DistanceLargeLocKey, num) + "</color>";
			if ((float)num <= this._navigationDistance.LargeDistrictThreshold)
			{
				return text;
			}
			return text + "\n" + str;
		}

		// Token: 0x04000018 RID: 24
		public static readonly string DistanceLocKey = "Enterable.DistanceToDistrict";

		// Token: 0x04000019 RID: 25
		public static readonly string DistanceLargeLocKey = "Enterable.DistanceToDistrictLarge";

		// Token: 0x0400001A RID: 26
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x0400001B RID: 27
		public readonly ILoc _loc;
	}
}
