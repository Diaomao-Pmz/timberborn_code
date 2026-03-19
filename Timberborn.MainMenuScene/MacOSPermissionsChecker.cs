using System;
using Timberborn.ApplicationLifetime;
using Timberborn.CoreUI;
using Timberborn.FileSystem;
using UnityEngine;

namespace Timberborn.MainMenuScene
{
	// Token: 0x02000009 RID: 9
	public class MacOSPermissionsChecker
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000234C File Offset: 0x0000054C
		public MacOSPermissionsChecker(DialogBoxShower dialogBoxShower, IFileService fileService)
		{
			this._dialogBoxShower = dialogBoxShower;
			this._fileService = fileService;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002364 File Offset: 0x00000564
		public void CheckPermissions(Action onSuccessfulCheck)
		{
			if (this._fileService.HasDocumentsPermissions)
			{
				onSuccessfulCheck();
				return;
			}
			Debug.Log("Missing access to Documents folder. Shutting down.");
			this._dialogBoxShower.Create().SetLocalizedMessage(MacOSPermissionsChecker.MissingMacOSPermissionsLocKey).SetConfirmButton(new Action(MacOSPermissionsChecker.OpenDocumentsFolderSettings)).Show();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023BB File Offset: 0x000005BB
		public static void OpenDocumentsFolderSettings()
		{
			Application.OpenURL("x-apple.systempreferences:com.apple.preference.security?Privacy_DocumentsFolder");
			GameQuitter.Quit();
		}

		// Token: 0x04000011 RID: 17
		public static readonly string MissingMacOSPermissionsLocKey = "Saving.MissingMacOSPermissionsLocKey";

		// Token: 0x04000012 RID: 18
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x04000013 RID: 19
		public readonly IFileService _fileService;
	}
}
