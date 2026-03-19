using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200001D RID: 29
	public class ZiplineTowerAnimationController : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00004585 File Offset: 0x00002785
		public ZiplineTowerAnimationController(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000459B File Offset: 0x0000279B
		public void Awake()
		{
			this._ziplineTowerOperationValidator = base.GetComponent<ZiplineTowerOperationValidator>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000045B6 File Offset: 0x000027B6
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimatorSpeed();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000045BE File Offset: 0x000027BE
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._ziplineTowerOperationValidator.OperativeStateChanged += this.OnOperativeStateChanged;
			this.UpdateAnimatorState();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000045E9 File Offset: 0x000027E9
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._ziplineTowerOperationValidator.OperativeStateChanged -= this.OnOperativeStateChanged;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000460E File Offset: 0x0000280E
		public void OnOperativeStateChanged(object sender, EventArgs e)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004616 File Offset: 0x00002816
		public void UpdateAnimatorState()
		{
			this._animator.Enabled = this._ziplineTowerOperationValidator.IsOperative;
			this.UpdateAnimatorSpeed();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004634 File Offset: 0x00002834
		public void UpdateAnimatorSpeed()
		{
			this._animator.Speed = this._nonlinearAnimationManager.SpeedMultiplier;
		}

		// Token: 0x04000059 RID: 89
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400005A RID: 90
		public readonly EventBus _eventBus;

		// Token: 0x0400005B RID: 91
		public ZiplineTowerOperationValidator _ziplineTowerOperationValidator;

		// Token: 0x0400005C RID: 92
		public IAnimator _animator;
	}
}
