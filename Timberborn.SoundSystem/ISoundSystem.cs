using System;
using UnityEngine;

namespace Timberborn.SoundSystem
{
	// Token: 0x02000010 RID: 16
	public interface ISoundSystem
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60
		Vector3 ListenerPosition { get; }

		// Token: 0x0600003D RID: 61
		void SetListenerPosition(Vector3 position, Quaternion rotation);

		// Token: 0x0600003E RID: 62
		float GetMixerVolume(string name);

		// Token: 0x0600003F RID: 63
		void SetMixerVolume(string name, float level);

		// Token: 0x06000040 RID: 64
		void SetMasterVolume(float level);

		// Token: 0x06000041 RID: 65
		void SetMusicVolume(float level);

		// Token: 0x06000042 RID: 66
		void SetUIVolume(float level);

		// Token: 0x06000043 RID: 67
		void SetEnvironmentVolume(float level);

		// Token: 0x06000044 RID: 68
		void PlaySound2D(GameObject emitter, string soundName, int priority);

		// Token: 0x06000045 RID: 69
		void PlaySound2D(GameObject emitter, string soundName, int priority, float delay, Action callback);

		// Token: 0x06000046 RID: 70
		void PlaySound3D(GameObject emitter, string soundName, int priority);

		// Token: 0x06000047 RID: 71
		void PlaySound3D(GameObject emitter, string soundName, int priority, Action callback);

		// Token: 0x06000048 RID: 72
		void AddLimitedAreaSound(Transform parent, string soundName, int priority, int cutoffDistance, string customMixer);

		// Token: 0x06000049 RID: 73
		void AddAreaEmitter(Transform parent, GameObject emitter);

		// Token: 0x0600004A RID: 74
		void RemoveAreaEmitter(Transform parent, GameObject emitter);

		// Token: 0x0600004B RID: 75
		void AddLargeAreaSound(Transform parent, IEmitterMap emitterMap, string soundName, int priority, int cutoffDistance, string customMixer);

		// Token: 0x0600004C RID: 76
		void LoopSingle3DSound(GameObject emitter, string soundName, int priority);

		// Token: 0x0600004D RID: 77
		void LoopSingle3DSound(GameObject emitter, string soundName, int priority, Vector3 soundOffset);

		// Token: 0x0600004E RID: 78
		void LoopSingle2DSound(GameObject emitter, string soundName, int priority);

		// Token: 0x0600004F RID: 79
		void StopSound(GameObject emitter, string soundName);

		// Token: 0x06000050 RID: 80
		void SetCustomMixer(GameObject emitter, string soundName, string mixerName);

		// Token: 0x06000051 RID: 81
		void InvalidateSounds(GameObject emitter);
	}
}
