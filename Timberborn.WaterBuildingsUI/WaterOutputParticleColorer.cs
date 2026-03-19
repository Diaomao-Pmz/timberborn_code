using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterBuildings;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000023 RID: 35
	public class WaterOutputParticleColorer : BaseComponent, IInitializableEntity
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00005396 File Offset: 0x00003596
		public WaterOutputParticleColorer(WaterOutputParticleColors waterOutputParticleColors)
		{
			this._waterOutputParticleColors = waterOutputParticleColors;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000053A5 File Offset: 0x000035A5
		public void InitializeEntity()
		{
			this._particlesMainModule = base.GetComponent<WaterOutputParticle>().ParticleSystem.main;
			base.GetComponent<WaterOutput>().WaterAdded += this.OnWaterAdded;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000053D4 File Offset: 0x000035D4
		public void OnWaterAdded(object sender, WaterAddition e)
		{
			ParticleSystem.MinMaxGradient startColor = this._particlesMainModule.startColor;
			float num = e.ContaminatedWater / (e.CleanWater + e.ContaminatedWater);
			startColor.color = this._waterOutputParticleColors.WaterContaminationParticleGradient.Evaluate(num);
			this._particlesMainModule.startColor = startColor;
		}

		// Token: 0x040000E0 RID: 224
		public readonly WaterOutputParticleColors _waterOutputParticleColors;

		// Token: 0x040000E1 RID: 225
		public ParticleSystem.MainModule _particlesMainModule;
	}
}
