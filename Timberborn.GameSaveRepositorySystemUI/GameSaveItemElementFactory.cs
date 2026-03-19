using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000005 RID: 5
	public class GameSaveItemElementFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002115 File Offset: 0x00000315
		public GameSaveItemElementFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public VisualElement Create()
		{
			return this._visualElementLoader.LoadVisualElement("Options/GameSaveItemElement");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002138 File Offset: 0x00000338
		public void Bind(VisualElement visualElement, GameSaveItem gameSaveItem)
		{
			UQueryExtensions.Q<Label>(visualElement, "DisplayName", null).text = gameSaveItem.DisplayName;
			UQueryExtensions.Q<Label>(visualElement, "Timestamp", null).text = gameSaveItem.Timestamp;
			UQueryExtensions.Q<Label>(visualElement, "GameTime", null).text = gameSaveItem.GameTime;
		}

		// Token: 0x0400000B RID: 11
		public readonly VisualElementLoader _visualElementLoader;
	}
}
