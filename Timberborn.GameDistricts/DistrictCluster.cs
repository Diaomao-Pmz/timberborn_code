using System;
using System.Collections.Generic;
using System.Linq;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000019 RID: 25
	public class DistrictCluster
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00003A96 File Offset: 0x00001C96
		public DistrictCluster(DistrictCenter districtCenter)
		{
			this._districtCenters = new HashSet<DistrictCenter>
			{
				districtCenter
			};
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003AB1 File Offset: 0x00001CB1
		public bool TryAddDistrict(DistrictCenter districtCenter)
		{
			if (this._districtCenters.First<DistrictCenter>().IsGloballyReachableFromAnotherDistrict(districtCenter))
			{
				this._districtCenters.Add(districtCenter);
				return true;
			}
			return false;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003AD6 File Offset: 0x00001CD6
		public bool Contains(DistrictCenter districtCenter)
		{
			return this._districtCenters.Contains(districtCenter);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public IEnumerable<DistrictCenter> GetDistrictsOtherThan(DistrictCenter districtCenter)
		{
			foreach (DistrictCenter districtCenter2 in this._districtCenters)
			{
				if (districtCenter2 && districtCenter != districtCenter2)
				{
					yield return districtCenter2;
				}
			}
			HashSet<DistrictCenter>.Enumerator enumerator = default(HashSet<DistrictCenter>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x04000047 RID: 71
		public readonly HashSet<DistrictCenter> _districtCenters;
	}
}
