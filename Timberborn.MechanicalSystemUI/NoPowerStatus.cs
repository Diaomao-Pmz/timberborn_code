using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using Timberborn.StatusSystem;
using Timberborn.TickSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000029 RID: 41
	public class NoPowerStatus : TickableComponent, IAwakableComponent
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000041AE File Offset: 0x000023AE
		public NoPowerStatus(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000041BD File Offset: 0x000023BD
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNode.AddedToGraph += this.OnAddedToGraph;
			base.DisableComponent();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000041E8 File Offset: 0x000023E8
		public override void StartTickable()
		{
			this.UpdateStatus();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000041E8 File Offset: 0x000023E8
		public override void Tick()
		{
			this.UpdateStatus();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000041F0 File Offset: 0x000023F0
		public void OnAddedToGraph(object sender, EventArgs e)
		{
			if (this._mechanicalNode.IsConsumer)
			{
				this.InitializeStatus();
				this.UpdateStatus();
				base.EnableComponent();
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004214 File Offset: 0x00002414
		public void InitializeStatus()
		{
			if (this._noPowerStatusToggle == null)
			{
				if (base.HasComponent<NoPowerStatusAlertDisablerSpec>())
				{
					this._noPowerStatusToggle = StatusToggle.CreateNormalStatus(NoPowerStatus.NoPowerSpriteName, this._loc.T(NoPowerStatus.NoPowerLocKey));
				}
				else
				{
					this._noPowerStatusToggle = StatusToggle.CreateNormalStatusWithAlertAndFloatingIcon(NoPowerStatus.NoPowerSpriteName, this._loc.T(NoPowerStatus.NoPowerLocKey), this._loc.T(NoPowerStatus.NoPowerShortLocKey), 0f);
				}
				base.GetComponent<StatusSubject>().RegisterStatus(this._noPowerStatusToggle);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000429C File Offset: 0x0000249C
		public void UpdateStatus()
		{
			if (!this._mechanicalNode.CanPotentiallyBePowered())
			{
				this._noPowerStatusToggle.Activate();
				return;
			}
			if (this._mechanicalNode.Powered || !this._mechanicalNode.IsConsuming)
			{
				this._noPowerStatusToggle.Deactivate();
				return;
			}
			this._noPowerStatusToggle.Activate();
		}

		// Token: 0x04000082 RID: 130
		public static readonly string NoPowerSpriteName = "NoPower";

		// Token: 0x04000083 RID: 131
		public static readonly string NoPowerLocKey = "Status.Mechanical.NoPower";

		// Token: 0x04000084 RID: 132
		public static readonly string NoPowerShortLocKey = "Status.Mechanical.NoPower.Short";

		// Token: 0x04000085 RID: 133
		public readonly ILoc _loc;

		// Token: 0x04000086 RID: 134
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000087 RID: 135
		public StatusToggle _noPowerStatusToggle;
	}
}
