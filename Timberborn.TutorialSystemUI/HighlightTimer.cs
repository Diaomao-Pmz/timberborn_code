using System;
using UnityEngine;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000008 RID: 8
	public static class HighlightTimer
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022AD File Offset: 0x000004AD
		public static bool IsTimeForSteadyHighlight()
		{
			return HighlightTimer.IsTimeForHighlight(0.5f);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B9 File Offset: 0x000004B9
		public static bool IsTimeForPulsingHighlight()
		{
			return HighlightTimer.IsTimeForHighlight(3f) && HighlightTimer.IsTimeForHighlight(0.5f);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022D3 File Offset: 0x000004D3
		public static bool IsTimeForHighlight(float highlightDuration)
		{
			return Time.unscaledTime % (highlightDuration * 2f) > highlightDuration;
		}
	}
}
