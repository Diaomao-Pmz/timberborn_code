using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;
using Timberborn.Workshops;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000012 RID: 18
	public class WorkshopAnimationController : BaseComponent, IAwakableComponent, IPostLoadableEntity, IFinishedStateListener
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000030F0 File Offset: 0x000012F0
		public WorkshopAnimationController(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003108 File Offset: 0x00001308
		public void Awake()
		{
			this._workshop = base.GetComponent<Workshop>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._workshopAnimationSpeedModifier = base.GetComponent<IWorkshopAnimationSpeedModifier>();
			if (this._workshopAnimationSpeedModifier != null)
			{
				this._workshopAnimationSpeedModifier.SpeedModifierChanged += this.OnSpeedModifierChanged;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003159 File Offset: 0x00001359
		public void PostLoadEntity()
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003159 File Offset: 0x00001359
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003161 File Offset: 0x00001361
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._workshop.WorkshopStateChanged += this.OnWorkshopStateChanged;
			this.UpdateAnimatorState();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000318C File Offset: 0x0000138C
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._workshop.WorkshopStateChanged -= this.OnWorkshopStateChanged;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003159 File Offset: 0x00001359
		public void OnSpeedModifierChanged(object sender, EventArgs e)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003159 File Offset: 0x00001359
		public void OnWorkshopStateChanged(object sender, WorkshopStateChangedEventArgs e)
		{
			this.UpdateAnimatorState();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000031B4 File Offset: 0x000013B4
		public void UpdateAnimatorState()
		{
			this._animator.Enabled = this._workshop.CurrentlyWorking;
			IAnimator animator = this._animator;
			float speedMultiplier = this._nonlinearAnimationManager.SpeedMultiplier;
			IWorkshopAnimationSpeedModifier workshopAnimationSpeedModifier = this._workshopAnimationSpeedModifier;
			animator.Speed = speedMultiplier * ((workshopAnimationSpeedModifier != null) ? workshopAnimationSpeedModifier.SpeedModifier : 1f);
		}

		// Token: 0x04000030 RID: 48
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000031 RID: 49
		public readonly EventBus _eventBus;

		// Token: 0x04000032 RID: 50
		public Workshop _workshop;

		// Token: 0x04000033 RID: 51
		public IAnimator _animator;

		// Token: 0x04000034 RID: 52
		public IWorkshopAnimationSpeedModifier _workshopAnimationSpeedModifier;
	}
}
