using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.Particles
{
	// Token: 0x0200000A RID: 10
	public class FinishedStateParticlesSpeedMultiplier : BaseComponent, IAwakableComponent, IFinishedStateListener, IParticlesSpeedMultiplier
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026E2 File Offset: 0x000008E2
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026EA File Offset: 0x000008EA
		public float SpeedMultiplier { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026F3 File Offset: 0x000008F3
		public bool IsValid
		{
			get
			{
				return this._blockObject != null;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026FE File Offset: 0x000008FE
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000270C File Offset: 0x0000090C
		public void AddParticlesRunner(ParticlesRunner particlesRunner)
		{
			this._particlesRunner.Add(particlesRunner);
			particlesRunner.AddParticleSpeedMultiplier(this);
			if (this._blockObject.IsFinished)
			{
				this.Enable();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002734 File Offset: 0x00000934
		public void OnEnterFinishedState()
		{
			this.Enable();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000273C File Offset: 0x0000093C
		public void OnExitFinishedState()
		{
			this.UpdateSpeedMultiplier(0);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002745 File Offset: 0x00000945
		public void Enable()
		{
			this.UpdateSpeedMultiplier(1);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002750 File Offset: 0x00000950
		public void UpdateSpeedMultiplier(int speedMultiplier)
		{
			foreach (ParticlesRunner particlesRunner in this._particlesRunner)
			{
				this.SpeedMultiplier = (float)speedMultiplier;
				particlesRunner.UpdateSimulationSpeed();
			}
		}

		// Token: 0x04000013 RID: 19
		public BlockObject _blockObject;

		// Token: 0x04000014 RID: 20
		public readonly List<ParticlesRunner> _particlesRunner = new List<ParticlesRunner>();
	}
}
