using System;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.UISound
{
	// Token: 0x02000005 RID: 5
	public class UISoundController : ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public UISoundController(ISoundSystem soundSystem, RootObjectProvider rootObjectProvider)
		{
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E7 File Offset: 0x000002E7
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("UISoundController");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FF File Offset: 0x000002FF
		public void PlaySound(string sound)
		{
			this._soundSystem.PlaySound2D(this._parent, sound, 10);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002115 File Offset: 0x00000315
		public void PlayClickSound()
		{
			this.PlaySound(UISoundController.Click);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002122 File Offset: 0x00000322
		public void PlayCancelSound()
		{
			this.PlaySound(UISoundController.Cancel);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212F File Offset: 0x0000032F
		public void PlayCantDoSound()
		{
			this.PlaySound(UISoundController.CantDo);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string Click = "UI.Click";

		// Token: 0x04000007 RID: 7
		public static readonly string Cancel = "UI.Cancel";

		// Token: 0x04000008 RID: 8
		public static readonly string CantDo = "UI.CantDo";

		// Token: 0x04000009 RID: 9
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400000A RID: 10
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400000B RID: 11
		public GameObject _parent;
	}
}
