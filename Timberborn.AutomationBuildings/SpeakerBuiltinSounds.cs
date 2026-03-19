using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000040 RID: 64
	public class SpeakerBuiltinSounds : ILoadableSingleton
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00008044 File Offset: 0x00006244
		public SpeakerBuiltinSounds(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00008069 File Offset: 0x00006269
		public ReadOnlyList<string> SoundIds
		{
			get
			{
				return this._soundIds.AsReadOnlyList<string>();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008078 File Offset: 0x00006278
		public void Load()
		{
			foreach (SpeakerSoundSpec speakerSoundSpec in this._specService.GetSpecs<SpeakerSoundSpec>())
			{
				this._soundIds.Add(speakerSoundSpec.SoundId);
				this._soundNames[speakerSoundSpec.SoundId] = speakerSoundSpec.DisplayName.Value;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000080F0 File Offset: 0x000062F0
		public string GetSoundDisplayName(string soundId)
		{
			return this._soundNames[soundId];
		}

		// Token: 0x0400015F RID: 351
		public readonly ISpecService _specService;

		// Token: 0x04000160 RID: 352
		public readonly List<string> _soundIds = new List<string>();

		// Token: 0x04000161 RID: 353
		public readonly Dictionary<string, string> _soundNames = new Dictionary<string, string>();
	}
}
