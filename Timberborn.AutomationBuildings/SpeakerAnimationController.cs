using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200003F RID: 63
	public class SpeakerAnimationController : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00007F93 File Offset: 0x00006193
		public SpeakerAnimationController(NonlinearAnimationManager nonlinearAnimationManager, EventBus eventBus)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._eventBus = eventBus;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00007FA9 File Offset: 0x000061A9
		public void Awake()
		{
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._speaker = base.GetComponent<Speaker>();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007FC4 File Offset: 0x000061C4
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
			this._speaker.PlaybackStateChanged += this.OnPlaybackStateChanged;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007FE9 File Offset: 0x000061E9
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
			this._speaker.PlaybackStateChanged -= this.OnPlaybackStateChanged;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000800E File Offset: 0x0000620E
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimator();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000800E File Offset: 0x0000620E
		public void OnPlaybackStateChanged(object sender, EventArgs e)
		{
			this.UpdateAnimator();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008016 File Offset: 0x00006216
		public void UpdateAnimator()
		{
			this._animator.Enabled = this._speaker.IsPlaying;
			this._animator.Speed = this._nonlinearAnimationManager.SpeedMultiplier;
		}

		// Token: 0x0400015B RID: 347
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400015C RID: 348
		public readonly EventBus _eventBus;

		// Token: 0x0400015D RID: 349
		public IAnimator _animator;

		// Token: 0x0400015E RID: 350
		public Speaker _speaker;
	}
}
