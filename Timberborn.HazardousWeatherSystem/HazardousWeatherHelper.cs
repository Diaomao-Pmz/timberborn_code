using System;
using UnityEngine;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x02000007 RID: 7
	public static class HazardousWeatherHelper
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000025B8 File Offset: 0x000007B8
		public static float GetHandicapMultiplier(int cycle, float handicapMultiplier, float handicapCycles)
		{
			if (handicapCycles > 0f)
			{
				float num = Mathf.Clamp01((float)(cycle - 1) / handicapCycles);
				return Mathf.Lerp(handicapMultiplier, 1f, num);
			}
			return 1f;
		}
	}
}
