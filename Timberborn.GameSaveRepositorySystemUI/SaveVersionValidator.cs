using System;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Localization;
using Timberborn.StoreSystem;
using Timberborn.Versioning;
using Timberborn.VersioningSerialization;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000013 RID: 19
	public class SaveVersionValidator : IGameLoadValidator
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000030C3 File Offset: 0x000012C3
		public SaveVersionValidator(SaveVersionCompatibilityService saveVersionCompatibilityService, DialogBoxShower dialogBoxShower, ILoc loc, GameSaveDeserializer gameSaveDeserializer, VersionSerializer versionSerializer, IStore store)
		{
			this._saveVersionCompatibilityService = saveVersionCompatibilityService;
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
			this._gameSaveDeserializer = gameSaveDeserializer;
			this._versionSerializer = versionSerializer;
			this._store = store;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030F8 File Offset: 0x000012F8
		public int Priority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030FC File Offset: 0x000012FC
		public void ValidateSave(SaveReference saveReference, Action continueCallback)
		{
			Version saveVersionNumber = this.GetSaveVersionNumber(saveReference);
			if (this._saveVersionCompatibilityService.VersionIsFullyCompatible(saveVersionNumber))
			{
				continueCallback();
				return;
			}
			if (this._saveVersionCompatibilityService.VersionIsSemiCompatible(saveVersionNumber))
			{
				this.ShowSemiCompatibleDialog(saveVersionNumber, continueCallback);
				return;
			}
			this.ShowNonCompatibleDialog();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003143 File Offset: 0x00001343
		public Version GetSaveVersionNumber(SaveReference saveReference)
		{
			return this._gameSaveDeserializer.ReadFromSaveFileUnsafe<Version>(saveReference, this._versionSerializer);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003158 File Offset: 0x00001358
		public void ShowSemiCompatibleDialog(Version saveVersion, Action continueCallback)
		{
			string message = this._loc.T<string, string>(SaveVersionValidator.SemiCompatibleSaveVersionLocKey, saveVersion.NumericWithBranch, GameVersions.CurrentVersion.NumericWithBranch);
			if (this._saveVersionCompatibilityService.VersionIsForwardCompatible(saveVersion))
			{
				message = this.AddCompatibilityMessage(message);
			}
			this._dialogBoxShower.Create().SetMessage(message).SetConfirmButton(continueCallback).SetDefaultCancelButton().Show();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000031C2 File Offset: 0x000013C2
		public void ShowNonCompatibleDialog()
		{
			this._dialogBoxShower.Create().SetMessage(this.AddCompatibilityMessage(this._loc.T(SaveVersionValidator.NonCompatibleSaveVersionLocKey))).Show();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000031F0 File Offset: 0x000013F0
		public string AddCompatibilityMessage(string message)
		{
			return message + this._store.GetCompatibilityMessage();
		}

		// Token: 0x04000050 RID: 80
		public static readonly string SemiCompatibleSaveVersionLocKey = "Saving.SemiCompatibleSaveVersion";

		// Token: 0x04000051 RID: 81
		public static readonly string NonCompatibleSaveVersionLocKey = "Saving.NonCompatibleSaveVersion";

		// Token: 0x04000052 RID: 82
		public readonly SaveVersionCompatibilityService _saveVersionCompatibilityService;

		// Token: 0x04000053 RID: 83
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000054 RID: 84
		public readonly ILoc _loc;

		// Token: 0x04000055 RID: 85
		public readonly GameSaveDeserializer _gameSaveDeserializer;

		// Token: 0x04000056 RID: 86
		public readonly VersionSerializer _versionSerializer;

		// Token: 0x04000057 RID: 87
		public readonly IStore _store;
	}
}
