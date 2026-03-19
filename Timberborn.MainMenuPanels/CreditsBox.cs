using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuPanels
{
	// Token: 0x02000004 RID: 4
	public class CreditsBox : IPanelController, ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public CreditsBox(VisualElementLoader visualElementLoader, PanelStack panelStack, MainMenuSoundController mainMenuSoundController, InputService inputService)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._mainMenuSoundController = mainMenuSoundController;
			this._inputService = inputService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Options/CreditsBox");
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			this._creditsContent = UQueryExtensions.Q(this._root, "CreditsContent", null);
			this._scrollViewWrapper = UQueryExtensions.Q(this._root, "ScrollViewWrapper", null);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		public VisualElement GetPanel()
		{
			this._creditsContent.ToggleDisplayStyle(false);
			this._initializedOffset = false;
			this._isVisible = true;
			this._mainMenuSoundController.PlayCreditsMusic();
			return this._root;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002189 File Offset: 0x00000389
		public bool OnUIConfirmed()
		{
			this.OnUICancelled();
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002192 File Offset: 0x00000392
		public void OnUICancelled()
		{
			this._creditsContent.style.translate = Vector3.zero;
			this._isVisible = false;
			this._panelStack.Pop(this);
			this._mainMenuSoundController.PlayThemeMusic();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021CC File Offset: 0x000003CC
		public void UpdateSingleton()
		{
			if (this._isVisible)
			{
				if (!this._initializedOffset)
				{
					this.InitializeOffset();
					return;
				}
				this.ScrollCredits();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021EB File Offset: 0x000003EB
		public float ViewHeight
		{
			get
			{
				return this._scrollViewWrapper.resolvedStyle.height;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021FD File Offset: 0x000003FD
		public void InitializeOffset()
		{
			if (!float.IsNaN(this.ViewHeight))
			{
				this.ScrollCredits(-this.ViewHeight);
				this._creditsContent.ToggleDisplayStyle(true);
				this._initializedOffset = true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000222C File Offset: 0x0000042C
		public void ScrollCredits()
		{
			float scrollSpeed = this.GetScrollSpeed();
			float y = this._creditsContent.resolvedStyle.translate.y;
			bool flag = y >= -this._creditsContent.resolvedStyle.height + this.ViewHeight / 2f;
			bool flag2 = y < this.ViewHeight;
			if ((scrollSpeed > 0f && flag) || (scrollSpeed < 0f && flag2))
			{
				this.ScrollCredits(Time.deltaTime * scrollSpeed);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022A8 File Offset: 0x000004A8
		public void ScrollCredits(float delta)
		{
			this._creditsContent.style.translate = this._creditsContent.resolvedStyle.translate - new Vector3(0f, delta, 0f);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022E4 File Offset: 0x000004E4
		public float GetScrollSpeed()
		{
			if (this._inputService.IsKeyHeld(CreditsBox.ForwardCreditsKey))
			{
				return CreditsBox.FastSpeed;
			}
			if (this._inputService.IsKeyHeld(CreditsBox.RewindCreditsKey))
			{
				return -CreditsBox.FastSpeed;
			}
			return CreditsBox.BaseSpeed;
		}

		// Token: 0x04000006 RID: 6
		public static readonly float BaseSpeed = 50f;

		// Token: 0x04000007 RID: 7
		public static readonly float FastSpeed = 500f;

		// Token: 0x04000008 RID: 8
		public static readonly string ForwardCreditsKey = "ForwardCredits";

		// Token: 0x04000009 RID: 9
		public static readonly string RewindCreditsKey = "RewindCredits";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly PanelStack _panelStack;

		// Token: 0x0400000C RID: 12
		public readonly MainMenuSoundController _mainMenuSoundController;

		// Token: 0x0400000D RID: 13
		public readonly InputService _inputService;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;

		// Token: 0x0400000F RID: 15
		public VisualElement _creditsContent;

		// Token: 0x04000010 RID: 16
		public VisualElement _scrollViewWrapper;

		// Token: 0x04000011 RID: 17
		public bool _initializedOffset;

		// Token: 0x04000012 RID: 18
		public bool _isVisible;
	}
}
