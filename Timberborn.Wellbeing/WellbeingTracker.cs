using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200001B RID: 27
	public class WellbeingTracker : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007C RID: 124 RVA: 0x0000303C File Offset: 0x0000123C
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00003074 File Offset: 0x00001274
		public event EventHandler<WellbeingChangedEventArgs> WellbeingChanged;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000030A9 File Offset: 0x000012A9
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000030B1 File Offset: 0x000012B1
		public int Wellbeing { get; private set; }

		// Token: 0x06000080 RID: 128 RVA: 0x000030BA File Offset: 0x000012BA
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000030C8 File Offset: 0x000012C8
		public void InitializeEntity()
		{
			this._needManager.NeedChangedActiveState += this.OnNeedChangedActiveState;
			this._needManager.NeedChangedIsFavorable += this.OnNeedChangedIsFavorable;
			this.UpdateWellbeing();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030FE File Offset: 0x000012FE
		public void OnNeedChangedActiveState(object sender, NeedChangedActiveStateEventArgs e)
		{
			this.UpdateWellbeing();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030FE File Offset: 0x000012FE
		public void OnNeedChangedIsFavorable(object sender, NeedChangedIsFavorableEventArgs e)
		{
			this.UpdateWellbeing();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003108 File Offset: 0x00001308
		public void UpdateWellbeing()
		{
			int wellbeing = this.Wellbeing;
			this.Wellbeing = 0;
			for (int i = 0; i < this._needManager.NeedSpecs.Length; i++)
			{
				NeedSpec needSpec = this._needManager.NeedSpecs[i];
				this.Wellbeing += this._needManager.GetNeedWellbeing(needSpec.Id);
			}
			if (wellbeing != this.Wellbeing)
			{
				EventHandler<WellbeingChangedEventArgs> wellbeingChanged = this.WellbeingChanged;
				if (wellbeingChanged == null)
				{
					return;
				}
				wellbeingChanged(this, new WellbeingChangedEventArgs(wellbeing, this.Wellbeing));
			}
		}

		// Token: 0x0400003C RID: 60
		public NeedManager _needManager;
	}
}
