using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MapRepositorySystem;
using Timberborn.Versioning;

namespace Timberborn.MapRepositorySystemUI
{
	// Token: 0x02000012 RID: 18
	public class MapVersionValidator : IMapLoadValidator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public MapVersionValidator(MapVersionCompatibilityService mapVersionCompatibilityService, DialogBoxShower dialogBoxShower, ILoc loc)
		{
			this._mapVersionCompatibilityService = mapVersionCompatibilityService;
			this._dialogBoxShower = dialogBoxShower;
			this._loc = loc;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002BFD File Offset: 0x00000DFD
		public int Priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C00 File Offset: 0x00000E00
		public void ValidateForNewGame(MapFileReference mapFileReference, Action continueCallback)
		{
			this.ValidateMap(mapFileReference, continueCallback, false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C0B File Offset: 0x00000E0B
		public void ValidateForMapEditor(MapFileReference mapFileReference, Action continueCallback)
		{
			this.ValidateMap(mapFileReference, continueCallback, true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C18 File Offset: 0x00000E18
		public void ValidateMap(MapFileReference mapFileReference, Action continueCallback, bool acceptSemiCompatibility)
		{
			Version mapVersionNumber = this._mapVersionCompatibilityService.GetMapVersionNumber(mapFileReference);
			if (this._mapVersionCompatibilityService.VersionIsFullyCompatible(mapVersionNumber))
			{
				continueCallback();
				return;
			}
			if (!this._mapVersionCompatibilityService.VersionIsSemiCompatible(mapVersionNumber))
			{
				this.ShowNonCompatibleDialog(mapVersionNumber);
				return;
			}
			if (acceptSemiCompatibility)
			{
				continueCallback();
				return;
			}
			this.ShowSemiCompatibleDialog(continueCallback, mapVersionNumber);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C70 File Offset: 0x00000E70
		public void ShowSemiCompatibleDialog(Action continueCallback, Version mapVersion)
		{
			string message = this._loc.T<Version, Version>(MapVersionValidator.SemiCompatibleMapVersionLocKey, mapVersion, GameVersions.CurrentVersion);
			this._dialogBoxShower.Create().SetMessage(message).SetConfirmButton(continueCallback).SetDefaultCancelButton(this._loc.T(CommonLocKeys.CancelKey)).Show();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public void ShowNonCompatibleDialog(Version mapVersion)
		{
			string message = this._loc.T<Version, Version>(MapVersionValidator.NonCompatibleMapVersionLocKey, mapVersion, GameVersions.CurrentVersion);
			this._dialogBoxShower.Create().SetMessage(message).Show();
		}

		// Token: 0x04000037 RID: 55
		public static readonly string SemiCompatibleMapVersionLocKey = "MapSelection.SemiCompatibleMapVersion";

		// Token: 0x04000038 RID: 56
		public static readonly string NonCompatibleMapVersionLocKey = "MapSelection.NonCompatibleMapVersion";

		// Token: 0x04000039 RID: 57
		public readonly MapVersionCompatibilityService _mapVersionCompatibilityService;

		// Token: 0x0400003A RID: 58
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400003B RID: 59
		public readonly ILoc _loc;
	}
}
