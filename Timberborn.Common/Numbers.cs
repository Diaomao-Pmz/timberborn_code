using System;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000026 RID: 38
	public static class Numbers
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000341F File Offset: 0x0000161F
		public static float RoundToPrecision(float value, float precision)
		{
			return Mathf.Round(value / precision) * precision;
		}
	}
}
