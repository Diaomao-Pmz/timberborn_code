using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200001D RID: 29
	public class WellbeingTrackerRegistry
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000032C6 File Offset: 0x000014C6
		public void Register(WellbeingTracker wellbeingTracker)
		{
			this._wellbeingTrackers.Add(wellbeingTracker);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032D5 File Offset: 0x000014D5
		public void Unregister(WellbeingTracker wellbeingTracker)
		{
			this._wellbeingTrackers.Remove(wellbeingTracker);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032E4 File Offset: 0x000014E4
		public int GetAverageWellbeing()
		{
			if (this._wellbeingTrackers.Count > 0)
			{
				float num = 0f;
				foreach (WellbeingTracker wellbeingTracker in this._wellbeingTrackers)
				{
					num += (float)wellbeingTracker.Wellbeing;
				}
				return Mathf.FloorToInt(num / (float)this._wellbeingTrackers.Count);
			}
			return 0;
		}

		// Token: 0x04000041 RID: 65
		public readonly HashSet<WellbeingTracker> _wellbeingTrackers = new HashSet<WellbeingTracker>();
	}
}
