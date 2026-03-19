using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace Timberborn.AlertPanelSystem
{
	// Token: 0x02000006 RID: 6
	public class AlertPanelModule
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022B7 File Offset: 0x000004B7
		public FrozenDictionary<int, IAlertFragment> AlertFragments { get; }

		// Token: 0x0600000D RID: 13 RVA: 0x000022BF File Offset: 0x000004BF
		public AlertPanelModule(Dictionary<int, IAlertFragment> panels)
		{
			this.AlertFragments = panels.ToFrozenDictionary(null);
		}

		// Token: 0x02000007 RID: 7
		public class Builder
		{
			// Token: 0x0600000E RID: 14 RVA: 0x000022D4 File Offset: 0x000004D4
			public void AddAlertFragment(IAlertFragment alertFragment, int order)
			{
				this._alertFragments.Add(order, alertFragment);
			}

			// Token: 0x0600000F RID: 15 RVA: 0x000022E3 File Offset: 0x000004E3
			public AlertPanelModule Build()
			{
				return new AlertPanelModule(this._alertFragments);
			}

			// Token: 0x04000010 RID: 16
			public readonly Dictionary<int, IAlertFragment> _alertFragments = new Dictionary<int, IAlertFragment>();
		}
	}
}
