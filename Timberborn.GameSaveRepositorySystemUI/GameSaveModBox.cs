using System;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.SaveMetadataSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000008 RID: 8
	public class GameSaveModBox
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002447 File Offset: 0x00000647
		public GameSaveModBox(VisualElementLoader visualElementLoader, SimpleModItemFactory simpleModItemFactory, DialogBoxShower dialogBoxShower, GameSaveDeserializer gameSaveDeserializer, SaveMetadataSerializer saveMetadataSerializer)
		{
			this._visualElementLoader = visualElementLoader;
			this._simpleModItemFactory = simpleModItemFactory;
			this._dialogBoxShower = dialogBoxShower;
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._saveMetadataSerializer = saveMetadataSerializer;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002474 File Offset: 0x00000674
		public void Show(GameSaveItem gameSaveItem)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Modding/GameSaveModBox");
			SaveMetadata metadata = this._gameSaveDeserializer.ReadFromSaveFile<SaveMetadata>(gameSaveItem.SaveReference, this._saveMetadataSerializer);
			this._simpleModItemFactory.FillSavedMods(UQueryExtensions.Q<ScrollView>(visualElement, "SavedMods", null), metadata);
			this._dialogBoxShower.Create().AddContent(visualElement).Show();
		}

		// Token: 0x0400001A RID: 26
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001B RID: 27
		public readonly SimpleModItemFactory _simpleModItemFactory;

		// Token: 0x0400001C RID: 28
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400001D RID: 29
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x0400001E RID: 30
		public readonly SaveMetadataSerializer _saveMetadataSerializer;
	}
}
