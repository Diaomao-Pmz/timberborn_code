using System;
using System.IO;
using System.IO.Compression;
using Timberborn.CoreUI;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.Localization;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x0200000E RID: 14
	public class SaveFileValidator : IGameLoadValidator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public SaveFileValidator(GameSaveRepository gameSaveRepository, DialogBoxShower dialogBoxShower, ILoc loc)
		{
			this._gameSaveRepository = gameSaveRepository;
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002AED File Offset: 0x00000CED
		public int Priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void ValidateSave(SaveReference saveReference, Action continueCallback)
		{
			if (this.SaveIsValid(saveReference))
			{
				continueCallback();
				return;
			}
			string message = this._loc.T<string, string>(SaveFileValidator.InvalidFileLocKey, saveReference.SettlementReference.SettlementName, saveReference.SaveName);
			this._dialogBoxShower.Create().SetMessage(message).Show();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B48 File Offset: 0x00000D48
		public bool SaveIsValid(SaveReference saveReference)
		{
			bool result;
			try
			{
				using (Stream stream = this._gameSaveRepository.OpenSaveWithoutLogging(saveReference))
				{
					using (ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
					{
						result = (zipArchive.Entries.Count > 0);
					}
				}
			}
			catch (InvalidDataException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x04000037 RID: 55
		public static readonly string InvalidFileLocKey = "Saving.InvalidFile";

		// Token: 0x04000038 RID: 56
		public readonly GameSaveRepository _gameSaveRepository;

		// Token: 0x04000039 RID: 57
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400003A RID: 58
		public readonly ILoc _loc;
	}
}
