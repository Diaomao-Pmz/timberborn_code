using System;
using UnityEngine;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000029 RID: 41
	public interface ICitizenPositionOverrider
	{
		// Token: 0x06000118 RID: 280
		bool TryGetOverridenPosition(out Vector3 position);
	}
}
