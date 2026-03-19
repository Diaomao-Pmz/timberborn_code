using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.Particles
{
	// Token: 0x02000015 RID: 21
	public class ParticlesRunnerCreator : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000319B File Offset: 0x0000139B
		public void Awake()
		{
			this._finishedStateParticlesSpeedMultiplier = base.GetComponent<FinishedStateParticlesSpeedMultiplier>();
			this._nonLinearParticlesSpeedMultiplier = base.GetComponent<NonLinearParticlesSpeedMultiplier>();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031B8 File Offset: 0x000013B8
		public ParticlesRunner Create(IEnumerable<ParticleSystem> particleSystems)
		{
			ParticlesRunner particlesRunner = ParticlesRunner.Create(this, particleSystems);
			this._nonLinearParticlesSpeedMultiplier.AddParticlesRunner(particlesRunner);
			if (this._finishedStateParticlesSpeedMultiplier.IsValid)
			{
				this._finishedStateParticlesSpeedMultiplier.AddParticlesRunner(particlesRunner);
			}
			particlesRunner.UpdateSimulationSpeed();
			return particlesRunner;
		}

		// Token: 0x0400002A RID: 42
		public FinishedStateParticlesSpeedMultiplier _finishedStateParticlesSpeedMultiplier;

		// Token: 0x0400002B RID: 43
		public NonLinearParticlesSpeedMultiplier _nonLinearParticlesSpeedMultiplier;
	}
}
