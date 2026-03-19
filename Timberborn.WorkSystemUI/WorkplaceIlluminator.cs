using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Illumination;
using Timberborn.TickSystem;
using Timberborn.WorkSystem;

namespace Timberborn.WorkSystemUI
{
	// Token: 0x02000019 RID: 25
	public class WorkplaceIlluminator : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000379A File Offset: 0x0000199A
		public void Awake()
		{
			this._illuminatorToggle = base.GetComponent<Illuminator>().CreateToggle();
			this._workplace = base.GetComponent<Workplace>();
			base.GetComponent<WorkplaceWorkerType>().WorkerTypeChanged += this.OnWorkerTypeChanged;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037D0 File Offset: 0x000019D0
		public override void StartTickable()
		{
			this.UpdateIllumination(true);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000037D9 File Offset: 0x000019D9
		public override void Tick()
		{
			this.UpdateIllumination(false);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000037D0 File Offset: 0x000019D0
		public void OnWorkerTypeChanged(object sender, WorkerTypeChangedEventArgs e)
		{
			this.UpdateIllumination(true);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000037E4 File Offset: 0x000019E4
		public void UpdateIllumination(bool forceUpdate)
		{
			bool flag = this._workplace.AnyWorkerHasJobRunning();
			if (flag != this._illuminationEnabled || forceUpdate)
			{
				this._illuminationEnabled = flag;
				if (this._illuminationEnabled)
				{
					this._illuminatorToggle.TurnOn();
					return;
				}
				this._illuminatorToggle.TurnOff();
			}
		}

		// Token: 0x0400007E RID: 126
		public IlluminatorToggle _illuminatorToggle;

		// Token: 0x0400007F RID: 127
		public Workplace _workplace;

		// Token: 0x04000080 RID: 128
		public bool _illuminationEnabled;
	}
}
