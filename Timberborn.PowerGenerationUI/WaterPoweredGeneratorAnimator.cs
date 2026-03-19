using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.PowerGeneration;
using Timberborn.SingletonSystem;
using Timberborn.TimbermeshAnimations;
using Timberborn.TimeSystem;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x0200000E RID: 14
	public class WaterPoweredGeneratorAnimator : BaseComponent, IFinishedStateListener, IAwakableComponent
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000026E1 File Offset: 0x000008E1
		public WaterPoweredGeneratorAnimator(WaterPoweredGeneratorSpeedCalculator waterPoweredGeneratorSpeedCalculator, EventBus eventBus)
		{
			this._waterPoweredGeneratorSpeedCalculator = waterPoweredGeneratorSpeedCalculator;
			this._eventBus = eventBus;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026F7 File Offset: 0x000008F7
		public void Awake()
		{
			this._waterPoweredGenerator = base.GetComponent<WaterPoweredGenerator>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._animator = base.GetComponentInChildren<IAnimator>(true);
			this._waterPoweredGenerator.RotationUpdated += delegate(object _, EventArgs _)
			{
				this.UpdateAnimation();
			};
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002735 File Offset: 0x00000935
		public void OnEnterFinishedState()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002743 File Offset: 0x00000943
		public void OnExitFinishedState()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002751 File Offset: 0x00000951
		[OnEvent]
		public void OnCurrentSpeedChanged(CurrentSpeedChangedEvent currentSpeedChangedEvent)
		{
			this.UpdateAnimation();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000275C File Offset: 0x0000095C
		public void UpdateAnimation()
		{
			if (this._mechanicalNode.Active)
			{
				float generatorRotation = this._waterPoweredGenerator.GeneratorRotation;
				this._animator.Enabled = true;
				this._animator.Speed = this._waterPoweredGeneratorSpeedCalculator.CalculateSpeed(generatorRotation);
				return;
			}
			this._animator.Enabled = false;
			this._animator.Speed = 0f;
		}

		// Token: 0x04000015 RID: 21
		public readonly WaterPoweredGeneratorSpeedCalculator _waterPoweredGeneratorSpeedCalculator;

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public WaterPoweredGenerator _waterPoweredGenerator;

		// Token: 0x04000018 RID: 24
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000019 RID: 25
		public IAnimator _animator;
	}
}
