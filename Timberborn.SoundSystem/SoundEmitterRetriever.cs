using System;
using Bindito.Unity;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000016 RID: 22
	public class SoundEmitterRetriever
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003040 File Offset: 0x00001240
		public SoundEmitterRetriever(IInstantiator instantiator)
		{
			this._instantiator = instantiator;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003050 File Offset: 0x00001250
		public SoundEmitter GetSoundEmitter(GameObject emitter)
		{
			SoundEmitter result;
			if (emitter.TryGetComponent<SoundEmitter>(ref result))
			{
				return result;
			}
			this._instantiator.AddComponent<Sounds>(emitter);
			this._instantiator.AddComponent<LoopingSoundPlayer>(emitter);
			return this._instantiator.AddComponent<SoundEmitter>(emitter);
		}

		// Token: 0x04000039 RID: 57
		public readonly IInstantiator _instantiator;
	}
}
