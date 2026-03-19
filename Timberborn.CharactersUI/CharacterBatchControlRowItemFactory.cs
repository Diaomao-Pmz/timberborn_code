using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.Characters;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.EntityNamingUI;
using Timberborn.EntityPanelSystem;
using Timberborn.SelectionSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CharactersUI
{
	// Token: 0x02000005 RID: 5
	public class CharacterBatchControlRowItemFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020FB File Offset: 0x000002FB
		public CharacterBatchControlRowItemFactory(VisualElementLoader visualElementLoader, EntityBadgeService entityBadgeService, EntityNameDialog entityNameDialog, EntitySelectionService entitySelectionService)
		{
			this._visualElementLoader = visualElementLoader;
			this._entityBadgeService = entityBadgeService;
			this._entityNameDialog = entityNameDialog;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			string elementName = "Game/BatchControl/CharacterBatchControlRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Character character = entity.GetComponent<Character>();
			Button button = UQueryExtensions.Q<Button>(visualElement, "EntityAvatar", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._entitySelectionService.SelectAndFollow(entity);
			}, 0);
			Sprite entityAvatar = this._entityBadgeService.GetEntityAvatar(character);
			button.style.backgroundImage = new StyleBackground(entityAvatar);
			UQueryExtensions.Q<Button>(visualElement, "EntityName", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnEntityNameClicked(character);
			}, 0);
			return new CharacterBatchControlRowItem(visualElement, UQueryExtensions.Q<Label>(visualElement, "EntityNameText", null), character);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021DA File Offset: 0x000003DA
		public void OnEntityNameClicked(Character character)
		{
			this._entityNameDialog.Show(character.GetComponent<NamedEntity>());
		}

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly EntityBadgeService _entityBadgeService;

		// Token: 0x0400000B RID: 11
		public readonly EntityNameDialog _entityNameDialog;

		// Token: 0x0400000C RID: 12
		public readonly EntitySelectionService _entitySelectionService;
	}
}
