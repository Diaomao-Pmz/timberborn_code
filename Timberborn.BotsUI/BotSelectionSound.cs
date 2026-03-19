using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MortalSystem;
using Timberborn.SelectionSystem;
using Timberborn.SoundSystem;
using Timberborn.StatusSystem;

namespace Timberborn.BotsUI
{
	// Token: 0x0200000C RID: 12
	public class BotSelectionSound : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000246C File Offset: 0x0000066C
		public BotSelectionSound(ISoundSystem soundSystem)
		{
			this._soundSystem = soundSystem;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000247B File Offset: 0x0000067B
		public void Awake()
		{
			this._mortal = base.GetComponent<Mortal>();
			this._statusSubject = base.GetComponent<StatusSubject>();
			this._botSelectionSoundSpec = base.GetComponent<BotSelectionSoundSpec>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A1 File Offset: 0x000006A1
		public void OnSelect()
		{
			this.PlaySound();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002284 File Offset: 0x00000484
		public void OnUnselect()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024AC File Offset: 0x000006AC
		public void PlaySound()
		{
			if (!this._mortal.Dead)
			{
				string soundName = "UI.Bots.Selected." + this._botSelectionSoundSpec.SoundNameKey + "_" + this.GetKey();
				this._soundSystem.PlaySound2D(base.GameObject, soundName, 10);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024FC File Offset: 0x000006FC
		public string GetKey()
		{
			if (this._statusSubject.ActiveStatuses.Count <= 0)
			{
				return BotSelectionSound.ContentKey;
			}
			return BotSelectionSound.DiscontentKey;
		}

		// Token: 0x0400001F RID: 31
		public static readonly string ContentKey = "Content";

		// Token: 0x04000020 RID: 32
		public static readonly string DiscontentKey = "Discontent";

		// Token: 0x04000021 RID: 33
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000022 RID: 34
		public Mortal _mortal;

		// Token: 0x04000023 RID: 35
		public StatusSubject _statusSubject;

		// Token: 0x04000024 RID: 36
		public BotSelectionSoundSpec _botSelectionSoundSpec;
	}
}
