using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200001D RID: 29
	public class FireIntensityController : BaseComponent, IUpdatableComponent, IInitializableEntity
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003FC9 File Offset: 0x000021C9
		public void InitializeEntity()
		{
			this._flame = base.GetComponent<Fire>().SingleFlame;
			this._initialStartSizeMultiplier = this._flame.startSizeMultiplier;
			this._initialStartLifetimeMultiplier = this._flame.startLifetimeMultiplier;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FFE File Offset: 0x000021FE
		public void Update()
		{
			if (this._modificationEnabled && this._modificationEndTimestamp < Time.time)
			{
				this.SetIntensity(1f, 1f);
				this._modificationEnabled = false;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000402C File Offset: 0x0000222C
		public void Strengthen()
		{
			this.StartFlameModification(2.5f, 3.1f, 0.25f);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004043 File Offset: 0x00002243
		public void Dampen()
		{
			this.StartFlameModification(0f, 0f, 0.5f);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000405A File Offset: 0x0000225A
		public void StartFlameModification(float sizeMultiplier, float lifetimeMultiplier, float duration)
		{
			this.SetIntensity(sizeMultiplier, lifetimeMultiplier);
			this._modificationEndTimestamp = Time.time + duration;
			this._modificationEnabled = true;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004078 File Offset: 0x00002278
		public void SetIntensity(float sizeMultiplier, float lifetimeMultiplier)
		{
			this._flame.startSizeMultiplier = this._initialStartSizeMultiplier * sizeMultiplier;
			this._flame.startLifetimeMultiplier = this._initialStartLifetimeMultiplier * lifetimeMultiplier;
		}

		// Token: 0x04000050 RID: 80
		public float _initialStartSizeMultiplier;

		// Token: 0x04000051 RID: 81
		public float _initialStartLifetimeMultiplier;

		// Token: 0x04000052 RID: 82
		public float _modificationEndTimestamp;

		// Token: 0x04000053 RID: 83
		public bool _modificationEnabled;

		// Token: 0x04000054 RID: 84
		public ParticleSystem.MainModule _flame;
	}
}
