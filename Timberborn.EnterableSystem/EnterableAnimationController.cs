using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000008 RID: 8
	public class EnterableAnimationController : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002511 File Offset: 0x00000711
		public EnterableAnimationController(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002527 File Offset: 0x00000727
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._enterableAnimationControllerSpec = base.GetComponent<EnterableAnimationControllerSpec>();
			base.DisableComponent();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002554 File Offset: 0x00000754
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._enterable.EntererAdded += this.OnEntererAdded;
			this._enterable.EntererRemoved += this.OnEntererRemoved;
			base.EnableComponent();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025A4 File Offset: 0x000007A4
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._enterable.EntererAdded -= this.OnEntererAdded;
			this._enterable.EntererRemoved -= this.OnEntererRemoved;
			base.DisableComponent();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025F1 File Offset: 0x000007F1
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025F9 File Offset: 0x000007F9
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			if (this._enterableAnimationControllerSpec.ResetAnimationUponExit)
			{
				this._animator.SetTime(0f);
			}
			this.UpdateAnimatorState();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025F1 File Offset: 0x000007F1
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000261E File Offset: 0x0000081E
		public void UpdateAnimatorState()
		{
			this._animator.Enabled = (this._enterable.NumberOfEnterersInside > 0);
			this._animator.Speed = this._nonlinearAnimationManager.SpeedMultiplier;
		}

		// Token: 0x0400000E RID: 14
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400000F RID: 15
		public readonly EventBus _eventBus;

		// Token: 0x04000010 RID: 16
		public Enterable _enterable;

		// Token: 0x04000011 RID: 17
		public IAnimator _animator;

		// Token: 0x04000012 RID: 18
		public EnterableAnimationControllerSpec _enterableAnimationControllerSpec;
	}
}
