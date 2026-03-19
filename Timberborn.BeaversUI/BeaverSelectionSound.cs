using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.BehaviorSystem;
using Timberborn.GameFactionSystem;
using Timberborn.MortalSystem;
using Timberborn.NeedBehaviorSystem;
using Timberborn.SelectionSystem;
using Timberborn.SleepSystem;
using Timberborn.SoundSystem;
using Timberborn.StatusSystem;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000012 RID: 18
	public class BeaverSelectionSound : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002F9F File Offset: 0x0000119F
		public BeaverSelectionSound(ISoundSystem soundSystem, FactionService factionService)
		{
			this._soundSystem = soundSystem;
			this._factionService = factionService;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FB5 File Offset: 0x000011B5
		public void Awake()
		{
			this._child = base.GetComponent<Child>();
			this._mortal = base.GetComponent<Mortal>();
			this._behaviorManager = base.GetComponent<BehaviorManager>();
			this._statusSubject = base.GetComponent<StatusSubject>();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FE7 File Offset: 0x000011E7
		public void OnSelect()
		{
			this.PlaySound();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D63 File Offset: 0x00000F63
		public void OnUnselect()
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FF0 File Offset: 0x000011F0
		public void PlaySound()
		{
			if (!this._mortal.Dead)
			{
				string soundId = this._factionService.Current.SoundId;
				string text = this._child ? BeaverSelectionSound.ChildKey : BeaverSelectionSound.AdultKey;
				string stateKey = this.GetStateKey();
				string soundName = string.Concat(new string[]
				{
					"UI.Beavers.",
					soundId,
					".Selected.",
					text,
					stateKey
				});
				this._soundSystem.PlaySound2D(base.GameObject, soundName, 10);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003078 File Offset: 0x00001278
		public string GetStateKey()
		{
			if (this._behaviorManager.IsRunningBehavior<SleepNeedBehavior>())
			{
				if (!this._behaviorManager.IsRunningExecutor<ApplyEffectExecutor>())
				{
					return BeaverSelectionSound.SleepyKey;
				}
				return BeaverSelectionSound.SleepingKey;
			}
			else
			{
				if (this._statusSubject.ActiveStatuses.Count > 0)
				{
					return BeaverSelectionSound.DiscontentKey;
				}
				return BeaverSelectionSound.ContentKey;
			}
		}

		// Token: 0x04000056 RID: 86
		public static readonly string ChildKey = "Child_";

		// Token: 0x04000057 RID: 87
		public static readonly string AdultKey = "Adult_";

		// Token: 0x04000058 RID: 88
		public static readonly string SleepingKey = "Sleeping";

		// Token: 0x04000059 RID: 89
		public static readonly string SleepyKey = "Sleepy";

		// Token: 0x0400005A RID: 90
		public static readonly string ContentKey = "Content";

		// Token: 0x0400005B RID: 91
		public static readonly string DiscontentKey = "Discontent";

		// Token: 0x0400005C RID: 92
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400005D RID: 93
		public readonly FactionService _factionService;

		// Token: 0x0400005E RID: 94
		public Child _child;

		// Token: 0x0400005F RID: 95
		public Mortal _mortal;

		// Token: 0x04000060 RID: 96
		public BehaviorManager _behaviorManager;

		// Token: 0x04000061 RID: 97
		public StatusSubject _statusSubject;
	}
}
