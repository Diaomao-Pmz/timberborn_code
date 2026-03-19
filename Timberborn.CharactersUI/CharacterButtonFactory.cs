using System;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GameFactionSystem;
using UnityEngine.UIElements;

namespace Timberborn.CharactersUI
{
	// Token: 0x02000008 RID: 8
	public class CharacterButtonFactory
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002395 File Offset: 0x00000595
		public CharacterButtonFactory(VisualElementLoader visualElementLoader, FactionService factionService, EntityBadgeService entityBadgeService)
		{
			this._visualElementLoader = visualElementLoader;
			this._factionService = factionService;
			this._entityBadgeService = entityBadgeService;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B4 File Offset: 0x000005B4
		public CharacterButton Create()
		{
			VisualElement root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/CharacterButton");
			return this.Create(root);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023D9 File Offset: 0x000005D9
		public CharacterButton Create(VisualElement root)
		{
			CharacterButton characterButton = new CharacterButton(root, this._factionService, this._entityBadgeService);
			characterButton.Initialize();
			return characterButton;
		}

		// Token: 0x04000015 RID: 21
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000016 RID: 22
		public readonly FactionService _factionService;

		// Token: 0x04000017 RID: 23
		public readonly EntityBadgeService _entityBadgeService;
	}
}
