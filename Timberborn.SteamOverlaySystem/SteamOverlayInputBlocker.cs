using System;
using JetBrains.Annotations;
using Steamworks;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.SteamStoreSystem;
using UnityEngine.UIElements;

namespace Timberborn.SteamOverlaySystem
{
	// Token: 0x02000004 RID: 4
	public class SteamOverlayInputBlocker : IPostLoadableSingleton, IUnloadableSingleton, IPanelController
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public SteamOverlayInputBlocker(SteamManager steamManager, IInputStateResetter inputStateResetter, PanelStack panelStack)
		{
			this._steamManager = steamManager;
			this._inputStateResetter = inputStateResetter;
			this._panelStack = panelStack;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public void PostLoad()
		{
			if (this._steamManager.Initialized)
			{
				this._steamOverlayCallback = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.SteamOverlayActivated));
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002101 File Offset: 0x00000301
		public void Unload()
		{
			Callback<GameOverlayActivated_t> steamOverlayCallback = this._steamOverlayCallback;
			if (steamOverlayCallback != null)
			{
				steamOverlayCallback.Dispose();
			}
			this._steamOverlayCallback = null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		public VisualElement GetPanel()
		{
			return new VisualElement();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002122 File Offset: 0x00000322
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002125 File Offset: 0x00000325
		public void OnUICancelled()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002128 File Offset: 0x00000328
		public void SteamOverlayActivated(GameOverlayActivated_t callback)
		{
			if (callback.m_nAppID == SteamAppId.AppId)
			{
				if (callback.m_bActive == 1)
				{
					this._panelStack.PushOverlay(this);
					return;
				}
				this._panelStack.Pop(this);
				this._inputStateResetter.ResetInputState();
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly SteamManager _steamManager;

		// Token: 0x04000007 RID: 7
		public readonly IInputStateResetter _inputStateResetter;

		// Token: 0x04000008 RID: 8
		public readonly PanelStack _panelStack;

		// Token: 0x04000009 RID: 9
		[UsedImplicitly]
		public Callback<GameOverlayActivated_t> _steamOverlayCallback;
	}
}
