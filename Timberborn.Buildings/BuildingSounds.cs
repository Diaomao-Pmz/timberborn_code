using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreSound;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x02000018 RID: 24
	public class BuildingSounds : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003656 File Offset: 0x00001856
		public BuildingSounds(ISoundSystem soundSystem)
		{
			this._soundSystem = soundSystem;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003665 File Offset: 0x00001865
		public void Awake()
		{
			this._building = base.GetComponent<Building>();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003673 File Offset: 0x00001873
		public void ToggleSound(bool start)
		{
			if (this.HasLoopingSound && this._isStarted != start)
			{
				this._isStarted = start;
				if (this._isStarted)
				{
					this.StartSound(this._building);
					return;
				}
				this.StopSound(this._building);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000036AE File Offset: 0x000018AE
		public bool HasLoopingSound
		{
			get
			{
				return !string.IsNullOrEmpty(this._building.Spec.LoopingSoundName);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000036C8 File Offset: 0x000018C8
		public void StartSound(Building emitter)
		{
			string soundName = BuildingSounds.GetSoundName(emitter);
			this._soundSystem.LoopSingle3DSound(emitter.GameObject, BuildingSounds.GetSoundName(emitter), 128, BuildingSounds.GetSoundOffset(emitter));
			this._soundSystem.SetCustomMixer(emitter.GameObject, soundName, MixerNames.BuildingMixerNameKey);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003715 File Offset: 0x00001915
		public void StopSound(Building emitter)
		{
			this._soundSystem.StopSound(emitter.GameObject, BuildingSounds.GetSoundName(emitter));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000372E File Offset: 0x0000192E
		public static string GetSoundName(Building emitter)
		{
			return "Environment.Buildings.Loop." + emitter.Spec.LoopingSoundName;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003748 File Offset: 0x00001948
		public static Vector3 GetSoundOffset(Building emitter)
		{
			BlockObjectCenter component = emitter.GetComponent<BlockObjectCenter>();
			Vector3 vector = emitter.Transform.position - component.WorldCenter;
			return new Vector3(Mathf.Abs(vector.x), 0f, Mathf.Abs(vector.z));
		}

		// Token: 0x04000038 RID: 56
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000039 RID: 57
		public Building _building;

		// Token: 0x0400003A RID: 58
		public bool _isStarted;
	}
}
