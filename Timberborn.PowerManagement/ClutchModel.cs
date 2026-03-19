using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.PowerManagement
{
	// Token: 0x02000009 RID: 9
	public class ClutchModel : TickableComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedStateListener, IPreviewStateListener
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000022FF File Offset: 0x000004FF
		public ClutchModel(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002318 File Offset: 0x00000518
		public void Awake()
		{
			ClutchModelSpec component = base.GetComponent<ClutchModelSpec>();
			this._clutch = base.GetComponent<Clutch>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._engagedModel = base.GameObject.FindChild(component.EngagedModelName);
			this._disengagedModel = base.GameObject.FindChild(component.DisengagedModelName);
			this._engagedAnimator = this._engagedModel.GetComponentInChildren<IAnimator>(true);
			this._disengagedAnimator = this._disengagedModel.GetComponentInChildren<IAnimator>(true);
			base.DisableComponent();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000239C File Offset: 0x0000059C
		public override void Tick()
		{
			this.UpdateModels();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023A4 File Offset: 0x000005A4
		public void OnEnterFinishedState()
		{
			this.UpdateModels();
			this._clutch.IsEngagedChanged += this.OnIsEngagedChanged;
			this._eventBus.Register(this);
			if (this._clutch.IsEngaged)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023E2 File Offset: 0x000005E2
		public void OnExitFinishedState()
		{
			this._engagedAnimator.Enabled = false;
			this._clutch.IsEngagedChanged -= this.OnIsEngagedChanged;
			this._eventBus.Unregister(this);
			base.DisableComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002419 File Offset: 0x00000619
		public void OnEnterUnfinishedState()
		{
			this.UpdateActiveModels();
			this._clutch.IsEngagedChanged += this.OnIsEngagedChanged;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002438 File Offset: 0x00000638
		public void OnExitUnfinishedState()
		{
			this._clutch.IsEngagedChanged -= this.OnIsEngagedChanged;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002451 File Offset: 0x00000651
		public void OnEnterPreviewState()
		{
			this.UpdateActiveModels();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002459 File Offset: 0x00000659
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimators();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002464 File Offset: 0x00000664
		public void OnIsEngagedChanged(object sender, EventArgs e)
		{
			this.UpdateModels();
			if (this._clutch.IsEngaged)
			{
				this._engagedAnimator.SetTime(this._disengagedAnimator.Time);
				base.EnableComponent();
				return;
			}
			this._disengagedAnimator.SetTime(this._engagedAnimator.Time);
			base.DisableComponent();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024BD File Offset: 0x000006BD
		public void UpdateModels()
		{
			this.UpdateActiveModels();
			this.UpdateAnimators();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024CB File Offset: 0x000006CB
		public void UpdateActiveModels()
		{
			this._engagedModel.SetActive(this._clutch.IsEngaged);
			this._disengagedModel.SetActive(!this._clutch.IsEngaged);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024FC File Offset: 0x000006FC
		public void UpdateAnimators()
		{
			bool activeAndPowered = this._mechanicalNode.ActiveAndPowered;
			this._engagedAnimator.Enabled = activeAndPowered;
			if (activeAndPowered)
			{
				this._engagedAnimator.Speed = this._mechanicalNode.PowerEfficiency * this._nonlinearAnimationManager.SpeedMultiplier;
			}
		}

		// Token: 0x04000013 RID: 19
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000014 RID: 20
		public readonly EventBus _eventBus;

		// Token: 0x04000015 RID: 21
		public Clutch _clutch;

		// Token: 0x04000016 RID: 22
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000017 RID: 23
		public GameObject _engagedModel;

		// Token: 0x04000018 RID: 24
		public GameObject _disengagedModel;

		// Token: 0x04000019 RID: 25
		public IAnimator _engagedAnimator;

		// Token: 0x0400001A RID: 26
		public IAnimator _disengagedAnimator;
	}
}
