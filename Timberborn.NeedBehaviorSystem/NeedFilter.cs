using System;
using Timberborn.NeedSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200001E RID: 30
	public readonly struct NeedFilter
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000032E0 File Offset: 0x000014E0
		public NeedFilter(NeedManager needManager, bool onlyCritical, bool onlyCriticalState, bool belowWarningThreshold)
		{
			this._needManager = needManager;
			this._onlyCritical = onlyCritical;
			this._onlyCriticalState = onlyCriticalState;
			this._belowWarningThreshold = belowWarningThreshold;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032FF File Offset: 0x000014FF
		public static NeedFilter NeedIsInCriticalState(NeedManager needManager)
		{
			return new NeedFilter(needManager, true, true, false);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000330A File Offset: 0x0000150A
		public static NeedFilter NeedIsCritical(NeedManager needManager)
		{
			return new NeedFilter(needManager, true, false, false);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003315 File Offset: 0x00001515
		public static NeedFilter NeedIsBelowWarningThreshold(NeedManager needManager)
		{
			return new NeedFilter(needManager, false, false, true);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003320 File Offset: 0x00001520
		public static NeedFilter AnyNeed()
		{
			return new NeedFilter(null, false, false, false);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000332B File Offset: 0x0000152B
		public bool OnlyCriticalStateNeeds
		{
			get
			{
				return this._needManager != null && this._onlyCriticalState;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003340 File Offset: 0x00001540
		public bool Filter(string needId)
		{
			return this._needManager == null || ((!this._onlyCritical || this._needManager.NeedIsCritical(needId)) && (!this._onlyCriticalState || this._needManager.NeedIsInCriticalState(needId)) && (!this._belowWarningThreshold || this._needManager.NeedIsBelowWarningThreshold(needId)));
		}

		// Token: 0x04000047 RID: 71
		public readonly NeedManager _needManager;

		// Token: 0x04000048 RID: 72
		public readonly bool _onlyCritical;

		// Token: 0x04000049 RID: 73
		public readonly bool _onlyCriticalState;

		// Token: 0x0400004A RID: 74
		public readonly bool _belowWarningThreshold;
	}
}
