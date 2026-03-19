using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.BlockingSystem
{
	// Token: 0x02000008 RID: 8
	public class BlockableObjectAnimationController : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000226C File Offset: 0x0000046C
		public BlockableObjectAnimationController(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002282 File Offset: 0x00000482
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000229D File Offset: 0x0000049D
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A8 File Offset: 0x000004A8
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			this.UpdateAnimatorState();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F5 File Offset: 0x000004F5
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229D File Offset: 0x0000049D
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000229D File Offset: 0x0000049D
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002331 File Offset: 0x00000531
		public void UpdateAnimatorState()
		{
			this._animator.Enabled = this._blockableObject.IsUnblocked;
			this._animator.Speed = this._nonlinearAnimationManager.SpeedMultiplier;
		}

		// Token: 0x0400000B RID: 11
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400000C RID: 12
		public readonly EventBus _eventBus;

		// Token: 0x0400000D RID: 13
		public IAnimator _animator;

		// Token: 0x0400000E RID: 14
		public BlockableObject _blockableObject;
	}
}
