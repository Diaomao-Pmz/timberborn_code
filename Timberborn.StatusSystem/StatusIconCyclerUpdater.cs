using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000015 RID: 21
	public class StatusIconCyclerUpdater : IUpdatableSingleton
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002D90 File Offset: 0x00000F90
		public void UpdateSingleton()
		{
			for (int i = this._statusIconCyclers.Count - 1; i >= 0; i--)
			{
				this._statusIconCyclers[i].UpdateStatusVisibility();
			}
			float unscaledTime = Time.unscaledTime;
			if (unscaledTime > this._nextUpdateTime)
			{
				for (int j = this._statusIconCyclers.Count - 1; j >= 0; j--)
				{
					this._statusIconCyclers[j].IntervalUpdate();
				}
				this._nextUpdateTime = unscaledTime + StatusIconCyclerUpdater.UpdateInterval;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E0B File Offset: 0x0000100B
		public void AddStatusIconCycler(StatusIconCycler statusIconCycler)
		{
			this._statusIconCyclers.Add(statusIconCycler);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E19 File Offset: 0x00001019
		public void RemoveStatusIconCycler(StatusIconCycler statusIconCycler)
		{
			this._statusIconCyclers.Remove(statusIconCycler);
		}

		// Token: 0x0400003B RID: 59
		public static readonly float UpdateInterval = 1.25f;

		// Token: 0x0400003C RID: 60
		public readonly List<StatusIconCycler> _statusIconCyclers = new List<StatusIconCycler>();

		// Token: 0x0400003D RID: 61
		public float _nextUpdateTime;
	}
}
