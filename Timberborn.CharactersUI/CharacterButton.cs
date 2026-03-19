using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.GameFactionSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CharactersUI
{
	// Token: 0x02000007 RID: 7
	public class CharacterButton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002218 File Offset: 0x00000418
		public VisualElement Root { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002220 File Offset: 0x00000420
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002228 File Offset: 0x00000428
		public Action ClickAction { get; private set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002231 File Offset: 0x00000431
		public CharacterButton(VisualElement root, FactionService factionService, EntityBadgeService entityBadgeService)
		{
			this.Root = root;
			this._factionService = factionService;
			this._entityBadgeService = entityBadgeService;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000224E File Offset: 0x0000044E
		public void Initialize()
		{
			this._button = UQueryExtensions.Q<Button>(this.Root, "CharacterButton", null);
			this._button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.InvokeClickAction), 0);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002280 File Offset: 0x00000480
		public void ShowFilled(BaseComponent user, Action onClick)
		{
			Sprite entityAvatar = this._entityBadgeService.GetEntityAvatar(user);
			this.ShowFilled(entityAvatar, onClick);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A2 File Offset: 0x000004A2
		public void ShowAdultEmpty()
		{
			this.SetBackground(this._factionService.Current.Avatar.Asset);
			this.ChangeOnClickAction(null);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C6 File Offset: 0x000004C6
		public void ShowChildEmpty()
		{
			this.SetBackground(this._factionService.Current.ChildAvatar.Asset);
			this.ChangeOnClickAction(null);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022EA File Offset: 0x000004EA
		public void ShowBotEmpty()
		{
			this.SetBackground(this._factionService.Current.BotAvatar.Asset);
			this.ChangeOnClickAction(null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230E File Offset: 0x0000050E
		public void ShowEmpty()
		{
			this._button.style.backgroundImage = null;
			this.ChangeOnClickAction(null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000232D File Offset: 0x0000052D
		public void ShowFilled(Sprite avatar, Action onClick)
		{
			this._button.style.backgroundImage = null;
			this.SetBackground(avatar);
			this.ChangeOnClickAction(onClick);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002353 File Offset: 0x00000553
		public void SetBackground(Sprite sprite)
		{
			this._button.style.backgroundImage = new StyleBackground(sprite);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000236B File Offset: 0x0000056B
		public void ChangeOnClickAction(Action onClick = null)
		{
			this._button.SetEnabled(onClick != null);
			this.ClickAction = onClick;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002383 File Offset: 0x00000583
		public void InvokeClickAction(ClickEvent evt)
		{
			Action clickAction = this.ClickAction;
			if (clickAction == null)
			{
				return;
			}
			clickAction();
		}

		// Token: 0x04000012 RID: 18
		public readonly FactionService _factionService;

		// Token: 0x04000013 RID: 19
		public readonly EntityBadgeService _entityBadgeService;

		// Token: 0x04000014 RID: 20
		public Button _button;
	}
}
