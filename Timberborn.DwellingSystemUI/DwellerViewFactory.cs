using System;
using Timberborn.CharactersUI;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.DwellingSystemUI
{
	// Token: 0x02000005 RID: 5
	public class DwellerViewFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000221D File Offset: 0x0000041D
		public DwellerViewFactory(VisualElementLoader visualElementLoader, CharacterButtonFactory characterButtonFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._characterButtonFactory = characterButtonFactory;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002234 File Offset: 0x00000434
		public DwellerView Create()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DwellerView");
			CharacterButton characterButton = this._characterButtonFactory.Create(UQueryExtensions.Q<Button>(visualElement, "CharacterButton", null));
			Button button = UQueryExtensions.Q<Button>(visualElement, "DwellerView", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				characterButton.ClickAction();
			}, 0);
			return new DwellerView(UQueryExtensions.Q<VisualElement>(visualElement, "DwellerView", null), characterButton, button, UQueryExtensions.Q<Label>(visualElement, "Name", null), UQueryExtensions.Q<Label>(visualElement, "Subtitle", null), UQueryExtensions.Q<Label>(visualElement, "WellbeingScore", null));
		}

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly CharacterButtonFactory _characterButtonFactory;
	}
}
