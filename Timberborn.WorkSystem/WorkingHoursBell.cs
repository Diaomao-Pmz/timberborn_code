using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using Timberborn.TimbermeshAnimations;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200001B RID: 27
	public class WorkingHoursBell : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000035EA File Offset: 0x000017EA
		public WorkingHoursBell(ISoundSystem soundSystem, EventBus eventBus)
		{
			this._soundSystem = soundSystem;
			this._eventBus = eventBus;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003600 File Offset: 0x00001800
		public void Awake()
		{
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._buildingSoundController = base.GetComponent<BuildingSoundController>();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000361B File Offset: 0x0000181B
		public void InitializeEntity()
		{
			this._animator.Enabled = true;
			this._animator.Stop();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003634 File Offset: 0x00001834
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003642 File Offset: 0x00001842
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003650 File Offset: 0x00001850
		[OnEvent]
		public void OnWorkingHoursTransitioned(WorkingHoursTransitionedEvent workingHoursTransitionedEvent)
		{
			this.PlayBellSound();
			this.AnimateBellToll();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000365E File Offset: 0x0000185E
		public void PlayBellSound()
		{
			if (this._buildingSoundController.PlaySound)
			{
				this._soundSystem.PlaySound3D(base.GameObject, "Environment.Buildings.BellToll", 30);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003685 File Offset: 0x00001885
		public void AnimateBellToll()
		{
			this._animator.Play("Default", false);
		}

		// Token: 0x0400003F RID: 63
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000040 RID: 64
		public readonly EventBus _eventBus;

		// Token: 0x04000041 RID: 65
		public IAnimator _animator;

		// Token: 0x04000042 RID: 66
		public BuildingSoundController _buildingSoundController;
	}
}
