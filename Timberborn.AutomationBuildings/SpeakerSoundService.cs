using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.Common;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000045 RID: 69
	public class SpeakerSoundService : ILoadableSingleton
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00008453 File Offset: 0x00006653
		public SpeakerSoundService(SpeakerBuiltinSounds speakerBuiltinSounds, SpeakerCustomSoundLoader speakerCustomSoundLoader, EventBus eventBus)
		{
			this._speakerBuiltinSounds = speakerBuiltinSounds;
			this._speakerCustomSoundLoader = speakerCustomSoundLoader;
			this._eventBus = eventBus;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000847B File Offset: 0x0000667B
		public ReadOnlyList<string> BuiltInSounds
		{
			get
			{
				return this._speakerBuiltinSounds.SoundIds;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00008488 File Offset: 0x00006688
		public ReadOnlyList<string> CustomSounds
		{
			get
			{
				return this._customSoundIds.AsReadOnlyList<string>();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008495 File Offset: 0x00006695
		public void Load()
		{
			this.LoadCustomSounds();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000084A0 File Offset: 0x000066A0
		public string GetValidatedSoundId(string soundName)
		{
			if (this.CustomSounds.Contains(soundName) || this.BuiltInSounds.Contains(soundName))
			{
				return soundName;
			}
			return this.BuiltInSounds.First<string>();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000084E1 File Offset: 0x000066E1
		public void ReloadCustomSounds()
		{
			this._customSoundIds.Clear();
			this.LoadCustomSounds();
			this._eventBus.Post(new SpeakerSoundsReloadedEvent());
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008504 File Offset: 0x00006704
		public string GetCustomSoundDirectory()
		{
			return Path.Combine(UserDataFolder.Folder, SpeakerSoundService.FolderName);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008515 File Offset: 0x00006715
		public string GetSoundDisplayName(string soundId)
		{
			return this._speakerBuiltinSounds.GetSoundDisplayName(soundId);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008524 File Offset: 0x00006724
		public void LoadCustomSounds()
		{
			string customSoundDirectory = this.GetCustomSoundDirectory();
			foreach (AudioClip audioClip in this._speakerCustomSoundLoader.LoadCustomSounds(customSoundDirectory))
			{
				this._customSoundIds.Add(audioClip.name);
			}
		}

		// Token: 0x0400016D RID: 365
		public static readonly string FolderName = "Sounds";

		// Token: 0x0400016E RID: 366
		public readonly SpeakerBuiltinSounds _speakerBuiltinSounds;

		// Token: 0x0400016F RID: 367
		public readonly SpeakerCustomSoundLoader _speakerCustomSoundLoader;

		// Token: 0x04000170 RID: 368
		public readonly EventBus _eventBus;

		// Token: 0x04000171 RID: 369
		public readonly List<string> _customSoundIds = new List<string>();
	}
}
