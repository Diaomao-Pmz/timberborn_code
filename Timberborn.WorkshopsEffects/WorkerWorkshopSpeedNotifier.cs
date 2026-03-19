using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EnterableSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x02000011 RID: 17
	public class WorkerWorkshopSpeedNotifier : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002F91 File Offset: 0x00001191
		public WorkerWorkshopSpeedNotifier(EventBus eventBus, NonlinearAnimationManager nonlinearAnimationManager)
		{
			this._eventBus = eventBus;
			this._nonlinearAnimationManager = nonlinearAnimationManager;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002FB4 File Offset: 0x000011B4
		public void Awake()
		{
			this._workshopAnimationSpeedModifier = base.GetComponent<IWorkshopAnimationSpeedModifier>();
			this._workshopAnimationSpeedModifier.SpeedModifierChanged += delegate(object _, EventArgs _)
			{
				this.UpdateWorkersSpeed();
			};
			Enterable component = base.GetComponent<Enterable>();
			component.EntererAdded += this.OnEntererAdded;
			component.EntererRemoved += this.OnEntererRemoved;
			this._eventBus.Register(this);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003019 File Offset: 0x00001219
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateWorkersSpeed();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003024 File Offset: 0x00001224
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			CharacterAnimator component = e.Enterer.GetComponent<CharacterAnimator>();
			this._workerCharacterAnimators.Add(component);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000304C File Offset: 0x0000124C
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			CharacterAnimator component = e.Enterer.GetComponent<CharacterAnimator>();
			this._workerCharacterAnimators.Remove(component);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003074 File Offset: 0x00001274
		public void UpdateWorkersSpeed()
		{
			float value = this._workshopAnimationSpeedModifier.SpeedModifier * this._nonlinearAnimationManager.SpeedMultiplier;
			foreach (CharacterAnimator characterAnimator in this._workerCharacterAnimators)
			{
				characterAnimator.SetFloat(WorkerWorkshopSpeedNotifier.WorkshopSpeedKey, value);
			}
		}

		// Token: 0x0400002B RID: 43
		public static readonly string WorkshopSpeedKey = "WorkshopSpeed";

		// Token: 0x0400002C RID: 44
		public readonly EventBus _eventBus;

		// Token: 0x0400002D RID: 45
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x0400002E RID: 46
		public IWorkshopAnimationSpeedModifier _workshopAnimationSpeedModifier;

		// Token: 0x0400002F RID: 47
		public readonly List<CharacterAnimator> _workerCharacterAnimators = new List<CharacterAnimator>();
	}
}
