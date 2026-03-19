using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.AutomationBuildings;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200002C RID: 44
	public class SpeakerSoundDropdownProvider : ILoadableSingleton, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00006389 File Offset: 0x00004589
		public SpeakerSoundDropdownProvider(SpeakerSoundService speakerSoundService, IAssetLoader assetLoader)
		{
			this._speakerSoundService = speakerSoundService;
			this._assetLoader = assetLoader;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000063AA File Offset: 0x000045AA
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._sounds.AsReadOnlyList<string>();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000063BC File Offset: 0x000045BC
		public void Load()
		{
			this._buildInSoundIcon = this._assetLoader.Load<Sprite>("UI/Images/Game/sound-builtin");
			this._customSoundIcon = this._assetLoader.Load<Sprite>("UI/Images/Game/sound-custom");
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000063EC File Offset: 0x000045EC
		public void UpdateSounds()
		{
			this._sounds.Clear();
			this._sounds.AddRange(this._speakerSoundService.BuiltInSounds);
			this._sounds.AddRange(from sound in this._speakerSoundService.CustomSounds
			orderby sound
			select sound);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000645E File Offset: 0x0000465E
		public void SetSpeaker(Speaker speaker)
		{
			this._speaker = speaker;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006467 File Offset: 0x00004667
		public void ClearSpeaker()
		{
			this._speaker = null;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006470 File Offset: 0x00004670
		public string GetValue()
		{
			return this._speaker.SoundId;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000647D File Offset: 0x0000467D
		public void SetValue(string value)
		{
			this._speaker.SetSoundId(value);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000648C File Offset: 0x0000468C
		public string FormatDisplayText(string value, bool selected)
		{
			if (!this._speakerSoundService.BuiltInSounds.Contains(value))
			{
				return value;
			}
			return this._speakerSoundService.GetSoundDisplayName(value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000064C0 File Offset: 0x000046C0
		public Sprite GetIcon(string value)
		{
			if (!this._speakerSoundService.BuiltInSounds.Contains(value))
			{
				return this._customSoundIcon;
			}
			return this._buildInSoundIcon;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005ED8 File Offset: 0x000040D8
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x04000144 RID: 324
		public readonly SpeakerSoundService _speakerSoundService;

		// Token: 0x04000145 RID: 325
		public readonly IAssetLoader _assetLoader;

		// Token: 0x04000146 RID: 326
		public readonly List<string> _sounds = new List<string>();

		// Token: 0x04000147 RID: 327
		public Speaker _speaker;

		// Token: 0x04000148 RID: 328
		public int _selectedIndex;

		// Token: 0x04000149 RID: 329
		public Sprite _buildInSoundIcon;

		// Token: 0x0400014A RID: 330
		public Sprite _customSoundIcon;
	}
}
