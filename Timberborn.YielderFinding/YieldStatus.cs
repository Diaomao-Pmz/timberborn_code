using System;
using Timberborn.BaseComponentSystem;
using Timberborn.WorkSystem;

namespace Timberborn.YielderFinding
{
	// Token: 0x0200000C RID: 12
	public class YieldStatus : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000024E6 File Offset: 0x000006E6
		public void Awake()
		{
			this._nothingToDoInRangeStatus = base.GetComponent<NothingToDoInRangeStatus>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024F4 File Offset: 0x000006F4
		public void UpdateStatus(YielderSearchResult yielderSearchResult)
		{
			if (yielderSearchResult.NoYielderInRange)
			{
				this._nothingToDoInRangeStatus.ActivateStatus();
				return;
			}
			this._nothingToDoInRangeStatus.DeactivateStatus();
		}

		// Token: 0x04000011 RID: 17
		public NothingToDoInRangeStatus _nothingToDoInRangeStatus;
	}
}
