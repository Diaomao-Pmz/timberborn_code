using System;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Modding;
using Timberborn.SaveMetadataSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000010 RID: 16
	public class SaveModsValidator : IGameLoadValidator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002EBE File Offset: 0x000010BE
		public SaveModsValidator(GameSaveDeserializer gameSaveDeserializer, VisualElementLoader visualElementLoader, SaveMetadataSerializer saveMetadataSerializer, ModRepository modRepository, DialogBoxShower dialogBoxShower, SimpleModItemFactory simpleModItemFactory)
		{
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._visualElementLoader = visualElementLoader;
			this._saveMetadataSerializer = saveMetadataSerializer;
			this._modRepository = modRepository;
			this._dialogBoxShower = dialogBoxShower;
			this._simpleModItemFactory = simpleModItemFactory;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002EF3 File Offset: 0x000010F3
		public int Priority
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EF8 File Offset: 0x000010F8
		public void ValidateSave(SaveReference saveReference, Action continueCallback)
		{
			SaveMetadata metadata = this._gameSaveDeserializer.ReadFromSaveFile<SaveMetadata>(saveReference, this._saveMetadataSerializer);
			if (this.ModsAreCompatible(metadata))
			{
				continueCallback();
				return;
			}
			this.ShowModsIncompatibilityDialog(metadata, continueCallback);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F30 File Offset: 0x00001130
		public void ShowModsIncompatibilityDialog(SaveMetadata metadata, Action continueCallback)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Modding/ModIncompatibilityDialogBox");
			this._simpleModItemFactory.FillActiveMods(UQueryExtensions.Q<ScrollView>(visualElement, "ActiveMods", null));
			this._simpleModItemFactory.FillSavedMods(UQueryExtensions.Q<ScrollView>(visualElement, "SavedMods", null), metadata);
			this._dialogBoxShower.Create().AddContent(visualElement).SetMaxWidth(SaveModsValidator.IncompatibilityDialogBoxMaxWidth).SetConfirmButton(continueCallback).SetDefaultCancelButton().Show();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FAC File Offset: 0x000011AC
		public bool ModsAreCompatible(SaveMetadata metadata)
		{
			if (metadata != null)
			{
				foreach (ModReference modReference in metadata.Mods)
				{
					if (this._modRepository.ModIsNotEnabled(modReference.Id) || this._modRepository.ModIsOnDifferentVersion(modReference.Id, modReference.Version))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000045 RID: 69
		public static readonly int IncompatibilityDialogBoxMaxWidth = 1200;

		// Token: 0x04000046 RID: 70
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x04000047 RID: 71
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000048 RID: 72
		public readonly SaveMetadataSerializer _saveMetadataSerializer;

		// Token: 0x04000049 RID: 73
		public readonly ModRepository _modRepository;

		// Token: 0x0400004A RID: 74
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400004B RID: 75
		public readonly SimpleModItemFactory _simpleModItemFactory;
	}
}
