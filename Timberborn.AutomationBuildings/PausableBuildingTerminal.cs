using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Localization;
using Timberborn.StatusSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200002B RID: 43
	public class PausableBuildingTerminal : BaseComponent, IAwakableComponent, IStartableComponent, IFinishedStateListener, ITerminal
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00005ACF File Offset: 0x00003CCF
		public PausableBuildingTerminal(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public void Awake()
		{
			this._automatable = base.GetComponent<Automatable>();
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._pausableBuilding = base.GetComponent<PausableBuilding>();
			this._statusToggle = StatusToggle.CreatePriorityStatusWithFloatingIcon("PausedByAutomation", this._loc.T(PausableBuildingTerminal.PausedByAutomationLocKey), 0f);
			base.DisableComponent();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005B3C File Offset: 0x00003D3C
		public void Start()
		{
			base.GetComponent<StatusSubject>().RegisterStatus(this._statusToggle);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005B4F File Offset: 0x00003D4F
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateBlockable();
			this._pausableBuilding.PausedChanged += this.OnPausedChanged;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005B74 File Offset: 0x00003D74
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.UpdateBlockable();
			this._pausableBuilding.PausedChanged -= this.OnPausedChanged;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005B99 File Offset: 0x00003D99
		public void Evaluate()
		{
			this.UpdateBlockable();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005BA1 File Offset: 0x00003DA1
		public void OnPausedChanged(object sender, EventArgs e)
		{
			this.UpdateStatusToggle();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005BA9 File Offset: 0x00003DA9
		public void UpdateBlockable()
		{
			if (base.Enabled && this.ShouldBlock())
			{
				this._blockableObject.Block(this);
				this.UpdateStatusToggle();
				return;
			}
			this._blockableObject.Unblock(this);
			this.UpdateStatusToggle();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public void UpdateStatusToggle()
		{
			this._statusToggle.Toggle(this.ShouldBlock() && !this._pausableBuilding.Paused);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005C06 File Offset: 0x00003E06
		public bool ShouldBlock()
		{
			return this._automatable.State == ConnectionState.Off;
		}

		// Token: 0x040000C6 RID: 198
		public static readonly string PausedByAutomationLocKey = "Automation.PausedByAutomation";

		// Token: 0x040000C7 RID: 199
		public readonly ILoc _loc;

		// Token: 0x040000C8 RID: 200
		public Automatable _automatable;

		// Token: 0x040000C9 RID: 201
		public BlockableObject _blockableObject;

		// Token: 0x040000CA RID: 202
		public PausableBuilding _pausableBuilding;

		// Token: 0x040000CB RID: 203
		public StatusToggle _statusToggle;
	}
}
