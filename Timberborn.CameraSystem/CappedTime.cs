using System;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000013 RID: 19
	public static class CappedTime
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003FB8 File Offset: 0x000021B8
		public static float CappedUnscaledDeltaTime()
		{
			return Math.Min(Time.unscaledDeltaTime, 0.2f);
		}
	}
}
