using System;
using JetBrains.Annotations;
using Steamworks;
using Timberborn.SingletonSystem;
using Timberborn.SteamStoreSystem;

namespace Timberborn.SteamWorkshop
{
	// Token: 0x02000005 RID: 5
	public class ItemInstalledNotifier : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public ItemInstalledNotifier(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		public void Load()
		{
			this._installationCallback = Callback<ItemInstalled_t>.Create(new Callback<ItemInstalled_t>.DispatchDelegate(this.OnItemInstalled));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Unload()
		{
			Callback<ItemInstalled_t> installationCallback = this._installationCallback;
			if (installationCallback != null)
			{
				installationCallback.Dispose();
			}
			this._installationCallback = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002102 File Offset: 0x00000302
		public void OnItemInstalled(ItemInstalled_t itemInstalled)
		{
			if (itemInstalled.m_unAppID == SteamAppId.AppId)
			{
				this._eventBus.Post(new ItemInstalledEvent());
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly EventBus _eventBus;

		// Token: 0x04000007 RID: 7
		[UsedImplicitly]
		public Callback<ItemInstalled_t> _installationCallback;
	}
}
