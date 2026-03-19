using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000017 RID: 23
	public class MechanicalNodeAnimator : TickableComponent, IAwakableComponent, IFinishedStateListener, IPreviewStateListener, IUnfinishedStateListener
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002D66 File Offset: 0x00000F66
		public MechanicalNodeAnimator(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D7C File Offset: 0x00000F7C
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._mechanicalNodeAnimatorSpec = base.GetComponent<MechanicalNodeAnimatorSpec>();
			this._animators = base.GameObject.GetComponentsInChildren<IAnimator>(true);
			base.DisableComponent();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DAE File Offset: 0x00000FAE
		public override void StartTickable()
		{
			this.FindAndUpdateActiveAnimator();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DB6 File Offset: 0x00000FB6
		public override void Tick()
		{
			this.UpdateAnimation();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DBE File Offset: 0x00000FBE
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._eventBus.Register(this);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DE6 File Offset: 0x00000FE6
		public void OnEnterPreviewState()
		{
			this.StopAnimators();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DE6 File Offset: 0x00000FE6
		public void OnEnterUnfinishedState()
		{
			this.StopAnimators();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002DEE File Offset: 0x00000FEE
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DB6 File Offset: 0x00000FB6
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimation();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public void StopAnimators()
		{
			IAnimator[] animators = this._animators;
			for (int i = 0; i < animators.Length; i++)
			{
				animators[i].Enabled = false;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E1B File Offset: 0x0000101B
		public void FindAndUpdateActiveAnimator()
		{
			this._activeAnimator = base.GetComponentInChildren<IAnimator>(false);
			this.StopAnimators();
			this.UpdateAnimation();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E38 File Offset: 0x00001038
		public void UpdateAnimation()
		{
			if (this._activeAnimator != null)
			{
				if (this.CanAnimate())
				{
					this._activeAnimator.Enabled = true;
					this._activeAnimator.Speed = this.GetPowerMultiplier() * this._nonlinearAnimationManager.SpeedMultiplier;
					return;
				}
				this._activeAnimator.Enabled = false;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E8C File Offset: 0x0000108C
		public bool CanAnimate()
		{
			return this._mechanicalNode.ActiveAndPowered && (this._mechanicalNode.IsConsuming || this._mechanicalNode.IsGenerator || (this._mechanicalNode.IsShaft && this._mechanicalNode.Powered) || (this._mechanicalNode.IsIntermediary && this._mechanicalNode.Powered));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EF8 File Offset: 0x000010F8
		public float GetPowerMultiplier()
		{
			float powerEfficiency = this._mechanicalNode.PowerEfficiency;
			float minSpeedMultiplier = this._mechanicalNodeAnimatorSpec.MinSpeedMultiplier;
			return (powerEfficiency + minSpeedMultiplier) / (1f + minSpeedMultiplier);
		}

		// Token: 0x0400003B RID: 59
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400003C RID: 60
		public readonly EventBus _eventBus;

		// Token: 0x0400003D RID: 61
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400003E RID: 62
		public MechanicalNodeAnimatorSpec _mechanicalNodeAnimatorSpec;

		// Token: 0x0400003F RID: 63
		public IAnimator[] _animators;

		// Token: 0x04000040 RID: 64
		public IAnimator _activeAnimator;

		// Token: 0x04000041 RID: 65
		public float _currentAnimationSpeed;
	}
}
