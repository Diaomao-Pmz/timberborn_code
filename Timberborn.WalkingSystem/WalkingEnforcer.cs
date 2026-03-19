using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200001F RID: 31
	public class WalkingEnforcer : BaseComponent
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003E4D File Offset: 0x0000204D
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003E55 File Offset: 0x00002055
		public bool ForcedWalking { get; private set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00003E60 File Offset: 0x00002060
		public WalkingEnforcerToggle GetWalkingEnforcerToggle()
		{
			WalkingEnforcerToggle walkingEnforcerToggle = new WalkingEnforcerToggle();
			this._toggles.Add(walkingEnforcerToggle);
			walkingEnforcerToggle.ForcedWalkingChanged += this.OnForcedWalkingChanged;
			return walkingEnforcerToggle;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E94 File Offset: 0x00002094
		public void OnForcedWalkingChanged(object sender, EventArgs e)
		{
			bool flag = this._toggles.FastAny((WalkingEnforcerToggle toggle) => toggle.ForcedWalking);
			if (this.ForcedWalking != flag)
			{
				this.ForcedWalking = flag;
				EventHandler forcedWalkingChanged = this.ForcedWalkingChanged;
				if (forcedWalkingChanged == null)
				{
					return;
				}
				forcedWalkingChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000069 RID: 105
		public EventHandler ForcedWalkingChanged;

		// Token: 0x0400006A RID: 106
		public readonly List<WalkingEnforcerToggle> _toggles = new List<WalkingEnforcerToggle>();
	}
}
