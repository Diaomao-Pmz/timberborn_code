using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000008 RID: 8
	public class GoodPoweredGeneratorAnimator : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000228A File Offset: 0x0000048A
		public GoodPoweredGeneratorAnimator(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022A0 File Offset: 0x000004A0
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			base.DisableComponent();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C1 File Offset: 0x000004C1
		public override void Tick()
		{
			this.UpdateAnimation();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C9 File Offset: 0x000004C9
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._eventBus.Register(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022DD File Offset: 0x000004DD
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022C1 File Offset: 0x000004C1
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimation();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F4 File Offset: 0x000004F4
		public void UpdateAnimation()
		{
			if (this._mechanicalNode.OutputMultiplier > 0f)
			{
				this._animator.Enabled = true;
				this._animator.Speed = this._nonlinearAnimationManager.SpeedMultiplier;
				return;
			}
			this._animator.Enabled = false;
		}

		// Token: 0x0400000D RID: 13
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400000E RID: 14
		public readonly EventBus _eventBus;

		// Token: 0x0400000F RID: 15
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000010 RID: 16
		public IAnimator _animator;
	}
}
