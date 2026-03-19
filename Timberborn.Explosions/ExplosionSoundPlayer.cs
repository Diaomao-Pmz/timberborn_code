using System;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000011 RID: 17
	public class ExplosionSoundPlayer
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000358C File Offset: 0x0000178C
		public ExplosionSoundPlayer(ISoundSystem soundSystem)
		{
			this._soundSystem = soundSystem;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000359B File Offset: 0x0000179B
		public void Play(GameObject emitter)
		{
			this._soundSystem.PlaySound3D(emitter, "Environment.Buildings.DynamiteExplosion", 30);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000035B0 File Offset: 0x000017B0
		public void PlayGlobal(GameObject emitter)
		{
			this._soundSystem.PlaySound2D(emitter, "Environment.Buildings.DynamiteExplosion", 30);
		}

		// Token: 0x04000049 RID: 73
		public readonly ISoundSystem _soundSystem;
	}
}
