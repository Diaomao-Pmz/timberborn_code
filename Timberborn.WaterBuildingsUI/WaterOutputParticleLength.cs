using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.TickSystem;
using Timberborn.WaterBuildings;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000027 RID: 39
	public class WaterOutputParticleLength : TickableComponent, IAwakableComponent, IInitializableEntity, IPostLoadableEntity, IFinishedStateListener
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000561E File Offset: 0x0000381E
		public void Awake()
		{
			this._waterOutput = base.GetComponent<WaterOutput>();
			base.DisableComponent();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005632 File Offset: 0x00003832
		public void InitializeEntity()
		{
			this._particlesMainModule = base.GetComponent<WaterOutputParticle>().ParticleSystem.main;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000564A File Offset: 0x0000384A
		public void PostLoadEntity()
		{
			this.UpdateLifetime();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000564A File Offset: 0x0000384A
		public override void Tick()
		{
			this.UpdateLifetime();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004ACD File Offset: 0x00002CCD
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000027A4 File Offset: 0x000009A4
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005654 File Offset: 0x00003854
		public void UpdateLifetime()
		{
			float availableSpace = this._waterOutput.AvailableSpace;
			this._particlesMainModule.startLifetime = availableSpace * WaterOutputParticleLength.LengthMultiplier + WaterOutputParticleLength.NozzleLength;
		}

		// Token: 0x040000E7 RID: 231
		public static readonly float LengthMultiplier = 0.5f;

		// Token: 0x040000E8 RID: 232
		public static readonly float NozzleLength = 0.1f;

		// Token: 0x040000E9 RID: 233
		public ParticleSystem.MainModule _particlesMainModule;

		// Token: 0x040000EA RID: 234
		public WaterOutput _waterOutput;
	}
}
