using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000009 RID: 9
	public class ModularShaftAnimator : BaseComponent, IAwakableComponent, IInitializableEntity, IFinishedStateListener, IUnfinishedStateListener, IPreviewStateListener
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000235C File Offset: 0x0000055C
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002364 File Offset: 0x00000564
		public bool IsAnimated { get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x0000236D File Offset: 0x0000056D
		public ModularShaftAnimator(NonlinearAnimationManager nonlinearAnimationManager, ModularShaftAnimatorUpdater modularShaftAnimatorUpdater)
		{
			this._nonlinearAnimationManager = nonlinearAnimationManager;
			this._modularShaftAnimatorUpdater = modularShaftAnimatorUpdater;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000238E File Offset: 0x0000058E
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._modularShaftModelUpdater = base.GetComponent<ModularShaftModelUpdater>();
			this._modularShaftModelUpdater.ModelUpdated += delegate(object _, EventArgs _)
			{
				this.UpdateAll();
			};
			base.DisableComponent();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023C5 File Offset: 0x000005C5
		public void InitializeEntity()
		{
			this.UpdateAll();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023CD File Offset: 0x000005CD
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this._modularShaftAnimatorUpdater.Register(this);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023E1 File Offset: 0x000005E1
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this._modularShaftAnimatorUpdater.Unregister(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023F5 File Offset: 0x000005F5
		public void OnEnterUnfinishedState()
		{
			this.StopAnimators();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022DB File Offset: 0x000004DB
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023F5 File Offset: 0x000005F5
		public void OnEnterPreviewState()
		{
			this.StopAnimators();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002400 File Offset: 0x00000600
		public void UpdateAnimation()
		{
			this.IsAnimated = (this._mechanicalNode.ActiveAndPowered && this._mechanicalNode.PowerEfficiency > 0f);
			foreach (IAnimator animator in this._animators)
			{
				animator.Enabled = this.IsAnimated;
				if (this.IsAnimated)
				{
					animator.Speed = this._mechanicalNode.PowerEfficiency * this._nonlinearAnimationManager.SpeedMultiplier;
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024A8 File Offset: 0x000006A8
		public void StopAnimators()
		{
			foreach (IAnimator animator in this._animators)
			{
				animator.Enabled = false;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024FC File Offset: 0x000006FC
		public void UpdateAll()
		{
			this.StopAnimators();
			this.CollectActiveAnimators();
			this.UpdateAnimation();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002510 File Offset: 0x00000710
		public void CollectActiveAnimators()
		{
			this._animators.Clear();
			this._animators.AddRange(base.GameObject.GetComponentsInChildren<IAnimator>(true));
		}

		// Token: 0x04000012 RID: 18
		public readonly NonlinearAnimationManager _nonlinearAnimationManager;

		// Token: 0x04000013 RID: 19
		public readonly ModularShaftAnimatorUpdater _modularShaftAnimatorUpdater;

		// Token: 0x04000014 RID: 20
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000015 RID: 21
		public ModularShaftModelUpdater _modularShaftModelUpdater;

		// Token: 0x04000016 RID: 22
		public readonly List<IAnimator> _animators = new List<IAnimator>();

		// Token: 0x04000017 RID: 23
		public float _currentAnimationSpeed;
	}
}
